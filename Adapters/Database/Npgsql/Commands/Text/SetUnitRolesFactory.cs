// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetUnitRolesFactory.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.R1.Adapters.Database.Npgsql.Commands.Text
{
    using System.Collections.Generic;
    using System.Text;

    using Allors.R1.Adapters.Database.Sql;
    using Allors.R1.Adapters.Database.Sql.Commands;
    using Allors.R1.Meta;

    using global::Npgsql;

    using Database = Database;
    using DatabaseSession = Allors.R1.Adapters.Database.Npgsql.DatabaseSession;

    public class SetUnitRolesFactory : ISetUnitRolesFactory
    {
        public readonly Database Database;

        public SetUnitRolesFactory(Database database)
        {
            this.Database = database;
        }

        public ISetUnitRoles Create(Sql.DatabaseSession session)
        {
            return new SetUnitRoles(session);
        }

        private class SetUnitRoles : DatabaseCommand, ISetUnitRoles
        {
            private readonly DatabaseSession session;

            private readonly Dictionary<ObjectType, Dictionary<IList<RoleType>, NpgsqlCommand>> commandByKeyByObjectType; 

            public SetUnitRoles(Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.session = (DatabaseSession)session;
                this.commandByKeyByObjectType = new Dictionary<ObjectType, Dictionary<IList<RoleType>, NpgsqlCommand>>();
            }

            public void Execute(Roles roles, IList<RoleType> sortedRoleTypes)
            {
                var schema = this.Database.Schema;

                var exclusiveRootClass = roles.Reference.ObjectType.ExclusiveRootClass;

                Dictionary<IList<RoleType>, NpgsqlCommand> commandByKey;
                if (!this.commandByKeyByObjectType.TryGetValue(exclusiveRootClass, out commandByKey))
                {
                    commandByKey = new Dictionary<IList<RoleType>, NpgsqlCommand>(new SortedRoleTypesComparer());
                    this.commandByKeyByObjectType.Add(exclusiveRootClass, commandByKey);
                }

                NpgsqlCommand command;
                if (!commandByKey.TryGetValue(sortedRoleTypes, out command))
                {
                    command = this.session.CreateNpgsqlCommand();
                    this.AddInObject(command, schema.ObjectId.Param, roles.Reference.ObjectId.Value);

                    var sql = new StringBuilder();
                    sql.Append("UPDATE " + schema.Table(exclusiveRootClass) + " SET\n");

                    var count = 0;
                    foreach (var roleType in sortedRoleTypes)
                    {
                        if (count > 0)
                        {
                            sql.Append(" , ");
                        }

                        ++count;

                        var column = schema.Column(roleType);
                        sql.Append(column + "= " + column.Param.InvocationName);

                        var unit = roles.ModifiedRoleByRoleType[roleType];
                        this.AddInObject(command, column.Param, unit);
                    }

                    sql.Append("\nWHERE " + schema.ObjectId + "= :" + schema.ObjectId.Param + "\n");

                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();

                    commandByKey.Add(sortedRoleTypes, command);
                }
                else
                {
                    this.SetInObject(command, schema.ObjectId.Param, roles.Reference.ObjectId.Value);
                    
                    foreach (var roleType in sortedRoleTypes)
                    {
                        var column = schema.Column(roleType);

                        var unit = roles.ModifiedRoleByRoleType[roleType];
                        this.SetInObject(command, column.Param, unit);
                    }

                    command.ExecuteNonQuery();
                }
            }

            private class SortedRoleTypesComparer : IEqualityComparer<IList<RoleType>>
            {
                public bool Equals(IList<RoleType> x, IList<RoleType> y)
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

                public int GetHashCode(IList<RoleType> roleTypes)
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