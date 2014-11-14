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
    using System;
    using System.Collections.Generic;
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
            
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Table Types
                    var tableType = this.schema.GetTableType(Mapping.TableTypeNameForObjects);
                    if (tableType == null)
                    {
                        var cmdText = @"
CREATE TYPE " + this.mapping.Database.SchemaName + "." + Mapping.TableTypeNameForObjects + @" AS TABLE
(" + Mapping.TableTypeColumnNameForObject + " " + this.mapping.SqlTypeForId + @")
";
                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    var tableTypeNames = new HashSet<string> { Mapping.TableTypeNameForObjects };
                    foreach (var relationType in this.mapping.Database.MetaPopulation.RelationTypes)
                    {
                        var tableTypeName = this.mapping.GetTableTypeName(relationType);
                        if (!tableTypeNames.Contains(tableTypeName))
                        {
                            tableType = this.schema.GetTableType(tableTypeName);
                            if (tableType == null)
                            {
                                var tableTypeSqlType = this.mapping.GetTableTypeSqlType(relationType);

                                var cmdText = @"
CREATE TYPE " + this.mapping.Database.SchemaName + "." + tableTypeName + @" AS TABLE
(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.SqlTypeForId + @",
" + Mapping.TableTypeColumnNameForRole + " " + tableTypeSqlType + @")
";
                                using (var command = new SqlCommand(cmdText, connection))
                                {
                                    command.ExecuteNonQuery();
                                }

                            }

                            tableTypeNames.Add(tableTypeName);
                        }
                    }

                    // Objects (table)
                    var table = this.schema.GetTable(Mapping.TableNameForObjects);
                    if (table != null)
                    {
                        var cmdText = @"
TRUNCATE TABLE " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @";
";
                        using (var command = new SqlCommand(cmdText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                         var cmdText = @"
CREATE TABLE " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
(
    " + Mapping.ColumnNameForObject + @" " + this.mapping.SqlTypeForId + @" IDENTITY(1,1),
    " + Mapping.ColumnNameForType + @" " + Mapping.SqlTypeForType + @",
    " + Mapping.ColumnNameForCache + @" " + Mapping.SqlTypeForCache + @",
    PRIMARY KEY (O)
);
";
                         using (var command = new SqlCommand(cmdText, connection))
                         {
                             command.ExecuteNonQuery();
                         }
                    }

                    // Relations (tables)
                    foreach (var relationType in this.mapping.Database.MetaPopulation.RelationTypes)
                    {
                        var tableName = this.mapping.GetTableName(relationType);
                        table = this.schema.GetTable(tableName);

                        if (table != null)
                        {
                            var cmdText = @"
TRUNCATE TABLE " + this.mapping.Database.SchemaName + "." + tableName + @";
";
                            using (var command = new SqlCommand(cmdText, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            var associationType = relationType.AssociationType;
                            var roleType = relationType.RoleType;

                            var sqlTypeForRole = this.mapping.GetSqlType(roleType);

                            var primaryKeys = Mapping.ColumnNameForAssociation;
                            if (roleType.ObjectType is IComposite)
                            {
                                if (associationType.IsMany && roleType.IsMany)
                                {
                                    primaryKeys = Mapping.ColumnNameForAssociation + @" , " + Mapping.ColumnNameForRole;
                                }
                                else if (roleType.IsMany)
                                {
                                    primaryKeys = Mapping.ColumnNameForRole;
                                }
                            }

                            var cmdText = @"
CREATE TABLE " + this.mapping.Database.SchemaName + "." + tableName + @"
(
    " + Mapping.ColumnNameForAssociation + @" " + this.mapping.SqlTypeForId + @",
    " + Mapping.ColumnNameForRole + @" " + sqlTypeForRole + @",
    PRIMARY KEY ( " + primaryKeys + @")
);
";
                            using (var command = new SqlCommand(cmdText, connection))
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
    }
}