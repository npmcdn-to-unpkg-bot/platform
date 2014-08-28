//------------------------------------------------------------------------------------------------- 
// <copyright file="RoleType.cs" company="Allors bvba">
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
// <summary>Defines the RoleType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;

    /// <summary>
    /// A <see cref="RoleType"/> defines the role side of a relation.
    /// This is also called the 'passive' side.
    /// RoleTypes can have composite and unit <see cref="ObjectType"/>s.
    /// </summary>
    public partial class RoleType : PropertyType, IComparable
    {
        public ObjectType ObjectType;

        public List<Class> DerivedRootClasses = new List<Class>();

        public int? Scale;

        public int? Precision;

        public int? Size;

        public string DerivedHierarchyPluralName;

        public string DerivedHierarchySingularName;

        public string AssignedPluralName;

        public string DerivedRootName;

        public bool IsMany;

        public string AssignedSingularName;

        public RoleType(RelationType relationType, Guid roleTypeId)
        {
            this.RelationType = relationType;
            this.Id = roleTypeId;
        }

        // RelationType -> RoleType
        public RelationType RelationType { get; private set; }
        
        /// <summary>
        /// The maximum size value.
        /// </summary>
        public const int MaximumSize = -1;

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name .</value>
        public override string Name
        {
            get
            {
                if (this.IsMany)
                {
                    if (this.HierarchyPluralName != null)
                    {
                        return this.HierarchyPluralName;
                    }

                    return this.PluralName;
                }

                if (this.HierarchySingularName != null)
                {
                    return this.HierarchySingularName;
                }

                return this.SingularName;
            }
        }

        /// <summary>
        /// Gets the singular name.
        /// </summary>
        /// <value>The singular name.</value>
        public string SingularName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.AssignedSingularName))
                {
                    return this.AssignedSingularName;
                }

                return this.ObjectType.SingularName;
            }
        }

        /// <summary>
        /// Gets the plural name.
        /// </summary>
        /// <value>The plural name.</value>
        public string PluralName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.AssignedPluralName))
                {
                    return this.AssignedPluralName;
                }

                return this.ObjectType.PluralName;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name .</value>
        public string FullName
        {
            get
            {
                if (this.IsMany)
                {
                    return this.FullPluralName;
                }

                return this.FullSingularName;
            }
        }

        /// <summary>
        /// Gets the full singular name.
        /// </summary>
        /// <value>The full singular name.</value>
        public string FullSingularName
        {
            get { return this.RelationType.AssociationType.SingularName + this.SingularName; }
        }

        /// <summary>
        /// Gets the full plural name.
        /// </summary>
        /// <value>The full plural name.</value>
        public string FullPluralName
        {
            get { return this.RelationType.AssociationType.SingularName + this.PluralName; }
        }

        /// <summary>
        /// Gets the name of the root.
        /// </summary>
        /// <value>The name of the root.</value>
        public string RootName
        {
            get
            {
                return this.DerivedRootName;
            }
        }

        /// <summary>
        /// Gets the hierarchy singular name.
        /// </summary>
        /// <value>The name of the hierarchy singular.</value>
        public string HierarchySingularName
        {
            get
            {
                return this.DerivedHierarchySingularName;
            }
        }

        /// <summary>
        /// Gets the hierarchy plural name.
        /// </summary>
        /// <value>The name of the hierarchy plural.</value>
        public string HierarchyPluralName
        {
            get
            {
                return DerivedHierarchyPluralName;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has multiplicity one.
        /// </summary>
        /// <value><c>true</c> if this instance's multiplicity is one; otherwise, <c>false</c>.</value>
        public bool IsOne
        {
            get { return !this.IsMany; }
            set { this.IsMany = !value; }
        }

        /// <summary>
        /// Gets the association.
        /// </summary>
        /// <value>The association.</value>
        public AssociationType AssociationType
        {
            get { return this.RelationType.AssociationType; }
        }

        /// <summary>
        /// Gets the root objectTypes.
        /// </summary>
        /// <value>The root objectTypes.</value>
        public IList<Class> RootClasses
        {
            get
            {
                return this.DerivedRootClasses;
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
                return "role type " + RelationType.Name;
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
            var that = obj as RoleType;
            if (that != null)
            {
                return string.CompareOrdinal(this.SingularName, that.SingularName);
            }

            return -1;
        }

        /// <summary>
        /// Get the object type.
        /// </summary>
        /// <returns>
        /// The <see cref="ObjectType"/>.
        /// </returns>
        public override ObjectType GetObjectType()
        {
            return this.ObjectType;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            try
            {
                return this.RelationType.ToString();
            }
            catch
            {
                return base.ToString();
            }
        }

        /// <summary>
        /// Derive multiplicity, scale and size.
        /// </summary>
        internal void DeriveMultiplicityScaleAndSize()
        {
            var unitType = this.ObjectType as UnitType;
            if (unitType != null)
            {
                this.IsMany = false;

                switch ((UnitTags)unitType.UnitTag)
                {
                    case UnitTags.AllorsString:
                        if (!this.Size.HasValue)
                        {
                            this.Size = 256;
                        }

                        this.Scale = null;
                        this.Precision = null;
                

                        break;
                    case UnitTags.AllorsBinary:
                        if (!this.Size.HasValue)
                        {
                            this.Size = MaximumSize;
                        }

                        this.Scale = null;
                        this.Precision = null;

                        break;
                    case UnitTags.AllorsDecimal:
                        if (!this.Precision.HasValue)
                        {
                            this.Precision = 19;
                        }

                        if (!this.Scale.HasValue)
                        {
                            this.Scale = 2;
                        }

                        this.Size = null;

                        break;

                    default:
                        this.Size = null;
                        this.Scale = null;
                        this.Precision = null;
                
                        break;
                }
            }
            else
            {
                this.Size = null;
                this.Scale = null;
                this.Precision = null;
            }
        }

        /// <summary>
        /// Derive root objectTypes.
        /// </summary>
        internal void DeriveRootClasses()
        {
            this.DerivedRootClasses =  new List<Class>();

            // TODO: Test
            if (this.AssociationType.ObjectType != null)
            {
                if (this.ObjectType is UnitType)
                {
                    this.DerivedRootClasses = new List<Class>(this.AssociationType.ObjectType.RootClasses);
                }
                else
                {
                    if (!this.RelationType.IsManyToMany && this.RelationType.ExistExclusiveRootClasses && !this.IsMany)
                    {
                        this.DerivedRootClasses = new List<Class>(this.AssociationType.ObjectType.RootClasses);
                    }
                }
            }
        }

        /// <summary>
        /// Derive hierarchy plural name.
        /// </summary>
        /// <param name="objectTypes">The object Types.</param>
        internal void DeriveHierarchyPluralName(HashSet<CompositeType> objectTypes)
        {
            objectTypes.Clear();
            this.DerivedHierarchyPluralName = null;

            if (this.ObjectType != null && this.AssociationType.ObjectType != null)
            {
                objectTypes.Add(this.AssociationType.ObjectType);
                objectTypes.UnionWith(this.AssociationType.ObjectType.Supertypes);

                var associationInterface = this.AssociationType.ObjectType as Interface;
                if (associationInterface != null)
                {
                    objectTypes.UnionWith(associationInterface.Subtypes);
                    foreach (var subType in associationInterface.Subtypes)
                    {
                        foreach (var superType in subType.Supertypes)
                        {
                            if (!objectTypes.Contains(superType))
                            {
                                objectTypes.Add(superType);
                            }
                        }
                    }
                }

                this.DerivedHierarchyPluralName = this.PluralName;

                foreach (var objectType in objectTypes)
                {
                    foreach (var otherAsssociation in objectType.AssociationTypes)
                    {
                        if (!this.AssociationType.Equals(otherAsssociation))
                        {
                            if (otherAsssociation.RelationType != null)
                            {
                                var otherRelationType = otherAsssociation.RelationType;
                                if (otherRelationType.RoleType != null)
                                {
                                    var otherRole = otherRelationType.RoleType;
                                    if (otherRole.ObjectType != null)
                                    {
                                        // TODO: Test for PluralName == null
                                        if (otherRole.PluralName.Equals(this.PluralName))
                                        {
                                            DerivedHierarchyPluralName = this.FullPluralName;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Derive hierarchy singular name.
        /// </summary>
        /// <param name="objectTypes">The object types.</param>
        internal void DeriveHierarchySingularName(HashSet<CompositeType> objectTypes)
        {
            objectTypes.Clear();
            this.DerivedHierarchySingularName = null;

            if (this.ObjectType != null && this.AssociationType.ObjectType != null)
            {
                var myAssociation = this.AssociationType;
                var myAssociationType = myAssociation.ObjectType;

                objectTypes.Add(myAssociationType);
                objectTypes.UnionWith(myAssociationType.Supertypes);

                var myAssociationInterface = myAssociationType as Interface;
                if (myAssociationInterface != null)
                {
                    objectTypes.UnionWith(myAssociationInterface.Subtypes);
                    foreach (var subType in myAssociationInterface.Subtypes)
                    {
                        foreach (var superType in subType.Supertypes)
                        {
                            if (!objectTypes.Contains(superType))
                            {
                                objectTypes.Add(superType);
                            }
                        }
                    }
                }

                DerivedHierarchySingularName = this.SingularName;

                foreach (var type in objectTypes)
                {
                    foreach (var otherAsssociation in type.AssociationTypes)
                    {
                        if (!myAssociation.Equals(otherAsssociation))
                        {
                            if (otherAsssociation.RelationType != null)
                            {
                                var otherRelationType = otherAsssociation.RelationType;
                                if (otherRelationType.RoleType != null)
                                {
                                    var otherRole = otherRelationType.RoleType;
                                    if (otherRole.ObjectType != null)
                                    {
                                        if (otherRole.SingularName.Equals(this.SingularName))
                                        {
                                            DerivedHierarchySingularName = this.FullSingularName;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Derive root name.
        /// </summary>
        internal void DeriveRootName()
        {
            this.DerivedRootName = null;

            if (this.ObjectType != null && this.AssociationType.ObjectType != null)
            {
                this.DerivedRootName = this.FullSingularName;

                if (this.DerivedRootClasses.Count > 0)
                {
                    this.DerivedRootName = this.SingularName;

                    foreach (var rootType in this.DerivedRootClasses)
                    {
                        foreach (var otherRole in rootType.DerivedRoleTypes)
                        {
                            if (!Equals(otherRole))
                            {
                                if (otherRole.ObjectType != null)
                                {
                                    if (otherRole.SingularName.Equals(this.SingularName))
                                    {
                                        DerivedRootName = this.FullSingularName;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validates the instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (this.ObjectType != null)
            {
                var message = this.ValidationName + " has no ObjectType";
                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.ObjectType");
            }
            else
            {
                var unitType = this.ObjectType as UnitType;
                if (unitType != null)
                {
                    switch ((UnitTags)unitType.UnitTag)
                    {
                        case UnitTags.AllorsString:
                            if (this.Size.HasValue)
                            {
                                var message = this.ValidationName + " should have a size.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Size");
                            }

                            break;
                        case UnitTags.AllorsBinary:
                            if (this.Size.HasValue)
                            {
                                var message = this.ValidationName + " should have a size.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Size");
                            }

                            break;
                        case UnitTags.AllorsDecimal:
                            if (this.Precision.HasValue)
                            {
                                var message = this.ValidationName + " should have a precision.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Precision");
                            }

                            if (this.Scale.HasValue)
                            {
                                var message = this.ValidationName + " should have a scale.";
                                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.Scale");
                            }

                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.AssignedSingularName) && string.IsNullOrEmpty(this.AssignedPluralName))
            {
                var message = this.ValidationName + " has a singular but no plural name";
                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.AssignedPluralName");
            }

            if (!string.IsNullOrEmpty(this.AssignedSingularName) && this.AssignedSingularName.Length < 2)
            {
                var message = this.ValidationName + " should have an assigned singular name with at least 2 characters";
                validationLog.AddError(message, this, ValidationKind.MinimumLength, "RoleType.AssignedSingularName");
            }

            if (!string.IsNullOrEmpty(this.AssignedPluralName) && this.AssignedPluralName.Length < 2)
            {
                var message = this.ValidationName + " should have an assigned plural role name with at least 2 characters";
                validationLog.AddError(message, this, ValidationKind.MinimumLength, "RoleType.AssignedPluralName");
            }
        }
    }
}