// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCompositeAssociationsFactory.cs" company="Allors bvba">
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

    using Allors.Databases.Object.SqlClient;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class GetCompositeAssociationsFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IAssociationType, string> sqlByIAssociationType;

        internal GetCompositeAssociationsFactory(Database database)
        {
            this.Database = database;
            this.sqlByIAssociationType = new Dictionary<IAssociationType, string>();
        }

        internal GetCompositeAssociations Create(DatabaseSession session)
        {
            return new GetCompositeAssociations(this, session);
        }

        internal string GetSql(IAssociationType associationType)
        {
            if (!this.sqlByIAssociationType.ContainsKey(associationType))
            {
                IRoleType roleType = associationType.RoleType;

                string sql;
                if (roleType.IsMany || !associationType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[associationType.ObjectType.ExclusiveLeafClass][roleType.RelationType];
                }

                this.sqlByIAssociationType[associationType] = sql;
            }

            return this.sqlByIAssociationType[associationType];
        }

        internal class GetCompositeAssociations : DatabaseCommand
        {
            private readonly GetCompositeAssociationsFactory factory;
            private readonly Dictionary<IAssociationType, SqlCommand> commandByIAssociationType;

            internal GetCompositeAssociations(GetCompositeAssociationsFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIAssociationType = new Dictionary<IAssociationType, SqlCommand>();
            }

            internal ObjectId[] Execute(Strategy role, IAssociationType associationType)
            {
                SqlCommand command;
                if (!this.commandByIAssociationType.TryGetValue(associationType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(associationType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, Mapping.ParamNameForRole, this.factory.Database.Mapping.SqlDbTypeForObject, role.ObjectId.Value);

                    this.commandByIAssociationType[associationType] = command;
                }
                else
                {
                    this.SetInObject(command, Mapping.ParamNameForRole, role.ObjectId.Value);
                }

                var objectIds = new List<ObjectId>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObjectId id = this.Database.ObjectIds.Parse(reader[0].ToString());
                        objectIds.Add(id);
                    }
                }

                return objectIds.ToArray();
            }
        }
    }
}