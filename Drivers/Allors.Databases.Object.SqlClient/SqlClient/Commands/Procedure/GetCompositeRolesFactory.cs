// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCompositeRolesFactory.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters.Database.SqlClient.Commands.Text
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.R1.Adapters.Database.Sql;
    using Allors.R1.Adapters.Database.Sql.Commands;

    using Meta;

    using Database = Database;
    using DatabaseSession = Allors.R1.Adapters.Database.SqlClient.DatabaseSession;
    using Schema = Schema;

    public class GetCompositeRolesFactory : IGetCompositeRolesFactory
    {
        public readonly Database Database;
        private readonly Dictionary<RoleType, string> sqlByRoleType;

        public GetCompositeRolesFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleType = new Dictionary<RoleType, string>();
        }

        public IGetCompositeRoles Create(Sql.DatabaseSession session)
        {
            return new GetCompositeRoles(this, session);
        }

        public string GetSql(RoleType roleType)
        {
            if (!this.sqlByRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "GR_" + roleType.FullSingularName;
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "GR_" + roleType.ObjectType.ExclusiveRootClass.Name + "_" + associationType.RootName;
                }
 
                this.sqlByRoleType[roleType] = sql;
            }

            return this.sqlByRoleType[roleType];
        }

        private class GetCompositeRoles : DatabaseCommand, IGetCompositeRoles
        {
            private readonly GetCompositeRolesFactory factory;
            private readonly Dictionary<RoleType, SqlCommand> commandByRoleType;

            public GetCompositeRoles(GetCompositeRolesFactory factory, Sql.DatabaseSession session)
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

                var objectIds = new List<ObjectId>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = this.Database.AllorsObjectIds.Parse(reader[0].ToString());
                        objectIds.Add(id);
                    }
                }

                roles.CachedObject.SetValue(roleType, objectIds.ToArray());
            }
        }
    }
}