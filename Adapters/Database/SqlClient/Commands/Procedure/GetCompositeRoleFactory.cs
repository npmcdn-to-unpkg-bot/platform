// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCompositeRoleFactory.cs" company="Allors bvba">
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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class GetCompositeRoleFactory : IGetCompositeRoleFactory
    {
        public readonly Database Database;
        private readonly Dictionary<RoleType, string> sqlByRoleType;

        public GetCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleType = new Dictionary<RoleType, string>();
        }

        public IGetCompositeRole Create(Sql.DatabaseSession session)
        {
            return new GetCompositeRole(this, session);
        }

        public string GetSql(RoleType roleType)
        {
            if (!this.sqlByRoleType.ContainsKey(roleType))
            {
                AssociationType associationType = roleType.AssociationType;

                string sql;
                if (!roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "GR_" + roleType.FullSingularName;
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "GR_" + associationType.ObjectType.ExclusiveRootClass.Name + "_" + roleType.RootName;
                }

                this.sqlByRoleType[roleType] = sql;
            }

            return this.sqlByRoleType[roleType];
        }

        private class GetCompositeRole : DatabaseCommand, IGetCompositeRole
        {
            private readonly GetCompositeRoleFactory factory;
            private readonly Dictionary<RoleType, SqlCommand> commandByRoleType;

            public GetCompositeRole(GetCompositeRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleType = new Dictionary<RoleType, SqlCommand>();
            }

            public void Execute(Roles roles, RoleType roleType)
            {
                var reference = roles.Reference;

                SqlCommand command;
                if (!this.commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, this.Database.Schema.AssociationId.Param, reference.ObjectId.Value);

                    this.commandByRoleType[roleType] = command;
                }
                else
                {
                    this.SetInObject(command, this.Database.Schema.AssociationId.Param, reference.ObjectId.Value);
                }

                object result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    roles.CachedObject.SetValue(roleType, null);
                }
                else
                {
                    var objectId = this.Database.AllorsObjectIds.Parse(result.ToString());
                    roles.CachedObject.SetValue(roleType, objectId);
                }
            }
        }
    }
}