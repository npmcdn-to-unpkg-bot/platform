// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpenseEntry.cs" company="Allors bvba">
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
    public partial class ExpenseEntry
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

            derivation.Log.AssertExists(this, ExpenseEntries.Meta.Description);
            derivation.Log.AssertExists(this, ExpenseEntries.Meta.Amount);
            derivation.Log.AssertAtLeastOne(this, ExpenseEntries.Meta.WorkEffort, ExpenseEntries.Meta.EngagementItem);

            this.DisplayName = string.Format(
                "{0} {1} for {2}",
                this.ExistDescription ? this.Description : null,
                this.ExistAmount ? this.Amount : 0,
                this.ExistWorkEffort ? this.WorkEffort.ComposeDisplayName() : null);

            var characterBoundaryText = string.Format(
                "{0} {1}",
                this.ExistDescription ? this.Description : null,
                this.ExistWorkEffort ? this.WorkEffort.ComposeSearchDataCharacterBoundaryText() : null);

            var wordBoundaryText = this.ExistWorkEffort ? this.WorkEffort.ComposeSearchDataWordBoundaryText() : null;

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
            
        }
    }
}