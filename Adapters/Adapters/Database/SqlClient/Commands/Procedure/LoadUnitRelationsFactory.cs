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
        private readonly Dictionary<IObjectType, Dictionary<IRoleType, string>> sqlByRoleTypeByObjectType;

        public LoadUnitRelationsFactory(SqlClient.ManagementSession session)
        {
            this.ManagementSession = session;
            this.sqlByRoleTypeByObjectType = new Dictionary<IObjectType, Dictionary<IRoleType, string>>();
        }

        public ILoadUnitRelations Create()
        {
            return new LoadUnitRelations(this);
        }

        public string GetSql(IObjectType objectType, IRoleType roleType)
        {
            Dictionary<IRoleType, string> sqlByRoleType;
            if (!this.sqlByRoleTypeByObjectType.TryGetValue(objectType, out sqlByRoleType))
            {
                sqlByRoleType = new Dictionary<IRoleType, string>();
                this.sqlByRoleTypeByObjectType.Add(objectType, sqlByRoleType);
            }

            if (!sqlByRoleType.ContainsKey(roleType))
            {
                var sql = Schema.AllorsPrefix + "SR_" + objectType.SingularName + "_" + roleType.SingularPropertyName;
                sqlByRoleType[roleType] = sql;
            }

            return sqlByRoleType[roleType];
        }

        private class LoadUnitRelations : Commands.Command, ILoadUnitRelations
        {
            private readonly LoadUnitRelationsFactory factory;
            private readonly Dictionary<IObjectType, Dictionary<IRoleType, SqlCommand>> commandByRoleTypeByObjectType;

            public LoadUnitRelations(LoadUnitRelationsFactory factory)
            {
                this.factory = factory;
                this.commandByRoleTypeByObjectType = new Dictionary<IObjectType, Dictionary<IRoleType, SqlCommand>>();
            }

            public void Execute(IList<UnitRelation> relations, IObjectType exclusiveLeafClass, IRoleType roleType)
            {
                var database = this.factory.ManagementSession.SqlClientDatabase;
                var schema = database.SqlClientSchema;

                Dictionary<IRoleType, SqlCommand> commandByRoleType;
                if (!this.commandByRoleTypeByObjectType.TryGetValue(exclusiveLeafClass, out commandByRoleType))
                {
                    commandByRoleType = new Dictionary<IRoleType, SqlCommand>();
                    this.commandByRoleTypeByObjectType.Add(exclusiveLeafClass, commandByRoleType);
                }

                SchemaTableParameter tableParam;

                var unitType = (IUnit)roleType.ObjectType;
                var unitTypeTag = unitType.UnitTag;
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

                    case UnitTags.AllorsDate:
                        tableParam = schema.DateTimeRelationTableParam;
                        break;

                    case UnitTags.AllorsUnique:
                        tableParam = schema.UniqueRelationTableParam;
                        break;

                    case UnitTags.AllorsBinary:
                        tableParam = schema.BinaryRelationTableParam;
                        break;

                    case UnitTags.AllorsDecimal:
                        tableParam = schema.DecimalRelationTableParameterByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                        break;

                    default:
                        throw new ArgumentException("Unknown Unit IObjectType: " + unitTypeTag);
                }

                SqlCommand command;
                if (!commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.factory.ManagementSession.CreateSqlCommand(this.factory.GetSql(exclusiveLeafClass, roleType));
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