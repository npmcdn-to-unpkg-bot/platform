// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetIObjectTypeFactory.cs" company="Allors bvba">
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
    using System;
    using System.Data.SqlClient;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class GetIObjectTypeFactory
    {
        internal readonly Database Database;
        internal readonly string Sql;

        internal GetIObjectTypeFactory(Database database)
        {
            this.Database = database;
            this.Sql = "SELECT " + database.Mapping.TypeId + "\n";
            this.Sql += "FROM " + database.Mapping.Objects + "\n";
            this.Sql += "WHERE " + database.Mapping.ObjectId + "=" + database.Mapping.ObjectId.Param + "\n";
        }

        internal GetIObjectType Create(DatabaseSession session)
        {
            return new GetIObjectType(this, session);
        }

        internal class GetIObjectType : DatabaseCommand
        {
            private readonly GetIObjectTypeFactory factory;
            private SqlCommand command;

            internal GetIObjectType(GetIObjectTypeFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
            }

            internal IClass Execute(ObjectId objectId)
            {
                if (this.command == null)
                {
                    this.command = this.Session.CreateSqlCommand(this.factory.Sql);
                    this.AddInObject(this.command, this.Database.Mapping.ObjectId.Param, objectId.Value);
                }
                else
                {
                    this.SetInObject(this.command, this.Database.Mapping.ObjectId.Param, objectId.Value);
                }

                var result = this.command.ExecuteScalar();
                if (result == null)
                {
                    return null;
                }

                return (IClass)this.Session.SqlClientDatabase.ObjectFactory.GetObjectTypeForType((Guid)result);
            }
        }
    }
}