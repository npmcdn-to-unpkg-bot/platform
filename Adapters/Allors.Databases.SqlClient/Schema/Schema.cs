namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Schema
    {
        private readonly Database database;

        private readonly bool exists;
        private readonly Dictionary<string, Table> tableByLowercaseTableName;
        private readonly Dictionary<string, TableType> tableTypeByLowercaseTableTypeName;

        public Schema(Database database)
        {
            this.database = database;
            this.tableByLowercaseTableName = new Dictionary<string, Table>();
            this.tableTypeByLowercaseTableTypeName = new Dictionary<string, TableType>();

            using (var connection = new SqlConnection(database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Schema
                    var cmdText = @"
SELECT  count(schema_name)
FROM    information_schema.schemata
WHERE   schema_name = @schemaName";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@schemaName", SqlDbType.NVarChar).Value = database.SchemaName;
                        var schemaCount = (int)command.ExecuteScalar();
                        this.exists = schemaCount != 0;
                    }

                    // Objects
                    cmdText = @"
SELECT T.table_name,
       C.column_name, 
       C.data_type, 
       C.character_maximum_length,
       C.numeric_precision, 
       C.numeric_scale
FROM information_schema.tables AS T
FULL OUTER JOIN information_schema.columns AS C
ON T.table_name = C.table_name
AND T.table_schema = @tableSchema
AND C.table_schema = @tableSchema";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            var tableNameOrdinal = reader.GetOrdinal("table_name");
                            var columnNameOrdinal = reader.GetOrdinal("column_name");
                            var dataTypeOrdinal = reader.GetOrdinal("data_type");
                            var characterMaximumLengthOrdinal = reader.GetOrdinal("character_maximum_length");
                            var numericPrecisionOrdinal = reader.GetOrdinal("numeric_precision");
                            var numericScaleOrdinal = reader.GetOrdinal("numeric_scale");

                            while (reader.Read())
                            {
                                var tableName = reader.GetString(tableNameOrdinal);
                                var columnName = reader.GetString(columnNameOrdinal);
                                var dataType = reader.GetString(dataTypeOrdinal).Trim().ToLowerInvariant();
                                var characterMaximumLength = reader.IsDBNull(characterMaximumLengthOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(characterMaximumLengthOrdinal));
                                var numericPrecision = reader.IsDBNull(numericPrecisionOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(numericPrecisionOrdinal));
                                var numericScale = reader.IsDBNull(numericScaleOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(numericScaleOrdinal));

                                tableName = tableName.Trim().ToLowerInvariant();
                                Table table;
                                if (!this.TableByLowercaseTableName.TryGetValue(tableName, out table))
                                {
                                    table = new Table(this, tableName);
                                    this.TableByLowercaseTableName[tableName] = table;
                                }

                                if (!reader.IsDBNull(columnNameOrdinal))
                                {
                                    var column = new TableColumn(table, columnName, dataType, characterMaximumLength, numericPrecision, numericScale);
                                    table.ColumnByLowercaseColumnName[column.LowercaseName] = column;
                                }
                            }
                        }
                    }

                    // Table Types
                    cmdText = @"
SELECT domain_name
FROM information_schema.domains
WHERE data_type='table type' AND
domain_schema = @domainSchema";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@domainSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tableTypeName = (string)reader["domain_name"];
                                tableTypeName = tableTypeName.Trim().ToLowerInvariant();
                                this.TableTypeByLowercaseTableTypeName[tableTypeName] = new TableType(this, tableTypeName);
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Dictionary<string, Table> TableByLowercaseTableName
        {
            get
            {
                return this.tableByLowercaseTableName;
            }
        }

        public Dictionary<string, TableType> TableTypeByLowercaseTableTypeName
        {
            get
            {
                return this.tableTypeByLowercaseTableTypeName;
            }
        }

        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        public bool Exists
        {
            get
            {
                return this.exists;
            }
        }

        public Table GetTable(string tableName)
        {
            Table table;
            this.tableByLowercaseTableName.TryGetValue(tableName.ToLowerInvariant(), out table);
            return table;
        }

        public TableType GetTableType(string tableTypeName)
        {
            TableType tableType;
            this.tableTypeByLowercaseTableTypeName.TryGetValue(tableTypeName.ToLowerInvariant(), out tableType);
            return tableType;
        }
    }
}