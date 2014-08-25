//------------------------------------------------------------------------------------------------- 
// <copyright file="MetaAssociation.cs" company="Allors bvba">
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
// <summary>Defines the AssociationType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    using AllorsGenerated;

    /// <summary>
    /// An <see cref="MetaAssociation"/> defines the association side of a relation.
    /// This is also called the 'active', 'controlling' or 'owning' side.
    /// AssociationTypes can only have composite <see cref="ObjectType"/>s.
    /// </summary>
    public sealed partial class MetaAssociation : IComparable
    {
        /// <summary>
        /// Used to form names to navigate from <see cref="RoleType"/> To <see cref="MetaAssociation"/>;
        /// </summary>
        private const string Where = "Where";

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
        /// Gets the plural name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The plural name when using <see cref="Where"/>.</value>
        public string PluralName
        {
            get { return this.ObjectType.Name + Where + this.RelationTypeWhereAssociationType.RoleType.SingularName; }
        }

        /// <summary>
        /// Gets the singular name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The singular name when using <see cref="Where"/>.</value>
        public string SingularName
        {
            get { return this.ObjectType.Name + Where + this.RelationTypeWhereAssociationType.RoleType.SingularName; }
        }

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
        /// Gets or sets a value indicating whether this instance has a multiplicity of many.
        /// </summary>
        /// <value><c>true</c> if this instance is many; otherwise, <c>false</c>.</value>
        public override bool IsMany
        {
            get
            {
                this.EnsureRelationTypeDerivations();
                return base.IsMany;
            }

            set
            {
                this.StaleRelationTypeDerivations();
                base.IsMany = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a multiplicity of one.
        /// </summary>
        /// <value><c>true</c> if this instance is one; otherwise, <c>false</c>.</value>
        public bool IsOne
        {
            get { return !this.IsMany; }
            set { this.IsMany = !value; }
        }

        /// <summary>
        /// Gets or sets ObjectType.
        /// </summary>
        /// <value>The ObjectType.</value>
        public override MetaObject ObjectType
        {
            get
            {
                return base.ObjectType;
            }

            set
            {
                this.StaleRelationTypeDerivations();
                base.ObjectType = value;
            }
        }

        /// <summary>
        /// Gets the relation type.
        /// </summary>
        /// <value>The type of the relation.</value>
        public MetaRelation RelationType
        {
            get { return RelationTypeWhereAssociationType; }
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <value>The role .</value>
        public MetaRole RoleType
        {
            get { return RelationTypeWhereAssociationType.RoleType; }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        /// <value>The name of the validation.</value>
        protected override string ValidationName
        {
            get
            {
                if (ExistRelationTypeWhereAssociationType)
                {
                    return "association type " + RelationTypeWhereAssociationType.Name;
                }

                return "unknown association type";
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
            var that = obj as MetaAssociation;
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
        public override MetaObject GetObjectType()
        {
            return this.ObjectType;
        }

        /// <summary>
        /// Removes the IsMany.
        /// </summary>
        public override void RemoveIsMany()
        {
            this.StaleRelationTypeDerivations();
            base.RemoveIsMany();
        }

        /// <summary>
        /// Removes the ObjectType.
        /// </summary>
        public override void RemoveObjectType()
        {
            this.StaleRelationTypeDerivations();
            base.RemoveObjectType();
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
        /// Creates a new instance.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>A new instance</returns>
        internal static MetaAssociation Create(AllorsEmbeddedSession session)
        {
            var association = (MetaAssociation)session.Create(AllorsEmbeddedDomain.AssociationType);

            association.IsMany = false;

            return association;
        }

        /// <summary>
        /// Derive the multiplicity.
        /// </summary>
        internal void DeriveMultiplicity()
        {
            if (this.RoleType != null && this.RoleType.ExistObjectType && this.RoleType.ObjectType.IsUnit)
            {
                if (!this.ExistIsMany || this.IsMany)
                {
                    base.IsMany = false;
                }
            }
        }

        /// <summary>
        /// Validates this object.
        /// </summary>
        /// <param name="validationLog">The validation information.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (!ExistObjectType)
            {
                var message = this.ValidationName + " has no object type";
                validationLog.AddError(message, this, ValidationKind.Required, AllorsEmbeddedDomain.AssociationTypeObjectType);
            }

            if (!ExistRelationTypeWhereAssociationType)
            {
                var message = this.ValidationName + " has no relation type";
                validationLog.AddError(message, this, ValidationKind.Required, AllorsEmbeddedDomain.RelationTypeAssociationType);
            }
        }

        /// <summary>
        /// Ensures that the relation type derivations are up to date.
        /// </summary>
        private void EnsureRelationTypeDerivations()
        {
            if (ExistRelationTypeWhereAssociationType)
            {
                RelationTypeWhereAssociationType.EnsureRelationTypeDerivations();
            }
            else if (ExistObjectType)
            {
                ObjectType.EnsureRelationTypeDerivations();
            }
            else
            {
                this.Domain.EnsureRelationTypeDerivations();
            }
        }

        /// <summary>
        /// Make the relation type derivations stale.
        /// </summary>
        private void StaleRelationTypeDerivations()
        {
            if (this.ExistRelationTypeWhereAssociationType)
            {
                this.RelationTypeWhereAssociationType.Domain.StaleRelationTypeDerivations();
            }
            else if (this.ExistObjectType)
            {
                this.ObjectType.StaleRelationTypeDerivations();
            }
            else
            {
                this.Domain.StaleRelationTypeDerivations();
            }
        }
    }
}