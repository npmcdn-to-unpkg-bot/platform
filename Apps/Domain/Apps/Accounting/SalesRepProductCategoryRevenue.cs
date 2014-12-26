// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesRepProductCategoryRevenue.cs" company="Allors bvba">
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

    public partial class SalesRepProductCategoryRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;
            
            this.SalesRepName = this.SalesRep.DeriveDisplayName();

            this.AppsDeriveDisplayName(derivation);

            this.AppsDeriveRevenue();
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistSalesRep)
            {
                uiText.Append(this.SalesRep.DeriveDisplayName());
            }

            if (this.ExistProductCategory)
            {
                uiText.Append(", ");
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

        private void AppsDeriveRevenue()
        {
            this.Revenue = 0;

            var salesRepPartyProductCategoryRevenues = this.ProductCategory.SalesRepPartyProductCategoryRevenuesWhereProductCategory;
            salesRepPartyProductCategoryRevenues.Filter.AddEquals(SalesRepPartyProductCategoryRevenues.Meta.InternalOrganisation, this.InternalOrganisation);
            salesRepPartyProductCategoryRevenues.Filter.AddEquals(SalesRepPartyProductCategoryRevenues.Meta.SalesRep, this.SalesRep);
            salesRepPartyProductCategoryRevenues.Filter.AddEquals(SalesRepPartyProductCategoryRevenues.Meta.Year, this.Year);
            salesRepPartyProductCategoryRevenues.Filter.AddEquals(SalesRepPartyProductCategoryRevenues.Meta.Month, this.Month);

            foreach (SalesRepPartyProductCategoryRevenue salesRepPartyProductCategoryRevenue in salesRepPartyProductCategoryRevenues)
            {
                this.Revenue += salesRepPartyProductCategoryRevenue.Revenue;
            }

            if (this.ProductCategory.ExistParents)
            {
                SalesRepProductCategoryRevenues.AppsFindOrCreateAsDependable(this.Strategy.Session, this);
            }
        }
    }
}