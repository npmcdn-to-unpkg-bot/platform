// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleOneContainedInExtent.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Workspace.Memory
{
    using Allors.Adapters;

    using Allors.Meta;
    using Allors.Populations;

    internal sealed class RoleOneContainedInExtent : Predicate
    {
        private readonly IRoleType roleType;
        private readonly Allors.Extent containingExtent;

        internal RoleOneContainedInExtent(Extent extent, IRoleType roleType, Allors.Extent containingExtent)
        {
            extent.CheckForRoleType(roleType);
            CompositePredicateAssertions.ValidateRoleContainedIn(roleType, containingExtent);

            this.roleType = roleType;
            this.containingExtent = containingExtent;
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            IObject roleStrategy = strategy.GetCompositeRole(this.roleType);

            if (roleStrategy == null)
            {
                return ThreeValuedLogic.False;
            }

            return this.containingExtent.Contains(roleStrategy)
                       ? ThreeValuedLogic.True
                       : ThreeValuedLogic.False;
        }
    }
}