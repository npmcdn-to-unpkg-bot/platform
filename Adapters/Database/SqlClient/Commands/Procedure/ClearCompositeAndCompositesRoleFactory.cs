// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearCompositeAndCompositesRoleFactory.cs" company="Allors bvba">
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

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class ClearCompositeAndCompositesRoleFactory : IClearCompositeAndCompositesRoleFactory
    {
        public readonly Database Database;
        private readonly Dictionary<RoleType, string> sqlByRoleType;

        public ClearCompositeAndCompositesRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleType = new Dictionary<RoleType, string>();
        }

        public IClearCompositeAndCompositesRole Create(Sql.DatabaseSession session)
        {
            return new ClearCompositeAndCompoisitesAndCompositesAndCompoisitesRole(this, session);
        }

        public string GetSql(RoleType roleType)
        {
            if (!this.sqlByRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if ((roleType.IsMany && associationType.IsMany) || !roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "C_" + roleType.FullSingularName;
                }
                else
                {
                    if (roleType.IsOne)
                    {
                        sql = Sql.Schema.AllorsPrefix + "C_" + associationType.ObjectType.ExclusiveRootClass.Name + "_" + roleType.RootName;
                    }
                    else
                    {
                        sql = Sql.Schema.AllorsPrefix + "C_" + roleType.ObjectType.ExclusiveRootClass.Name + "_" + associationType.RootName;
                    }
                }

                this.sqlByRoleType[roleType] = sql;
            }

            return this.sqlByRoleType[roleType];
        }

        private class ClearCompositeAndCompoisitesAndCompositesAndCompoisitesRole : DatabaseCommand, IClearCompositeAndCompositesRole
        {
            private readonly ClearCompositeAndCompositesRoleFactory factory;
            private readonly Dictionary<RoleType, SqlCommand> commandByRoleType;

            public ClearCompositeAndCompoisitesAndCompositesAndCompoisitesRole(ClearCompositeAndCompositesRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleType = new Dictionary<RoleType, SqlCommand>();
            }

            public void Execute(IList<ObjectId> associations, RoleType roleType)
            {
                var schema = this.factory.Database.SqlClientSchema;

                SqlCommand command;
                if (!this.commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, schema.ObjectTableParam, this.Database.CreateObjectTable(associations));

                    this.commandByRoleType[roleType] = command;
                }
                else
                {
                    this.SetInTable(command, schema.ObjectTableParam, this.Database.CreateObjectTable(associations));
                }

                command.ExecuteNonQuery();
            }
        }
    }
}