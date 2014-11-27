// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateObjectFactory.cs" company="Allors bvba">
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
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
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

    internal class CreateObjectFactory : ICreateObjectFactory
    {
        internal readonly Database Database;

        public CreateObjectFactory(Database database)
        {
            this.Database = database;
        }

        public ICreateObject Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new CreateObject(session);
        }

        private class CreateObject : DatabaseCommand, ICreateObject
        {
            private readonly Dictionary<IObjectType, SqlCommand> commandByIObjectType;

            public CreateObject(Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.commandByIObjectType = new Dictionary<IObjectType, SqlCommand>();
            }

            public Reference Execute(IClass objectType)
            {
                var exclusiveRootClass = ((IComposite)objectType).ExclusiveLeafClass;
                var schema = this.Database.Schema;

                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(exclusiveRootClass, out command))
                {
                    command = this.Session.CreateSqlCommand(Adapters.Database.Sql.Schema.AllorsPrefix + "CO_" + exclusiveRootClass.Name);
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, schema.TypeId.Param, objectType.Id);

                    this.commandByIObjectType[exclusiveRootClass] = command;
                }
                else
                {
                    this.SetInObject(command, schema.TypeId.Param, objectType.Id);
                }

                var result = command.ExecuteScalar();
                var objectId = this.Database.AllorsObjectIds.Parse(result.ToString());
                return this.Session.CreateAssociationForNewObject(objectType, objectId);
            }
        }
    }
}