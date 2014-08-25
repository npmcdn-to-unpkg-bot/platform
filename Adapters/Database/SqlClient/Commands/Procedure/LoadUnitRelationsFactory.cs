// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadUnitRelationsFactory.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.SqlClient.Commands.Text
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;
    using Allors.Meta;

    using Schema = Allors.Adapters.Database.SqlClient.Schema;

    public class LoadUnitRelationsFactory : ILoadUnitRelationsFactory
    {
        public readonly SqlClient.ManagementSession ManagementSession;
        private readonly Dictionary<MetaObject, Dictionary<MetaRole, string>> sqlByRoleTypeByObjectType;

        public LoadUnitRelationsFactory(SqlClient.ManagementSession session)
        {
            this.ManagementSession = session;
            this.sqlByRoleTypeByObjectType = new Dictionary<MetaObject, Dictionary<MetaRole, string>>();
        }

        public ILoadUnitRelations Create()
        {
            return new LoadUnitRelations(this);
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
                var sql = Schema.AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName;
                sqlByRoleType[roleType] = sql;
            }

            return sqlByRoleType[roleType];
        }

        private class LoadUnitRelations : Commands.Command, ILoadUnitRelations
        {
            private readonly LoadUnitRelationsFactory factory;
            private readonly Dictionary<MetaObject, Dictionary<MetaRole, SqlCommand>> commandByRoleTypeByObjectType;

            public LoadUnitRelations(LoadUnitRelationsFactory factory)
            {
                this.factory = factory;
                this.commandByRoleTypeByObjectType = new Dictionary<MetaObject, Dictionary<MetaRole, SqlCommand>>();
            }

            public void Execute(IList<UnitRelation> relations, MetaObject exclusiveRootClass, MetaRole roleType)
            {
                var database = this.factory.ManagementSession.SqlClientDatabase;
                var schema = database.SqlClientSchema;

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
                    command = this.factory.ManagementSession.CreateSqlCommand(this.factory.GetSql(exclusiveRootClass, roleType));
                    command.CommandType = CommandType.StoredProcedure;
                    this.AddInTable(command, tableParam, database.CreateRelationTable(roleType, relations));
                }
                else
                {
                    this.SetInTable(command, tableParam, database.CreateRelationTable(roleType, relations));
                }

                command.ExecuteNonQuery();
            }
        }
    }
}