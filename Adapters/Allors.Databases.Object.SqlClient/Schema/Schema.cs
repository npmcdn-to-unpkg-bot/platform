namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Schema
    {
        public readonly bool Exists;
        
        private readonly Dictionary<string, SchemaTable> tableByLowercaseTableName;
        private readonly Dictionary<string, SchemaTableType> tableTypeByLowercaseTableTypeName;
        private readonly Dictionary<string, SchemaProcedure> procedureByFullyQualifiedProcedureName;
        private readonly Dictionary<string, Dictionary<string, SchemaIndex>> indexByLowercaseIndexNameByLowercaseTableName;

        public Schema(Database database)
        {
            this.tableByLowercaseTableName = new Dictionary<string, SchemaTable>();
            this.tableTypeByLowercaseTableTypeName = new Dictionary<string, SchemaTableType>();
            this.procedureByFullyQualifiedProcedureName = new Dictionary<string, SchemaProcedure>();
            this.indexByLowercaseIndexNameByLowercaseTableName = new Dictionary<string, Dictionary<string, SchemaIndex>>();

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
                        this.Exists = schemaCount != 0;
                    }

                    // Tables
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
WHERE T.table_type = 'BASE TABLE'
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
                                tableName = database.Mapping.NormalizeName(tableName);
                                var fullyQualifiedTableName = database.SchemaName + "." + tableName;

                                columnName = database.Mapping.NormalizeName(columnName);

                                SchemaTable table;
                                if (!this.tableByLowercaseTableName.TryGetValue(fullyQualifiedTableName, out table))
                                {
                                    table = new SchemaTable(this, fullyQualifiedTableName);
                                    this.tableByLowercaseTableName[fullyQualifiedTableName] = table;
                                }

                                if (!reader.IsDBNull(columnNameOrdinal))
                                {
                                    var column = new SchemaTableColumn(table, columnName, dataType, characterMaximumLength, numericPrecision, numericScale);
                                    table.ColumnByLowercaseColumnName[column.LowercaseName] = column;
                                }
                            }
                        }
                    }

                    // Table Types
                    cmdText = @"
select tt.name as table_name, c.name as column_name, t.name AS data_type, c.max_length, c.precision, c.scale
from sys.table_types tt
inner join sys.columns c on c.object_id = tt.type_table_object_id
INNER JOIN sys.types AS t ON t.user_type_id = c.user_type_id
where tt.schema_id = SCHEMA_ID(@domainSchema)";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@domainSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            var tableNameOrdinal = reader.GetOrdinal("table_name");
                            var columnNameOrdinal = reader.GetOrdinal("column_name");
                            var dataTypeOrdinal = reader.GetOrdinal("data_type");
                            var maximumLengthOrdinal = reader.GetOrdinal("max_length");
                            var precisionOrdinal = reader.GetOrdinal("precision");
                            var scaleOrdinal = reader.GetOrdinal("scale");

                            while (reader.Read())
                            {
                                var tableName = reader.GetString(tableNameOrdinal);
                                var columnName = reader.GetString(columnNameOrdinal);
                                var dataType = reader.GetString(dataTypeOrdinal).Trim().ToLowerInvariant();
                                var maximumLength = reader.IsDBNull(maximumLengthOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(maximumLengthOrdinal));
                                var precision = reader.IsDBNull(precisionOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(precisionOrdinal));
                                var scale = reader.IsDBNull(scaleOrdinal) ? (int?)null : Convert.ToInt32(reader.GetValue(scaleOrdinal));

                                tableName = tableName.Trim().ToLowerInvariant();
                                var fullyQualifiedTableName = database.SchemaName + "." + tableName;

                                SchemaTableType tableType;
                                if (!this.tableTypeByLowercaseTableTypeName.TryGetValue(fullyQualifiedTableName, out tableType))
                                {
                                    tableType = new SchemaTableType(this, fullyQualifiedTableName);
                                    this.tableTypeByLowercaseTableTypeName[fullyQualifiedTableName] = tableType;
                                }

                                if (!reader.IsDBNull(columnNameOrdinal))
                                {
                                    var column = new SchemaTableTypeColumn(tableType, columnName, dataType, maximumLength, precision, scale);
                                    tableType.ColumnByLowercaseColumnName[column.Name] = column;
                                }
                            }
                        }
                    }

                    // Procedures
                    cmdText = @"
SELECT routine_name, routine_definition
FROM information_schema.routines
WHERE routine_schema = @routineSchema";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@routineSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var routineName = (string)reader["routine_name"];
                                var routineDefinition = (string)reader["routine_definition"];
                                var lowercaseRoutineName = routineName.Trim().ToLowerInvariant();
                                var fullyQualifiedName = database.SchemaName + "." + lowercaseRoutineName;
                                this.procedureByFullyQualifiedProcedureName[fullyQualifiedName] = new SchemaProcedure(this, routineName, routineDefinition);
                            }
                        }
                    }

                    // Indeces
                    cmdText = @"
SELECT	o.name AS table_name,
		i.name AS index_name
FROM		
		sys.indexes i
		INNER JOIN sys.objects o ON i.object_id = o.object_id
		INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE 
	i.name IS NOT NULL
	AND o.type = 'U'
	AND i.type = 2
	AND s.name = @tableSchema";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            var tableNameOrdinal = reader.GetOrdinal("table_name");
                            var indexNameOrdinal = reader.GetOrdinal("index_name");

                            while (reader.Read())
                            {
                                var tableName = reader.GetString(tableNameOrdinal);
                                var indexName = reader.GetString(indexNameOrdinal);

                                tableName = tableName.Trim().ToLowerInvariant();
                                indexName = indexName.Trim().ToLowerInvariant();

                                Dictionary<string, SchemaIndex> indexByLowercaseIndexName;
                                if (!this.indexByLowercaseIndexNameByLowercaseTableName.TryGetValue(tableName, out indexByLowercaseIndexName))
                                {
                                    indexByLowercaseIndexName = new Dictionary<string, SchemaIndex>();
                                    this.indexByLowercaseIndexNameByLowercaseTableName[tableName] = indexByLowercaseIndexName;
                                }

                                SchemaIndex index;
                                if (!indexByLowercaseIndexName.TryGetValue(indexName, out index))
                                {
                                    index = new SchemaIndex(this, indexName);
                                    indexByLowercaseIndexName[indexName] = index;
                                }
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

        public SchemaTable GetTable(string tableName)
        {
            SchemaTable table;
            this.tableByLowercaseTableName.TryGetValue(tableName.ToLowerInvariant(), out table);
            return table;
        }

        public SchemaTableType GetTableType(string tableTypeName)
        {
            SchemaTableType tableType;
            this.tableTypeByLowercaseTableTypeName.TryGetValue(tableTypeName, out tableType);
            return tableType;
        }

        public SchemaProcedure GetProcedure(string procedureName)
        {
            SchemaProcedure procedure;
            this.procedureByFullyQualifiedProcedureName.TryGetValue(procedureName, out procedure);
            return procedure;
        }

        public SchemaIndex GetIndex(string tableName, string indexName)
        {
            Dictionary<string, SchemaIndex> indexByLowercaseIndexName;
            if (this.indexByLowercaseIndexNameByLowercaseTableName.TryGetValue(tableName.ToLowerInvariant(), out indexByLowercaseIndexName))
            {
                SchemaIndex index;
                indexByLowercaseIndexName.TryGetValue(indexName.ToLowerInvariant(), out index);
                return index;
            }

            return null;
        }
    }
}