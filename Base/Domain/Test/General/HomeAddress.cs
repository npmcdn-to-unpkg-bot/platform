//------------------------------------------------------------------------------------------------- 
// <copyright file="HomeAddress.cs" company="Allors bvba">
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
// <summary>Defines the HomeAddress type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using global::System.Text;

    using Allors.Domain;

    /// <summary>
    /// A HomeAddress is a fysical address with a street/number and a place
    /// </summary>
    public partial class HomeAddress
    {
        public override string ToString()
        {
            return DisplayName;
        }

        protected override void CoreDerive(IDerivation derivation)
        {
            base.CoreDerive(derivation);

            derivation.Log.AssertExists(this, HomeAddresses.Meta.Street);
            derivation.Log.AssertNonEmptyString(this, HomeAddresses.Meta.Street);

            var sb = new StringBuilder();
            if (ExistStreet)
            {
                sb.AppendFormat("{0} ", Street);
            }

            if (ExistHouseNumber)
            {
                sb.Append(HouseNumber);
            }

            if (ExistPlace)
            {
                sb.AppendFormat(", {0}", this.Place.DisplayName);
            }

            this.DisplayName = sb.ToString();
        }
    }
}