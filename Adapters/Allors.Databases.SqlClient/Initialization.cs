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
    using System.Data.SqlClient;
    using System.Text;

    using Allors.Meta;

    internal class Initialization 
    {
        private readonly Mapping mapping;
        private readonly Schema schema;
        private readonly bool useViews;

        internal Initialization(Mapping mapping, Schema schema, bool useViews)
        {
            this.mapping = mapping;
            this.schema = schema;
            this.useViews = useViews;
        }

        internal void Execute()
        {
            this.EnableSnapshotIsolation();
            this.CreateSchema();
            this.CreateTables();
            this.CreateProcedures();

            if (this.useViews)
            {
                this.CreateViews();
            }
            else
            {
                this.DeleteViews();
            }
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

        private void CreateTables()
        {
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
(" + Mapping.TableTypeColumnNameForObject + " " + this.mapping.SqlTypeForObject + @")
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
(" + Mapping.TableTypeColumnNameForAssociation + " " + this.mapping.SqlTypeForObject + @",
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
    " + Mapping.ColumnNameForObject + @" " + this.mapping.SqlTypeForObject + @" IDENTITY(1,1),
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
    " + Mapping.ColumnNameForAssociation + @" " + this.mapping.SqlTypeForObject + @",
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

        private void CreateProcedures()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    // Create Object
                    var procedureName = Mapping.ProcedureNameForCreateObject;
                    var definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForType + @" " + Mapping.SqlTypeForType + @",
    " + Mapping.ParameterNameForCache + @" " + Mapping.SqlTypeForCache + @"
AS 

INSERT INTO " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + " (" + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + @")
OUTPUT INSERTED." + Mapping.ColumnNameForObject + @"
VALUES (" + Mapping.ParameterNameForType + ", " + Mapping.ParameterNameForCache + @");
";

                    this.CreateProcedure(connection, procedureName, definition);

                    // Create Objects
                    procedureName = Mapping.ProcedureNameForCreateObjects;
                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForCount + @" " + Mapping.SqlTypeForCount + @",
    " + Mapping.ParameterNameForType + @" " + Mapping.SqlTypeForType + @",
    " + Mapping.ParameterNameForCache + @" " + Mapping.SqlTypeForCache + @"
AS 

DECLARE @_x TABLE(
    Id " + this.mapping.SqlTypeForObject + @"
);

DECLARE @i INT = 0;

WHILE @i < " + Mapping.ParameterNameForCount + @"
BEGIN

INSERT INTO " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + " (" + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + @")
OUTPUT INSERTED." + Mapping.ColumnNameForObject + @" INTO @_x
VALUES (" + Mapping.ParameterNameForType + ", " + Mapping.ParameterNameForCache + @");

SET @i = @i + 1
END

SELECT Id from @_x;
";

                    this.CreateProcedure(connection, procedureName, definition);
                    
                    // Insert Object
                    procedureName = Mapping.ProcedureNameForInsertObject;
                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForObject + @" " + this.mapping.SqlTypeForObject + @",
    " + Mapping.ParameterNameForType + @" " + Mapping.SqlTypeForType + @",
    " + Mapping.ParameterNameForCache + @" " + Mapping.SqlTypeForCache + @"
AS 

SET IDENTITY_INSERT " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @" ON;

INSERT INTO " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + " (" + Mapping.ColumnNameForObject + ", " + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + @")
VALUES (" + Mapping.ParameterNameForObject + ", " + Mapping.ParameterNameForType + @", " + Mapping.ParameterNameForCache + @");

SET IDENTITY_INSERT " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @" OFF;
";

                    this.CreateProcedure(connection, procedureName, definition);

                    // Fetch Object
                    procedureName = Mapping.ProcedureNameForFetchObjects;
                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForObjectTable + @" " + Mapping.TableTypeNameForObjects + @" READONLY
AS 

SELECT " + Mapping.ColumnNameForObject + ", " + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + @"
FROM " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
WHERE " + Mapping.ColumnNameForObject + @" IN (SELECT " + Mapping.TableTypeColumnNameForObject + " FROM " + Mapping.ParameterNameForObjectTable + @");
";

                    this.CreateProcedure(connection, procedureName, definition);

                    // Delete Objects
                    procedureName = Mapping.ProcedureNameForDeleteObjects;
                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForObjectTable + @" " + Mapping.TableTypeNameForObjects + @" READONLY
AS 

DELETE FROM " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
WHERE " + Mapping.ColumnNameForObject + @" IN (SELECT " + Mapping.TableTypeColumnNameForObject + " FROM " + Mapping.ParameterNameForObjectTable + @");
";

                    this.CreateProcedure(connection, procedureName, definition);

                    // Update Cache Ids
                    procedureName = Mapping.ProcedureNameForUpdateCacheIds;
                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForObjectTable + @" " + Mapping.TableTypeNameForObjects + @" READONLY
AS 

UPDATE " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
SET " + Mapping.ColumnNameForCache + " = " + Mapping.ColumnNameForCache + @" - 1
WHERE " + Mapping.ColumnNameForObject + " IN ( SELECT * FROM " + Mapping.ParameterNameForObjectTable + @");
";

                    this.CreateProcedure(connection, procedureName, definition);

                    foreach (var relationType in this.mapping.Database.MetaPopulation.RelationTypes)
                    {
                        var associationType = relationType.AssociationType;
                        var roleType = relationType.RoleType;

                        if (roleType.ObjectType is IUnit)
                        {
                            // Get Unit Role
                            procedureName = this.mapping.GetProcedureNameForGetRole(relationType);
                            definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForAssociation + @" " + this.mapping.SqlTypeForObject + @"
AS 

SELECT " + Mapping.ColumnNameForRole + @"
FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForAssociation + @"=" + Mapping.ParameterNameForAssociation + @";
";

                            this.CreateProcedure(connection, procedureName, definition);

                            // Set Unit Role
                            procedureName = this.mapping.GetProcedureNameForSetRole(relationType);
                            definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

MERGE " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT * FROM " + Mapping.ParameterNameForRelationTable + @") AS _Y
    ON _X." + Mapping.ColumnNameForAssociation + @" = _Y." + Mapping.TableTypeColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
    SET " + Mapping.ColumnNameForRole + @" = _Y." + Mapping.TableTypeColumnNameForRole + @"
WHEN NOT MATCHED THEN
    INSERT (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
    VALUES(_Y." + Mapping.TableTypeColumnNameForAssociation + @", _Y." + Mapping.TableTypeColumnNameForRole + @");
";

                            this.CreateProcedure(connection, procedureName, definition);
                        }
                        else
                        {
                            if (roleType.IsOne)
                            {
                                // Get Composite Role
                                procedureName = this.mapping.GetProcedureNameForGetRole(relationType);
                                definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForAssociation + @" " + this.mapping.SqlTypeForObject + @"
AS 

SELECT " + Mapping.ColumnNameForRole + @"
FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForAssociation + @"=" + Mapping.ParameterNameForAssociation + @";
";

                                this.CreateProcedure(connection, procedureName, definition);
                            }
                            else
                            {
                                // Get Composite Roles
                                procedureName = this.mapping.GetProcedureNameForGetRole(relationType);
                                definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForAssociation + @" " + this.mapping.SqlTypeForObject + @"
AS 

SELECT " + Mapping.ColumnNameForRole + @"
FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForAssociation + @"=" + Mapping.ParameterNameForAssociation + @";
";

                                this.CreateProcedure(connection, procedureName, definition);
                            }

                            if (associationType.IsOne)
                            {
                                // Get Composite Association
                                procedureName = this.mapping.GetProcedureNameForGetAssociation(relationType);
                                definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRole + @" " + this.mapping.SqlTypeForObject + @"
AS 

SELECT " + Mapping.ColumnNameForAssociation + @"
FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(associationType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @"=" + Mapping.ParameterNameForRole + @";
";

                                this.CreateProcedure(connection, procedureName, definition);
                            }
                            else
                            {
                                // Get Composites Association
                                procedureName = this.mapping.GetProcedureNameForGetAssociation(relationType);
                                definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRole + @" " + this.mapping.SqlTypeForObject + @"
AS 

SELECT " + Mapping.ColumnNameForAssociation + @"
FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(associationType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @"=" + Mapping.ParameterNameForRole + @";
";

                                this.CreateProcedure(connection, procedureName, definition);
                            }

                            switch (relationType.Multiplicity)
                            {
                                case Multiplicity.OneToOne:

                                    // Set Composite Role
                                    procedureName = this.mapping.GetProcedureNameForSetRole(relationType);
                                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

DELETE FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @" IN (SELECT " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @");

MERGE " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT * FROM " + Mapping.ParameterNameForRelationTable + @") AS _Y
    ON _X." + Mapping.ColumnNameForAssociation + @" = _Y." + Mapping.TableTypeColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
    SET " + Mapping.ColumnNameForRole + @" = _Y." + Mapping.TableTypeColumnNameForRole + @"
WHEN NOT MATCHED THEN
    INSERT (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
    VALUES(_Y." + Mapping.TableTypeColumnNameForAssociation + @", _Y." + Mapping.TableTypeColumnNameForRole + @");
";

                                    this.CreateProcedure(connection, procedureName, definition);
                                    
                                    break;

                                case Multiplicity.ManyToOne:

                                    // Set Composite Role
                                    procedureName = this.mapping.GetProcedureNameForSetRole(relationType);
                                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

MERGE " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT * FROM " + Mapping.ParameterNameForRelationTable + @") AS _Y
    ON _X." + Mapping.ColumnNameForAssociation + @" = _Y." + Mapping.TableTypeColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
        SET " + Mapping.ColumnNameForRole + @" = _Y." + Mapping.TableTypeColumnNameForRole + @"
WHEN NOT MATCHED THEN
INSERT (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
VALUES(_Y." + Mapping.TableTypeColumnNameForAssociation + @", _Y." + Mapping.TableTypeColumnNameForRole + @");
";

                                    this.CreateProcedure(connection, procedureName, definition);
                                
                                    break;

                                case Multiplicity.OneToMany:

                                    // Add Composite Role
                                    procedureName = this.mapping.GetProcedureNameForAddRole(relationType);
                                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

DELETE FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @" IN (SELECT " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @");
                    
INSERT INTO " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @" (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
SELECT " + Mapping.TableTypeColumnNameForAssociation + ", " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @";
";

                                    this.CreateProcedure(connection, procedureName, definition);

                                    // Remove Composite Role
                                    procedureName = this.mapping.GetProcedureNameForRemoveRole(relationType);
                                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

DELETE _x
FROM " + (this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType)) + @" AS _x
INNER JOIN " + Mapping.ParameterNameForRelationTable + @" AS _y
ON _x." + Mapping.ColumnNameForAssociation + @"=_y." + Mapping.TableTypeColumnNameForAssociation + @"
WHERE _x." + Mapping.ColumnNameForRole + @" = _y." + Mapping.TableTypeColumnNameForRole + @";
";

                                    this.CreateProcedure(connection, procedureName, definition);

                                    break;
                                case Multiplicity.ManyToMany:

                                    // Add Composite Role
                                    procedureName = this.mapping.GetProcedureNameForAddRole(relationType);
                                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

INSERT INTO " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @" (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
SELECT " + Mapping.TableTypeColumnNameForAssociation + ", " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @";
";

                                    this.CreateProcedure(connection, procedureName, definition);

                                    // Remove Composite Role
                                    procedureName = this.mapping.GetProcedureNameForRemoveRole(relationType);
                                    definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForRelationTable + @" " + this.mapping.GetTableTypeName(relationType) + @" READONLY
AS 

DELETE _x
FROM " + (this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType)) + @" AS _x
INNER JOIN " + Mapping.ParameterNameForRelationTable + @" AS _y
ON _x." + Mapping.ColumnNameForAssociation + @"=_y." + Mapping.TableTypeColumnNameForAssociation + @"
WHERE _x." + Mapping.ColumnNameForRole + @" = _y." + Mapping.TableTypeColumnNameForRole + @";
";

                                    this.CreateProcedure(connection, procedureName, definition);

                                    break;
                                default:
                                    throw new Exception("unknown multiplicity");
                            }
                        }

                        // Delete Role
                        procedureName = this.mapping.GetProcedureNameForDeleteRole(relationType);
                        definition = @"
CREATE PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName + @"
    " + Mapping.ParameterNameForObjectTable + @" " + Mapping.TableTypeNameForObjects + @" READONLY
AS 

DELETE FROM " + this.mapping.Database.SchemaName + "." + this.mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForAssociation + @" IN ( SELECT * FROM " + Mapping.ParameterNameForObjectTable + @");
";

                        this.CreateProcedure(connection, procedureName, definition);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void CreateProcedure(SqlConnection connection, string procedureName, string definition)
        {
            var procedure = this.schema.GetProcedure(procedureName);
            if (procedure != null && !procedure.IsDefinitionCompatible(definition))
            {
                using (var command = new SqlCommand("DROP PROCEDURE " + this.mapping.Database.SchemaName + "." + procedureName, connection))
                {
                    command.ExecuteNonQuery();
                    procedure = null;
                }
            }

            if (procedure == null)
            {
                using (var command = new SqlCommand(definition, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateViews()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var objectType in this.mapping.Database.MetaPopulation.Classes)
                    {
                        var stringBuilder = new StringBuilder();
                        stringBuilder.Append("CREATE VIEW " + this.mapping.Database.SchemaName + ".[" + objectType.Name + "]\n");
                        stringBuilder.Append("AS\n");
                        stringBuilder.Append("SELECT * FROM " + this.mapping.Database.SchemaName + "." + Mapping.TableNameForObjects + "\n");

                    //    var schemaTable = new SchemaTable(this, objectType.SingularName, SchemaTableKind.Object, objectType);
                    //    this.TablesByName.Add(schemaTable.Name, schemaTable);
                    //    this.TableByObjectType.Add(objectType, schemaTable);

                    //    schemaTable.AddColumn(this.ObjectId);
                    //    schemaTable.AddColumn(this.TypeId);

                    //    var roleTypes = new List<RoleType>();
                    //    var associationTypes = new List<AssociationType>();

                    //    var subClassesAndSelf = new List<ObjectType>(objectType.Subclasses) { objectType };

                    //    foreach (var subClass in subClassesAndSelf)
                    //    {
                    //        foreach (var roleType in subClass.ExclusiveRoleTypes)
                    //        {
                    //            if (!roleTypes.Contains(roleType))
                    //            {
                    //                roleTypes.Add(roleType);
                    //            }
                    //        }

                    //        foreach (var associationType in subClass.ExclusiveAssociationTypes)
                    //        {
                    //            if (!associationTypes.Contains(associationType))
                    //            {
                    //                associationTypes.Add(associationType);
                    //            }
                    //        }
                    //    }

                    //    foreach (var associationType in associationTypes)
                    //    {
                    //        var relationType = associationType.RelationTypeWhereAssociationType;
                    //        var roleType = relationType.RoleType;
                    //        if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveRootClasses && roleType.IsMany)
                    //        {
                    //            schemaTable.AddColumn(this.Column(relationType));
                    //        }
                    //    }

                    //    foreach (var roleType in roleTypes)
                    //    {
                    //        var relationType = roleType.RelationTypeWhereRoleType;
                    //        var associationType = relationType.AssociationType;
                    //        if (roleType.ObjectType.IsUnit)
                    //        {
                    //            schemaTable.AddColumn(this.Column(relationType));
                    //        }
                    //        else
                    //        {
                    //            if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveRootClasses && !roleType.IsMany)
                    //            {
                    //                schemaTable.AddColumn(this.Column(relationType));
                    //            }
                    //        }
                    //    }
                    //}

                    //foreach (var relationType in this.Database.Domain.RelationTypes)
                    //{
                    //    var associationType = relationType.AssociationType;
                    //    var roleType = relationType.RoleType;

                    //    if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveRootClasses))
                    //    {
                    //        var schemaTable = new SchemaTable(this, relationType.Name, SchemaTableKind.Relation, relationType);
                    //        this.TablesByName.Add(schemaTable.Name, schemaTable);
                    //        this.TablesByRelationType.Add(relationType, schemaTable);

                    //        schemaTable.AddColumn(this.AssociationId);
                    //        schemaTable.AddColumn(this.Column(relationType));
                    //    }


                        var definition = stringBuilder.ToString();
                        var view = this.schema.GetView(objectType.Name);
                        if (view != null && !view.IsDefinitionCompatible(definition))
                        {
                            using (var command = new SqlCommand("DROP VIEW " + this.mapping.Database.SchemaName + ".[" + objectType.Name + "]", connection))
                            {
                                command.ExecuteNonQuery();
                                view = null;
                            }
                        }

                        if (view == null)
                        {
                            using (var command = new SqlCommand(definition, connection))
                            {
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch
                                {
                                    Console.WriteLine(1);
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

        private void DeleteViews()
        {
            using (var connection = new SqlConnection(this.mapping.Database.ConnectionString))
            {
                connection.Open();
                try
                {
                    foreach (var view in this.schema.ViewByLowercaseViewName.Values)
                    {
                        var cmdText = @"
DROP VIEW " + this.mapping.Database.SchemaName + ".[" + view.Name + @"]
";
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
        }
    }
}