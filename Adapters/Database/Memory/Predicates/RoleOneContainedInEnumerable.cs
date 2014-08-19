// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleOneContainedInEnumerable.cs" company="Allors bvba">
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

namespace Allors.R1.Adapters.Database.Memory
{
    using System.Collections.Generic;

    using Meta;

    internal sealed class RoleOneContainedInEnumerable : Predicate
    {
        private readonly RoleType roleType;
        private readonly IEnumerable<IObject> containingEnumerable;

        internal RoleOneContainedInEnumerable(ExtentFiltered extent, RoleType roleType, IEnumerable<IObject> containingEnumerable)
        {
            extent.CheckForRoleType(roleType);
            CompositePredicateAssertions.ValidateRoleContainedIn(roleType, containingEnumerable);

            this.roleType = roleType;
            this.containingEnumerable = containingEnumerable;
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            var containing = new HashSet<IObject>(this.containingEnumerable);
            IObject roleStrategy = strategy.GetCompositeRole(this.roleType);

            if (roleStrategy == null)
            {
                return ThreeValuedLogic.False;
            }

            return containing.Contains(roleStrategy)
                       ? ThreeValuedLogic.True
                       : ThreeValuedLogic.False;
        }
    }
}