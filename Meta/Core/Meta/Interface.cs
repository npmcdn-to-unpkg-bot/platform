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
    using System.Linq;

    /// <summary>
    /// An <see cref="ObjectType"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public partial class Interface : CompositeType
    {
        public List<CompositeType> DerivedDirectSubtypes = new List<CompositeType>();

        public List<CompositeType> DerivedSubtypes = new List<CompositeType>();

        public List<Class> DerivedSubclasses = new List<Class>();

        private List<Class> derivedRootClasses = new List<Class>();

        /// <summary>
        /// A cache for the ids of the <see cref="RoleTypes"/>.
        /// </summary>
        private HashSet<ObjectType> rootClassesCache;

        private Class derivedExclusiveRootClass;

        public Interface(Domain domain, Guid id)
            : base(domain, id)
        {
            this.Domain.OnInterfaceCreated(this);
        }

        /// <summary>
        /// Gets the subclasses.
        /// </summary>
        /// <value>The subclasses.</value>
        public IList<Class> Subclasses
        {
            get
            {
                return this.DerivedSubclasses;
            }
        }

        /// <summary>
        /// Gets the sub types.
        /// </summary>
        /// <value>The super types.</value>
        public IList<CompositeType> Subtypes
        {
            get
            {
                return this.DerivedSubtypes;
            }
        }

        public override IList<Class> RootClasses
        {
            get
            {
                return this.derivedRootClasses;
            }
        }

        public override Class ExclusiveRootClass
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
        /// Determines whether the specified super type is a valid super type.
        /// </summary>
        /// <param name="supertype">The super type.</param>
        /// <returns>
        ///  <c>true</c> if the specified super type is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidSupertype(ObjectType supertype)
        {
            if (!this.IsCyclicInheritance(supertype))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether adding the specified super type will result in a cycle.
        /// </summary>
        /// <param name="superType">The super type.</param>
        /// <returns>
        /// <c>true</c> if adding the specified super type will result in a cycle; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsCyclicInheritance(ObjectType superType)
        {
            if (this.Equals(superType))
            {
                return true;
            }

            foreach (var directSubtype in this.DerivedDirectSubtypes)
            {
                if (directSubtype is Interface)
                {
                    if (((Interface)directSubtype).IsCyclicInheritance(superType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Derive direct sub type derivations.
        /// </summary>
        /// <param name="directSubtypes">The direct super types.</param>
        internal void DeriveDirectSubtypes(HashSet<CompositeType> directSubtypes)
        {
            directSubtypes.Clear();
            foreach (var inheritance in this.Domain.Inheritances.Where(inheritance => this.Equals(inheritance.Supertype)))
            {
                directSubtypes.Add(inheritance.Subtype);
            }

            this.DerivedDirectSubtypes = new List<CompositeType>(directSubtypes);
        }

        /// <summary>
        /// Derive subclasses.
        /// </summary>
        /// <param name="subClasses">The sub classes.</param>
        internal void DeriveSubclasses(HashSet<Class> subClasses)
        {
            subClasses.Clear();
            foreach (var subType in this.DerivedSubtypes)
            {
                if (subType is Class)
                {
                    subClasses.Add((Class)subType);
                }
            }

            this.DerivedSubclasses = new List<Class>(subClasses);
        }

        /// <summary>
        /// Derive sub types.
        /// </summary>
        /// <param name="subTypes">The super types.</param>
        internal void DeriveSubtypes(HashSet<CompositeType> subTypes)
        {
            subTypes.Clear();
            this.DeriveSubtypesRecursively(this, subTypes);

            this.DerivedSubtypes = new List<CompositeType>(subTypes);
        }

        /// <summary>
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        internal void DeriveExclusiveRootClass()
        {
            this.derivedExclusiveRootClass = null;
            if (this.derivedRootClasses.Count == 1)
            {
                this.derivedExclusiveRootClass = this.derivedRootClasses[0];
            }
        }

        /// <summary>
        /// Derive root class for classes.
        /// </summary>
        internal void DeriveRootClasses()
        {
            this.derivedRootClasses = this.DerivedSubclasses;
            this.rootClassesCache = new HashSet<ObjectType>(this.derivedRootClasses);
        }

        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="superTypes">The super types.</param>
        private void DeriveSubtypesRecursively(ObjectType type, HashSet<CompositeType> subTypes)
        {
            foreach (var directSubtype in this.DerivedDirectSubtypes)
            {
                if (!Equals(directSubtype, type))
                {
                    subTypes.Add(directSubtype);
                    if (directSubtype is Interface)
                    {
                        ((Interface)directSubtype).DeriveSubtypesRecursively(this, subTypes);
                    }
                }
            }
        }
    }
}