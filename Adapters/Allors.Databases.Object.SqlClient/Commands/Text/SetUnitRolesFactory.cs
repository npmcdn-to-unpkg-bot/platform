// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetUnitRolesFactory.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient.Commands.Text
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    using Allors.Databases.Object.SqlClient;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class SetUnitRolesFactory
    {
        internal readonly Database Database;

        internal SetUnitRolesFactory(Database database)
        {
            this.Database = database;
        }

        internal SetUnitRoles Create(DatabaseSession session)
        {
            return new SetUnitRoles(session);
        }

        internal class SetUnitRoles : DatabaseCommand
        {
            private readonly DatabaseSession session;

            private readonly Dictionary<IObjectType, Dictionary<IList<IRoleType>, SqlCommand>> commandByKeyByIObjectType; 

            internal SetUnitRoles(DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.session = (DatabaseSession)session;
                this.commandByKeyByIObjectType = new Dictionary<IObjectType, Dictionary<IList<IRoleType>, SqlCommand>>();
            }

            internal void Execute(Roles roles, IList<IRoleType> sortedIRoleTypes)
            {
                var mapping = this.Database.Mapping;

                var exclusiveRootClass = roles.Reference.ObjectType.ExclusiveLeafClass;

                Dictionary<IList<IRoleType>, SqlCommand> commandByKey;
                if (!this.commandByKeyByIObjectType.TryGetValue(exclusiveRootClass, out commandByKey))
                {
                    commandByKey = new Dictionary<IList<IRoleType>, SqlCommand>(new SortedIRoleTypesComparer());
                    this.commandByKeyByIObjectType.Add(exclusiveRootClass, commandByKey);
                }

                SqlCommand command;
                if (!commandByKey.TryGetValue(sortedIRoleTypes, out command))
                {
                    command = this.session.CreateSqlCommand();
                    var sqlParameter = command.CreateParameter();
                    sqlParameter.ParameterName = Mapping.ParamNameForObject;
                    sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                    sqlParameter.Value = roles.Reference.ObjectId.Value ?? DBNull.Value;

                    command.Parameters.Add(sqlParameter);

                    var sql = new StringBuilder();
                    sql.Append("UPDATE " + mapping.TableNameForObjectByClass[exclusiveRootClass] + " SET\n");

                    var count = 0;
                    foreach (var roleType in sortedIRoleTypes)
                    {
                        if (count > 0)
                        {
                            sql.Append(" , ");
                        }

                        ++count;

                        var column = mapping.ColumnNameByRelationType[roleType.RelationType];
                        sql.Append(column + "=" + mapping.ParamNameByRoleType[roleType]);

                        var unit = roles.ModifiedRoleByIRoleType[roleType];
                        var sqlParameter1 = command.CreateParameter();
                        sqlParameter1.ParameterName = mapping.ParamNameByRoleType[roleType];
                        sqlParameter1.SqlDbType = mapping.GetSqlDbType(roleType);
                        sqlParameter1.Value = unit ?? DBNull.Value;

                        command.Parameters.Add(sqlParameter1);
                    }

                    sql.Append("\nWHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n");

                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();

                    commandByKey.Add(sortedIRoleTypes, command);
                }
                else
                {
                    command.Parameters[Mapping.ParamNameForObject].Value = roles.Reference.ObjectId.Value ?? DBNull.Value;

                    foreach (var roleType in sortedIRoleTypes)
                    {
                        var column = mapping.ColumnNameByRelationType[roleType.RelationType];

                        var unit = roles.ModifiedRoleByIRoleType[roleType];
                        command.Parameters[mapping.ParamNameByRoleType[roleType]].Value = unit ?? DBNull.Value;
                    }

                    command.ExecuteNonQuery();
                }
            }

            private class SortedIRoleTypesComparer : IEqualityComparer<IList<IRoleType>>
            {
                public bool Equals(IList<IRoleType> x, IList<IRoleType> y)
                {
                    if (x.Count == y.Count)
                    {
                        for (var i = 0; i < x.Count; i++)
                        {
                            if (!x[i].Equals(y[i]))
                            {
                                return false;
                            }
                        }

                        return true;
                    }

                    return false;
                }

                public int GetHashCode(IList<IRoleType> roleTypes)
                {
                    var hashCode = 0;
                    foreach (var roleType in roleTypes)
                    {
                        hashCode = hashCode ^ roleType.GetHashCode();
                    }

                    return hashCode;
                }
            }
        }
    }
}