// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Derivable.cs" company="Allors bvba">
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
    using System;
    using System.Collections.Generic;

    using Allors.Meta;

    public static partial class DerivableExtensions
    {
        private static Dictionary<Type, RoleType[]> requiredRoleTypesByClass; 

        public static void BasePrepareDerivation(this Derivable @this, DerivablePrepareDerivation method)
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

        public static void BaseDerive(this Derivable @this, DerivableDerive method)
        {
            var derivation = method.Derivation;
            var @class = (Class)@this.Strategy.ObjectType;

            // TODO: Optimize
            foreach (var concreteRoleType in @class.ConcreteRoleTypes)
            {
                if (concreteRoleType.IsRequired)
                {
                    var roleType = concreteRoleType.RoleType;
                    derivation.Log.AssertExists(@this, roleType);
                }
            }
        }
    }

    public abstract partial class DerivablePrepareDerivation
    {
        public Derivation Derivation { get; set; }
    }

    public abstract partial class DerivableDerive
    {
        public IDerivation Derivation { get; set; }

        public DerivableDerive WithDerivation(IDerivation derivation)
        {
            this.Derivation = derivation;
            return this;
        }
    }

    public abstract partial class DerivableApplySecurityOnDerive
    {
        public Derivation Derivation { get; set; }
    }
}