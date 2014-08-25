//------------------------------------------------------------------------------------------------- 
// <copyright file="ObjectType.cs" company="Allors bvba">
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
    using System.Globalization;

    /// <summary>
    /// An <see cref="ObjectType"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public partial class ObjectType : MetaObject, IComparable
    {
        public List<AssociationType> DerivedExclusiveAssociationTypes = new List<AssociationType>();

        public List<ObjectType> DerivedExclusiveSuperinterfaces = new List<ObjectType>();

        public List<ObjectType> DerivedSubclasses = new List<ObjectType>();

        public List<AssociationType> DerivedAssociationTypes = new List<AssociationType>();

        public List<ObjectType> DerivedDirectSupertypes = new List<ObjectType>();

        public ObjectType DerivedDirectSuperclass;

        public string PluralName;

        public List<MethodType> DerivedMethodTypes = new List<MethodType>();

        public List<ObjectType> DerivedSuperinterfaces = new List<ObjectType>();

        public bool IsInterface;

        public List<ObjectType> DerivedSubinterfaces = new List<ObjectType>();

        public bool IsUnit;

        public ObjectType DerivedExclusiveConcreteLeafClass;

        public List<ObjectType> DerivedDirectSuperinterfaces = new List<ObjectType>();

        public List<RoleType> DerivedUnitRoleTypes = new List<RoleType>();

        public List<ObjectType> DerivedRootClasses = new List<ObjectType>();

        public int UnitTag;

        public List<RoleType> DerivedCompositeRoleTypes = new List<RoleType>();

        public List<ObjectType> DerivedSuperclasses = new List<ObjectType>();

        public List<RoleType> DerivedRoleTypes = new List<RoleType>();

        public string SingularName;

        public List<RoleType> DerivedExclusiveRoleTypes = new List<RoleType>();

        public List<ObjectType> DerivedSupertypes = new List<ObjectType>();

        // Domain -> ObjectType
        public Domain Domain { get; private set; }

        /// <summary>
        /// An empty array of object types.
        /// </summary>
        private static readonly ObjectType[] EmptyArray = new ObjectType[0];

        /// <summary>
        /// A cache for the ids of the <see cref="AssociationTypes"/>.
        /// </summary>
        private Dictionary<Guid, object> associationIdsCache;
        
        /// <summary>
        /// A cache for the ids of the <see cref="RoleTypes"/>.
        /// </summary>
        private Dictionary<Guid, object> roleIdsCache;

        /// <summary>
        /// A cache for the ids of the <see cref="RoleTypes"/>.
        /// </summary>
        private HashSet<ObjectType> concreteClassesCache;

        public ObjectType(Domain domain, Guid objectTypeId)
        {
            this.Domain = domain;
            this.Id = objectTypeId;
        }

        /// <summary>
        /// Gets the associations.
        /// </summary>
        /// <value>The associations.</value>
        public IList<AssociationType> AssociationTypes
        {
            get
            {
                return this.DerivedAssociationTypes;
            }
        }

        /// <summary>
        /// Gets the composite roles.
        /// </summary>
        /// <value>The composite roles.</value>
        public IList<RoleType> CompositeRoleTypes
        {
            get
            {
                return this.DerivedCompositeRoleTypes;
            }
        }

        /// <summary>
        /// Gets the concrete sub classes or
        /// self if this is a concrete class.
        /// </summary>
        /// <value>The concrete classes.</value>
        public ObjectType[] ConcreteClasses
        {
            get
            {
                if (!this.IsUnit && !this.IsInterface)
                {
                    ObjectType[] selfArray = { this };
                    return selfArray;
                }

                return this.Subclasses.Length == 0 ? EmptyArray : this.Subclasses;
            }
        }

        /// <summary>
        /// Gets the direct subtypes.
        /// </summary>
        /// <value>The direct subtypes.</value>
        public ObjectType[] DirectSubtypes
        {
            get { return this.ObjectTypesWhereDirectSupertype; }
        }

        /// <summary>
        /// Gets the direct super interfaces.
        /// </summary>
        /// <value>The direct super interfaces.</value>
        public IList<ObjectType> DirectSuperinterfaces
        {
            get
            {
                return this.DerivedDirectSuperinterfaces;
            }
        }

        /// <summary>
        /// Gets the direct super types.
        /// </summary>
        /// <value>The direct super types.</value>
        public IList<ObjectType> DirectSupertypes
        {
            get
            {
                return this.DerivedDirectSupertypes;
            }
        }

        /// <summary>
        /// Gets the exclusive associations.
        /// </summary>
        /// <value>The exclusive associations.</value>
        public IList<AssociationType> ExclusiveAssociationTypes
        {
            get
            {
                return this.DerivedExclusiveAssociationTypes;
            }
        }

        /// <summary>
        /// Gets the exclusive concrete subclass.
        /// </summary>
        /// <value>The exclusive concrete subclass.</value>
        public ObjectType ExclusiveConcreteSubclass
        {
            get
            {
                return this.DerivedExclusiveConcreteLeafClass;
            }
        }

        /// <summary>
        /// Gets the exclusive roles.
        /// </summary>
        /// <value>The exclusive roles.</value>
        public IList<RoleType> ExclusiveRoleTypes
        {
            get
            {
                return this.DerivedExclusiveRoleTypes;
            }
        }

        /// <summary>
        /// Gets the exclusive root class.
        /// </summary>
        /// <value>The exclusive root class.</value>
        public ObjectType ExclusiveRootClass
        {
            get
            {
                if (this.RootClasses.Length == 1)
                {
                    return this.RootClasses[0];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the exclusive super interfaces.
        /// </summary>
        /// <value>The exclusive super interfaces.</value>
        public IList<ObjectType> ExclusiveSuperinterfaces
        {
            get
            {
                return this.DerivedExclusiveSuperinterfaces;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a binary.
        /// </summary>
        /// <value><c>true</c> if this instance is a binary; otherwise, <c>false</c>.</value>
        public bool IsBinary
        {
            get { return this.Id.Equals(UnitIds.BinaryId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a boolean.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a boolean; otherwise, <c>false</c>.
        /// </value>
        public bool IsBoolean
        {
            get { return this.Id.Equals(UnitIds.BooleanId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a date time.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a date time; otherwise, <c>false</c>.
        /// </value>
        public bool IsDateTime
        {
            get { return this.Id.Equals(UnitIds.DatetimeId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a decimal.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a decimal; otherwise, <c>false</c>.
        /// </value>
        public bool IsDecimal
        {
            get { return this.Id.Equals(UnitIds.DecimalId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a double.
        /// </summary>
        /// <value><c>true</c> if this instance is a double; otherwise, <c>false</c>.</value>
        public bool IsDouble
        {
            get { return this.Id.Equals(UnitIds.DoubleId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is an integer.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is an integer; otherwise, <c>false</c>.
        /// </value>
        public bool IsInteger
        {
            get { return this.Id.Equals(UnitIds.IntegerId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is long.
        /// </summary>
        /// <value><c>true</c> if this instance is a long; otherwise, <c>false</c>.</value>
        public bool IsLong
        {
            get { return this.Id.Equals(UnitIds.LongId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance requires precision.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance requires precision; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrecisionRequired
        {
            get
            {
                if (this.IsDecimal)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance requires a scale.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance requires a scale; otherwise, <c>false</c>.
        /// </value>
        public bool IsScaleRequired
        {
            get
            {
                if (this.IsDecimal)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance requires a size.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance requires a size; otherwise, <c>false</c>.
        /// </value>
        public bool IsSizeRequired
        {
            get
            {
                if (this.IsString || this.IsBinary)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a string.
        /// </summary>
        /// <value><c>true</c> if this instance is a string; otherwise, <c>false</c>.</value>
        public bool IsString
        {
            get { return this.Id.Equals(UnitIds.StringId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a unique.
        /// </summary>
        /// <value><c>true</c> if this instance is a unique; otherwise, <c>false</c>.</value>
        public bool IsUnique
        {
            get { return this.Id.Equals(UnitIds.Unique); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name .</value>
        public string Name
        {
            get
            {
                return this.SingularName;
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
                return this.DerivedMethodTypes;
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
                return this.DerivedRoleTypes;
            }
        }

        /// <summary>
        /// Gets the roles where this instance is the root type.
        /// </summary>
        /// <value>The roles where this instance is the root type.</value>
        public IList<RoleType> RolesTypesWhereRootType
        {
            get
            {
                return this.RoleTypesWhereDerivedRootType;
            }
        }

        /// <summary>
        /// Gets the root classes.
        /// </summary>
        /// <value>The root classes.</value>
        public IList<ObjectType> RootClasses
        {
            get
            {
                return this.DerivedRootClasses;
            }
        }

        /// <summary>
        /// Gets the subclasses.
        /// </summary>
        /// <value>The subclasses.</value>
        public IList<ObjectType> Subclasses
        {
            get
            {
                return this.DerivedSubclasses;
            }
        }

        /// <summary>
        /// Gets the sub interfaces.
        /// </summary>
        /// <value>The sub interfaces.</value>
        public IList<ObjectType> Subinterfaces
        {
            get
            {
                return this.DerivedSubinterfaces;
            }
        }

        /// <summary>
        /// Gets the super classes.
        /// </summary>
        /// <value>The super classes.</value>
        public IList<ObjectType> Superclasses
        {
            get
            {
                return this.DerivedSuperclasses;
            }
        }

        /// <summary>
        /// Gets the super types.
        /// </summary>
        /// <value>The super types.</value>
        public IList<ObjectType> Supertypes
        {
            get
            {
                return this.DerivedSupertypes;
            }
        }


        /// <summary>
        /// Gets the unit roles.
        /// </summary>
        /// <value>The unit roles.</value>
        public IList<RoleType> UnitRoleTypes
        {
            get
            {
                return this.DerivedUnitRoleTypes;
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        /// <value>The validation name.</value>
        protected override string ValidationName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SingularName))
                {
                    return "object type " + this.SingularName;
                }
                
                return "object type " + this.Id;
            }
        }

        /// <summary>
        /// Gets the object types where this instance is a direct supertype.
        /// </summary>
        /// <value>The object types where this instance is a direct supertype.</value>
        private ObjectType[] ObjectTypesWhereDirectSupertype
        {
            get
            {
                return this.ObjectTypesWhereDerivedDirectSupertype;
            }
        }

        /// <summary>
        /// Adds the direct supertype.
        /// </summary>
        /// <param name="supertype">The supertype.</param>
        /// <returns>The inheritance.</returns> 
        public Inheritance AddDirectSupertype(ObjectType supertype)
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

                inheritance = this.Domain.AddDeclaredInheritance(Guid.NewGuid());
                inheritance.Subtype = this;
                inheritance.Supertype = supertype;
            }

            return inheritance;
        }
        
        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="obj"/> is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            var that = obj as ObjectType;
            if (that != null)
            {
                return string.CompareOrdinal(this.Name, that.Name);
            }

            return -1;
        }

        /// <summary>
        /// Determines whether this instance contains the specified association.
        /// </summary>
        /// <param name="association">The association.</param>
        /// <returns>
        ///  <c>true</c> if this instance contains the specified association; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsAssociationType(AssociationType association)
        {
            return this.associationIdsCache.ContainsKey(association.RelationType.Id);
        }

        /// <summary>
        /// Determines whether this instance contains the specified role.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <returns>
        ///  <c>true</c> if this instance contains the specified role; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsRoleType(RoleType role)
        {
            return this.roleIdsCache.ContainsKey(role.RelationType.Id);
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
        public bool ContainsConcreteClass(ObjectType objectType)
        {
            return this.concreteClassesCache.Contains(objectType);
        }
        
        /// <summary>
        /// Finds the inheritance where this instance is the direct subtype.
        /// </summary>
        /// <param name="supertype">The supertype.</param>
        /// <returns>The inheritance.</returns>
        public Inheritance FindInheritanceWhereDirectSubtype(ObjectType supertype)
        {
            foreach (var inheritance in this.InheritancesWhereSubtype)
            {
                if (supertype.Equals(inheritance.Supertype))
                {
                    return inheritance;
                }
            }

            return null;
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
        /// Sets the direct super interfaces.
        /// </summary>
        /// <param name="superInterfaces">The super interfaces.</param>
        public void SetDirectSuperinterfaces(ObjectType[] superInterfaces)
        {
            foreach (var superType in superInterfaces)
            {
                this.AddDirectSupertype(superType);
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.SingularName))
            {
                return this.SingularName;
            }

            return this.IdAsString;
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

            foreach (var directSub in this.ObjectTypesWhereDirectSupertype)
            {
                if (directSub.IsCyclicInheritance(superType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Derive association types.
        /// </summary>
        /// <param name="associations">The associations.</param>
        internal void DeriveAssociationTypes(HashSet<AssociationType> associations)
        {
            associations.Clear();
            foreach (var role in this.RoleTypesWhereObjectType)
            {
                associations.Add(role.RelationType.AssociationType);
            }

            foreach (var superType in this.Supertypes)
            {
                foreach (var role in superType.RoleTypesWhereObjectType)
                {
                    associations.Add(role.RelationType.AssociationType);
                }
            }

            this.DerivedAssociationTypes = new List<AssociationType>(associations);
        }

        /// <summary>
        /// Derive composite role types.
        /// </summary>
        /// <param name="roles">The roles.</param>
        internal void DeriveCompositeRoleTypes(HashSet<RoleType> roles)
        {
            roles.Clear();
            foreach (var role in this.DerivedRoleTypes)
            {
                // TODO: Test
                if (role.ObjectType != null && !role.ObjectType.IsUnit)
                {
                    roles.Add(role);
                }
            }

            this.DerivedCompositeRoleTypes = new List<RoleType>(roles);
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
            foreach (var containsRole in this.DerivedRoleTypes)
            {
                this.roleIdsCache[containsRole.RelationType.Id] = null;
            }
        }

        /// <summary>
        /// Derive concrete classes cache.
        /// </summary>
        internal void DeriveConcreteClassesCache()
        {
            this.concreteClassesCache = new HashSet<ObjectType>(this.ConcreteClasses);
        }

        /// <summary>
        /// Derive direct super interface.
        /// </summary>
        /// <param name="directSuperinterfaces">The direct super interfaces.</param>
        internal void DeriveDirectSuperinterface(HashSet<ObjectType> directSuperinterfaces)
        {
            directSuperinterfaces.Clear();
            foreach (var directSupertype in this.DerivedDirectSupertypes)
            {
                if (directSupertype.IsInterface)
                {
                    directSuperinterfaces.Add(directSupertype);
                }
            }

            this.DerivedDirectSuperinterfaces = new List<ObjectType>(directSuperinterfaces);
        }

        /// <summary>
        /// Derive direct super type derivations.
        /// </summary>
        /// <param name="directSupertypes">The direct super types.</param>
        internal void DeriveDirectSupertypes(HashSet<ObjectType> directSupertypes)
        {
            directSupertypes.Clear();
            foreach (var inheritance in this.InheritancesWhereSubtype)
            {
                directSupertypes.Add(inheritance.Supertype);
            }

            this.DerivedDirectSupertypes = new List<ObjectType>(directSupertypes);
        }

        /// <summary>
        /// Derive exclusive association types.
        /// </summary>
        /// <param name="exclusiveAssociationTypes">The exclusive association types.</param>
        internal void DeriveExclusiveAssociationTypes(HashSet<AssociationType> exclusiveAssociationTypes)
        {
            exclusiveAssociationTypes.Clear();
            foreach (var role in this.RoleTypesWhereObjectType)
            {
                exclusiveAssociationTypes.Add(role.RelationType.AssociationType);
            }

            foreach (var superType in this.ExclusiveSuperinterfaces)
            {
                foreach (var role in superType.RoleTypesWhereObjectType)
                {
                    exclusiveAssociationTypes.Add(role.RelationType.AssociationType);
                }
            }

            this.DerivedExclusiveAssociationTypes = new List<AssociationType>(exclusiveAssociationTypes);
        }

        /// <summary>
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        /// <param name="concreteLeafClasses">The concrete leaf classes.</param>
        internal void DeriveExclusiveConcreteLeafClass(HashSet<ObjectType> concreteLeafClasses)
        {
            concreteLeafClasses.Clear();

            this.DerivedExclusiveConcreteLeafClass = null;
            if (!this.IsInterface && this.DerivedSubclasses.Count == 0)
            {
                concreteLeafClasses.Add(this);
            }

            foreach (var rootClass in this.RootClasses)
            {
                foreach (var rootSubClass in rootClass.Subclasses)
                {
                    if (!rootSubClass.IsInterface && rootSubClass.DerivedSubclasses.Count == 0)
                    {
                        if (!concreteLeafClasses.Contains(rootSubClass))
                        {
                            concreteLeafClasses.Add(rootSubClass);
                        }
                    }
                }
            }

            if (concreteLeafClasses.Count == 1)
            {
                var concreteLeafClassArray = new ObjectType[concreteLeafClasses.Count];
                concreteLeafClasses.CopyTo(concreteLeafClassArray);

                this.DerivedExclusiveConcreteLeafClass = concreteLeafClassArray[0];
            }
        }

        /// <summary>
        /// Derive exclusive roles.
        /// </summary>
        /// <param name="exclusiveRoles">The exclusive roles.</param>
        internal void DeriveExclusiveRoleTypes(HashSet<RoleType> exclusiveRoles)
        {
            exclusiveRoles.Clear();
            foreach (var association in this.AssociationTypesWhereObjectType)
            {
                exclusiveRoles.Add(association.RelationType.RoleType);
            }

            foreach (var superType in this.ExclusiveSuperinterfaces)
            {
                foreach (var association in superType.AssociationTypesWhereObjectType)
                {
                    exclusiveRoles.Add(association.RelationType.RoleType);
                }
            }

            this.DerivedExclusiveRoleTypes = new List<RoleType>(exclusiveRoles);
        }

        /// <summary>
        /// Derive exclusive super interfaces.
        /// </summary>
        /// <param name="superInterfaces">The super interfaces.</param>
        internal void DeriveExclusiveSuperinterfaces(HashSet<ObjectType> superInterfaces)
        {
            superInterfaces.Clear();
            foreach (var superType in this.DerivedSupertypes)
            {
                if (superType.IsInterface && !superType.IsImplementedByAnyOf(this.Superclasses))
                {
                    superInterfaces.Add(superType);
                }
            }

            this.DerivedExclusiveSuperinterfaces = new List<ObjectType>(superInterfaces);
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
            foreach (var methodType in this.MethodTypesWhereObjectType)
            {
                methodTypes.Add(methodType);
            }

            foreach (var superType in this.Supertypes)
            {
                foreach (var methodType in superType.MethodTypesWhereObjectType)
                {
                    methodTypes.Add(methodType);
                }
            }

            this.DerivedMethodTypes = new List<MethodType>(methodTypes);
        }

        /// <summary>
        /// Derive role types.
        /// </summary>
        /// <param name="roleTypes">The role types.</param>
        internal void DeriveRoleTypes(HashSet<RoleType> roleTypes)
        {
            roleTypes.Clear();
            foreach (var association in this.AssociationTypesWhereObjectType)
            {
                roleTypes.Add(association.RelationType.RoleType);
            }

            foreach (var superType in this.Supertypes)
            {
                foreach (var association in superType.AssociationTypesWhereObjectType)
                {
                    roleTypes.Add(association.RelationType.RoleType);
                }
            }

            this.DerivedRoleTypes = new List<RoleType>(roleTypes);
        }

        /// <summary>
        /// Derive root class for classes.
        /// </summary>
        internal void DeriveRootClasses()
        {
            if (this.IsInterface)
            {
                this.DerivedRootClasses = this.DerivedSubclasses;
            }
            else
            {
                this.DerivedRootClasses = new List<ObjectType>() { this };
            }
        }

        /// <summary>
        /// Derive subclasses.
        /// </summary>
        /// <param name="subClasses">The sub classes.</param>
        internal void DeriveSubclasses(HashSet<ObjectType> subClasses)
        {
            subClasses.Clear();
            foreach (var subType in this.ObjectTypesWhereDerivedSupertype)
            {
                if (!subType.IsInterface)
                {
                    subClasses.Add(subType);
                }
            }

            this.DerivedSubclasses = new List<ObjectType>(subClasses);
        }

        /// <summary>
        /// Derive sub interfaces.
        /// </summary>
        /// <param name="subInterfaces">The sub interfaces.</param>
        internal void DeriveSubinterfaces(HashSet<ObjectType> subInterfaces)
        {
            subInterfaces.Clear();
            foreach (var subType in this.ObjectTypesWhereDerivedSupertype)
            {
                if (subType.IsInterface)
                {
                    subInterfaces.Add(subType);
                }
            }

            this.DerivedSubinterfaces = new List<ObjectType>(subInterfaces);
        }

        /// <summary>
        /// Derive super classes.
        /// </summary>
        /// <param name="superClasses">The super classes.</param>
        internal void DeriveSuperclasses(HashSet<ObjectType> superClasses)
        {
            superClasses.Clear();
            foreach (var superTypes in this.DerivedSupertypes)
            {
                if (!superTypes.IsInterface)
                {
                    superClasses.Add(superTypes);
                }
            }

            this.DerivedSuperclasses = new List<ObjectType>(superClasses);
        }

        /// <summary>
        /// Derive super interface.
        /// </summary>
        /// <param name="superInterfaces">The super interfaces.</param>
        internal void DeriveSuperinterfaces(HashSet<ObjectType> superInterfaces)
        {
            superInterfaces.Clear();
            foreach (var superType in this.DerivedSupertypes)
            {
                if (superType.IsInterface)
                {
                    superInterfaces.Add(superType);
                }
            }

            this.DerivedSuperinterfaces = new List<ObjectType>(superInterfaces);
        }

        /// <summary>
        /// Derive super types.
        /// </summary>
        /// <param name="superTypes">The super types.</param>
        internal void DeriveSupertypes(HashSet<ObjectType> superTypes)
        {
            superTypes.Clear();
            this.DeriveSupertypesRecursively(this, superTypes);

            this.DerivedSupertypes = new List<ObjectType>(superTypes);
        }

        /// <summary>
        /// Derive unit role types.
        /// </summary>
        /// <param name="roles">The roles.</param>
        internal void DeriveUnitRoleTypes(HashSet<RoleType> roles)
        {
            roles.Clear();
            foreach (var role in this.DerivedRoleTypes)
            {
                // TODO: Test
                if (role.ObjectType != null && role.ObjectType.IsUnit)
                {
                    roles.Add(role);
                }
            }

            this.DerivedUnitRoleTypes = new List<RoleType>(roles);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (!string.IsNullOrEmpty(this.SingularName))
            {
                if (this.SingularName.Length < 2)
                {
                    var message = this.ValidationName + " should have a singular name with at least 2 characters";
                    validationLog.AddError(message, this, ValidationKind.MinimumLength, "ObjectType.SingularName");
                }
                else
                {
                    if (!char.IsLetter(this.SingularName[0]))
                    {
                        var message = this.ValidationName + "'s singular name should start with an alfabetical character";
                        validationLog.AddError(message, this, ValidationKind.Format, "ObjectType.SingularName");
                    }

                    for (var i = 1; i < this.SingularName.Length; i++)
                    {
                        if (!char.IsLetter(this.SingularName[i]) && !char.IsDigit(this.SingularName[i]))
                        {
                            var message = this.ValidationName + "'s singular name should only contain alfanumerical characters";
                            validationLog.AddError(message, this, ValidationKind.Format, "ObjectType.SingularName");
                            break;
                        }
                    }
                }

                if (validationLog.ExistObjectTypeName(this.SingularName))
                {
                    var message = "The singular name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "ObjectType.SingularName");
                }
                else
                {
                    validationLog.AddObjectTypeName(this.SingularName);
                }
            }
            else
            {
                validationLog.AddError(this.ValidationName + " has no singular name", this, ValidationKind.Required, "ObjectType.SingularName");
            }

            if (this.ExistPluralName)
            {
                if (this.PluralName.Length < 2)
                {
                    var message = this.ValidationName + " should have a plural name with at least 2 characters";
                    validationLog.AddError(message, this, ValidationKind.MinimumLength, "ObjectType.PluralName");
                }
                else
                {
                    if (!char.IsLetter(this.PluralName[0]))
                    {
                        var message = this.ValidationName + "'s plural name should start with an alfabetical character";
                        validationLog.AddError(message, this, ValidationKind.Format, "ObjectType.PluralName");
                    }

                    for (var i = 1; i < this.PluralName.Length; i++)
                    {
                        if (!char.IsLetter(this.PluralName[i]) && !char.IsDigit(this.PluralName[i]))
                        {
                            var message = this.ValidationName + "'s plural name should only contain alfanumerical characters";
                            validationLog.AddError(message, this, ValidationKind.Format, "ObjectType.PluralName");
                            break;
                        }
                    }
                }

                if (validationLog.ExistObjectTypeName(this.PluralName))
                {
                    var message = "The plural name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "ObjectType.PluralName");
                }
                else
                {
                    validationLog.AddObjectTypeName(this.PluralName);
                }
            }
            else
            {
                validationLog.AddError(this.ValidationName + " has no plural name", this, ValidationKind.Required, "ObjectType.PluralName");
            }
        }

        /// <summary>
        /// Determines whether this instance is implemented by any of the specified object types.
        /// </summary>
        /// <param name="objectTypes">The object types.</param>
        /// <returns>
        ///  <c>true</c> if this instance is implemented by any of the specified object types; otherwise, <c>false</c>.
        /// </returns>
        private bool IsImplementedByAnyOf(IEnumerable<ObjectType> objectTypes)
        {
            foreach (var domainType in objectTypes)
            {
                foreach (var superType in domainType.DerivedSupertypes)
                {
                    if (this.Equals(superType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="superTypes">The super types.</param>
        private void DeriveSupertypesRecursively(ObjectType type, HashSet<ObjectType> superTypes)
        {
            foreach (var directSupertype in this.DerivedDirectSupertypes)
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