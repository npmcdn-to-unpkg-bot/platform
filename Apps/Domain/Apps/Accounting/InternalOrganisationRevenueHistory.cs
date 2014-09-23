// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InternalOrganisationRevenueHistory.cs" company="Allors bvba">
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

    using System;
    using System.Text;

    

    public partial class InternalOrganisationRevenueHistory
    {
        public string RevenueAsCurrencyString
        {
            get
            {
                return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
            }
        }

        public void AppsDeriveMovingAverage()
        {
            this.Revenue = 0;

            var startDate = DateTime.Now.AddYears(-1);
            var year = startDate.Year;
            var month = startDate.Month;

            var revenues = this.InternalOrganisation.InternalOrganisationRevenuesWhereInternalOrganisation;

            foreach (InternalOrganisationRevenue revenue in revenues)
            {
                if ((revenue.Year == year && revenue.Month >= month) || (revenue.Year == DateTime.Now.Year && revenue.Month < month))
                {
                    this.Revenue += revenue.Revenue;
                }
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            this.AppsDeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistInternalOrganisation)
            {
                uiText.Append(this.InternalOrganisation.Name);
            }

            if (this.ExistRevenue)
            {
                uiText.Append(": ");
                uiText.Append(DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat));
                uiText.Append(" revenue trailing twelve months");
            }

            this.DisplayName = uiText.ToString();
        }
    }
}
