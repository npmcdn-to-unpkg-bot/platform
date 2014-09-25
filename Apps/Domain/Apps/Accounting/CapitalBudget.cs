// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CapitalBudget.cs" company="Allors bvba">
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
    public partial class CapitalBudget
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistCurrentObjectState)
            {
                this.CurrentObjectState = new BudgetObjectStates(this.DatabaseSession).Opened;
            }

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        protected override void AppsDerive(IDerivation derivation)
        {
            this.AppsBudgetDerive(derivation);

            derivation.Log.AssertExists(this, CapitalBudgets.Meta.FromDate);
            derivation.Log.AssertExists(this, CapitalBudgets.Meta.ThroughDate);
            derivation.Log.AssertExists(this, CapitalBudgets.Meta.Description);

            this.DisplayName = this.Description;

            this.SearchData.CharacterBoundaryText = this.DisplayName;
            this.SearchData.RemoveWordBoundaryText();
            
            this.PreviousObjectState = this.CurrentObjectState;
        }

        ObjectState Transitional.PreviousObjectState
        {
            get
            {
                return this.PreviousObjectState;
            }
        }

        ObjectState Transitional.CurrentObjectState
        {
            get
            {
                return this.CurrentObjectState;
            }
        }
    }
}
