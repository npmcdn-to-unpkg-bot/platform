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
        public static Domain Allors(MetaPopulation meta)
        {

            // Allors Domain
            var domain = new Domain(meta, new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12")) { Name = "Allors" };

            new UnitBuilder(domain, UnitIds.StringId).WithSingularName("AllorsString").WithPluralName("AllorsStrings").WithUnitTag(UnitTags.AllorsString).Build();
            new UnitBuilder(domain, UnitIds.IntegerId).WithSingularName("AllorsInteger").WithPluralName("AllorsIntegers").WithUnitTag(UnitTags.AllorsInteger).Build();
            new UnitBuilder(domain, UnitIds.LongId).WithSingularName("AllorsLong").WithPluralName("AllorsLongs").WithUnitTag(UnitTags.AllorsLong).Build();
            new UnitBuilder(domain, UnitIds.DecimalId).WithSingularName("AllorsDecimal").WithPluralName("AllorsDecimals").WithUnitTag(UnitTags.AllorsDecimal).Build();
            new UnitBuilder(domain, UnitIds.DoubleId).WithSingularName("AllorsDouble").WithPluralName("AllorsDoubles").WithUnitTag(UnitTags.AllorsDouble).Build();
            new UnitBuilder(domain, UnitIds.BooleanId).WithSingularName("AllorsBoolean").WithPluralName("AllorsBooleans").WithUnitTag(UnitTags.AllorsBoolean).Build();
            new UnitBuilder(domain, UnitIds.DatetimeId).WithSingularName("AllorsDateTime").WithPluralName("AllorsDateTimes").WithUnitTag(UnitTags.AllorsDateTime).Build();
            new UnitBuilder(domain, UnitIds.Unique).WithSingularName("AllorsUnique").WithPluralName("AllorsUniques").WithUnitTag(UnitTags.AllorsUnique).Build();
            new UnitBuilder(domain, UnitIds.BinaryId).WithSingularName("AllorsBinary").WithPluralName("AllorsBinaries").WithUnitTag(UnitTags.AllorsBinary).Build();

            return domain;
        }
    }
}