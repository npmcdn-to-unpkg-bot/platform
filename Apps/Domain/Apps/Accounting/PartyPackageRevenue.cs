// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartyPackageRevenue.cs" company="Allors bvba">
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

    public partial class PartyPackageRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;
   

            this.PartyPackageName = string.Concat(this.Party.DisplayName, "/", this.Package.DisplayName);

            this.AppsDeriveDisplayName(derivation);

            this.AppsDeriveRevenue(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistParty)
            {
                uiText.Append(this.Party.DeriveDisplayName());
            }

            if (this.ExistPackage)
            {
                uiText.Append(", ");
                uiText.Append(this.Package.Name);
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

            var partyProductCategoryRevenues = this.Party.PartyProductCategoryRevenuesWhereParty;
            partyProductCategoryRevenues.Filter.AddEquals(PartyProductCategoryRevenues.Meta.InternalOrganisation, this.InternalOrganisation);
            partyProductCategoryRevenues.Filter.AddEquals(PartyProductCategoryRevenues.Meta.Year, this.Year);
            partyProductCategoryRevenues.Filter.AddEquals(PartyProductCategoryRevenues.Meta.Month, this.Month);

            foreach (PartyProductCategoryRevenue productCategoryRevenue in partyProductCategoryRevenues)
            {
                if (productCategoryRevenue.ProductCategory.ExistPackage && productCategoryRevenue.ProductCategory.Package.Equals(this.Package))
                {
                    this.Revenue += productCategoryRevenue.Revenue;
                }
            }

            var months = ((DateTime.Now.Year - this.Year) * 12) + DateTime.Now.Month - this.Month;
            if (months <= 12)
            {
                var histories = this.Party.PartyPackageRevenueHistoriesWhereParty;
                histories.Filter.AddEquals(PartyPackageRevenueHistories.Meta.InternalOrganisation, this.InternalOrganisation);
                histories.Filter.AddEquals(PartyPackageRevenueHistories.Meta.Package, this.Package);
                var history = histories.First ?? new PartyPackageRevenueHistoryBuilder(this.Session)
                                                     .WithCurrency(this.Currency)
                                                     .WithInternalOrganisation(this.InternalOrganisation)
                                                     .WithParty(this.Party)
                                                     .WithPackage(this.Package)
                                                     .WithRevenue(0)
                                                     .Build();

                history.AppsDeriveHistory();
            }

            if (this.ExistPackage)
            {
                var packageRevenue = PackageRevenues.AppsFindOrCreateAsDependable(this.Session, this);
                packageRevenue.Derive().Execute();
            }
        }
    }
}
