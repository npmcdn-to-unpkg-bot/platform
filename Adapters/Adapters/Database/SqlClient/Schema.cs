// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Schema.cs" company="Allors bvba">
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

    public class Schema
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

        private readonly object lockObject = new object();

        private readonly string connectionString;
        private readonly string schemaName;
        private readonly IMetaPopulation metaPopulation;

        private readonly string sqlTypeForId;
        private readonly SqlDbType sqlDbTypeForId;

        private readonly Dictionary<IRelationType, string> tableNameByRelationType;
        private readonly Dictionary<IRoleType, string> sqlTypeByRoleType;
        private readonly Dictionary<IRoleType, SqlDbType> sqlDbTypeByRoleType;

        private bool? isValid;

        public Schema(Configuration configuration)
        {
            this.metaPopulation = configuration.ObjectFactory.MetaPopulation;
            this.connectionString = configuration.ConnectionString;
            this.schemaName = configuration.SchemaName;
            var objectIds = configuration.ObjectIds;

            if (!this.metaPopulation.IsValid)
            {
                throw new Exception("MetaPopulation is invalid.");
            }

            if (objectIds is ObjectIdsInteger)
            {
                this.sqlTypeForId = "INT";
                this.sqlDbTypeForId = SqlDbType.Int;
            }
            else if (objectIds is ObjectIdsLong)
            {
                this.sqlTypeForId = "BIGINT";
                this.sqlDbTypeForId = SqlDbType.BigInt;
            }
            else
            {
                throw new NotSupportedException("ObjectIds of type " + objectIds.GetType() + " are not supported.");
            }

            this.tableNameByRelationType = new Dictionary<IRelationType, string>();
            this.sqlTypeByRoleType = new Dictionary<IRoleType, string>();
            this.sqlDbTypeByRoleType = new Dictionary<IRoleType, SqlDbType>();

            foreach (var relationType in this.MetaPopulation.RelationTypes)
            {
                var tableName = "_" + relationType.Id.ToString("N").ToUpperInvariant();
                SqlDbType sqlDbType;
                string sqlType;

                var roleType = relationType.RoleType;
                var unit = roleType.ObjectType as IUnit;
                if (unit != null)
                {
                    switch (unit.UnitTag)
                    {
                        case UnitTags.AllorsBinary:
                            tableName = tableName + "_BINARY";
                            sqlDbType = SqlDbType.VarBinary;
                            if (roleType.Size != -1 && roleType.Size <= 8000)
                            {
                                sqlType = "VARBINARY(" + roleType.Size + ")";
                            }
                            else
                            {
                                sqlType = "VARBINARY(MAX)";
                            }
                            
                            break;

                        case UnitTags.AllorsBoolean:
                            tableName = tableName + "_BOOLEAN";
                            sqlDbType = SqlDbType.Bit;
                            sqlType = "BIT";
                            break;

                        case UnitTags.AllorsDecimal:
                            tableName = tableName + "_DECIMAL";
                            sqlDbType = SqlDbType.Decimal;
                            sqlType = "DECIMAL(" + roleType.Precision + "," + roleType.Scale + ") ";
                            break;

                        case UnitTags.AllorsFloat:
                            tableName = tableName + "_FLOAT";
                            sqlDbType = SqlDbType.Float;
                            sqlType = "FLOAT";
                            break;

                        case UnitTags.AllorsInteger:
                            tableName = tableName + "_INTEGER";
                            sqlDbType = SqlDbType.Int;
                            sqlType = "INT";
                            break;

                        case UnitTags.AllorsString:
                            tableName = tableName + "_STRING";
                            sqlDbType = SqlDbType.NVarChar;
                            if (roleType.Size != -1 && roleType.Size <= 4000)
                            {
                                sqlType = "NVARCHAR(" + roleType.Size + ")";
                            }
                            else
                            {
                                sqlType = "NVARCHAR(MAX)";
                            }

                            break;

                        case UnitTags.AllorsUnique:
                            tableName = tableName + "_UNIQUE";
                            sqlDbType = SqlDbType.UniqueIdentifier;
                            sqlType = "UNIQUEIDENTIFIER";
                            break;

                        default:
                            throw new NotSupportedException("Unit " + unit + "is not supported.");
                    }
                }
                else
                {
                    if (roleType.IsMany)
                    {
                        tableName = tableName + "_MANY";
                    }
                    else
                    {
                        tableName = tableName + "_ONE";
                    }

                    sqlDbType = this.SqlDbTypeForId;
                    sqlType = this.SqlTypeForId;
                }

                this.tableNameByRelationType[relationType] = tableName;
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
                            return validate.Success;
                        }
                    }
                }

                return this.isValid.Value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        public IMetaPopulation MetaPopulation
        {
            get
            {
                return this.metaPopulation;
            }
        }

        public string SchemaName
        {
            get
            {
                return this.schemaName;
            }
        }

        public ValidateResult Validate()
        {
            var validateResult = new ValidateResult(this);
            this.isValid = validateResult.Success;
            return validateResult;
        }

        public void Init()
        {
            bool schemaExists;
            using (var connection = new SqlConnection(this.ConnectionString))
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
                        command.Parameters.Add("@schemaName", SqlDbType.NVarChar).Value = this.schemaName;
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
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (!schemaExists)
                    {
                        var cmdText = @"
CREATE SCHEMA " + this.schemaName;
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

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Objects
                    var cmdText = @"
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = @tableName AND table_schema = @tableSchema)
BEGIN
TRUNCATE TABLE " + this.SchemaName + "." + TableNameForObjects + @"
END
ELSE
BEGIN
CREATE TABLE " + this.SchemaName + "." + TableNameForObjects + @"
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
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.schemaName;
                        command.ExecuteNonQuery();
                    }


                    // Relations
                    foreach (var relationType in this.MetaPopulation.RelationTypes)
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
TRUNCATE TABLE " + this.SchemaName + "." + tableName + @"
END
ELSE
BEGIN
CREATE TABLE " + this.SchemaName + "." + tableName + @"
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
                            command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.schemaName;
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
    }
}