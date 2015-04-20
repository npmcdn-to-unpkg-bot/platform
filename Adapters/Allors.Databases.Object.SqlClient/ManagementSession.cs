// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagementSession.cs" company="Allors bvba">
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

    using Allors.Meta;

    internal class ManagementSession : IDisposable
    {
        private readonly Database database;
        
        private SqlTransaction transaction;
        private SqlConnection connection;

        internal ManagementSession(Database database)
        {
            this.database = database;
        }
        
        ~ManagementSession()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.Rollback();
        }

        public void LoadObjects(IObjectType objectType, ObjectId[] objectIds)
        {
            var exclusiveRootClass = ((IComposite)objectType).ExclusiveClass;
            var schema = this.database.Mapping;

            var sql = this.database.Mapping.ProcedureNameForLoadObjectByClass[exclusiveRootClass];
            using (var command = this.CreateSqlCommand(sql))
            {
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForType;
                sqlParameter.SqlDbType = Mapping.SqlDbTypeForType;
                sqlParameter.Value = (object)objectType.Id ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);
                var sqlParameter1 = command.CreateParameter();
                sqlParameter1.SqlDbType = SqlDbType.Structured;
                sqlParameter1.TypeName = schema.TableTypeNameForObject;
                sqlParameter1.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter1.Value = database.CreateObjectTable(objectIds);

                command.Parameters.Add(sqlParameter1);
                command.ExecuteNonQuery();
            }
        }

        public void LoadUnitRelations(List<UnitRelation> relations, IObjectType exclusiveRootClass, IRoleType roleType)
        {
            string tableTypeName;

            var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    tableTypeName = this.database.Mapping.TableTypeNameForStringRelation;
                    break;

                case UnitTags.AllorsInteger:
                    tableTypeName = this.database.Mapping.TableTypeNameForIntegerRelation;
                    break;

                case UnitTags.AllorsFloat:
                    tableTypeName = this.database.Mapping.TableTypeNameForFloatRelation;
                    break;

                case UnitTags.AllorsBoolean:
                    tableTypeName = this.database.Mapping.TableTypeNameForBooleanRelation;
                    break;

                case UnitTags.AllorsDateTime:
                    tableTypeName = this.database.Mapping.TableTypeNameForDateTimeRelation;
                    break;

                case UnitTags.AllorsUnique:
                    tableTypeName = this.database.Mapping.TableTypeNameForUniqueRelation;
                    break;

                case UnitTags.AllorsBinary:
                    tableTypeName = this.database.Mapping.TableTypeNameForBinaryRelation;
                    break;

                case UnitTags.AllorsDecimal:
                    tableTypeName = this.database.Mapping.TableTypeNameForDecimalRelationByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                    break;

                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }

            var sql = this.database.Mapping.ProcedureNameForSetUnitRoleByRelationTypeByClass[(IClass)exclusiveRootClass][roleType.RelationType];

            var command = this.CreateSqlCommand(sql);
            command.CommandType = CommandType.StoredProcedure;
            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = tableTypeName;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = this.database.CreateRelationTable(roleType, relations);

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
                    sql = this.database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                }
            }
            else
            {
                if (!roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }
            }

            using (var command = this.CreateSqlCommand(sql))
            {
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = database.Mapping.TableTypeNameForCompositeRelation;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = database.CreateRelationTable(relations);

                command.Parameters.Add(sqlParameter);

                command.ExecuteNonQuery();
            }
        }

        internal Command CreateCommand(string commandText)
        {
            var command = this.CreateSqlCommand(commandText);
            return new Command(command);
        }

        internal void Commit()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Commit();
                }
            }
            finally
            {
                this.LazyDisconnect();
            }
        }

        internal void Rollback()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Rollback();
                }
            }
            finally
            {
                this.LazyDisconnect();
            }
        }

        private SqlCommand CreateSqlCommand(string sql)
        {
            this.LazyConnect();
            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            command.CommandTimeout = this.database.CommandTimeout;
            command.CommandText = sql;
            return command;
        }

        private void LazyConnect()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.database.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction();
            }
        }

        private void LazyDisconnect()
        {
            try
            {
                if (this.connection != null)
                {
                    this.connection.Close();
                }
            }
            finally
            {
                this.connection = null;
                this.transaction = null;
            }
        }
    }
}