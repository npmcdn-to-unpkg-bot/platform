//------------------------------------------------------------------------------------------------- 
// <copyright file="Repository.cs" company="Allors bvba">
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

namespace Allors.Meta
{
    using System;

    public static partial class Repository
    {
        public static Domain Core(MetaPopulation env)
        {
            var domain = new Domain(env, new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));

            domain.DefineUnit(UnitIds.StringId, "AllorsString", "AllorsStrings", UnitTags.AllorsString);
            domain.DefineUnit(UnitIds.IntegerId, "AllorsInteger", "AllorsIntegers", UnitTags.AllorsInteger);
            domain.DefineUnit(UnitIds.LongId, "AllorsLong", "AllorsLongs", UnitTags.AllorsLong);
            domain.DefineUnit(UnitIds.DecimalId, "AllorsDecimal", "AllorsDecimals", UnitTags.AllorsDecimal);
            domain.DefineUnit(UnitIds.DoubleId, "AllorsDouble", "AllorsDoubles", UnitTags.AllorsDouble);
            domain.DefineUnit(UnitIds.BooleanId, "AllorsBoolean", "AllorsBooleans", UnitTags.AllorsBoolean);
            domain.DefineUnit(UnitIds.DatetimeId, "AllorsDateTime", "AllorsDateTimes", UnitTags.AllorsDateTime);
            domain.DefineUnit(UnitIds.Unique, "AllorsUnique", "AllorsUniques", UnitTags.AllorsUnique);
            domain.DefineUnit(UnitIds.BinaryId, "AllorsBinary", "AllorsBinaries", UnitTags.AllorsBinary);

            return domain;
        }
    }
}