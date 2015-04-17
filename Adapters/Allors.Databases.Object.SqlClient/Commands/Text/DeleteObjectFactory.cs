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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class DeleteObjectFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IObjectType, string> sqlByMetaType;

        internal DeleteObjectFactory(Database database)
        {
            this.Database = database;
            this.sqlByMetaType = new Dictionary<IObjectType, string>();
        }

        internal DeleteObject Create(DatabaseSession session)
        {
            return new DeleteObject(this, session);
        }

        internal string GetSql(IObjectType objectType)
        {
            if (!this.sqlByMetaType.ContainsKey(objectType))
            {
                var mapping = this.Database.Mapping;

                var sql = string.Empty;

                sql += "BEGIN\n";

                sql += "DELETE FROM " + mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + ";\n";

                sql += "DELETE FROM " + mapping.TableNameForObjectByClass[((IComposite)objectType).ExclusiveLeafClass] + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + ";\n";

                sql += "END;";

                this.sqlByMetaType[objectType] = sql;
            }

            return this.sqlByMetaType[objectType];
        }

        internal class DeleteObject : DatabaseCommand
        {
            private readonly DeleteObjectFactory factory;
            private readonly Dictionary<IObjectType, SqlCommand> commandByIObjectType;

            internal DeleteObject(DeleteObjectFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIObjectType = new Dictionary<IObjectType, SqlCommand>();
            }

            internal void Execute(Strategy strategy)
            {
                var objectType = strategy.ObjectType;

                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(objectType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(objectType));
                    var sqlParameter = command.CreateParameter();
                    sqlParameter.ParameterName = Mapping.ParamNameForObject;
                    sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                    sqlParameter.Value = strategy.ObjectId.Value ?? DBNull.Value;

                    command.Parameters.Add(sqlParameter);

                    this.commandByIObjectType[objectType] = command;
                }
                else
                {
                    command.Parameters[Mapping.ParamNameForObject].Value = strategy.ObjectId.Value ?? DBNull.Value;
                }

                command.ExecuteNonQuery();
            }
        }
    }
}