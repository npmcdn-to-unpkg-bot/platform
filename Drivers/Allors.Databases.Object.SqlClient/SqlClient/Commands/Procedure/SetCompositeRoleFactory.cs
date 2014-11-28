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

namespace Allors.Databases.Object.SqlClient.Commands.Text
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class SetCompositeRoleFactory
    {
        public readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        public SetCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        public SetCompositeRole Create(DatabaseSession session)
        {
            return new SetCompositeRole(this, session);
        }

        public string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (!roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = SqlClient.Schema.AllorsPrefix + "S_" + roleType.SingularFullName;
                }
                else
                {
                    sql = SqlClient.Schema.AllorsPrefix + "S_" + associationType.ObjectType.ExclusiveLeafClass.Name + "_" + roleType.SingularFullName;
                }

                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        public class SetCompositeRole : DatabaseCommand
        {
            private readonly SetCompositeRoleFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            public SetCompositeRole(SetCompositeRoleFactory factory, DatabaseSession session)
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