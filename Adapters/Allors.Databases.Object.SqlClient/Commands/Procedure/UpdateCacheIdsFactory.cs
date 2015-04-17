// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateCacheIdsFactory.cs" company="Allors bvba">
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
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;

    using Microsoft.SqlServer.Server;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class UpdateCacheIdsFactory
    {
        private readonly Database database;

        internal UpdateCacheIdsFactory(Database database)
        {
            this.database = database;
        }

        internal Database Database
        {
            get
            {
                return this.database;
            }
        }

        internal UpdateCacheIds Create(DatabaseSession session)
        {
            return new UpdateCacheIds(this, session);
        }

        internal class UpdateCacheIds : DatabaseCommand
        {
            private readonly UpdateCacheIdsFactory factory;
            private SqlCommand command;

            internal UpdateCacheIds(UpdateCacheIdsFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
            }

            internal void Execute(Dictionary<Reference, Roles> modifiedRolesByReference)
            {
                var schema = this.factory.Database.Mapping;

                if (this.command == null)
                {
                    var sql = this.Database.Mapping.ProcedureNameForUpdateCache;
                    this.command = this.Session.CreateSqlCommand(sql);
                    this.command.CommandType = CommandType.StoredProcedure;
                    var sqlParameter = this.command.CreateParameter();
                    sqlParameter.SqlDbType = SqlDbType.Structured;
                    sqlParameter.TypeName = schema.TableTypeNameForObject;
                    sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                    sqlParameter.Value = this.Database.CreateObjectTable(modifiedRolesByReference.Keys);

                    this.command.Parameters.Add(sqlParameter);
                }
                else
                {
                    this.command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(modifiedRolesByReference.Keys);
                }

                this.command.ExecuteNonQuery();
            }
        }
    }
}