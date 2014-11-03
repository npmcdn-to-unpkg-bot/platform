namespace Allors.Adapters.Database.SqlClient
{
    using System.Collections.Generic;

    public class Validation
    {
        public readonly HashSet<string> MissingTableNames;
        public readonly Dictionary<Table, HashSet<string>> MissingColumnNamesByTable;

        private readonly Database database;
        private readonly Schema schema;

        private readonly bool success;

        public Validation(Database database)
        {
            this.database = database;
            this.schema = new Schema(database);
            
            this.MissingTableNames = new HashSet<string>();
            this.MissingColumnNamesByTable = new Dictionary<Table, HashSet<string>>();
            
            this.Validate();

            this.success = this.MissingTableNames.Count == 0 & this.MissingTableNames.Count == 0;
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
            // Objects Table
            var objectsTable = this.Schema[Mapping.TableNameForObjects];
            if (objectsTable == null)
            {
                this.MissingTableNames.Add(Mapping.TableNameForObjects);
            }
            else
            {
                var objectColumn = objectsTable[Mapping.ColumnNameForObject];
                var typeColumn = objectsTable[Mapping.ColumnNameForType];
                var cacheColumn = objectsTable[Mapping.ColumnNameForCache];

                if (objectColumn == null)
                {
                    this.AddMissingColumnName(objectsTable, Mapping.ColumnNameForObject);
                }

                if (typeColumn == null)
                {
                    this.AddMissingColumnName(objectsTable, Mapping.ColumnNameForType);
                }

                if (cacheColumn == null)
                {
                    this.AddMissingColumnName(objectsTable, Mapping.ColumnNameForCache);
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