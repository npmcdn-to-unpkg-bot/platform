// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCacheIdsFactory.cs" company="Allors bvba">
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

    internal class GetCacheIdsFactory
    {
        private readonly Database database;

        internal GetCacheIdsFactory(Database database)
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

        internal GetCacheIds Create(DatabaseSession session)
        {
            return new GetCacheIds(this, session);
        }

        internal class GetCacheIds : DatabaseCommand
        {
            private readonly GetCacheIdsFactory factory;
            private SqlCommand command;

            internal GetCacheIds(GetCacheIdsFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
            }

            internal Dictionary<ObjectId, int> Execute(ISet<Reference> strategyReferences)
            {
                var schema = this.factory.Database.SqlClientMapping;

                if (this.command == null)
                {
                    this.command = this.Session.CreateSqlCommand(Mapping.AllorsPrefix + "GC");
                    this.command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(this.command, schema.ObjectTableParam, this.Database.CreateObjectTable(strategyReferences));
                }
                else
                {
                    this.SetInTable(this.command, schema.ObjectTableParam, this.Database.CreateObjectTable(strategyReferences));
                }

                var cacheIdByObjectId = new Dictionary<ObjectId, int>();

                using (var reader = this.command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectId = this.Database.ObjectIds.Parse(reader[0].ToString());
                        var cacheId = reader.GetInt32(1);

                        cacheIdByObjectId.Add(objectId, cacheId);
                    }
                }

                return cacheIdByObjectId;
            }
        }
    }
}