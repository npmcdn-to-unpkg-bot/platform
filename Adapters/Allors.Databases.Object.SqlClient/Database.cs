// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Xml;

    using Allors.Databases.Object.SqlClient.Caching;
    using Allors.Meta;
    using Allors.Populations;

    using Microsoft.SqlServer.Server;

    public class Database : IDatabase
    {
        private const string ConnectionStringsKey = "allors";

        private readonly object lockObject = new object();

        private readonly IObjectFactory objectFactory;

        private readonly Dictionary<IObjectType, object> concreteClassesByObjectType;

        private readonly string id;

        private readonly IWorkspaceFactory workspaceFactory;

        private readonly string connectionString;

        private readonly int commandTimeout;

        private readonly IsolationLevel isolationLevel;

        private readonly Dictionary<IObjectType, IRoleType[]> sortedUnitRolesByObjectType;

        private readonly ICache cache;

        private readonly ObjectIds objectIds;

        private readonly string schemaName;

        private Mapping mapping;

        private Dictionary<string, object> properties;

        private bool? isValid;

        public Database(Configuration configuration)
        {
            this.objectFactory = configuration.ObjectFactory;
            if (!this.objectFactory.MetaPopulation.IsValid)
            {
                throw new ArgumentException("Domain is invalid");
            }

            this.concreteClassesByObjectType = new Dictionary<IObjectType, object>();

            this.workspaceFactory = configuration.WorkspaceFactory;
            this.connectionString = configuration.ConnectionString;
            this.commandTimeout = configuration.CommandTimeout;
            this.isolationLevel = configuration.IsolationLevel;

            this.sortedUnitRolesByObjectType = new Dictionary<IObjectType, IRoleType[]>();

            this.cache = configuration.CacheFactory.CreateCache(this);

            var connectionStringBuilder = new SqlConnectionStringBuilder(this.ConnectionString);
            var applicationName = connectionStringBuilder.ApplicationName.Trim();
            if (!string.IsNullOrWhiteSpace(applicationName))
            {
                this.id = applicationName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(connectionStringBuilder.InitialCatalog))
                {
                    this.id = connectionStringBuilder.InitialCatalog.ToLowerInvariant();
                }
                else
                {
                    using (var connection = new SqlConnection(this.ConnectionString))
                    {
                        connection.Open();
                        this.id = connection.Database.ToLowerInvariant();
                    }
                }
            }

            this.schemaName = (configuration.SchemaName ?? "allors").ToLowerInvariant();
            this.objectIds = configuration.ObjectIds ?? new ObjectIdsInteger();
        }

        public event ObjectNotLoadedEventHandler ObjectNotLoaded;

        public event RelationNotLoadedEventHandler RelationNotLoaded;

        public string Id
        {
            get
            {
                return this.id;
            }
        }
        
        public string SchemaName
        {
            get
            {
                return this.schemaName;
            }
        }

        public IObjectFactory ObjectFactory
        {
            get
            {
                return this.objectFactory;
            }
        }

        public IMetaPopulation MetaPopulation
        {
            get
            {
                return this.objectFactory.MetaPopulation;
            }
        }

        public bool IsDatabase
        {
            get
            {
                return true;
            }
        }

        public bool IsWorkspace
        {
            get
            {
                return false;
            }
        }

        public bool IsShared
        {
            get
            {
                return true;
            }
        }

        public bool IsValid
        {
            get
            {
                if (!this.isValid.HasValue)
                {
                    lock (this.lockObject)
                    {
                        if (!this.isValid.HasValue)
                        {
                            var validate = this.Validate();
                            return validate.IsValid;
                        }
                    }
                }

                return this.isValid.Value;
            }
        }

        internal string AscendingSortAppendix
        {
            get
            {
                return null;
            }
        }

        internal string DescendingSortAppendix
        {
            get
            {
                return null;
            }
        }

        internal string Except
        {
            get
            {
                return "EXCEPT";
            }
        }

        internal ICache Cache
        {
            get
            {
                return this.cache;
            }
        }

        internal string ConnectionString
        {
            get
            {
                if (this.connectionString == null)
                {
                    return
                        System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringsKey]
                            .ConnectionString;
                }

                return this.connectionString;
            }
        }

        internal int CommandTimeout
        {
            get
            {
                return this.commandTimeout;
            }
        }

        internal IsolationLevel IsolationLevel
        {
            get
            {
                return this.isolationLevel;
            }
        }

        internal ObjectIds ObjectIds
        {
            get { return this.objectIds; }
        }

        internal Mapping Mapping
        {
            get
            {
                if (this.ObjectFactory.MetaPopulation != null)
                {
                    if (this.mapping == null)
                    {
                        if (this.ObjectIds is ObjectIdsInteger)
                        {
                            this.mapping = new Mapping(this, true);
                        }
                        else if (this.ObjectIds is ObjectIdsLong)
                        {
                            this.mapping = new Mapping(this, false);
                        }
                        else
                        {
                            throw new NotSupportedException("ObjectIds of type " + this.ObjectIds.GetType() + " are not supported.");
                        }
                    }
                }

                return this.mapping;
            }
        }
        
        public object this[string name]
        {
            get
            {
                if (this.properties == null)
                {
                    return null;
                }

                object value;
                this.properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.properties.Remove(name);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public ISession CreateSession()
        {
            return this.CreateDatabaseSession();
        }

        public void Init()
        {
            try
            {
                new Initialization(this).Execute();
            }
            finally
            {
                this.properties = null;
                this.ResetSchema();
                this.Cache.Invalidate();
            }
        }

        public void Load(XmlReader reader)
        {
            this.Init();

            var session = this.CreateManagementSession();
            try
            {
                var load = this.CreateLoad(this.ObjectNotLoaded, this.RelationNotLoaded, reader);
                load.Execute(session);
                session.Commit();
            }
            catch
            {
                session.Rollback();
                this.Init();
                throw;
            }
        }

        public void Save(XmlWriter writer)
        {
            var session = this.CreateManagementSession();
            try
            {
                var save = this.CreateSave(writer);
                save.Execute(session);
            }
            finally
            {
                session.Rollback();
            }
        }

        public override string ToString()
        {
            return "Population[driver=Sql, type=Connected, id=" + this.GetHashCode() + "]";
        }

        public IWorkspace CreateWorkspace()
        {
            if (this.workspaceFactory == null)
            {
                throw new Exception("No workspacefactory defined");
            }

            return this.workspaceFactory.CreateWorkspace(this);
        }

        public Validation Validate()
        {
            var validateResult = new Validation(this);
            this.isValid = validateResult.IsValid;
            return validateResult;
        }

        IDatabaseSession IDatabase.CreateSession()
        {
            return this.CreateDatabaseSession();
        }

        internal bool ContainsConcreteClass(IObjectType objectType, IObjectType concreteClass)
        {
            object concreteClassOrClasses;
            if (!this.concreteClassesByObjectType.TryGetValue(objectType, out concreteClassOrClasses))
            {
                if (objectType.IsClass)
                {
                    concreteClassOrClasses = objectType;
                    this.concreteClassesByObjectType[objectType] = concreteClassOrClasses;
                }
                else
                {
                    concreteClassOrClasses = new HashSet<IObjectType>(((IInterface)objectType).Subclasses);
                    this.concreteClassesByObjectType[objectType] = concreteClassOrClasses;
                }
            }

            if (concreteClassOrClasses is IObjectType)
            {
                return concreteClass.Equals(concreteClassOrClasses);
            }

            var concreteClasses = (HashSet<IObjectType>)concreteClassOrClasses;
            return concreteClasses.Contains(concreteClass);
        }

        internal IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<ObjectId> objectids)
        {
            return new ObjectDataRecord(this.mapping, objectids);
        }

        internal IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<Reference> strategies)
        {
            return new CompositesRoleDataRecords(this.mapping, strategies);
        }

        internal IEnumerable<SqlDataRecord> CreateRelationTable(IEnumerable<CompositeRelation> relations)
        {
            return new CompositeRoleDataRecords(this.mapping, relations);
        }

        internal IEnumerable<SqlDataRecord> CreateRelationTable(IRoleType roleType, IEnumerable<UnitRelation> relations)
        {
            return new UnitRoleDataRecords(this, roleType, relations);
        }

        internal Type GetDomainType(IObjectType objectType)
        {
            return this.ObjectFactory.GetTypeForObjectType(objectType);
        }

        internal IRoleType[] GetSortedUnitRolesByObjectType(IObjectType objectType)
        {
            IRoleType[] sortedUnitRoles;
            if (!this.sortedUnitRolesByObjectType.TryGetValue(objectType, out sortedUnitRoles))
            {
                var sortedUnitRoleList =
                    new List<IRoleType>(((IComposite)objectType).RoleTypes.Where(r => r.ObjectType.IsUnit));
                sortedUnitRoleList.Sort(MetaObjectComparer.ById);
                sortedUnitRoles = sortedUnitRoleList.ToArray();
                this.sortedUnitRolesByObjectType[objectType] = sortedUnitRoles;
            }

            return sortedUnitRoles;
        }

        // TODO: inline
        internal SqlMetaData GetSqlMetaData(string name, IRoleType roleType)
        {
            var unit = (IUnit)roleType.ObjectType;
            switch (unit.UnitTag)
            {
                case UnitTags.AllorsString:
                    if (roleType.Size == -1 || roleType.Size > 4000)
                    {
                        return new SqlMetaData(name, SqlDbType.NVarChar, -1);
                    }

                    return new SqlMetaData(name, SqlDbType.NVarChar, roleType.Size.Value);
                case UnitTags.AllorsInteger:
                    return new SqlMetaData(name, SqlDbType.Int);
                case UnitTags.AllorsDecimal:
                    return new SqlMetaData(name, SqlDbType.Decimal, (byte)roleType.Precision.Value, (byte)roleType.Scale.Value);
                case UnitTags.AllorsFloat:
                    return new SqlMetaData(name, SqlDbType.Float);
                case UnitTags.AllorsBoolean:
                    return new SqlMetaData(name, SqlDbType.Bit);
                case UnitTags.AllorsDateTime:
                    return new SqlMetaData(name, SqlDbType.DateTime2);
                case UnitTags.AllorsUnique:
                    return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
                case UnitTags.AllorsBinary:
                    if (roleType.Size == -1 || roleType.Size > 8000)
                    {
                        return new SqlMetaData(name, SqlDbType.VarBinary, -1);
                    }

                    return new SqlMetaData(name, SqlDbType.VarBinary, (long)roleType.Size);
                default:
                    throw new Exception("!UNKNOWN VALUE TYPE!");
            }
        }

        private ManagementSession CreateSqlClientManagementSession()
        {
            return new ManagementSession(this);
        }

        private void ResetSchema()
        {
            this.mapping = null;
        }

        private IDatabaseSession CreateDatabaseSession()
        {
            if (!this.IsValid)
            {
                throw new Exception("Schema is invalid.");
            }


            //if (this.Mapping.MappingValidationErrors.HasErrors)
            //{
            //    var errors = new StringBuilder();
            //    foreach (var error in this.Mapping.MappingValidationErrors.Errors)
            //    {
            //        errors.Append("\n");
            //        errors.Append(error.Message);
            //    }

            //    throw new MappingValidationException(
            //        this.Mapping.MappingValidationErrors,
            //        "Database schema is not compatible with domain.\nUpgrade manually or use Save & Load.\n" + errors);
            //}

            return new DatabaseSession(this);
        }
       
        private ManagementSession CreateManagementSession()
        {
            return this.CreateSqlClientManagementSession();
        }

        private Load CreateLoad(ObjectNotLoadedEventHandler objectNotLoaded, RelationNotLoadedEventHandler relationNotLoaded, XmlReader reader)
        {
            return new Load(this, objectNotLoaded, relationNotLoaded, reader);
        }

        private Save CreateSave(XmlWriter writer)
        {
            return new Save(this, writer);
        }
    }
}