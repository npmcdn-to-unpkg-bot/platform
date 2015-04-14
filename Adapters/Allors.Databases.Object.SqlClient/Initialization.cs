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

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    public class Initialization
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
            this.EnableSnapshotIsolation();
            this.CreateSchema();
            this.CreateUserDefinedTypes();
            this.CreateTables();
            this.CreateProcedures();
            this.CreateIndeces();
        }

        private void EnableSnapshotIsolation()
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
        }

        private void CreateSchema()
        {
            if (!this.schema.Exists)
            {
                // CREATE SCHEMA must be in its own batch
                using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        var cmdText = @"
CREATE SCHEMA " + this.mapping.Database.SchemaName;
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
            }
        }

        private void CreateUserDefinedTypes()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForObject) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForObject + " " + this.mapping.GetSqlType(this.mapping.ObjectId) + ")\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForObject);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForCompositeRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " " + this.mapping.GetSqlType(this.mapping.RoleId) + ")\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForCompositeRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForStringRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " NVARCHAR(MAX))\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForStringRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForIntegerRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " INT)\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForIntegerRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForFloatRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " FLOAT)\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForFloatRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForDateTimeRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " DATETIME2)\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForDateTimeRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForBooleanRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " BIT)\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForBooleanRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForUniqueRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " UNIQUEIDENTIFIER)\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForUniqueRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForBinaryRelation) + " AS TABLE\n");
                        sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                        sql.Append(Mapping.TableTypeColumnNameForRole + " VARBINARY(MAX))\n");

                        var tableType = this.schema.GetTableType(Mapping.TableTypeNameForBinaryRelation);
                        if (tableType == null)
                        {
                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }

                    }

                    foreach (var precisionEntry in this.mapping.DecimalRelationTableByScaleByPrecision)
                    {
                        var precision = precisionEntry.Key;
                        foreach (var scaleEntry in precisionEntry.Value)
                        {
                            var scale = scaleEntry.Key;
                            var decimalRelationTable = scaleEntry.Value;

                            var sql = new StringBuilder();
                            sql.Append("CREATE TYPE " + (this.mapping.Database.SchemaName + "." + decimalRelationTable) + " AS TABLE\n");
                            sql.Append("(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.GetSqlType(this.mapping.AssociationId) + ",\n");
                            sql.Append(Mapping.TableTypeColumnNameForRole + " DECIMAL(" + precision + "," + scale + ") )\n");

                            var tableType = this.schema.GetTableType(decimalRelationTable);
                            if (tableType == null)
                            {
                                using (var command = new SqlCommand(sql.ToString(), connection))
                                {
                                    command.ExecuteNonQuery();
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

        private void CreateTables()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (MappingTable table in this.mapping)
                    {
                        var schemaTable = this.schema.GetTable(table.Name);
                        if (schemaTable != null)
                        {
                            var cmdText = @"
TRUNCATE TABLE " + this.mapping.Database.SchemaName + "." + table + @";
";
                            using (var command = new SqlCommand(cmdText, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            var sql = new StringBuilder();
                            sql.Append("CREATE TABLE " + this.mapping.Database.SchemaName + "." + table + "\n");
                            sql.Append("(\n");

                            foreach (MappingColumn column in table)
                            {
                                sql.Append(column + " ");
                                sql.Append(this.mapping.GetSqlType(column));

                                if (column.IsIdentity)
                                {
                                    sql.Append(" IDENTITY");
                                }

                                sql.Append(",\n");
                            }

                            sql.Append("PRIMARY KEY (");
                            var firstKey = true;
                            foreach (MappingColumn field in table)
                            {
                                if (field.IsKey)
                                {
                                    if (firstKey)
                                    {
                                        firstKey = false;
                                    }
                                    else
                                    {
                                        sql.Append(", ");
                                    }

                                    sql.Append(field.Name);
                                }
                            }

                            sql.Append(")\n");
                            sql.Append(")\n");

                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
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

        private void CreateProcedures()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var procedure in this.mapping.Procedures)
                    {
                        var schemaProcedure = this.schema.GetProcedure(procedure.Name);
                        if (schemaProcedure == null)
                        {
                            using (var command = new SqlCommand(procedure.Definition, connection))
                            {
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.StackTrace);
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

        private void CreateIndeces()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (MappingTable table in this.mapping)
                    {
                        foreach (MappingColumn column in table)
                        {
                            if (column.IndexType != MappingIndexType.None)
                            {
                                var indexName = "_" + column.Name;

                                if (column.IndexType == MappingIndexType.Single)
                                {
                                    var sql = new StringBuilder();
                                    sql.Append("CREATE INDEX " + indexName + "\n");
                                    sql.Append("ON " + this.mapping.Database.SchemaName + "." + table + " (" + column + ")");

                                    var schemaIndex = this.schema.GetIndex(table.Name, indexName);
                                    if (schemaIndex == null)
                                    {
                                        using (var command = new SqlCommand(sql.ToString(), connection))
                                        {
                                            try
                                            {
                                                command.ExecuteNonQuery();
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(0);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var sql = new StringBuilder();
                                    sql.Append("CREATE INDEX " + indexName + "\n");
                                    sql.Append("ON " + this.mapping.Database.SchemaName + "." + table + " (" + column + ", " + table.FirstKeyColumn + ")");

                                    var schemaIndex = this.schema.GetIndex(table.Name, indexName);
                                    if (schemaIndex == null)
                                    {
                                        using (var command = new SqlCommand(sql.ToString(), connection))
                                        {
                                            try
                                            {
                                                command.ExecuteNonQuery();
                                            }
                                            catch (Exception e)
                                            {
                                                Console.WriteLine(0);
                                            }
                                        }
                                    }
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
    }
}