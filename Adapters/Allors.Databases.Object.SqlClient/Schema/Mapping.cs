//------------------------------------------------------------------------------------------------- 
// <copyright file="Mapping.cs" company="Allors bvba">
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
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    using Allors.Meta;

    public class Mapping
    {
        public const string ParamFormat = "@{0}";

        public const string ColumnNameForObject = "o";
        public const string ColumnNameForType = "t";
        public const string ColumnNameForCache = "c";
        public const string ColumnNameForAssociation = "a";
        public const string ColumnNameForRole = "r";

        public const string SqlTypeForType = "uniqueidentifier";
        public const string SqlTypeForCache = "int";
        public const string SqlTypeForCount = "int";

        public const SqlDbType SqlDbTypeForType = SqlDbType.UniqueIdentifier;
        public const SqlDbType SqlDbTypeForCache = SqlDbType.Int;
        public const SqlDbType SqlDbTypeForCount = SqlDbType.Int;

        public readonly string SqlTypeForObject;
        public readonly SqlDbType SqlDbTypeForObject;

        public readonly Dictionary<IRoleType, string> ParamNameByRoleType;

        internal static readonly string ParamNameForObject = string.Format(ParamFormat, ColumnNameForObject);
        internal static readonly string ParamNameForType = string.Format(ParamFormat, ColumnNameForType);
        internal static readonly string ParamNameForCache = string.Format(ParamFormat, ColumnNameForCache);
        internal static readonly string ParamNameForAssociation = string.Format(ParamFormat, ColumnNameForAssociation);
        internal static readonly string ParamNameForRole = string.Format(ParamFormat, ColumnNameForRole);
        internal static readonly string ParamNameForCount = string.Format(ParamFormat, "count");
        internal static readonly string ParamNameForTableType = string.Format(ParamFormat, "table");

        internal readonly string TableNameForObjects;

        internal readonly Dictionary<IClass, string> TableNameForObjectByClass;
        internal readonly Dictionary<IRelationType, string> ColumnNameByRelationType;
        internal readonly Dictionary<IRelationType, string> TableNameForRelationByRelationType;

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
        
        internal readonly Dictionary<IClass, string> ProcedureNameForLoadObjectByClass;
        internal readonly Dictionary<IClass, string> ProcedureNameForCreateObjectByClass;
        internal readonly Dictionary<IClass, string> ProcedureNameForCreateObjectsByClass;

        internal readonly Dictionary<IClass, string> ProcedureNameForGetUnitRolesByClass;
        internal readonly Dictionary<IClass, string> ProcedureNameForPrefetchUnitRolesByClass;
        internal readonly Dictionary<IClass, Dictionary<IRelationType, string>> ProcedureNameForSetUnitRoleByRelationTypeByClass;
        
        internal readonly Dictionary<IRelationType, string> ProcedureNameForGetRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForPrefetchRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForSetRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForAddRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForRemoveRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForClearRoleByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForGetAssociationByRelationType;
        internal readonly Dictionary<IRelationType, string> ProcedureNameForPrefetchAssociationByRelationType;
        
        private const string ProcedurePrefixForGetCache = "gc";
        private const string ProcedurePrefixForUpdateCache = "uc";
        private const string ProcedurePrefixForResetCache = "rc";
        private const string ProcedurePrefixForCreateObject = "co_";
        private const string ProcedurePrefixForCreateObjects = "cos_";
        private const string ProcedurePrefixForLoad = "l_";

        private const string ProcedurePrefixForGetUnits = "gu_";
        private const string ProcedurePrefixForPrefetchUnits = "pu_";
        private const string ProcedurePrefixForGetRole = "gr_";
        private const string ProcedurePrefixForPrefetchRole = "pr_";
        private const string ProcedurePrefixForSetRole = "sr_";
        private const string ProcedurePrefixForClearRole = "cr_";
        private const string ProcedurePrefixForAddRole = "ar_";
        private const string ProcedurePrefixForRemoveRole = "rr_";
        private const string ProcedurePrefixForGetAssociation = "ga_";
        private const string ProcedurePrefixForPrefetchAssociation = "pa_";

        private readonly Database database;

        private readonly Dictionary<string, string> procedureDefinitionByName;
        private readonly Dictionary<string, string> tableTypeDefinitionByName;

        public Mapping(Database database, bool isObjectIdInteger)
        {
            this.database = database;
            this.IsObjectIdInteger = isObjectIdInteger;

            if (this.IsObjectIdInteger)
            {
                this.SqlTypeForObject = "int";
                this.SqlDbTypeForObject = SqlDbType.Int;
            }
            else
            {
                this.SqlTypeForObject = "bigint";
                this.SqlDbTypeForObject = SqlDbType.BigInt;
            }

            // TableTypes
            // ----------
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

            this.tableTypeDefinitionByName = new Dictionary<string, string>();

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForObject + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForObject + " " + this.SqlTypeForObject + ")\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForObject, sql.ToString()); 
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForCompositeRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " " + this.SqlTypeForObject + ")\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForCompositeRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForStringRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " nvarchar(max))\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForStringRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForIntegerRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " int)\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForIntegerRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForFloatRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " float)\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForFloatRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForDateTimeRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " datetime2)\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForDateTimeRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForBooleanRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " bit)\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForBooleanRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForUniqueRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " uniqueidentifier)\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForUniqueRelation, sql.ToString());
            }

            {
                var sql = new StringBuilder();
                sql.Append("CREATE TYPE " + this.TableTypeNameForBinaryRelation + " AS TABLE\n");
                sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                sql.Append(this.TableTypeColumnNameForRole + " varbinary(max))\n");

                this.tableTypeDefinitionByName.Add(this.TableTypeNameForBinaryRelation, sql.ToString());
            }

            foreach (var precisionEntry in this.TableTypeNameForDecimalRelationByScaleByPrecision)
            {
                var precision = precisionEntry.Key;
                foreach (var scaleEntry in precisionEntry.Value)
                {
                    var scale = scaleEntry.Key;
                    var decimalRelationTable = scaleEntry.Value;

                    var sql = new StringBuilder();
                    sql.Append("CREATE TYPE " + decimalRelationTable + " AS TABLE\n");
                    sql.Append("(" + this.TableTypeColumnNameForAssociation + " " + this.SqlTypeForObject + ",\n");
                    sql.Append(this.TableTypeColumnNameForRole + " DECIMAL(" + precision + "," + scale + ") )\n");

                    this.tableTypeDefinitionByName.Add(decimalRelationTable, sql.ToString());
                }
            }


            // Tables
            // ------
            this.TableNameForObjects = database.SchemaName + "." + "_o";
            this.TableNameForObjectByClass = new Dictionary<IClass, string>();
            this.ColumnNameByRelationType = new Dictionary<IRelationType, string>();
            this.ParamNameByRoleType = new Dictionary<IRoleType, string>();

            foreach (var @class in this.Database.MetaPopulation.Classes)
            {
                this.TableNameForObjectByClass.Add(@class, this.database.SchemaName + "." + this.NormalizeName(@class.SingularName));

                foreach (var associationType in @class.AssociationTypes)
                {
                    var relationType = associationType.RelationType;
                    var roleType = relationType.RoleType;
                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses && roleType.IsMany)
                    {
                        this.ColumnNameByRelationType[relationType] = this.NormalizeName(associationType.SingularPropertyName);
                    }
                }

                foreach (var roleType in @class.RoleTypes)
                {
                    var relationType = roleType.RelationType;
                    var associationType3 = relationType.AssociationType;
                    if (roleType.ObjectType.IsUnit)
                    {
                        this.ColumnNameByRelationType[relationType] = this.NormalizeName(roleType.SingularPropertyName);
                        this.ParamNameByRoleType[roleType] = string.Format(ParamFormat, roleType.SingularFullName);
                    }
                    else
                    {
                        if (!(associationType3.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses && !roleType.IsMany)
                        {
                            this.ColumnNameByRelationType[relationType] = this.NormalizeName(roleType.SingularPropertyName);
                        }
                    }
                }
            }

            this.TableNameForRelationByRelationType = new Dictionary<IRelationType, string>();

            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;

                if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveClasses))
                {
                    this.TableNameForRelationByRelationType.Add(relationType, this.database.SchemaName + "." + this.NormalizeName(relationType.RoleType.SingularFullName));
                }
            }


            // Procedures
            // ----------
            this.procedureDefinitionByName = new Dictionary<string, string>();

            this.ProcedureNameForLoadObjectByClass = new Dictionary<IClass, string>();
            this.ProcedureNameForCreateObjectByClass = new Dictionary<IClass, string>();
            this.ProcedureNameForCreateObjectsByClass = new Dictionary<IClass, string>();

            this.ProcedureNameForGetUnitRolesByClass = new Dictionary<IClass, string>();
            this.ProcedureNameForPrefetchUnitRolesByClass = new Dictionary<IClass, string>();
            this.ProcedureNameForSetUnitRoleByRelationTypeByClass = new Dictionary<IClass, Dictionary<IRelationType, string>>();

            this.ProcedureNameForGetRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForPrefetchRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForSetRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForAddRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForRemoveRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForClearRoleByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForGetAssociationByRelationType = new Dictionary<IRelationType, string>();
            this.ProcedureNameForPrefetchAssociationByRelationType = new Dictionary<IRelationType, string>();

            // Get Cache Ids
            this.ProcedureNameForGetCache = this.Database.SchemaName + "." + ProcedurePrefixForGetCache;
            var definition =
