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

namespace Allors.Adapters.Database.SqlClient.Commands.Procedure
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

        public ICreateObject Create(Sql.DatabaseSession session)
        {
            return new CreateObject(session);
        }

        private class CreateObject : DatabaseCommand, ICreateObject
        {
            private readonly Dictionary<MetaObject, SqlCommand> commandByObjectType;

            public CreateObject(Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.commandByObjectType = new Dictionary<MetaObject, SqlCommand>();
            }

            public Reference Execute(MetaObject objectType)
            {
                var exclusiveRootClass = objectType.ExclusiveRootClass;
                var schema = this.Database.Schema;

                SqlCommand command;
                if (!this.commandByObjectType.TryGetValue(exclusiveRootClass, out command))
                {
                    command = this.Session.CreateSqlCommand(Sql.Schema.AllorsPrefix + "CO_" + exclusiveRootClass.Name);
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, schema.TypeId.Param, objectType.Id);

                    this.commandByObjectType[exclusiveRootClass] = command;
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