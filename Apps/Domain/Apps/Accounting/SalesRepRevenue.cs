// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesRepRevenue.cs" company="Allors bvba">
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

    

    public partial class SalesRepRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        protected override void AppsDerive(IDerivation derivation)
        {
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

            var salesRepPartiesRevenue = this.SalesRep.SalesRepPartyRevenuesWhereSalesRep;
            salesRepPartiesRevenue.Filter.AddEquals(SalesRepPartyRevenues.Meta.InternalOrganisation, this.InternalOrganisation);
            salesRepPartiesRevenue.Filter.AddEquals(SalesRepPartyRevenues.Meta.Year, this.Year);
            salesRepPartiesRevenue.Filter.AddEquals(SalesRepPartyRevenues.Meta.Month, this.Month);

            foreach (SalesRepPartyRevenue salesRepPartyRevenue in salesRepPartiesRevenue)
            {
                this.Revenue += salesRepPartyRevenue.Revenue;
            }

            var months = ((DateTime.Now.Year - this.Year) * 12) + DateTime.Now.Month - this.Month;
            if (months <= 12)
            {
                var histories = this.SalesRep.SalesRepRevenueHistoriesWhereSalesRep;
                histories.Filter.AddEquals(SalesRepRevenueHistories.Meta.InternalOrganisation, this.InternalOrganisation);
                var history = histories.First ?? new SalesRepRevenueHistoryBuilder(this.Session)
                                                     .WithCurrency(this.Currency)
                                                     .WithInternalOrganisation(this.InternalOrganisation)
                                                     .WithSalesRep(this.SalesRep)
                                                     .Build();

                history.AppsDeriveRevenue();
            }
        }
    }
}
