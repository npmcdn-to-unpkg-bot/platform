// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteObjectFactory.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.R1.Adapters.Database.Npgsql.Commands.Text
{
    using System.Collections.Generic;

    using Allors.R1.Adapters.Database.Sql;
    using Allors.R1.Adapters.Database.Sql.Commands;
    using Allors.R1.Meta;

    using global::Npgsql;

    using Database = Database;
    using DatabaseSession = Allors.R1.Adapters.Database.Npgsql.DatabaseSession;

    public class DeleteObjectFactory : IDeleteObjectFactory
    {
        public readonly Database Database;
        private readonly Dictionary<ObjectType, string> sqlByMetaType;

        public DeleteObjectFactory(Database database)
        {
            this.Database = database;
            this.sqlByMetaType = new Dictionary<ObjectType, string>();
        }

        public IDeleteObject Create(Sql.DatabaseSession session)
        {
            return new DeleteObject(this, session);
        }

        public string GetSql(ObjectType objectType)
        {
            if (!this.sqlByMetaType.ContainsKey(objectType))
            {
                Schema schema = this.Database.Schema;

                string sql = string.Empty;

                sql += "DELETE FROM " + schema.Objects + "\n";
                sql += "WHERE " + schema.ObjectId + "=" + schema.ObjectId.Param.InvocationName + ";\n";

                sql += "DELETE FROM " + schema.Table(objectType.ExclusiveRootClass) + "\n";
                sql += "WHERE " + schema.ObjectId + "=" + schema.ObjectId.Param.InvocationName + ";\n";

                this.sqlByMetaType[objectType] = sql;
            }

            return this.sqlByMetaType[objectType];
        }

        private class DeleteObject : DatabaseCommand, IDeleteObject
        {
            private readonly DeleteObjectFactory factory;
            private readonly Dictionary<ObjectType, NpgsqlCommand> commandByObjectType;

            public DeleteObject(DeleteObjectFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByObjectType = new Dictionary<ObjectType, NpgsqlCommand>();
            }

            public void Execute(Strategy strategy)
            {
                var objectType = strategy.ObjectType;

                NpgsqlCommand command;
                if (!this.commandByObjectType.TryGetValue(objectType, out command))
                {
                    command = this.Session.CreateNpgsqlCommand(this.factory.GetSql(objectType));
                    this.AddInObject(command, this.Database.Schema.ObjectId.Param, strategy.ObjectId.Value);

                    this.commandByObjectType[objectType] = command;
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