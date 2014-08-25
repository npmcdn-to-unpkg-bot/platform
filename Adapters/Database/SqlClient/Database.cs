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

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Text;

    using Allors.Meta;

    using Microsoft.SqlServer.Server;

    using Sql;

    public abstract class Database : Sql.Database
    {
        private readonly CommandFactories commandFactories;

        protected Database(Configuration configuration)
            : base(configuration)
        {
            this.commandFactories = new CommandFactories(this);
        }

        public CommandFactories SqlClientCommandFactories
        {
            get
            {
                return this.commandFactories;
            }
        }

        public override Sql.Schema Schema
        {
            get
            {
                return this.SqlClientSchema;
            }
        }

        public abstract Schema SqlClientSchema { get; }

        public override Sql.CommandFactories CommandFactories
        {
            get { return this.commandFactories; }
        }


        public override DbConnection CreateDbConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        public override void DropIndex(Sql.ManagementSession session, SchemaTable table, SchemaColumn column)
        {
            var indexName = Sql.Schema.AllorsPrefix + table.Name + "_" + column.Name;

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

        internal abstract IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<ObjectId> objectids);

        internal abstract IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<Reference> strategies);

        internal abstract IEnumerable<SqlDataRecord> CreateObjectTable(Dictionary<Reference, Roles> rolesByReference);

        internal abstract IEnumerable<SqlDataRecord> CreateRelationTable(IEnumerable<CompositeRelation> compositeRelations);

        internal abstract IEnumerable<SqlDataRecord> CreateRelationTable(MetaRole roleType, IEnumerable<UnitRelation> unitRelations);

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
                case DbType.Int64:
                    return new SqlMetaData(name, SqlDbType.BigInt);
                case DbType.Decimal:
                    return new SqlMetaData(name, SqlDbType.Decimal, (byte)column.Precision.Value, (byte)column.Scale.Value);
                case DbType.Double:
                    return new SqlMetaData(name, SqlDbType.Float);
                case DbType.Boolean:
                    return new SqlMetaData(name, SqlDbType.Bit);
                case DbType.Date:
                    return new SqlMetaData(name, SqlDbType.Date);
                case DbType.DateTime:
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
                case DbType.Int64:
                    return "BIGINT ";
                case DbType.Decimal:
                    return "DECIMAL(" + column.Precision + "," + column.Scale + ") ";
                case DbType.Double:
                    return "FLOAT ";
                case DbType.Boolean:
                    return "BIT ";
                case DbType.Date:
                    return "DATE ";
                case DbType.DateTime:
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
                case DbType.Int64:
                    return "BIGINT ";
                default:
                    return "!UNKNOWN DBTYPE!";
            }
        }

        internal ManagementSession CreateSqlClientManagementSession()
        {
            return new ManagementSession(this);
        }

        protected internal override Sql.ManagementSession CreateManagementSession()
        {
            return this.CreateSqlClientManagementSession();
        }

        protected override void CreateUserDefinedTypes(Sql.ManagementSession session)
        {
            var sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.ObjectTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.ObjectTableObject + " " + this.GetSqlType(this.Schema.ObjectId) + ")\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.CompositeRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " " + this.GetSqlType(this.Schema.RoleId) + ")\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.StringRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " NVARCHAR(MAX))\n"); 
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.IntegerRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " INT)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.LongRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " BIGINT)\n");
            session.ExecuteSql(sql.ToString());
           
            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.DoubleRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " FLOAT)\n");
            session.ExecuteSql(sql.ToString());
            
            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.BooleanRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " BIT)\n");
            session.ExecuteSql(sql.ToString());
            
            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.DateRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " DATE)\n");
            session.ExecuteSql(sql.ToString());
            
            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.DateTimeRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " DATETIME2)\n");
            session.ExecuteSql(sql.ToString());
            
            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.UniqueRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
            sql.Append(this.SqlClientSchema.RelationTableRole + " UNIQUEIDENTIFIER)\n");
            session.ExecuteSql(sql.ToString());

            sql = new StringBuilder();
            sql.Append("CREATE TYPE " + this.SqlClientSchema.BinaryRelationTable + " AS TABLE\n");
            sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
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
                     sql.Append("(" + this.SqlClientSchema.RelationTableAssociation + " " + this.GetSqlType(this.Schema.AssociationId) + ",\n");
                     sql.Append(this.SqlClientSchema.RelationTableRole + " DECIMAL(" + precision + "," + scale + ") )\n");
                     session.ExecuteSql(sql.ToString());
                 }
             }
        }

        protected override IDatabaseSession CreateSqlSession()
        {
            return new DatabaseSession(this);
        }

        protected override void DropUserDefinedTypes(Sql.ManagementSession session)
        {
            this.DropUserDefinedType(session, this.SqlClientSchema.ObjectTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.CompositeRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.StringRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.IntegerRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.LongRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.DoubleRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.BooleanRelationTable);
            this.DropUserDefinedType(session, this.SqlClientSchema.DateRelationTable);
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

        protected override void DropTable(Sql.ManagementSession session, SchemaTable schemaTable)
        {
            var sql = new StringBuilder();
            sql.Append("IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'" + schemaTable.Name + "'))\n");
            sql.Append("DROP TABLE " + schemaTable);
            session.ExecuteSql(sql.ToString());
        }

        protected override void CreateTable(Sql.ManagementSession session, SchemaTable table)
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

        protected override void DropProcedures(Sql.ManagementSession session)
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
                            if (procedureName.StartsWith(Sql.Schema.AllorsPrefix))
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

        protected override void CreateProcedure(Sql.ManagementSession session, SchemaProcedure storedProcedure)
        {
            session.ExecuteSql(storedProcedure.Definition);
        }

        protected override void CreateIndex(Sql.ManagementSession session, SchemaTable table, SchemaColumn column)
        {
            var indexName = Sql.Schema.AllorsPrefix + table.Name + "_" + column.Name;

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

        protected override void TruncateTables(Sql.ManagementSession session, SchemaTable table)
        {
            var sql = new StringBuilder();
            sql.Append("TRUNCATE TABLE " + table.StatementName);
            session.ExecuteSql(sql.ToString());
        }

        protected override Sql.Load CreateLoad(ObjectNotLoadedEventHandler objectNotLoaded, RelationNotLoadedEventHandler relationNotLoaded, System.Xml.XmlReader reader)
        {
            return new Load(this, objectNotLoaded, relationNotLoaded, reader);
        }

        protected override Sql.Save CreateSave(System.Xml.XmlWriter writer)
        {
            return new Save(this, writer);
        }

        private void DropUserDefinedType(Sql.ManagementSession session, string userDefinedType)
        {
            var sql = new StringBuilder();
            sql.Append("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.DOMAINS WHERE DOMAIN_NAME = N'" + userDefinedType + "')\n");
            sql.Append("DROP TYPE " + userDefinedType);
            session.ExecuteSql(sql.ToString());
        }

        private void DropProcedure(Sql.ManagementSession session, string procedureName)
        {
            var sql = new StringBuilder();
            sql.Append("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = N'" + procedureName + "')\n");
            sql.Append("DROP PROCEDURE " + procedureName);
            session.ExecuteSql(sql.ToString());
        }
    }
}