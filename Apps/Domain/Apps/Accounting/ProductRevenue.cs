// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductRevenue.cs" company="Allors bvba">
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
    using System;
    using System.Text;

    public partial class ProductRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.ProductName = this.Product.Description;

            this.AppsDeriveRevenue(derivation);

            this.AppsDeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistProduct)
            {
                uiText.Append(this.Product.ComposeDisplayName());
            }

            if (this.ExistRevenue)
            {
                uiText.Append(": ");
                uiText.Append(this.Year);
                uiText.Append("/");
                uiText.Append(this.Month);
                uiText.Append(" ");
                uiText.Append(DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat));
            }

            if (this.ExistInternalOrganisation)
            {
                uiText.Append(" at ");
                uiText.Append(this.InternalOrganisation.Name);
            }

            this.DisplayName = uiText.ToString();
        }

        private void AppsDeriveRevenue(IDerivation derivation)
        {
            this.Revenue = 0;

            var partyProductRevenues = this.Product.PartyProductRevenuesWhereProduct;
            partyProductRevenues.Filter.AddEquals(PartyProductRevenues.Meta.InternalOrganisation, this.InternalOrganisation);
            partyProductRevenues.Filter.AddEquals(PartyProductRevenues.Meta.Year, this.Year);
            partyProductRevenues.Filter.AddEquals(PartyProductRevenues.Meta.Month, this.Month);

            foreach (PartyProductRevenue partyProductRevenue in partyProductRevenues)
            {
                this.Revenue += partyProductRevenue.Revenue;
            }

            var months = ((DateTime.Now.Year - this.Year) * 12) + DateTime.Now.Month - this.Month;
            if (months <= 12)
            {
                var histories = this.Product.ProductRevenueHistoriesWhereProduct;
                histories.Filter.AddEquals(ProductRevenueHistories.Meta.InternalOrganisation, this.InternalOrganisation);
                var history = histories.First ?? new ProductRevenueHistoryBuilder(this.Strategy.Session)
                                                     .WithCurrency(this.Currency)
                                                     .WithInternalOrganisation(this.InternalOrganisation)
                                                     .WithProduct(this.Product)
                                                     .Build();
            }

            foreach (ProductCategory productCategory in this.Product.ProductCategories)
            {
                productCategory.Derive().WithDerivation(derivation).Execute();
            }
        }
    }
}
