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

namespace Allors.R1.Adapters.Database.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.R1.Meta;

    using Database = Database;
    using DatabaseSession = Allors.R1.Adapters.Database.SqlClient.DatabaseSession;

    internal class RemoveCompositeRoleFactory : Sql.Commands.IRemoveCompositeRoleFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<RoleType, string> sqlByRoleType;

        internal RemoveCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleType = new Dictionary<RoleType, string>();
        }

        public Sql.Commands.IRemoveCompositeRole Create(Sql.DatabaseSession session)
        {
            return new RemoveCompositeRole(this, session);
        }

        internal string GetSql(RoleType roleType)
        {
            if (!this.sqlByRoleType.ContainsKey(roleType))
            {
                string sql;
                var associationType = roleType.AssociationType;

                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "R_" + roleType.FullSingularName;
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "R_" + roleType.ObjectType.ExclusiveRootClass.Name + "_" + associationType.RootName;
                }
 
                this.sqlByRoleType[roleType] = sql;
            }

            return this.sqlByRoleType[roleType];
        }

        private class RemoveCompositeRole : DatabaseCommand, Sql.Commands.IRemoveCompositeRole
        {
            private readonly RemoveCompositeRoleFactory factory;
            private readonly Dictionary<RoleType, SqlCommand> commandByRoleType;

            public RemoveCompositeRole(RemoveCompositeRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleType = new Dictionary<RoleType, SqlCommand>();
            }

            public void Execute(IList<CompositeRelation> relations, RoleType roleType)
            {
                SqlCommand command;
                if (!this.commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, this.Database.SqlClientSchema.CompositeRelationTableParam, this.Database.CreateRelationTable(relations));

                    this.commandByRoleType[roleType] = command;
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