@"CREATE PROCEDURE " + this.ProcedureNameForGetCache + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    SELECT " + ColumnNameForObject + ", " + ColumnNameForCache + @"
    FROM " + this.TableNameForObjects + @"
    WHERE " + ColumnNameForObject + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
            this.procedureDefinitionByName.Add(this.ProcedureNameForGetCache, definition);

            // Update Cache Ids
            this.ProcedureNameForUpdateCache = this.Database.SchemaName + "." + ProcedurePrefixForUpdateCache;
            definition =
@"CREATE PROCEDURE " + this.ProcedureNameForUpdateCache + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    UPDATE " + this.TableNameForObjects + @"
    SET " + ColumnNameForCache + " = " + ColumnNameForCache + @" - 1
    FROM " + this.TableNameForObjects + @"
    WHERE " + ColumnNameForObject + " IN ( SELECT " + this.TableTypeColumnNameForObject + " FROM " + ParamNameForTableType + ");\n\n";
            this.procedureDefinitionByName.Add(this.ProcedureNameForUpdateCache, definition);

            // Reset Cache Ids
            this.ProcedureNameForResetCache = this.Database.SchemaName + "." + ProcedurePrefixForResetCache;
            definition =
@"CREATE PROCEDURE " + this.ProcedureNameForResetCache + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    UPDATE " + this.TableNameForObjects + @"
    SET " + ColumnNameForCache + " = " + Reference.InitialCacheId + @"
    FROM " + this.TableNameForObjects + @"
    WHERE " + ColumnNameForObject + " IN ( SELECT " + this.TableTypeColumnNameForObject + " FROM " + ParamNameForTableType + ");\n\n";
            this.procedureDefinitionByName.Add(this.ProcedureNameForResetCache, definition);

            foreach (var @class in this.Database.MetaPopulation.Classes)
            {
                var className = @class.Name.ToLowerInvariant();
                var table = this.TableNameForObjectByClass[@class];

                // Load Objects
                this.ProcedureNameForLoadObjectByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForLoad + className);
                definition = @"CREATE PROCEDURE " + this.ProcedureNameForLoadObjectByClass[@class] + @"
    " + ParamNameForType + @" " + SqlTypeForType + @",
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS
    INSERT INTO " + table + " (" + ColumnNameForType + ", " + ColumnNameForObject + @")
    SELECT " + ParamNameForType + @", " + this.TableTypeColumnNameForObject + @"
    FROM " + ParamNameForTableType + "\n";
                this.procedureDefinitionByName.Add(this.ProcedureNameForLoadObjectByClass[@class], definition);

                // CreateObject
                this.ProcedureNameForCreateObjectByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForCreateObject + className);
                definition = @"CREATE PROCEDURE " + this.ProcedureNameForCreateObjectByClass[@class] + @"
