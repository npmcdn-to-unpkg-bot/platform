// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dimension.cs" company="Allors bvba">
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
    using Allors.Domain;
    

    public partial class Dimension
    {
        public void AddToBasePrice(BasePrice basePrice)
        {
            this.AddBasePrice(basePrice);
        }

        public void RemoveFromBasePrices(BasePrice basePrice)
        {
            if (this.AppsPrices.Contains(basePrice))
            {
                this.RemoveBasePrice(basePrice);
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

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, Dimensions.Meta.UnitOfMeasure);

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
            return string.Format("{0} {1}", this.ExistUnit ? this.Unit : 0, this.ExistUnitOfMeasure ? this.UnitOfMeasure.DisplayName : null);

        }

        private string AppsComposeSearchDataCharacterBoundaryText()
        {
            return null;
        }

        private string AppsComposeSearchDataWordBoundaryText()
        {
            return this.ExistUnitOfMeasure ? this.UnitOfMeasure.DisplayName : null;
        }
    }
}