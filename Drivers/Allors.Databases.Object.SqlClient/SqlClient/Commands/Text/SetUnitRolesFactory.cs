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
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    public class SetUnitRolesFactory : ISetUnitRolesFactory
    {
        public readonly Database Database;

        public SetUnitRolesFactory(Database database)
        {
            this.Database = database;
        }

        public ISetUnitRoles Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new SetUnitRoles(session);
        }

        private class SetUnitRoles : DatabaseCommand, ISetUnitRoles
        {
            private readonly DatabaseSession session;

            private readonly Dictionary<IObjectType, Dictionary<IList<IRoleType>, SqlCommand>> commandByKeyByIObjectType; 

            public SetUnitRoles(Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.session = (DatabaseSession)session;
                this.commandByKeyByIObjectType = new Dictionary<IObjectType, Dictionary<IList<IRoleType>, SqlCommand>>();
            }

            public void Execute(Roles roles, IList<IRoleType> sortedIRoleTypes)
            {
                var schema = this.Database.Schema;

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
                    this.AddInObject(command, schema.ObjectId.Param, roles.Reference.ObjectId.Value);

                    var sql = new StringBuilder();
                    sql.Append("UPDATE " + schema.Table(exclusiveRootClass) + " SET\n");

                    var count = 0;
                    foreach (var roleType in sortedIRoleTypes)
                    {
                        if (count > 0)
                        {
                            sql.Append(" , ");
                        }

                        ++count;

                        var column = schema.Column(roleType);
                        sql.Append(column + "=" + column.Param);

                        var unit = roles.ModifiedRoleByIRoleType[roleType];
                        this.AddInObject(command, column.Param, unit);
                    }

                    sql.Append("\nWHERE " + schema.ObjectId + "=" + schema.ObjectId.Param + "\n");

                    command.CommandText = sql.ToString();
                    command.ExecuteNonQuery();

                    commandByKey.Add(sortedIRoleTypes, command);
                }
                else
                {
                    this.SetInObject(command, schema.ObjectId.Param, roles.Reference.ObjectId.Value);
                    
                    foreach (var roleType in sortedIRoleTypes)
                    {
                        var column = schema.Column(roleType);

                        var unit = roles.ModifiedRoleByIRoleType[roleType];
                        this.SetInObject(command, column.Param, unit);
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