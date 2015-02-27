// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Object.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Meta;

    public static partial class ObjectExtensions
    {
        private static readonly Dictionary<string, RoleType[]> RequiredRoleTypesByClassName = new Dictionary<string, RoleType[]>();
        private static readonly Dictionary<string, RoleType[]> UniqueRoleTypesByClassName = new Dictionary<string, RoleType[]>(); 

        public static void BasePrepareDerivation(this Object @this, ObjectPrepareDerivation method)
        {
            var derivation = method.Derivation;
            var changeSet = derivation.ChangeSet;
            if (derivation.IsForced(@this.Id) || changeSet.Associations.Contains(@this.Id) || changeSet.Created.Contains(@this.Id))
            {
                if (!derivation.DerivedObjects.Contains(@this))
                {
                    derivation.AddDerivable(@this);
                }
            }
        }

        public static void BaseDerive(this Object @this, ObjectDerive method)
        {
            var derivation = method.Derivation;
            var @class = (Class)@this.Strategy.ObjectType;

            // Required
            RoleType[] requiredRoleTypes;
            if (!RequiredRoleTypesByClassName.TryGetValue(@class.Name, out requiredRoleTypes))
            {
                requiredRoleTypes = @class.ConcreteRoleTypes
                    .Where(concreteRoleType => concreteRoleType.IsRequired)
                    .Select(concreteRoleType => concreteRoleType.RoleType)
                    .ToArray();

                RequiredRoleTypesByClassName[@class.Name] = requiredRoleTypes;
            }

            foreach (var roleType in requiredRoleTypes)
            {
                derivation.Log.AssertExists(@this, roleType);
            }

            // Unique
            RoleType[] uniqueRoleTypes;
            if (!UniqueRoleTypesByClassName.TryGetValue(@class.Name, out uniqueRoleTypes))
            {
                uniqueRoleTypes = @class.ConcreteRoleTypes
                    .Where(concreteRoleType => concreteRoleType.IsUnique)
                    .Select(concreteRoleType => concreteRoleType.RoleType)
                    .ToArray();

                UniqueRoleTypesByClassName[@class.Name] = uniqueRoleTypes;
            }

            foreach (var roleType in uniqueRoleTypes)
            {
                derivation.Log.AssertIsUnique(@this, roleType);
            }
        }
    }

    public abstract partial class ObjectPrepareDerivation
    {
        public Derivation Derivation { get; set; }
    }

    public abstract partial class ObjectDerive
    {
        public IDerivation Derivation { get; set; }

        public ObjectDerive WithDerivation(IDerivation derivation)
        {
            this.Derivation = derivation;
            return this;
        }
    }

    public abstract partial class ObjectApplySecurityOnDerive
    {
        public Derivation Derivation { get; set; }
    }
}