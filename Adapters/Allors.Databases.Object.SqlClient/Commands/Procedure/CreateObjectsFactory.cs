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
    using System;
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
                var exclusiveRootClass = objectType.ExclusiveLeafClass;
                var mapping = this.Database.Mapping;

                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(exclusiveRootClass, out command))
                {
                    var sql = this.Database.Mapping.ProcedureNameForCreateObjectsByClass[exclusiveRootClass];
                    command = this.Session.CreateSqlCommand(sql);
                    command.CommandType = CommandType.StoredProcedure;
                    var sqlParameter = command.CreateParameter();
                    sqlParameter.ParameterName = Mapping.ParamNameForType;
                    sqlParameter.SqlDbType = Mapping.SqlDbTypeForType;
                    sqlParameter.Value = (object)objectType.Id ?? DBNull.Value;

                    command.Parameters.Add(sqlParameter);
                    var sqlParameter1 = command.CreateParameter();
                    sqlParameter1.ParameterName = Mapping.ParamNameForCount;
                    sqlParameter1.SqlDbType = Mapping.SqlDbTypeForCount;
                    sqlParameter1.Value = (object)count ?? DBNull.Value;

                    command.Parameters.Add(sqlParameter1);

                    this.commandByIObjectType[exclusiveRootClass] = command;
                }
                else
                {
                    command.Parameters[Mapping.ParamNameForType].Value = (object)objectType.Id ?? DBNull.Value;
                    command.Parameters[Mapping.ParamNameForCount].Value = (object)count ?? DBNull.Value;
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