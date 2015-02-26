// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartyProductRevenueHistory.cs" company="Allors bvba">
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

    public partial class PartyProductRevenueHistory
    {
        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            this.AppsDeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistParty)
            {
                uiText.Append(this.Party.DeriveDisplayName());
            }

            if (this.ExistProduct)
            {
                uiText.Append(", ");
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

        internal void AppsDeriveHistory()
        {
            this.Revenue = 0;

            var startDate = DateTime.Now.AddYears(-1);
            var year = startDate.Year;
            var month = startDate.Month;

            var revenues = this.Party.PartyProductRevenuesWhereParty;

            //// sum revenue trailing twelve months + revenue current month
            foreach (PartyProductRevenue revenue in revenues)
            {
                if (revenue.InternalOrganisation.Equals(this.InternalOrganisation) &&
                    revenue.Product == this.Product &&
                    ((revenue.Year == year && revenue.Month >= month) || (revenue.Year == DateTime.Now.Year && revenue.Month <= month)))
                {
                    this.Revenue += revenue.Revenue;
                    this.Quantity += revenue.Quantity;
                }
            }
        }
    }
}
