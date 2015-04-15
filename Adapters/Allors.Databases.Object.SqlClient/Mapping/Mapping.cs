//------------------------------------------------------------------------------------------------- 
// <copyright file="cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Allors.Meta;

    public class Mapping : IEnumerable<MappingTable>
    {
        public const string ParamFormat = "@{0}";

        public const string SqlTypeForType = "uniqueidentifier";
        public const string SqlTypeForCache = "int";

        public const string TableNameObjects = "_o";
        public const string TableColumnNameForObject = "o";
        public const string TableColumnNameForType = "t";
        public const string TableColumnNameForCache = "c";
        public const string TableColumnNameForAssociation = "a";
        public const string TableColumnNameForRole = "r";

        internal static readonly string TableTypeParam = string.Format(ParamFormat, "table");

        internal readonly string TableTypeNameForObject;
        internal readonly string TableTypeColumnNameForObject;
        internal readonly string TableTypeNameForCompositeRelation;
        internal readonly string TableTypeNameForStringRelation;
        internal readonly string TableTypeNameForIntegerRelation;
        internal readonly string TableTypeNameForFloatRelation;
        internal readonly string TableTypeNameForBooleanRelation;
        internal readonly string TableTypeNameForDateTimeRelation;
        internal readonly string TableTypeNameForUniqueRelation;
        internal readonly string TableTypeNameForBinaryRelation;
        internal readonly string TableTypeColumnNameForAssociation;
        internal readonly string TableTypeColumnNameForRole;

        internal readonly string TableTypeNamePrefixForDecimalRelation;
        internal readonly Dictionary<int, Dictionary<int, string>> TableTypeNameForDecimalRelationByScaleByPrecision;
       
        internal readonly string ProcedureNameForGetCache;
        internal readonly string ProcedureNameForUpdateCache;
        internal readonly string ProcedureNameForResetCache;
        
        internal readonly Dictionary<IClass, string> ProcedureNameForGetUnitsByClass;
        internal readonly Dictionary<IClass, string> ProcedureNameForLoadObjectByClass;
        internal readonly Dictionary<IClass, string> ProcedureNameForCreateObjectByClass;
        internal readonly Dictionary<IClass, string> ProcedureNameForCreateObjectsByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForSetRoleByRelationTypeByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForGetRoleByRelationTypeByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForGetAssociationByRelationTypeByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForClearRoleByRelationTypeByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForAddRoleByRelationTypeByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForRemoveRoleByRelationTypeByClass;

        internal readonly Dictionary<IRelationType, string> ProcedureNameForSetRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForGetRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForGetAssociationByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForClearRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForAddRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForRemoveRoleByRelationType;


        private const string ProcedurePrefixForGetCache = "gc";
        private const string ProcedurePrefixForUpdateCache = "uc";
        private const string ProcedurePrefixForResetCache = "rc";
        
        private const string ProcedurePrefixForLoad = "l_";
        private const string ProcedurePrefixForCreateObject = "co_";
        private const string ProcedurePrefixForCreateObjects = "cos_";
        private const string ProcedurePrefixForGetUnits = "gu_";
        private const string ProcedurePrefixForGetRole = "gr_";
        private const string ProcedurePrefixForSetRole = "sr_";
        private const string ProcedurePrefixForClearRole = "cr_";
        private const string ProcedurePrefixForAddRole = "ar_";
        private const string ProcedurePrefixForRemoveRole = "rr_";
        private const string ProcedurePrefixForGetAssociation = "ga_";

        private readonly Database database;
        private readonly string sqlTypeForObject;

        private readonly Dictionary<IRelationType, MappingColumn> columnsByRelationType;
        private readonly Dictionary<IRelationType, MappingTable> tableByRelationType;
        private readonly Dictionary<IClass, MappingTable> tableByObjectType;
        private readonly Dictionary<string, MappingTable> tableByName;

        private readonly MappingColumn objectId;
        private readonly MappingColumn associationId;
        private readonly MappingColumn roleId;
        private readonly MappingColumn typeId;
        private readonly MappingColumn cacheId;

        private readonly MappingTable objects;
        private readonly MappingColumn objectsObjectId;
        private readonly MappingColumn objectsTypeId;
        private readonly MappingColumn objectsCacheId;

        private readonly MappingParameter countParam;

        private readonly DbType cacheDbType;
        private readonly DbType typeDbType;

        private readonly Dictionary<string, string> procedureDefinitionByName;

        public Mapping(Database database, string sqlTypeForObject, bool isObjectIdInteger, DbType objectDbType)
        {
            this.database = database;
            this.sqlTypeForObject = sqlTypeForObject;
            this.IsObjectIdInteger = isObjectIdInteger;
            this.ObjectDbType = objectDbType;

            this.TableTypeNameForObject = database.SchemaName + "." + "_t_o";
            this.TableTypeNameForCompositeRelation = database.SchemaName + "." + "_t_c";
            this.TableTypeNameForStringRelation = database.SchemaName + "." + "_t_s";
            this.TableTypeNameForIntegerRelation = database.SchemaName + "." + "_t_i";
            this.TableTypeNameForFloatRelation = database.SchemaName + "." + "_t_f";
            this.TableTypeNameForBooleanRelation = database.SchemaName + "." + "_t_bo";
            this.TableTypeNameForDateTimeRelation = database.SchemaName + "." + "_t_da";
            this.TableTypeNameForUniqueRelation = database.SchemaName + "." + "_t_u";
            this.TableTypeNameForBinaryRelation = database.SchemaName + "." + "_t_bi";
            this.TableTypeNamePrefixForDecimalRelation = database.SchemaName + "." + "_t_de";
            
            this.TableTypeColumnNameForObject = "_o";
            this.TableTypeColumnNameForAssociation = "_a";
            this.TableTypeColumnNameForRole = "_r";

            this.TableTypeNameForDecimalRelationByScaleByPrecision = new Dictionary<int, Dictionary<int, string>>();
            foreach (var relationType in database.MetaPopulation.RelationTypes)
            {
                var roleType = relationType.RoleType;
                if (roleType.ObjectType.IsUnit && ((IUnit)roleType.ObjectType).IsDecimal)
                {
                    var precision = roleType.Precision;
                    var scale = roleType.Scale;

                    var tableName = this.TableTypeNamePrefixForDecimalRelation + precision + "_" + scale;

                    // table
                    Dictionary<int, string> decimalRelationTableByScale;
                    if (!this.TableTypeNameForDecimalRelationByScaleByPrecision.TryGetValue(precision.Value, out decimalRelationTableByScale))
                    {
                        decimalRelationTableByScale = new Dictionary<int, string>();
                        this.TableTypeNameForDecimalRelationByScaleByPrecision[precision.Value] = decimalRelationTableByScale;
                    }

                    if (!decimalRelationTableByScale.ContainsKey(scale.Value))
                    {
                        decimalRelationTableByScale[scale.Value] = tableName;
                    }
                }
            }

            this.countParam = new MappingParameter(this, "count", DbType.Int32);

            this.typeDbType = DbType.Guid;
            this.cacheDbType = DbType.Int32;

            this.tableByName = new Dictionary<string, MappingTable>();
            this.tableByObjectType = new Dictionary<IClass, MappingTable>();
            this.tableByRelationType = new Dictionary<IRelationType, MappingTable>();

            this.columnsByRelationType = new Dictionary<IRelationType, MappingColumn>();

            this.objectId = new MappingColumn(this, TableColumnNameForObject, this.ObjectDbType, false, true, MappingIndexType.None);
            this.cacheId = new MappingColumn(this, TableColumnNameForCache, this.CacheDbType, false, false, MappingIndexType.None);
            this.typeId = new MappingColumn(this, TableColumnNameForType, this.TypeDbType, false, false, MappingIndexType.None);

            this.associationId = new MappingColumn(this, TableColumnNameForAssociation, this.ObjectDbType, false, true, MappingIndexType.None);
            this.roleId = new MappingColumn(this, TableColumnNameForRole, this.ObjectDbType, false, true, MappingIndexType.None);

            // Objects
            this.objects = new MappingTable(this, TableNameObjects);
            this.objectsObjectId = new MappingColumn(this, this.ObjectId.Name, this.ObjectDbType, true, true, MappingIndexType.None);
            this.objectsCacheId = new MappingColumn(this, this.CacheId.Name, this.CacheDbType, false, false, MappingIndexType.None);
            this.objectsTypeId = new MappingColumn(this, this.TypeId.Name, this.TypeDbType, false, false, MappingIndexType.None);

            this.Objects.AddColumn(this.ObjectsObjectId);
            this.Objects.AddColumn(this.ObjectsTypeId);
            this.Objects.AddColumn(this.ObjectsCacheId);
            this.tableByName.Add(this.Objects.Name, this.Objects);

            this.CreateTables();

            // Procedures
            this.procedureDefinitionByName = new Dictionary<string, string>();

            // Get Cache Ids
            this.ProcedureNameForGetCache = this.Database.SchemaName + "." + ProcedurePrefixForGetCache;
            var definition =
@"CREATE PROCEDURE " + this.ProcedureNameForGetCache + @"
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    SELECT " + this.ObjectId + ", " + this.CacheId + @"
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + TableTypeParam + ")";
            this.procedureDefinitionByName.Add(this.ProcedureNameForGetCache, definition);

            // Update Cache Ids
            this.ProcedureNameForUpdateCache = this.Database.SchemaName + "." + ProcedurePrefixForUpdateCache;
            definition =
@"CREATE PROCEDURE " + this.ProcedureNameForUpdateCache + @"
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    UPDATE " + this.Objects + @"
    SET " + this.CacheId + " = " + this.CacheId + @" - 1
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN ( SELECT " + this.TableTypeColumnNameForObject + " FROM " + TableTypeParam + ");\n\n";
            this.procedureDefinitionByName.Add(this.ProcedureNameForUpdateCache, definition);

            // Reset Cache Ids
            this.ProcedureNameForResetCache = this.Database.SchemaName + "." + ProcedurePrefixForResetCache;
            definition =
@"CREATE PROCEDURE " + this.ProcedureNameForResetCache + @"
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    UPDATE " + this.Objects + @"
    SET " + this.CacheId + " = " + Reference.InitialCacheId + @"
    FROM " + this.Objects + @"
    WHERE " + this.ObjectId + " IN ( SELECT " + this.TableTypeColumnNameForObject + " FROM " + TableTypeParam + ");\n\n";
            this.procedureDefinitionByName.Add(this.ProcedureNameForResetCache, definition);

            this.ProcedureNameForGetUnitsByClass = new Dictionary<IClass, string>();
            foreach (var @class in this.Database.MetaPopulation.Classes)
            {
                var className = @class.Name.ToLowerInvariant();

                var sortedUnitRoleTypes = this.Database.GetSortedUnitRolesByIObjectType(@class);
                if (sortedUnitRoleTypes.Length > 0)
                {
                    // Get Unit Roles
                    this.ProcedureNameForGetUnitsByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForGetUnits + className);
                    definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetUnitsByClass[@class] + @"
    " + this.ObjectId.Param + " AS " + this.GetSqlType(this.ObjectId) + @"
AS 
    SELECT ";
                    var first = true;
                    foreach (var role in sortedUnitRoleTypes)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            definition += ", ";
                        }

                        definition += this.ColumnsByRelationType[role.RelationType];
                    }

definition += @"
    FROM " + this.Table(@class.ExclusiveLeafClass) + @"
    WHERE " + this.ObjectId + "=" + this.ObjectId.Param;
                    this.procedureDefinitionByName.Add(this.ProcedureNameForGetUnitsByClass[@class], definition);
                }
            }

            this.ProcedureNameForLoadObjectByClass = new Dictionary<IClass, string>();
            this.ProcedureNameForCreateObjectByClass = new Dictionary<IClass, string>();
            this.ProcedureNameForCreateObjectsByClass = new Dictionary<IClass, string>();

            this.ProcedureNameForSetRoleByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();
            this.ProcedureNameForGetRoleByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();
            this.ProcedureNameForGetAssociationByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();
            this.ProcedureNameForClearRoleByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();
            this.ProcedureNameForAddRoleByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();
            this.ProcedureNameForRemoveRoleByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();

            foreach (var dictionaryEntry in this.TableByObjectType)
            {
                var @class = dictionaryEntry.Key;
                var table = dictionaryEntry.Value;
                var className = @class.Name.ToLowerInvariant();

                // Load Objects
                this.ProcedureNameForLoadObjectByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForLoad + className);
                definition =
