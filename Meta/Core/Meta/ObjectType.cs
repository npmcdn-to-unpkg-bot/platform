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
    using System.Linq;

    /// <summary>
    /// An <see cref="ObjectType"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public abstract partial class ObjectType : MetaObject, IComparable
    {
        public string SingularName;

        public string PluralName;


        public List<Interface> DerivedDirectSupertypes = new List<Interface>();

        public List<Interface> DerivedSupertypes = new List<Interface>();

        public List<CompositeType> DerivedDirectSubtypes = new List<CompositeType>();

        public List<CompositeType> DerivedSubtypes = new List<CompositeType>();

        public List<Class> DerivedSubclasses = new List<Class>();

        public List<Class> DerivedRootClasses = new List<Class>();

        public Class DerivedExclusiveRootClass;

        
        // Domain -> ObjectType
        public Domain Domain { get; private set; }
        
        /// <summary>
        /// An empty array of object types.
        /// </summary>
        private static readonly ObjectType[] EmptyArray = new ObjectType[0];

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
        /// Gets the exclusive concrete subclass.
        /// </summary>
        /// <value>The exclusive concrete subclass.</value>
        public Class ExclusiveRootClass
        {
            get
            {
                return this.DerivedExclusiveRootClass;
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
        /// Gets the root classes.
        /// </summary>
        /// <value>The root classes.</value>
        public IList<Class> RootClasses
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
        public IList<Class> Subclasses
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
        public IList<Interface> Supertypes
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
        public IList<CompositeType> Subtypes
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

            this.DerivedDirectSupertypes = new List<Interface>(directSupertypes);
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
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        /// <param name="concreteLeafClasses">The concrete leaf classes.</param>
        internal void DeriveExclusiveRootClass()
        {
            this.DerivedExclusiveRootClass = null;
            if (this.DerivedRootClasses.Count == 1)
            {
                this.DerivedExclusiveRootClass = this.DerivedRootClasses[0];
            }
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
                this.DerivedRootClasses = new List<Class> { (Class)this };
            }

            this.rootClassesCache = new HashSet<ObjectType>(this.DerivedRootClasses);
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
        /// Derive super types.
        /// </summary>
        /// <param name="superTypes">The super types.</param>
        internal void DeriveSupertypes(HashSet<Interface> superTypes)
        {
            superTypes.Clear();
            this.DeriveSupertypesRecursively(this, superTypes);

            this.DerivedSupertypes = new List<Interface>(superTypes);
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
        private void DeriveSupertypesRecursively(ObjectType type, HashSet<Interface> superTypes)
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
        private void DeriveSubtypesRecursively(ObjectType type, HashSet<CompositeType> subTypes)
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