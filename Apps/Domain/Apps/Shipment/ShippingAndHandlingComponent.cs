// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShippingAndHandlingComponent.cs" company="Allors bvba">
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
    
    

    public partial class ShippingAndHandlingComponent
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSpecifiedFor)
            {
                this.SpecifiedFor = Domain.Singleton.Instance(this.Session).DefaultInternalOrganisation;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            this.AppsDeriveDisplayName();

            var characterBoundaryText = string.Format(
                "{0}",
                this.ExistGeographicBoundary ? this.GeographicBoundary.ComposeSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = string.Format(
                "{0}",
                this.ExistGeographicBoundary ? this.GeographicBoundary.ComposeSearchDataWordBoundaryText() : null);

            if (this.ExistShipmentValue)
            {
                if (this.ShipmentValue.ExistFromAmount)
                {
                    wordBoundaryText += " " + this.ShipmentValue.FromAmount;
                }

                if (this.ShipmentValue.ExistThroughAmount)
                {
                    wordBoundaryText += " " + this.ShipmentValue.ThroughAmount;
                }
            }

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
            
        }

        private void AppsDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            uiText.Append("Shipping & handling costs: ");

            if (this.ExistCost)
            {
                if (this.ExistCurrency)
                {
                    uiText.Append(this.Currency.Symbol);
                }

                uiText.Append(" ");
                uiText.Append(string.Format("{0:N2}", this.Cost));
            }

            uiText.Append(" ");

            if (this.ExistGeographicBoundary)
            {
                uiText.Append(", with boundary ");
                uiText.Append(this.GeographicBoundary.ComposeDisplayName());
            }

            if (this.ExistShipmentMethod)
            {
                uiText.Append(", with shipment method");
                uiText.Append(this.ShipmentMethod.Name);
            }

            if (this.ExistShipmentValue)
            {
                uiText.Append(", with order value ");
                if (this.ShipmentValue.ExistFromAmount)
                {
                    uiText.Append("from ");
                    uiText.Append(this.ShipmentValue.FromAmount);
                }

                if (this.ShipmentValue.ExistThroughAmount)
                {
                    if (this.ShipmentValue.ExistFromAmount)
                    {
                        uiText.Append(" ");
                    }

                    uiText.Append("through ");
                    uiText.Append(this.ShipmentValue.ThroughAmount);
                }
            }

            this.DisplayName = uiText.ToString();
        }
    }
}