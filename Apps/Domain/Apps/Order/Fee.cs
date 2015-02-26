// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fee.cs" company="Allors bvba">
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

    public partial class Fee
    {
        public void AppsPrepareDerivation(ObjectPrepareDerivation method)
        {
            var derivation = method.Derivation;

            // TODO:
            if (derivation.ChangeSet.Associations.Contains(this.Id))
            {
                if (this.ExistOrderWhereFee)
                {
                    var salesOrder = (SalesOrder)this.OrderWhereFee;
                    derivation.AddDependency(this, salesOrder);
                }

                if (this.ExistInvoiceWhereFee)
                {
                    var salesInvoice = (Allors.Domain.SalesInvoice)this.InvoiceWhereFee;
                    derivation.AddDependency(this, salesInvoice);
                }
            }
        }

        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, Fees.Meta.Amount, Fees.Meta.Percentage);
            derivation.Log.AssertExistsAtMostOne(this, Fees.Meta.Amount, Fees.Meta.Percentage);

            this.DeriveDisplayName();
        }

        private void AppsDeriveDisplayName()
        {
            var cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            var currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();

            if (this.ExistOrderWhereFee)
            {
                var salesOrder = (SalesOrder)this.OrderWhereFee;
                var cultureInfoName = salesOrder.Locale.Name;

                cultureInfo = new CultureInfo(cultureInfoName, false);
                currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = salesOrder.TakenByInternalOrganisation.PreferredCurrency.Symbol;
            }

            if (this.ExistInvoiceWhereFee)
            {
                var salesInvoice = (Allors.Domain.SalesInvoice)this.InvoiceWhereFee;
                var cultureInfoName = salesInvoice.Locale.Name;

                cultureInfo = new CultureInfo(cultureInfoName, false);
                currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = salesInvoice.BilledFromInternalOrganisation.PreferredCurrency.Symbol;
            }

            var uiText = new StringBuilder();

            uiText.Append("Fee: ");

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