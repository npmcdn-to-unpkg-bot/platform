// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InstantiateObjectFactory.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient.Commands.Text
{
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    public class InstantiateObjectFactory
    {
        public readonly Database Database;
        public readonly string Sql;

        public InstantiateObjectFactory(Database database)
        {
            this.Database = database;
            this.Sql = "SELECT " + database.Schema.TypeId + ", " + database.Schema.CacheId + "\n";
            this.Sql += "FROM " + database.Schema.Objects + "\n";
            this.Sql += "WHERE " + database.Schema.ObjectId + "=" + database.Schema.ObjectId.Param + "\n";
        }

        public InstantiateObject Create(DatabaseSession session)
        {
            return new InstantiateObject(this, session);
        }

        public class InstantiateObject : DatabaseCommand
        {
            private readonly InstantiateObjectFactory factory;
            private SqlCommand command;

            public InstantiateObject(InstantiateObjectFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
            }

            public Reference Execute(ObjectId objectId)
            {
                if (this.command == null)
                {
                    this.command = this.Session.CreateSqlCommand(this.factory.Sql);
                    this.AddInObject(this.command, this.Database.Schema.ObjectId.Param, objectId.Value);
                }
                else
                {
                    this.SetInObject(this.command, this.Database.Schema.ObjectId.Param, objectId.Value);
                }

                using (var reader = this.command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var classId = this.GetClassId(reader, 0);
                        var cacheId = this.GetCachId(reader, 1);

                        var type = (IClass)this.factory.Database.MetaPopulation.Find(classId);
                        return this.Session.GetOrCreateAssociationForExistingObject(type, objectId, cacheId);
                    }

                    return null;
                }
            }
        }
    }
}