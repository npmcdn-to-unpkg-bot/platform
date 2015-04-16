// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadObjectsFactory.cs" company="Allors bvba">
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

    using Allors.Meta;

    internal class LoadObjectsFactory
    {
        internal readonly SqlClient.ManagementSession ManagementSession;

        internal LoadObjectsFactory(SqlClient.ManagementSession session)
        {
            this.ManagementSession = session;
        }

        internal LoadObjects Create(IObjectType objectType)
        {
            return new LoadObjects(this);
        }

        internal class LoadObjects : Commands.Command
        {
            private readonly LoadObjectsFactory factory;

            internal LoadObjects(LoadObjectsFactory factory)
            {
                this.factory = factory;
            }

            internal void Execute(IObjectType objectType, IEnumerable<ObjectId> objectIds)
            {
                var database = this.factory.ManagementSession.SqlClientDatabase;

                var exclusiveRootClass = ((IComposite)objectType).ExclusiveLeafClass;
                var schema = database.Mapping;

                lock (database)
                {
                    var sql = this.factory.ManagementSession.Database.Mapping.ProcedureNameForLoadObjectByClass[exclusiveRootClass];
                    using (var command = this.factory.ManagementSession.CreateSqlCommand(sql))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        this.AddInObject(command, Mapping.ParamNameForType, Mapping.SqlDbTypeForType, objectType.Id);
                        this.AddInTable(command, schema.TableTypeNameForObject, database.CreateObjectTable(objectIds));
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}