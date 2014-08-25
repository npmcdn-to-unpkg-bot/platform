//------------------------------------------------------------------------------------------------- 
// <copyright file="MetaUnitTags.cs" company="Allors bvba">
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
// <summary>Defines the UnitTypeTag type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors.Meta
{
    using Allors.Meta.AllorsGenerated;

    /// <summary>
    /// The tags for <see cref="AllorsClassObjectType.UnitTag"/>s.
    /// Do not use tags for long term persistence, UnitTypeIds should be used for that.
    /// </summary>
    public enum MetaUnitTags
    {
        /// <summary>
        /// The tag for the binary <see cref="MetaObject"/>.
        /// </summary>
        AllorsBinary,

        /// <summary>
        /// The tag for the boolean <see cref="MetaObject"/>.
        /// </summary>
        AllorsBoolean,

        /// <summary>
        /// The tag for the date time <see cref="MetaObject"/>.
        /// </summary>
        AllorsDateTime,

        /// <summary>
        /// The tag for the decimal <see cref="MetaObject"/>.
        /// </summary>
        AllorsDecimal,

        /// <summary>
        /// The tag for the double <see cref="MetaObject"/>.
        /// </summary>
        AllorsDouble,

        /// <summary>
        /// The tag for the integer <see cref="MetaObject"/>.
        /// </summary>
        AllorsInteger,

        /// <summary>
        /// The tag for the long integer <see cref="MetaObject"/>.
        /// </summary>
        AllorsLong,

        /// <summary>
        /// The tag for the string <see cref="MetaObject"/>.
        /// </summary>
        AllorsString,

        /// <summary>
        /// The tag for the unique <see cref="MetaObject"/>.
        /// </summary>
        AllorsUnique,
    }
}