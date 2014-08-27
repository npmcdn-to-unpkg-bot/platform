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
    using System.Linq;

    /// <summary>
    /// An <see cref="ObjectType"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public abstract partial class ObjectType : MetaObject, IComparable
    {
        public int UnitTag;

        public string SingularName;

        public string PluralName;


        public List<ObjectType> DerivedDirectSupertypes = new List<ObjectType>();

        public List<ObjectType> DerivedSupertypes = new List<ObjectType>();

        public List<ObjectType> DerivedDirectSubtypes = new List<ObjectType>();

        public List<ObjectType> DerivedSubtypes = new List<ObjectType>();

        public List<ObjectType> DerivedSubclasses = new List<ObjectType>();

        public List<ObjectType> DerivedRootClasses = new List<ObjectType>();

        public ObjectType DerivedExclusiveRootClass;


        public List<RoleType> DerivedRoleTypes = new List<RoleType>();

        public List<AssociationType> DerivedAssociationTypes = new List<AssociationType>();

        public List<AssociationType> DerivedExclusiveAssociationTypes = new List<AssociationType>();
        
        public List<MethodType> DerivedMethodTypes = new List<MethodType>();

        
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
        private HashSet<ObjectType> rootClassesCache;

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
        public ObjectType ExclusiveRootclass
        {
            get
            {
                return this.DerivedExclusiveRootClass;
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
                if (this.RootClasses.Count == 1)
                {
                    return this.RootClasses[0];
                }

                return null;
            }
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
        /// Gets the sub types.
        /// </summary>
        /// <value>The super types.</value>
        public IList<ObjectType> Subtypes
        {
            get
            {
                return this.DerivedSubtypes;
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
                if (supertype is Class)
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
            return this.rootClassesCache.Contains(objectType);
        }
        
        /// <summary>
        /// Finds the inheritance where this instance is the direct subtype.
        /// </summary>
        /// <param name="supertype">The supertype.</param>
        /// <returns>The inheritance.</returns>
        public Inheritance FindInheritanceWhereDirectSubtype(ObjectType supertype)
        {
            return this.Domain.Inheritances.FirstOrDefault(inheritance => this.Equals(inheritance.Subtype) && supertype.Equals(inheritance.Supertype));
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

            foreach (var directSubtypes in this.DerivedDirectSubtypes)
            {
                if (directSubtypes.IsCyclicInheritance(superType))
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

            this.DerivedAssociationTypes = new List<AssociationType>(associations);
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
        internal void DeriveRootClassesCache()
        {
            this.rootClassesCache = new HashSet<ObjectType>(this.DerivedRootClasses);
        }

        /// <summary>
        /// Derive direct super type derivations.
        /// </summary>
        /// <param name="directSupertypes">The direct super types.</param>
        internal void DeriveDirectSupertypes(HashSet<ObjectType> directSupertypes)
        {
            directSupertypes.Clear();
            foreach (var inheritance in this.Domain.Inheritances.Where(inheritance => this.Equals(inheritance.Subtype)))
            {
                directSupertypes.Add(inheritance.Supertype);
            }

            this.DerivedDirectSupertypes = new List<ObjectType>(directSupertypes);
        }

        /// <summary>
        /// Derive direct sub type derivations.
        /// </summary>
        /// <param name="directSubtypes">The direct super types.</param>
        internal void DeriveDirectSubtypes(HashSet<ObjectType> directSubtypes)
        {
            directSubtypes.Clear();
            foreach (var inheritance in this.Domain.Inheritances.Where(inheritance => this.Equals(inheritance.Supertype)))
            {
                directSubtypes.Add(inheritance.Subtype);
            }

            this.DerivedDirectSubtypes = new List<ObjectType>(directSubtypes);
        }

        /// <summary>
        /// Derive exclusive association types.
        /// </summary>
        /// <param name="exclusiveAssociationTypes">The exclusive association types.</param>
        internal void DeriveExclusiveAssociationTypes(HashSet<AssociationType> exclusiveAssociationTypes)
        {
            exclusiveAssociationTypes.Clear();
            foreach (var relationType in this.Domain.RelationTypes.Where(rel => this.Equals(rel.RoleType.ObjectType)))
            {
                exclusiveAssociationTypes.Add(relationType.AssociationType);
            }

            foreach (var superType in this.Supertypes)
            {
                var type = superType;
                foreach (var relationType in this.Domain.RelationTypes.Where(rel => type.Equals(rel.RoleType.ObjectType)))
                {
                    exclusiveAssociationTypes.Add(relationType.AssociationType);
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

            this.DerivedExclusiveRootClass = null;
            if (!(this is Interface) && this.DerivedSubclasses.Count == 0)
            {
                concreteLeafClasses.Add(this);
            }

            foreach (var rootClass in this.RootClasses)
            {
                foreach (var rootSubClass in rootClass.Subclasses)
                {
                    if (!(rootSubClass is Interface) && rootSubClass.DerivedSubclasses.Count == 0)
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

                this.DerivedExclusiveRootClass = concreteLeafClassArray[0];
            }
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

            this.DerivedMethodTypes = new List<MethodType>(methodTypes);
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

            this.DerivedRoleTypes = new List<RoleType>(roleTypes);
        }

        /// <summary>
        /// Derive root class for classes.
        /// </summary>
        internal void DeriveRootClasses()
        {
            if (this is Interface)
            {
                this.DerivedRootClasses = this.DerivedSubclasses;
            }
            else
            {
                this.DerivedRootClasses = new List<ObjectType> { this };
            }
        }

        /// <summary>
        /// Derive subclasses.
        /// </summary>
        /// <param name="subClasses">The sub classes.</param>
        internal void DeriveSubclasses(HashSet<ObjectType> subClasses)
        {
            subClasses.Clear();
            foreach (var subType in this.DerivedSubtypes)
            {
                if (!(subType is Interface))
                {
                    subClasses.Add(subType);
                }
            }

            this.DerivedSubclasses = new List<ObjectType>(subClasses);
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
        /// Derive sub types.
        /// </summary>
        /// <param name="subTypes">The super types.</param>
        internal void DeriveSubtypes(HashSet<ObjectType> subTypes)
        {
            subTypes.Clear();
            this.DeriveSubtypesRecursively(this, subTypes);

            this.DerivedSubtypes = new List<ObjectType>(subTypes);
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

            if (!string.IsNullOrEmpty(this.PluralName))
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

        /// <summary>
        /// Derive super types recursively.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <param name="superTypes">The super types.</param>
        private void DeriveSubtypesRecursively(ObjectType type, HashSet<ObjectType> subTypes)
        {
            foreach (var directSubtype in this.DerivedDirectSubtypes)
            {
                if (!Equals(directSubtype, type))
                {
                    subTypes.Add(directSubtype);
                    directSubtype.DeriveSubtypesRecursively(type, subTypes);
                }
            }
        }
    }
}