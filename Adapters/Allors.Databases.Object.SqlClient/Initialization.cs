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
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    using Allors.Meta;

    public class Initialization
    {
        private readonly Database database;
        private readonly Mapping mapping;
        
        private Validation validation;

        internal Initialization(Database database)
        {
            this.database = database;
            this.mapping = database.Mapping;
        }

        internal void Execute()
        {
            this.validation = new Validation(this.database);

            this.AllowSnapshotIsolation();

            if (this.validation.IsValid)
            {
                this.ProcessTables();
            }
            else
            {
                this.CreateSchema();

                this.DropProcedures();

                this.ProcessTables();

                this.DropTableTypes();

                this.CreateTableTypes();

                this.CreateProcedures();

                this.CreateIndeces();
            }
        }

        private void AllowSnapshotIsolation()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
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
            if (!this.validation.Schema.Exists)
            {
                // CREATE SCHEMA must be in its own batch
                using (var connection = new SqlConnection(this.database.ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        var cmdText = @"
CREATE SCHEMA " + this.database.SchemaName;
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

        private void DropProcedures()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var dictionaryEntry in this.mapping.ProcedureDefinitionByName)
                    {
                        var name = dictionaryEntry.Key;

                        var procedure = this.validation.Schema.GetProcedure(name);
                        if (procedure != null)
                        {
                            using (var command = new SqlCommand("DROP PROCEDURE " + name, connection))
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

        private void DropTableTypes()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var dictionaryEntry in this.mapping.TableTypeDefinitionByName)
                    {
                        var name = dictionaryEntry.Key;

                        if (!this.validation.MissingTableTypeNames.Contains(name))
                        {
                            using (var command = new SqlCommand("DROP TYPE " + name, connection))
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

        private void CreateTableTypes()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var dictionaryEntry in this.mapping.TableTypeDefinitionByName)
                    {
                        var definition = dictionaryEntry.Value;
                        
                        using (var command = new SqlCommand(definition, connection))
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
        }

        private void ProcessTables()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (!this.TruncateOrDropTable(connection, this.mapping.TableNameForObjects))
                    {
                        var sql = new StringBuilder();
                        sql.Append("CREATE TABLE " + this.mapping.TableNameForObjects + "\n");
                        sql.Append("(\n");
                        sql.Append(Mapping.ColumnNameForObject + " " + this.mapping.SqlTypeForObject + " IDENTITY(1,1) PRIMARY KEY,\n");
                        sql.Append(Mapping.ColumnNameForType + " " + Mapping.SqlTypeForType + ",\n");
                        sql.Append(Mapping.ColumnNameForCache + " " + Mapping.SqlTypeForCache + "\n");
                        sql.Append(")\n");

                        using (var command = new SqlCommand(sql.ToString(), connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    
                    foreach (var @class in this.mapping.Database.MetaPopulation.Classes)
                    {
                        var name = this.mapping.TableNameForObjectByClass[@class];

                        if (name.Contains("User"))
                        {
                            Console.Write(0);
                        }

                        if (!this.TruncateOrDropTable(connection, name))
                        {
                            var sql = new StringBuilder();
                            sql.Append("CREATE TABLE " + name + "\n");
                            sql.Append("(\n");
                            sql.Append(Mapping.ColumnNameForObject + " " + this.mapping.SqlTypeForObject + " PRIMARY KEY,\n");
                            sql.Append(Mapping.ColumnNameForType + " " + Mapping.SqlTypeForType);

                            foreach (var associationType in @class.AssociationTypes)
                            {
                                var relationType = associationType.RelationType;
                                var roleType = relationType.RoleType;
                                if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses && roleType.IsMany)
                                {
                                    sql.Append(",\n" + this.mapping.ColumnNameByRelationType[relationType] + " " + this.mapping.SqlTypeForObject);
                                }
                            }

                            foreach (var roleType in @class.RoleTypes)
                            {
                                var relationType = roleType.RelationType;
                                var associationType3 = relationType.AssociationType;
                                if (roleType.ObjectType.IsUnit)
                                {
                                    sql.Append(",\n" + this.mapping.ColumnNameByRelationType[relationType] + " " + this.mapping.GetSqlType(roleType));
                                }
                                else
                                {
                                    if (!(associationType3.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses && !roleType.IsMany)
                                    {
                                        sql.Append(",\n" + this.mapping.ColumnNameByRelationType[relationType] + " " + this.mapping.SqlTypeForObject);
                                    }
                                }
                            }

                            sql.Append(")\n");

                            using (var command = new SqlCommand(sql.ToString(), connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    foreach (var relationType in this.mapping.Database.MetaPopulation.RelationTypes)
                    {
                        var associationType = relationType.AssociationType;
                        var roleType = relationType.RoleType;

                        if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveLeafClasses))
                        {
                            var tableName = this.mapping.TableNameForRelationByRelationType[relationType];
                            if (!this.TruncateOrDropTable(connection, tableName))
                            {
                                var primaryKey = false;

                                var sql = new StringBuilder();
                                sql.Append("CREATE TABLE " + tableName + "\n");
                                sql.Append("(\n");
                                sql.Append(Mapping.ColumnNameForAssociation + " " + this.mapping.SqlTypeForObject);

                                if (associationType.IsOne)
                                {
                                    sql.Append(" PRIMARY KEY");
                                    primaryKey = true;
                                }

                                sql.Append(",\n");

                                sql.Append(Mapping.ColumnNameForRole + " " + this.mapping.SqlTypeForObject);

                                if (!primaryKey && roleType.IsOne)
                                {
                                    sql.Append(" PRIMARY KEY");
                                    primaryKey = true;
                                }

                                if (!primaryKey && roleType.IsMany)
                                {
                                    sql.Append(",\nCONSTRAINT PK_" + tableName.Replace(".","_") + " PRIMARY KEY (" + Mapping.ColumnNameForAssociation + "," + Mapping.ColumnNameForRole + ")\n");
                                }

                                sql.Append(")\n");

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
        
        private void CreateProcedures()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var dictionaryEntry in this.mapping.ProcedureDefinitionByName)
                    {
                        var definition = dictionaryEntry.Value;
                        using (var command = new SqlCommand(definition, connection))
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
        }

        private void CreateIndeces()
        {
            using (var connection = new SqlConnection(this.database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // TODO:


                    //foreach (MappingTable table in this.mapping)
                    //{
                    //    foreach (MappingColumn column in table)
                    //    {
                    //        if (column.IndexType != MappingIndexType.None)
                    //        {
                    //            var indexName = "_" + column.Name;

                    //            if (column.IndexType == MappingIndexType.Single)
                    //            {
                    //                var sql = new StringBuilder();
                    //                sql.Append("CREATE INDEX " + indexName + "\n");
                    //                sql.Append("ON " + this.database.SchemaName + "." + table + " (" + column + ")");

                    //                var schemaIndex = this.schema.GetIndex(table.Name, indexName);
                    //                if (schemaIndex == null)
                    //                {
                    //                    using (var command = new SqlCommand(sql.ToString(), connection))
                    //                    {
                    //                        command.ExecuteNonQuery();
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                var sql = new StringBuilder();
                    //                sql.Append("CREATE INDEX " + indexName + "\n");
                    //                sql.Append("ON " + this.database.SchemaName + "." + table + " (" + column + ", " + table.FirstKeyColumn + ")");

                    //                var schemaIndex = this.schema.GetIndex(table.Name, indexName);
                    //                if (schemaIndex == null)
                    //                {
                    //                    using (var command = new SqlCommand(sql.ToString(), connection))
                    //                    {
                    //                        try
                    //                        {
                    //                            command.ExecuteNonQuery();
                    //                        }
                    //                        catch (Exception e)
                    //                        {
                    //                            Console.WriteLine(0);
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private bool TruncateOrDropTable(SqlConnection connection, string tableName)
        {
            if (!this.validation.MissingTableNames.Contains(tableName))
            {
                var table = this.validation.Schema.GetTable(tableName);
                if (this.validation.InvalidTables.Contains(table))
                {
                    using (var command = new SqlCommand("DROP TABLE " + tableName, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    var cmdText = @"TRUNCATE TABLE " + tableName + @";";
                    using (var command = new SqlCommand(cmdText, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
            }

            return false;
        }
    }
}