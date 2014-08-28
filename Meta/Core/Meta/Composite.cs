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

    public abstract partial class Composite : ObjectType
    {
        private IList<Interface> derivedDirectSupertypes;

        private IList<Interface> derivedSupertypes;

        private IList<AssociationType> derivedAssociationTypes;

        private IList<RoleType> derivedRoleTypes;

        private IList<MethodType> derivedMethodTypes;

        /// <summary>
        /// A cache for the ids of the <see cref="AssociationTypes"/>.
        /// </summary>
        private Dictionary<Guid, object> associationIdsCache;

        /// <summary>
        /// A cache for the ids of the <see cref="RoleTypes"/>.
        /// </summary>
        private Dictionary<Guid, object> roleIdsCache;

        protected Composite(Domain domain, Guid id)
            : base(domain, id)
        {
        }

        /// <summary>
        /// Gets the exclusive concrete subclass.
        /// </summary>
        /// <value>The exclusive concrete subclass.</value>
        public abstract Class ExclusiveRootClass { get; }

        /// <summary>
        /// Gets the root classes.
        /// </summary>
        /// <value>The root classes.</value>
        public abstract IList<Class> RootClasses { get; }

        /// <summary>
        /// Gets the direct super types.
        /// </summary>
        /// <value>The super types.</value>
        public IList<Interface> DirectSupertypes
        {
            get
            {
                return this.derivedDirectSupertypes;
            }
        }

        /// <summary>
        /// Gets the super types.
        /// </summary>
        /// <value>The super types.</value>
        public IList<Interface> Supertypes
        {
            get
            {
                return this.derivedSupertypes;
            }
        }

        /// <summary>
        /// Gets the associations.
        /// </summary>
        /// <value>The associations.</value>
        public IList<AssociationType> AssociationTypes
        {
            get
            {
                return this.derivedAssociationTypes;
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public IList<RoleType> RoleTypes
        {
            get
            {
                return this.derivedRoleTypes;
            }
        }

        /// <summary>
        /// Gets the method types.
        /// </summary>
        /// <value>The method types.</value>
        public IList<MethodType> MethodTypes
        {
            get
            {
                return this.derivedMethodTypes;
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
        public abstract bool ContainsRootClass(ObjectType objectType);
        
        /// <summary>
        /// Derive direct super type derivations.
        /// </summary>
        /// <param name="directSupertypes">The direct super types.</param>
        internal void DeriveDirectSupertypes(HashSet<Interface> directSupertypes)
        {
            directSupertypes.Clear();
            foreach (var inheritance in this.Domain.Inheritances.Where(inheritance => this.Equals(inheritance.Subtype)))
            {
                directSupertypes.Add(inheritance.Supertype);
            }

            this.derivedDirectSupertypes = new List<Interface>(directSupertypes);
        }

        /// <summary>
        /// Derive super types.
        /// </summary>
        /// <param name="superTypes">The super types.</param>
        internal void DeriveSupertypes(HashSet<Interface> superTypes)
        {
            superTypes.Clear();
            this.DeriveSupertypesRecursively(this, superTypes);

            this.derivedSupertypes = new List<Interface>(superTypes);
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
            foreach (var methodType in this.Domain.MethodTypes.Where(m => this.Equals(m.ObjectType)))
            {
                methodTypes.Add(methodType);
            }

            foreach (var superType in this.Supertypes)
            {
                var type = superType;
                foreach (var methodType in this.Domain.MethodTypes.Where(m => type.Equals(m.ObjectType)))
                {
                    methodTypes.Add(methodType);
                }
            }

            this.derivedMethodTypes = new List<MethodType>(methodTypes);
        }

        /// <summary>
        /// Derive role types.
        /// </summary>
        /// <param name="roleTypes">The role types.</param>
        internal void DeriveRoleTypes(HashSet<RoleType> roleTypes)
        {
            roleTypes.Clear();
            foreach (var relationType in this.Domain.RelationTypes.Where(rel => this.Equals(rel.AssociationType.ObjectType)))
            {
                roleTypes.Add(relationType.RoleType);
            }

            foreach (var superType in this.Supertypes)
            {
                var type = superType;
                foreach (var relationType in this.Domain.RelationTypes.Where(rel => type.Equals(rel.AssociationType.ObjectType)))
                {
                    roleTypes.Add(relationType.RoleType);
                }
            }

            this.derivedRoleTypes = new List<RoleType>(roleTypes);
        }
        
        /// <summary>
        /// Derive association types.
        /// </summary>
        /// <param name="associations">The associations.</param>
        internal void DeriveAssociationTypes(HashSet<AssociationType> associations)
        {
            associations.Clear();
            foreach (var relationType in this.Domain.RelationTypes.Where(rel => this.Equals(rel.RoleType.ObjectType)))
            {
                associations.Add(relationType.AssociationType);
            }

            foreach (var superType in this.Supertypes)
            {
                var type = superType;
                foreach (var relationType in this.Domain.RelationTypes.Where(rel => type.Equals(rel.RoleType.ObjectType)))
                {
                    associations.Add(relationType.AssociationType);
                }
            }

            this.derivedAssociationTypes = new List<AssociationType>(associations);
        }

        /// <summary>
        /// Derive association ids cache.
        /// </summary>
        internal void DeriveAssociationIdsCache()
        {
            this.associationIdsCache = new Dictionary<Guid, object>();
            foreach (var containsAssociation in this.AssociationTypes)
            {
                this.associationIdsCache[containsAssociation.RelationType.Id] = null;
            }
        }

        /// <summary>
        /// Derive role ids cache.
        /// </summary>
        internal void DeriveRoleTypeIdsCache()
        {
            this.roleIdsCache = new Dictionary<Guid, object>();
            foreach (var containsRole in this.derivedRoleTypes)
            {
                this.roleIdsCache[containsRole.RelationType.Id] = null;
            }
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