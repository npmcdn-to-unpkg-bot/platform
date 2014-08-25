// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetUnitRoleFactory.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.SqlClient.Commands.Procedure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;

    using Allors.Meta;

    using Database = Database;
    using DatabaseSession = DatabaseSession;
    using Schema = Schema;

    public class SetUnitRoleFactory : ISetUnitRoleFactory
    {
        public readonly Database Database;
        private readonly Dictionary<ObjectType, Dictionary<RoleType, string>> sqlByRoleTypeByObjectType;

        public SetUnitRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleTypeByObjectType = new Dictionary<ObjectType, Dictionary<RoleType, string>>();
        }

        public ISetUnitRole Create(Sql.DatabaseSession session)
        {
            return new SetUnitRole(this, session);
        }

        public string GetSql(ObjectType objectType, RoleType roleType)
        {
            Dictionary<RoleType, string> sqlByRoleType;
            if (!this.sqlByRoleTypeByObjectType.TryGetValue(objectType, out sqlByRoleType))
            {
                sqlByRoleType = new Dictionary<RoleType, string>();
                this.sqlByRoleTypeByObjectType.Add(objectType, sqlByRoleType);
            }

            if (!sqlByRoleType.ContainsKey(roleType))
            {
                var sql = Sql.Schema.AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName;
                sqlByRoleType[roleType] = sql;
            }

            return sqlByRoleType[roleType];
        }

        private class SetUnitRole : DatabaseCommand, ISetUnitRole
        {
            private readonly SetUnitRoleFactory factory;
            private readonly Dictionary<ObjectType, Dictionary<RoleType, SqlCommand>> commandByRoleTypeByObjectType;

            public SetUnitRole(SetUnitRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleTypeByObjectType = new Dictionary<ObjectType, Dictionary<RoleType, SqlCommand>>();
            }

            public void Execute(IList<UnitRelation> relation, ObjectType exclusiveRootClass, RoleType roleType)
            {
                var schema = this.Database.SqlClientSchema;

                Dictionary<RoleType, SqlCommand> commandByRoleType;
                if (!this.commandByRoleTypeByObjectType.TryGetValue(exclusiveRootClass, out commandByRoleType))
                {
                    commandByRoleType = new Dictionary<RoleType, SqlCommand>();
                    this.commandByRoleTypeByObjectType.Add(exclusiveRootClass, commandByRoleType);
                }

                SchemaTableParameter tableParam;

                var unitTypeTag = (UnitTags)roleType.ObjectType.UnitTag;
                switch (unitTypeTag)
                {
                    case UnitTags.AllorsString:
                        tableParam = schema.StringRelationTableParam;
                        break;

                    case UnitTags.AllorsInteger:
                        tableParam = schema.IntegerRelationTableParam;
                        break;

                    case UnitTags.AllorsLong:
                        tableParam = schema.LongRelationTableParam;
                        break;

                    case UnitTags.AllorsDouble:
                        tableParam = schema.DoubleRelationTableParam;
                        break;

                    case UnitTags.AllorsBoolean:
                        tableParam = schema.BooleanRelationTableParam;
                        break;

                    case UnitTags.AllorsDateTime:
                        tableParam = schema.DateTimeRelationTableParam;
                        break;

                    case UnitTags.AllorsUnique:
                        tableParam = schema.UniqueRelationTableParam;
                        break;

                    case UnitTags.AllorsBinary:
                        tableParam = schema.BinaryRelationTableParam;
                        break;

                    case UnitTags.AllorsDecimal:
                        tableParam = schema.DecimalRelationTableParameterByScaleByPrecision[roleType.Precision][roleType.Scale];
                        break;

                    default:
                        throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
                }

                SqlCommand command;
                if (!commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.Session.CreateSqlCommand(this.factory.GetSql(exclusiveRootClass, roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, tableParam, this.Database.CreateRelationTable(roleType, relation));
                }
                else
                {
                    this.SetInTable(command, tableParam, this.Database.CreateRelationTable(roleType, relation));
                }

                command.ExecuteNonQuery();
            }
        }
    }
}