// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoryRevenue.cs" company="Allors bvba">
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

    public partial class ProductCategoryRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.AppsDeriveRevenue(derivation);
            this.AppsDeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistProductCategory)
            {
                uiText.Append(this.ProductCategory.Description);
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

            var partyProductCategoryRevenues = this.ProductCategory.PartyProductCategoryRevenuesWhereProductCategory;
            partyProductCategoryRevenues.Filter.AddEquals(PartyProductCategoryRevenues.Meta.InternalOrganisation, this.InternalOrganisation);
            partyProductCategoryRevenues.Filter.AddEquals(PartyProductCategoryRevenues.Meta.Year, this.Year);
            partyProductCategoryRevenues.Filter.AddEquals(PartyProductCategoryRevenues.Meta.Month, this.Month);

            foreach (PartyProductCategoryRevenue productCategoryRevenue in partyProductCategoryRevenues)
            {
                this.Revenue += productCategoryRevenue.Revenue;
            }

            if (this.ProductCategory.ExistParents)
            {
                ProductCategoryRevenues.AppsFindOrCreateAsDependable(this.Strategy.Session, this);
            }

            var months = ((DateTime.Now.Year - this.Year) * 12) + DateTime.Now.Month - this.Month;
            if (months <= 12)
            {
                var histories = this.ProductCategory.ProductCategoryRevenueHistoriesWhereProductCategory;
                histories.Filter.AddEquals(ProductCategoryRevenueHistories.Meta.InternalOrganisation, this.InternalOrganisation);
                var history = histories.First ?? new ProductCategoryRevenueHistoryBuilder(this.Strategy.Session)
                                                     .WithCurrency(this.Currency)
                                                     .WithInternalOrganisation(this.InternalOrganisation)
                                                     .WithProductCategory(this.ProductCategory)
                                                     .Build();
            }

            foreach (ProductCategory parentCategory in this.ProductCategory.Parents)
            {
                var productCategoryRevenues = parentCategory.ProductCategoryRevenuesWhereProductCategory;
                productCategoryRevenues.Filter.AddEquals(ProductCategoryRevenues.Meta.InternalOrganisation, this.InternalOrganisation);
                productCategoryRevenues.Filter.AddEquals(ProductCategoryRevenues.Meta.Year, this.Year);
                productCategoryRevenues.Filter.AddEquals(ProductCategoryRevenues.Meta.Month, this.Month);
                var productCategoryRevenue = productCategoryRevenues.First ?? new ProductCategoryRevenueBuilder(this.Strategy.Session)
                                                                                    .WithInternalOrganisation(this.InternalOrganisation)
                                                                                    .WithProductCategory(parentCategory)
                                                                                    .WithYear(this.Year)
                                                                                    .WithMonth(this.Month)
                                                                                    .WithCurrency(this.Currency)
                                                                                    .WithRevenue(0M)
                                                                                    .Build();
                productCategoryRevenue.Derive().WithDerivation(derivation).Execute();
            }
        }
    }
}
