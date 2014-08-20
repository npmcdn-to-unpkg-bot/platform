// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleUnitEquals.cs" company="Allors bvba">
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

namespace Allors.Adapters.Database.Memory
{
    using System;
    using Allors.Meta;

    internal sealed class RoleUnitEquals : Predicate
    {
        private readonly ExtentFiltered extent;
        private readonly RoleType roleType;
        private readonly object equals;

        internal RoleUnitEquals(ExtentFiltered extent, RoleType roleType, object equals)
        {
            extent.CheckForRoleType(roleType);
            CompositePredicateAssertions.ValidateRoleEquals(roleType, equals);

            this.extent = extent;
            this.roleType = roleType;
            if (equals is Enum)
            {
                if (roleType.ObjectType.IsInteger)
                {
                    this.equals = (int)equals;
                }
                else 
                {
                    throw new Exception("Role Object Type " + roleType.ObjectType.Name + " doesn't support enumerations.");
                }
            }
            else
            {
                this.equals = equals;
            }
        }

        internal override ThreeValuedLogic Evaluate(Strategy strategy)
        {
            var value = strategy.GetInternalizedUnitRole(this.roleType);

            if (value == null)
            {
                return ThreeValuedLogic.Unknown;
            }

            var equalsValue = this.equals;

            if (this.equals is RoleType)
            {
                var equalsRole = (RoleType)this.equals;
                equalsValue = strategy.GetInternalizedUnitRole(equalsRole);
            }
            else
            {
                if (this.roleType.ObjectType.IsUnit)
                {
                    equalsValue = this.extent.Session.MemoryDatabase.Internalize(this.equals, this.roleType);
                }
            }

            if (equalsValue == null)
            {
                return ThreeValuedLogic.False;
            }

            return value.Equals(equalsValue)
                       ? ThreeValuedLogic.True
                       : ThreeValuedLogic.False;
        }
    }
}