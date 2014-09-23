// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesInvoice.v.cs" company="Allors bvba">
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

    public partial class SalesInvoice
    {
        public string SalesRepNames()
        {
            return this.AppsSalesRepNames;
        }

        public void CancelInvoice()
        {
            this.AppsCancelInvoice();
        }

        public void Send()
        {
            this.AppsSend();
        }

        public void WriteOff()
        {
            this.AppsWriteOff();
        }

        public void DeriveLocale(IDerivation derivation)
        {
            this.AppsDeriveLocale(derivation);
        }

        public void DeriveInvoiceTotals(IDerivation derivation)
        {
            this.AppsDeriveInvoiceTotals(derivation);
        }

        public void DeriveTemplate(IDerivation derivation)
        {
            this.AppsDeriveTemplate(derivation);
        }

        public void DeriveSalesOrderPaymentStatus(IDerivation derivation)
        {
            this.AppsDeriveSalesOrderPaymentStatus(derivation);
        }

        public void DeriveCustomers(IDerivation derivation)
        {
            this.AppsDeriveCustomers(derivation);
        }

        public void DeriveMarkupAndProfitMargin(IDerivation derivation)
        {
            this.AppsDeriveMarkupAndProfitMargin(derivation);
        }

        public void DeriveSalesReps(IDerivation derivation)
        {
            this.AppsDeriveSalesReps(derivation);
        }

        public void DeriveInvoiceItems(IDerivation derivation)
        {
            this.AppsDeriveInvoiceItems(derivation);
        }

        public void DeriveDisplayName()
        {
            this.AppsDeriveDisplayName();
        }

        public void DeriveSearchDataCharacterBoundaryText()
        {
            this.AppsDeriveSearchDataCharacterBoundaryText();
        }

        public void DeriveSearchDataWordBoundaryText()
        {
            this.AppsDeriveSearchDataWordBoundaryText();
        }

        public string ComposeDisplayName()
        {
            return this.AppsComposeDisplayName();
        }

        public string ComposeSearchDataCharacterBoundaryText()
        {
            return this.AppsComposeSearchDataCharacterBoundaryText();
        }

        public string ComposeSearchDataWordBoundaryText()
        {
            return this.AppsComposeSearchDataWordBoundaryText();
        }
    }
}