@"CREATE PROCEDURE " + this.ProcedureNameForLoadObjectByClass[@class] + @"
    " + this.TypeId.Param + @" " + this.GetSqlType(this.TypeId) + @",
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS
    INSERT INTO " + table + " (" + this.TypeId + ", " + this.ObjectId + @")
    SELECT " + this.TypeId.Param + @", " + this.TableTypeColumnNameForObject + @"
    FROM " + TableTypeParam + "\n";
                this.procedureDefinitionByName.Add(this.ProcedureNameForLoadObjectByClass[@class], definition);

                // CreateObject
                this.ProcedureNameForCreateObjectByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForCreateObject + className);
                definition =
@"CREATE PROCEDURE " + this.ProcedureNameForCreateObjectByClass[@class] + @"
" + this.TypeId.Param + @" " + this.GetSqlType(this.TypeId) + @"
AS 
DECLARE  " + this.ObjectId.Param + " AS " + this.GetSqlType(this.ObjectId) + @"

INSERT INTO " + this.Objects + " (" + this.TypeId + ", " + this.CacheId + @")
VALUES (" + this.TypeId.Param + ", " + Reference.InitialCacheId + @");

SELECT " + this.ObjectId.Param + @" = SCOPE_IDENTITY();

INSERT INTO " + table + " (" + this.ObjectId + "," + this.TypeId + @")
VALUES (" + this.ObjectId.Param + "," + this.TypeId.Param + @");

