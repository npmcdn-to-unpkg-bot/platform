//------------------------------------------------------------------------------------------------- 
// <copyright file="Mapping.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
//-------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Allors.Meta;

    internal abstract class Mapping : IEnumerable<MappingTable>
    {
        /// <summary>
        /// This prefix will be used for
        /// <ul>
        /// <li>System tables (e.g. OC)</li>
        /// <li>Indexes</li>
        /// <li>Parameters</li>
        /// </ul>
        /// in order to avoid naming conflictts with existing tables
        /// </summary>
        internal const string AllorsPrefix = "_";

        internal readonly string ParamInvocationFormat;
        internal readonly string ParamFormat;

        private readonly string prefix;
        private readonly string postfix;

        private Dictionary<IRelationType, MappingColumn> columnsByRelationType;
        private Dictionary<IRelationType, MappingTable> tablesByRelationType;
        private Dictionary<IObjectType, MappingTable> tableByObjectType;
        private Dictionary<string, MappingTable> tablesByName;

        private MappingColumn objectId;
        private MappingColumn associationId;
        private MappingColumn roleId;
        private MappingColumn typeId;
        private MappingColumn cacheId;

        private MappingTable objects;
        private MappingColumn objectsObjectId;
        private MappingColumn objectsTypeId;
        private MappingColumn objectsCacheId;

        private MappingParameter countParam;
        private MappingParameter matchRoleParam;

        private DbType cacheDbType;
        private DbType typeDbType;

        /// <summary>
        /// Gets the parameter to pass a count to.
        /// <example>
        /// Is used in CreateObjects to denote the amount of objects to create.
        /// </example>
        /// </summary>
        internal MappingParameter CountParam
        {
            get { return this.countParam; }
        }

        internal MappingParameter MatchRoleParam
        {
            get { return this.matchRoleParam; }
        }

        internal MappingColumn TypeId
        {
            get { return this.typeId; }
        }

        internal MappingColumn CacheId
        {
            get { return this.cacheId; }
        }

        internal MappingColumn AssociationId
        {
            get { return this.associationId; }
        }

        internal MappingColumn RoleId
        {
            get { return this.roleId; }
        }

        internal MappingColumn ObjectId
        {
            get { return this.objectId; }
        }

        internal MappingTable Objects
        {
            get { return this.objects; }
        }

        internal MappingColumn ObjectsObjectId
        {
            get { return this.objectsObjectId; }
        }

        internal MappingColumn ObjectsTypeId
        {
            get { return this.objectsTypeId; }
        }

        internal MappingColumn ObjectsCacheId
        {
            get { return this.objectsCacheId; }
        }

        public abstract bool IsObjectIdInteger { get; }

        public abstract bool IsObjectIdLong { get; }

        /// <summary>
        /// Gets the type used to store object (ids) .
        /// </summary>
        protected abstract DbType ObjectDbType { get; }

        protected Database Database
        {
            get { return this.database; }
        }

        protected Dictionary<IRelationType, MappingColumn> ColumnsByRelationType
        {
            get { return this.columnsByRelationType; }
        }

        protected Dictionary<string, MappingTable> TablesByName
        {
            get { return this.tablesByName; }
        }

        protected Dictionary<IRelationType, MappingTable> TablesByRelationType
        {
            get { return this.tablesByRelationType; }
        }

        protected Dictionary<IObjectType, MappingTable> TableByObjectType
        {
            get { return this.tableByObjectType; }
        }

        private DbType TypeDbType
        {
            get { return this.typeDbType; }
        }

        private DbType CacheDbType
        {
            get { return this.cacheDbType; }
        }

        internal MappingTable this[string tableName]
        {
            get { return this.tablesByName[tableName.ToLowerInvariant()]; }
        }

        internal static void AddError(MappingValidationErrors mappingValidationErrors, MappingTable table, MappingValidationErrorKind kind)
        {
            mappingValidationErrors.AddTableError(table.ObjectType, table.RelationType, null, table.ToString(), null, kind, kind + ": " + table);
        }

        internal static void AddError(MappingValidationErrors mappingValidationErrors, MappingTable table, MappingColumn column, MappingValidationErrorKind kind)
        {
            var roleType = column.RelationType == null ? null : column.RelationType.RoleType;
            mappingValidationErrors.AddTableError(null, null, roleType, table.ToString(), column.ToString(), kind, kind + ": " + table + "." + column);
        }

        internal static void AddError(MappingValidationErrors mappingValidationErrors, MappingProcedure mappingProcedure, MappingValidationErrorKind kind, string message)
        {
            mappingValidationErrors.AddProcedureError(mappingProcedure, kind, message);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<MappingTable>)this).GetEnumerator();
        }

        IEnumerator<MappingTable> IEnumerable<MappingTable>.GetEnumerator()
        {
            return this.tablesByName.Values.GetEnumerator();
        }

        internal MappingColumn Column(IRelationType relationType)
        {
            return this.columnsByRelationType[relationType];
        }

        internal MappingColumn Column(IAssociationType association)
        {
            return this.columnsByRelationType[association.RelationType];
        }

        internal MappingColumn Column(IRoleType role)
        {
            return this.columnsByRelationType[role.RelationType];
        }

        internal MappingTable Table(IObjectType type)
        {
            return this.tableByObjectType[type];
        }

        internal MappingTable Table(IRelationType relationType)
        {
            return this.tablesByRelationType[relationType];
        }

        internal MappingTable Table(IAssociationType association)
        {
            return this.tablesByRelationType[association.RelationType];
        }

        internal MappingTable Table(IRoleType role)
        {
            return this.tablesByRelationType[role.RelationType];
        }

        internal string EscapeIfReserved(string name)
        {
            if (ReservedWords.Names.Contains(name.ToLowerInvariant()))
            {
                return this.prefix + name + this.postfix;
            }

            return name;
        }

        protected virtual DbType GetDbType(IRoleType role)
        {
            var unitTypeTag = ((IUnit)role.ObjectType).UnitTag;
            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    return DbType.String;
                case UnitTags.AllorsInteger:
                    return DbType.Int32;
                case UnitTags.AllorsFloat:
                    return DbType.Double;
                case UnitTags.AllorsDecimal:
                    return DbType.Decimal;
                case UnitTags.AllorsBoolean:
                    return DbType.Boolean;
                case UnitTags.AllorsDateTime:
                    return DbType.DateTime2;
                case UnitTags.AllorsUnique:
                    return DbType.Guid;
                case UnitTags.AllorsBinary:
                    return DbType.Binary;
                default:
                    throw new ArgumentException("Unkown unit type " + role.ObjectType);
            }
        }

        protected bool Contains(string tableName)
        {
            return this.tablesByName.ContainsKey(tableName.ToLowerInvariant());
        }

        private void CreateTablesFromMeta()
        {
            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;

                if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveLeafClasses))
                {
                    var column = new MappingColumn(this, "R", this.ObjectDbType, false, true, relationType.IsIndexed ? MappingIndexType.Combined : MappingIndexType.None, relationType);
                    this.ColumnsByRelationType.Add(relationType, column);
                }
                else
                {
                    if (roleType.ObjectType.IsUnit)
                    {
                        var size = roleType.Size;
                        var precision = roleType.Precision;
                        var scale = roleType.Scale;

                        var index = relationType.IsIndexed ? MappingIndexType.Single : MappingIndexType.None;
                        var unit = (IUnit)roleType.ObjectType;
                        if (unit.IsBinary || unit.IsString)
                        {
                            if (roleType.Size == -1 || roleType.Size > 8000)
                            {
                                index = MappingIndexType.None;
                            }
                        }

                        var column = new MappingColumn(this, roleType.SingularPropertyName, this.GetDbType(roleType), false, false, index, relationType, size, precision, scale);
                        this.ColumnsByRelationType.Add(relationType, column);
                    }
                    else if (relationType.ExistExclusiveLeafClasses)
                    {
                        if (roleType.IsOne)
                        {
                            var column = new MappingColumn(this, roleType.SingularPropertyName, this.ObjectDbType, false, false, relationType.IsIndexed ? MappingIndexType.Combined : MappingIndexType.None, relationType);
                            this.ColumnsByRelationType.Add(relationType, column);
                        }
                        else
                        {
                            var column = new MappingColumn(this, associationType.SingularPropertyName, this.ObjectDbType, false, false, relationType.IsIndexed ? MappingIndexType.Combined : MappingIndexType.None, relationType);
                            this.ColumnsByRelationType.Add(relationType, column);
                        }
                    }
                }
            }

            foreach (IClass objectType in this.Database.MetaPopulation.Classes)
            {
                var schemaTable = new MappingTable(this, objectType.SingularName, MapingTableKind.Object, objectType);
                this.TablesByName.Add(schemaTable.Name, schemaTable);
                this.TableByObjectType.Add(objectType, schemaTable);

                schemaTable.AddColumn(this.ObjectId);
                schemaTable.AddColumn(this.TypeId);

                foreach (var associationType in objectType.AssociationTypes)
                {
                    var relationType = associationType.RelationType;
                    var roleType = relationType.RoleType;
                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses && roleType.IsMany)
                    {
                        schemaTable.AddColumn(this.Column(relationType));
                    }
                }

                foreach (var roleType in objectType.RoleTypes)
                {
                    var relationType = roleType.RelationType;
                    var associationType = relationType.AssociationType;
                    if (roleType.ObjectType.IsUnit)
                    {
                        schemaTable.AddColumn(this.Column(relationType));
                    }
                    else
                    {
                        if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses && !roleType.IsMany)
                        {
                            schemaTable.AddColumn(this.Column(relationType));
                        }
                    }
                }
            }

            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;

                if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveLeafClasses))
                {
                    var schemaTable = new MappingTable(this, relationType.RoleType.SingularFullName, MapingTableKind.Relation, relationType);
                    this.TablesByName.Add(schemaTable.Name, schemaTable);
                    this.TablesByRelationType.Add(relationType, schemaTable);

                    schemaTable.AddColumn(this.AssociationId);
                    schemaTable.AddColumn(this.Column(relationType));
                }
            }
        }































        internal readonly string ObjectTable = AllorsPrefix + "_O";
        internal readonly string ObjectTableObject = "_o";
        internal readonly MappingTableParameter ObjectTableParam;

        internal readonly string RelationTableAssociation = "_a";
        internal readonly string RelationTableRole = "_r";

        internal readonly string CompositeRelationTable = AllorsPrefix + "_CompositeR";
        internal readonly MappingTableParameter CompositeRelationTableParam;

        internal readonly string StringRelationTable = AllorsPrefix + "_StringR";
        internal readonly MappingTableParameter StringRelationTableParam;

        internal readonly string IntegerRelationTable = AllorsPrefix + "_IntegerR";
        internal readonly MappingTableParameter IntegerRelationTableParam;
        
        internal readonly string FloatRelationTable = AllorsPrefix + "_FloatR";
        internal readonly MappingTableParameter FloatRelationTableParam;
        
        internal readonly string BooleanRelationTable = AllorsPrefix + "_BooleanR";
        internal readonly MappingTableParameter BooleanRelationTableParam;

        internal readonly string DateTimeRelationTable = AllorsPrefix + "_DateTimeR";
        internal readonly MappingTableParameter DateTimeRelationTableParam;

        internal readonly string UniqueRelationTable = AllorsPrefix + "_UniqueR";
        internal readonly MappingTableParameter UniqueRelationTableParam;

        internal readonly string BinaryRelationTable = AllorsPrefix + "_BinaryR";
        internal readonly MappingTableParameter BinaryRelationTableParam;

        internal readonly Dictionary<int, Dictionary<int, string>> DecimalRelationTableByScaleByPrecision = new Dictionary<int, Dictionary<int, string>>();
        internal readonly Dictionary<int, Dictionary<int, MappingTableParameter>> DecimalRelationTableParameterByScaleByPrecision = new Dictionary<int, Dictionary<int, MappingTableParameter>>(); 

        private readonly Database database;

        private MappingValidationErrors mappingValidationErrors;
        private Dictionary<string, MappingProcedure> procedureByName;

        internal Mapping(Database database)
        {
            this.database = database;
            this.ParamInvocationFormat = "@{0}";
            this.ParamFormat = "@{0}";
            this.prefix = "[";
            this.postfix = "]";
            
            this.database = database;
            this.ObjectTableParam = new MappingTableParameter(this, AllorsPrefix + "p_o", this.ObjectTable);
            this.CompositeRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.CompositeRelationTable);
            this.StringRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.StringRelationTable);
            this.IntegerRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.IntegerRelationTable);
            this.FloatRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.FloatRelationTable);
            this.BooleanRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.BooleanRelationTable);
            this.DateTimeRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.DateTimeRelationTable);
            this.UniqueRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.UniqueRelationTable);
            this.BinaryRelationTableParam = new MappingTableParameter(this, AllorsPrefix + "p_r", this.BinaryRelationTable);

            foreach (var relationType in database.MetaPopulation.RelationTypes)
            {
                var roleType = relationType.RoleType;
                if (roleType.ObjectType.IsUnit && ((IUnit)roleType.ObjectType).IsDecimal)
                {
                    var precision = roleType.Precision;
                    var scale = roleType.Scale;

                    var tableName = AllorsPrefix + "_DecimalR_" + precision + "_" + scale;

                    // table
                    Dictionary<int, string> decimalRelationTableByScale;
                    if (!this.DecimalRelationTableByScaleByPrecision.TryGetValue(precision.Value, out decimalRelationTableByScale))
                    {
                        decimalRelationTableByScale = new Dictionary<int, string>();
                        this.DecimalRelationTableByScaleByPrecision[precision.Value] = decimalRelationTableByScale;
                    }

                    if (!decimalRelationTableByScale.ContainsKey(scale.Value))
                    {
                        decimalRelationTableByScale[scale.Value] = tableName;
                    }

                    // param
                    Dictionary<int, MappingTableParameter> schemaTableParameterByScale;
                    if (!this.DecimalRelationTableParameterByScaleByPrecision.TryGetValue(precision.Value, out schemaTableParameterByScale))
                    {
                        schemaTableParameterByScale = new Dictionary<int, MappingTableParameter>();
                        this.DecimalRelationTableParameterByScaleByPrecision[precision.Value] = schemaTableParameterByScale;
                    }

                    if (!schemaTableParameterByScale.ContainsKey(scale.Value))
                    {
                        schemaTableParameterByScale[scale.Value] = new MappingTableParameter(this, AllorsPrefix + "p_r", tableName); 
                    }
                }
            }
        }

        internal MappingValidationErrors MappingValidationErrors
        {
            get
            {
                if (this.mappingValidationErrors == null)
                {
                    this.mappingValidationErrors = new MappingValidationErrors();

                    var session = this.database.CreateSqlClientManagementSession();
                    try
                    {

                        var tableNames = new HashSet<string>();
                        lock (this.Database)
                        {
                            using (
                                var command = session.CreateCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES"))
                            {
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        var tableName = reader.GetString(0).ToLowerInvariant();
                                        if (this.Contains(tableName))
                                        {
                                            tableNames.Add(tableName);
                                        }
                                    }
                                }
                            }
                        }

                        var dataRowViewByExistingColumnsByTable = new Dictionary<MappingTable, Dictionary<MappingColumn, MappingExistingColumn>>();
                        lock (this.Database)
                        {
                            using (var command = session.CreateCommand(
@"SELECT    table_name, 
            column_name, 
            data_type, 
            character_maximum_length,
            character_octet_length, 
            numeric_precision, 
            numeric_scale, 
            datetime_precision 
FROM information_schema.columns"))
                            {
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        var tableName = reader.GetString(0).ToLowerInvariant();
                                        if (this.Contains(tableName))
                                        {
                                            var table = this[tableName];
                                            var columnName = reader.GetString(1);

                                            if (table.Contains(columnName))
                                            {
                                                var column = table[columnName];

                                                var existingColumn = new MappingExistingColumn
                                                                         {
                                                                             DataType = reader.GetString(2), 
                                                                             CharacterMaximumLength = reader.IsDBNull(3) ? 0 : (int)reader[3],
                                                                             CharacterOctetLength = reader.IsDBNull(4) ? 0 : (int)reader[4],
                                                                             NumericPrecision = reader.IsDBNull(5) ? 0 : int.Parse(reader[5].ToString()),
                                                                             NumericScale = reader.IsDBNull(6) ? 0 : (int)reader[6],
                                                                             DateTimePrecision = reader.IsDBNull(7) ? 0 : int.Parse(reader[7].ToString())
                                                                         };

                                                if (!dataRowViewByExistingColumnsByTable.ContainsKey(table))
                                                {
                                                    dataRowViewByExistingColumnsByTable.Add(
                                                        table, new Dictionary<MappingColumn, MappingExistingColumn>());
                                                }

                                                dataRowViewByExistingColumnsByTable[table].Add(column, existingColumn);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (MappingTable table in this)
                        {
                            if (tableNames.Contains(table.Name))
                            {
                                if (dataRowViewByExistingColumnsByTable.ContainsKey(table))
                                {
                                    var dataRowViewByExistingColumns = dataRowViewByExistingColumnsByTable[table];
                                    foreach (MappingColumn column in table)
                                    {
                                        if (dataRowViewByExistingColumns.ContainsKey(column))
                                        {
                                            var existingColumn = dataRowViewByExistingColumns[column];
                                            if (column.RelationType != null)
                                            {
                                                var dataType = existingColumn.DataType.ToLower();

                                                if (column.RelationType.RoleType.ObjectType.IsComposite)
                                                {
                                                    if (!dataType.Equals(SqlDbType.ToString().ToLower()))
                                                    {
                                                        AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                    }
                                                }
                                                else
                                                {
                                                    var unitTypeTag = ((IUnit)column.RelationType.RoleType.ObjectType).UnitTag;
                                                    switch (unitTypeTag)
                                                    {
                                                        case UnitTags.AllorsString:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.NVarChar.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }
                                                            else
                                                            {
                                                                var size = existingColumn.CharacterMaximumLength;

                                                                if (column.RelationType.RoleType.Size > size
                                                                    && size != -1)
                                                                {
                                                                    AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                                }
                                                            }

                                                            break;

                                                        case UnitTags.AllorsInteger:
                                                            if (!dataType.Equals(SqlDbType.Int.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsFloat:
                                                            if (!dataType.Equals(SqlDbType.Float.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsDecimal:
                                                            if (!dataType.Equals(SqlDbType.Decimal.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }
                                                            else
                                                            {
                                                                var precision = existingColumn.NumericPrecision;
                                                                var scale = existingColumn.NumericScale;

                                                                var role = column.RelationType.RoleType;
                                                                if (role.Precision > precision)
                                                                {
                                                                    AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                                }

                                                                if (role.Scale > scale)
                                                                {
                                                                    AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                                }
                                                            }

                                                            break;

                                                        case UnitTags.AllorsDateTime:
                                                            if (!dataType.Equals(SqlDbType.DateTime2.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsBoolean:
                                                            if (!dataType.Equals(SqlDbType.Bit.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsUnique:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.UniqueIdentifier.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsBinary:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.VarBinary.ToString().ToLower()))
                                                            {
                                                                AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                            }
                                                            else
                                                            {
                                                                var size = existingColumn.CharacterOctetLength;

                                                                if (column.RelationType.RoleType.Size > size
                                                                    && size != -1)
                                                                {
                                                                    AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Incompatible);
                                                                }
                                                            }

                                                            break;

                                                        default:
                                                            throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AddError(this.mappingValidationErrors, table, column, MappingValidationErrorKind.Missing);
                                        }
                                    }
                                }
                                else
                                {
                                    AddError(this.mappingValidationErrors, table, MappingValidationErrorKind.Missing);
                                }
                            }
                            else
                            {
                                AddError(this.mappingValidationErrors, table, MappingValidationErrorKind.Missing);
                            }
                        }

                        var procedureDefinitionByName = new Dictionary<string, string>();
                        lock (this.Database)
                        {
                            using (
                                var command = session.CreateCommand("SELECT ROUTINE_NAME, ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES"))
                            {
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        var name = reader.GetString(0);
                                        var definition = reader.GetString(1);
                                        procedureDefinitionByName.Add(name.ToLowerInvariant(), definition);
                                    }
                                }
                            }
                        }

                        foreach (var procedure in this.Procedures)
                        {
                            string existingProcedureDefinition;
                            if (!procedureDefinitionByName.TryGetValue(procedure.Name.ToLowerInvariant(), out existingProcedureDefinition))
                            {
                                AddError(this.mappingValidationErrors, procedure, MappingValidationErrorKind.Missing, "Procedure " + procedure.Name + " is missing.");
                            }
                            else
                            {
                                if (!procedure.Definition.Equals(existingProcedureDefinition))
                                {
                                    AddError(this.mappingValidationErrors, procedure, MappingValidationErrorKind.Incompatible, "Procedure " + procedure.Name + " is incompatible.");
                                }
                            }
                        }
                    }
                    catch
                    {
                        this.mappingValidationErrors = null;
                        throw;
                    }
                    finally
                    {
                        session.Rollback();
                    }
                }

                return this.mappingValidationErrors;
            }
        }

        internal IEnumerable<MappingProcedure> Procedures
        {
            get
            {
                return this.procedureByName.Values;
            }
        }

        protected Database SqlClientRowDatabase
        {
            get
            {
                return this.database;
            }
        }

        protected abstract SqlDbType SqlDbType
        {
            get;
        }

        internal MappingParameter CreateParameter(string name, DbType dbType)
        {
            return new MappingParameter(this, name, dbType);
        }

        protected void OnConstructed()
        {
            this.countParam = this.CreateParameter("COUNT", DbType.Int32);
            this.matchRoleParam = this.CreateParameter("MR", DbType.Guid);

            this.typeDbType = DbType.Guid;
            this.cacheDbType = DbType.Int32;

            this.tablesByName = new Dictionary<string, MappingTable>();

            this.tableByObjectType = new Dictionary<IObjectType, MappingTable>();
            this.tablesByRelationType = new Dictionary<IRelationType, MappingTable>();
            this.columnsByRelationType = new Dictionary<IRelationType, MappingColumn>();

            this.objectId = new MappingColumn(this, "O", this.ObjectDbType, false, true, MappingIndexType.None);
            this.cacheId = new MappingColumn(this, "C", this.CacheDbType, false, false, MappingIndexType.None);
            this.associationId = new MappingColumn(this, "A", this.ObjectDbType, false, true, MappingIndexType.None);
            this.roleId = new MappingColumn(this, "R", this.ObjectDbType, false, true, MappingIndexType.None);
            this.typeId = new MappingColumn(this, "T", this.TypeDbType, false, false, MappingIndexType.None);

            // Objects
            this.objects = new MappingTable(this, AllorsPrefix + "O", MapingTableKind.System);
            this.objectsObjectId = new MappingColumn(this, this.ObjectId.Name, this.ObjectDbType, true, true, MappingIndexType.None);
            this.objectsCacheId = new MappingColumn(this, "C", this.CacheDbType, false, false, MappingIndexType.None);
            this.objectsTypeId = new MappingColumn(this, this.TypeId.Name, this.TypeDbType, false, false, MappingIndexType.None);

            this.Objects.AddColumn(this.ObjectsObjectId);
            this.Objects.AddColumn(this.ObjectsTypeId);
            this.Objects.AddColumn(this.ObjectsCacheId);
            this.tablesByName.Add(this.Objects.Name, this.Objects);

            this.CreateTablesFromMeta();

            this.procedureByName = new Dictionary<string, MappingProcedure>();

            // Get Cache Ids
            var procedure = new MappingProcedure { Name = AllorsPrefix + "GC" };
            procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    SELECT " + this.ObjectId + ", " + this.CacheId + @"
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN (SELECT " + this.ObjectTableObject + @" FROM " + this.ObjectTableParam + ")";

            this.procedureByName.Add(procedure.Name, procedure);

            // Update Cache Ids
            procedure = new MappingProcedure { Name = AllorsPrefix + "UC" };
            procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    UPDATE " + this.Objects + @"
    SET " + this.CacheId + " = " + this.CacheId + @" - 1
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN ( SELECT " + this.ObjectTableObject + " FROM " + this.ObjectTableParam + ");\n\n";

            this.procedureByName.Add(procedure.Name, procedure);

            // Reset Cache Ids
            procedure = new MappingProcedure { Name = AllorsPrefix + "RC" };
            procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    UPDATE " + this.Objects + @"
    SET " + this.CacheId + " = " + Reference.InitialCacheId + @"
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN ( SELECT " + this.ObjectTableObject + " FROM " + this.ObjectTableParam + ");\n\n";

            this.procedureByName.Add(procedure.Name, procedure);

            foreach (var @class in this.Database.MetaPopulation.Classes)
            {
                var sortedUnitIRoleTypes = this.Database.GetSortedUnitRolesByIObjectType(@class);

                if (sortedUnitIRoleTypes.Length > 0)
                {
                    // Get Unit Roles
                    procedure = new MappingProcedure { Name = AllorsPrefix + "GU_" + @class.Name };
                    procedure.Definition = @"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectId.Param + " AS " + this.database.GetSqlType(this.ObjectId) + @"
AS 
    SELECT ";
                    var first = true;
                    foreach (var role in sortedUnitIRoleTypes)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            procedure.Definition += ", ";
                        }

                        procedure.Definition += this.ColumnsByRelationType[role.RelationType];
                    }

                    procedure.Definition += @"
    FROM " + this.Table(@class.ExclusiveLeafClass) + @"
    WHERE " + this.ObjectId + "=" + this.ObjectId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);
                }
            }

            foreach (var dictionaryEntry in this.TableByObjectType)
            {
                var objectType = dictionaryEntry.Key;
                var table = dictionaryEntry.Value;

                // Load Objects
                procedure = new MappingProcedure { Name = AllorsPrefix + "L_" + objectType.Name };
                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.TypeId.Param + @" " + this.database.GetSqlType(this.TypeId) + @",
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS
    INSERT INTO " + table + " (" + this.TypeId + ", " + this.ObjectId + @")
    SELECT " + this.TypeId.Param + @", " + this.ObjectTableObject + @"
    FROM " + this.ObjectTableParam + "\n";

                this.procedureByName.Add(procedure.Name, procedure);
                
                // CreateObject
                procedure = new MappingProcedure { Name = AllorsPrefix + "CO_" + objectType.Name };
                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
" + this.TypeId.Param + @" " + this.database.GetSqlType(this.TypeId) + @"
AS 
DECLARE  " + this.ObjectId.Param + " AS " + this.database.GetSqlType(this.ObjectId) + @"

INSERT INTO " + this.Objects + " (" + this.TypeId + ", " + this.CacheId + @")
VALUES (" + this.TypeId.Param + ", " + Reference.InitialCacheId + @");

SELECT " + this.ObjectId.Param + @" = SCOPE_IDENTITY();

INSERT INTO " + table + " (" + this.ObjectId + "," + this.TypeId + @")
VALUES (" + this.ObjectId.Param + "," + this.TypeId.Param + @");

SELECT " + this.ObjectId.Param + @";";

                this.procedureByName.Add(procedure.Name, procedure);

                // CreateObjects
                procedure = new MappingProcedure { Name = AllorsPrefix + "COS_" + objectType.Name };
                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
" + this.TypeId.Param + @" " + this.database.GetSqlType(this.TypeId) + @",
" + this.CountParam + @" " + this.database.GetSqlType(this.CountParam.DbType) + @"
AS 
BEGIN
DECLARE @IDS TABLE (id INT);
DECLARE @O INT, @COUNTER INT

SET @COUNTER = 0
WHILE @COUNTER < " + this.CountParam + @"
    BEGIN

    INSERT INTO " + this.Objects.StatementName + " (" + this.TypeId + ", " + this.CacheId + @")
    VALUES (" + this.TypeId.Param + ", " + Reference.InitialCacheId + @" );

    INSERT INTO @IDS(id)
    VALUES (SCOPE_IDENTITY());

    SET @COUNTER = @COUNTER+1;
    END

INSERT INTO " + this.Table(((IComposite)objectType).ExclusiveLeafClass) + " (" + this.ObjectId + "," + this.TypeId + @")
SELECT ID," + this.TypeId.Param + @" FROM @IDS;

SELECT id FROM @IDS;
END";

                this.procedureByName.Add(procedure.Name, procedure);

                foreach (MappingColumn column in table)
                {
                    var relationType = column.RelationType;
                    if (relationType != null)
                    {
                        var roleType = relationType.RoleType;
                        var associationType = relationType.AssociationType;

                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            var unitTypeTag = ((IUnit)relationType.RoleType.ObjectType).UnitTag;
                            switch (unitTypeTag)
                            {
                                case UnitTags.AllorsString:
                                    // Set String Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.StringRelationTableParam + @" " + this.StringRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                case UnitTags.AllorsInteger:
                                    // Set Integer Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.IntegerRelationTableParam + @" " + this.IntegerRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                case UnitTags.AllorsFloat:
                                    // Set Double Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.FloatRelationTableParam + @" " + this.FloatRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                case UnitTags.AllorsDecimal:
                                    // Set Decimal Role
                                    var decimalRelationTable = this.DecimalRelationTableByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                                    var decimalRelationParameter = this.DecimalRelationTableParameterByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];

                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
" + decimalRelationParameter + @" " + decimalRelationTable + @" READONLY
AS 
UPDATE " + table + @"
SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
FROM " + table + @"
INNER JOIN " + this.CompositeRelationTableParam + @" AS r
ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";
                                    this.procedureByName.Add(procedure.Name, procedure);

                                    break;

                                case UnitTags.AllorsBoolean:
                                    // Set Boolean Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.BooleanRelationTableParam + @" " + this.BooleanRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                case UnitTags.AllorsDateTime:
                                    // Set DateTime Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.DateTimeRelationTableParam + @" " + this.DateTimeRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                case UnitTags.AllorsUnique:
                                    // Set Unique Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.UniqueRelationTableParam + @" " + this.UniqueRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";
                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                case UnitTags.AllorsBinary:
                                    // Set Binary Role
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.SingularFullName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.BinaryRelationTableParam + @" " + this.BinaryRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                    this.procedureByName.Add(procedure.Name, procedure);
                                    break;

                                default:
                                    throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.SingularName);
                            }
                        }
                        else
                        {
                            if (roleType.IsOne)
                            {
                                // Get Composite Role (1-1 and *-1) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "GR_" + objectType.Name + "_" + roleType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.AssociationId.Param + @" " + this.database.GetSqlType(this.AssociationId) + @"
AS 
    SELECT " + this.Column(roleType) + @"
    FROM " + table + @"
    WHERE " + this.ObjectId + "=" + this.AssociationId.Param;

                                this.procedureByName.Add(procedure.Name, procedure);

                                if (associationType.IsOne)
                                {
                                    // Get Composite Association (1-1) [object table]
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "GA_" + objectType.Name + "_" + associationType.SingularFullName };
                                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.RoleId.Param + @" " + this.database.GetSqlType(this.RoleId) + @"
AS 
    SELECT " + this.ObjectId + @"
    FROM " + table + @"
    WHERE " + this.Column(roleType) + "=" + this.RoleId.Param;

                                    this.procedureByName.Add(procedure.Name, procedure);
                                }
                                else
                                {
                                    // Get Composite Association (*-1) [object table]
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "GA_" + objectType.Name + "_" + associationType.SingularFullName };
                                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.RoleId.Param + @" " + this.database.GetSqlType(this.RoleId) + @"
AS 
    SELECT " + this.ObjectId + @"
    FROM " + table + @"
    WHERE " + this.Column(roleType) + "=" + this.RoleId.Param;

                                    this.procedureByName.Add(procedure.Name, procedure);
                                }

                                // Set Composite Role (1-1 and *-1) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "S_" + objectType.Name + "_" + roleType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.RelationTableRole + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableAssociation + @"
";

                                this.procedureByName.Add(procedure.Name, procedure);

                                // Clear Composite Role (1-1 and *-1) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "C_" + objectType.Name + "_" + roleType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + @" = null
    FROM " + table + @"
    INNER JOIN " + this.ObjectTableParam + @" AS a
    ON " + this.ObjectId + " = a." + this.ObjectTableObject;
                                
                                this.procedureByName.Add(procedure.Name, procedure);
                            }
                            else
                            {
                                // Get Composites Role (1-*) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "GR_" + objectType.Name + "_" + associationType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.AssociationId.Param + @" " + this.database.GetSqlType(this.AssociationId) + @"
AS
    SELECT " + this.ObjectId + @"
    FROM " + table + @"
    WHERE " + this.Column(associationType) + "=" + this.AssociationId.Param;

                                this.procedureByName.Add(procedure.Name, procedure);

                                if (associationType.IsOne)
                                {
                                    // Get Composite Association (1-*) [object table]
                                    procedure = new MappingProcedure { Name = AllorsPrefix + "GA_" + objectType.Name + "_" + associationType.SingularFullName };
                                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.RoleId.Param + @" " + this.database.GetSqlType(this.RoleId) + @"
AS
    SELECT " + this.Column(associationType) + @"
    FROM " + table + @"
    WHERE " + this.ObjectId + "=" + this.RoleId.Param;

                                    this.procedureByName.Add(procedure.Name, procedure);
                                }

                                // Add Composite Role (1-*) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "A_" + objectType.Name + "_" + associationType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS
    UPDATE " + table + @"
    SET " + this.Column(associationType) + " = r." + this.RelationTableAssociation + @"
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.ObjectId + " = r." + this.RelationTableRole;

                                this.procedureByName.Add(procedure.Name, procedure);

                                // Remove Composite Role (1-*) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "R_" + objectType.Name + "_" + associationType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS
    UPDATE " + table + @"
    SET " + this.Column(associationType) + @" = null
    FROM " + table + @"
    INNER JOIN " + this.CompositeRelationTableParam + @" AS r
    ON " + this.Column(associationType) + " = r." + this.RelationTableAssociation + @" AND
    " + this.ObjectId + " = r." + this.RelationTableRole;

                                this.procedureByName.Add(procedure.Name, procedure);

                                // Clear Composites Role (1-*) [object table]
                                procedure = new MappingProcedure { Name = AllorsPrefix + "C_" + objectType.Name + "_" + associationType.SingularFullName };
                                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 

    UPDATE " + table + @"
    SET " + this.Column(associationType) + @" = null
    FROM " + table + @"
    INNER JOIN " + this.ObjectTableParam + @" AS a
    ON " + this.Column(associationType) + " = a." + this.ObjectTableObject;
                                this.procedureByName.Add(procedure.Name, procedure);
                            }
                        }
                    }
                }
            }

            foreach (var dictionaryEntry in this.TablesByRelationType)
            {
                var relationType = dictionaryEntry.Key;
                var roleType = relationType.RoleType;
                var associationType = relationType.AssociationType;
                var table = dictionaryEntry.Value;

                if (roleType.IsMany)
                {
                    // Get Composites Role (1-* and *-*) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "GR_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.AssociationId.Param + @" " + this.database.GetSqlType(this.AssociationId) + @",
    " + this.RoleId.Param + @" " + this.ObjectTable + @" READONLY
AS
    SELECT " + this.RoleId + @"
    FROM " + this.Table(roleType) + @"
    WHERE " + this.AssociationId + "=" + this.AssociationId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);

                    // Add Composite Role (1-* and *-*) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "A_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS
    INSERT INTO " + table + " (" + this.AssociationId + "," + this.RoleId + @")
    SELECT " + this.RelationTableAssociation + @", " + this.RelationTableRole + @"
    FROM " + this.CompositeRelationTableParam + "\n";

                    this.procedureByName.Add(procedure.Name, procedure);

                    // Remove Composite Role (1-* and *-*) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "R_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS
    DELETE T 
    FROM " + table + @" T
    INNER JOIN " + this.CompositeRelationTableParam + @" R
    ON T." + this.AssociationId + " = R." + this.RelationTableAssociation + @"
    AND T." + this.RoleId + " = R." + this.RelationTableRole + @";";

                    this.procedureByName.Add(procedure.Name, procedure);
                }
                else
                {
                    // Get Composite Role (1-1 and *-1) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "GR_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.AssociationId.Param + @" " + this.database.GetSqlType(this.AssociationId) + @"
AS
    SELECT " + this.RoleId + @"
    FROM " + this.Table(roleType) + @"
    WHERE " + this.AssociationId + "=" + this.AssociationId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);

                    // Set Composite Role (1-1 and *-1) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "S_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS
    MERGE " + table + @" T
    USING " + this.CompositeRelationTableParam + @" AS r
    ON T." + this.AssociationId + @" = r." + this.RelationTableAssociation + @"

    WHEN MATCHED THEN
    UPDATE SET " + this.RoleId + @"= r." + this.RelationTableRole + @"

    WHEN NOT MATCHED THEN
    INSERT (" + this.AssociationId + "," + this.RoleId + @")
    VALUES (r." + this.RelationTableAssociation + ", r." + this.RelationTableRole + @");";

                    this.procedureByName.Add(procedure.Name, procedure);
                }

                if (associationType.IsOne)
                {
                    // Get Composite Association (1-1) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "GA_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.RoleId.Param + @" " + this.database.GetSqlType(this.RoleId) + @"
AS
    SELECT " + this.AssociationId + @"
    FROM " + table + @"
    WHERE " + this.RoleId + "=" + this.RoleId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);
                }
                else
                {
                    // Get Composite Association (*-1) [relation table]
                    procedure = new MappingProcedure { Name = AllorsPrefix + "GA_" + roleType.SingularFullName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.RoleId.Param + @" " + this.database.GetSqlType(this.RoleId) + @"
AS
    SELECT " + this.AssociationId + @"
    FROM " + table + @"
    WHERE " + this.RoleId + "=" + this.RoleId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);
                }

                // Clear Composite Role (1-1 and *-1) [relation table]
                procedure = new MappingProcedure { Name = AllorsPrefix + "C_" + roleType.SingularFullName };
                procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    DELETE T 
    FROM " + table + @" T
    INNER JOIN " + this.ObjectTableParam + @" A
    ON T." + this.AssociationId + " = A." + this.ObjectTableObject;
                this.procedureByName.Add(procedure.Name, procedure);
            }
        }

        internal class MappingExistingColumn
        {
            internal string DataType { get; set; }

            internal int CharacterMaximumLength { get; set; }

            internal int CharacterOctetLength { get; set; }

            internal int NumericPrecision { get; set; }

            internal int NumericScale { get; set; }

            internal int DateTimePrecision { get; set; }
        }
    }
}