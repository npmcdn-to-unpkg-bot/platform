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

namespace Allors.Adapters.Database.SqlClient.Commands.Text
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class GetCompositeAssociationsFactory : IGetCompositeAssociationsFactory
    {
        public readonly Database Database;
        private readonly Dictionary<MetaAssociation, string> sqlByAssociationType;

        public GetCompositeAssociationsFactory(Database database)
        {
            this.Database = database;
            this.sqlByAssociationType = new Dictionary<MetaAssociation, string>();
        }

        public IGetCompositeAssociations Create(Sql.DatabaseSession session)
        {
            return new GetCompositeAssociations(this, session);
        }

        public string GetSql(MetaAssociation associationType)
        {
            if (!this.sqlByAssociationType.ContainsKey(associationType))
            {
                MetaRole roleType = associationType.RoleType;

                string sql;
                if (roleType.IsMany || !associationType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Sql.Schema.AllorsPrefix + "GA_" + roleType.FullSingularName;
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "GA_" + associationType.ObjectType.ExclusiveRootClass.Name + "_" + associationType.RootName;
                }

                this.sqlByAssociationType[associationType] = sql;
            }

            return this.sqlByAssociationType[associationType];
        }

        public class GetCompositeAssociations : DatabaseCommand, IGetCompositeAssociations
        {
            private readonly GetCompositeAssociationsFactory factory;
            private readonly Dictionary<MetaAssociation, SqlCommand> commandByAssociationType;

            public GetCompositeAssociations(GetCompositeAssociationsFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByAssociationType = new Dictionary<MetaAssociation, SqlCommand>();
            }

            public ObjectId[] Execute(Strategy role, MetaAssociation associationType)
            {
                SqlCommand command;
                if (!this.commandByAssociationType.TryGetValue(associationType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(associationType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, this.Database.Schema.RoleId.Param, role.ObjectId.Value);

                    this.commandByAssociationType[associationType] = command;
                }
                else
                {
                    this.SetInObject(command, this.Database.Schema.RoleId.Param, role.ObjectId.Value);
                }

                var objectIds = new List<ObjectId>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObjectId id = this.Database.AllorsObjectIds.Parse(reader[0].ToString());
                        objectIds.Add(id);
                    }
                }

                return objectIds.ToArray();
            }
        }
    }
}