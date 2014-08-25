//------------------------------------------------------------------------------------------------- 
// <copyright file="MetaRelation.cs" company="Allors bvba">
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

    using AllorsGenerated;

    /// <summary>
    /// A <see cref="MetaRelation"/> defines the state and behavior for
    /// a set of <see cref="AssociationType"/>s and <see cref="RoleType"/>s.
    /// </summary>
    public partial class MetaRelation : IComparable
    {
        /// <summary>
        /// Gets or sets the <see cref="AssociationType"/>.
        /// </summary>
        /// <value>The AssociationType.</value>
        public override MetaAssociation AssociationType
        {
            get
            {
                return base.AssociationType;
            }

            set
            {
                if (this.ExistAssociationType)
                {
                    throw new ArgumentException("AssociationType is write once");
                }

                base.AssociationType = value;
            }
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
                if (this.ExistAssociationType && this.AssociationType.ExistObjectType &&
                    this.ExistRoleType && this.RoleType.ExistObjectType)
                {
                    return this.AssociationType.ObjectType.ExistExclusiveRootClass && this.RoleType.ObjectType.ExistExclusiveRootClass;
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
                try
                {
                    return this.AssociationType.SingularName + this.RoleType.SingularName;
                }
                catch
                {
                    return this.IdAsString;
                }
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
                try
                {
                    return this.RoleType.SingularName + this.AssociationType.SingularName;
                }
                catch
                {
                    return this.IdAsString;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="RoleType"/>.
        /// </summary>
        /// <value>The RoleType    .</value>
        public override MetaRole RoleType
        {
            get
            {
                return base.RoleType;
            }

            set
            {
                if (this.ExistRoleType)
                {
                    throw new ArgumentException("RoleType is write once");
                }

                base.RoleType = value;
            }
        }

        /// <summary>
        /// Gets the safe name.
        /// </summary>
        /// <value>The safe name.</value>
        internal string SafeName
        {
            get
            {
                if (this.ExistId)
                {
                    return this.IdAsString;
                }

                return this.AllorsObjectId.ToString(CultureInfo.InvariantCulture);
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
            var that = obj as MetaRelation;
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

                if (this.ExistRoleType && !string.IsNullOrEmpty(this.RoleType.FullName))
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
        /// Creates a new instance.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The new instance.</returns>
        internal static MetaRelation Create(AllorsEmbeddedSession session)
        {
            var relationType = (MetaRelation)session.Create(AllorsEmbeddedDomain.RelationType);

            relationType.IsIndexed = false;
            relationType.IsDerived = false;

            relationType.AssociationType = MetaAssociation.Create(session);
            relationType.RoleType = MetaRole.Create(session);

            return relationType;
        }

        /// <summary>
        /// Purges the derivations.
        /// </summary>
        internal void PurgeDerivations()
        {
            if (this.ExistRoleType)
            {
                this.RoleType.PurgeDerivations();
            }
        }

        /// <summary>
        /// Validates this. instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (this.ExistAssociationType && this.ExistRoleType)
            {
                if (validationLog.ExistRelationName(this.Name))
                {
                    var message = "name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, AllorsEmbeddedDomain.RoleTypeAssignedSingularName);
                }
                else
                {
                    validationLog.AddRelationTypeName(this.Name);
                }

                if (validationLog.ExistRelationName(this.ReverseName))
                {
                    var message = "reversed name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, AllorsEmbeddedDomain.RoleTypeAssignedSingularName);
                }
                else
                {
                    validationLog.AddRelationTypeName(this.ReverseName);
                }

                if (validationLog.ExistObjectTypeName(this.Name))
                {
                    var message = "name of " + this.ValidationName + " is in conflict with object type " + this.Name;
                    validationLog.AddError(message, this, ValidationKind.Unique, AllorsEmbeddedDomain.RoleTypeAssignedSingularName);
                }

                if (validationLog.ExistObjectTypeName(this.ReverseName))
                {
                    var message = "reversed name of " + this.ValidationName + " is in conflict with object type " + this.Name;
                    validationLog.AddError(message, this, ValidationKind.Unique, AllorsEmbeddedDomain.RoleTypeAssignedSingularName);
                }
            }
            else
            {
                if (!this.ExistAssociationType)
                {
                    var message = this.ValidationName + " has no association type";
                    validationLog.AddError(message, this, ValidationKind.Required, AllorsEmbeddedDomain.RelationTypeAssociationType);
                }

                if (!this.ExistRoleType)
                {
                    var message = this.ValidationName + " has no role type";
                    validationLog.AddError(message, this, ValidationKind.Required, AllorsEmbeddedDomain.RelationTypeRoleType);
                }
            }
        }
    }
}