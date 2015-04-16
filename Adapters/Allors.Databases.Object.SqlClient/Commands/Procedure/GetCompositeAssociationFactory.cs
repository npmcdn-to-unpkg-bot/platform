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

    internal class GetCompositeAssociationFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IAssociationType, string> sqlByIAssociationType;

        internal GetCompositeAssociationFactory(Database database)
        {
            this.Database = database;
            this.sqlByIAssociationType = new Dictionary<IAssociationType, string>();
        }

        internal GetCompositeAssociation Create(DatabaseSession session)
        {
            return new GetCompositeAssociation(this, session);
        }

        internal string GetSql(IAssociationType associationType)
        {
            if (!this.sqlByIAssociationType.ContainsKey(associationType))
            {
                var roleType = associationType.RoleType;

                string sql;
                if (associationType.RelationType.ExistExclusiveLeafClasses)
                {
                    if (roleType.IsOne)
                    {
                        sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[associationType.ObjectType.ExclusiveLeafClass][roleType.RelationType];
                    }
                    else
                    {
                        sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveLeafClass][roleType.RelationType];
                    }
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationType[roleType.RelationType];
                }

                this.sqlByIAssociationType[associationType] = sql;
            }

            return this.sqlByIAssociationType[associationType];
        }

        internal class GetCompositeAssociation : DatabaseCommand
        {
            private readonly GetCompositeAssociationFactory factory;
            private readonly Dictionary<IAssociationType, SqlCommand> commandByIAssociationType;

            internal GetCompositeAssociation(GetCompositeAssociationFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIAssociationType = new Dictionary<IAssociationType, SqlCommand>();
            }

            internal Reference Execute(Reference role, IAssociationType associationType)
            {
                Reference associationObject = null;

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

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    ObjectId id = this.Database.ObjectIds.Parse(result.ToString());

                    if (associationType.ObjectType.ExistExclusiveLeafClass)
                    {
                        associationObject = this.Session.GetOrCreateAssociationForExistingObject(associationType.ObjectType.ExclusiveLeafClass, id);
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