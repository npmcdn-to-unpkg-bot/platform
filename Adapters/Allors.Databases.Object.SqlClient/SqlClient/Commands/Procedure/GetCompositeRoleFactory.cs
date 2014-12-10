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

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    internal class GetCompositeRoleFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        internal GetCompositeRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        internal GetCompositeRole Create(DatabaseSession session)
        {
            return new GetCompositeRole(this, session);
        }

        internal string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                IAssociationType associationType = roleType.AssociationType;

                string sql;
                if (!roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = SqlClient.Schema.AllorsPrefix + "GR_" + roleType.SingularFullName;
                }
                else
                {
                    sql = SqlClient.Schema.AllorsPrefix + "GR_" + associationType.ObjectType.ExclusiveLeafClass.Name + "_" + roleType.SingularFullName;
                }

                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        internal class GetCompositeRole : DatabaseCommand
        {
            private readonly GetCompositeRoleFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            internal GetCompositeRole(GetCompositeRoleFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
            }

            internal void Execute(Roles roles, IRoleType roleType)
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