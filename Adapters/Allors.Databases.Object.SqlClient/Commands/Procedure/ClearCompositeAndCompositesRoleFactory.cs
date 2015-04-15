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
    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class ClearCompositeAndCompositesRoleFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IRoleType, string> sqlByIRoleType;

        internal ClearCompositeAndCompositesRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByIRoleType = new Dictionary<IRoleType, string>();
        }

        internal ClearCompositeAndCompositesRole Create(DatabaseSession session)
        {
            return new ClearCompositeAndCompositesRole(this, session);
        }

        internal string GetSql(IRoleType roleType)
        {
            if (!this.sqlByIRoleType.ContainsKey(roleType))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if ((roleType.IsMany && associationType.IsMany) || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForClearRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    if (roleType.IsOne)
                    {
                        sql = this.Database.Mapping.ProcedureNameForClearRoleByRelationTypeByClass[associationType.ObjectType.ExclusiveLeafClass][roleType.RelationType];
                    }
                    else
                    {
                        sql = this.Database.Mapping.ProcedureNameForClearRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveLeafClass][roleType.RelationType];
                    }
                }

                this.sqlByIRoleType[roleType] = sql;
            }

            return this.sqlByIRoleType[roleType];
        }

        internal class ClearCompositeAndCompositesRole : DatabaseCommand
        {
            private readonly ClearCompositeAndCompositesRoleFactory factory;
            private readonly Dictionary<IRoleType, SqlCommand> commandByIRoleType;

            internal ClearCompositeAndCompositesRole(ClearCompositeAndCompositesRoleFactory factory, DatabaseSession session)
                : base(session)
            {
                this.factory = factory;
                this.commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
            }

            internal void Execute(IList<ObjectId> associations, IRoleType roleType)
            {
                var mapping = this.factory.Database.SqlClientMapping;

                SqlCommand command;
                if (!this.commandByIRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, mapping.TableTypeNameForObject, this.Database.CreateObjectTable(associations));

                    this.commandByIRoleType[roleType] = command;
                }
                else
                {
                    this.SetInTable(command, this.Database.CreateObjectTable(associations));
                }

                command.ExecuteNonQuery();
            }
        }
    }
}