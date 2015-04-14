// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InstantiateObjectsFactory.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class InstantiateObjectsFactory
    {
        internal readonly Database Database;
        internal readonly string Sql;

        internal InstantiateObjectsFactory(Database database)
        {
            this.Database = database;
            this.Sql += "SELECT " + database.Mapping.ObjectId + "," + database.Mapping.TypeId + "," + database.Mapping.CacheId + "\n";
            this.Sql += "FROM " + database.Mapping.Objects + "\n";
            this.Sql += "WHERE " + database.Mapping.ObjectId + " IN\n";
            this.Sql += "( SELECT " + Mapping.ColumnNameForObject + " FROM " + this.Database.SqlClientMapping.ObjectTableParam.Name + " )\n";
        }

        internal InstantiateObjects Create(DatabaseSession session)
        {
            return new InstantiateObjects(this, session);
        }

        internal class InstantiateObjects : DatabaseCommand
        {
            private readonly InstantiateObjectsFactory factory;
            private SqlCommand command;

            internal InstantiateObjects(InstantiateObjectsFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
            }

            internal IList<Reference> Execute(IList<ObjectId> objectids)
            {
                var strategies = new List<Reference>();

                if (this.command == null)
                {
                    this.command = this.Session.CreateSqlCommand(this.factory.Sql);
                    this.AddInTable(this.command, this.Database.SqlClientMapping.ObjectTableParam, this.Database.CreateObjectTable(objectids));
                }
                else
                {
                    this.SetInTable(this.command, this.Database.SqlClientMapping.ObjectTableParam, this.Database.CreateObjectTable(objectids));
                }

                using (var reader = this.command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectIdString = reader.GetValue(0).ToString();
                        var classId = this.GetClassId(reader, 1);
                        var cacheId = this.GetCachId(reader, 2);

                        var objectId = this.Database.ObjectIds.Parse(objectIdString);
                        var type = (IClass)this.Database.ObjectFactory.GetObjectTypeForType(classId);
                        strategies.Add(this.Session.GetOrCreateAssociationForExistingObject(type, objectId, cacheId));
                    }
                }

                return strategies;
            }
        }
    }
}