SELECT " + this.ObjectId.Param + @";";
                this.procedureDefinitionByName.Add(this.ProcedureNameForCreateObjectByClass[@class], definition);

                // CreateObjects
                this.ProcedureNameForCreateObjectsByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForCreateObjects + className);
                definition =
                    @"CREATE PROCEDURE " + this.ProcedureNameForCreateObjectsByClass[@class] + @"
" + this.TypeId.Param + @" " + this.GetSqlType(this.TypeId) + @",
" + this.CountParam + @" " + this.GetSqlType(this.CountParam.DbType) + @"
AS 
BEGIN
DECLARE @IDS TABLE (id INT);
DECLARE @O INT, @COUNTER INT

SET @COUNTER = 0
WHILE @COUNTER < " + this.CountParam + @"
    BEGIN

    INSERT INTO " + this.Objects.StatementName + " (" + this.TypeId + ", " + this.CacheId + @")
    VALUES (" + this.TypeId.Param + ", " + Reference.InitialCacheId + @" );

    INSERT INTO @IDS(id)
    VALUES (SCOPE_IDENTITY());

    SET @COUNTER = @COUNTER+1;
    END

INSERT INTO " + this.Table(@class.ExclusiveLeafClass) + " (" + this.ObjectId + "," + this.TypeId + @")
SELECT ID," + this.TypeId.Param + @" FROM @IDS;

