//------------------------------------------------------------------------------------------------- 
// <copyright file="Schema.cs" company="Allors bvba">
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
// <summary>Defines the Schema type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    public abstract class Schema : Sql.Schema
    {
        public readonly string ObjectTable = AllorsPrefix + "_O";
        public readonly string ObjectTableObject = "_o";
        public readonly SchemaTableParameter ObjectTableParam;

        public readonly string RelationTableAssociation = "_a";
        public readonly string RelationTableRole = "_r";

        public readonly string CompositeRelationTable = AllorsPrefix + "_CompositeR";
        public readonly SchemaTableParameter CompositeRelationTableParam;

        public readonly string StringRelationTable = AllorsPrefix + "_StringR";
        public readonly SchemaTableParameter StringRelationTableParam;

        public readonly string IntegerRelationTable = AllorsPrefix + "_IntegerR";
        public readonly SchemaTableParameter IntegerRelationTableParam;
        
        public readonly string LongRelationTable = AllorsPrefix + "_LongR";
        public readonly SchemaTableParameter LongRelationTableParam;
       
        public readonly string DoubleRelationTable = AllorsPrefix + "_DoubleR";
        public readonly SchemaTableParameter DoubleRelationTableParam;
        
        public readonly string BooleanRelationTable = AllorsPrefix + "_BooleanR";
        public readonly SchemaTableParameter BooleanRelationTableParam;
        
        public readonly string DateRelationTable = AllorsPrefix + "_DateR";
        public readonly SchemaTableParameter DateRelationTableParam;
        
        public readonly string DateTimeRelationTable = AllorsPrefix + "_DateTimeR";
        public readonly SchemaTableParameter DateTimeRelationTableParam;
        
        public readonly string UniqueRelationTable = AllorsPrefix + "_UniqueR";
        public readonly SchemaTableParameter UniqueRelationTableParam;

        public readonly string BinaryRelationTable = AllorsPrefix + "_BinaryR";
        public readonly SchemaTableParameter BinaryRelationTableParam;

        public readonly Dictionary<int, Dictionary<int, string>> DecimalRelationTableByScaleByPrecision = new Dictionary<int, Dictionary<int, string>>();
        public readonly Dictionary<int, Dictionary<int, SchemaTableParameter>> DecimalRelationTableParameterByScaleByPrecision = new Dictionary<int, Dictionary<int, SchemaTableParameter>>(); 

        private readonly Database database;

        private SchemaValidationErrors schemaValidationErrors;
        private Dictionary<string, SchemaProcedure> procedureByName;

        internal Schema(Database database)
            : base(database, "@{0}", "@{0}", "[", "]")
        {
            this.database = database;
            this.ObjectTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_o", this.ObjectTable);
            this.CompositeRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.CompositeRelationTable);
            this.StringRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.StringRelationTable);
            this.IntegerRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.IntegerRelationTable);
            this.LongRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.LongRelationTable);
            this.DoubleRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.DoubleRelationTable);
            this.BooleanRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.BooleanRelationTable);
            this.DateRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.DateRelationTable);
            this.DateTimeRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.DateTimeRelationTable);
            this.UniqueRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.UniqueRelationTable);
            this.BinaryRelationTableParam = new SchemaTableParameter(this, AllorsPrefix + "p_r", this.BinaryRelationTable);

            foreach (var relationType in database.Domain.RelationTypes)
            {
                var roleType = relationType.RoleType;
                if (roleType.ObjectType.IsDecimal)
                {
                    var precision = roleType.Precision;
                    var scale = roleType.Scale;

                    var tableName = AllorsPrefix + "_DecimalR_" + precision + "_" + scale;

                    // table
                    Dictionary<int, string> decimalRelationTableByScale;
                    if (!this.DecimalRelationTableByScaleByPrecision.TryGetValue(precision, out decimalRelationTableByScale))
                    {
                        decimalRelationTableByScale = new Dictionary<int, string>();
                        this.DecimalRelationTableByScaleByPrecision[precision] = decimalRelationTableByScale;
                    }

                    if (!decimalRelationTableByScale.ContainsKey(scale))
                    {
                        decimalRelationTableByScale[scale] = tableName;
                    }

                    // param
                    Dictionary<int, SchemaTableParameter> schemaTableParameterByScale;
                    if (!this.DecimalRelationTableParameterByScaleByPrecision.TryGetValue(precision, out schemaTableParameterByScale))
                    {
                        schemaTableParameterByScale = new Dictionary<int, SchemaTableParameter>();
                        this.DecimalRelationTableParameterByScaleByPrecision[precision] = schemaTableParameterByScale;
                    }

                    if (!schemaTableParameterByScale.ContainsKey(scale))
                    {
                        schemaTableParameterByScale[scale] = new SchemaTableParameter(this, AllorsPrefix + "p_r", tableName); 
                    }
                }
            }
        }

        public override SchemaValidationErrors SchemaValidationErrors
        {
            get
            {
                if (this.schemaValidationErrors == null)
                {
                    this.schemaValidationErrors = new SchemaValidationErrors();

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

                        var dataRowViewByExistingColumnsByTable = new Dictionary<SchemaTable, Dictionary<SchemaColumn, SchemaExistingColumn>>();
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

                                                var existingColumn = new SchemaExistingColumn
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
                                                        table, new Dictionary<SchemaColumn, SchemaExistingColumn>());
                                                }

                                                dataRowViewByExistingColumnsByTable[table].Add(column, existingColumn);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (SchemaTable table in this)
                        {
                            if (tableNames.Contains(table.Name))
                            {
                                if (dataRowViewByExistingColumnsByTable.ContainsKey(table))
                                {
                                    var dataRowViewByExistingColumns = dataRowViewByExistingColumnsByTable[table];
                                    foreach (SchemaColumn column in table)
                                    {
                                        if (dataRowViewByExistingColumns.ContainsKey(column))
                                        {
                                            var existingColumn = dataRowViewByExistingColumns[column];
                                            if (column.RelationType != null)
                                            {
                                                var dataType = existingColumn.DataType.ToLower();

                                                if (!column.RelationType.RoleType.ObjectType.IsUnit)
                                                {
                                                    if (!dataType.Equals(this.SqlDbType.ToString().ToLower()))
                                                    {
                                                        AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                    }
                                                }
                                                else
                                                {
                                                    var unitTypeTag =
                                                        (UnitTags)column.RelationType.RoleType.ObjectType.UnitTag;
                                                    switch (unitTypeTag)
                                                    {
                                                        case UnitTags.AllorsString:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.NVarChar.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }
                                                            else
                                                            {
                                                                var size = existingColumn.CharacterMaximumLength;

                                                                if (column.RelationType.RoleType.Size > size
                                                                    && size != -1)
                                                                {
                                                                    AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                                }
                                                            }

                                                            break;

                                                        case UnitTags.AllorsInteger:
                                                            if (!dataType.Equals(SqlDbType.Int.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsLong:
                                                            if (!dataType.Equals(SqlDbType.BigInt.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsDecimal:
                                                            if (!dataType.Equals(SqlDbType.Decimal.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }
                                                            else
                                                            {
                                                                var precision = existingColumn.NumericPrecision;
                                                                var scale = existingColumn.NumericScale;

                                                                var role = column.RelationType.RoleType;
                                                                if (role.Precision > precision)
                                                                {
                                                                    AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                                }

                                                                if (role.Scale > scale)
                                                                {
                                                                    AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                                }
                                                            }

                                                            break;

                                                        case UnitTags.AllorsDouble:
                                                            if (!dataType.Equals(SqlDbType.Float.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsBoolean:
                                                            if (!dataType.Equals(SqlDbType.Bit.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsDateTime:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.DateTime2.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsUnique:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.UniqueIdentifier.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }

                                                            break;

                                                        case UnitTags.AllorsBinary:
                                                            if (
                                                                !dataType.Equals(
                                                                    SqlDbType.VarBinary.ToString().ToLower()))
                                                            {
                                                                AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
                                                            }
                                                            else
                                                            {
                                                                var size = existingColumn.CharacterOctetLength;

                                                                if (column.RelationType.RoleType.Size > size
                                                                    && size != -1)
                                                                {
                                                                    AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Incompatible);
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
                                            AddError(this.schemaValidationErrors, table, column, SchemaValidationErrorKind.Missing);
                                        }
                                    }
                                }
                                else
                                {
                                    AddError(this.schemaValidationErrors, table, SchemaValidationErrorKind.Missing);
                                }
                            }
                            else
                            {
                                AddError(this.schemaValidationErrors, table, SchemaValidationErrorKind.Missing);
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
                                AddError(this.schemaValidationErrors, procedure, SchemaValidationErrorKind.Missing, "Procedure " + procedure.Name + " is missing.");
                            }
                            else
                            {
                                if (!procedure.Definition.Equals(existingProcedureDefinition))
                                {
                                    AddError(this.schemaValidationErrors, procedure, SchemaValidationErrorKind.Incompatible, "Procedure " + procedure.Name + " is incompatible.");
                                }
                            }
                        }
                    }
                    catch
                    {
                        this.schemaValidationErrors = null;
                        throw;
                    }
                    finally
                    {
                        session.Rollback();
                    }
                }

                return this.schemaValidationErrors;
            }
        }

        public override IEnumerable<SchemaProcedure> Procedures
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

        public override Sql.SchemaParameter CreateParameter(string name, DbType dbType)
        {
            return new SchemaParameter(this, name, dbType);
        }

        protected override void OnConstructed()
        {
            base.OnConstructed();

            this.procedureByName = new Dictionary<string, SchemaProcedure>();

            // Get Cache Ids
            var procedure = new SchemaProcedure { Name = AllorsPrefix + "GC" };
            procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    SELECT " + this.ObjectId + ", " + this.CacheId + @"
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN (SELECT " + this.ObjectTableObject + @" FROM " + this.ObjectTableParam + ")";

            this.procedureByName.Add(procedure.Name, procedure);

            // Update Cache Ids
            procedure = new SchemaProcedure { Name = AllorsPrefix + "UC" };
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
            procedure = new SchemaProcedure { Name = AllorsPrefix + "RC" };
            procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectTableParam + @" " + this.ObjectTable + @" READONLY
AS 
    UPDATE " + this.Objects + @"
    SET " + this.CacheId + " = " + Reference.InitialCacheId + @"
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN ( SELECT " + this.ObjectTableObject + " FROM " + this.ObjectTableParam + ");\n\n";

            this.procedureByName.Add(procedure.Name, procedure);

            foreach (var concreteComposite in this.Database.Domain.ConcreteCompositeObjectTypes)
            {
                var sortedUnitRoleTypes = this.Database.GetSortedUnitRolesByObjectType(concreteComposite);

                if (sortedUnitRoleTypes.Length > 0)
                {
                    // Get Unit Roles
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GU_" + concreteComposite.Name };
                    procedure.Definition = @"CREATE PROCEDURE " + procedure.Name + @"
    " + this.ObjectId.Param + " AS " + this.database.GetSqlType(this.ObjectId) + @"
AS 
    SELECT ";
                    var first = true;
                    foreach (var role in sortedUnitRoleTypes)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            procedure.Definition += ", ";
                        }

                        procedure.Definition += this.ColumnsByRelationType[role.RelationTypeWhereRoleType];
                    }

                    procedure.Definition += @"
    FROM " + this.Table(concreteComposite.ExclusiveRootClass) + @"
    WHERE " + this.ObjectId + "=" + this.ObjectId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);
                }
            }

            foreach (var dictionaryEntry in this.TableByObjectType)
            {
                var objectType = dictionaryEntry.Key;
                var table = dictionaryEntry.Value;

                // Load Objects
                procedure = new SchemaProcedure { Name = AllorsPrefix + "L_" + objectType.Name };
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
                procedure = new SchemaProcedure { Name = AllorsPrefix + "CO_" + objectType.Name };
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
                procedure = new SchemaProcedure { Name = AllorsPrefix + "COS_" + objectType.Name };
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

INSERT INTO " + this.Table(objectType.ExclusiveRootClass) + " (" + this.ObjectId + "," + this.TypeId + @")
SELECT ID," + this.TypeId.Param + @" FROM @IDS;

SELECT id FROM @IDS;
END";

                this.procedureByName.Add(procedure.Name, procedure);

                foreach (SchemaColumn column in table)
                {
                    var relationType = column.RelationType;
                    if (relationType != null)
                    {
                        var roleType = relationType.RoleType;
                        var associationType = relationType.AssociationType;

                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            var unitTypeTag = (UnitTags)relationType.RoleType.ObjectType.UnitTag;
                            switch (unitTypeTag)
                            {
                                case UnitTags.AllorsString:
                                    // Set String Role
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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

                                case UnitTags.AllorsLong:
                                    // Set Long Role
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.LongRelationTableParam + @" " + this.LongRelationTable + @" READONLY
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
                                    var decimalRelationTable = this.DecimalRelationTableByScaleByPrecision[roleType.Precision][roleType.Scale];
                                    var decimalRelationParameter = this.DecimalRelationTableParameterByScaleByPrecision[roleType.Precision][roleType.Scale];

                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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

                                case UnitTags.AllorsDouble:
                                    // Set Double Role
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
                                    procedure.Definition = "CREATE PROCEDURE " + procedure.Name + @"
    " + this.DoubleRelationTableParam + @" " + this.DoubleRelationTable + @" READONLY
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "SR_" + objectType.Name + "_" + roleType.RootName };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "GR_" + objectType.Name + "_" + roleType.RootName };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GA_" + objectType.Name + "_" + associationType.Name };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GA_" + objectType.Name + "_" + associationType.Name };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "S_" + objectType.Name + "_" + roleType.RootName };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "C_" + objectType.Name + "_" + roleType.RootName };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "GR_" + objectType.Name + "_" + associationType.Name };
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
                                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GA_" + objectType.Name + "_" + associationType.Name };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "A_" + objectType.Name + "_" + associationType.Name };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "R_" + objectType.Name + "_" + associationType.Name };
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
                                procedure = new SchemaProcedure { Name = AllorsPrefix + "C_" + objectType.Name + "_" + associationType.Name };
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
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GR_" + roleType.FullSingularName };
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
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "A_" + roleType.FullSingularName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.CompositeRelationTableParam + @" " + this.CompositeRelationTable + @" READONLY
AS
    INSERT INTO " + table + " (" + this.AssociationId + "," + this.RoleId + @")
    SELECT " + this.RelationTableAssociation + @", " + this.RelationTableRole + @"
    FROM " + this.CompositeRelationTableParam + "\n";

                    this.procedureByName.Add(procedure.Name, procedure);

                    // Remove Composite Role (1-* and *-*) [relation table]
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "R_" + roleType.FullSingularName };
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
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GR_" + roleType.FullSingularName };
                    procedure.Definition =
@"CREATE PROCEDURE " + procedure.Name + @"
    " + this.AssociationId.Param + @" " + this.database.GetSqlType(this.AssociationId) + @"
AS
    SELECT " + this.RoleId + @"
    FROM " + this.Table(roleType) + @"
    WHERE " + this.AssociationId + "=" + this.AssociationId.Param;

                    this.procedureByName.Add(procedure.Name, procedure);

                    // Set Composite Role (1-1 and *-1) [relation table]
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "S_" + roleType.FullSingularName };
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
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GA_" + roleType.FullSingularName };
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
                    procedure = new SchemaProcedure { Name = AllorsPrefix + "GA_" + roleType.FullSingularName };
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
                procedure = new SchemaProcedure { Name = AllorsPrefix + "C_" + roleType.FullSingularName };
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

        public class SchemaExistingColumn
        {
            public string DataType { get; set; }

            public int CharacterMaximumLength { get; set; }

            public int CharacterOctetLength { get; set; }

            public int NumericPrecision { get; set; }

            public int NumericScale { get; set; }

            public int DateTimePrecision { get; set; }
        }
    }
}