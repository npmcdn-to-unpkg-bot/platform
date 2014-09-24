// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountingTransactionDetail.cs" company="Allors bvba">
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
    public partial class AccountingTransactionDetail
    {
        public string AppsDebitCreditString
        {
            get
            {
               return this.Debit ? "Debit" : "Credit";
            }
        }

        protected override void AppsPrepareDerivation(IDerivation derivation)
        {
            base.AppsPrepareDerivation(derivation);

            if (this.ExistOrganisationGlAccountBalance && this.OrganisationGlAccountBalance.ExistAccountingPeriod)
            {
                derivation.AddDependency(this, this.OrganisationGlAccountBalance.AccountingPeriod);
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            

            derivation.Log.AssertExists(this, AccountingTransactionDetails.Meta.Amount);
            derivation.Log.AssertExists(this, AccountingTransactionDetails.Meta.Debit);
            derivation.Log.AssertExists(this, AccountingTransactionDetails.Meta.OrganisationGlAccountBalance);

            this.DisplayName = string.Format(
                "{0} {1} period {2} balance amount {3} account {4} {5} for {6}",
                this.ExistAmount ? this.Amount : 0,
                this.DebitCreditString(),
                this.ExistOrganisationGlAccountBalance ? this.OrganisationGlAccountBalance.ExistAccountingPeriod ? this.OrganisationGlAccountBalance.AccountingPeriod.DisplayName : null : null,
                this.ExistOrganisationGlAccountBalance ? this.OrganisationGlAccountBalance.ExistAmount ? this.OrganisationGlAccountBalance.Amount : 0 : 0,
                this.ExistOrganisationGlAccountBalance ? this.OrganisationGlAccountBalance.ExistOrganisationGlAccount ? this.OrganisationGlAccountBalance.OrganisationGlAccount.ExistGeneralLedgerAccount ? this.OrganisationGlAccountBalance.OrganisationGlAccount.GeneralLedgerAccount.AccountNumber : null : null : null,
                this.ExistOrganisationGlAccountBalance ? this.OrganisationGlAccountBalance.ExistOrganisationGlAccount ? this.OrganisationGlAccountBalance.OrganisationGlAccount.ExistGeneralLedgerAccount ? this.OrganisationGlAccountBalance.OrganisationGlAccount.GeneralLedgerAccount.Name : null : null : null,
                this.ExistOrganisationGlAccountBalance ? this.OrganisationGlAccountBalance.ExistOrganisationGlAccount ? this.OrganisationGlAccountBalance.OrganisationGlAccount.ExistInternalOrganisation ? this.OrganisationGlAccountBalance.OrganisationGlAccount.InternalOrganisation.Name : null : null : null);
        }
    }
}