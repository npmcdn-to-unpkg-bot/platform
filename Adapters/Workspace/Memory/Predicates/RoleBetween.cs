// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleBetween.cs" company="Allors bvba">
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
    using System;

    using Allors.Adapters;

    using Allors.Meta;

    internal sealed class RoleBetween : Predicate
    {
        private readonly Extent extent;
        private readonly MetaRole roleType;
        private readonly object first;
        private readonly object second;

        internal RoleBetween(Extent extent, MetaRole roleType, object first, object second)
        {
            extent.CheckForRoleType(roleType);
            CompositePredicateAssertions.ValidateRoleBetween(roleType, first, second);

            this.extent = extent;
            this.roleType = roleType;
            this.first = first;
            this.second = second;
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            var firstValue = this.first;
            var secondValue = this.second;

            var firstRole = this.first as MetaRole;
            if (firstRole != null)
            {
                firstValue = strategy.GetInternalizedUnitRole(firstRole);
            }
            else
            {
                if (this.roleType.ObjectType.IsUnit)
                {
                    firstValue = this.extent.Session.MemoryWorkspace.Internalize(this.first, this.roleType);
                }
            }

            var secondRole = this.second as MetaRole;
            if (secondRole != null)
            {
                secondValue = strategy.GetInternalizedUnitRole(secondRole);
            }
            else
            {
                if (this.roleType.ObjectType.IsUnit)
                {
                    secondValue = this.extent.Session.MemoryWorkspace.Internalize(this.second, this.roleType);
                }
            }

            var comparable = strategy.GetInternalizedUnitRole(this.roleType) as IComparable;

            if (comparable == null)
            {
                return ThreeValuedLogic.Unknown;
            }

            return (comparable.CompareTo(firstValue) >= 0 && comparable.CompareTo(secondValue) <= 0)
                       ? ThreeValuedLogic.True
                       : ThreeValuedLogic.False;
        }
    }
}