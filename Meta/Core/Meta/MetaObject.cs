//------------------------------------------------------------------------------------------------- 
// <copyright file="MetaObject.cs" company="Allors bvba">
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

    using AllorsGenerated;

    /// <summary>
    /// An <see cref="MetaObject"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public partial class MetaObject : IComparable
    {
        /// <summary>
        /// An empty array of object types.
        /// </summary>
        private static readonly MetaObject[] EmptyArray = new MetaObject[0];

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
        private HashSet<MetaObject> concreteClassesCache;

        /// <summary>
        /// Gets the association count.
        /// </summary>
        /// <value>The association count.</value>
        public int AssociationTypesCount
        {
            get { return this.AssociationTypes.Length; }
        }

        /// <summary>
        /// Gets a value indicating whether the association count is greater than 32.
        /// </summary>
        /// <value>
        /// <c>true</c> if association count is greater than 32; otherwise, <c>false</c>.
        /// </value>
        public bool AssociationTypesCountGreaterThan32
        {
            get { return this.AssociationTypes.Length > 32; }
        }

        /// <summary>
        /// Gets the associations.
        /// </summary>
        /// <value>The associations.</value>
        public MetaAssociation[] AssociationTypes
        {
            get
            {
                return this.DerivedAssociationTypes;
            }
        }

        /// <summary>
        /// Gets the associations where this instance is the root type.
        /// </summary>
        /// <value>The associations where this instance is the root type.</value>
        public MetaAssociation[] AssociationTypesWhereRootType
        {
            get
            {
                return this.AssociationTypesWhereDerivedRootObjectType;
            }
        }

        /// <summary>
        /// Gets the binary roles.
        /// </summary>
        /// <value>The binary roles.</value>
        public MetaRole[] BinaryRoles
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsBinary); }
        }

        /// <summary>
        /// Gets the boolean roles.
        /// </summary>
        /// <value>The boolean roles.</value>
        public MetaRole[] BooleanRoles
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsBoolean); }
        }

        /// <summary>
        /// Gets the double roles.
        /// </summary>
        /// <value>The double roles.</value>
        public MetaRole[] DoubleRoleTypes
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsDouble); }
        }

        /// <summary>
        /// Gets the date time role types.
        /// </summary>
        /// <value>The date time role types.</value>
        public MetaRole[] DateTimeRoleTypes
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsDateTime); }
        }

        /// <summary>
        /// Gets the decimal role types.
        /// </summary>
        /// <value>The decimal role types.</value>
        public MetaRole[] DecimalRoleTypes
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsDecimal); }
        }

        /// <summary>
        /// Gets the integer32 roles.
        /// </summary>
        /// <value>The integer32 roles.</value>
        public MetaRole[] Integer32Roles
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsInteger); }
        }

        /// <summary>
        /// Gets the integer64 roles.
        /// </summary>
        /// <value>The integer64 roles.</value>
        public MetaRole[] Integer64Roles
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsLong); }
        }

        /// <summary>
        /// Gets the string roles.
        /// </summary>
        /// <value>The string roles.</value>
        public MetaRole[] StringRoleTypes
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsString); }
        }

        /// <summary>
        /// Gets the composite role count.
        /// </summary>
        /// <value>The composite role count.</value>
        public int CompositeRoleTypeCount
        {
            get { return this.CompositeRoleTypes.Length; }
        }

        /// <summary>
        /// Gets a value indicating whether the composite role count is greater than 32.
        /// </summary>
        /// <value>
        /// <c>true</c> if the composite role count is greater than32; otherwise, <c>false</c>.
        /// </value>
        public bool CompositeRoleTypeCountGreaterThan32
        {
            get { return this.CompositeRoleTypes.Length > 32; }
        }

        /// <summary>
        /// Gets the composite roles.
        /// </summary>
        /// <value>The composite roles.</value>
        public MetaRole[] CompositeRoleTypes
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
        public MetaObject[] ConcreteClasses
        {
            get
            {
                if (this.IsConcreteComposite)
                {
                    MetaObject[] selfArray = { this };
                    return selfArray;
                }

                return this.Subclasses.Length == 0 ? EmptyArray : this.Subclasses;
            }
        }

        /// <summary>
        /// Gets the direct subtypes.
        /// </summary>
        /// <value>The direct subtypes.</value>
        public MetaObject[] DirectSubtypes
        {
            get { return this.ObjectTypesWhereDirectSupertype; }
        }

        /// <summary>
        /// Gets the direct super interfaces.
        /// </summary>
        /// <value>The direct super interfaces.</value>
        public MetaObject[] DirectSuperinterfaces
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
        public MetaObject[] DirectSupertypes
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
        public MetaAssociation[] ExclusiveAssociationTypes
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
        public MetaObject ExclusiveConcreteSubclass
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
        public MetaRole[] ExclusiveRoleTypes
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
        public MetaObject ExclusiveRootClass
        {
            get
            {
                if (this.ExistExclusiveRootClass)
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
        public MetaObject[] ExclusiveSuperinterfaces
        {
            get
            {
                return this.DerivedExclusiveSuperinterfaces;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there exist concrete classes.
        /// </summary>
        /// <value>
        ///  <c>true</c> if there exist concrete classes; otherwise, <c>false</c>.
        /// </value>
        public bool ExistConcreteClasses
        {
            get { return this.ConcreteClasses.Length > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether there exists direct super interfaces.
        /// </summary>
        /// <value>
        ///  <c>true</c> if there exists direct super interfaces; otherwise, <c>false</c>.
        /// </value>
        public bool ExistDirectSuperinterfaces
        {
            get
            {
                return this.ExistDerivedDirectSuperinterfaces;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there exists direct super types.
        /// </summary>
        /// <value>
        /// <c>true</c> if there exists direct super types; otherwise, <c>false</c>.
        /// </value>
        public bool ExistDirectSupertypes
        {
            get
            {
                return this.ExistDerivedDirectSupertypes;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there exists an exclusive concrete subclass.
        /// </summary>
        /// <value>
        /// <c>true</c> if there exists an exclusive concrete subclass; otherwise, <c>false</c>.
        /// </value>
        public bool ExistExclusiveConcreteSubclass
        {
            get
            {
                return this.ExistDerivedExclusiveConcreteLeafClass;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [exist exclusive root class].
        /// </summary>
        /// <value>
        /// <c>true</c> if [exist exclusive root class]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistExclusiveRootClass
        {
            get { return this.RootClasses.Length == 1; }
        }

        /// <summary>
        /// Gets a value indicating whether there exist root classes.
        /// </summary>
        /// <value><c>true</c> if there exist root classes; otherwise, <c>false</c>.</value>
        public bool ExistRootClasses
        {
            get
            {
                return this.ExistDerivedRootClasses;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there exist subclasses.
        /// </summary>
        /// <value><c>true</c> if there exist subclasses; otherwise, <c>false</c>.</value>
        public bool ExistSubclasses
        {
            get
            {
                return this.ExistDerivedSubclasses;
            }
        }

        /// <summary>
        /// Gets a value indicating whether there exists super classes.
        /// </summary>
        /// <value><c>true</c> if there exist super classes; otherwise, <c>false</c>.</value>
        public bool ExistSuperclasses
        {
            get
            {
                return this.ExistDerivedSuperclasses;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a binary.
        /// </summary>
        /// <value><c>true</c> if this instance is a binary; otherwise, <c>false</c>.</value>
        public bool IsBinary
        {
            get { return this.Id.Equals(MetaUnitIds.BinaryId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a boolean.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a boolean; otherwise, <c>false</c>.
        /// </value>
        public bool IsBoolean
        {
            get { return this.Id.Equals(MetaUnitIds.BooleanId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a class.
        /// </summary>
        /// <value><c>true</c> if this instance is a class; otherwise, <c>false</c>.</value>
        public bool IsClass
        {
            get { return !this.IsInterface; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a composite.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a composite; otherwise, <c>false</c>.
        /// </value>
        public bool IsComposite
        {
            get { return !this.IsUnit; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is concrete.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is concrete; otherwise, <c>false</c>.
        /// </value>
        public bool IsConcrete
        {
            get
            {
                return !this.IsInterface;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a concrete composite.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a concrete composite; otherwise, <c>false</c>.
        /// </value>
        public bool IsConcreteComposite
        {
            get { return !this.IsUnit && this.IsConcrete; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a date time.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a date time; otherwise, <c>false</c>.
        /// </value>
        public bool IsDateTime
        {
            get { return this.Id.Equals(MetaUnitIds.DatetimeId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a decimal.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a decimal; otherwise, <c>false</c>.
        /// </value>
        public bool IsDecimal
        {
            get { return this.Id.Equals(MetaUnitIds.DecimalId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a double.
        /// </summary>
        /// <value><c>true</c> if this instance is a double; otherwise, <c>false</c>.</value>
        public bool IsDouble
        {
            get { return this.Id.Equals(MetaUnitIds.DoubleId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is an integer.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is an integer; otherwise, <c>false</c>.
        /// </value>
        public bool IsInteger
        {
            get { return this.Id.Equals(MetaUnitIds.IntegerId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is long.
        /// </summary>
        /// <value><c>true</c> if this instance is a long; otherwise, <c>false</c>.</value>
        public bool IsLong
        {
            get { return this.Id.Equals(MetaUnitIds.LongId); }
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
        /// Gets a value indicating whether this instance is a root class.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is a root class; otherwise, <c>false</c>.
        /// </value>
        public bool IsRootClass
        {
            get
            {
                return this.ExistObjectTypesWhereDerivedRootClass;
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
            get { return this.Id.Equals(MetaUnitIds.StringId); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a unique.
        /// </summary>
        /// <value><c>true</c> if this instance is a unique; otherwise, <c>false</c>.</value>
        public bool IsUnique
        {
            get { return this.Id.Equals(MetaUnitIds.Unique); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name .</value>
        public string Name
        {
            get
            {
                if (this.ExistSingularName)
                {
                    return this.SingularName;
                }

                return this.IdAsString;
            }
        }

        /// <summary>
        /// Gets the object types where this instance is a direct super interface.
        /// </summary>
        /// <value>The object types where this instance is a direct super interface.</value>
        public MetaObject[] ObjectTypesWhereDirectSuperinterface
        {
            get
            {
                return this.ObjectTypesWhereDerivedDirectSuperinterface;
            }
        }

        /// <summary>
        /// Gets the object types where this instance is an exclusive super interface.
        /// </summary>
        /// <value>The object types where this instance is an exclusive super interface.</value>
        public MetaObject[] ObjectTypesWhereExclusiveSuperinterface
        {
            get
            {
                return this.ObjectTypesWhereDerivedExclusiveSuperinterface;
            }
        }

        /// <summary>
        /// Gets the object types where this instance is the root class.
        /// </summary>
        /// <value>The object types where this instance is the root class.</value>
        public MetaObject[] ObjectTypesWhereRootClass
        {
            get
            {
                return this.ObjectTypesWhereDerivedRootClass;
            }
        }

        /// <summary>
        /// Gets the object types where this instance is the super class.
        /// </summary>
        /// <value>The object types where this instance is the super class.</value>
        public MetaObject[] ObjectTypesWhereSuperclass
        {
            get
            {
                return this.ObjectTypesWhereDerivedSuperclass;
            }
        }

        /// <summary>
        /// Gets the object types where this instance is the super interface.
        /// </summary>
        /// <value>The object types where this instance is the super interface.</value>
        public MetaObject[] ObjectTypesWhereSuperinterface
        {
            get
            {
                return this.ObjectTypesWhereDerivedSuperinterface;
            }
        }

        /// <summary>
        /// Gets the method types.
        /// </summary>
        /// <value>The method types.</value>
        public MetaMethod[] MethodTypes
        {
            get
            {
                return this.DerivedMethodTypes;
            }
        }

        /// <summary>
        /// Gets the role count.
        /// </summary>
        /// <value>The role count.</value>
        public int RoleTypeCount
        {
            get { return this.RoleTypes.Length; }
        }

        /// <summary>
        /// Gets a value indicating whether the role count is greater than 32.
        /// </summary>
        /// <value>
        ///  <c>true</c> if the role count is greater than32; otherwise, <c>false</c>.
        /// </value>
        public bool RoleTypeCountGreaterThan32
        {
            get { return this.RoleTypes.Length > 32; }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public MetaRole[] RoleTypes
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
        public MetaRole[] RolesTypesWhereRootType
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
        public MetaObject[] RootClasses
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
        public MetaObject[] Subclasses
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
        public MetaObject[] Subinterfaces
        {
            get
            {
                return this.DerivedSubinterfaces;
            }
        }

        /// <summary>
        /// Gets the subtypes.
        /// </summary>
        /// <value>The subtypes.</value>
        public MetaObject[] Subtypes
        {
            get
            {
                return this.ObjectTypesWhereDerivedSupertype;
            }
        }

        /// <summary>
        /// Gets the super classes.
        /// </summary>
        /// <value>The super classes.</value>
        public MetaObject[] Superclasses
        {
            get
            {
                return this.DerivedSuperclasses;
            }
        }

        /// <summary>
        /// Gets the super interfaces.
        /// </summary>
        /// <value>The super interfaces.</value>
        public MetaObject[] Superinterfaces
        {
            get
            {
                return this.DerivedSuperinterfaces;
            }
        }

        /// <summary>
        /// Gets the super types.
        /// </summary>
        /// <value>The super types.</value>
        public MetaObject[] Supertypes
        {
            get
            {
                return this.DerivedSupertypes;
            }
        }

        /// <summary>
        /// Gets the unique roles.
        /// </summary>
        /// <value>The unique roles.</value>
        public MetaRole[] UniqueRoleTypes
        {
            get { return this.GetUnitRoleTypes(MetaUnitTags.AllorsUnique); }
        }

        /// <summary>
        /// Gets the unit role count.
        /// </summary>
        /// <value>The unit role count.</value>
        public int UnitRoleTypesCount
        {
            get { return this.UnitRoleTypes.Length; }
        }

        /// <summary>
        /// Gets a value indicating whether the unit role count is greater than32.
        /// </summary>
        /// <value>
        ///  <c>true</c> if the unit role count is greater than 32; otherwise, <c>false</c>.
        /// </value>
        public bool UnitRoleTypesCountGreaterThan32
        {
            get { return this.UnitRoleTypes.Length > 32; }
        }

        /// <summary>
        /// Gets the unit roles.
        /// </summary>
        /// <value>The unit roles.</value>
        public MetaRole[] UnitRoleTypes
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
                if (this.ExistSingularName)
                {
                    return "object type " + this.SingularName;
                }
                
                if (this.ExistId)
                {
                    return "object type " + this.Id;
                }

                return "unknown object type";
            }
        }

        /// <summary>
        /// Gets the object types where this instance is a direct supertype.
        /// </summary>
        /// <value>The object types where this instance is a direct supertype.</value>
        private MetaObject[] ObjectTypesWhereDirectSupertype
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
        public MetaInheritance AddDirectSupertype(MetaObject supertype)
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
                if (supertype.IsConcreteComposite)
                {
                    throw new ArgumentException("The inheritance " + this + "::" + supertype + " can not have a concrete superclass");
                }

                inheritance = this.MetaDomain.AddDeclaredInheritance(Guid.NewGuid());
                inheritance.Subtype = this;
                inheritance.Supertype = supertype;
            }

            return inheritance;
        }

        /// <summary>
        /// Adds a <see cref="MetaMethod"/> to this object.
        /// </summary>
        /// <param name="methodId">The method id</param>
        /// <param name="methodName">The method name</param>
        /// <returns>The method type</returns>
        public MetaMethod AddMethodType(Guid methodId, string methodName)
        {
            var methodType = this.MetaDomain.AddDeclaredMethodType(methodId);
            methodType.ObjectType = this;
            methodType.Name = methodName;
            return methodType;
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
            var that = obj as MetaObject;
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
        public bool ContainsAssociationType(MetaAssociation association)
        {
            return this.associationIdsCache.ContainsKey(association.RelationTypeWhereAssociationType.Id);
        }

        /// <summary>
        /// Determines whether this instance contains the specified role.
        /// </summary>
        /// <param name="role">The role .</param>
        /// <returns>
        ///  <c>true</c> if this instance contains the specified role; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsRoleType(MetaRole role)
        {
            return this.roleIdsCache.ContainsKey(role.RelationTypeWhereRoleType.Id);
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
        public bool ContainsConcreteClass(MetaObject objectType)
        {
            return this.concreteClassesCache.Contains(objectType);
        }

        /// <summary>
        /// Delete this instance and its associations and inheritances.
        /// </summary>
        public void DeleteRecursive()
        {
            var inheritances = new List<MetaInheritance>();
            inheritances.AddRange(this.InheritancesWhereSubtype);
            inheritances.AddRange(this.InheritancesWhereSupertype);

            var associations = this.AssociationTypesWhereObjectType;

            foreach (var association in associations)
            {
            }

            foreach (var inheritance in inheritances)
            {
            }
        }

        /// <summary>
        /// Finds the inheritance where this instance is the direct subtype.
        /// </summary>
        /// <param name="supertype">The supertype.</param>
        /// <returns>The inheritance.</returns>
        public MetaInheritance FindInheritanceWhereDirectSubtype(MetaObject supertype)
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
        public bool IsValidSupertype(MetaObject supertype)
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
        public void SetDirectSuperinterfaces(MetaObject[] superInterfaces)
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
            if (this.ExistSingularName)
            {
                return this.SingularName;
            }

            if (this.ExistId)
            {
                return this.IdAsString;
            }

            return this.AllorsObjectId.ToString(CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The new instance.</returns>
        internal static MetaObject Create(AllorsEmbeddedSession session)
        {
            var type = (MetaObject)session.Create(AllorsEmbeddedDomain.ObjectType);

            type.IsInterface = false;
            type.IsUnit = false;

            return type;
        }

        /// <summary>
        /// Determines whether adding the specified super type will result in a cycle.
        /// </summary>
        /// <param name="superType">The super type.</param>
        /// <returns>
        /// <c>true</c> if adding the specified super type will result in a cycle; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsCyclicInheritance(MetaObject superType)
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
        /// Purges the derivations.
        /// </summary>
        internal void PurgeDerivations()
        {
            this.RemoveDerivedAssociationTypes();
            this.RemoveDerivedCompositeRoleTypes();
            this.RemoveDerivedDirectSuperinterfaces();
            this.RemoveDerivedExclusiveAssociationTypes();
            this.RemoveDerivedExclusiveConcreteLeafClass();
            this.RemoveDerivedExclusiveRoleTypes();
            this.RemoveDerivedExclusiveSuperinterfaces();
            this.RemoveDerivedRoleTypes();
            this.RemoveDerivedRootClasses();
            this.RemoveDerivedSubclasses();
            this.RemoveDerivedSubinterfaces();
            this.RemoveDerivedSuperclasses();
            this.RemoveDerivedSuperinterfaces();
            this.RemoveDerivedSupertypes();
            this.RemoveDerivedUnitRoleTypes();
        }

        /// <summary>
        /// Derive association types.
        /// </summary>
        /// <param name="associations">The associations.</param>
        internal void DeriveAssociationTypes(HashSet<MetaAssociation> associations)
        {
            associations.Clear();
            foreach (var role in this.RoleTypesWhereObjectType)
            {
                associations.Add(role.RelationTypeWhereRoleType.AssociationType);
            }

            foreach (var superType in this.Supertypes)
            {
                foreach (var role in superType.RoleTypesWhereObjectType)
                {
                    associations.Add(role.RelationTypeWhereRoleType.AssociationType);
                }
            }

            var associationArray = new MetaAssociation[associations.Count];
            associations.CopyTo(associationArray);

            this.DerivedAssociationTypes = associationArray;
        }

        /// <summary>
        /// Derive composite role types.
        /// </summary>
        /// <param name="roles">The roles.</param>
        internal void DeriveCompositeRoleTypes(HashSet<MetaRole> roles)
        {
            roles.Clear();
            foreach (var role in this.DerivedRoleTypes)
            {
                // TODO: Test
                if (role.ExistObjectType && role.ObjectType.IsComposite)
                {
                    roles.Add(role);
                }
            }

            var roleArray = new MetaRole[roles.Count];
            roles.CopyTo(roleArray);

            this.DerivedCompositeRoleTypes = roleArray;
        }

        /// <summary>
        /// Derive association ids cache.
        /// </summary>
        internal void DeriveAssociationIdsCache()
        {
            this.associationIdsCache = new Dictionary<Guid, object>();
            foreach (var containsAssociation in this.AssociationTypes)
            {
                this.associationIdsCache[containsAssociation.RelationTypeWhereAssociationType.Id] = null;
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
                this.roleIdsCache[containsRole.RelationTypeWhereRoleType.Id] = null;
            }
        }

        /// <summary>
        /// Derive concrete classes cache.
        /// </summary>
        internal void DeriveConcreteClassesCache()
        {
            this.concreteClassesCache = new HashSet<MetaObject>(this.ConcreteClasses);
        }

        /// <summary>
        /// Derive direct super interface.
        /// </summary>
        /// <param name="directSuperinterfaces">The direct super interfaces.</param>
        internal void DeriveDirectSuperinterface(HashSet<MetaObject> directSuperinterfaces)
        {
            directSuperinterfaces.Clear();
            foreach (var directSupertype in this.DerivedDirectSupertypes)
            {
                if (directSupertype.IsInterface)
                {
                    directSuperinterfaces.Add(directSupertype);
                }
            }

            var directSuperinterfaceArray = new MetaObject[directSuperinterfaces.Count];
            directSuperinterfaces.CopyTo(directSuperinterfaceArray);

            this.DerivedDirectSuperinterfaces = directSuperinterfaceArray;
        }

        /// <summary>
        /// Derive direct super type derivations.
        /// </summary>
        /// <param name="directSupertypes">The direct super types.</param>
        internal void DeriveDirectSupertypes(HashSet<MetaObject> directSupertypes)
        {
            directSupertypes.Clear();
            foreach (var inheritance in this.InheritancesWhereSubtype)
            {
                directSupertypes.Add(inheritance.Supertype);
            }

            var directSupertypeArray = new MetaObject[directSupertypes.Count];
            directSupertypes.CopyTo(directSupertypeArray);

            this.DerivedDirectSupertypes = directSupertypeArray;
        }

        /// <summary>
        /// Derive exclusive association types.
        /// </summary>
        /// <param name="exclusiveAssociationTypes">The exclusive association types.</param>
        internal void DeriveExclusiveAssociationTypes(HashSet<MetaAssociation> exclusiveAssociationTypes)
        {
            exclusiveAssociationTypes.Clear();
            foreach (var role in this.RoleTypesWhereObjectType)
            {
                exclusiveAssociationTypes.Add(role.RelationTypeWhereRoleType.AssociationType);
            }

            foreach (var superType in this.ExclusiveSuperinterfaces)
            {
                foreach (var role in superType.RoleTypesWhereObjectType)
                {
                    exclusiveAssociationTypes.Add(role.RelationTypeWhereRoleType.AssociationType);
                }
            }

            var exclusiveAssociationTypeArray = new MetaAssociation[exclusiveAssociationTypes.Count];
            exclusiveAssociationTypes.CopyTo(exclusiveAssociationTypeArray);

            this.DerivedExclusiveAssociationTypes = exclusiveAssociationTypeArray;
        }

        /// <summary>
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        /// <param name="concreteLeafClasses">The concrete leaf classes.</param>
        internal void DeriveExclusiveConcreteLeafClass(HashSet<MetaObject> concreteLeafClasses)
        {
            concreteLeafClasses.Clear();

            this.DerivedExclusiveConcreteLeafClass = null;
            if (this.IsConcrete && !this.ExistSubclasses)
            {
                concreteLeafClasses.Add(this);
            }

            foreach (var rootClass in this.RootClasses)
            {
                foreach (var rootSubClass in rootClass.Subclasses)
                {
                    if (rootSubClass.IsConcrete && !rootSubClass.ExistSubclasses)
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
                var concreteLeafClassArray = new MetaObject[concreteLeafClasses.Count];
                concreteLeafClasses.CopyTo(concreteLeafClassArray);

                this.DerivedExclusiveConcreteLeafClass = concreteLeafClassArray[0];
            }
        }

        /// <summary>
        /// Derive exclusive roles.
        /// </summary>
        /// <param name="exclusiveRoles">The exclusive roles.</param>
        internal void DeriveExclusiveRoleTypes(HashSet<MetaRole> exclusiveRoles)
        {
            exclusiveRoles.Clear();
            foreach (var association in this.AssociationTypesWhereObjectType)
            {
                exclusiveRoles.Add(association.RelationTypeWhereAssociationType.RoleType);
            }

            foreach (var superType in this.ExclusiveSuperinterfaces)
            {
                foreach (var association in superType.AssociationTypesWhereObjectType)
                {
                    exclusiveRoles.Add(association.RelationTypeWhereAssociationType.RoleType);
                }
            }

            var exclusiveRoleArray = new MetaRole[exclusiveRoles.Count];
            exclusiveRoles.CopyTo(exclusiveRoleArray);

            this.DerivedExclusiveRoleTypes = exclusiveRoleArray;
        }

        /// <summary>
        /// Derive exclusive super interfaces.
        /// </summary>
        /// <param name="superInterfaces">The super interfaces.</param>
        internal void DeriveExclusiveSuperinterfaces(HashSet<MetaObject> superInterfaces)
        {
            superInterfaces.Clear();
            foreach (var superType in this.DerivedSupertypes)
            {
                if (superType.IsInterface && !superType.IsImplementedByAnyOf(this.Superclasses))
                {
                    superInterfaces.Add(superType);
                }
            }

            var superInterfaceArray = new MetaObject[superInterfaces.Count];
            superInterfaces.CopyTo(superInterfaceArray);

            this.DerivedExclusiveSuperinterfaces = superInterfaceArray;
        }

        /// <summary>
        /// Derive method types.
        /// </summary>
        /// <param name="methodTypes">
        /// The method types.
        /// </param>
        internal void DeriveMethodTypes(HashSet<MetaMethod> methodTypes)
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

            var methodTypeArray = new MetaMethod[methodTypes.Count];
            methodTypes.CopyTo(methodTypeArray);

            this.DerivedMethodTypes = methodTypeArray;
        }

        /// <summary>
        /// Derive role types.
        /// </summary>
        /// <param name="roleTypes">The role types.</param>
        internal void DeriveRoleTypes(HashSet<MetaRole> roleTypes)
        {
            roleTypes.Clear();
            foreach (var association in this.AssociationTypesWhereObjectType)
            {
                roleTypes.Add(association.RelationTypeWhereAssociationType.RoleType);
            }

            foreach (var superType in this.Supertypes)
            {
                foreach (var association in superType.AssociationTypesWhereObjectType)
                {
                    roleTypes.Add(association.RelationTypeWhereAssociationType.RoleType);
                }
            }

            var roleTypeArray = new MetaRole[roleTypes.Count];
            roleTypes.CopyTo(roleTypeArray);

            this.DerivedRoleTypes = roleTypeArray;
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
                this.DerivedRootClasses = new[] { this };
            }
        }

        /// <summary>
        /// Derive subclasses.
        /// </summary>
        /// <param name="subClasses">The sub classes.</param>
        internal void DeriveSubclasses(HashSet<MetaObject> subClasses)
        {
            subClasses.Clear();
            foreach (var subType in this.ObjectTypesWhereDerivedSupertype)
            {
                if (!subType.IsInterface)
                {
                    subClasses.Add(subType);
                }
            }

            var subClassArray = new MetaObject[subClasses.Count];
            subClasses.CopyTo(subClassArray);

            this.DerivedSubclasses = subClassArray;
        }

        /// <summary>
        /// Derive sub interfaces.
        /// </summary>
        /// <param name="subInterfaces">The sub interfaces.</param>
        internal void DeriveSubinterfaces(HashSet<MetaObject> subInterfaces)
        {
            subInterfaces.Clear();
            foreach (var subType in this.ObjectTypesWhereDerivedSupertype)
            {
                if (subType.IsInterface)
                {
                    subInterfaces.Add(subType);
                }
            }

            var subInterfaceArray = new MetaObject[subInterfaces.Count];
            subInterfaces.CopyTo(subInterfaceArray);

            this.DerivedSubinterfaces = subInterfaceArray;
        }

        /// <summary>
        /// Derive super classes.
        /// </summary>
        /// <param name="superClasses">The super classes.</param>
        internal void DeriveSuperclasses(HashSet<MetaObject> superClasses)
        {
            superClasses.Clear();
            foreach (var superTypes in this.DerivedSupertypes)
            {
                if (!superTypes.IsInterface)
                {
                    superClasses.Add(superTypes);
                }
            }

            var superClassArray = new MetaObject[superClasses.Count];
            superClasses.CopyTo(superClassArray);

            this.DerivedSuperclasses = superClassArray;
        }

        /// <summary>
        /// Derive super interface.
        /// </summary>
        /// <param name="superInterfaces">The super interfaces.</param>
        internal void DeriveSuperinterfaces(HashSet<MetaObject> superInterfaces)
        {
            superInterfaces.Clear();
            foreach (var superType in this.DerivedSupertypes)
            {
                if (superType.IsInterface)
                {
                    superInterfaces.Add(superType);
                }
            }

            var superInterfaceArray = new MetaObject[superInterfaces.Count];
            superInterfaces.CopyTo(superInterfaceArray);

            this.DerivedSuperinterfaces = superInterfaceArray;
        }

        /// <summary>
        /// Derive super types.
        /// </summary>
        /// <param name="superTypes">The super types.</param>
        internal void DeriveSupertypes(HashSet<MetaObject> superTypes)
        {
            superTypes.Clear();
            this.DeriveSupertypesRecursively(this, superTypes);

            var superTypeArray = new MetaObject[superTypes.Count];
            superTypes.CopyTo(superTypeArray);

            this.DerivedSupertypes = superTypeArray;
        }

        /// <summary>
        /// Derive unit role types.
        /// </summary>
        /// <param name="roles">The roles.</param>
        internal void DeriveUnitRoleTypes(HashSet<MetaRole> roles)
        {
            roles.Clear();
            foreach (var role in this.DerivedRoleTypes)
            {
                // TODO: Test
                if (role.ExistObjectType && role.ObjectType.IsUnit)
                {
                    roles.Add(role);
                }
            }

            var roleArray = new MetaRole[roles.Count];
            roles.CopyTo(roleArray);

            this.DerivedUnitRoleTypes = roleArray;
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (this.ExistSingularName)
            {
                if (this.SingularName.Length < 2)
                {
                    var message = this.ValidationName + " should have a singular name with at least 2 characters";
                    validationLog.AddError(message, this, ValidationKind.MinimumLength, AllorsEmbeddedDomain.ObjectTypeSingularName);
                }
                else
                {
                    if (!char.IsLetter(this.SingularName[0]))
                    {
                        var message = this.ValidationName + "'s singular name should start with an alfabetical character";
                        validationLog.AddError(message, this, ValidationKind.Format, AllorsEmbeddedDomain.ObjectTypeSingularName);
                    }

                    for (var i = 1; i < this.SingularName.Length; i++)
                    {
                        if (!char.IsLetter(this.SingularName[i]) && !char.IsDigit(this.SingularName[i]))
                        {
                            var message = this.ValidationName + "'s singular name should only contain alfanumerical characters";
                            validationLog.AddError(message, this, ValidationKind.Format, AllorsEmbeddedDomain.ObjectTypeSingularName);
                            break;
                        }
                    }
                }

                if (validationLog.ExistObjectTypeName(this.SingularName))
                {
                    var message = "The singular name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, AllorsEmbeddedDomain.ObjectTypeSingularName);
                }
                else
                {
                    validationLog.AddObjectTypeName(this.SingularName);
                }
            }
            else
            {
                validationLog.AddError(this.ValidationName + " has no singular name", this, ValidationKind.Required, AllorsEmbeddedDomain.ObjectTypeSingularName);
            }

            if (this.ExistPluralName)
            {
                if (this.PluralName.Length < 2)
                {
                    var message = this.ValidationName + " should have a plural name with at least 2 characters";
                    validationLog.AddError(message, this, ValidationKind.MinimumLength, AllorsEmbeddedDomain.ObjectTypePluralName);
                }
                else
                {
                    if (!char.IsLetter(this.PluralName[0]))
                    {
                        var message = this.ValidationName + "'s plural name should start with an alfabetical character";
                        validationLog.AddError(message, this, ValidationKind.Format, AllorsEmbeddedDomain.ObjectTypePluralName);
                    }

                    for (var i = 1; i < this.PluralName.Length; i++)
                    {
                        if (!char.IsLetter(this.PluralName[i]) && !char.IsDigit(this.PluralName[i]))
                        {
                            var message = this.ValidationName + "'s plural name should only contain alfanumerical characters";
                            validationLog.AddError(message, this, ValidationKind.Format, AllorsEmbeddedDomain.ObjectTypePluralName);
                            break;
                        }
                    }
                }

                if (validationLog.ExistObjectTypeName(this.PluralName))
                {
                    var message = "The plural name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, AllorsEmbeddedDomain.ObjectTypePluralName);
                }
                else
                {
                    validationLog.AddObjectTypeName(this.PluralName);
                }
            }
            else
            {
                validationLog.AddError(this.ValidationName + " has no plural name", this, ValidationKind.Required, AllorsEmbeddedDomain.ObjectTypePluralName);
            }
        }

        /// <summary>
        /// Gets the unit roles.
        /// </summary>
        /// <param name="unitTypeTags">The unit type tag.</param>
        /// <returns>The roles.</returns>
        private MetaRole[] GetUnitRoleTypes(MetaUnitTags unitTypeTags)
        {
            var roles = new List<MetaRole>(this.UnitRoleTypes.Length);
            foreach (var role in this.UnitRoleTypes)
            {
                if (role.ObjectType.UnitTag == (int)unitTypeTags)
                {
                    roles.Add(role);
                }
            }

            return roles.ToArray();
        }

        /// <summary>
        /// Determines whether this instance is implemented by any of the specified object types.
        /// </summary>
        /// <param name="objectTypes">The object types.</param>
        /// <returns>
        ///  <c>true</c> if this instance is implemented by any of the specified object types; otherwise, <c>false</c>.
        /// </returns>
        private bool IsImplementedByAnyOf(IEnumerable<MetaObject> objectTypes)
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
        private void DeriveSupertypesRecursively(MetaObject type, HashSet<MetaObject> superTypes)
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