" + ParamNameForType + @" " + SqlTypeForType + @"
AS 
DECLARE  " + ParamNameForObject + " AS " + this.SqlTypeForObject + @"

INSERT INTO " + this.TableNameForObjects + " (" + ColumnNameForType + ", " + ColumnNameForCache + @")
VALUES (" + ParamNameForType + ", " + Reference.InitialCacheId + @");

SELECT " + ParamNameForObject + @" = SCOPE_IDENTITY();

INSERT INTO " + table + " (" + ColumnNameForObject + "," + ColumnNameForType + @")
VALUES (" + ParamNameForObject + "," + ParamNameForType + @");

SELECT " + ParamNameForObject + @";";
                this.procedureDefinitionByName.Add(this.ProcedureNameForCreateObjectByClass[@class], definition);

                // CreateObjects
                this.ProcedureNameForCreateObjectsByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForCreateObjects + className);
                definition = @"CREATE PROCEDURE " + this.ProcedureNameForCreateObjectsByClass[@class] + @"
" + ParamNameForType + @" " + SqlTypeForType + @",
" + ParamNameForCount + @" " + SqlTypeForCount + @"
AS 
BEGIN
DECLARE @IDS TABLE (id INT);
DECLARE @O INT, @COUNTER INT

SET @COUNTER = 0
WHILE @COUNTER < " + ParamNameForCount + @"
    BEGIN

    INSERT INTO " + this.TableNameForObjects + " (" + ColumnNameForType + ", " + ColumnNameForCache + @")
    VALUES (" + ParamNameForType + ", " + Reference.InitialCacheId + @" );

    INSERT INTO @IDS(id)
    VALUES (SCOPE_IDENTITY());

    SET @COUNTER = @COUNTER+1;
    END

