﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateObjectsFactory.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters.Database.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.R1.Adapters.Database.Sql;
    using Allors.R1.Adapters.Database.Sql.Commands;
    using Allors.R1.Meta;

    using Database = Database;
    using DatabaseSession = Allors.R1.Adapters.Database.SqlClient.DatabaseSession;
    using Schema = Schema;

    internal class CreateObjectsFactory : ICreateObjectsFactory
    {
        internal readonly Database Database;

        internal CreateObjectsFactory(Database database)
        {
            this.Database = database;
        }

        public ICreateObjects Create(Sql.DatabaseSession session)
        {
            return new CreateObjects(this, session);
        }

        private class CreateObjects : DatabaseCommand, ICreateObjects
        {
            private readonly CreateObjectsFactory factory;
            private readonly Dictionary<ObjectType, SqlCommand> commandByObjectType;

            public CreateObjects(CreateObjectsFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByObjectType = new Dictionary<ObjectType, SqlCommand>();
            }

            public IList<Reference> Execute(ObjectType objectType, int count)
            {
                ObjectType exclusiveRootClass = objectType.ExclusiveRootClass;
                Sql.Schema schema = this.Database.Schema;

                SqlCommand command;
                if (!this.commandByObjectType.TryGetValue(exclusiveRootClass, out command))
                {
                    command = this.Session.CreateSqlCommand(Sql.Schema.AllorsPrefix + "COS_" + exclusiveRootClass.Name);
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, schema.TypeId.Param, objectType.Id);
                    this.AddInObject(command, schema.CountParam, count);

                    this.commandByObjectType[exclusiveRootClass] = command;
                }
                else
                {
                    this.SetInObject(command, schema.TypeId.Param, objectType.Id);
                    this.SetInObject(command, schema.CountParam, count);
                }

                var objectIds = new List<object>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object id = this.Database.AllorsObjectIds.Parse(reader[0].ToString());
                        objectIds.Add(id);
                    }
                }

                var strategies = new List<Reference>();

                foreach (object id in objectIds)
                {
                    ObjectId objectId = this.factory.Database.AllorsObjectIds.Parse(id.ToString());
                    var strategySql = this.Session.CreateAssociationForNewObject(objectType, objectId);
                    strategies.Add(strategySql);
                }

                return strategies;
            }
        }
    }
}