// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesRepPartyProductCategoryRevenue.cs" company="Allors bvba">
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

    public partial class SalesRepPartyProductCategoryRevenue
    {
        public string RevenueAsCurrencyString()
        {
            return DecimalExtensions.AsCurrencyString(this.Revenue, this.InternalOrganisation.CurrencyFormat);
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            this.SalesRepName = this.SalesRep.DeriveDisplayName();

            this.AppsDeriveRevenue(derivation);

            this.AppsDeriveDisplayName(derivation);
        }

        private void AppsDeriveDisplayName(IDerivation derivation)
        {
            var uiText = new StringBuilder();

            if (this.ExistSalesRep)
            {
                uiText.Append(this.SalesRep.DeriveDisplayName());
            }

            if (this.ExistParty)
            {
                uiText.Append(", ");
                uiText.Append(this.Party.DeriveDisplayName());
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

        private void AppsDeriveRevenue(IDerivation derivation)
        {
            this.Revenue = 0;

            var lastDayOfMonth = new DateTime(this.Year, this.Month, 01).AddMonths(1).AddSeconds(-1).Day;

            var invoices = this.Party.SalesInvoicesWhereBillToCustomer;
            invoices.Filter.AddEquals(SalesInvoices.Meta.BilledFromInternalOrganisation, this.InternalOrganisation);
            invoices.Filter.AddNot().AddEquals(SalesInvoices.Meta.CurrentObjectState, new SalesInvoiceObjectStates(this.Strategy.DatabaseSession).WrittenOff);
            invoices.Filter.AddBetween(SalesInvoices.Meta.InvoiceDate, new DateTime(this.Year, this.Month, 01), new DateTime(this.Year, this.Month, lastDayOfMonth));

            foreach (SalesInvoice salesInvoice in invoices)
            {
                foreach (SalesInvoiceItem salesInvoiceItem in salesInvoice.SalesInvoiceItems)
                {
                    if (salesInvoiceItem.ExistProduct && salesInvoiceItem.ExistSalesRep && salesInvoiceItem.SalesRep.Equals(this.SalesRep))
                    {
                        if (this.ProductCategory.ProductsWhereProductCategory.Contains(salesInvoiceItem.Product))
                        {
                            this.Revenue += salesInvoiceItem.TotalExVat.Value;
                        }
                        else
                        {
                            if (salesInvoiceItem.Product.ProductCategoriesExpanded.Contains(this.ProductCategory))
                            {
                                this.Revenue += salesInvoiceItem.TotalExVat.Value;
                            }
                        }
                    }
                }
            }

            if (this.ProductCategory.ExistParents)
            {
                SalesRepPartyProductCategoryRevenues.AppsFindOrCreateAsDependable(this.Strategy.Session, this);
            }

            if (this.ExistParty && this.ExistProductCategory)
            {
                var salesRepProductCategoryRevenue = SalesRepProductCategoryRevenues.AppsFindOrCreateAsDependable(this.Strategy.Session, this);
                salesRepProductCategoryRevenue.Derive().Execute();

                var salesRepPartyRevenue = SalesRepPartyRevenues.AppsFindOrCreateAsDependable(this.Strategy.Session, this);
                salesRepPartyRevenue.Derive().Execute();
            }
        }
    }
}