INSERT INTO " + this.TableNameForObjectByClass[@class.ExclusiveClass] + " (" + ColumnNameForObject + "," + ColumnNameForType + @")
SELECT ID," + ParamNameForType + @" FROM @IDS;

SELECT id FROM @IDS;
END";
                this.procedureDefinitionByName.Add(this.ProcedureNameForCreateObjectsByClass[@class], definition);

                var sortedUnitRoleTypes = this.Database.GetSortedUnitRolesByObjectType(@class);
                if (sortedUnitRoleTypes.Length > 0)
                {
                    {
                        // Get Unit Roles
                        this.ProcedureNameForGetUnitRolesByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForGetUnits + className);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetUnitRolesByClass[@class] + @"
" + ParamNameForObject + " AS " + this.SqlTypeForObject + @"
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

                            definition += this.ColumnNameByRelationType[role.RelationType];
                        }

                        definition += @"
    FROM " + this.TableNameForObjectByClass[@class.ExclusiveClass] + @"
    WHERE " + ColumnNameForObject + "=" + ParamNameForObject;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForGetUnitRolesByClass[@class], definition);
                    }

                    {
                        // Prefetch Unit Roles
                        this.ProcedureNameForPrefetchUnitRolesByClass.Add(@class, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchUnits + className);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchUnitRolesByClass[@class] + @"
" + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    SELECT " + ColumnNameForObject + ", ";
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

                            definition += this.ColumnNameByRelationType[role.RelationType];
                        }

                        definition += @"
