// --------------------------------------------------------------------------------------------------------------------
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

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class CreateObjectsFactory
    {
        internal readonly Database Database;

        internal CreateObjectsFactory(Database database)
        {
            this.Database = database;
        }

        internal CreateObjects Create(DatabaseSession session)
        {
            return new CreateObjects(this, session);
        }

        internal class CreateObjects : DatabaseCommand
        {
            private readonly CreateObjectsFactory factory;
            private readonly Dictionary<IObjectType, SqlCommand> commandByIObjectType;

            internal CreateObjects(CreateObjectsFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIObjectType = new Dictionary<IObjectType, SqlCommand>();
            }

            internal IList<Reference> Execute(IClass objectType, int count)
            {
                IObjectType exclusiveRootClass = ((IComposite)objectType).ExclusiveLeafClass;
                SqlClient.Mapping mapping = this.Database.Mapping;

                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(exclusiveRootClass, out command))
                {
                    command = this.Session.CreateSqlCommand(SqlClient.Mapping.AllorsPrefix + "COS_" + exclusiveRootClass.Name);
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, mapping.TypeId.Param, objectType.Id);
                    this.AddInObject(command, mapping.CountParam, count);

                    this.commandByIObjectType[exclusiveRootClass] = command;
                }
                else
                {
                    this.SetInObject(command, mapping.TypeId.Param, objectType.Id);
                    this.SetInObject(command, mapping.CountParam, count);
                }

                var objectIds = new List<object>();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object id = this.Database.ObjectIds.Parse(reader[0].ToString());
                        objectIds.Add(id);
                    }
                }

                var strategies = new List<Reference>();

                foreach (object id in objectIds)
                {
                    ObjectId objectId = this.factory.Database.ObjectIds.Parse(id.ToString());
                    var strategySql = this.Session.CreateAssociationForNewObject(objectType, objectId);
                    strategies.Add(strategySql);
                }

                return strategies;
            }
        }
    }
}