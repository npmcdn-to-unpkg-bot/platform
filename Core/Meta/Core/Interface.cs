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
// <summary>Defines the IObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed partial class Interface : Composite, IInterface
    {
        private LazySet<Composite> derivedDirectSubtypes;

        private LazySet<Composite> derivedSubtypes;

        private LazySet<Class> derivedSubclasses;

        private LazySet<Class> derivedLeafClasses;

        private Class derivedExclusiveLeafClass;

        internal Interface(Domain domain, Guid id)
            : base(domain, id)
        {
            domain.OnInterfaceCreated(this);
        }

        #region Exist
        public bool ExistSubclasses
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSubclasses.Count > 0;
            }
        }

        public bool ExistSubtypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSubtypes.Count > 0;
            }
        }

        public override bool ExistLeafClasses
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedLeafClasses.Count > 0;
            }
        }

        #endregion

        IEnumerable<IClass> IInterface.Subclasses 
        {
            get
            {
                return this.Subclasses;
            }
        }

        /// <summary>
        /// Gets the subclasses.
        /// </summary>
        /// <value>The subclasses.</value>
        public IEnumerable<Class> Subclasses
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSubclasses;
            }
        }

        /// <summary>
        /// Gets the sub types.
        /// </summary>
        /// <value>The super types.</value>
        public IEnumerable<Composite> Subtypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSubtypes;
            }
        }

        public override IEnumerable<Class> LeafClasses
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedLeafClasses;
            }
        }

        public override Class ExclusiveLeafClass
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedExclusiveLeafClass;
            }
        }

        #region Contains

        /// <summary>
        /// Contains this concrete class.
        /// </summary>
        /// <param name="objectType">
        /// The concrete class.
        /// </param>
        /// <returns>
        /// True if this contains the concrete class.
        /// </returns>
        public override bool ContainsLeafClass(IClass objectType)
        {
            this.MetaPopulation.Derive();
            return this.derivedLeafClasses.Contains(objectType);
        }

        #endregion

        /// <summary>
        /// Derive direct sub type derivations.
        /// </summary>
        /// <param name="directSubtypes">The direct super types.</param>
        internal void DeriveDirectSubtypes(HashSet<Composite> directSubtypes)
        {
            directSubtypes.Clear();
            foreach (var inheritance in this.MetaPopulation.Inheritances.Where(inheritance => this.Equals(inheritance.Supertype)))
            {
                directSubtypes.Add(inheritance.Subtype);
            }

            this.derivedDirectSubtypes = new LazySet<Composite>(directSubtypes);
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
                if (subType is IClass)
                {
                    subClasses.Add((Class)subType);
                }
            }

            this.derivedSubclasses = new LazySet<Class>(subClasses);
        }

        /// <summary>
        /// Derive sub types.
        /// </summary>
        /// <param name="subTypes">The super types.</param>
        internal void DeriveSubtypes(HashSet<Composite> subTypes)
        {
            subTypes.Clear();
            this.DeriveSubtypesRecursively(this, subTypes);

            this.derivedSubtypes = new LazySet<Composite>(subTypes);
        }

        /// <summary>
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        internal void DeriveExclusiveLeafClass()
        {
            this.derivedExclusiveLeafClass = this.derivedLeafClasses.Count == 1 ? this.derivedLeafClasses.First() : null;
        }

        /// <summary>
        /// Derive root class for classes.
        /// </summary>
        internal void DeriveLeafClasses()
        {
            this.derivedLeafClasses = new LazySet<Class>(this.derivedSubclasses);
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
                    if (directSubtype is IInterface)
                    {
                        ((Interface)directSubtype).DeriveSubtypesRecursively(this, subTypes);
                    }
                }
            }
        }
    }
}