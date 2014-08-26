// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleGreaterThan.cs" company="Allors bvba">
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
// <summary>
//   Defines the AllorsPredicateRoleGreaterThanValueMemory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Workspace.Memory
{
    using System;

    using Allors.Adapters;

    using Allors.Meta;

    internal sealed class RoleGreaterThan : Predicate
    {
        private readonly Extent extent;
        private readonly RoleType roleType;
        private readonly object compare;

        internal RoleGreaterThan(Extent extent, RoleType roleType, object compare)
        {
            extent.CheckForRoleType(roleType);
            CompositePredicateAssertions.ValidateRoleGreaterThan(roleType, compare);

            this.extent = extent;
            this.roleType = roleType;
            this.compare = compare;
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            object compareValue = this.compare;

            var compareRole = this.compare as RoleType;
            if (compareRole != null)
            {
                compareValue = strategy.GetInternalizedUnitRole(compareRole);
            }
            else
            {
                if (this.roleType.ObjectType is UnitType)
                {
                    compareValue = this.extent.Session.MemoryWorkspace.Internalize(this.compare, this.roleType);
                }
            }

            var comparable = strategy.GetInternalizedUnitRole(this.roleType) as IComparable;

            if (comparable == null)
            {
                return ThreeValuedLogic.Unknown;
            }

            return comparable.CompareTo(compareValue) > 0
                       ? ThreeValuedLogic.True
                       : ThreeValuedLogic.False;
        }
    }
}