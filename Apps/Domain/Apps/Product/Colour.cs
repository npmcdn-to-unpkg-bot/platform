// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Colour.cs" company="Allors bvba">
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

    public partial class Colour
    {
        public void AddToBasePrice(BasePrice basePrice)
        {
            this.AddBasePrice(basePrice);
        }

        public void RemoveFromBasePrices(BasePrice basePrice)
        {
            this.RemoveBasePrice(basePrice);
        }

        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            this.AppsEnumerationDerive(derivation);

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
            return this.Name;
        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return this.Name;
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return null;
        }
    }
}
