//------------------------------------------------------------------------------------------------- 
// <copyright file="Place.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the Place type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    /// <summary>
    /// A Place is a fysical location somewhere on earth, defined as a postalcode/city and a country
    /// </summary>
    public partial class Place
    {
        public void TestsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, Places.Meta.PostalCode);
            derivation.Log.AssertNonEmptyString(this, Places.Meta.PostalCode);
            derivation.Log.AssertExists(this, Places.Meta.City);
            derivation.Log.AssertNonEmptyString(this, Places.Meta.City);
        }
    }
}