FROM " + this.TableNameForObjectByClass[@class.ExclusiveClass] + @"
WHERE " + ColumnNameForObject + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchUnitRolesByClass[@class], definition);
                    }
                }

                foreach (var associationType in @class.AssociationTypes)
                {
                    var relationType = associationType.RelationType;
                    var roleType = relationType.RoleType;
                    var relationTypeName = roleType.SingularFullName.ToLowerInvariant();

                    if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses && roleType.IsMany)
                    {
                        // Get Composites Role (1-*) [object table]
                        this.ProcedureNameForGetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetRoleByRelationType[relationType] + @"
    " + ParamNameForAssociation + @" " + this.SqlTypeForObject + @"
AS
    SELECT " + ColumnNameForObject + @"
    FROM " + table + @"
    WHERE " + this.ColumnNameByRelationType[relationType] + "=" + ParamNameForAssociation;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForGetRoleByRelationType[relationType], definition);

                        // Prefetch Composites Role (1-*) [object table]
                        this.ProcedureNameForPrefetchRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS

    SELECT " + this.ColumnNameByRelationType[relationType] + ", " + ColumnNameForObject + @"
    FROM " + table + @"
    WHERE " + this.ColumnNameByRelationType[relationType] + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchRoleByRelationType[relationType], definition);

                        if (associationType.IsOne)
                        {
                            // Get Composite Association (1-*) [object table]
                            this.ProcedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + className + "_" + relationTypeName);
                            definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + ParamNameForRole + @" " + this.SqlTypeForObject + @"
AS
    SELECT " + this.ColumnNameByRelationType[relationType] + @"
    FROM " + table + @"
    WHERE " + ColumnNameForObject + "=" + ParamNameForRole;
                            this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);

                            // Prefetch Composite Association (1-*) [object table]
                            this.ProcedureNameForPrefetchAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchAssociation + className + "_" + relationTypeName);
                            definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchAssociationByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS
    SELECT " + this.ColumnNameByRelationType[relationType] + ", " + ColumnNameForObject + @"
    FROM " + table + @"
    WHERE " + ColumnNameForObject + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                            this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchAssociationByRelationType[relationType], definition);
                        }

                        // Add Composite Role (1-*) [object table]
                        this.ProcedureNameForAddRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForAddRole + className + "_" + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForAddRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForAssociation + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForRole;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForAddRoleByRelationType[relationType], definition);

                        // Remove Composite Role (1-*) [object table]
                        this.ProcedureNameForRemoveRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForRemoveRole + className + "_" + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForRemoveRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + @" = null
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForAssociation + @" AND
    " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForRole;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForRemoveRoleByRelationType[relationType], definition);

                        // Clear Composites Role (1-*) [object table]
                        this.ProcedureNameForClearRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForClearRole + className + "_" + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForClearRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 

    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + @" = null
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS a
    ON " + this.ColumnNameByRelationType[relationType] + " = a." + this.TableTypeColumnNameForObject;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForClearRoleByRelationType[relationType], definition);
                    }
                }

                var procedureNameForSetUnitRoleByRelationType = new Dictionary<IRelationType, string>();
                this.ProcedureNameForSetUnitRoleByRelationTypeByClass.Add(@class, procedureNameForSetUnitRoleByRelationType);

                foreach (var roleType in @class.RoleTypes)
                {
                    var relationType = roleType.RelationType;
                    var associationType = relationType.AssociationType;
                    var relationTypeName = roleType.SingularFullName.ToLowerInvariant();

                    if (roleType.ObjectType.IsUnit)
                    {
                        procedureNameForSetUnitRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForSetRole + className + "_" + relationTypeName);

                        var unitTypeTag = ((IUnit)relationType.RoleType.ObjectType).UnitTag;
                        switch (unitTypeTag)
                        {
                            case UnitTags.AllorsString:
                                // Set String Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForStringRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsInteger:
                                // Set Integer Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForIntegerRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsFloat:
                                // Set Double Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForFloatRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsDecimal:
                                // Set Decimal Role
                                var decimalRelationTable = this.TableTypeNameForDecimalRelationByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];

                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
" + ParamNameForTableType + @" " + decimalRelationTable + @" READONLY
AS 
UPDATE " + table + @"
SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
FROM " + table + @"
INNER JOIN " + ParamNameForTableType + @" AS r
ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsBoolean:
                                // Set Boolean Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForBooleanRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsDateTime:
                                // Set DateTime Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForDateTimeRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsUnique:
                                // Set Unique Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForUniqueRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            case UnitTags.AllorsBinary:
                                // Set Binary Role
                                definition = "CREATE PROCEDURE " + procedureNameForSetUnitRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForBinaryRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                                break;

                            default:
                                throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.SingularName);
                        }

                        this.procedureDefinitionByName.Add(procedureNameForSetUnitRoleByRelationType[relationType], definition);
                    }
                    else
                    {
                        if (!(associationType.IsMany && roleType.IsMany) && relationType.ExistExclusiveClasses && roleType.IsOne)
                        {
                            // Get Composite Role (1-1 and *-1) [object table]
                            this.ProcedureNameForGetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetRole + relationTypeName);
                            definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetRoleByRelationType[relationType] + @"
    " + ParamNameForAssociation + @" " + this.SqlTypeForObject + @"
AS 
    SELECT " + this.ColumnNameByRelationType[relationType] + @"
    FROM " + table + @"
    WHERE " + ColumnNameForObject + "=" + ParamNameForAssociation;
                            this.procedureDefinitionByName.Add(this.ProcedureNameForGetRoleByRelationType[relationType], definition);

                            // Prefetch Composite Role (1-1 and *-1) [object table]
                            this.ProcedureNameForPrefetchRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchRole + relationTypeName);
                            definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    SELECT  " + ColumnNameForObject + ", " + this.ColumnNameByRelationType[relationType] + @"
    FROM " + table + @"
    WHERE " + ColumnNameForObject + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                            this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchRoleByRelationType[relationType], definition);

                            if (associationType.IsOne)
                            {
                                // Get Composite Association (1-1) [object table]
                                this.ProcedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + className + "_" + relationTypeName);
                                definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + ParamNameForRole + @" " + this.SqlTypeForObject + @"
