namespace Allors.Databases.Relation.SqlClient
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

            this.columnByLowercaseColumnName = new Dictionary<string, TableColumn>();
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