// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaymentBudgetAllocation.cs" company="Allors bvba">
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
    public partial class PaymentBudgetAllocation
    {
        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, PaymentBudgetAllocations.Meta.Amount);
            derivation.Log.AssertExists(this, PaymentBudgetAllocations.Meta.BudgetItem);
            derivation.Log.AssertExists(this, PaymentBudgetAllocations.Meta.Payment);

            this.DisplayName = string.Format(
                "{0} from payment {1} allocated to {2} - {3}",
                this.ExistAmount ? this.Amount : 0,
                this.ExistPayment ? this.Payment.ComposeDisplayName() : null,
                this.ExistBudgetItem ? this.BudgetItem.Purpose : null,
                this.ExistBudgetItem ? this.BudgetItem.Amount : 0);
        }
    }
}