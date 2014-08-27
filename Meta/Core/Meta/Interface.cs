//------------------------------------------------------------------------------------------------- 
// <copyright file="Interface.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the ObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An <see cref="ObjectType"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public partial class Interface : CompositeType
    {
        private List<Class> derivedRootClasses = new List<Class>();

        /// <summary>
        /// A cache for the ids of the <see cref="RoleTypes"/>.
        /// </summary>
        private HashSet<ObjectType> rootClassesCache;

        private Class derivedExclusiveRootClass;

        public Interface(Domain domain, Guid objectTypeId)
            : base(domain, objectTypeId)
        {
            this.Domain.OnInterfaceCreated(this);
        }

        public override List<Class> DerivedRootClasses
        {
            get
            {
                return this.derivedRootClasses;
            }
        }

        public override Class DerivedExclusiveRootClass
        {
            get
            {
                return this.derivedExclusiveRootClass;
            }
        }

        /// <summary>
        /// Contains this concrete class.
        /// </summary>
        /// <param name="objectType">
        /// The concrete class.
        /// </param>
        /// <returns>
        /// True if this contains the concrete class.
        /// </returns>
        public override bool ContainsRootClass(ObjectType objectType)
        {
            return this.rootClassesCache.Contains(objectType);
        }

        /// <summary>
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        internal void DeriveExclusiveRootClass()
        {
            this.derivedExclusiveRootClass = null;
            if (this.DerivedRootClasses.Count == 1)
            {
                this.derivedExclusiveRootClass = this.DerivedRootClasses[0];
            }
        }

        /// <summary>
        /// Derive root class for classes.
        /// </summary>
        internal void DeriveRootClasses()
        {
            this.derivedRootClasses = this.DerivedSubclasses;
            this.rootClassesCache = new HashSet<ObjectType>(this.DerivedRootClasses);
        }
    }
}