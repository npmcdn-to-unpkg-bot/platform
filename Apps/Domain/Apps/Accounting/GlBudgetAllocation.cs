// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlBudgetAllocation.cs" company="Allors bvba">
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
    public partial class GlBudgetAllocation
    {
        public void AppsDerive(ObjectDerive method)
        {
            this.DisplayName = string.Format(
                "{0}% of account {1} {2} allocated to {3} - {4}",
                this.ExistAllocationPercentage ? this.AllocationPercentage : 0,
                this.ExistGeneralLedgerAccount ? this.GeneralLedgerAccount.AccountNumber : null,
                this.ExistGeneralLedgerAccount ? this.GeneralLedgerAccount.Name : null,
                this.ExistBudgetItem ? this.BudgetItem.Purpose : null,
                this.ExistBudgetItem ? this.BudgetItem.Amount : 0);
        }
    }
}