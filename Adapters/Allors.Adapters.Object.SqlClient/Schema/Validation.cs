namespace Allors.Adapters.Object.SqlClient
{
    using System.Collections.Generic;
    using System.Text;

    public class Validation
    {
        public readonly HashSet<string> MissingTableNames;
        public readonly HashSet<SchemaTable> InvalidTables;

        public readonly HashSet<string> MissingTableTypeNames;
        public readonly HashSet<SchemaTableType> InvalidTableTypes;

        public readonly HashSet<string> MissingProcedureNames;
        public readonly HashSet<SchemaProcedure> InvalidProcedures;

        private readonly Database database;
        private readonly Mapping mapping;
        private readonly Schema schema;

        private readonly bool isValid;

        public Validation(Database database)
        {
            this.database = database;
            this.mapping = database.Mapping;
            this.schema = new Schema(database);
            
            this.MissingTableNames = new HashSet<string>();
            this.InvalidTables = new HashSet<SchemaTable>();

            this.MissingTableTypeNames = new HashSet<string>();
            this.InvalidTableTypes = new HashSet<SchemaTableType>();

            this.MissingProcedureNames = new HashSet<string>();
            this.InvalidProcedures = new HashSet<SchemaProcedure>();

            this.Validate();

            this.isValid = 
                this.MissingTableNames.Count == 0 & 
                this.InvalidTables.Count == 0 &
                this.MissingTableTypeNames.Count == 0 &
                this.InvalidTableTypes.Count == 0 &
                this.MissingProcedureNames.Count == 0 &
                this.InvalidProcedures.Count == 0;
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
        }

        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        public Schema Schema
        {
            get
            {
                return this.schema;
            }
        }

        public string Message 
        {
            get
            {
                var message = new StringBuilder();

                if (this.MissingTableNames.Count > 0)
                {
                    message.Append("Missing Tables:\n");
                    foreach (var missingTable in this.MissingTableNames)
                    {
                        message.Append("- " + missingTable + ":\n");
                    }
                }

                if (this.InvalidTables.Count > 0)
                {
                    message.Append("Invalid Tables:\n");
                    foreach (var invalidTable in this.InvalidTables)
                    {
                        message.Append("- " + invalidTable.Name + ":\n");
                    }
                }

                if (this.MissingTableTypeNames.Count > 0)
                {
                    message.Append("Missing Table Types:\n");
                    foreach (var missingTableType in this.MissingTableTypeNames)
                    {
                        message.Append("- " + missingTableType + ":\n");
                    }
                }

                if (this.InvalidTableTypes.Count > 0)
                {
                    message.Append("Invalid Table Types:\n");
                    foreach (var invalidTableType in this.InvalidTableTypes)
                    {
                        message.Append("- " + invalidTableType.Name + ":\n");
                    }
                }

                if (this.MissingProcedureNames.Count > 0)
                {
                    message.Append("Missing Procedures:\n");
                    foreach (var missingProcedure in this.MissingProcedureNames)
                    {
                        message.Append("- " + missingProcedure + ":\n");
                    }
                }

                if (this.InvalidProcedures.Count > 0)
                {
                    message.Append("Invalid Procedures:\n");
                    foreach (var invalidProcedure in this.InvalidProcedures)
                    {
                        message.Append("- " + invalidProcedure.Name + ":\n");
                    }
                }

                return message.ToString();
            }
        }

        private void Validate()
        {
            // Objects Table
            var objectsTable = this.Schema.GetTable(this.database.Mapping.TableNameForObjects);
            if (objectsTable == null)
            {
                this.MissingTableNames.Add(this.mapping.TableNameForObjects);
            }
            else
            {
                if (objectsTable.ColumnByLowercaseColumnName.Count != 3)
                {
                    this.InvalidTables.Add(objectsTable);
                }

                this.ValidateColumn(objectsTable, Mapping.ColumnNameForObject, this.Database.Mapping.SqlTypeForObject);
                this.ValidateColumn(objectsTable, Mapping.ColumnNameForClass, Mapping.SqlTypeForClass);
                this.ValidateColumn(objectsTable, Mapping.ColumnNameForVersion, Mapping.SqlTypeForVersion);
            }
            
            // Object Tables
            foreach (var @class in this.Database.MetaPopulation.Classes)
            {
                var tableName = this.mapping.TableNameForObjectByClass[@class];
                var table = this.Schema.GetTable(tableName);

                if (table == null)
                {
                    this.MissingTableNames.Add(tableName);
                }
                else
                {
                    this.ValidateColumn(table, Mapping.ColumnNameForObject, this.Database.Mapping.SqlTypeForObject);
                    this.ValidateColumn(table, Mapping.ColumnNameForClass, Mapping.SqlTypeForClass);

                    foreach (var associationType in @class.AssociationTypes)
                    {
                        var relationType = associationType.RelationType;
                        var roleType = relationType.RoleType;
                        if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses
                            && roleType.IsMany)
                        {
                            this.ValidateColumn(
                                table,
                                this.database.Mapping.ColumnNameByRelationType[relationType],
                                this.Database.Mapping.SqlTypeForObject);
                        }
                    }

                    foreach (var roleType in @class.RoleTypes)
                    {
                        var relationType = roleType.RelationType;
                        var associationType = relationType.AssociationType;
                        if (roleType.ObjectType.IsUnit)
                        {
                            this.ValidateColumn(
                                table,
                                this.database.Mapping.ColumnNameByRelationType[relationType],
                                this.Database.Mapping.GetSqlType(relationType.RoleType));
                        }
                        else
                        {
                            if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses
                                && !roleType.IsMany)
                            {
                                this.ValidateColumn(
                                    table,
                                    this.database.Mapping.ColumnNameByRelationType[relationType],
                                    this.Database.Mapping.SqlTypeForObject);
                            }
                        }
                    }
                }
            }

            // Relation Tables
            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;

                if (!roleType.ObjectType.IsUnit && 
                    ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveClasses))
                {
                    var tableName = this.mapping.TableNameForRelationByRelationType[relationType];
                    var table = this.Schema.GetTable(tableName);

                    if (table == null)
                    {
                        this.MissingTableNames.Add(tableName);
                    }
                    else
                    {
                        if (table.ColumnByLowercaseColumnName.Count != 2)
                        {
                            this.InvalidTables.Add(table);
                        }

                        this.ValidateColumn(table, Mapping.ColumnNameForAssociation, this.Database.Mapping.SqlTypeForObject);

                        var roleSqlType = relationType.RoleType.ObjectType.IsComposite ? this.Database.Mapping.SqlTypeForObject : this.mapping.GetSqlType(relationType.RoleType);
                        this.ValidateColumn(table, Mapping.ColumnNameForRole, roleSqlType);
                    }
                }
            }

            // TableTypes
            {
                // Object TableType
                var tableType = this.schema.GetTableType(this.mapping.TableTypeNameForObject);
                if (tableType == null)
                {
                    this.MissingTableTypeNames.Add(this.mapping.TableTypeNameForObject);
                }
                else
                {
                    if (tableType.ColumnByLowercaseColumnName.Count != 1)
                    {
                        this.InvalidTableTypes.Add(tableType);
                    }

                    this.ValidateColumn(tableType, this.mapping.TableTypeColumnNameForObject, this.Database.Mapping.SqlTypeForObject);
                }
            }

            this.ValidateTableType(this.mapping.TableTypeNameForCompositeRelation, this.Database.Mapping.SqlTypeForObject);
            this.ValidateTableType(this.mapping.TableTypeNameForStringRelation, "nvarchar(max)");
            this.ValidateTableType(this.mapping.TableTypeNameForIntegerRelation, "int");
            this.ValidateTableType(this.mapping.TableTypeNameForFloatRelation, "float");
            this.ValidateTableType(this.mapping.TableTypeNameForDateTimeRelation, "datetime2");
            this.ValidateTableType(this.mapping.TableTypeNameForBooleanRelation, "bit");
            this.ValidateTableType(this.mapping.TableTypeNameForUniqueRelation, "uniqueidentifier");
            this.ValidateTableType(this.mapping.TableTypeNameForBinaryRelation, "varbinary(max)");
            this.ValidateTableType(this.mapping.TableTypeNameForBinaryRelation, "varbinary(max)");

            // Decimal TableType
            foreach (var precisionEntry in this.mapping.TableTypeNameForDecimalRelationByScaleByPrecision)
            {
                foreach (var scaleEntry in precisionEntry.Value)
                {
                    var name = scaleEntry.Value;
                    var precision = precisionEntry.Key;
                    var scale = scaleEntry.Key;

                    this.ValidateTableType(name, "decimal(" + precision + "," + scale + ")");
                }
            }

            // Procedures Tables
            foreach (var dictionaryEntry in this.mapping.ProcedureDefinitionByName)
            {
                var procedureName = dictionaryEntry.Key;
                var procedureDefinition = dictionaryEntry.Value;

                var procedure = this.Schema.GetProcedure(procedureName);

                if (procedure == null)
                {
                    this.MissingProcedureNames.Add(procedureName);
                }
                else
                {
                    if (!procedure.IsDefinitionCompatible(procedureDefinition))
                    {
                        this.InvalidProcedures.Add(procedure);
                    }
                }
            }

            // TODO: Primary Keys and Indeces
        }

        private void ValidateTableType(string name, string columnType)
        {
            var tableType = this.schema.GetTableType(name);
            if (tableType == null)
            {
                this.MissingTableTypeNames.Add(name);
            }
            else
            {
                if (tableType.ColumnByLowercaseColumnName.Count != 2)
                {
                    this.InvalidTableTypes.Add(tableType);
                }

                this.ValidateColumn(tableType, this.mapping.TableTypeColumnNameForAssociation, this.Database.Mapping.SqlTypeForObject);
                this.ValidateColumn(tableType, this.mapping.TableTypeColumnNameForRole, columnType);
            }
        }

        private void ValidateColumn(SchemaTable table, string columnName, string sqlType)
        {
            var objectColumn = table.GetColumn(columnName);

            if (objectColumn == null || !objectColumn.SqlType.Equals(sqlType))
            {
                this.InvalidTables.Add(table);
            }
        }

        private void ValidateColumn(SchemaTableType tableType, string columnName, string sqlType)
        {
            var objectColumn = tableType.GetColumn(columnName);

            if (objectColumn == null || !objectColumn.SqlType.Equals(sqlType))
            {
                this.InvalidTableTypes.Add(tableType);
            }
        }
    }
}