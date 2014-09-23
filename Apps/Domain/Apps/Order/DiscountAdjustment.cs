// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscountAdjustment.cs" company="Allors bvba">
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
    using System.Globalization;
    using System.Text;

    using Allors.Domain;

    public partial class DiscountAdjustment
    {
        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            // TODO:
            if (derivation.ChangeSet.Associations.Contains(this.Id))
            {
                if (this.ExistOrderItemWhereDiscountAdjustment)
                {
                    var salesOrderItem = (SalesOrderItem)this.OrderItemWhereDiscountAdjustment;
                    derivation.AddDependency(this, salesOrderItem);
                }

                if (this.ExistOrderWhereDiscountAdjustment)
                {
                    var salesOrder = (SalesOrder)this.OrderWhereDiscountAdjustment;
                    derivation.AddDependency(this, salesOrder);
                }

                if (this.ExistInvoiceItemWhereDiscountAdjustment)
                {
                    var salesInvoiceItem = (Allors.Domain.SalesInvoiceItem)this.InvoiceItemWhereDiscountAdjustment;
                    derivation.AddDependency(this, salesInvoiceItem);
                }

                if (this.ExistInvoiceWhereDiscountAdjustment)
                {
                    var salesInvoice = (Allors.Domain.SalesInvoice)this.InvoiceWhereDiscountAdjustment;
                    derivation.AddDependency(this, salesInvoice);
                }
            }        
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            base.AppsDerive(derivation);

            derivation.Log.AssertAtLeastOne(this, DiscountAdjustments.Meta.Amount, DiscountAdjustments.Meta.Percentage);
            derivation.Log.AssertExistsAtMostOne(this, DiscountAdjustments.Meta.Amount, DiscountAdjustments.Meta.Percentage);

            this.DeriveDisplayName();
        }

        private void AppsDeriveDisplayName()
        {
            var cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            var currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();

            if (this.ExistOrderItemWhereDiscountAdjustment)
            {
                var salesOrderItem = (SalesOrderItem)this.OrderItemWhereDiscountAdjustment;
                var cultureInfoName = salesOrderItem.SalesOrderWhereSalesOrderItem.Locale.Name;

                cultureInfo = new CultureInfo(cultureInfoName, false);
                currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = salesOrderItem.SalesOrderWhereSalesOrderItem.TakenByInternalOrganisation.PreferredCurrency.Symbol;
            }

            if (this.ExistOrderWhereDiscountAdjustment)
            {
                var salesOrder = (SalesOrder)this.OrderWhereDiscountAdjustment;
                var cultureInfoName = salesOrder.Locale.Name;

                cultureInfo = new CultureInfo(cultureInfoName, false);
                currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = salesOrder.TakenByInternalOrganisation.PreferredCurrency.Symbol;
            }

            if (this.ExistInvoiceItemWhereDiscountAdjustment)
            {
                var salesInvoiceItem = (Allors.Domain.SalesInvoiceItem)this.InvoiceItemWhereDiscountAdjustment;
                var cultureInfoName = salesInvoiceItem.SalesInvoiceWhereSalesInvoiceItem.Locale.Name;

                cultureInfo = new CultureInfo(cultureInfoName, false);
                currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = salesInvoiceItem.SalesInvoiceWhereSalesInvoiceItem.BilledFromInternalOrganisation.PreferredCurrency.Symbol;
            }

            if (this.ExistInvoiceWhereDiscountAdjustment)
            {
                var salesInvoice = (Allors.Domain.SalesInvoice)this.InvoiceWhereDiscountAdjustment;
                var cultureInfoName = salesInvoice.Locale.Name;

                cultureInfo = new CultureInfo(cultureInfoName, false);
                currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = salesInvoice.BilledFromInternalOrganisation.PreferredCurrency.Symbol;
            }

            var uiText = new StringBuilder();

            uiText.Append("Discount: ");

            if (this.ExistAmount)
            {
                uiText.Append(this.Amount.ToString("C", currencyFormat));
            }
            else
            {
                if (this.ExistPercentage)
                {
                    uiText.Append(this.Percentage.ToString("##.##"));
                    uiText.Append("%");
                }
            }

            this.DisplayName = uiText.ToString();
        }
    }
}