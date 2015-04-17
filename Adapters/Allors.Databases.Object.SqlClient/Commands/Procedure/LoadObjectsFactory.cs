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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Meta;

    using Microsoft.SqlServer.Server;

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

        internal class LoadObjects
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
                        var sqlParameter = command.CreateParameter();
                        sqlParameter.ParameterName = Mapping.ParamNameForType;
                        sqlParameter.SqlDbType = Mapping.SqlDbTypeForType;
                        sqlParameter.Value = (object)objectType.Id ?? DBNull.Value;

                        command.Parameters.Add(sqlParameter);
                        var sqlParameter1 = command.CreateParameter();
                        sqlParameter1.SqlDbType = SqlDbType.Structured;
                        sqlParameter1.TypeName = schema.TableTypeNameForObject;
                        sqlParameter1.ParameterName = Mapping.ParamNameForTableType;
                        sqlParameter1.Value = database.CreateObjectTable(objectIds);

                        command.Parameters.Add(sqlParameter1);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}