//------------------------------------------------------------------------------------------------- 
// <copyright file="RelationType.cs" company="Allors bvba">
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
// <summary>Defines the RelationType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// A <see cref="RelationType"/> defines the state and behavior for
    /// a set of <see cref="AssociationType"/>s and <see cref="RoleType"/>s.
    /// </summary>
    public partial class RelationType : MetaObject, IComparable
    {
        public bool IsDerived;

        public bool IsIndexed;

        public RoleType RoleType;

        public AssociationType AssociationType;

        // Domain -> RelationType
        public Domain Domain { get; private set; }

        public RelationType(Domain domain, Guid relationTypeId, Guid associationTypeId, Guid roleTypeId)
        {
            this.Domain = domain;
            this.Id = relationTypeId;
            this.AssociationType = new AssociationType(this, associationTypeId);
            this.RoleType = new RoleType(this, roleTypeId);

            this.Domain.OnRelationTypeCreated(this);
        }
        
        /// <summary>
        /// Gets a value indicating whether there exist exclusive root classes.
        /// </summary>
        /// <value>
        ///  <c>true</c> if [exist exclusive root classes]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistExclusiveRootClasses
        {
            get
            {
                if (this.AssociationType != null && this.AssociationType.ObjectType != null &&
                    this.RoleType != null && this.RoleType.ObjectType != null)
                {
                    return this.AssociationType.ObjectType.RootClasses.Count == 1 && this.RoleType.ObjectType.RootClasses.Count == 1;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is many to many.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is many to many; otherwise, <c>false</c>.
        /// </value>
        public bool IsManyToMany
        {
            get { return this.AssociationType.IsMany && this.RoleType.IsMany; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is many to one.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is many to one; otherwise, <c>false</c>.
        /// </value>
        public bool IsManyToOne
        {
            get { return this.AssociationType.IsMany && !this.RoleType.IsMany; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is one to many.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is one to many; otherwise, <c>false</c>.
        /// </value>
        public bool IsOneToMany
        {
            get { return this.AssociationType.IsOne && this.RoleType.IsMany; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is one to one.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is one to one; otherwise, <c>false</c>.
        /// </value>
        public bool IsOneToOne
        {
            get { return this.AssociationType.IsOne && !this.RoleType.IsMany; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                var validationReport = new ValidationLog();
                this.Validate(validationReport);
                return !validationReport.ContainsErrors;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name  .</value>
        public string Name
        {
            get
            {
                return this.AssociationType.SingularName + this.RoleType.SingularName;
            }
        }

        /// <summary>
        /// Gets the name of the reverse.
        /// </summary>
        /// <value>The name of the reverse.</value>
        public string ReverseName
        {
            get
            {
                return this.RoleType.SingularName + this.AssociationType.SingularName;
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        /// <value>The validation name.</value>
        protected override string ValidationName
        {
            get { return "relation type" + this.Name; }
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
            var that = obj as RelationType;
            if (that != null)
            {
                return string.CompareOrdinal(this.Name, that.Name);
            }

            return -1;
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
                var toString = new StringBuilder();

                if (this.RoleType != null && !string.IsNullOrEmpty(this.RoleType.FullName))
                {
                    toString.Append(this.RoleType.Name);
                }
                else
                {
                    toString.Append(this.IdAsString);
                }

                return toString.ToString();
            }
            catch
            {
                return this.IdAsString;
            }
        }

        /// <summary>
        /// Validates this. instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (this.AssociationType != null && this.RoleType != null)
            {
                if (validationLog.ExistRelationName(this.Name))
                {
                    var message = "name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "RelationType.Name");
                }
                else
                {
                    validationLog.AddRelationTypeName(this.Name);
                }

                if (validationLog.ExistRelationName(this.ReverseName))
                {
                    var message = "reversed name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "RelationType.Name");
                }
                else
                {
                    validationLog.AddRelationTypeName(this.ReverseName);
                }

                if (validationLog.ExistObjectTypeName(this.Name))
                {
                    var message = "name of " + this.ValidationName + " is in conflict with object type " + this.Name;
                    validationLog.AddError(message, this, ValidationKind.Unique, "RelationType.Name");
                }

                if (validationLog.ExistObjectTypeName(this.ReverseName))
                {
                    var message = "reversed name of " + this.ValidationName + " is in conflict with object type " + this.Name;
                    validationLog.AddError(message, this, ValidationKind.Unique, "RelationType.Name");
                }
            }
            else
            {
                if (this.AssociationType == null)
                {
                    var message = this.ValidationName + " has no association type";
                    validationLog.AddError(message, this, ValidationKind.Required, "RelationType.AssociationType");
                }
                else
                {
                    var message = this.ValidationName + " has no role type";
                    validationLog.AddError(message, this, ValidationKind.Required, "RelationType.RoleType");
                }
            }
        }
    }
}