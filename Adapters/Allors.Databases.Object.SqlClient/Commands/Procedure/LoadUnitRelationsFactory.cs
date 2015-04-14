﻿// --------------------------------------------------------------------------------------------------------------------
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

namespace Allors.Databases.Object.SqlClient.Commands.Text
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    internal class LoadUnitRelationsFactory
    {
        internal readonly SqlClient.ManagementSession ManagementSession;
        private readonly Dictionary<IObjectType, Dictionary<IRoleType, string>> sqlByIRoleTypeByIObjectType;

        internal LoadUnitRelationsFactory(SqlClient.ManagementSession session)
        {
            this.ManagementSession = session;
            this.sqlByIRoleTypeByIObjectType = new Dictionary<IObjectType, Dictionary<IRoleType, string>>();
        }

        internal LoadUnitRelations Create()
        {
            return new LoadUnitRelations(this);
        }

        internal string GetSql(IObjectType objectType, IRoleType roleType)
        {
            Dictionary<IRoleType, string> sqlByIRoleType;
            if (!this.sqlByIRoleTypeByIObjectType.TryGetValue(objectType, out sqlByIRoleType))
            {
                sqlByIRoleType = new Dictionary<IRoleType, string>();
                this.sqlByIRoleTypeByIObjectType.Add(objectType, sqlByIRoleType);
            }

            if (!sqlByIRoleType.ContainsKey(roleType))
            {
                var sql = this.ManagementSession.Database.SchemaName + "." + "SR_" + objectType.Name + "_" + roleType.SingularFullName;
                sqlByIRoleType[roleType] = sql;
            }

            return sqlByIRoleType[roleType];
        }

        internal class LoadUnitRelations : Commands.Command
        {
            private readonly LoadUnitRelationsFactory factory;
            private readonly Dictionary<IObjectType, Dictionary<IRoleType, SqlCommand>> commandByIRoleTypeByIObjectType;

            internal LoadUnitRelations(LoadUnitRelationsFactory factory)
            {
                this.factory = factory;
                this.commandByIRoleTypeByIObjectType = new Dictionary<IObjectType, Dictionary<IRoleType, SqlCommand>>();
            }

            internal void Execute(IList<UnitRelation> relations, IObjectType exclusiveRootClass, IRoleType roleType)
            {
                var database = this.factory.ManagementSession.SqlClientDatabase;
                var schema = database.SqlClientMapping;

                Dictionary<IRoleType, SqlCommand> commandByIRoleType;
                if (!this.commandByIRoleTypeByIObjectType.TryGetValue(exclusiveRootClass, out commandByIRoleType))
                {
                    commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
                    this.commandByIRoleTypeByIObjectType.Add(exclusiveRootClass, commandByIRoleType);
                }

                MappingTableParameter tableParam;

                var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
                switch (unitTypeTag)
                {
                    case UnitTags.AllorsString:
                        tableParam = schema.StringRelationTableParam;
                        break;

                    case UnitTags.AllorsInteger:
                        tableParam = schema.IntegerRelationTableParam;
                        break;

                    case UnitTags.AllorsFloat:
                        tableParam = schema.FloatRelationTableParam;
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
                        tableParam = schema.DecimalRelationTableParameterByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                        break;

                    default:
                        throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
                }

                SqlCommand command;
                if (!commandByIRoleType.TryGetValue(roleType, out command))
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