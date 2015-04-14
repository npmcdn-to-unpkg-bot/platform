namespace Allors.Databases.Object.SqlClient
{
    using System.Collections.Generic;

    using Allors.Meta;

    public class Validation
    {
        public readonly HashSet<string> MissingTableNames;
        public readonly Dictionary<Table, HashSet<string>> MissingColumnNamesByTable;

        public readonly HashSet<Table> InvalidTables;
        public readonly HashSet<TableColumn> InvalidColumns; 

        private readonly Database database;
        private readonly Schema schema;

        private readonly bool success;

        public Validation(Database database)
        {
            this.database = database;
            this.schema = new Schema(database);
            
            this.MissingTableNames = new HashSet<string>();
            this.MissingColumnNamesByTable = new Dictionary<Table, HashSet<string>>();
            
            this.InvalidTables = new HashSet<Table>();
            this.InvalidColumns = new HashSet<TableColumn>();

            this.Validate();

            this.success = this.MissingTableNames.Count == 0 &
                           this.MissingColumnNamesByTable.Count == 0 &
                           this.InvalidTables.Count == 0 & 
                           this.InvalidColumns.Count == 0;
        }

        public bool Success
        {
            get
            {
                return this.success;
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

        private void Validate()
        {
            var mapping = this.Database.Mapping;

            // Objects Table
            var objectsTable = this.Schema.GetTable(Mapping.TableNameObjects);
            if (objectsTable == null)
            {
                this.MissingTableNames.Add(Mapping.TableNameObjects);
            }
            else
            {
                if (objectsTable.ColumnByLowercaseColumnName.Count != 3)
                {
                    this.InvalidTables.Add(objectsTable);
                }

                this.ValidateColumn(objectsTable, Mapping.TableColumnNameForObject, this.Database.Mapping.SqlTypeForObject);
                this.ValidateColumn(objectsTable, Mapping.TableColumnNameForType, Mapping.SqlTypeForType);
                this.ValidateColumn(objectsTable, Mapping.TableColumnNameForCache, Mapping.SqlTypeForCache);
            }
            
            // Object Tables
            foreach (var objectType in this.Database.MetaPopulation.Classes)
            {
                var tableName = mapping.GetTableName(objectType);
                var table = this.Schema.GetTable(tableName);

                if (table == null)
                {
                    this.MissingTableNames.Add(Mapping.TableNameObjects);
                }
                else
                {
                    this.ValidateColumn(table, Mapping.TableColumnNameForObject, this.Database.Mapping.SqlTypeForObject);
                    this.ValidateColumn(table, Mapping.TableColumnNameForType, Mapping.SqlTypeForType);

                    foreach (var associationType in objectType.AssociationTypes)
                    {
                        var relationType = associationType.RelationType;
                        var roleType = relationType.RoleType;
                        if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses
                            && roleType.IsMany)
                        {
                            this.ValidateColumn(
                                table,
                                this.database.Mapping.Column(relationType).Name,
                                this.Database.Mapping.SqlTypeForObject);
                        }
                    }

                    foreach (var roleType in objectType.RoleTypes)
                    {
                        var relationType = roleType.RelationType;
                        var associationType = relationType.AssociationType;
                        if (roleType.ObjectType.IsUnit)
                        {
                            this.ValidateColumn(
                                table,
                                this.database.Mapping.Column(relationType).Name,
                                this.Database.Mapping.GetSqlType(relationType.RoleType));
                        }
                        else
                        {
                            if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses
                                && !roleType.IsMany)
                            {
                                this.ValidateColumn(
                                    table,
                                    this.database.Mapping.Column(relationType).Name,
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
                    ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveLeafClasses))
                {
                    var tableName = mapping.GetTableName(relationType);
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

                        this.ValidateColumn(table, Mapping.TableColumnNameForAssociation, this.Database.Mapping.SqlTypeForObject);

                        var roleSqlType = relationType.RoleType.ObjectType.IsComposite ? this.Database.Mapping.SqlTypeForObject : mapping.GetSqlType(relationType.RoleType);
                        this.ValidateColumn(table, Mapping.TableColumnNameForRole, roleSqlType);
                    }
                }
            }
        }

        private void ValidateColumn(Table table, string columnName, string sqlType)
        {
            var objectColumn = table.GetColumn(columnName);

            if (objectColumn == null)
            {
                this.AddMissingColumnName(table, columnName);
            }
            else
            {
                if (!objectColumn.SqlType.Equals(sqlType))
                {
                    this.InvalidColumns.Add(objectColumn);
                }
            }
        }

        private void AddMissingColumnName(Table table, string columnName)
        {
            HashSet<string> missingColumnNames;
            if (!this.MissingColumnNamesByTable.TryGetValue(table, out missingColumnNames))
            {
                missingColumnNames = new HashSet<string>();
                this.MissingColumnNamesByTable[table] = missingColumnNames;
            }

            missingColumnNames.Add(columnName);
        }
    }
}