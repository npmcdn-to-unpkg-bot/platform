namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Table
    {
        private readonly Schema schema;
        private readonly string name;
        private readonly string lowercaseName;
        private readonly Dictionary<string, TableColumn> columnByLowercaseColumnName;

        public Table(Schema schema, string name)
        {
            this.schema = schema;
            this.name = name;
            this.lowercaseName = name.ToLowerInvariant();

            using (var connection = new SqlConnection(schema.Database.ConnectionString))
            {
                this.columnByLowercaseColumnName = new Dictionary<string, TableColumn>();

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
                        command.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = this.Name;
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

                                var column = new TableColumn(this, columnName, dataType, characterMaximumLength, numericPrecision, numericScale);
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

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public Dictionary<string, TableColumn> ColumnByLowercaseColumnName
        {
            get
            {
                return this.columnByLowercaseColumnName;
            }
        }

        public string LowercaseName
        {
            get
            {
                return this.lowercaseName;
            }
        }

        public Schema Schema
        {
            get
            {
                return this.schema;
            }
        }

        public TableColumn GetColumn(string columnName)
        {
            TableColumn tableColumn;
            this.columnByLowercaseColumnName.TryGetValue(columnName.ToLowerInvariant(), out tableColumn);
            return tableColumn;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}