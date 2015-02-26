// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductRevenueHistory.cs" company="Allors bvba">
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

    public partial class ProductRevenueHistory
    {
        public string RevenueAsCurrencyString
        {
            get
            {
                return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;
            
            this.AppsDeriveDisplayName(derivation);

            this.AppsDeriveMovingAverage();
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
                uiText.Append(DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat));
            }

            if (this.ExistInternalOrganisation)
            {
                uiText.Append(" revenue trailing twelve months at ");
                uiText.Append(this.InternalOrganisation.Name);
            }

            this.DisplayName = uiText.ToString();
        }

        private void AppsDeriveMovingAverage()
        {
            this.Revenue = 0;

            var startDate = DateTime.Now.AddYears(-1);
            var year = startDate.Year;
            var month = startDate.Month;

            var revenues = this.Product.ProductRevenuesWhereProduct;

            foreach (ProductRevenue revenue in revenues)
            {
                if (revenue.InternalOrganisation.Equals(this.InternalOrganisation) &&
                    ((revenue.Year == year && revenue.Month >= month) || (revenue.Year == DateTime.Now.Year && revenue.Month < month)))
                {
                    this.Revenue += revenue.Revenue;
                }
            }
        }
    }
}
