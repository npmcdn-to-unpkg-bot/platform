﻿// --------------------------------------------------------------------------------------------------------------------
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
                sql += "    SELECT " + Mapping.ColumnNameForObject + "\n";
                sql += "    FROM " + this.Database.SchemaName + "." + schema.TableNameForObjectByClass[objectType.ExclusiveLeafClass] + "\n";
                sql += "    WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n";
                sql += ")\n";
                sql += "    SELECT 1\n";
                sql += "ELSE\n";
                sql += "    BEGIN\n";

                sql += "    SET IDENTITY_INSERT " + schema.TableNameForObjects + " ON\n";

                sql += "    INSERT INTO " + this.Database.SchemaName + "." + schema.TableNameForObjects + " (" + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + "," + Mapping.ColumnNameForCache + ")\n";
                sql += "    VALUES (" + Mapping.ParamNameForObject + "," + Mapping.ParamNameForType + ", " + Reference.InitialCacheId + ");\n";

                sql += "    SET IDENTITY_INSERT " + schema.TableNameForObjects + " OFF;\n";

                sql += "    INSERT INTO " + this.Database.SchemaName + "." + schema.TableNameForObjectByClass[objectType.ExclusiveLeafClass] + " (" + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + ")\n";
                sql += "    VALUES (" + Mapping.ParamNameForObject + "," + Mapping.ParamNameForType + ");\n";

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

                    this.AddInObject(command, Mapping.ParamNameForObject, this.Database.Mapping.SqlDbTypeForObject, objectId.Value);
                    this.AddInObject(command, Mapping.ParamNameForType, Mapping.SqlDbTypeForType, objectType.Id);

                    this.commandByIObjectType[objectType] = command;
                }
                else
                {
                    this.SetInObject(command, Mapping.ParamNameForObject, objectId.Value);
                    this.SetInObject(command, Mapping.ParamNameForType, objectType.Id);
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