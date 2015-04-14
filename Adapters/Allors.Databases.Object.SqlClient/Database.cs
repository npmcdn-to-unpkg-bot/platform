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
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using Allors.Databases.Object.SqlClient.Caching;
    using Allors.Meta;
    using Allors.Populations;

    using Microsoft.SqlServer.Server;

    public abstract class Database : IDatabase
    {
        private const string ConnectionStringsKey = "allors";

        private readonly IObjectFactory objectFactory;

        private readonly Dictionary<IObjectType, object> concreteClassesByIObjectType;

        private readonly string id;

        private readonly IWorkspaceFactory workspaceFactory;

        private readonly string connectionString;

        private readonly int commandTimeout;

        private readonly IsolationLevel isolationLevel;

        private readonly Dictionary<IObjectType, IRoleType[]> sortedUnitRolesByIObjectType;

        private readonly ICache cache;

        private readonly CommandFactories commandFactories;

        private Dictionary<string, object> properties;

        protected Database(Configuration configuration)
        {
            this.objectFactory = configuration.ObjectFactory;
            this.concreteClassesByIObjectType = new Dictionary<IObjectType, object>();

            this.workspaceFactory = configuration.WorkspaceFactory;
            this.connectionString = configuration.ConnectionString;
            this.commandTimeout = configuration.CommandTimeout;
            this.isolationLevel = configuration.IsolationLevel;

            this.sortedUnitRolesByIObjectType = new Dictionary<IObjectType, IRoleType[]>();

            this.cache = configuration.CacheFactory.CreateCache(this);

            this.commandFactories = new CommandFactories(this);

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

        internal abstract IObjectIds AllorsObjectIds { get; }

        internal CommandFactories SqlClientCommandFactories
        {
            get
            {
                return this.commandFactories;
            }
        }

        internal Schema Schema
        {
            get
            {
                return this.SqlClientSchema;
            }
        }

        internal abstract Schema SqlClientSchema { get; }

        internal CommandFactories CommandFactories
        {
            get
            {
                return this.commandFactories;
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
            this.Init(true);
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

        IDatabaseSession Allors.IDatabase.CreateSession()
        {
            return this.CreateDatabaseSession();
        }

        internal bool ContainsConcreteClass(IObjectType objectType, IObjectType concreteClass)
        {
            object concreteClassOrClasses;
            if (!this.concreteClassesByIObjectType.TryGetValue(objectType, out concreteClassOrClasses))
            {
                if (objectType.IsClass)
                {
                    concreteClassOrClasses = objectType;
                    this.concreteClassesByIObjectType[objectType] = concreteClassOrClasses;
                }
                else
                {
                    concreteClassOrClasses = new HashSet<IObjectType>(((IInterface)objectType).Subclasses);
                    this.concreteClassesByIObjectType[objectType] = concreteClassOrClasses;
                }
            }

            if (concreteClassOrClasses is IObjectType)
            {
                return concreteClass.Equals(concreteClassOrClasses);
            }

            var concreteClasses = (HashSet<IObjectType>)concreteClassOrClasses;
            return concreteClasses.Contains(concreteClass);
        }

        internal abstract IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<ObjectId> objectids);

        internal abstract IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<Reference> strategies);

        internal abstract IEnumerable<SqlDataRecord> CreateObjectTable(Dictionary<Reference, Roles> rolesByReference);

        internal abstract IEnumerable<SqlDataRecord> CreateRelationTable(IEnumerable<CompositeRelation> compositeRelations);

        internal abstract IEnumerable<SqlDataRecord> CreateRelationTable(IRoleType roleType, IEnumerable<UnitRelation> unitRelations);

        internal void Recover()
        {
            if (!ObjectFactory.MetaPopulation.IsValid)
            {
                throw new ArgumentException("Domain is invalid");
            }

            var session = this.CreateManagementSession();
            try
            {
                this.DropIndexes(session);
                this.DropTriggers(session);
                this.DropProcedures(session);
                this.DropFunctions(session);
                this.DropUserDefinedTypes(session);

                this.CreateUserDefinedTypes(session);
                this.CreateFunctions(session);
                this.CreateProcedures(session);
                this.CreateTriggers(session);
                this.CreateIndexes(session);

                session.Commit();
            }
            catch
            {
                session.Rollback();
                throw;
            }
            finally
            {
                this.ResetSchema();
                this.Cache.Invalidate();
            }
        }

        internal Type GetDomainType(IObjectType objectType)
        {
            return this.ObjectFactory.GetTypeForObjectType(objectType);
        }

        internal IRoleType[] GetSortedUnitRolesByIObjectType(IObjectType objectType)
        {
            IRoleType[] sortedUnitRoles;
            if (!this.sortedUnitRolesByIObjectType.TryGetValue(objectType, out sortedUnitRoles))
            {
                var sortedUnitRoleList =
                    new List<IRoleType>(((IComposite)objectType).RoleTypes.Where(r => r.ObjectType.IsUnit));
                sortedUnitRoleList.Sort(MetaObjectComparer.ById);
                sortedUnitRoles = sortedUnitRoleList.ToArray();
                this.sortedUnitRolesByIObjectType[objectType] = sortedUnitRoles;
            }

            return sortedUnitRoles;
        }

        internal DbConnection CreateDbConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        internal void DropIndex(ManagementSession session, SchemaTable table, SchemaColumn column)
        {
            var indexName = SqlClient.Schema.AllorsPrefix + table.Name + "_" + column.Name;

            var sql = new StringBuilder();
            sql.Append("IF EXISTS (\n");
            sql.Append("    SELECT *\n");
            sql.Append("    FROM sysindexes\n");
            sql.Append("    WHERE   name = '" + indexName + "'\n");
            sql.Append("            AND OBJECT_NAME(id) = N'" + table.Name + "'\n");
            sql.Append(")\n");

            sql.Append("    DROP INDEX " + table + "." + indexName);
            session.ExecuteSql(sql.ToString());
        }

        internal SqlMetaData GetSqlMetaData(string name, SchemaColumn column)
        {
            switch (column.DbType)
            {
                case DbType.String:
                    if (column.Size == -1 || column.Size > 4000)
                    {
                        return new SqlMetaData(name, SqlDbType.NVarChar, -1);
                    }

                    return new SqlMetaData(name, SqlDbType.NVarChar, column.Size.Value);
                case DbType.Int32:
                    return new SqlMetaData(name, SqlDbType.Int);
                case DbType.Decimal:
                    return new SqlMetaData(
                        name,
                        SqlDbType.Decimal,
                        (byte)column.Precision.Value,
                        (byte)column.Scale.Value);
                case DbType.Double:
                    return new SqlMetaData(name, SqlDbType.Float);
                case DbType.Boolean:
                    return new SqlMetaData(name, SqlDbType.Bit);
                case DbType.DateTime2:
                    return new SqlMetaData(name, SqlDbType.DateTime2);
                case DbType.Guid:
                    return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
                case DbType.Binary:
                    if (column.Size == -1 || column.Size > 8000)
                    {
                        return new SqlMetaData(name, SqlDbType.VarBinary, -1);
                    }

                    return new SqlMetaData(name, SqlDbType.VarBinary, (long)column.Size);
                default:
                    throw new Exception("!UNKNOWN VALUE TYPE!");
            }
        }

        internal string GetSqlType(SchemaColumn column)
        {
            switch (column.DbType)
            {
                case DbType.String:
                    if (column.Size == -1 || column.Size > 4000)
                    {
                        return "NVARCHAR(MAX) ";
                    }

                    return "NVARCHAR(" + column.Size + ") ";
                case DbType.Int32:
                    return "INT ";
                case DbType.Decimal:
                    return "DECIMAL(" + column.Precision + "," + column.Scale + ") ";
                case DbType.Double:
                    return "FLOAT ";
                case DbType.Boolean:
                    return "BIT ";
                case DbType.DateTime2:
                    return "DATETIME2 ";
                case DbType.Guid:
                    return "UNIQUEIDENTIFIER ";
                case DbType.Binary:
                    if (column.Size == -1 || column.Size > 8000)
                    {
                        return "VARBINARY(MAX) ";
                    }

                    return "VARBINARY(" + column.Size + ") ";
                default:
                    return "!UNKNOWN VALUE TYPE!";
            }
        }

        internal string GetSqlType(DbType type)
        {
            switch (type)
            {
                case DbType.Int32:
                    return "INT ";
                default:
                    return "!UNKNOWN DBTYPE!";
            }
        }

        internal ManagementSession CreateSqlClientManagementSession()
        {
            return new ManagementSession(this);
        }

        protected abstract void ResetSchema();

        private IDatabaseSession CreateDatabaseSession()
        {
            if (Schema.SchemaValidationErrors.HasErrors)
            {
                var errors = new StringBuilder();
                foreach (var error in Schema.SchemaValidationErrors.Errors)
                {
                    errors.Append("\n");
                    errors.Append(error.Message);
                }

                throw new SchemaValidationException(
                    Schema.SchemaValidationErrors,
                    "Database schema is not compatible with domain.\nUpgrade manually or use Save & Load.\n" + errors);
            }

            var session = this.CreateSqlSession();
            return session;
        }

        private void Init(bool allowTruncate)
        {
            if (!ObjectFactory.MetaPopulation.IsValid)
            {
                throw new ArgumentException("Domain is invalid");
            }

            var session = this.CreateManagementSession();
            try
            {
                if (allowTruncate && !this.Schema.SchemaValidationErrors.HasErrors)
                {
                    this.TruncateTables(session);
                }
                else
                {
                    this.DropTriggers(session);
                    this.DropProcedures(session);
                    this.DropFunctions(session);
                    this.DropUserDefinedTypes(session);
                    this.ResetSequence(session);
                    this.DropTables(session);
                    this.CreateTables(session);
                    this.CreateUserDefinedTypes(session);
                    this.CreateFunctions(session);
                    this.CreateProcedures(session);
                    this.CreateTriggers(session);
                    this.CreateIndexes(session);
                }

                session.Commit();
            }
            catch
            {
                session.Rollback();
                throw;
            }
            finally
            {
                this.properties = null;
                this.ResetSchema();
                this.Cache.Invalidate();
            }
        }

        private void TruncateTables(ManagementSession session)
        {
            this.ResetSequence(session);

            foreach (SchemaTable table in Schema)
            {
                switch (table.Kind)
                {
                    case SchemaTableKind.System:
                    case SchemaTableKind.Object:
                    case SchemaTableKind.Relation:
                        this.TruncateTables(session, table);
                        break;
                }
            }
        }

        private void CreateFunctions(ManagementSession session)
        {
        }

        private void CreateIndexes(ManagementSession session)
        {
            foreach (SchemaTable table in Schema)
            {
                foreach (SchemaColumn column in table)
                {
                    if (column.IndexType != SchemaIndexType.None)
                    {
                        this.CreateIndex(session, table, column);
                    }
                }
            }
        }

        private void DropIndexes(ManagementSession session)
        {
            foreach (SchemaTable table in Schema)
            {
                foreach (SchemaColumn column in table)
                {
                    if (column.IndexType != SchemaIndexType.None)
                    {
                        this.DropIndex(session, table, column);
                    }
                }
            }
        }

        private void CreateProcedures(ManagementSession session)
        {
            foreach (var procedure in Schema.Procedures)
            {
                this.CreateProcedure(session, procedure);
            }
        }

        private void CreateTables(ManagementSession session)
        {
            foreach (SchemaTable table in Schema)
            {
                this.CreateTable(session, table);
            }
        }

        private void CreateTriggers(ManagementSession session)
        {
        }

        private void DropFunctions(ManagementSession session)
        {
        }

        private void DropTables(ManagementSession session)
        {
            foreach (SchemaTable table in Schema)
            {
                this.DropTable(session, table);
            }
        }

        private void DropTriggers(ManagementSession session)
        {
        }

        private void ResetSequence(ManagementSession session)
        {
        }

        private ManagementSession CreateManagementSession()
        {
            return this.CreateSqlClientManagementSession();
        }

        private void CreateUserDefinedTypes(ManagementSession session)
        {
            var sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.ObjectTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.ObjectTableObject + " " + this.GetSqlType(this.Schema.ObjectId) + ")\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.CompositeRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " " + this.GetSqlType(this.Schema.RoleId) + ")\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.StringRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " NVARCHAR(MAX))\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.IntegerRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " INT)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.FloatRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " FLOAT)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.DateTimeRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " DATETIME2)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.BooleanRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " BIT)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.UniqueRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " UNIQUEIDENTIFIER)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.BinaryRelationTable + " AS TABLE\n");
            sql.Append(
                "(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId)
                + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " VARBINARY(MAX))\n");
            session.ExecuteSql(sql.ToString());

            foreach (var precisionEntry in this.SqlClientSchema.DecimalRelationTableByScaleByPrecision)
            {
                var precision = precisionEntry.Key;
                foreach (var scaleEntry in precisionEntry.Value)
                {
                    var scale = scaleEntry.Key;
                    var decimalRelationTable = scaleEntry.Value;

                    sql = new StringBuilder();
                    sql.Append("CREATE TYPE " + decimalRelationTable + " AS TABLE\n");
                    sql.Append(
                        "(" + this.SqlClientSchema.RelationTableAssociation + " "
                        + this.GetSqlType(this.Schema.AssociationId) + ",\n");
                    sql.Append(this.SqlClientSchema.RelationTableRole + " DECIMAL(" + precision + "," + scale + ") )\n");
                    session.ExecuteSql(sql.ToString());
                }
            }
        }

        private IDatabaseSession CreateSqlSession()
        {
            return new DatabaseSession(this);
        }

        private void DropUserDefinedTypes(ManagementSession session)
        {
            this.DropUserDefinedType(session, this.SqlClientSchema.ObjectTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.CompositeRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.StringRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.IntegerRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.FloatRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.BooleanRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.DateTimeRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.UniqueRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.BinaryRelationTable);
            foreach (var precisionEntry in this.SqlClientSchema.DecimalRelationTableByScaleByPrecision)
            {
                foreach (var scaleEntry in precisionEntry.Value)
                {
                    var decimalRelationTable = scaleEntry.Value;
                    this.DropUserDefinedType(session, decimalRelationTable);
                }
            }
        }

        private void DropTable(ManagementSession session, SchemaTable schemaTable)
        {
            var sql = new StringBuilder();
            sql.Append("IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'" + schemaTable.Name + "'))\n");
            sql.Append("DROP TABLE " + schemaTable);
            session.ExecuteSql(sql.ToString());
        }

        private void CreateTable(ManagementSession session, SchemaTable table)
        {
            var sql = new StringBuilder();
            sql.Append("CREATE TABLE " + table + "\n");
            sql.Append("(\n");

            foreach (SchemaColumn column in table)
            {
                sql.Append(column + " ");
                sql.Append(this.GetSqlType(column));

                if (column.IsIdentity)
                {
                    sql.Append(" IDENTITY");
                }

                sql.Append(",\n");
            }

            sql.Append("PRIMARY KEY (");
            var firstKey = true;
            foreach (SchemaColumn field in table)
            {
                if (field.IsKey)
                {
                    if (firstKey)
                    {
                        firstKey = false;
                    }
                    else
                    {
                        sql.Append(", ");
                    }

                    sql.Append(field.Name);
                }
            }

            sql.Append(")\n");
            sql.Append(")\n");
            session.ExecuteSql(sql.ToString());
        }

        private void DropProcedures(ManagementSession session)
        {
            var allorsProcedureNames = new List<string>();
            lock (this)
            {
                using (var command = session.CreateCommand("SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES"))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var procedureName = reader.GetString(0);
                            if (procedureName.StartsWith(SqlClient.Schema.AllorsPrefix))
                            {
                                allorsProcedureNames.Add(procedureName);
                            }
                        }
                    }
                }
            }

            foreach (var procedureName in allorsProcedureNames)
            {
                this.DropProcedure(session, procedureName);
            }
        }

        private void CreateProcedure(ManagementSession session, SchemaProcedure storedProcedure)
        {
            session.ExecuteSql(storedProcedure.Definition);
        }

        private void CreateIndex(ManagementSession session, SchemaTable table, SchemaColumn column)
        {
            var indexName = SqlClient.Schema.AllorsPrefix + table.Name + "_" + column.Name;

            if (column.IndexType == SchemaIndexType.Single)
            {
                var sql = new StringBuilder();
                sql.Append("CREATE INDEX " + indexName + "\n");
                sql.Append("ON " + table + " (" + column + ")");
                session.ExecuteSql(sql.ToString());
            }
            else
            {
                var sql = new StringBuilder();
                sql.Append("CREATE INDEX " + indexName + "\n");
                sql.Append("ON " + table + " (" + column + ", " + table.FirstKeyColumn + ")");
                session.ExecuteSql(sql.ToString());
            }
        }

        private void TruncateTables(ManagementSession session, SchemaTable table)
        {
            var sql = new StringBuilder();
            sql.Append("TRUNCATE TABLE " + table.StatementName);
            session.ExecuteSql(sql.ToString());
        }

        private Load CreateLoad(ObjectNotLoadedEventHandler objectNotLoaded, RelationNotLoadedEventHandler relationNotLoaded, System.Xml.XmlReader reader)
        {
            return new Load(this, objectNotLoaded, relationNotLoaded, reader);
        }

        private Save CreateSave(XmlWriter writer)
        {
            return new Save(this, writer);
        }

        private void DropUserDefinedType(ManagementSession session, string userDefinedType)
        {
            var sql = new StringBuilder();
            sql.Append(
                "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.DOMAINS WHERE DOMAIN_NAME = N'" + userDefinedType + "')\n");
            sql.Append("DROP TYPE " + userDefinedType);
            session.ExecuteSql(sql.ToString());
        }

        private void DropProcedure(ManagementSession session, string procedureName)
        {
            var sql = new StringBuilder();
            sql.Append(
                "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = N'" + procedureName + "')\n");
            sql.Append("DROP PROCEDURE " + procedureName);
            session.ExecuteSql(sql.ToString());
        }
    }
}