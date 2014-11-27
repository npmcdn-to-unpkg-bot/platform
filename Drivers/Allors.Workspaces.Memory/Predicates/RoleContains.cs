// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleContains.cs" company="Allors bvba">
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

namespace Allors.Workspaces.Memory
{
    using Allors.Meta;
    using Allors.Populations;

    internal sealed class RoleContains : Predicate
    {
        private readonly IRoleType roleType;
        private readonly IObject containedObject;

        internal RoleContains(Extent extent, IRoleType roleType, IObject containedObject)
        {
            extent.CheckForRoleType(roleType);
            PredicateAssertions.ValidateRoleContains(roleType, containedObject);

            this.roleType = roleType;
            this.containedObject = containedObject;
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            var roles = strategy.GetCompositeRoles(this.roleType);
            if (roles != null)
            {
                return roles.Contains(this.containedObject) ? ThreeValuedLogic.True : ThreeValuedLogic.False;
            }

            return ThreeValuedLogic.False;
        }
    }
}