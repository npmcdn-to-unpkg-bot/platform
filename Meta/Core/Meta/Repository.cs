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
        public static readonly Environment Environment = new Environment();

        private static void Core()
        {
            var core = new Domain(Environment, new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));

            {
                var objectType = new Unit(core, UnitIds.StringId);
                objectType.SingularName = UnitTags.AllorsString.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsString;
            }

            {
                var objectType = new Unit(core, UnitIds.IntegerId);
                objectType.SingularName = UnitTags.AllorsInteger.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsInteger;
            }

            {
                var objectType = new Unit(core, UnitIds.LongId);
                objectType.SingularName = UnitTags.AllorsLong.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsLong;
            }

            {
                var objectType = new Unit(core, UnitIds.DecimalId);
                objectType.SingularName = UnitTags.AllorsDecimal.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsDecimal;
            }

            {
                var objectType = new Unit(core, UnitIds.DoubleId);
                objectType.SingularName = UnitTags.AllorsDouble.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsDouble;
            }

            {
                var objectType = new Unit(core, UnitIds.BooleanId);
                objectType.SingularName = UnitTags.AllorsBoolean.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsBoolean;
            }

            {
                var objectType = new Unit(core, UnitIds.DatetimeId);
                objectType.SingularName = UnitTags.AllorsDateTime.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsDateTime;
            }

            {
                var objectType = new Unit(core, UnitIds.Unique);
                objectType.SingularName = UnitTags.AllorsUnique.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsUnique;
            }

            {
                var objectType = new Unit(core, UnitIds.BinaryId);
                objectType.SingularName = UnitTags.AllorsBinary.ToString();
                objectType.PluralName = objectType.SingularName + "s";
                objectType.UnitTag = (int)UnitTags.AllorsBinary;
            }
        }
    }
}