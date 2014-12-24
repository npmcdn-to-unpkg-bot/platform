// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequirementBudgetAllocation.cs" company="Allors bvba">
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
    public partial class RequirementBudgetAllocation
    {
        protected override void AppsOnPostBuild(IObjectBuilder builder)
        {
            base.AppsOnPostBuild(builder);

            if (!this.ExistSearchData)
            {
                this.SearchData = new SearchDataBuilder(this.Session).Build();
            }
        }

        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, RequirementBudgetAllocations.Meta.Amount);
            derivation.Log.AssertExists(this, RequirementBudgetAllocations.Meta.BudgetItem);
            derivation.Log.AssertExists(this, RequirementBudgetAllocations.Meta.Requirement);

            this.DisplayName = string.Format(
                "{0} from budget item {1} - {2} allocated to {3}",
                this.ExistAmount ? this.Amount : 0,
                this.ExistBudgetItem ? this.BudgetItem.Purpose : null,
                this.ExistBudgetItem ? this.BudgetItem.Amount : 0,
                this.ExistRequirement ? this.Requirement.Description : null);

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistRequirement ? this.Requirement.Description : null,
                this.ExistBudgetItem ? this.BudgetItem.Purpose : null);

            var wordBoundaryText = string.Format(
                "{0}",
                this.ExistAmount ? this.Amount : 0);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
        }
    }
}
