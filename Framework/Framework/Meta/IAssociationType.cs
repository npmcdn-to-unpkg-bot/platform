//------------------------------------------------------------------------------------------------- 
// <copyright file="IAssociationType.cs" company="Allors bvba">
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
    
    /// <summary>
    /// An association type defines the association side of a relation.
    /// This is also called the 'active', 'controlling' or 'owning' side.
    /// AssociationTypes can only have composite <see cref="ObjectType"/>s.
    /// </summary>
    public partial class IAssociationType : IPropertyType, IComparable
    {
        /// <summary>
        /// Used to create property names.
        /// </summary>
        private const string Where = "Where";

        private readonly IRelationType relationType;

        private bool isMany;

        private IComposite objectType;

        public IAssociationType(IRelationType relationType, Guid id)
            : base(relationType.DefiningDomain, id)
        {
            this.relationType = relationType;

            relationType.DefiningDomain.OnAssociationTypeCreated(this);
        }

        public bool IsMany
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.isMany;
            }

            set
            {
                this.MetaPopulation.AssertUnlocked();
                this.isMany = value;
                this.MetaPopulation.Stale();
            }
        }

        public IComposite ObjectType
        {
            get
            {
                return this.objectType;
            }

            set
            {
                this.MetaPopulation.AssertUnlocked();
                this.objectType = value;
                this.MetaPopulation.Stale();
            }
        }

        public IRelationType RelationType
        {
            get
            {
                return this.relationType;
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
                return this.FullName;
            }
        }

        /// <summary>
        /// Gets the singular name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The singular name when using <see cref="Where"/>.</value>
        public string SingularName
        {
            get
            {
                return this.ObjectType != null ? this.ObjectType.SingularName : this.IdAsString;
            }
        }

        /// <summary>
        /// Gets the plural name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The plural name when using <see cref="Where"/>.</value>
        public string PluralName
        {
            get { return this.ObjectType != null ? this.ObjectType.PluralName : this.IdAsString; }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name</value>
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
        /// Gets the singular name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The singular name when using <see cref="Where"/>.</value>
        public string SingularFullName
        {
            get { return this.ObjectType != null ? this.RelationType.RoleType.SingularName + this.ObjectType.SingularName : this.IdAsString; }
        }

        /// <summary>
        /// Gets the plural name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The plural name when using <see cref="Where"/>.</value>
        public string PluralFullName
        {
            get { return this.ObjectType != null ? this.RelationType.RoleType.SingularName + this.ObjectType.PluralName : this.IdAsString; }
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

        /// <summary>
        /// Gets the singular name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The singular name when using <see cref="Where"/>.</value>
        public string SingularPropertyName
        {
            get { return this.ObjectType != null ? this.ObjectType.SingularName + Where + this.RelationType.RoleType.SingularName : this.IdAsNumberString; }
        }

        /// <summary>
        /// Gets the plural name when using <see cref="Where"/>.
        /// </summary>
        /// <value>The plural name when using <see cref="Where"/>.</value>
        public string PluralPropertyName
        {
            get { return this.ObjectType != null ? this.ObjectType.PluralName + Where + this.RelationType.RoleType.SingularName : this.IdAsNumberString; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return this.PropertyName;
            }
        }

        /// <summary>
        /// Get the value of the association on this object.
        /// </summary>
        /// <param name="strategy">
        /// The strategy.
        /// </param>
        /// <returns>
        /// The association value.
        /// </returns>
        public override object Get(IStrategy strategy)
        {
            return strategy.GetAssociation(this);
        }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <value>The role .</value>
        public IRoleType RoleType
        {
            get { return RelationType.RoleType; }
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
        /// Gets the validation name.
        /// </summary>
        /// <value>The name of the validation.</value>
        public override string ValidationName
        {
            get
            {
                return "association type " + RelationType.Name;
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
            var that = obj as IAssociationType;
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
        public override IObjectType GetObjectType()
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
        /// Derive the multiplicity.
        /// </summary>
        public void DeriveMultiplicity()
        {
            if (this.RoleType != null && this.RoleType.ObjectType != null && this.RoleType.ObjectType is IUnit)
            {
                this.IsMany = false;
            }
        }

        /// <summary>
        /// Validates this object.
        /// </summary>
        /// <param name="validationLog">The validation information.</param>
        public override void Validate(ValidationLog validationLog)
        {
            if (this.ObjectType == null)
            {
                var message = this.ValidationName + " has no object type";
                validationLog.AddError(message, this, ValidationKind.Required, "AssociationType.IObjectType");
            }

            if (this.RelationType == null)
            {
                var message = this.ValidationName + " has no relation type";
                validationLog.AddError(message, this, ValidationKind.Required, "AssociationType.RelationType");
            }
        }
    }
}