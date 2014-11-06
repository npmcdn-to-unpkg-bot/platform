// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Initialization.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Meta;

    internal class Initialization 
    {
        private readonly Mapping mapping;
        private readonly Schema schema;

        internal Initialization(Mapping mapping, Schema schema)
        {
            this.mapping = mapping;
            this.schema = schema;
        }

        internal void Execute()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    var cmdText = @"
alter database " + connection.Database + @"
set allow_snapshot_isolation on";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            bool schemaExists;
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    var cmdText = @"
SELECT  count(schema_name)
FROM    information_schema.schemata
WHERE   schema_name = @schemaName";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@schemaName", SqlDbType.NVarChar).Value = this.mapping.Database.SchemaName;
                        var schemaCount = (int)command.ExecuteScalar();
                        schemaExists = schemaCount != 0;
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            // CREATE SCHEMA must be in its own batch
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (!schemaExists)
                    {
                        var cmdText = @"
CREATE SCHEMA " + this.mapping.Database.SchemaName;
                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Objects
                    var cmdText = @"
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = @tableName AND table_schema = @tableSchema)
BEGIN
TRUNCATE TABLE " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
END
ELSE
BEGIN
CREATE TABLE " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
(
" + Mapping.ColumnNameForObject + @" " + this.mapping.SqlTypeForId + @" IDENTITY(1,1),
" + Mapping.ColumnNameForType + @" " + Mapping.SqlTypeForType + @",
" + Mapping.ColumnNameForCache + @" " + Mapping.SqlTypeForCache + @",
PRIMARY KEY (O)
)
END
";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = Mapping.TableNameForObjects;
                        command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.mapping.Database.SchemaName;
                        command.ExecuteNonQuery();
                    }

                    // Relations
                    foreach (var relationType in this.mapping.Database.MetaPopulation.RelationTypes)
                    {
                        var roleType = relationType.RoleType;

                        var tableName = this.mapping.GetTableName(relationType);
                        var sqlTypeForRole = this.mapping.GetSqlType(roleType);
                        var primaryKeys = "a";
                        if (roleType.ObjectType is IComposite && roleType.IsMany)
                        {
                            primaryKeys = Mapping.ColumnNameForAssociation + @" , " + Mapping.ColumnNameForRole;
                        }

                        cmdText = @"
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = @tableName AND table_schema = @tableSchema)
BEGIN
TRUNCATE TABLE " + this.mapping.Database.SchemaName + "." + tableName + @"
END
ELSE
BEGIN
CREATE TABLE " + this.mapping.Database.SchemaName + "." + tableName + @"
(
" + Mapping.ColumnNameForAssociation + @" " + this.mapping.SqlTypeForId + @",
" + Mapping.ColumnNameForRole + @" " + sqlTypeForRole + @",
PRIMARY KEY ( " + primaryKeys + @")
)
END
";
                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.Parameters.Add("@tableName", SqlDbType.NVarChar).Value = tableName;
                            command.Parameters.Add("@tableSchema", SqlDbType.NVarChar).Value = this.mapping.Database.SchemaName;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}