SELECT id FROM @IDS;
END";
                this.procedureDefinitionByName.Add(this.ProcedureNameForCreateObjectsByClass[@class], definition);

                var procedureNameForSetRoleByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForSetRoleByRelationTypeByClass.Add(@class, procedureNameForSetRoleByRelationType);

                var procedureNameForGetRoleByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForGetRoleByRelationTypeByClass.Add(@class, procedureNameForGetRoleByRelationType);

                var procedureNameForGetAssociationByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForGetAssociationByRelationTypeByClass.Add(@class, procedureNameForGetAssociationByRelationType);

                var procedureNameForClearRoleByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForClearRoleByRelationTypeByClass.Add(@class, procedureNameForClearRoleByRelationType);

                var procedureNameForAddRoleByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForAddRoleByRelationTypeByClass.Add(@class, procedureNameForAddRoleByRelationType);

                var procedureNameForRemoveRoleByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForRemoveRoleByRelationTypeByClass.Add(@class, procedureNameForRemoveRoleByRelationType);

                foreach (MappingColumn column in table)
                {
                    var relationType = column.RelationType;
                    if (relationType != null)
                    {
                        var roleType = relationType.RoleType;
                        var associationType = relationType.AssociationType;

                        var relationTypeName = roleType.SingularFullName.ToLowerInvariant();

                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            procedureNameForSetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForSetRole + className + "_" + relationTypeName);

                            var unitTypeTag = ((IUnit)relationType.RoleType.ObjectType).UnitTag;
                            switch (unitTypeTag)
                            {
                                case UnitTags.AllorsString:
                                    // Set String Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForStringRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsInteger:
                                    // Set Integer Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForIntegerRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsFloat:
                                    // Set Double Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForFloatRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsDecimal:
                                    // Set Decimal Role
                                    var decimalRelationTable = this.TableTypeNameForDecimalRelationByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];

definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
" + TableTypeParam + @" " + decimalRelationTable + @" READONLY
AS 
UPDATE " + table + @"
SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
FROM " + table + @"
INNER JOIN " + TableTypeParam + @" AS r
ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsBoolean:
                                    // Set Boolean Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForBooleanRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsDateTime:
                                    // Set DateTime Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForDateTimeRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsUnique:
                                    // Set Unique Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForUniqueRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                case UnitTags.AllorsBinary:
                                    // Set Binary Role
definition = "CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForBinaryRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                    break;

                                default:
                                    throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.SingularName);
                            }

                            this.procedureDefinitionByName.Add(procedureNameForSetRoleByRelationType[relationType], definition);
                        }
                        else
                        {
                            procedureNameForGetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetRole + className + "_" + relationTypeName);
                            procedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + className + "_" + relationTypeName);
                            procedureNameForClearRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForClearRole + className + "_" + relationTypeName);

                            if (roleType.IsOne)
                            {
                                // Get Composite Role (1-1 and *-1) [object table]
                                definition =
@"CREATE PROCEDURE " + procedureNameForGetRoleByRelationType[relationType] + @"
    " + this.AssociationId.Param + @" " + this.GetSqlType(this.AssociationId) + @"
AS 
    SELECT " + this.Column(roleType) + @"
    FROM " + table + @"
    WHERE " + this.ObjectId + "=" + this.AssociationId.Param;
                                this.procedureDefinitionByName.Add(procedureNameForGetRoleByRelationType[relationType], definition);

                                if (associationType.IsOne)
                                {
                                    // Get Composite Association (1-1) [object table]
                                    definition =
@"CREATE PROCEDURE " + procedureNameForGetAssociationByRelationType[relationType] + @"
    " + this.RoleId.Param + @" " + this.GetSqlType(this.RoleId) + @"
AS 
    SELECT " + this.ObjectId + @"
    FROM " + table + @"
    WHERE " + this.Column(roleType) + "=" + this.RoleId.Param;
                                    this.procedureDefinitionByName.Add(procedureNameForGetAssociationByRelationType[relationType], definition);
                                }
                                else
                                {
                                    // Get Composite Association (*-1) [object table]
                                    definition =
@"CREATE PROCEDURE " + procedureNameForGetAssociationByRelationType[relationType] + @"
    " + this.RoleId.Param + @" " + this.GetSqlType(this.RoleId) + @"
AS 
    SELECT " + this.ObjectId + @"
    FROM " + table + @"
    WHERE " + this.Column(roleType) + "=" + this.RoleId.Param;
                                    this.procedureDefinitionByName.Add(procedureNameForGetAssociationByRelationType[relationType], definition);
                                }

                                // Set Composite Role (1-1 and *-1) [object table]
                                procedureNameForSetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForSetRole + className + "_" + relationTypeName);
                                definition =
