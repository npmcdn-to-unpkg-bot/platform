// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EstimatedLaborCost.cs" company="Allors bvba">
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
    using System.Text;

    using Allors.Domain;
    

    public partial class EstimatedLaborCost
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, EstimatedProductCosts.Meta.Cost);
            derivation.Log.AssertExists(this, EstimatedProductCosts.Meta.Currency);
            derivation.Log.AssertExists(this, EstimatedProductCosts.Meta.FromDate);

            this.DeriveDisplayName();

            var characterBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistDescription ? this.Description : null,
                this.ExistGeographicBoundary ? this.GeographicBoundary.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistOrganisation ? this.Organisation.DeriveSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistCost ? string.Format("{0:N2}", this.Cost) : null,
                this.ExistGeographicBoundary ? this.GeographicBoundary.ComposeSearchDataWordBoundaryText() : null,
                this.ExistOrganisation ? this.Organisation.DeriveSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }

        private void AppsDeriveDisplayName()
        {
            StringBuilder uiText = new StringBuilder();

            uiText.Append("Laborcost: ");

            if (this.ExistDescription)
            {
                uiText.Append(this.Description);
                uiText.Append(" ");
            }

            if (this.ExistCost)
            {
                if (this.ExistCurrency)
                {
                    uiText.Append(this.Currency.Symbol);
                }

                uiText.Append(" ");
                uiText.Append(string.Format("{0:N2}", this.Cost));
            }

            if (this.ExistGeographicBoundary)
            {
                uiText.Append(", with boundary ");
                uiText.Append(this.GeographicBoundary.ComposeDisplayName());
            }

            if (this.ExistOrganisation)
            {
                uiText.Append(", for ");
                uiText.Append(this.Organisation.DeriveDisplayName());
            }

            this.DisplayName = uiText.ToString();
        }
    }
}