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

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class ClearCompositeAndCompositesRoleFactory : IClearCompositeAndCompositesRoleFactory
    {
        public readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        public ClearCompositeAndCompositesRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        public IClearCompositeAndCompositesRole Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new ClearCompositeAndCompoisitesAndCompositesAndCompoisitesRole(this, session);
        }

        public string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if ((roleType.IsMany && associationType.IsMany) || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Adapters.Database.Sql.Schema.AllorsPrefix + "C_" + roleType.SingularFullName;
                }
                else
                {
                    if (roleType.IsOne)
                    {
                        sql = Adapters.Database.Sql.Schema.AllorsPrefix + "C_" + associationType.ObjectType.ExclusiveLeafClass.Name + "_" + roleType.SingularFullName;
                    }
                    else
                    {
                        sql = Adapters.Database.Sql.Schema.AllorsPrefix + "C_" + ((IComposite)roleType.ObjectType).ExclusiveLeafClass.Name + "_" + associationType.SingularFullName;
                    }
                }

                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        private class ClearCompositeAndCompoisitesAndCompositesAndCompoisitesRole : DatabaseCommand, IClearCompositeAndCompositesRole
        {
            private readonly ClearCompositeAndCompositesRoleFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            public ClearCompositeAndCompoisitesAndCompositesAndCompoisitesRole(ClearCompositeAndCompositesRoleFactory factory, Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
            }

            public void Execute(IList<ObjectId> associations, IRoleType roleType)
            {
                var schema = this.factory.Database.SqlClientSchema;

                SqlCommand command;
                if (!this.commandByIRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, schema.ObjectTableParam, this.Database.CreateObjectTable(associations));

                    this.commandByIRoleType[roleType] = command;
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