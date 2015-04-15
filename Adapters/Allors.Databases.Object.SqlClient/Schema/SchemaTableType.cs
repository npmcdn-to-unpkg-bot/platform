namespace Allors.Databases.Object.SqlClient
{
    public class SchemaTableType
    {
        private readonly string name;

        public SchemaTableType(Schema schema, string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}