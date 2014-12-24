// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkEffortInventoryAssignment.cs" company="Allors bvba">
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
    public partial class WorkEffortInventoryAssignment
    {
        public void AppsDerive(DerivableDerive method)
        {
            var derivation = method.Derivation;

            derivation.Log.AssertExists(this, WorkEffortInventoryAssignments.Meta.Assignment);
            derivation.Log.AssertExists(this, WorkEffortInventoryAssignments.Meta.InventoryItem);

            this.DisplayName = string.Format(
                "{0} assigned to {1}",
                this.ExistInventoryItem ? this.InventoryItem.ComposeDisplayName() : null,
                this.ExistAssignment ? this.Assignment.ComposeDisplayName() : null);
        }
    }
}