@"CREATE PROCEDURE " + procedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                this.procedureDefinitionByName.Add(procedureNameForSetRoleByRelationType[relationType], definition);

                                // Clear Composite Role (1-1 and *-1) [object table]
                                definition =
@"CREATE PROCEDURE " + procedureNameForClearRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.Column(roleType) + @" = null
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS a
    ON " + this.ObjectId + " = a." + this.TableTypeColumnNameForObject;
                                this.procedureDefinitionByName.Add(procedureNameForClearRoleByRelationType[relationType], definition);
                            }
                            else
                            {
                                // Get Composites Role (1-*) [object table]
                                definition =
@"CREATE PROCEDURE " + procedureNameForGetRoleByRelationType[relationType] + @"
    " + this.AssociationId.Param + @" " + this.GetSqlType(this.AssociationId) + @"
AS
    SELECT " + this.ObjectId + @"
    FROM " + table + @"
    WHERE " + this.Column(associationType) + "=" + this.AssociationId.Param;
                                this.procedureDefinitionByName.Add(procedureNameForGetRoleByRelationType[relationType], definition);

                                if (associationType.IsOne)
                                {
                                    // Get Composite Association (1-*) [object table]
                                    definition =
@"CREATE PROCEDURE " + procedureNameForGetAssociationByRelationType[relationType] + @"
    " + this.RoleId.Param + @" " + this.GetSqlType(this.RoleId) + @"
AS
    SELECT " + this.Column(associationType) + @"
    FROM " + table + @"
    WHERE " + this.ObjectId + "=" + this.RoleId.Param;
                                    this.procedureDefinitionByName.Add(procedureNameForGetAssociationByRelationType[relationType], definition);
                                }

                                // Add Composite Role (1-*) [object table]
                                procedureNameForAddRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForAddRole + className + "_" + relationTypeName);
                                definition =
@"CREATE PROCEDURE " + procedureNameForAddRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    UPDATE " + table + @"
    SET " + this.Column(associationType) + " = r." + this.TableTypeColumnNameForAssociation + @"
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.ObjectId + " = r." + this.TableTypeColumnNameForRole;
                                this.procedureDefinitionByName.Add(procedureNameForAddRoleByRelationType[relationType], definition);

                                // Remove Composite Role (1-*) [object table]
                                procedureNameForRemoveRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForRemoveRole + className + "_" + relationTypeName);
                                definition =
@"CREATE PROCEDURE " + procedureNameForRemoveRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    UPDATE " + table + @"
    SET " + this.Column(associationType) + @" = null
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS r
    ON " + this.Column(associationType) + " = r." + this.TableTypeColumnNameForAssociation + @" AND
    " + this.ObjectId + " = r." + this.TableTypeColumnNameForRole;
                                this.procedureDefinitionByName.Add(procedureNameForRemoveRoleByRelationType[relationType], definition);

                                // Clear Composites Role (1-*) [object table]
                                definition =
