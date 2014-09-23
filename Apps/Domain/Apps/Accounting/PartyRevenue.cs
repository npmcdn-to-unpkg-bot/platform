// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PartyRevenue.cs" company="Allors bvba">
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

    public partial class PartyRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        internal void AppsDeriveRevenue()
        {
            this.RemoveRevenue();

            if (this.Year != null && this.Month != null)
            {
                var year = this.Year.Value;
                var month = this.Month.Value;

                this.Revenue = 0;

                var lastDayOfMonth = new DateTime(year, month, 01).AddMonths(1).AddSeconds(-1).Day;

                var invoices = this.Party.SalesInvoicesWhereBillToCustomer;
                invoices.Filter.AddEquals(SalesInvoices.Meta.BilledFromInternalOrganisation, this.InternalOrganisation);
                invoices.Filter.AddNot().AddEquals(SalesInvoices.Meta.CurrentObjectState, new SalesInvoiceObjectStates(this.DatabaseSession).WrittenOff);
                invoices.Filter.AddBetween(SalesInvoices.Meta.InvoiceDate, new DateTime(year, month, 01), new DateTime(year, month, lastDayOfMonth));

                foreach (SalesInvoice salesInvoice in invoices)
                {
                    this.Revenue += salesInvoice.TotalExVat;
                }

                var months = ((DateTime.Now.Year - this.Year) * 12) + DateTime.Now.Month - this.Month;
                if (months <= 12)
                {
                    var histories = this.Party.PartyRevenueHistoriesWhereParty;
                    histories.Filter.AddEquals(PartyRevenueHistories.Meta.InternalOrganisation, this.InternalOrganisation);
                    var history = histories.First ?? new PartyRevenueHistoryBuilder(this.Session)
                                                         .WithCurrency(this.Currency)
                                                         .WithInternalOrganisation(this.InternalOrganisation)
                                                         .WithParty(this.Party)
                                                         .WithRevenue(0)
                                                         .Build();

                    history.AppsDeriveHistory();
                }
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            derivation.AddDependency(this.Party, this);
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            this.PartyName = this.Party.DeriveDisplayName();

            this.AppsDeriveDisplayName(derivation);

            this.AppsDeriveRevenue();
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistParty)
            {
                uiText.Append(this.Party.DeriveDisplayName());
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
    }
}
