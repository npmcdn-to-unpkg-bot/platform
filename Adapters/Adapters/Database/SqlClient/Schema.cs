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
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Text;

    using Allors.Meta;

    public class Schema
    {
        public const string TableNameForObjects = "_O";

        public const string ColumnNameForObject = "o";
        public const string ColumnNameForType = "t";
        public const string ColumnNameForCache = "c";
        public const string ColumnNameForAssociation = "a";
        public const string ColumnNameForRole = "r";

        public const string ParameterNameForObject = "@o";
        public const string ParameterNameForType = "@t";
        public const string ParameterNameForCache = "@c";
        public const string ParameterNameForAssociation = "@a";
        public const string ParameterNameForRole = "@r";

        public const string SqlTypeForType = "UNIQUEIDENTIFIER";
        public const string SqlTypeForCache = "INT";

        public const SqlDbType SqlDbTypeForType = SqlDbType.UniqueIdentifier;
        public const SqlDbType SqlDbTypeForCache = SqlDbType.Int;

        private readonly string connectionString;
        private readonly IMetaPopulation metaPopulation;

        private readonly string sqlTypeForId;
        private readonly SqlDbType sqlDbTypeForId;

        private readonly Dictionary<IRelationType, string> tableNameByRelationType;
        private readonly Dictionary<IRoleType, string> sqlTypeByRoleType;
        private readonly Dictionary<IRoleType, SqlDbType> sqlDbTypeByRoleType;
        
        public Schema(IMetaPopulation metaPopulation, string connectionString, ObjectIds objectIds)
        {
            if (!metaPopulation.IsValid)
            {
                throw new Exception("MetaPopulation is invalid.");
            }

            this.connectionString = connectionString;
            this.metaPopulation = metaPopulation;

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

            foreach (var relationType in this.metaPopulation.RelationTypes)
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

                        case UnitTags.AllorsDate:
                            tableName = tableName + "_DATE";
                            sqlDbType = SqlDbType.Date;
                            sqlType = "DATE";
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
                return sqlDbTypeForId;
            }
        }

        public void Init()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                try
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Objects
                            var cmdText = @"
IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'" + TableNameForObjects + @"'))
    BEGIN
    TRUNCATE TABLE " + TableNameForObjects + @"
    END
ELSE
    BEGIN
    CREATE TABLE " + TableNameForObjects + @"
    (
        " + ColumnNameForObject + @" " + this.SqlTypeForId + @" IDENTITY(1,1),
	    " + ColumnNameForType + @" " + SqlTypeForType + @",
	    " + ColumnNameForCache + @" " + SqlTypeForCache + @",
        PRIMARY KEY (O)
    )
    END
";
                            using (var command = new SqlCommand(cmdText, connection, transaction))
                            {
                                command.ExecuteNonQuery();
                            }


                            // Relations
                            foreach (var relationType in this.metaPopulation.RelationTypes)
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
IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'" + tableName + @"'))
    BEGIN
    TRUNCATE TABLE " + tableName + @"
    END
ELSE
    BEGIN
    CREATE TABLE " + tableName + @"
    (
        " + ColumnNameForAssociation + @" " + this.SqlTypeForId + @",
        " + ColumnNameForRole + @" " + sqlTypeForRole + @",
        PRIMARY KEY ( " + primaryKeys + @")
    )
    END
";
                                using (var command = new SqlCommand(cmdText, connection, transaction))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        finally
                        {
                            transaction.Commit();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
        public string GetTableName(IRelationType relationType)
        {
            string tableName;
            this.tableNameByRelationType.TryGetValue(relationType, out tableName);
            return tableName;
        }

        public string GetSqlType(IRoleType roleType)
        {
            string sqlType;
            this.sqlTypeByRoleType.TryGetValue(roleType, out sqlType);
            return sqlType;
        }

        public SqlDbType GetSqlDbType(IRoleType roleType)
        {
            SqlDbType sqlDbType;
            this.sqlDbTypeByRoleType.TryGetValue(roleType, out sqlDbType);
            return sqlDbType;
        }
    }
}