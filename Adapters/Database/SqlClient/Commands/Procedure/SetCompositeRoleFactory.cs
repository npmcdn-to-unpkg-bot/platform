// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetCompositeRoleFactory.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.SqlClient.Commands.Text
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class SetCompositeRoleFactory : ISetCompositeRoleFactory
    {
        public readonly Database Database;
        private readonly Dictionary<RoleType, string> sqlByRoleType;

        public SetCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleType = new Dictionary<RoleType, string>();
        }

        public ISetCompositeRole Create(Sql.DatabaseSession session)
        {
            return new SetCompositeRole(this, session);
        }

        public string GetSql(RoleType roleType)
        {
            if (!this.sqlByRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (!roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "S_" + roleType.SingularFullName;
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "S_" + associationType.ObjectType.ExclusiveLeafClass.Name + "_" + roleType.SingularPropertyName;
                }

                this.sqlByRoleType[roleType] = sql;
            }

            return this.sqlByRoleType[roleType];
        }

        private class SetCompositeRole : DatabaseCommand, ISetCompositeRole
        {
            private readonly SetCompositeRoleFactory factory;
            private readonly Dictionary<RoleType, SqlCommand> commandByRoleType;

            public SetCompositeRole(SetCompositeRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleType = new Dictionary<RoleType, SqlCommand>();
            }

            public void Execute(IList<CompositeRelation> relations, RoleType roleType)
            {
                var schema = this.factory.Database.SqlClientSchema;

                SqlCommand command;
                if (!this.commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, schema.CompositeRelationTableParam, this.Database.CreateRelationTable(relations));
                    this.commandByRoleType[roleType] = command;
                }
                else
                {
                    this.SetInTable(command, schema.CompositeRelationTableParam, this.Database.CreateRelationTable(relations));
                }

                command.ExecuteNonQuery();
            }
        }
    }
}