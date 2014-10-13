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
        public static readonly MetaPopulation MetaPopulation;

        public static Domain Test(MetaPopulation meta)
        {
            // Imports
            // Core
            var core = (Domain)meta.Find(new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));

            var allorsString = (Unit)meta.Find(UnitIds.StringId);
            var allorsInteger = (Unit)meta.Find(UnitIds.IntegerId);
            var allorsLong = (Unit)meta.Find(UnitIds.LongId);
            var allorsDecimal = (Unit)meta.Find(UnitIds.DecimalId);
            var allorsDouble = (Unit)meta.Find(UnitIds.DoubleId);
            var allorsBoolean = (Unit)meta.Find(UnitIds.BooleanId);
            var allorsDate = (Unit)meta.Find(UnitIds.DateId);
            var allorsUnique = (Unit)meta.Find(UnitIds.Unique);
            var allorsBinary = (Unit)meta.Find(UnitIds.BinaryId);

            // Base
            var @base = (Domain)meta.Find(new Guid("AB41FD0C-C887-4A1D-BEDA-CED69527E69A"));

            var stringTemplate = (Class)meta.Find(new Guid("0c50c02a-cc9c-4617-8530-15a24d4ac969"));
            var printable = (Interface)meta.Find(new Guid("61207a42-3199-4249-baa4-9dd11dc0f5b1"));
            var searchResult = (Interface)meta.Find(new Guid("a0ac7040-6984-4267-a200-919875e08909"));
            var enumeration = (Interface)meta.Find(new Guid("b7bcc22f-03f0-46fd-b738-4e035921d445"));
            var country = (Class)meta.Find(new Guid("c22bf60e-6428-4d10-8194-94f7be396f28"));
            var person = (Class)meta.Find(new Guid("c799ca62-a554-467d-9aa2-1663293bb37f"));
            var media = (Class)meta.Find(new Guid("da5b86a3-4f33-4c0d-965d-f4fbc1179374"));
            var searchable = (Interface)meta.Find(new Guid("ff34f3f1-6a17-404f-a9e5-5cffcdaa3d31"));
            var uniquelyIdentifiable = (Interface)meta.Find(new Guid("122ccfe1-f902-44c1-9d6c-6f6a0afa9469"));
            var singleton = (Class)meta.Find(new Guid("313b97a5-328c-4600-9dd2-b5bc146fb13b"));
            var accessControlledObject = (Interface)meta.Find(new Guid("eb0ff756-3e3d-4cf9-8935-8802a73d2df2"));
            var userInterfaceable = (Interface)meta.Find(new Guid("eea17b39-8912-40b3-8403-293bd5a3316d"));

            // Test
            var domain = new Domain(meta, new Guid("47636693-E55F-4ED3-93B6-3D75F11D5D4B")) { Name = "Test" };
            domain.AddDirectSuperdomain(@base);

            return domain;
       }
    }
}