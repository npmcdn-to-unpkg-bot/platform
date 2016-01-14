// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagementSession.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Data;

    using Allors;
    using Allors.Meta;

    internal class ManagementSession : IDisposable
    {
        internal ManagementSession(Database database, IConnectionFactory connectionFactory)
        {
            this.Database = database;
            this.Connection = connectionFactory.Create(database);
        }
        
        ~ManagementSession()
        {
            this.Dispose();
        }

        public Database Database { get; }

        public Connection Connection { get; }

        public void Dispose()
        {
            this.Rollback();
        }

        public void LoadObjects(IObjectType objectType, long[] objectIds)
        {
            var exclusiveRootClass = ((IComposite)objectType).ExclusiveClass;
            var schema = this.Database.Mapping;

            var sql = this.Database.Mapping.ProcedureNameForLoadObjectByClass[exclusiveRootClass];
            var command = this.Connection.CreateCommand();
            command.CommandText = sql;
            using (command)
            {
                command.CommandType = CommandType.StoredProcedure;

                var classSqlParameter = command.CreateParameter();
                classSqlParameter.ParameterName = Mapping.ParamNameForClass;
                classSqlParameter.SqlDbType = Mapping.SqlDbTypeForClass;
                classSqlParameter.Value = objectType.Id;

                command.Parameters.Add(classSqlParameter);

                var objectSqlParameter = command.CreateParameter();
                objectSqlParameter.SqlDbType = SqlDbType.Structured;
                objectSqlParameter.TypeName = schema.TableTypeNameForObject;
                objectSqlParameter.ParameterName = Mapping.ParamNameForTableType;
                objectSqlParameter.Value = this.Database.CreateObjectTable(objectIds);

                command.Parameters.Add(objectSqlParameter);
                command.ExecuteNonQuery();
            }
        }

        public void LoadUnitRelations(List<UnitRelation> relations, IObjectType exclusiveRootClass, IRoleType roleType)
        {
            string tableTypeName;

            var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
            switch (unitTypeTag)
            {
                case UnitTags.String:
                    tableTypeName = this.Database.Mapping.TableTypeNameForStringRelation;
                    break;

                case UnitTags.Integer:
                    tableTypeName = this.Database.Mapping.TableTypeNameForIntegerRelation;
                    break;

                case UnitTags.Float:
                    tableTypeName = this.Database.Mapping.TableTypeNameForFloatRelation;
                    break;

                case UnitTags.Boolean:
                    tableTypeName = this.Database.Mapping.TableTypeNameForBooleanRelation;
                    break;

                case UnitTags.DateTime:
                    tableTypeName = this.Database.Mapping.TableTypeNameForDateTimeRelation;
                    break;

                case UnitTags.Unique:
                    tableTypeName = this.Database.Mapping.TableTypeNameForUniqueRelation;
                    break;

                case UnitTags.Binary:
                    tableTypeName = this.Database.Mapping.TableTypeNameForBinaryRelation;
                    break;

                case UnitTags.Decimal:
                    tableTypeName = this.Database.Mapping.TableTypeNameForDecimalRelationByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                    break;

                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }

            var sql = this.Database.Mapping.ProcedureNameForSetUnitRoleByRelationTypeByClass[(IClass)exclusiveRootClass][roleType.RelationType];

            var command = this.Connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = tableTypeName;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = this.Database.CreateRelationTable(roleType, relations);

            command.Parameters.Add(sqlParameter);

            command.ExecuteNonQuery();
        }

        public void LoadCompositeRelations(IRoleType roleType, List<CompositeRelation> relations)
        {
            var associationType = roleType.AssociationType;

            string sql;
            if (roleType.IsMany)
            {
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                }
            }
            else
            {
                if (!roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }
            }

            var command = this.Connection.CreateCommand();
            command.CommandText = sql;
            using (command)
            {
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForCompositeRelation;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateRelationTable(relations);

                command.Parameters.Add(sqlParameter);

                command.ExecuteNonQuery();
            }
        }
        
        internal void Commit()
        {
            this.Connection.Commit();
        }

        internal void Rollback()
        {
            this.Connection.Rollback();
        }
    }
}