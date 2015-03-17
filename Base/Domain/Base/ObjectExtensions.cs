// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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

namespace Allors
{
    using System;

    using Allors.Domain;
    using Allors.Meta;

    public static partial class ObjectExtensions
    {
        public static void BaseOnPostBuild(this Domain.Object @this, ObjectOnPostBuild method)
        {
            // TODO: Optimize
            foreach (var concreteRoleType in ((Class)@this.Strategy.ObjectType).ConcreteRoleTypes)
            {
                if (concreteRoleType.IsRequired)
                {
                    var roleType = concreteRoleType.RoleType;
                    var unit = roleType.ObjectType as IUnit;
                    if (unit != null && !@this.Strategy.ExistRole(roleType))
                    {
                        switch (unit.UnitTag)
                        {
                            case UnitTags.AllorsBoolean:
                                @this.Strategy.SetUnitRole(roleType, false);
                                break;
                            case UnitTags.AllorsDecimal:
                                @this.Strategy.SetUnitRole(roleType, 0m);
                                break;
                            case UnitTags.AllorsFloat:
                                @this.Strategy.SetUnitRole(roleType, 0d);
                                break;
                            case UnitTags.AllorsInteger:
                                @this.Strategy.SetUnitRole(roleType, 0);
                                break;
                            case UnitTags.AllorsUnique:
                                @this.Strategy.SetUnitRole(roleType, Guid.NewGuid());
                                break;
                        }
                    }
                }
            }
        }

        // TODO: move to security
        public static void AddCreatorSecurityToken(this Domain.Object @this)
        {
            var accessControlledObject = @this as AccessControlledObject;
            if (accessControlledObject != null)
            {
                var creator = new Users(@this.Strategy.Session).GetCurrentAuthenticatedUser();

                if (creator != null)
                {
                    accessControlledObject.AddSecurityToken(creator.OwnerSecurityToken);
                }
            }
        }
    }
}