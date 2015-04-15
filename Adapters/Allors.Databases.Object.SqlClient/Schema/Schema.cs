namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Schema
    {
        private readonly bool exists;
        private readonly Dictionary<string, SchemaTable> tableByLowercaseTableName;
        private readonly Dictionary<string, SchemaTableType> tableTypeByLowercaseTableTypeName;
        private readonly Dictionary<string, SchemaProcedure> procedureByFullyQualifiedProcedureName;
        private readonly Dictionary<string, SchemaView> viewByLowercaseViewName;
        private readonly Dictionary<string, Dictionary<string, SchemaIndex>> indexByLowercaseIndexNameByLowercaseTableName;

        public Schema(Database database)
        {
            this.tableByLowercaseTableName = new Dictionary<string, SchemaTable>();
            this.tableTypeByLowercaseTableTypeName = new Dictionary<string, SchemaTableType>();
            this.procedureByFullyQualifiedProcedureName = new Dictionary<string, SchemaProcedure>();
            this.viewByLowercaseViewName = new Dictionary<string, SchemaView>();
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
                        this.exists = schemaCount != 0;
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
                                SchemaTable table;
                                if (!this.TableByLowercaseTableName.TryGetValue(tableName, out table))
                                {
                                    table = new SchemaTable(this, tableName);
                                    this.TableByLowercaseTableName[tableName] = table;
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
                                var lowercaseTableTypeName = tableTypeName.Trim().ToLowerInvariant();
                                var fullyQualifiedName = database.SchemaName + "." + lowercaseTableTypeName;
                                this.TableTypeByLowercaseTableTypeName[fullyQualifiedName] = new SchemaTableType(this, tableTypeName);
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
                                this.ProcedureByFullyQualifiedProcedureName[fullyQualifiedName] = new SchemaProcedure(this, routineName, routineDefinition);
                            }
                        }
                    }

                    // Views
                    cmdText = @"
SELECT  _v.table_name AS view_name,
        _v.view_definition,
        _u.table_name
FROM information_schema.views AS _v
LEFT JOIN information_schema.view_table_usage AS _u
ON _v.table_name = _u.view_name
AND _v.table_schema = _u.view_schema
AND _v.table_schema = _u.table_schema
WHERE _v.table_schema = @tableSchema";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            var viewNameOrdinal = reader.GetOrdinal("view_name");
                            var viewDefinitionOrdinal = reader.GetOrdinal("view_definition");
                            var tableNameOrdinal = reader.GetOrdinal("table_name");

                            while (reader.Read())
                            {
                                var viewName = reader.GetString(viewNameOrdinal);
                                var viewDefinition = reader.GetString(viewDefinitionOrdinal);

                                viewName = viewName.Trim().ToLowerInvariant();
                                SchemaView view;
                                if (!this.ViewByLowercaseViewName.TryGetValue(viewName, out view))
                                {
                                    view = new SchemaView(this, viewName, viewDefinition);
                                    this.ViewByLowercaseViewName[viewName] = view;
                                }

                                if (!reader.IsDBNull(tableNameOrdinal))
                                {
                                    var tableName = reader.GetString(tableNameOrdinal);

                                    SchemaTable table;
                                    if (this.TableByLowercaseTableName.TryGetValue(tableName.ToLowerInvariant(), out table))
                                    {
                                        view.Tables.Add(table);
                                    }
                                }
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

        public Dictionary<string, SchemaTable> TableByLowercaseTableName
        {
            get
            {
                return this.tableByLowercaseTableName;
            }
        }

        public Dictionary<string, SchemaTableType> TableTypeByLowercaseTableTypeName
        {
            get
            {
                return this.tableTypeByLowercaseTableTypeName;
            }
        }

        public Dictionary<string, SchemaProcedure> ProcedureByFullyQualifiedProcedureName
        {
            get
            {
                return this.procedureByFullyQualifiedProcedureName;
            }
        }

        public Dictionary<string, SchemaView> ViewByLowercaseViewName
        {
            get
            {
                return this.viewByLowercaseViewName;
            }
        }
        
        public bool Exists
        {
            get
            {
                return this.exists;
            }
        }

        public Dictionary<string, Dictionary<string, SchemaIndex>> IndexByLowercaseIndexNameByLowercaseTableName
        {
            get
            {
                return this.indexByLowercaseIndexNameByLowercaseTableName;
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

        public SchemaView GetView(string viewName)
        {
            SchemaView view;
            this.viewByLowercaseViewName.TryGetValue(viewName.ToLowerInvariant(), out view);
            return view;
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