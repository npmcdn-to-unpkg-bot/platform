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

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class InstantiateObjectFactory
    {
        internal readonly Database Database;
        internal readonly string Sql;

        internal InstantiateObjectFactory(Database database)
        {
            this.Database = database;
            this.Sql = "SELECT " + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + "\n";
            this.Sql += "FROM " + database.Mapping.TableNameForObjects + "\n";
            this.Sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n";
        }

        internal InstantiateObject Create(DatabaseSession session)
        {
            return new InstantiateObject(this, session);
        }

        internal class InstantiateObject : DatabaseCommand
        {
            private readonly InstantiateObjectFactory factory;
            private SqlCommand command;

            internal InstantiateObject(InstantiateObjectFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
            }

            internal Reference Execute(ObjectId objectId)
            {
                if (this.command == null)
                {
                    this.command = this.Session.CreateSqlCommand(this.factory.Sql);
                    this.AddInObject(this.command, Mapping.ParamNameForObject, this.Database.Mapping.SqlDbTypeForObject, objectId.Value);
                }
                else
                {
                    this.SetInObject(this.command, Mapping.ParamNameForObject, objectId.Value);
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