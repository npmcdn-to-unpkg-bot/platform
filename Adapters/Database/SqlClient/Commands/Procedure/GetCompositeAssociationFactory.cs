// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCompositeAssociationFactory.cs" company="Allors bvba">
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

    public class GetCompositeAssociationFactory : IGetCompositeAssociationFactory
    {
        public readonly Database Database;
        private readonly Dictionary<AssociationType, string> sqlByAssociationType;

        public GetCompositeAssociationFactory(Database database)
        {
            this.Database = database;
            this.sqlByAssociationType = new Dictionary<AssociationType, string>();
        }

        public IGetCompositeAssociation Create(Sql.DatabaseSession session)
        {
            return new GetCompositeAssociation(this, session);
        }

        public string GetSql(AssociationType associationType)
        {
            if (!this.sqlByAssociationType.ContainsKey(associationType))
            {
                var roleType = associationType.RoleType;

                string sql;
                if (associationType.RelationType.ExistExclusiveRootClasses)
                {
                    if (roleType.IsOne)
                    {
                        sql = Sql.Schema.AllorsPrefix + "GA_" + associationType.ObjectType.ExclusiveRootClass.Name + "_" + associationType.Name;
                    }
                    else
                    {
                        var compositeType = (Composite)roleType.ObjectType;
                        sql = Sql.Schema.AllorsPrefix + "GA_" + compositeType.ExclusiveRootClass.Name + "_" + associationType.Name;
                    }
                }
                else
                {
                    sql = Sql.Schema.AllorsPrefix + "GA_" + roleType.FullSingularName;
                }

                this.sqlByAssociationType[associationType] = sql;
            }

            return this.sqlByAssociationType[associationType];
        }

        private class GetCompositeAssociation : DatabaseCommand, IGetCompositeAssociation
        {
            private readonly GetCompositeAssociationFactory factory;
            private readonly Dictionary<AssociationType, SqlCommand> commandByAssociationType;

            public GetCompositeAssociation(GetCompositeAssociationFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByAssociationType = new Dictionary<AssociationType, SqlCommand>();
            }

            public Reference Execute(Reference role, AssociationType associationType)
            {
                Reference associationObject = null;

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

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    ObjectId id = this.Database.AllorsObjectIds.Parse(result.ToString());

                    if (associationType.ObjectType.RootClasses.Count == 1 && associationType.ObjectType.ExclusiveRootClass.ExclusiveRootClass != null)
                    {
                        associationObject = this.Session.GetOrCreateAssociationForExistingObject(associationType.ObjectType.ExclusiveRootClass.ExclusiveRootClass, id);
                    }
                    else
                    {
                        associationObject = this.Session.GetOrCreateAssociationForExistingObject(id);
                    }
                }

                return associationObject;
            }
        }
    }
}