// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductPurchasePrice.cs" company="Allors bvba">
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
    using System.Globalization;
    using System.Text;

    using Allors.Domain;

    public partial class ProductPurchasePrice
    {
        public NumberFormatInfo CurrencyFormat
        {
            get
            {
                var cultureInfo = new CultureInfo(this.SupplierOfferingWhereProductPurchasePrice.Supplier.Locale.Name, false);
                var currencyFormat = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
                currencyFormat.CurrencySymbol = this.Currency.Symbol;
                return currencyFormat;
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, ProductPurchasePrices.Meta.Price);
            derivation.Log.AssertExists(this, ProductPurchasePrices.Meta.Currency);
            derivation.Log.AssertExists(this, ProductPurchasePrices.Meta.FromDate);
            derivation.Log.AssertExists(this, ProductPurchasePrices.Meta.UnitOfMeasure);

            this.DeriveDisplayName();
        }

        private void AppsDeriveDisplayName()
        {
            StringBuilder uiText = new StringBuilder();

            uiText.Append("Purchase price: ");

            if (this.ExistPrice)
            {
                if (this.ExistCurrency)
                {
                    uiText.Append(this.Currency.Symbol);
                }

                uiText.Append(" ");
                uiText.Append(string.Format("{0:N2}", this.Price));
            }

            this.DisplayName = uiText.ToString();
        }
    }
}