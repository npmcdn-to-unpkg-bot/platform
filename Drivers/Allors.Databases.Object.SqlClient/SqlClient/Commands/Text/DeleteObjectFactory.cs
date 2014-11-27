// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteObjectFactory.cs" company="Allors bvba">
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

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    public class DeleteObjectFactory : IDeleteObjectFactory
    {
        public readonly Database Database;
        private readonly Dictionary<IObjectType, string> sqlByMetaType;

        public DeleteObjectFactory(Database database)
        {
            this.Database = database;
            this.sqlByMetaType = new Dictionary<IObjectType, string>();
        }

        public IDeleteObject Create(Adapters.Database.Sql.DatabaseSession session)
        {
            return new DeleteObject(this, session);
        }

        public string GetSql(IObjectType objectType)
        {
            if (!this.sqlByMetaType.ContainsKey(objectType))
            {
                var schema = this.Database.Schema;

                var sql = string.Empty;

                sql += "BEGIN\n";

                sql += "DELETE FROM " + schema.Objects + "\n";
                sql += "WHERE " + schema.ObjectId + "=" + schema.ObjectId.Param + ";\n";

                sql += "DELETE FROM " + schema.Table(((IComposite)objectType).ExclusiveLeafClass) + "\n";
                sql += "WHERE " + schema.ObjectId + "=" + schema.ObjectId.Param + ";\n";

                sql += "END;";

                this.sqlByMetaType[objectType] = sql;
            }

            return this.sqlByMetaType[objectType];
        }

        private class DeleteObject : DatabaseCommand, IDeleteObject
        {
            private readonly DeleteObjectFactory factory;
            private readonly Dictionary<IObjectType, SqlCommand> commandByIObjectType;

            public DeleteObject(DeleteObjectFactory factory, Adapters.Database.Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIObjectType = new Dictionary<IObjectType, SqlCommand>();
            }

            public void Execute(Strategy strategy)
            {
                var objectType = strategy.ObjectType;

                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(objectType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(objectType));
                    this.AddInObject(command, this.Database.Schema.ObjectId.Param, strategy.ObjectId.Value);

                    this.commandByIObjectType[objectType] = command;
                }
                else
                {
                    this.SetInObject(command, this.Database.Schema.ObjectId.Param, strategy.ObjectId.Value);
                }
                
                command.ExecuteNonQuery();
            }
        }
    }
}