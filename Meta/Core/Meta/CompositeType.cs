//------------------------------------------------------------------------------------------------- 
// <copyright file="CompositeType.cs" company="Allors bvba">
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
    using System.Linq;

    public abstract partial class CompositeType : ObjectType
    {
        public List<Interface> DerivedDirectSuperinterfaces = new List<Interface>();

        public CompositeType(Domain domain, Guid objectTypeId)
            : base(domain, objectTypeId)
        {
        }

        public abstract bool IsInterface { get; }

        public abstract bool IsClass { get; }

        /// <summary>
        /// Gets the direct super interfaces.
        /// </summary>
        /// <value>The direct super interfaces.</value>
        public IList<Interface> DirectSuperinterfaces
        {
            get
            {
                return this.DerivedDirectSuperinterfaces;
            }
        }

        /// <summary>
        /// A cache for the ids of the <see cref="RoleTypes"/>.
        /// </summary>
        private HashSet<ObjectType> concreteClassesCache;

        /// <summary>
        /// Contains this concrete class.
        /// </summary>
        /// <param name="objectType">
        /// The concrete class.
        /// </param>
        /// <returns>
        /// True if this contains the concrete class.
        /// </returns>
        public bool ContainsConcreteClass(ObjectType objectType)
        {
            return this.concreteClassesCache.Contains(objectType);
        }

        /// <summary>
        /// Adds the direct supertype.
        /// </summary>
        /// <param name="supertype">The supertype.</param>
        /// <returns>The inheritance.</returns> 
        public Inheritance AddDirectSupertype(Interface supertype)
        {
            if (supertype == null)
            {
                return null;
            }

            if (!this.IsValidSupertype(supertype))
            {
                throw new ArgumentException(supertype + " is not a valid supertype for " + this);
            }

            var inheritance = this.FindInheritanceWhereDirectSubtype(supertype);
            if (inheritance == null)
            {
                if (!supertype.IsUnit && !supertype.IsInterface)
                {
                    throw new ArgumentException("The inheritance " + this + "::" + supertype + " can not have a concrete superclass");
                }

                inheritance = new Inheritance(this.Domain, Guid.NewGuid());
                inheritance.Subtype = this;
                inheritance.Supertype = supertype;
            }

            return inheritance;
        }

        /// <summary>
        /// Gets the concrete sub classes or
        /// self if this is a concrete class.
        /// </summary>
        /// <value>The concrete classes.</value>
        public abstract IList<ObjectType> ConcreteClasses { get; }

        /// <summary>
        /// Derive concrete classes cache.
        /// </summary>
        internal void DeriveConcreteClassesCache()
        {
            this.concreteClassesCache = new HashSet<ObjectType>(this.ConcreteClasses);
        }

        /// <summary>
        /// Derive direct super interfaces.
        /// </summary>
        /// <param name="directSupertypes">The direct super types.</param>
        internal void DeriveDirectSuperinterface(HashSet<Interface> directSuperinterfaces)
        {
            directSuperinterfaces.Clear();
            foreach (var inheritance in this.Domain.Inheritances.Where(inheritance => this.Equals(inheritance.Subtype)))
            {
                directSuperinterfaces.Add(inheritance.Supertype);
            }

            this.DerivedDirectSuperinterfaces = new List<Interface>(directSuperinterfaces);
        }

        /// <summary>
        /// Derive super types.
        /// </summary>
        /// <param name="superTypes">The super types.</param>
        internal void DeriveSupertypes(HashSet<Interface> superTypes)
        {
            superTypes.Clear();
            this.DeriveSupertypesRecursively(this, superTypes);

            this.DerivedSupertypes = new List<ObjectType>(superTypes);
        }

        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="superTypes">The super types.</param>
        private void DeriveSupertypesRecursively(CompositeType type, HashSet<Interface> superTypes)
        {
            foreach (var directSupertype in this.DerivedDirectSuperinterfaces)
            {
                if (!Equals(directSupertype, type))
                {
                    superTypes.Add(directSupertype);
                    directSupertype.DeriveSupertypesRecursively(type, superTypes);
                }
            }
        }

    }
}