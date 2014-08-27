// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadUnitRelationsFactory.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Npgsql.Commands.Text
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;
    using Allors.Meta;

    using global::Npgsql;

    public class LoadUnitRelationsFactory : ILoadUnitRelationsFactory
    {
        public readonly Npgsql.ManagementSession ManagementSession;
        private readonly Dictionary<ObjectType, Dictionary<RoleType, string>> sqlByRoleTypeByObjectType;

        public LoadUnitRelationsFactory(Npgsql.ManagementSession session)
        {
            this.ManagementSession = session;
            this.sqlByRoleTypeByObjectType = new Dictionary<ObjectType, Dictionary<RoleType, string>>();
        }

        public ILoadUnitRelations Create()
        {
            return new LoadUnitRelations(this);
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
                var sql = Schema.AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName;
                sqlByRoleType[roleType] = sql;
            }

            return sqlByRoleType[roleType];
        }

        private class LoadUnitRelations : Commands.Command, ILoadUnitRelations
        {
            private readonly LoadUnitRelationsFactory factory;
            private readonly Dictionary<ObjectType, Dictionary<RoleType, NpgsqlCommand>> commandByRoleTypeByObjectType;

            public LoadUnitRelations(LoadUnitRelationsFactory factory)
            {
                this.factory = factory;
                this.commandByRoleTypeByObjectType = new Dictionary<ObjectType, Dictionary<RoleType, NpgsqlCommand>>();
            }

            public void Execute(IList<UnitRelation> relations, ObjectType exclusiveRootClass, RoleType roleType)
            {
                var database = this.factory.ManagementSession.NpgsqlDatabase;
                var schema = database.NpgsqlSchema;

                Dictionary<RoleType, NpgsqlCommand> commandByRoleType;
                if (!this.commandByRoleTypeByObjectType.TryGetValue(exclusiveRootClass, out commandByRoleType))
                {
                    commandByRoleType = new Dictionary<RoleType, NpgsqlCommand>();
                    this.commandByRoleTypeByObjectType.Add(exclusiveRootClass, commandByRoleType);
                }

                SchemaArrayParameter arrayParam;

                var unitType = (UnitType)roleType.ObjectType;
                var unitTypeTag = (UnitTags)unitType.UnitTag;
                switch (unitTypeTag)
                {
                    case UnitTags.AllorsString:
                        arrayParam = schema.StringRelationArrayParam;
                        break;

                    case UnitTags.AllorsInteger:
                        arrayParam = schema.IntegerRelationArrayParam;
                        break;

                    case UnitTags.AllorsLong:
                        arrayParam = schema.LongRelationArrayParam;
                        break;

                    case UnitTags.AllorsDouble:
                        arrayParam = schema.DoubleRelationArrayParam;
                        break;

                    case UnitTags.AllorsBoolean:
                        arrayParam = schema.BooleanRelationArrayParam;
                        break;

                    case UnitTags.AllorsDateTime:
                        arrayParam = schema.DateTimeRelationArrayParam;
                        break;

                    case UnitTags.AllorsUnique:
                        arrayParam = schema.UniqueRelationArrayParam;
                        break;

                    case UnitTags.AllorsBinary:
                        arrayParam = schema.BinaryRelationArrayParam;
                        break;

                    case UnitTags.AllorsDecimal:
                        arrayParam = schema.DecimalRelationTableParameterByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                        break;

                    default:
                        throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
                }

                NpgsqlCommand command;
                if (!commandByRoleType.TryGetValue(roleType, out command))
                {
                    command = this.factory.ManagementSession.CreateNpgsqlCommand(this.factory.GetSql(exclusiveRootClass, roleType));
                    command.CommandType = CommandType.StoredProcedure;

                    this.AddInTable(command, database.NpgsqlSchema.ObjectArrayParam, database.CreateAssociationTable(relations));
                    this.AddInTable(command, arrayParam, database.CreateRoleTable(relations));
                }
                else
                {
                    this.SetInTable(command, database.NpgsqlSchema.ObjectArrayParam, database.CreateAssociationTable(relations));
                    this.SetInTable(command, arrayParam, database.CreateRoleTable(relations));
                }

                command.ExecuteNonQuery();
            }
        }
    }
}