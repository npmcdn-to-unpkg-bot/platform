// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesChannelRevenue.cs" company="Allors bvba">
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

    public partial class SalesChannelRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;
            
            this.SalesChannelName = this.SalesChannel.Name;

            this.AppsDeriveRevenue();
        }

        private void AppsDeriveRevenue()
        {
            this.Revenue = 0;

            var lastDayOfMonth = new DateTime(this.Year, this.Month, 01).AddMonths(1).AddSeconds(-1).Day;

            var invoices = this.SalesChannel.SalesInvoicesWhereSalesChannel;
            invoices.Filter.AddEquals(SalesInvoices.Meta.BilledFromInternalOrganisation, this.InternalOrganisation);
            invoices.Filter.AddNot().AddEquals(SalesInvoices.Meta.CurrentObjectState, new SalesInvoiceObjectStates(this.Strategy.DatabaseSession).WrittenOff);
            invoices.Filter.AddBetween(SalesInvoices.Meta.InvoiceDate, new DateTime(this.Year, this.Month, 01), new DateTime(this.Year, this.Month, lastDayOfMonth));

            foreach (SalesInvoice salesInvoice in invoices)
            {
                this.Revenue += salesInvoice.TotalExVat;
            }

            var months = ((DateTime.UtcNow.Year - this.Year) * 12) + DateTime.UtcNow.Month - this.Month;
            if (months <= 12)
            {
                var histories = this.SalesChannel.SalesChannelRevenueHistoriesWhereSalesChannel;
                histories.Filter.AddEquals(SalesChannelRevenueHistories.Meta.InternalOrganisation, this.InternalOrganisation);
                var history = histories.First ?? new SalesChannelRevenueHistoryBuilder(this.Strategy.Session)
                                                     .WithCurrency(this.Currency)
                                                     .WithInternalOrganisation(this.InternalOrganisation)
                                                     .WithSalesChannel(this.SalesChannel)
                                                     .Build();
            }
        }
    }
}