AS 
    SELECT " + ColumnNameForObject + @"
    FROM " + table + @"
    WHERE " + this.ColumnNameByRelationType[relationType] + "=" + ParamNameForRole;
                                this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);

                                // Prefetch Composite Association (1-1) [object table]
                                this.ProcedureNameForPrefetchAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchAssociation + className + "_" + relationTypeName);
                                definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchAssociationByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    SELECT " + ColumnNameForObject + ", " + this.ColumnNameByRelationType[relationType] + @"
    FROM " + table + @"
    WHERE " + this.ColumnNameByRelationType[relationType] + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                                this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchAssociationByRelationType[relationType], definition);
                            }
                            else
                            {
                                // Get Composite Association (*-1) [object table]
                                this.ProcedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + className + "_" + relationTypeName);
                                definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + ParamNameForRole + @" " + this.SqlTypeForObject + @"
AS 
    SELECT " + ColumnNameForObject + @"
    FROM " + table + @"
    WHERE " + this.ColumnNameByRelationType[relationType] + "=" + ParamNameForRole;
                                this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);

                                // Prefetch Composite Association (*-1) [object table]
                                this.ProcedureNameForPrefetchAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchAssociation + className + "_" + relationTypeName);
                                definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchAssociationByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    SELECT " + ColumnNameForObject + ", " + this.ColumnNameByRelationType[relationType] + @"
    FROM " + table + @"
    WHERE " + this.ColumnNameByRelationType[relationType] + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                                this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchAssociationByRelationType[relationType], definition);
                            }

                            // Set Composite Role (1-1 and *-1) [object table]
                            this.ProcedureNameForSetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForSetRole + className + "_" + relationTypeName);
                            definition = @"CREATE PROCEDURE " + this.ProcedureNameForSetRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + " = r." + this.TableTypeColumnNameForRole + @"
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS r
    ON " + ColumnNameForObject + " = r." + this.TableTypeColumnNameForAssociation + @"
