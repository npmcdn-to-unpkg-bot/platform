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

    using Allors.Meta;

    public class Mapping
    {
        public const string ParamPrefix = "@";

        public const string Alias1 = "_x";
        public const string Alias2 = "_y";

        public const string TableNameForObjects = "_o";
        public const string ColumnNameForObject = "o";
        public const string ColumnNameForType = "t";
        public const string ColumnNameForCache = "c";
        public const string ColumnNameForAssociation = "a";
        public const string ColumnNameForRole = "r";

        public const string TableTypeNameForObjects = "_t_o";
        public const string TableTypeColumnNameForObject = "_o";
        public const string TableTypeNameForCompositeRelations = "_t_c";
        public const string TableTypeColumnNameForAssociation = "_a";
        public const string TableTypeColumnNameForRole = "_r";
        
        public const string ParameterNameForObject = ParamPrefix + "o";
        public const string ParameterNameForType = ParamPrefix + "t";
        public const string ParameterNameForCache = ParamPrefix + "c";
        public const string ParameterNameForAssociation = ParamPrefix + "a";
        public const string ParameterNameForRole = ParamPrefix + "r";
        public const string ParameterNameForRelationTable = ParamPrefix + "rt";

        public const string SqlTypeForType = "uniqueidentifier";
        public const string SqlTypeForCache = "int";

        public const SqlDbType SqlDbTypeForType = SqlDbType.UniqueIdentifier;
        public const SqlDbType SqlDbTypeForCache = SqlDbType.Int;

        private readonly Database database;

        private readonly string sqlTypeForId;
        private readonly SqlDbType sqlDbTypeForId;

        private readonly Dictionary<IRelationType, string> tableNameByRelationType;
        private readonly Dictionary<IRoleType, string> sqlTypeByRoleType;
        private readonly Dictionary<IRoleType, SqlDbType> sqlDbTypeByRoleType;

        private readonly Dictionary<IRelationType, string> tableTypeNameByRelationType;
        private readonly Dictionary<IRelationType, string> tableTypeSqlTypeByRelationType;

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

            this.tableTypeNameByRelationType = new Dictionary<IRelationType, string>();
            this.tableTypeSqlTypeByRelationType = new Dictionary<IRelationType, string>();

            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var tableName = "_" + relationType.Id.ToString("N");
                SqlDbType sqlDbType;
                string sqlType;

                var tableTypeName = TableTypeNameForCompositeRelations;
                var tableTypeSqlType = this.sqlTypeForId;

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
                            
                            tableTypeName = "_t_bi";
                            tableTypeSqlType = "varbinary(MAX)";
                            break;

                        case UnitTags.AllorsBoolean:
                            tableName = tableName + "_boolean";
                            sqlDbType = SqlDbType.Bit;
                            sqlType = "bit";

                            tableTypeName = "_t_bo";
                            tableTypeSqlType = sqlType;
                            break;

                        case UnitTags.AllorsDecimal:
                            tableName = tableName + "_decimal_" + roleType.Precision + "_" + roleType.Scale;
                            sqlDbType = SqlDbType.Decimal;
                            sqlType = "decimal(" + roleType.Precision + "," + roleType.Scale + ")";

                            tableTypeName = "_t_d_" + roleType.Precision + "_" + roleType.Scale;
                            tableTypeSqlType = sqlType;
                            break;

                        case UnitTags.AllorsFloat:
                            tableName = tableName + "_float";
                            sqlDbType = SqlDbType.Float;
                            sqlType = "float";

                            tableTypeName = "_t_f";
                            tableTypeSqlType = sqlType;
                            break;

                        case UnitTags.AllorsInteger:
                            tableName = tableName + "_integer";
                            sqlDbType = SqlDbType.Int;
                            sqlType = "int";

                            tableTypeName = "_t_i";
                            tableTypeSqlType = sqlType;
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

                            tableTypeName = "_t_s";
                            tableTypeSqlType = "nvarchar(MAX)";
                            break;

                        case UnitTags.AllorsUnique:
                            tableName = tableName + "_unique";
                            sqlDbType = SqlDbType.UniqueIdentifier;
                            sqlType = "uniqueidentifier";

                            tableTypeName = "_t_u";
                            tableTypeSqlType = sqlType;
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
                            tableName = tableName + "_n1";
                        }
                        else
                        {
                            tableName = tableName + "_nm";
                        }
                    }


                    sqlDbType = this.SqlDbTypeForId;
                    sqlType = this.SqlTypeForId;
                }

                this.tableNameByRelationType[relationType] = tableName.ToLowerInvariant();
                this.sqlDbTypeByRoleType[roleType] = sqlDbType;
                this.sqlTypeByRoleType[roleType] = sqlType;

                this.tableTypeNameByRelationType[relationType] = tableTypeName.ToLowerInvariant();
                this.tableTypeSqlTypeByRelationType[relationType] = tableTypeSqlType.ToLowerInvariant();
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

        public string GetTableTypeName(IRelationType relationType)
        {
            string tableTypeName;
            this.tableTypeNameByRelationType.TryGetValue(relationType, out tableTypeName);
            return tableTypeName;
        }

        public string GetTableTypeSqlType(IRelationType relationType)
        {
            string tableTypeSqlType;
            this.tableTypeSqlTypeByRelationType.TryGetValue(relationType, out tableTypeSqlType);
            return tableTypeSqlType;
        }
    }
}