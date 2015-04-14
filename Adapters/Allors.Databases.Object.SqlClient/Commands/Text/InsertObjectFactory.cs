// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsertObjectFactory.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class InsertObjectFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IObjectType, string> sqlByMetaType;

        internal InsertObjectFactory(Database database)
        {
            this.Database = database;
            this.sqlByMetaType = new Dictionary<IObjectType, string>();
        }

        internal InsertObject Create(DatabaseSession session)
        {
            return new InsertObject(this, session);
        }

        internal string GetSql(IClass objectType)
        {
            if (!this.sqlByMetaType.ContainsKey(objectType))
            {
                var schema = this.Database.Mapping;

                // TODO: Make this a single pass Query.
                var sql = "IF EXISTS (\n";
                sql += "    SELECT " + schema.ObjectId + "\n";
                sql += "    FROM " + schema.Table(objectType.ExclusiveLeafClass) + "\n";
                sql += "    WHERE " + schema.ObjectId + "=" + schema.ObjectId.Param + "\n";
                sql += ")\n";
                sql += "    SELECT 1\n";
                sql += "ELSE\n";
                sql += "    BEGIN\n";

                sql += "    SET IDENTITY_INSERT " + schema.Objects + " ON\n";

                sql += "    INSERT INTO " + schema.Objects + " (" + schema.ObjectId + "," + schema.TypeId + "," + schema.CacheId + ")\n";
                sql += "    VALUES (" + schema.ObjectId.Param + "," + schema.TypeId.Param + ", " + Reference.InitialCacheId + ");\n";

                sql += "    SET IDENTITY_INSERT " + schema.Objects.StatementName + " OFF;\n";

                sql += "    INSERT INTO " + schema.Table(objectType.ExclusiveLeafClass) + " (" + schema.ObjectId + "," + schema.TypeId + ")\n";
                sql += "    VALUES (" + schema.ObjectId.Param + "," + schema.TypeId.Param + ");\n";

                sql += "    SELECT 0;\n";
                sql += "    END";
                
                this.sqlByMetaType[objectType] = sql;
            }

            return this.sqlByMetaType[objectType];
        }

        internal class InsertObject : DatabaseCommand
        {
            private readonly InsertObjectFactory factory;
            private readonly Dictionary<IObjectType, SqlCommand> commandByIObjectType;

            internal InsertObject(InsertObjectFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIObjectType = new Dictionary<IObjectType, SqlCommand>();
            }

            internal Reference Execute(IClass objectType, ObjectId objectId)
            {
                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(objectType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(objectType));

                    this.AddInObject(command, this.Database.Mapping.ObjectId.Param, objectId.Value);
                    this.AddInObject(command, this.Database.Mapping.TypeId.Param, objectType.Id);

                    this.commandByIObjectType[objectType] = command;
                }
                else
                {
                    this.SetInObject(command, this.Database.Mapping.ObjectId.Param, objectId.Value);
                    this.SetInObject(command, this.Database.Mapping.TypeId.Param, objectType.Id);
                }

                var result = command.ExecuteScalar();
                if (result == null)
                {
                    throw new Exception("Reader returned no rows");
                }

                if (long.Parse(result.ToString()) > 0)
                {
                    throw new Exception("Duplicate id error");
                }

                return this.Session.CreateAssociationForNewObject(objectType, objectId);
            }
        }
    }
}