@"CREATE PROCEDURE " + procedureNameForClearRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS 

    UPDATE " + table + @"
    SET " + this.Column(associationType) + @" = null
    FROM " + table + @"
    INNER JOIN " + TableTypeParam + @" AS a
    ON " + this.Column(associationType) + " = a." + this.TableTypeColumnNameForObject;
                                this.procedureDefinitionByName.Add(procedureNameForClearRoleByRelationType[relationType], definition);
                            }
                        }
                    }
                }
            }

            this.ProcedureNameForSetRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForGetRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForGetAssociationByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForClearRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForAddRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForRemoveRoleByRelationType = new Dictionary<IRelationType, string>();

            foreach (var dictionaryEntry in this.TableByRelationType)
            {
                var relationType = dictionaryEntry.Key;
                var roleType = relationType.RoleType;
                var associationType = relationType.AssociationType;
                var table = dictionaryEntry.Value;

                var relationTypeName = roleType.SingularFullName.ToLowerInvariant();

                this.ProcedureNameForGetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetRole + relationTypeName);
                this.ProcedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + relationTypeName);
                this.ProcedureNameForClearRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForClearRole + relationTypeName);

                if (roleType.IsMany)
                {
                    // Get Composites Role (1-* and *-*) [relation table]
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForGetRoleByRelationType[relationType] + @"
    " + this.AssociationId.Param + @" " + this.GetSqlType(this.AssociationId) + @",
    " + this.RoleId.Param + @" " + this.TableTypeNameForObject + @" READONLY
AS
    SELECT " + this.RoleId + @"
    FROM " + this.Table(roleType) + @"
    WHERE " + this.AssociationId + "=" + this.AssociationId.Param;
                    this.procedureDefinitionByName.Add(this.ProcedureNameForGetRoleByRelationType[relationType], definition);

                    // Add Composite Role (1-* and *-*) [relation table]
                    this.ProcedureNameForAddRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForAddRole + relationTypeName);
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForAddRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    INSERT INTO " + table + " (" + this.AssociationId + "," + this.RoleId + @")
    SELECT " + this.TableTypeColumnNameForAssociation + @", " + this.TableTypeColumnNameForRole + @"
    FROM " + TableTypeParam + "\n";
                    this.procedureDefinitionByName.Add(this.ProcedureNameForAddRoleByRelationType[relationType], definition);

                    // Remove Composite Role (1-* and *-*) [relation table]
                    this.ProcedureNameForRemoveRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForRemoveRole + relationTypeName);
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForRemoveRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    DELETE T 
    FROM " + table + @" T
    INNER JOIN " + TableTypeParam + @" R
    ON T." + this.AssociationId + " = R." + this.TableTypeColumnNameForAssociation + @"
    AND T." + this.RoleId + " = R." + this.TableTypeColumnNameForRole + @";";
                    this.procedureDefinitionByName.Add(this.ProcedureNameForRemoveRoleByRelationType[relationType], definition);
                }
                else
                {
                    // Get Composite Role (1-1 and *-1) [relation table]
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForGetRoleByRelationType[relationType] + @"
    " + this.AssociationId.Param + @" " + this.GetSqlType(this.AssociationId) + @"
AS
    SELECT " + this.RoleId + @"
    FROM " + this.Table(roleType) + @"
    WHERE " + this.AssociationId + "=" + this.AssociationId.Param;
                    this.procedureDefinitionByName.Add(this.ProcedureNameForGetRoleByRelationType[relationType], definition);

                    // Set Composite Role (1-1 and *-1) [relation table]
                    this.ProcedureNameForSetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForSetRole + relationTypeName);
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForSetRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    MERGE " + table + @" T
    USING " + TableTypeParam + @" AS r
    ON T." + this.AssociationId + @" = r." + this.TableTypeColumnNameForAssociation + @"

    WHEN MATCHED THEN
    UPDATE SET " + this.RoleId + @"= r." + this.TableTypeColumnNameForRole + @"

    WHEN NOT MATCHED THEN
    INSERT (" + this.AssociationId + "," + this.RoleId + @")
    VALUES (r." + this.TableTypeColumnNameForAssociation + ", r." + this.TableTypeColumnNameForRole + @");";
                    this.procedureDefinitionByName.Add(this.ProcedureNameForSetRoleByRelationType[relationType], definition);
                }

                if (associationType.IsOne)
                {
                    // Get Composite Association (1-1) [relation table]
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + this.RoleId.Param + @" " + this.GetSqlType(this.RoleId) + @"
AS
    SELECT " + this.AssociationId + @"
    FROM " + table + @"
    WHERE " + this.RoleId + "=" + this.RoleId.Param;
                    this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);
                }
                else
                {
                    // Get Composite Association (*-1) [relation table]
                    definition =
@"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + this.RoleId.Param + @" " + this.GetSqlType(this.RoleId) + @"
AS
    SELECT " + this.AssociationId + @"
    FROM " + table + @"
    WHERE " + this.RoleId + "=" + this.RoleId.Param;
                    this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);
                }

                // Clear Composite Role (1-1 and *-1) [relation table]
                definition =
