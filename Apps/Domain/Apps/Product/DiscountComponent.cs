// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscountComponent.cs" company="Allors bvba">
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
    

    

    public partial class DiscountComponent
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

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, DiscountComponents.Meta.SpecifiedFor);
            derivation.Log.AssertExists(this, DiscountComponents.Meta.FromDate);
            derivation.Log.AssertAtLeastOne(this, DiscountComponents.Meta.Price, DiscountComponents.Meta.Percentage);
            derivation.Log.AssertExistsAtMostOne(this, DiscountComponents.Meta.Price, DiscountComponents.Meta.Percentage);

            if (this.ExistPrice)
            {
                if (!this.ExistCurrency)
                {
                    this.Currency = this.SpecifiedFor.PreferredCurrency;
                }

                derivation.Log.AssertExists(this, BasePrices.Meta.Currency);
            }

            this.DeriveDisplayName();

            var characterBoundaryText = string.Format(
                "{0} {1} {2} {3} {4} {5}",
                this.ExistProduct ? this.Product.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistProductFeature ? this.ProductFeature.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistGeographicBoundary ? this.GeographicBoundary.ComposeSearchDataCharacterBoundaryText() : null,
                this.ExistProductCategory ? this.ProductCategory.Description : null,
                this.ExistOrderKind ? this.OrderKind.Description : null,
                this.ExistPartyClassification ? this.PartyClassification.Description : null);

            var wordBoundaryText = string.Format(
                "{0} {1} {2} {3}",
                this.ExistPrice ? string.Format("{0:N2}", this.Price) : null,
                this.ExistProduct ? this.Product.ComposeSearchDataWordBoundaryText() : null,
                this.ExistProductFeature ? this.ProductFeature.ComposeSearchDataWordBoundaryText() : null,
                this.ExistGeographicBoundary ? this.GeographicBoundary.ComposeSearchDataWordBoundaryText() : null);

            if (this.ExistOrderValue)
            {
                if (this.OrderValue.ExistFromAmount)
                {
                    wordBoundaryText += " " + this.OrderValue.FromAmount;
                }

                if (this.OrderValue.ExistThroughAmount)
                {
                    wordBoundaryText += " " + this.OrderValue.ThroughAmount;
                }
            }

            if (this.ExistOrderQuantityBreak)
            {
                if (this.OrderQuantityBreak.ExistFromAmount)
                {
                    wordBoundaryText += " " + this.OrderQuantityBreak.FromAmount;
                }

                if (this.OrderQuantityBreak.ExistThroughAmount)
                {
                    wordBoundaryText += " " + this.OrderQuantityBreak.ThroughAmount;
                }
            }

            if (this.ExistRevenueValueBreak)
            {
                if (this.RevenueValueBreak.ExistFromAmount)
                {
                    wordBoundaryText += " " + this.RevenueValueBreak.FromAmount;
                }

                if (this.RevenueValueBreak.ExistThroughAmount)
                {
                    wordBoundaryText += " " + this.RevenueValueBreak.ThroughAmount;
                }
            }

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;

            this.DeriveVirtualProductPriceComponent();
        }

        private void AppsDeriveVirtualProductPriceComponent()
        {
            if (this.ExistProduct)
            {
                this.Product.DeriveVirtualProductPriceComponent();
            }
        }

        private void AppsDeriveDisplayName()
        {
            var uiText = new StringBuilder();

            uiText.Append("Discount: ");

            if (this.ExistPrice)
            {
                if (this.ExistCurrency)
                {
                    uiText.Append(this.Currency.Symbol);
                }

                uiText.Append(" ");
                uiText.Append(string.Format("{0:N2}", this.Price));
            }

            if (this.ExistPercentage)
            {
                uiText.Append(string.Format("{0:N2}%", this.Percentage));
            }

            uiText.Append(" ");

            if (this.ExistProduct && !this.ExistProductFeature)
            {
                uiText.Append("for product ");
                uiText.Append(this.Product.ComposeDisplayName());
            }

            if (this.ExistProductFeature && !this.ExistProduct)
            {
                uiText.Append("for productfeature ");
                uiText.Append(this.ProductFeature.ComposeDisplayName());
            }

            if (this.ExistProductFeature && this.ExistProduct)
            {
                uiText.Append("for feature ");
                uiText.Append(this.ProductFeature.ComposeDisplayName());
                uiText.Append(" with product ");
                uiText.Append(this.Product.ComposeDisplayName());
            }

            if (this.ExistGeographicBoundary)
            {
                uiText.Append(", with boundary ");
                uiText.Append(this.GeographicBoundary.ComposeDisplayName());
            }

            if (this.ExistPartyClassification)
            {
                uiText.Append(", with party classification ");
                uiText.Append(this.PartyClassification.Description);
            }

            if (this.ExistProductCategory)
            {
                uiText.Append(", with product category ");
                uiText.Append(this.ProductCategory.Description);
            }

            if (this.ExistOrderKind)
            {
                uiText.Append(", with order of kind ");
                uiText.Append(this.OrderKind.Description);
            }

            if (this.ExistOrderQuantityBreak)
            {
                uiText.Append(", with order quantity break ");
                if (this.OrderQuantityBreak.ExistFromAmount)
                {
                    uiText.Append("from ");
                    uiText.Append(this.OrderQuantityBreak.FromAmount);
                }

                if (this.OrderQuantityBreak.ExistThroughAmount)
                {
                    if (this.OrderQuantityBreak.ExistFromAmount)
                    {
                        uiText.Append(" ");
                    }

                    uiText.Append("through ");
                    uiText.Append(this.OrderQuantityBreak.ThroughAmount);
                }
            }

            if (this.ExistRevenueValueBreak)
            {
                uiText.Append(", with revenue value break ");
                if (this.RevenueValueBreak.ExistFromAmount)
                {
                    uiText.Append("from ");
                    uiText.Append(this.RevenueValueBreak.FromAmount);
                }

                if (this.RevenueValueBreak.ExistThroughAmount)
                {
                    if (this.RevenueValueBreak.ExistFromAmount)
                    {
                        uiText.Append(" ");
                    }

                    uiText.Append("through ");
                    uiText.Append(this.RevenueValueBreak.ThroughAmount);
                }
            }

            if (this.ExistRevenueQuantityBreak)
            {
                uiText.Append(", with revenue quantity break ");
                if (this.RevenueQuantityBreak.ExistFrom)
                {
                    uiText.Append("from ");
                    uiText.Append(this.RevenueQuantityBreak.From);
                }

                if (this.RevenueQuantityBreak.ExistThrough)
                {
                    if (this.RevenueQuantityBreak.ExistFrom)
                    {
                        uiText.Append(" ");
                    }

                    uiText.Append("through ");
                    uiText.Append(this.RevenueQuantityBreak.Through);
                }
            }

            if (this.ExistOrderValue)
            {
                uiText.Append(", with order value ");
                if (this.OrderValue.ExistFromAmount)
                {
                    uiText.Append("from ");
                    uiText.Append(this.OrderValue.FromAmount);
                }

                if (this.OrderValue.ExistThroughAmount)
                {
                    if (this.OrderValue.ExistFromAmount)
                    {
                        uiText.Append(" ");
                    }

                    uiText.Append("through ");
                    uiText.Append(this.OrderValue.ThroughAmount);
                }
            }

            if (this.ExistSalesChannel)
            {
                uiText.Append(", with sales channel ");
                uiText.Append(this.SalesChannel.Name);
            }

            if (this.ExistPackageQuantityBreak)
            {
                uiText.Append(", with package count ");
                if (this.PackageQuantityBreak.ExistFrom)
                {
                    uiText.Append("from ");
                    uiText.Append(this.PackageQuantityBreak.From);
                }

                if (this.PackageQuantityBreak.ExistThrough)
                {
                    if (this.PackageQuantityBreak.ExistFrom)
                    {
                        uiText.Append(" ");
                    }

                    uiText.Append("through ");
                    uiText.Append(this.PackageQuantityBreak.Through);
                }
            }

            this.DisplayName = uiText.ToString();
        }
    }
}