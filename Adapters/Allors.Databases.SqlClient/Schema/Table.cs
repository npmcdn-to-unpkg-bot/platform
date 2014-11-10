namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Table
    {
        private readonly Schema schema;
        private readonly string tableName;
        private readonly string lowercaseTableName;
        private readonly Dictionary<string, Column> columnByLowercaseColumnName;

        public Table(Schema schema, string tableName)
        {
            this.schema = schema;
            this.tableName = tableName;
            this.lowercaseTableName = tableName.ToLowerInvariant();

            using (var connection = new SqlConnection(schema.Database.ConnectionString))
            {
                this.columnByLowercaseColumnName = new Dictionary<string, Column>();

                connection.Open();
                try
                {
                    // Objects
                    var cmdText = @"
SELECT  column_name, 
        data_type, 
        character_maximum_length,
        numeric_precision, 
        numeric_scale
FROM information_schema.columns
WHERE table_schema = @tableSchema
AND table_name = @tableName";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.Schema.Database.SchemaName;
                        command.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = this.TableName;
                        using (var reader = command.ExecuteReader())
                        {
                            var columnNameOrdinal = reader.GetOrdinal("column_name");
                            var dataTypeOrdinal = reader.GetOrdinal("data_type");
                            var characterMaximumLengthOrdinal = reader.GetOrdinal("character_maximum_length");
                            var numericPrecisionOrdinal = reader.GetOrdinal("numeric_precision");
                            var numericScaleOrdinal = reader.GetOrdinal("numeric_scale");

                            while (reader.Read())
                            {
                                var columnName = reader.GetString(columnNameOrdinal);
                                var dataType = reader.GetString(dataTypeOrdinal).Trim().ToLowerInvariant();
                                var characterMaximumLength = reader.IsDBNull(characterMaximumLengthOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(characterMaximumLengthOrdinal));
                                var numericPrecision = reader.IsDBNull(numericPrecisionOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(numericPrecisionOrdinal));
                                var numericScale = reader.IsDBNull(numericScaleOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(numericScaleOrdinal));

                                var column = new Column(this, columnName, dataType, characterMaximumLength, numericPrecision, numericScale);
                                this.ColumnByLowercaseColumnName[columnName.ToLowerInvariant()] = column;
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

        public string TableName
        {
            get
            {
                return this.tableName;
            }
        }

        public Dictionary<string, Column> ColumnByLowercaseColumnName
        {
            get
            {
                return this.columnByLowercaseColumnName;
            }
        }

        public string LowercaseTableName
        {
            get
            {
                return this.lowercaseTableName;
            }
        }

        public Schema Schema
        {
            get
            {
                return this.schema;
            }
        }

        public Column this[string columnName]
        {
            get
            {
                Column column;
                this.columnByLowercaseColumnName.TryGetValue(columnName.ToLowerInvariant(), out column);
                return column;
            }
        }

        public override string ToString()
        {
            return this.TableName;
        }
    }
}