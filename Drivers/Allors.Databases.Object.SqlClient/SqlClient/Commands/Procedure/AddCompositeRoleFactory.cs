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

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters;
    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    public class AddCompositeRoleFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        internal AddCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        public AddCompositeRole Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new AddCompositeRole(this, session);
        }

        internal string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Schema.AllorsPrefix + "A_" + roleType.SingularFullName;
                }
                else
                {
                    sql = Schema.AllorsPrefix + "A_" + ((IComposite)roleType.ObjectType).ExclusiveLeafClass.Name + "_" + associationType.SingularFullName;
                }
 
                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        public class AddCompositeRole : DatabaseCommand
        {
            private readonly AddCompositeRoleFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            public AddCompositeRole(AddCompositeRoleFactory factory, Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
            }

            public void Execute(IList<CompositeRelation> relations, IRoleType roleType)
            {
                var schema = this.factory.Database.SqlClientSchema;

                SqlCommand command;
                if (!this.commandByIRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, schema.CompositeRelationTableParam, this.Database.CreateRelationTable(relations));

                    this.commandByIRoleType[roleType] = command;
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