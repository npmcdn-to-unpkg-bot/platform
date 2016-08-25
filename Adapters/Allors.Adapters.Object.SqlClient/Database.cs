// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Xml;

    using Adapters.Object.SqlClient.Caching;

    using Allors;
    using Allors.Meta;

    using Microsoft.SqlServer.Server;

    public class Database : IDatabase
    {
        private const string ConnectionStringsKey = "allors";

        public static readonly long[] EmptyObjectIds = { };

        private readonly object lockObject = new object();

        private readonly IObjectFactory objectFactory;

        private readonly Dictionary<IObjectType, HashSet<IObjectType>> concreteClassesByObjectType;

        private readonly string connectionString;

        private readonly Dictionary<IObjectType, IRoleType[]> sortedUnitRolesByObjectType;

        private Mapping mapping;

        private ConcurrentDictionary<string, object> properties;

        private bool? isValid;

        private string validationMessage;

        private IConnectionFactory connectionFactory;

        private IConnectionFactory managementConnectionFactory;

        private ICacheFactory cacheFactory;

        public Database(Configuration configuration)
        {
            this.objectFactory = configuration.ObjectFactory;
            if (!this.objectFactory.MetaPopulation.IsValid)
            {
                throw new ArgumentException("Domain is invalid");
            }

            this.ConnectionFactory = configuration.ConnectionFactory;
            this.ManagementConnectionFactory = configuration.ManagementConnectionFactory;

            this.concreteClassesByObjectType = new Dictionary<IObjectType, HashSet<IObjectType>>();
            
            this.connectionString = configuration.ConnectionString;
            this.CommandTimeout = configuration.CommandTimeout;
            this.IsolationLevel = configuration.IsolationLevel;

            this.sortedUnitRolesByObjectType = new Dictionary<IObjectType, IRoleType[]>();

            this.CacheFactory = configuration.CacheFactory;
            this.Cache = this.CacheFactory.CreateCache();

            var connectionStringBuilder = new SqlConnectionStringBuilder(this.ConnectionString);
            var applicationName = connectionStringBuilder.ApplicationName.Trim();
            if (!string.IsNullOrWhiteSpace(applicationName))
            {
                this.Id = applicationName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(connectionStringBuilder.InitialCatalog))
                {
                    this.Id = connectionStringBuilder.InitialCatalog.ToLowerInvariant();
                }
                else
                {
                    using (var connection = new SqlConnection(this.ConnectionString))
                    {
                        connection.Open();
                        this.Id = connection.Database.ToLowerInvariant();
                    }
                }
            }

            this.SchemaName = (configuration.SchemaName ?? "allors").ToLowerInvariant();
        }

        public event ObjectNotLoadedEventHandler ObjectNotLoaded;

        public event RelationNotLoadedEventHandler RelationNotLoaded;

        public IConnectionFactory ConnectionFactory
        {
            get
            {
                return this.connectionFactory ?? (this.connectionFactory = new DefaultConnectionFactory());
            }

            set
            {
                this.connectionFactory = value;
            }
        }

        public IConnectionFactory ManagementConnectionFactory
        {
            get
            {
                return this.managementConnectionFactory ?? (this.managementConnectionFactory = new DefaultConnectionFactory());
            }

            set
            {
                this.managementConnectionFactory = value;
            }
        }

        public ICacheFactory CacheFactory
        {
            get
            {
                return this.cacheFactory;
            }

            set
            {
                this.cacheFactory = value ?? (this.cacheFactory = new DefaultCacheFactory());
            }
        }

        public string Id { get; }

        public string SchemaName { get; }

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
                            this.validationMessage = validate.Message;
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

        internal ICache Cache { get; }

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

        internal int CommandTimeout { get; }

        internal IsolationLevel IsolationLevel { get; }

        internal Mapping Mapping
        {
            get
            {
                if (this.ObjectFactory.MetaPopulation != null)
                {
                    if (this.mapping == null)
                    {
                        this.mapping = new Mapping(this);
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
                    this.properties = new ConcurrentDictionary<string, object>();
                }

                if (value == null)
                {
                    object removed;
                    this.properties.TryRemove(name, out removed);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public ISession CreateSession()
        {
            var connection = this.ConnectionFactory.Create(this);
            return this.CreateSession(connection);
        }

        public ISession CreateSession(Connection connection)
        {
            if (!this.IsValid)
            {
                throw new Exception(this.validationMessage);
            }

            return new Session(this, connection);
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
            lock (this)
            {
                var load = new Load(this, this.ObjectNotLoaded, this.RelationNotLoaded, reader);
                load.Execute();
            }
        }

        public void Save(XmlWriter writer)
        {
            lock (this)
            {
                var session = new ManagementSession(this, this.ManagementConnectionFactory);
                try
                {
                    var save = new Save(this, writer);
                    save.Execute(session);
                }
                finally
                {
                    session.Rollback();
                }
            }
        }

        public override string ToString()
        {
            return "Population[driver=Sql, type=Connected, id=" + this.GetHashCode() + "]";
        }

        public Validation Validate()
        {
            var validateResult = new Validation(this);
            this.isValid = validateResult.IsValid;
            return validateResult;
        }

        ISession IDatabase.CreateSession()
        {
            return this.CreateSession();
        }

        internal bool ContainsConcreteClass(IObjectType container, IObjectType containee)
        {
            if (container.IsClass)
            {
                return container.Equals(containee);
            }

            HashSet<IObjectType> concreteClasses;
            if (!this.concreteClassesByObjectType.TryGetValue(container, out concreteClasses))
            {
                concreteClasses = new HashSet<IObjectType>(((IInterface)container).Subclasses);
                this.concreteClassesByObjectType[container] = concreteClasses;
            }

            return concreteClasses.Contains(containee);
        }

        internal IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<long> objectids)
        {
            return new ObjectDataRecord(this.mapping, objectids);
        }

        internal IEnumerable<SqlDataRecord> CreateVersionedObjectTable(Dictionary<long, long> versionedObjects)
        {
            return new VersionedObjectDataRecord(this.mapping, versionedObjects);
        }

        internal IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<Reference> references)
        {
            return new CompositesRoleDataRecords(this.mapping, references);
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
                var sortedUnitRoleList = new List<IRoleType>(((IComposite)objectType).RoleTypes.Where(r => r.ObjectType.IsUnit));
                sortedUnitRoleList.Sort();
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
                case UnitTags.String:
                    if (roleType.Size == -1 || roleType.Size > 4000)
                    {
                        return new SqlMetaData(name, SqlDbType.NVarChar, -1);
                    }

                    return new SqlMetaData(name, SqlDbType.NVarChar, roleType.Size.Value);
                case UnitTags.Integer:
                    return new SqlMetaData(name, SqlDbType.Int);
                case UnitTags.Decimal:
                    return new SqlMetaData(name, SqlDbType.Decimal, (byte)roleType.Precision.Value, (byte)roleType.Scale.Value);
                case UnitTags.Float:
                    return new SqlMetaData(name, SqlDbType.Float);
                case UnitTags.Boolean:
                    return new SqlMetaData(name, SqlDbType.Bit);
                case UnitTags.DateTime:
                    return new SqlMetaData(name, SqlDbType.DateTime2);
                case UnitTags.Unique:
                    return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
                case UnitTags.Binary:
                    if (roleType.Size == -1 || roleType.Size > 8000)
                    {
                        return new SqlMetaData(name, SqlDbType.VarBinary, -1);
                    }

                    return new SqlMetaData(name, SqlDbType.VarBinary, (long)roleType.Size);
                default:
                    throw new Exception("!UNKNOWN VALUE TYPE!");
            }
        }

        private void ResetSchema()
        {
            this.mapping = null;
        }
    }
}