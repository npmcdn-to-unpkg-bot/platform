namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ValidateResult
    {
        public readonly HashSet<string> MissingTables;

        private readonly Mapping mapping;

        private readonly HashSet<string> tableNames; 
        private readonly Dictionary<string, Dictionary<string, Column>> columnByColumnNameByTableName;
        
        private readonly bool success;

        public ValidateResult(Mapping mapping)
        {
            this.mapping = mapping;

            this.tableNames = new HashSet<string>();
            this.columnByColumnNameByTableName = new Dictionary<string, Dictionary<string, Column>>();

            this.MissingTables = new HashSet<string>();

            this.FillTableNames();
            this.FillColumnByColumnNameByTableName();

            this.Validate();

            this.success = this.MissingTables.Count == 0;
        }

        public bool Success
        {
            get
            {
                return this.success;
            }
        }

        private void FillTableNames()
        {
            using (var connection = new SqlConnection(this.mapping.ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Objects
                            var cmdText = @"
SELECT  table_name
FROM information_schema.tables
WHERE table_schema = @tableSchema";

                            using (var command = new SqlCommand(cmdText, connection, transaction))
                            {
                                command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.mapping.SchemaName;
                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        var tableName = (string)reader["table_name"];
                                        tableName = tableName.Trim().ToLowerInvariant();
                                        this.tableNames.Add(tableName);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            transaction.Rollback();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void FillColumnByColumnNameByTableName()
        {
            using (var connection = new SqlConnection(this.mapping.ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Objects
                            var cmdText = @"
SELECT  table_name, 
        column_name, 
        data_type, 
        character_maximum_length,
        numeric_precision, 
        numeric_scale
FROM information_schema.columns
WHERE table_schema = @tableSchema";

                            using (var command = new SqlCommand(cmdText, connection, transaction))
                            {
                                command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.mapping.SchemaName;
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
                                        var tableName = reader.GetString(tableNameOrdinal).Trim().ToLowerInvariant();
                                        var columnName = reader.GetString(columnNameOrdinal).Trim().ToLowerInvariant();
                                        var dataType = reader.GetString(dataTypeOrdinal).Trim().ToLowerInvariant();
                                        var characterMaximumLength = reader.IsDBNull(characterMaximumLengthOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(characterMaximumLengthOrdinal));
                                        var numericPrecision = reader.IsDBNull(numericPrecisionOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(numericPrecisionOrdinal));
                                        var numericScale = reader.IsDBNull(numericScaleOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(numericScaleOrdinal));

                                        Dictionary<string, Column> columnByColumnName;
                                        if (!this.columnByColumnNameByTableName.TryGetValue(tableName, out columnByColumnName))
                                        {
                                            columnByColumnName = new Dictionary<string, Column>();
                                            this.columnByColumnNameByTableName[tableName] = columnByColumnName;
                                        }

                                        var column = new Column
                                        {
                                            TableName = tableName,
                                            ColumnName = columnName,
                                            DataType = dataType,
                                            CharacterMaximumLength = characterMaximumLength,
                                            NumericPrecision = numericPrecision,
                                            NumericScale = numericScale
                                        };

                                        columnByColumnName.Add(columnName, column);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            transaction.Rollback();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Validate()
        {
            // Objects Table
            if (!this.tableNames.Contains(Mapping.TableNameForObjects))
            {
                this.MissingTables.Add(Mapping.TableNameForObjects);
            }
        }

        private class Column
        {
            internal string TableName { get; set; }

            internal string ColumnName { get; set; }

            internal string DataType { get; set; }

            internal int? CharacterMaximumLength { get; set; }

            internal int? NumericPrecision { get; set; }

            internal int? NumericScale { get; set; }
        }
    }
}