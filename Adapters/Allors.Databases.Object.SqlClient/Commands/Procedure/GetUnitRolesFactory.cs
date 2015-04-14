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

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;

    using Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;

    internal class GetUnitRolesFactory
    {
        internal readonly Database Database;
        private readonly Dictionary<IObjectType, string> sqlByIObjectType;

        internal GetUnitRolesFactory(Database database)
        {
            this.Database = database;
            this.sqlByIObjectType = new Dictionary<IObjectType, string>();
        }

        internal GetUnitRoles Create(DatabaseSession session)
        {
            return new GetUnitRoles(this, session);
        }

        internal string GetSql(IObjectType objectType)
        {
            if (!this.sqlByIObjectType.ContainsKey(objectType))
            {
                var sql = "GU_" + objectType.Name;
                this.sqlByIObjectType[objectType] = sql;
            }

            return this.sqlByIObjectType[objectType];
        }

        internal class GetUnitRoles : DatabaseCommand
        {
            private readonly GetUnitRolesFactory factory;
            private readonly Dictionary<IObjectType, SqlCommand> commandByIObjectType;

            internal GetUnitRoles(GetUnitRolesFactory factory, DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByIObjectType = new Dictionary<IObjectType, SqlCommand>();
            }

            internal void Execute(Roles roles)
            {
                var reference = roles.Reference;
                var objectType = reference.ObjectType;

                SqlCommand command;
                if (!this.commandByIObjectType.TryGetValue(objectType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(objectType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInObject(command, this.Database.Mapping.ObjectId.Param, reference.ObjectId.Value);

                    this.commandByIObjectType[objectType] = command;
                }
                else
                {
                    this.SetInObject(command, this.Database.Mapping.ObjectId.Param, reference.ObjectId.Value);
                }

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var sortedUnitRoles = this.Database.GetSortedUnitRolesByIObjectType(reference.ObjectType);

                        for (var i = 0; i < sortedUnitRoles.Length; i++)
                        {
                            var roleType = sortedUnitRoles[i];

                            object unit = null;
                            if (!reader.IsDBNull(i))
                            {
                                var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
                                switch (unitTypeTag)
                                {
                                    case UnitTags.AllorsString:
                                        unit = reader.GetString(i);
                                        break;
                                    case UnitTags.AllorsInteger:
                                        unit = reader.GetInt32(i);
                                        break;
                                    case UnitTags.AllorsFloat:
                                        unit = reader.GetDouble(i);
                                        break;
                                    case UnitTags.AllorsDecimal:
                                        unit = reader.GetDecimal(i);
                                        break;
                                    case UnitTags.AllorsDateTime:
                                        var dateTime = reader.GetDateTime(i);
                                        if (dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
                                        {
                                            unit = dateTime;
                                        }
                                        else
                                        {
                                            unit = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
                                        }

                                        break;
                                    case UnitTags.AllorsBoolean:
                                        unit = reader.GetBoolean(i);
                                        break;
                                    case UnitTags.AllorsUnique:
                                        unit = reader.GetGuid(i);
                                        break;
                                    case UnitTags.AllorsBinary:
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