";
                            this.procedureDefinitionByName.Add(this.ProcedureNameForSetRoleByRelationType[relationType], definition);

                            // Clear Composite Role (1-1 and *-1) [object table]
                            this.ProcedureNameForClearRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForClearRole + className + "_" + relationTypeName);
                            definition = @"CREATE PROCEDURE " + this.ProcedureNameForClearRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    UPDATE " + table + @"
    SET " + this.ColumnNameByRelationType[relationType] + @" = null
    FROM " + table + @"
    INNER JOIN " + ParamNameForTableType + @" AS a
    ON " + ColumnNameForObject + " = a." + this.TableTypeColumnNameForObject;
                            this.procedureDefinitionByName.Add(this.ProcedureNameForClearRoleByRelationType[relationType], definition);
                        }
                    }
                }
            }

            foreach (var relationType in this.Database.MetaPopulation.RelationTypes)
            {
                var associationType = relationType.AssociationType;
                var roleType = relationType.RoleType;
                var relationTypeName = roleType.SingularFullName.ToLowerInvariant();
                
                if (!roleType.ObjectType.IsUnit && ((associationType.IsMany && roleType.IsMany) || !relationType.ExistExclusiveClasses))
                {
                    var table = this.TableNameForRelationByRelationType[relationType];

                    this.ProcedureNameForPrefetchAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchAssociation + relationTypeName);
                    this.ProcedureNameForClearRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForClearRole + relationTypeName);

                    if (roleType.IsMany)
                    {
                        // Get Composites Role (1-* and *-*) [relation table]
                        this.ProcedureNameForGetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetRoleByRelationType[relationType] + @"
    " + ParamNameForAssociation + @" " + this.SqlTypeForObject + @"
AS
    SELECT " + ColumnNameForRole + @"
    FROM " + table + @"
    WHERE " + ColumnNameForAssociation + "=" + ParamNameForAssociation;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForGetRoleByRelationType[relationType], definition);

                        // Prefetch Composites Role (1-* and *-*) [relation table]
                        this.ProcedureNameForPrefetchRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS
    SELECT " + ColumnNameForAssociation + ", " + ColumnNameForRole + @"
    FROM " + table + @"
    WHERE " + ColumnNameForAssociation + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchRoleByRelationType[relationType], definition);

                        // Add Composite Role (1-* and *-*) [relation table]
                        this.ProcedureNameForAddRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForAddRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForAddRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    INSERT INTO " + table + " (" + ColumnNameForAssociation + "," + ColumnNameForRole + @")
    SELECT " + this.TableTypeColumnNameForAssociation + @", " + this.TableTypeColumnNameForRole + @"
    FROM " + ParamNameForTableType + "\n";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForAddRoleByRelationType[relationType], definition);

                        // Remove Composite Role (1-* and *-*) [relation table]
                        this.ProcedureNameForRemoveRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForRemoveRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForRemoveRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    DELETE T 
    FROM " + table + @" T
    INNER JOIN " + ParamNameForTableType + @" R
    ON T." + ColumnNameForAssociation + " = R." + this.TableTypeColumnNameForAssociation + @"
    AND T." + ColumnNameForRole + " = R." + this.TableTypeColumnNameForRole + @";";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForRemoveRoleByRelationType[relationType], definition);
                    }
                    else
                    {
                        // Get Composite Role (1-1 and *-1) [relation table]
                        this.ProcedureNameForGetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetRoleByRelationType[relationType] + @"
    " + ParamNameForAssociation + @" " + this.SqlTypeForObject + @"
AS
    SELECT " + ColumnNameForRole + @"
    FROM " + table + @"
    WHERE " + ColumnNameForAssociation + "=" + ParamNameForAssociation;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForGetRoleByRelationType[relationType], definition);

                        // Prefetch Composite Role (1-1 and *-1) [relation table]
                        this.ProcedureNameForPrefetchRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForPrefetchRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS
    SELECT " + ColumnNameForAssociation + ", " + ColumnNameForRole + @"
    FROM " + table + @"
    WHERE " + ColumnNameForAssociation + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchRoleByRelationType[relationType], definition);

                        // Set Composite Role (1-1 and *-1) [relation table]
                        this.ProcedureNameForSetRoleByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForSetRole + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForSetRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForCompositeRelation + @" READONLY
AS
    MERGE " + table + @" T
    USING " + ParamNameForTableType + @" AS r
    ON T." + ColumnNameForAssociation + @" = r." + this.TableTypeColumnNameForAssociation + @"

    WHEN MATCHED THEN
    UPDATE SET " + ColumnNameForRole + @"= r." + this.TableTypeColumnNameForRole + @"

    WHEN NOT MATCHED THEN
    INSERT (" + ColumnNameForAssociation + "," + ColumnNameForRole + @")
    VALUES (r." + this.TableTypeColumnNameForAssociation + ", r." + this.TableTypeColumnNameForRole + @");";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForSetRoleByRelationType[relationType], definition);
                    }

                    if (associationType.IsOne)
                    {
                        // Get Composite Association (1-1) [relation table]
                        this.ProcedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + ParamNameForRole + @" " + this.SqlTypeForObject + @"
AS
    SELECT " + ColumnNameForAssociation + @"
    FROM " + table + @"
    WHERE " + ColumnNameForRole + "=" + ParamNameForRole;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);

                        // Prefetch Composite Association (1-1) [relation table]
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchAssociationByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS
    SELECT " + ColumnNameForAssociation + "," + ColumnNameForRole + @"
    FROM " + table + @"
    WHERE " + ColumnNameForRole + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchAssociationByRelationType[relationType], definition);
                    }
                    else
                    {
                        // Get Composite Association (*-1) [relation table]
                        this.ProcedureNameForGetAssociationByRelationType.Add(relationType, this.Database.SchemaName + "." + ProcedurePrefixForGetAssociation + relationTypeName);
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForGetAssociationByRelationType[relationType] + @"
    " + ParamNameForRole + @" " + this.SqlTypeForObject + @"
AS
    SELECT " + ColumnNameForAssociation + @"
    FROM " + table + @"
    WHERE " + ColumnNameForRole + "=" + ParamNameForRole;
                        this.procedureDefinitionByName.Add(this.ProcedureNameForGetAssociationByRelationType[relationType], definition);

                        // Prefetch Composite Association (*-1) [relation table]
                        definition = @"CREATE PROCEDURE " + this.ProcedureNameForPrefetchAssociationByRelationType[relationType] + @"
   " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS
    SELECT " + ColumnNameForAssociation + "," + ColumnNameForRole + @"
    FROM " + table + @"
    WHERE " + ColumnNameForRole + " IN (SELECT " + this.TableTypeColumnNameForObject + @" FROM " + ParamNameForTableType + ")";
                        this.procedureDefinitionByName.Add(this.ProcedureNameForPrefetchAssociationByRelationType[relationType], definition);
                    }

                    // Clear Composite Role (1-1 and *-1) [relation table]
                    definition = @"CREATE PROCEDURE " + this.ProcedureNameForClearRoleByRelationType[relationType] + @"
    " + ParamNameForTableType + @" " + this.TableTypeNameForObject + @" READONLY
