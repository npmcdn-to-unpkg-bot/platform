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
                var schema = this.factory.Database.SqlClientMapping;

                if (this.command == null)
                {
                    this.command = this.Session.CreateSqlCommand(SqlClient.Mapping.AllorsPrefix + "UC");
                    this.command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(this.command, schema.ObjectTableParam, this.Database.CreateObjectTable(modifiedRolesByReference.Keys));
                }
                else
                {
                    this.SetInTable(this.command, schema.ObjectTableParam, this.Database.CreateObjectTable(modifiedRolesByReference.Keys));
                }

                this.command.ExecuteNonQuery();
            }
        }
    }
}