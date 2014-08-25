// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCompositeRoleFactory.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class AddCompositeRoleFactory : Sql.Commands.IAddCompositeRoleFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<MetaRole, string> sqlByRoleType;

        internal AddCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleType = new Dictionary<MetaRole, string>();
        }

        public Sql.Commands.IAddCompositeRole Create(Sql.DatabaseSession session)
        {
            return new AddCompositeRole(this, session);
        }

        internal string GetSql(MetaRole roleType)
        {
            if (!this.sqlByRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "A_" + roleType.FullSingularName;
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "A_" + roleType.ObjectType.ExclusiveRootClass.Name + "_" + associationType.Name;
                }
 
                this.sqlByRoleType[roleType] = sql;
            }

            return this.sqlByRoleType[roleType];
        }

        private class AddCompositeRole : DatabaseCommand, Sql.Commands.IAddCompositeRole
        {
            private readonly AddCompositeRoleFactory factory;
            private readonly Dictionary<MetaRole, SqlCommand> commandByRoleType;

            public AddCompositeRole(AddCompositeRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleType = new Dictionary<MetaRole, SqlCommand>();
            }

            public void Execute(IList<CompositeRelation> relations, MetaRole roleType)
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