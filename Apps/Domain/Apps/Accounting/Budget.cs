// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Budget.cs" company="Allors bvba">
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
    public static partial class BudgetExtensions
    {
        public static void AppsBudgetDerive(this Budget budget, IDerivation derivation)
        {
            if (budget.ExistCurrentObjectState && !budget.CurrentObjectState.Equals(budget.PreviousObjectState))
            {
                var currentStatus = new BudgetStatusBuilder(budget.Strategy.Session).WithBudgetObjectState(budget.CurrentObjectState).Build();
                budget.AddBudgetStatus(currentStatus);
                budget.CurrentBudgetStatus = currentStatus;
            }

            if (budget.ExistCurrentObjectState)
            {
                budget.CurrentObjectState.Process(budget);
            }
        }

        public static void AppsBudgetClose(this Budget budget)
        {
            budget.CurrentObjectState = new BudgetObjectStates(budget.Strategy.Session).Closed;
        }

        public static void AppsBudgetReopen(this Budget budget)
        {
            budget.CurrentObjectState = new BudgetObjectStates(budget.Strategy.Session).Opened;
        }
    }
}
