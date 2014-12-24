// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrganisationGlAccountBalance.cs" company="Allors bvba">
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
    public partial class OrganisationGlAccountBalance
    {
        public void AppsPrepareDerivation(DerivablePrepareDerivation method)
        {
            var derivation = method.Derivation;

            if (this.ExistAccountingPeriod)
            {
                derivation.AddDependency(this, this.AccountingPeriod);
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            this.DisplayName = string.Format(
                "period {0} balance amount {1} account {2} {3} for {4}",
                this.ExistAccountingPeriod ? this.AccountingPeriod.DisplayName : null,
                this.ExistAmount ? this.Amount : 0,
                this.ExistOrganisationGlAccount ? this.OrganisationGlAccount.ExistGeneralLedgerAccount ? this.OrganisationGlAccount.GeneralLedgerAccount.AccountNumber : null : null,
                this.ExistOrganisationGlAccount ? this.OrganisationGlAccount.ExistGeneralLedgerAccount ? this.OrganisationGlAccount.GeneralLedgerAccount.Name : null : null,
                this.ExistOrganisationGlAccount ? this.OrganisationGlAccount.ExistInternalOrganisation ? this.OrganisationGlAccount.InternalOrganisation.Name : null : null);
        }
    }
}