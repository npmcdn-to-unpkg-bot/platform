// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mapping.cs" company="Allors bvba">
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
    using System.Data.SqlClient;

    using Allors.Meta;

    public class Mapping
    {
        public const string ParamPrefix = "@";

        public const string TableNameForObjects = "_o";

        public const string ColumnNameForObject = "o";
        public const string ColumnNameForType = "t";
        public const string ColumnNameForCache = "c";
        public const string ColumnNameForAssociation = "a";
        public const string ColumnNameForRole = "r";

        public const string ParameterNameForObject = ParamPrefix + "o";
        public const string ParameterNameForType = ParamPrefix + "t";
        public const string ParameterNameForCache = ParamPrefix + "c";
        public const string ParameterNameForAssociation = ParamPrefix + "a";
        public const string ParameterNameForRole = ParamPrefix + "r";

        public const string SqlTypeForType = "uniqueidentifier";
        public const string SqlTypeForCache = "int";

        public const SqlDbType SqlDbTypeForObject = SqlDbType.Int;
        public const SqlDbType SqlDbTypeForType = SqlDbType.UniqueIdentifier;
        public const SqlDbType SqlDbTypeForCache = SqlDbType.Int;

        private readonly Database database;

        private readonly string sqlTypeForId;
        private readonly SqlDbType sqlDbTypeForId;

        private readonly Dictionary<IRelationType, string> tableNameByRelationType;
        private readonly Dictionary<IRoleType, string> sqlTypeByRoleType;
        private readonly Dictionary<IRoleType, SqlDbType> sqlDbTypeByRoleType;

        public Mapping(Database database)
        {
            this.database = database;

            if (!this.database.MetaPopulation.IsValid)
            {
                throw new Exception("MetaPopulation is invalid.");
            }

            if (this.database.ObjectIds is ObjectIdsInteger)
            {
                this.sqlTypeForId = "int";
                this.sqlDbTypeForId = SqlDbType.Int;
            }
            else if (this.database.ObjectIds is ObjectIdsLong)
            {
                this.sqlTypeForId = "bigint";
                this.sqlDbTypeForId = SqlDbType.BigInt;
            }
            else
            {
                throw new NotSupportedException("ObjectIds of type " + this.database.ObjectIds.GetType() + " are not supported.");
            }

            this.tableNameByRelationType = new Dictionary<IRelationType, string>();
            this.sqlTypeByRoleType = new Dictionary<IRoleType, string>();
            this.sqlDbTypeByRoleType = new Dictionary<IRoleType, SqlDbType>();

            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var tableName = "_" + relationType.Id.ToString("N");
                SqlDbType sqlDbType;
                string sqlType;

                var roleType = relationType.RoleType;
                var unit = roleType.ObjectType as IUnit;
                if (unit != null)
                {
                    switch (unit.UnitTag)
                    {
                        case UnitTags.AllorsBinary:
                            sqlDbType = SqlDbType.VarBinary;
                            if (roleType.Size != -1 && roleType.Size <= 8000)
                            {
                                tableName = tableName + "_binary_" + roleType.Size;
                                sqlType = "varbinary(" + roleType.Size + ")";
                            }
                            else
                            {
                                tableName = tableName + "_binary";
                                sqlType = "varbinary(MAX)";
                            }
                            
                            break;

                        case UnitTags.AllorsBoolean:
                            tableName = tableName + "_boolean";
                            sqlDbType = SqlDbType.Bit;
                            sqlType = "bit";
                            break;

                        case UnitTags.AllorsDecimal:
                            tableName = tableName + "_decimal_" + roleType.Precision + "_" + roleType.Scale;
                            sqlDbType = SqlDbType.Decimal;
                            sqlType = "decimal(" + roleType.Precision + "," + roleType.Scale + ") ";
                            break;

                        case UnitTags.AllorsFloat:
                            tableName = tableName + "_float";
                            sqlDbType = SqlDbType.Float;
                            sqlType = "float";
                            break;

                        case UnitTags.AllorsInteger:
                            tableName = tableName + "_integer";
                            sqlDbType = SqlDbType.Int;
                            sqlType = "int";
                            break;

                        case UnitTags.AllorsString:
                            sqlDbType = SqlDbType.NVarChar;
                            if (roleType.Size != -1 && roleType.Size <= 4000)
                            {
                                tableName = tableName + "_string_" + roleType.Size;
                                sqlType = "nvarchar(" + roleType.Size + ")";
                            }
                            else
                            {
                                tableName = tableName + "_string";
                                sqlType = "nvarchar(MAX)";
                            }

                            break;

                        case UnitTags.AllorsUnique:
                            tableName = tableName + "_unique";
                            sqlDbType = SqlDbType.UniqueIdentifier;
                            sqlType = "uniqueidentifier";
                            break;

                        default:
                            throw new NotSupportedException("Unit " + unit + "is not supported.");
                    }
                }
                else
                {
                    if (relationType.AssociationType.IsOne)
                    {
                        if (roleType.IsOne)
                        {
                            tableName = tableName + "_11";
                        }
                        else
                        {
                            tableName = tableName + "_1m";
                        }
                    }
                    else
                    {
                        if (roleType.IsOne)
                        {
                            tableName = tableName + "_m1";
                        }
                        else
                        {
                            tableName = tableName + "_mm";
                        }
                    }


                    sqlDbType = this.SqlDbTypeForId;
                    sqlType = this.SqlTypeForId;
                }

                this.tableNameByRelationType[relationType] = tableName.ToLowerInvariant();
                this.sqlDbTypeByRoleType[roleType] = sqlDbType;
                this.sqlTypeByRoleType[roleType] = sqlType;
            }
        }

        public string SqlTypeForId
        {
            get
            {
                return this.sqlTypeForId;
            }
        }

        public SqlDbType SqlDbTypeForId
        {
            get
            {
                return this.sqlDbTypeForId;
            }
        }

        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        public string GetTableName(IAssociationType associationType)
        {
            return this.GetTableName(associationType.RelationType);
        }

        public string GetTableName(IRoleType roleType)
        {
            return this.GetTableName(roleType.RelationType);
        }

        public string GetTableName(IRelationType relationType)
        {
            string tableName;
            this.tableNameByRelationType.TryGetValue(relationType, out tableName);
            return tableName;
        }
        
        public SqlDbType GetSqlDbType(IRoleType roleType)
        {
            SqlDbType sqlDbType;
            this.sqlDbTypeByRoleType.TryGetValue(roleType, out sqlDbType);
            return sqlDbType;
        }

        public string GetSqlType(IRoleType roleType)
        {
            string sqlType;
            this.sqlTypeByRoleType.TryGetValue(roleType, out sqlType);
            return sqlType;
        }

        internal void Init()
        {
            using (var connection = new SqlConnection(this.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    var cmdText = @"
alter database " + connection.Database + @"
set allow_snapshot_isolation on";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            bool schemaExists;
            using (var connection = new SqlConnection(this.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    var cmdText = @"
SELECT  count(schema_name)
FROM    information_schema.schemata
WHERE   schema_name = @schemaName";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@schemaName", SqlDbType.NVarChar).Value = this.Database.SchemaName;
                        var schemaCount = (int)command.ExecuteScalar();
                        schemaExists = schemaCount != 0;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            // CREATE SCHEMA must be in its own batch
            using (var connection = new SqlConnection(this.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (!schemaExists)
                    {
                        var cmdText = @"
CREATE SCHEMA " + this.Database.SchemaName;
                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            using (var connection = new SqlConnection(this.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Objects
                    var cmdText = @"
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = @tableName AND table_schema = @tableSchema)
BEGIN
TRUNCATE TABLE " + this.Database.SchemaName + "." + TableNameForObjects + @"
END
ELSE
BEGIN
CREATE TABLE " + this.Database.SchemaName + "." + TableNameForObjects + @"
(
" + ColumnNameForObject + @" " + this.SqlTypeForId + @" IDENTITY(1,1),
" + ColumnNameForType + @" " + SqlTypeForType + @",
" + ColumnNameForCache + @" " + SqlTypeForCache + @",
PRIMARY KEY (O)
)
END
";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = TableNameForObjects;
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.Database.SchemaName;
                        command.ExecuteNonQuery();
                    }

                    // Relations
                    foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
                    {
                        var roleType = relationType.RoleType;

                        var tableName = this.tableNameByRelationType[relationType];
                        var sqlTypeForRole = this.sqlTypeByRoleType[roleType];
                        var primaryKeys = "a";
                        if (roleType.ObjectType is IComposite && roleType.IsMany)
                        {
                            primaryKeys = ColumnNameForAssociation + @" , " + ColumnNameForRole;
                        }

                        cmdText = @"
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = @tableName AND table_schema = @tableSchema)
BEGIN
TRUNCATE TABLE " + this.Database.SchemaName + "." + tableName + @"
END
ELSE
BEGIN
CREATE TABLE " + this.Database.SchemaName + "." + tableName + @"
(
" + ColumnNameForAssociation + @" " + this.SqlTypeForId + @",
" + ColumnNameForRole + @" " + sqlTypeForRole + @",
PRIMARY KEY ( " + primaryKeys + @")
)
END
";
                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = tableName;
                            command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.Database.SchemaName;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}