AS 
    DELETE T 
    FROM " + table + @" T
    INNER JOIN " + ParamNameForTableType + @" A
    ON T." + ColumnNameForAssociation + " = A." + this.TableTypeColumnNameForObject;
                    this.procedureDefinitionByName.Add(this.ProcedureNameForClearRoleByRelationType[relationType], definition);
                }
            }
        }

        public bool IsObjectIdInteger { get; private set; }
        
        public Dictionary<string, string> ProcedureDefinitionByName
        {
            get
            {
                return this.procedureDefinitionByName;
            }
        }

        public Dictionary<string, string> TableTypeDefinitionByName
        {
            get
            {
                return this.tableTypeDefinitionByName;
            }
        }

        protected internal Database Database
        {
            get { return this.database; }
        }

        internal string NormalizeName(string name)
        {
            name = name.ToLowerInvariant();
            if (ReservedWords.Names.Contains(name))
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

        internal SqlDbType GetSqlDbType(IRoleType roleType)
        {
            var unit = (IUnit)roleType.ObjectType;
            switch (unit.UnitTag)
            {
                case UnitTags.AllorsString:
                    return SqlDbType.NVarChar;
                case UnitTags.AllorsInteger:
                    return SqlDbType.Int;
                case UnitTags.AllorsDecimal:
                    return SqlDbType.Decimal;
                case UnitTags.AllorsFloat:
                    return SqlDbType.Float;
                case UnitTags.AllorsBoolean:
                    return SqlDbType.Bit;
                case UnitTags.AllorsDateTime:
                    return SqlDbType.DateTime2;
                case UnitTags.AllorsUnique:
                    return SqlDbType.UniqueIdentifier;
                case UnitTags.AllorsBinary:
                    return SqlDbType.VarBinary;
                default:
                    throw new Exception("Unknown Unit Type");
            }
        }
    }
}