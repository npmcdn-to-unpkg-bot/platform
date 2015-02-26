// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeEntry.cs" company="Allors bvba">
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
    public partial class TimeEntry
    {
        public void AppsDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertAtLeastOne(this, TimeEntries.Meta.WorkEffort, TimeEntries.Meta.EngagementItem);

            this.DisplayName = string.Format(
                "{0} {1} for {2}",
                this.ExistUnitOfMeasure ? this.UnitOfMeasure.Name : null,
                this.ExistCost ? this.Cost : 0,
                this.ExistWorkEffort ? this.WorkEffort.ComposeDisplayName() : null);

            var characterBoundaryText = this.ExistWorkEffort ? this.WorkEffort.ComposeSearchDataCharacterBoundaryText() : null;

            var wordBoundaryText = string.Format(
                "{0} {1} {2}",
                this.ExistCost ? this.Cost : 0,
                this.ExistUnitOfMeasure ? this.UnitOfMeasure.Name : null,
                this.ExistWorkEffort ? this.WorkEffort.ComposeSearchDataWordBoundaryText() : null);

            this.SearchData.CharacterBoundaryText = characterBoundaryText;
            this.SearchData.WordBoundaryText = wordBoundaryText;
            
        }
    }
}