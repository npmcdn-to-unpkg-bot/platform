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

    public partial class Interface : Composite
    {
        private LazySet<Composite> derivedDirectSubtypes;

        private LazySet<Composite> derivedSubtypes;

        private LazySet<IClass> derivedSubclasses;

        private LazySet<IClass> derivedLeafClasses;

        private IClass derivedExclusiveLeafClass;

        internal Interface(IDomain domain, Guid id)
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
        
        /// <summary>
        /// Gets the subclasses.
        /// </summary>
        /// <value>The subclasses.</value>
        public IEnumerable<IClass> Subclasses
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

        public override IEnumerable<IClass> LeafClasses
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedLeafClasses;
            }
        }

        public override IClass ExclusiveLeafClass
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
        public override bool ContainsLeafClass(IObjectType objectType)
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
        internal void DeriveSubclasses(HashSet<IClass> subClasses)
        {
            subClasses.Clear();
            foreach (var subType in this.derivedSubtypes)
            {
                if (subType is IClass)
                {
                    subClasses.Add((IClass)subType);
                }
            }

            this.derivedSubclasses = new LazySet<IClass>(subClasses);
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
            this.derivedLeafClasses = new LazySet<IClass>(this.derivedSubclasses);
        }

        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="subTypes">The super types.</param>
        private void DeriveSubtypesRecursively(IObjectType type, HashSet<Composite> subTypes)
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