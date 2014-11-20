namespace Allors.Databases.SqlClient
{
    public class ViewColumn
    {
        private readonly View view;
        private readonly string name;
        private readonly string lowercaseName;
        private readonly string dataType;
        private readonly int? characterMaximumLength;
        private readonly int? numericPrecision;
        private readonly int? numericScale;

        public ViewColumn(View view, string name, string dataType, int? characterMaximumLength, int? numericPrecision, int? numericScale)
        {
            this.view = view;
            this.name = name;
            this.lowercaseName = name.ToLowerInvariant();
            this.dataType = dataType;
            this.characterMaximumLength = characterMaximumLength;
            this.numericPrecision = numericPrecision;
            this.numericScale = numericScale;
        }

        public View View
        {
            get
            {
                return this.view;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string LowercaseName
        {
            get
            {
                return this.lowercaseName;
            }
        }

        public string DataType
        {
            get
            {
                return this.dataType;
            }
        }

        public string SqlType
        {
            get
            {
                if (this.dataType.Equals("nvarchar"))
                {
                    var length = this.CharacterMaximumLength == -1 ? "MAX" : this.CharacterMaximumLength.ToString();
                    return "nvarchar(" + length + ")";
                }

                if (this.dataType.Equals("varbinary"))
                {
                    var length = this.CharacterMaximumLength == -1 ? "MAX" : this.CharacterMaximumLength.ToString();
                    return "varbinary(" + length + ")";
                }


                if (this.dataType.Equals("decimal"))
                {
                    return "decimal(" + this.numericPrecision + "," + this.numericScale + ")";
                }

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

        public override string ToString()
        {
            return this.Name;
        }
    }
}