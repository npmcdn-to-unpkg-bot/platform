// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetUnitRolesFactory.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters.Database.SqlClient.Commands.Procedure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.R1.Adapters.Database.Sql;
    using Allors.R1.Adapters.Database.Sql.Commands;

    using Meta;

    using Database = Database;
    using DatabaseSession = Allors.R1.Adapters.Database.SqlClient.DatabaseSession;

    public class GetUnitRolesFactory : IGetUnitRolesFactory
    {
        public readonly Database Database;
        private readonly Dictionary<ObjectType, string> sqlByObjectType;

        public GetUnitRolesFactory(Database database)
        {
            this.Database = database;
            this.sqlByObjectType = new Dictionary<ObjectType, string>();
        }

        public IGetUnitRoles Create(Sql.DatabaseSession session)
        {
            return new GetUnitRoles(this, session);
        }

        public string GetSql(ObjectType objectType)
        {
            if (!this.sqlByObjectType.ContainsKey(objectType))
            {
                var sql = Sql.Schema.AllorsPrefix + "GU_" + objectType.Name;
                this.sqlByObjectType[objectType] = sql;
            }

            return this.sqlByObjectType[objectType];
        }

        public class GetUnitRoles : DatabaseCommand, IGetUnitRoles
        {
            private readonly GetUnitRolesFactory factory;
            private readonly Dictionary<ObjectType, SqlCommand> commandByObjectType;

            public GetUnitRoles(GetUnitRolesFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByObjectType = new Dictionary<ObjectType, SqlCommand>();
            }

            public void Execute(Roles roles)
            {
                var reference = roles.Reference;
                var objectType = reference.ObjectType;

                SqlCommand command;
                if (!this.commandByObjectType.TryGetValue(objectType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(objectType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, this.Database.Schema.ObjectId.Param, reference.ObjectId.Value);

                    this.commandByObjectType[objectType] = command;
                }
                else
                {
                    this.SetInObject(command, this.Database.Schema.ObjectId.Param, reference.ObjectId.Value);
                }

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var sortedUnitRoles = this.Database.GetSortedUnitRolesByObjectType(reference.ObjectType);

                        for (var i = 0; i < sortedUnitRoles.Length; i++)
                        {
                            var roleType = sortedUnitRoles[i];

                            object unit = null;
                            if (!reader.IsDBNull(i))
                            {
                                var unitTypeTag = (UnitTypeTags)roleType.ObjectType.UnitTag;
                                switch (unitTypeTag)
                                {
                                    case UnitTypeTags.AllorsString:
                                        unit = reader.GetString(i);
                                        break;
                                    case UnitTypeTags.AllorsInteger:
                                        unit = reader.GetInt32(i);
                                        break;
                                    case UnitTypeTags.AllorsLong:
                                        unit = reader.GetInt64(i);
                                        break;
                                    case UnitTypeTags.AllorsDecimal:
                                        unit = reader.GetDecimal(i);
                                        break;
                                    case UnitTypeTags.AllorsDouble:
                                        unit = reader.GetDouble(i);
                                        break;
                                    case UnitTypeTags.AllorsBoolean:
                                        unit = reader.GetBoolean(i);
                                        break;
                                    case UnitTypeTags.AllorsDateTime:
                                        var dateTime = reader.GetDateTime(i);
                                        unit = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
                                        break;
                                    case UnitTypeTags.AllorsUnique:
                                        unit = reader.GetGuid(i);
                                        break;
                                    case UnitTypeTags.AllorsBinary:
                                        var byteArray = (byte[])reader.GetValue(i);
                                        unit = byteArray;
                                        break;
                                    default:
                                        throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.Name);
                                }
                            }

                            roles.CachedObject.SetValue(roleType, unit);
                        }
                    }
                }
            }
        }
    }
}