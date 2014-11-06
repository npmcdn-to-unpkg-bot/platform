namespace Allors.Adapters.Database.SqlClient
{
    using System;

    public class Column
    {
        private readonly Table table;
        private readonly string columnName;
        private readonly string lowercaseColumnName;
        private readonly string dataType;
        private readonly int? characterMaximumLength;
        private readonly int? numericPrecision;
        private readonly int? numericScale;

        public Column(Table table, string columnName, string dataType, int? characterMaximumLength, int? numericPrecision, int? numericScale)
        {
            this.table = table;
            this.columnName = columnName;
            this.lowercaseColumnName = columnName.ToLowerInvariant();
            this.dataType = dataType;
            this.characterMaximumLength = characterMaximumLength;
            this.numericPrecision = numericPrecision;
            this.numericScale = numericScale;
        }

        public Table Table
        {
            get
            {
                return this.table;
            }
        }

        public string ColumnName
        {
            get
            {
                return this.columnName;
            }
        }

        public string LowercaseColumnName
        {
            get
            {
                return this.lowercaseColumnName;
            }
        }

        public string DataType
        {
            get
            {
                return this.dataType;
            }
        }

        public int? CharacterMaximumLength
        {
            get
            {
                return this.characterMaximumLength;
            }
        }

        public int? NumericPrecision
        {
            get
            {
                return this.numericPrecision;
            }
        }

        public int? NumericScale
        {
            get
            {
                return this.numericScale;
            }
        }

    }
}