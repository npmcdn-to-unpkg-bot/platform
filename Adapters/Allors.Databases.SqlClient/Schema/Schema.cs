namespace Allors.Adapters.Database.SqlClient
{
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
SELECT  table_name
FROM information_schema.tables
WHERE table_schema = @tableSchema";

                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = database.SchemaName;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var tableName = (string)reader["table_name"];
                                tableName = tableName.Trim().ToLowerInvariant();
                                this.TableByLowercaseTableName[tableName] = new Table(this, tableName);
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