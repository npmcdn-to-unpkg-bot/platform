namespace Allors.Databases.Relation.SqlClient
{
    using System.Collections.Generic;

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
            var objectsTable = this.Schema.GetTable(Mapping.TableNameForObjects);
            if (objectsTable == null)
            {
                this.MissingTableNames.Add(Mapping.TableNameForObjects);
            }
            else
            {
                var objectColumn = objectsTable.GetColumn(Mapping.ColumnNameForObject);
                var typeColumn = objectsTable.GetColumn(Mapping.ColumnNameForType);
                var cacheColumn = objectsTable.GetColumn(Mapping.ColumnNameForCache);

                if (objectsTable.ColumnByLowercaseColumnName.Count != 3)
                {
                    this.InvalidTables.Add(objectsTable);
                }

                if (objectColumn == null)
                {
                    this.AddMissingColumnName(objectsTable, Mapping.ColumnNameForObject);
                }
                else
                {
                    if (!objectColumn.DataType.Equals(this.Database.Mapping.SqlTypeForObject))
                    {
                        this.InvalidColumns.Add(objectColumn);
                    }
                }

                if (typeColumn == null)
                {
                    this.AddMissingColumnName(objectsTable, Mapping.ColumnNameForType);
                }
                else
                {
                    if (!typeColumn.DataType.Equals(Mapping.SqlTypeForType))
                    {
                        this.InvalidColumns.Add(typeColumn);
                    }
                }

                if (cacheColumn == null)
                {
                    this.AddMissingColumnName(objectsTable, Mapping.ColumnNameForCache);
                }
                else
                {
                    if (!cacheColumn.DataType.Equals(Mapping.SqlTypeForCache))
                    {
                        this.InvalidColumns.Add(cacheColumn);
                    }
                }
            }
            
            // Relations
            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
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

                    var associationColumn = table.GetColumn(Mapping.ColumnNameForAssociation);
                    var roleColumn = table.GetColumn(Mapping.ColumnNameForRole);

                    if (associationColumn == null)
                    {
                        this.AddMissingColumnName(table, Mapping.ColumnNameForAssociation);
                    }
                    else
                    {
                        if (!associationColumn.DataType.Equals(this.Database.Mapping.SqlTypeForObject))
                        {
                            this.InvalidColumns.Add(associationColumn);
                        }
                    }

                    if (roleColumn == null)
                    {
                        this.AddMissingColumnName(table, Mapping.ColumnNameForRole);
                    }
                    else
                    {
                        var sqlType = mapping.GetSqlType(relationType.RoleType);
                        if (!roleColumn.SqlType.Equals(sqlType))
                        {
                            this.InvalidColumns.Add(roleColumn);
                        }
                    }
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