@"CREATE PROCEDURE " + this.ProcedureNameForClearRoleByRelationType[relationType] + @"
    " + TableTypeParam + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    DELETE T 
    FROM " + table + @" T
    INNER JOIN " + TableTypeParam + @" A
    ON T." + this.AssociationId + " = A." + this.TableTypeColumnNameForObject;
                this.procedureDefinitionByName.Add(this.ProcedureNameForClearRoleByRelationType[relationType], definition);
            }
        }

        public string SqlTypeForObject
        {
            get
            {
                return this.sqlTypeForObject;
            }
        }

        public bool IsObjectIdInteger { get; private set; }

        public Dictionary<IRelationType, MappingColumn> ColumnsByRelationType
        {
            get { return this.columnsByRelationType; }
        }

        public Dictionary<IRelationType, MappingTable> TableByRelationType
        {
            get { return this.tableByRelationType; }
        }

        public Dictionary<IClass, MappingTable> TableByObjectType
        {
            get { return this.tableByObjectType; }
        }

        /// <summary>
        /// Gets the parameter to pass a count to.
        /// <example>
        /// Is used in CreateObjects to denote the amount of objects to create.
        /// </example>
        /// </summary>
        internal MappingParameter CountParam
        {
            get { return this.countParam; }
        }

        internal MappingColumn TypeId
        {
            get { return this.typeId; }
        }

        internal MappingColumn CacheId
        {
            get { return this.cacheId; }
        }

        internal MappingColumn AssociationId
        {
            get { return this.associationId; }
        }

        internal MappingColumn RoleId
        {
            get { return this.roleId; }
        }

        internal MappingColumn ObjectId
        {
            get { return this.objectId; }
        }

        internal MappingTable Objects
        {
            get { return this.objects; }
        }

        protected internal Database Database
        {
            get { return this.database; }
        }

        private Dictionary<string, MappingTable> TableByName
        {
            get { return this.tableByName; }
        }

        private DbType TypeDbType
        {
            get { return this.typeDbType; }
        }

        private DbType CacheDbType
        {
            get { return this.cacheDbType; }
        }

        private DbType ObjectDbType { get; set; }

        private MappingColumn ObjectsObjectId
        {
            get { return this.objectsObjectId; }
        }

        private MappingColumn ObjectsTypeId
        {
            get { return this.objectsTypeId; }
        }

        private MappingColumn ObjectsCacheId
        {
            get { return this.objectsCacheId; }
        }

        public Dictionary<string, string> ProcedureDefinitionByName
        {
            get
            {
                return this.procedureDefinitionByName;
            }
        }

        public string GetTableName(IClass @class)
        {
            return @class.Name;
        }

        public string GetTableName(IRelationType relationType)
        {
            return relationType.RoleType.SingularFullName;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<MappingTable>)this).GetEnumerator();
        }

        IEnumerator<MappingTable> IEnumerable<MappingTable>.GetEnumerator()
        {
            return this.tableByName.Values.GetEnumerator();
        }

        internal MappingColumn Column(IRelationType relationType)
        {
            return this.columnsByRelationType[relationType];
        }

        internal MappingColumn Column(IAssociationType association)
        {
            return this.columnsByRelationType[association.RelationType];
        }

        internal MappingColumn Column(IRoleType role)
        {
            return this.columnsByRelationType[role.RelationType];
        }

        internal MappingTable Table(IClass @class)
        {
            return this.tableByObjectType[@class];
        }

        internal MappingTable Table(IRelationType relationType)
        {
            return this.tableByRelationType[relationType];
        }

        internal MappingTable Table(IAssociationType association)
        {
            return this.tableByRelationType[association.RelationType];
        }

        internal MappingTable Table(IRoleType role)
        {
            return this.tableByRelationType[role.RelationType];
        }

        internal string EscapeIfReserved(string name)
        {
            if (ReservedWords.Names.Contains(name.ToLowerInvariant()))
            {
                return "[" + name + "]";
            }

            return name;
        }

        internal string GetSqlType(IRoleType roleType)
        {
            var unit = (IUnit)roleType.ObjectType;
            switch (unit.UnitTag)
            {
                case UnitTags.AllorsString:
                    if (roleType.Size == -1 || roleType.Size > 4000)
                    {
                        return "nvarchar(max)";
                    }

                    return "nvarchar(" + roleType.Size + ")";
                case UnitTags.AllorsInteger:
                    return "int";
                case UnitTags.AllorsDecimal:
                    return "decimal(" + roleType.Precision + "," + roleType.Scale + ")";
                case UnitTags.AllorsFloat:
                    return "float";
                case UnitTags.AllorsBoolean:
                    return "bit";
                case UnitTags.AllorsDateTime:
                    return "datetime2";
                case UnitTags.AllorsUnique:
                    return "uniqueidentifier";
                case UnitTags.AllorsBinary:
                    if (roleType.Size == -1 || roleType.Size > 8000)
                    {
                        return "varbinary(max)";
                    }

                    return "varbinary(" + roleType.Size + ")";
                default:
                    return "!UNKNOWN VALUE TYPE!";
            }
        }

        internal string GetSqlType(MappingColumn column)
        {
            switch (column.DbType)
            {
                case DbType.String:
                    if (column.Size == -1 || column.Size > 4000)
                    {
                        return "NVARCHAR(MAX) ";
                    }

                    return "NVARCHAR(" + column.Size + ") ";
                case DbType.Int32:
                    return "INT ";
                case DbType.Decimal:
                    return "DECIMAL(" + column.Precision + "," + column.Scale + ") ";
                case DbType.Double:
                    return "FLOAT ";
                case DbType.Boolean:
                    return "BIT ";
                case DbType.DateTime2:
                    return "DATETIME2 ";
                case DbType.Guid:
                    return "UNIQUEIDENTIFIER ";
                case DbType.Binary:
                    if (column.Size == -1 || column.Size > 8000)
                    {
                        return "VARBINARY(MAX) ";
                    }

                    return "VARBINARY(" + column.Size + ") ";
                default:
                    return "!UNKNOWN VALUE TYPE!";
            }
        }

        internal string GetSqlType(DbType type)
        {
            switch (type)
            {
                case DbType.Int32:
                    return "INT ";
                default:
                    return "!UNKNOWN DBTYPE!";
            }
        }

        private DbType GetDbType(IRoleType role)
        {
            var unitTypeTag = ((IUnit)role.ObjectType).UnitTag;
            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    return DbType.String;
                case UnitTags.AllorsInteger:
                    return DbType.Int32;
                case UnitTags.AllorsFloat:
                    return DbType.Double;
                case UnitTags.AllorsDecimal:
                    return DbType.Decimal;
                case UnitTags.AllorsBoolean:
                    return DbType.Boolean;
                case UnitTags.AllorsDateTime:
                    return DbType.DateTime2;
                case UnitTags.AllorsUnique:
                    return DbType.Guid;
                case UnitTags.AllorsBinary:
                    return DbType.Binary;
                default:
                    throw new ArgumentException("Unkown unit type " + role.ObjectType);
            }
        }

        private void CreateTables()
        {
            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;

                if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveLeafClasses))
                {
                    var column = new MappingColumn(this, "R", this.ObjectDbType, false, true, relationType.IsIndexed ? MappingIndexType.Combined : MappingIndexType.None, relationType);
                    this.ColumnsByRelationType.Add(relationType, column);
                }
                else
                {
                    if (roleType.ObjectType.IsUnit)
                    {
                        var size = roleType.Size;
                        var precision = roleType.Precision;
                        var scale = roleType.Scale;

                        var index = relationType.IsIndexed ? MappingIndexType.Single : MappingIndexType.None;
                        var unit = (IUnit)roleType.ObjectType;
                        if (unit.IsBinary || unit.IsString)
                        {
                            if (roleType.Size == -1 || roleType.Size > 8000)
                            {
                                index = MappingIndexType.None;
                            }
                        }

                        var column = new MappingColumn(this, roleType.SingularPropertyName, this.GetDbType(roleType), false, false, index, relationType, size, precision, scale);
                        this.ColumnsByRelationType.Add(relationType, column);
                    }
                    else if (relationType.ExistExclusiveLeafClasses)
                    {
                        if (roleType.IsOne)
                        {
                            var column = new MappingColumn(this, roleType.SingularPropertyName, this.ObjectDbType, false, false, relationType.IsIndexed ? MappingIndexType.Combined : MappingIndexType.None, relationType);
                            this.ColumnsByRelationType.Add(relationType, column);
                        }
                        else
                        {
                            var column = new MappingColumn(this, associationType.SingularPropertyName, this.ObjectDbType, false, false, relationType.IsIndexed ? MappingIndexType.Combined : MappingIndexType.None, relationType);
                            this.ColumnsByRelationType.Add(relationType, column);
                        }
                    }
                }
            }

            foreach (IClass objectType in this.Database.MetaPopulation.Classes)
            {
                var schemaTable = new MappingTable(this, objectType.SingularName);
                this.TableByName.Add(schemaTable.Name, schemaTable);
                this.TableByObjectType.Add(objectType, schemaTable);

                schemaTable.AddColumn(this.ObjectId);
                schemaTable.AddColumn(this.TypeId);

                foreach (var associationType in objectType.AssociationTypes)
                {
                    var relationType = associationType.RelationType;
                    var roleType = relationType.RoleType;
                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses && roleType.IsMany)
                    {
                        schemaTable.AddColumn(this.Column(relationType));
                    }
                }

                foreach (var roleType in objectType.RoleTypes)
                {
                    var relationType = roleType.RelationType;
                    var associationType = relationType.AssociationType;
                    if (roleType.ObjectType.IsUnit)
                    {
                        schemaTable.AddColumn(this.Column(relationType));
                    }
                    else
                    {
                        if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveLeafClasses && !roleType.IsMany)
                        {
                            schemaTable.AddColumn(this.Column(relationType));
                        }
                    }
                }
            }

            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;

                if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveLeafClasses))
                {
                    var schemaTable = new MappingTable(this, relationType.RoleType.SingularFullName);
                    this.TableByName.Add(schemaTable.Name, schemaTable);
                    this.TableByRelationType.Add(relationType, schemaTable);

                    schemaTable.AddColumn(this.AssociationId);
                    schemaTable.AddColumn(this.Column(relationType));
                }
            }
        }
    }
}