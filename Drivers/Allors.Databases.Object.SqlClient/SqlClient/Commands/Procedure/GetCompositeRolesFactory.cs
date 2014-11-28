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

namespace Allors.Databases.Object.SqlClient.Commands.Text
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class GetCompositeRolesFactory
    {
        public readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        public GetCompositeRolesFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        public GetCompositeRoles Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new GetCompositeRoles(this, session);
        }

        public string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Adapters.Database.Sql.Schema.AllorsPrefix + "GR_" + roleType.SingularFullName;
                }
                else
                {
                    sql = Adapters.Database.Sql.Schema.AllorsPrefix + "GR_" + ((IComposite)roleType.ObjectType).ExclusiveLeafClass.Name + "_" + associationType.SingularFullName;
                }
 
                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        public class GetCompositeRoles : DatabaseCommand
        {
            private readonly GetCompositeRolesFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            public GetCompositeRoles(GetCompositeRolesFactory factory, Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
            }

            public void Execute(Roles roles, IRoleType roleType)
            {
                var reference = roles.Reference;

                SqlCommand command;
                if (!this.commandByIRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, this.Database.Schema.AssociationId.Param, reference.ObjectId.Value);

                    this.commandByIRoleType[roleType] = command;
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