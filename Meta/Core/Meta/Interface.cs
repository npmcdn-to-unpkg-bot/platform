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
    public partial class Interface : Composite
    {
        private IList<Composite> derivedDirectSubtypes;

        private IList<Composite> derivedSubtypes;

        private IList<Class> derivedSubclasses;

        private IList<Class> derivedRootClasses;

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

        #region Exists
        public bool ExistSubclasses
        {
            get
            {
                return this.Subclasses.Count > 0;
            }
        }

        public bool ExistSubtypes
        {
            get
            {
                return this.Subtypes.Count > 0;
            }
        }
        #endregion

        /// <summary>
        /// Gets the subclasses.
        /// </summary>
        /// <value>The subclasses.</value>
        public IList<Class> Subclasses
        {
            get
            {
                return this.derivedSubclasses;
            }
        }

        /// <summary>
        /// Gets the sub types.
        /// </summary>
        /// <value>The super types.</value>
        public IList<Composite> Subtypes
        {
            get
            {
                return this.derivedSubtypes;
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
        /// Derive direct sub type derivations.
        /// </summary>
        /// <param name="directSubtypes">The direct super types.</param>
        internal void DeriveDirectSubtypes(HashSet<Composite> directSubtypes)
        {
            directSubtypes.Clear();
            foreach (var inheritance in this.Domain.Inheritances.Where(inheritance => this.Equals(inheritance.Supertype)))
            {
                directSubtypes.Add(inheritance.Subtype);
            }

            this.derivedDirectSubtypes = new List<Composite>(directSubtypes);
        }

        /// <summary>
        /// Derive subclasses.
        /// </summary>
        /// <param name="subClasses">The sub classes.</param>
        internal void DeriveSubclasses(HashSet<Class> subClasses)
        {
            subClasses.Clear();
            foreach (var subType in this.derivedSubtypes)
            {
                if (subType is Class)
                {
                    subClasses.Add((Class)subType);
                }
            }

            this.derivedSubclasses = new List<Class>(subClasses);
        }

        /// <summary>
        /// Derive sub types.
        /// </summary>
        /// <param name="subTypes">The super types.</param>
        internal void DeriveSubtypes(HashSet<Composite> subTypes)
        {
            subTypes.Clear();
            this.DeriveSubtypesRecursively(this, subTypes);

            this.derivedSubtypes = new List<Composite>(subTypes);
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
            this.derivedRootClasses = this.derivedSubclasses;
            this.rootClassesCache = new HashSet<ObjectType>(this.derivedRootClasses);
        }

        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="subTypes">The super types.</param>
        private void DeriveSubtypesRecursively(ObjectType type, HashSet<Composite> subTypes)
        {
            foreach (var directSubtype in this.derivedDirectSubtypes)
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