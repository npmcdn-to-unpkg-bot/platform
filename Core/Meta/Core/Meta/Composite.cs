//------------------------------------------------------------------------------------------------- 
// <copyright file="Composite.cs" company="Allors bvba">
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
    
    public abstract partial class Composite : ObjectType, IComposite
    {
        private LazySet<Interface> derivedDirectSupertypes;
        private LazySet<Interface> derivedSupertypes;

        private LazySet<AssociationType> derivedAssociationTypes;
        private LazySet<RoleType> derivedRoleTypes;
        private LazySet<MethodType> derivedMethodTypes;

        protected Composite(MetaPopulation metaPopulation)
            : base(metaPopulation)
        {
        }

        public bool ExistExclusiveClass
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.ExclusiveSubclass != null;
            }
        }

        public abstract bool ExistClass { get; }

        public bool ExistDirectSupertypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedDirectSupertypes.Count > 0;
            }
        }

        public bool ExistSupertypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSupertypes.Count > 0;
            }
        }

        public bool ExistAssociationTypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedAssociationTypes.Count > 0;
            }
        }

        public bool ExistRoleTypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedAssociationTypes.Count > 0;
            }
        }

        public bool ExistMethodTypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedMethodTypes.Count > 0;
            }
        }

        IClass IComposite.ExclusiveClass
        {
            get
            {
                return this.ExclusiveSubclass;
            }
        }

        /// <summary>
        /// Gets the exclusive concrete subclass.
        /// </summary>
        /// <value>The exclusive concrete subclass.</value>
        public abstract Class ExclusiveSubclass { get; }

        IEnumerable<IClass> IComposite.Classes
        {
            get
            {
                return this.Classes;
            }
        } 

        /// <summary>
        /// Gets the root classes.
        /// </summary>
        /// <value>The root classes.</value>
        public abstract IEnumerable<Class> Classes { get; }

        IEnumerable<IInterface> IComposite.DirectSupertypes
        {
            get
            {
                return this.DirectSupertypes;
            }
        }

        /// <summary>
        /// Gets the direct super types.
        /// </summary>
        /// <value>The super types.</value>
        public IEnumerable<Interface> DirectSupertypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedDirectSupertypes;
            }
        }

        IEnumerable<IInterface> IComposite.Supertypes
        {
            get
            {
                return this.Supertypes;
            }
        }

        /// <summary>
        /// Gets the super types.
        /// </summary>
        /// <value>The super types.</value>
        public IEnumerable<Interface> Supertypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSupertypes;
            }
        }
        
        IEnumerable<IAssociationType> IComposite.AssociationTypes 
        {
            get
            {
                return this.AssociationTypes;
            }
        }

        /// <summary>
        /// Gets the associations.
        /// </summary>
        /// <value>The associations.</value>
        public IEnumerable<AssociationType> AssociationTypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedAssociationTypes;
            }
        }

        public IEnumerable<AssociationType> ExclusiveAssociationTypes
        {
            get
            {
                return this.AssociationTypes.Where(associationType => this.Equals(associationType.RoleType.ObjectType)).ToArray();
            }
        }

        IEnumerable<IRoleType> IComposite.RoleTypes
        {
            get
            {
                return this.RoleTypes;
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public IEnumerable<RoleType> RoleTypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedRoleTypes;
            }
        }

        public IEnumerable<RoleType> UnitRoleTypes
        {
            get
            {
                return this.RoleTypes.Where(roleType => roleType.ObjectType.IsUnit).ToArray();
            }
        }

        public IEnumerable<RoleType> CompositeRoleTypes
        {
            get
            {
                return this.RoleTypes.Where(roleType => roleType.ObjectType.IsComposite).ToArray();
            }
        }

        public IEnumerable<RoleType> ExclusiveRoleTypes
        {
            get
            {
                return this.RoleTypes.Where(roleType => this.Equals(roleType.AssociationType.ObjectType)).ToArray();
            }
        }

        /// <summary>
        /// Gets the method types.
        /// </summary>
        /// <value>The method types.</value>
        public IEnumerable<MethodType> MethodTypes
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedMethodTypes;
            }
        }

        public IEnumerable<MethodType> ExclusiveMethodTypes
        {
            get
            {
                return this.MethodTypes.Where(methodType => this.Equals(methodType.ObjectType)).ToArray();
            }
        }

        public bool ExistSupertype(IInterface @interface)
        {
            this.MetaPopulation.Derive();
            return this.derivedSupertypes.Contains(@interface);
        }

        public bool ExistAssociationType(IAssociationType associationType)
        {
            this.MetaPopulation.Derive();
            return this.derivedAssociationTypes.Contains(associationType);
        }

        public bool ExistRoleType(IRoleType roleType)
        {
            this.MetaPopulation.Derive();
            return this.derivedRoleTypes.Contains(roleType);
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
        public abstract bool IsAssignableFrom(IClass objectType);

        /// <summary>
/// Derive direct super type derivations.
/// </summary>
/// <param name="directSupertypes">The direct super types.</param>
        internal void DeriveDirectSupertypes(HashSet<Interface> directSupertypes)
        {
            directSupertypes.Clear();
            foreach (var inheritance in this.MetaPopulation.Inheritances.Where(inheritance => this.Equals(inheritance.Subtype)))
            {
                directSupertypes.Add(inheritance.Supertype);
            }

            this.derivedDirectSupertypes = new LazySet<Interface>(directSupertypes);
        }

        /// <summary>
        /// Derive super types.
        /// </summary>
        /// <param name="superTypes">The super types.</param>
        internal void DeriveSupertypes(HashSet<Interface> superTypes)
        {
            superTypes.Clear();

            this.DeriveSupertypesRecursively(this, superTypes);

            this.derivedSupertypes = new LazySet<Interface>(superTypes);
        }

        /// <summary>
        /// Derive method types.
        /// </summary>
        /// <param name="methodTypes">
        /// The method types.
        /// </param>
        internal void DeriveMethodTypes(HashSet<MethodType> methodTypes)
        {
            methodTypes.Clear();
            foreach (var methodType in this.MetaPopulation.MethodTypes.Where(m => this.Equals(m.ObjectType)))
            {
                methodTypes.Add(methodType);
            }

            foreach (var superType in this.Supertypes)
            {
                var type = superType;
                foreach (var methodType in this.MetaPopulation.MethodTypes.Where(m => type.Equals(m.ObjectType)))
                {
                    methodTypes.Add(methodType);
                }
            }

            this.derivedMethodTypes = new LazySet<MethodType>(methodTypes);
        }

        /// <summary>
        /// Derive role types.
        /// </summary>
        /// <param name="roleTypes">The role types.</param>
        /// <param name="roleTypesByAssociationObjectType">RoleTypes grouped by the ObjectType of the Association</param>
        internal void DeriveRoleTypes(HashSet<RoleType> roleTypes, Dictionary<ObjectType, HashSet<RoleType>> roleTypesByAssociationObjectType)
        {
            roleTypes.Clear();

            HashSet<RoleType> classRoleTypes;
            if (roleTypesByAssociationObjectType.TryGetValue(this, out classRoleTypes))
            {
                roleTypes.UnionWith(classRoleTypes);
            }

            foreach (var superType in this.Supertypes)
            {
                HashSet<RoleType> superTypeRoleTypes;
                if (roleTypesByAssociationObjectType.TryGetValue(superType, out superTypeRoleTypes))
                {
                    roleTypes.UnionWith(superTypeRoleTypes);
                }
            }

            this.derivedRoleTypes = new LazySet<RoleType>(roleTypes);
        }

        /// <summary>
        /// Derive association types.
        /// </summary>
        /// <param name="associations">The associations.</param>
        /// <param name="relationTypesByRoleObjectType">AssociationTypes grouped by the ObjectType of the Role</param>
        internal void DeriveAssociationTypes(HashSet<AssociationType> associations, Dictionary<ObjectType, HashSet<AssociationType>> relationTypesByRoleObjectType)
        {
            associations.Clear();
            
            HashSet<AssociationType> classAssociationTypes;
            if (relationTypesByRoleObjectType.TryGetValue(this, out classAssociationTypes))
            {
                associations.UnionWith(classAssociationTypes);
            }

            foreach (var superType in this.Supertypes)
            {
                HashSet<AssociationType> interfaceAssociationTypes;
                if (relationTypesByRoleObjectType.TryGetValue(superType, out interfaceAssociationTypes))
                {
                    associations.UnionWith(interfaceAssociationTypes);
                }
            }

            this.derivedAssociationTypes = new LazySet<AssociationType>(associations);
        }
        
        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="superTypes">The super types.</param>
        private void DeriveSupertypesRecursively(ObjectType type, HashSet<Interface> superTypes)
        {
            foreach (var directSupertype in this.derivedDirectSupertypes)
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