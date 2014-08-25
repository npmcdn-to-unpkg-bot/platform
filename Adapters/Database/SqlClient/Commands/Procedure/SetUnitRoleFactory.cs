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
        private readonly Dictionary<MetaObject, Dictionary<MetaRole, string>> sqlByRoleTypeByObjectType;

        public SetUnitRoleFactory(Database database)
        {
            this.Database = database;
            this.sqlByRoleTypeByObjectType = new Dictionary<MetaObject, Dictionary<MetaRole, string>>();
        }

        public ISetUnitRole Create(Sql.DatabaseSession session)
        {
            return new SetUnitRole(this, session);
        }

        public string GetSql(MetaObject objectType, MetaRole roleType)
        {
            Dictionary<MetaRole, string> sqlByRoleType;
            if (!this.sqlByRoleTypeByObjectType.TryGetValue(objectType, out sqlByRoleType))
            {
                sqlByRoleType = new Dictionary<MetaRole, string>();
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
            private readonly Dictionary<MetaObject, Dictionary<MetaRole, SqlCommand>> commandByRoleTypeByObjectType;

            public SetUnitRole(SetUnitRoleFactory factory, Sql.DatabaseSession session)
                : base((DatabaseSession)session)
            {
                this.factory = factory;
                this.commandByRoleTypeByObjectType = new Dictionary<MetaObject, Dictionary<MetaRole, SqlCommand>>();
            }

            public void Execute(IList<UnitRelation> relation, MetaObject exclusiveRootClass, MetaRole roleType)
            {
                var schema = this.Database.SqlClientSchema;

                Dictionary<MetaRole, SqlCommand> commandByRoleType;
                if (!this.commandByRoleTypeByObjectType.TryGetValue(exclusiveRootClass, out commandByRoleType))
                {
                    commandByRoleType = new Dictionary<MetaRole, SqlCommand>();
                    this.commandByRoleTypeByObjectType.Add(exclusiveRootClass, commandByRoleType);
                }

                SchemaTableParameter tableParam;

                var unitTypeTag = (MetaUnitTags)roleType.ObjectType.UnitTag;
                switch (unitTypeTag)
                {
                    case MetaUnitTags.AllorsString:
                        tableParam = schema.StringRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsInteger:
                        tableParam = schema.IntegerRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsLong:
                        tableParam = schema.LongRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsDouble:
                        tableParam = schema.DoubleRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsBoolean:
                        tableParam = schema.BooleanRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsDateTime:
                        tableParam = schema.DateTimeRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsUnique:
                        tableParam = schema.UniqueRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsBinary:
                        tableParam = schema.BinaryRelationTableParam;
                        break;

                    case MetaUnitTags.AllorsDecimal:
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