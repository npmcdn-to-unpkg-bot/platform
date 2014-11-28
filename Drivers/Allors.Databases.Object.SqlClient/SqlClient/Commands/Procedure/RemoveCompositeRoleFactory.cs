﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveCompositeRoleFactory.cs" company="Allors bvba">
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
    using Allors.Adapters.Database.Sql.Commands;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class RemoveCompositeRoleFactory : IRemoveCompositeRoleFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        internal RemoveCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        public IRemoveCompositeRole Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new RemoveCompositeRole(this, session);
        }

        internal string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                string sql;
                var associationType = roleType.AssociationType;

                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Schema.AllorsPrefix + "R_" + roleType.SingularFullName;
                }
                else
                {
                    sql = Schema.AllorsPrefix + "R_" + ((IComposite)roleType.ObjectType).ExclusiveLeafClass.Name + "_" + associationType.SingularFullName;
                }
 
                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        private class RemoveCompositeRole : DatabaseCommand, IRemoveCompositeRole
        {
            private readonly RemoveCompositeRoleFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            public RemoveCompositeRole(RemoveCompositeRoleFactory factory, Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
            }

            public void Execute(IList<CompositeRelation> relations, IRoleType roleType)
            {
                SqlCommand command;
                if (!this.commandByIRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, this.Database.SqlClientSchema.CompositeRelationTableParam, this.Database.CreateRelationTable(relations));

                    this.commandByIRoleType[roleType] = command;
                }
                else
                {
                    this.SetInTable(command, this.Database.SqlClientSchema.CompositeRelationTableParam, this.Database.CreateRelationTable(relations));
                }

                command.ExecuteNonQuery();
            }
    }
    }
}