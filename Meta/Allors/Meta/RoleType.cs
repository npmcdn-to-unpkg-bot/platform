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

    /// <summary>
    /// A <see cref="RoleType"/> defines the role side of a relation.
    /// This is also called the 'passive' side.
    /// RoleTypes can have composite and unit <see cref="ObjectType"/>s.
    /// </summary>
    public partial class RoleType : PropertyType, IComparable
    {
        private readonly RelationType relationType;

        private ObjectType objectType;

        private string derivedSingularPropertyName;

        private string derivedPluralPropertyName;

        private string assignedSingularName;

        private string assignedPluralName;

        private bool isMany;

        public RoleType(RelationType relationType)
        {
            this.relationType = relationType;
        }

        public ObjectType ObjectType
        {
            get
            {
                return this.objectType;
            }

            set
            {
                this.RelationType.Environment.AssertUnlocked();
                this.objectType = value;
                this.RelationType.Environment.Stale();
            }
        }

        public string AssignedSingularName
        {
            get
            {
                return this.assignedSingularName;
            }

            set
            {
                this.RelationType.Environment.AssertUnlocked();
                this.assignedSingularName = value;
                this.RelationType.Environment.Stale();
            }
        }

        public string AssignedPluralName
        {
            get
            {
                return this.assignedPluralName;
            }

            set
            {
                this.RelationType.Environment.AssertUnlocked();
                this.assignedPluralName = value;
                this.RelationType.Environment.Stale();
            }
        }

        public bool IsMany
        {
            get
            {
                return this.isMany;
            }

            set
            {
                this.RelationType.Environment.AssertUnlocked();
                this.isMany = value;
                this.RelationType.Environment.Stale();
            }
        }

        public RelationType RelationType
        {
            get
            {
                return this.relationType;
            }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
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
        public string Name
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

                return this.ObjectType != null ? this.ObjectType.SingularName : null;
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

                return this.ObjectType != null ? this.ObjectType.PluralName : null;
            }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                if (this.IsMany)
                {
                    return this.PluralFullName;
                }

                return this.SingularFullName;
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

        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>The full name</value>
        public string PropertyName
        {
            get
            {
                if (this.IsMany)
                {
                    return this.PluralPropertyName;
                }

                return this.SingularPropertyName;
            }
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
        protected string ValidationName
        {
            get
            {
                return "role type " + RelationType.Name;
            }
        }

        public static int IdComparer(RoleType x, RoleType y)
        {
            return x.RelationType.Id.CompareTo(y.RelationType.Id);
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
        public ObjectType GetObjectType()
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
        internal void DeriveMultiplicity()
        {
            if (this.ObjectType is Unit && this.IsMany)
            {
                this.IsMany = false;
            }
        }

        internal void DeriveSingularPropertyName()
        {
            this.derivedSingularPropertyName = null;

            if (this.ObjectType != null && this.AssociationType.ObjectType != null)
            {
                this.derivedSingularPropertyName = this.SingularFullName;

                if (this.AssociationType.ObjectType.ExistLeafClasses)
                {
                    this.derivedSingularPropertyName = this.SingularName;

                    foreach (var leafClass in this.AssociationType.ObjectType.LeafClasses)
                    {
                        foreach (var otherRole in leafClass.RoleTypes)
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

                if (this.AssociationType.ObjectType.ExistLeafClasses)
                {
                    this.derivedPluralPropertyName = this.PluralName;

                    foreach (var leafClass in this.AssociationType.ObjectType.LeafClasses)
                    {
                        foreach (var otherRole in leafClass.RoleTypes)
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
        internal void Validate(ValidationLog validationLog)
        {
            if (this.ObjectType == null)
            {
                var message = this.ValidationName + " has no ObjectType";
                validationLog.AddError(message, this, ValidationKind.Required, "RoleType.ObjectType");
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