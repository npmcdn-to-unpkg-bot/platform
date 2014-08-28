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

    /// <summary>
    /// A <see cref="RoleType"/> defines the role side of a relation.
    /// This is also called the 'passive' side.
    /// RoleTypes can have composite and unit <see cref="ObjectType"/>s.
    /// </summary>
    public partial class RoleType : PropertyType, IComparable
    {
        /// <summary>
        /// The maximum size value.
        /// </summary>
        public const int MaximumSize = -1;

        private string derivedSingularPropertyName;

        private string derivedPluralPropertyName;

        public RoleType(RelationType relationType, Guid roleTypeId)
        {
            this.RelationType = relationType;
            this.Id = roleTypeId;
        }

        public ObjectType ObjectType { get; set; }

        public string AssignedSingularName { get; set; }

        public string AssignedPluralName { get; set; }

        public bool IsMany { get; set; }

        public int? Scale { get; set; }

        public int? Precision { get; set; }

        public int? Size { get; set; }

        public RelationType RelationType { get; private set; }

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
                    return this.PluralName;
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
        /// Gets the full singular name.
        /// </summary>
        /// <value>The full singular name.</value>
        public string SingularFullName
        {
            get { return this.RelationType.AssociationType.SingularName + this.SingularName; }
        }

        /// <summary>
        /// Gets the full plural name.
        /// </summary>
        /// <value>The full plural name.</value>
        public string PluralFullName
        {
            get { return this.RelationType.AssociationType.SingularName + this.PluralName; }
        }

        public string SingularPropertyName
        {
            get
            {
                return this.derivedSingularPropertyName;
            }
        }

        public string PluralPropertyName
        {
            get
            {
                return this.derivedPluralPropertyName;
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
            var unitType = this.ObjectType as Unit;
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

        internal void DeriveSingularPropertyName()
        {
            this.derivedSingularPropertyName = null;

            if (this.ObjectType != null && this.AssociationType.ObjectType != null)
            {
                this.derivedSingularPropertyName = this.SingularFullName;

                if (this.AssociationType.ObjectType.RootClasses.Count > 0)
                {
                    this.derivedSingularPropertyName = this.SingularName;

                    foreach (var rootClass in this.AssociationType.ObjectType.RootClasses)
                    {
                        foreach (var otherRole in rootClass.RoleTypes)
                        {
                            if (!Equals(otherRole))
                            {
                                if (otherRole.ObjectType != null)
                                {
                                    if (otherRole.SingularName.Equals(this.SingularName))
                                    {
                                        this.derivedSingularPropertyName = this.SingularFullName;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal void DerivePluralPropertyName()
        {
            this.derivedPluralPropertyName = null;

            if (this.ObjectType != null && this.AssociationType.ObjectType != null)
            {
                this.derivedPluralPropertyName = this.PluralFullName;

                if (this.AssociationType.ObjectType.RootClasses.Count > 0)
                {
                    this.derivedPluralPropertyName = this.PluralName;

                    foreach (var rootClass in this.AssociationType.ObjectType.RootClasses)
                    {
                        foreach (var otherRole in rootClass.RoleTypes)
                        {
                            if (!Equals(otherRole))
                            {
                                if (otherRole.ObjectType != null)
                                {
                                    if (otherRole.PluralName.Equals(this.PluralName))
                                    {
                                        this.derivedPluralPropertyName = this.PluralFullName;
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
                var unitType = this.ObjectType as Unit;
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