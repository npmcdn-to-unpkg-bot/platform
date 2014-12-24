// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TelecommunicationsNumber.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    public partial class TelecommunicationsNumber
    {
        public bool IsPostalAddress
        {
            get
            {
                return false;
            }
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsPrepareDerivation(DerivablePrepareDerivation method)
        {
            var derivation = method.Derivation;

            foreach (PartyContactMechanism partyContactMechanism in this.PartyContactMechanismsWhereContactMechanism)
            {
                derivation.AddDependency(partyContactMechanism, this);
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, TelecommunicationsNumbers.Meta.ContactNumber);

            this.DeriveDisplayName();
            this.DeriveSearchDataCharacterBoundaryText();
            this.DeriveSearchDataWordBoundaryText();
        }

        private void AppsDeriveDisplayName()
        {
            this.DisplayName = this.ComposeDisplayName();
        }

        private void AppsDeriveSearchDataCharacterBoundaryText()
        {
            this.SearchData.CharacterBoundaryText = this.AppsComposeSearchDataCharacterBoundaryText();
        }

        private void AppsDeriveSearchDataWordBoundaryText()
        {
            this.SearchData.WordBoundaryText = this.AppsComposeSearchDataWordBoundaryText();
        }

        private string AppsComposeDisplayName()
        {
            return string.Format(
                "{0} {1} {2}",
                this.ExistCountryCode ? this.CountryCode : null,
                this.ExistAreaCode ? this.AreaCode : null,
                this.ExistContactNumber ? this.ContactNumber : null);
        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return null;
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return string.Format(
                "{0} {1} {2}",
                this.ExistCountryCode ? this.CountryCode : null,
                this.ExistAreaCode ? this.AreaCode : null,
                this.ExistContactNumber ? this.ContactNumber : null);
        }
    }
}