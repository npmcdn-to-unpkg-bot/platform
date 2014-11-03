namespace Allors.Adapters.Database.SqlClient
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Schema
    {
        private readonly Database database;
        private readonly Dictionary<string, Table> tableByLowercaseTableName;

        public Schema(Database database)
        {
            this.database = database;
            this.tableByLowercaseTableName = new Dictionary<string, Table>();

            using (var connection = new SqlConnection(database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Objects
                    var cmdText = @"
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

        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        public Table this[string tableName]
        {
            get
            {
                Table table;
                this.tableByLowercaseTableName.TryGetValue(tableName.ToLowerInvariant(), out table);
                return table;
            }
        }
    }
}