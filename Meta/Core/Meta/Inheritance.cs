//------------------------------------------------------------------------------------------------- 
// <copyright file="Inheritance.cs" company="Allors bvba">
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
// <summary>Defines the Inheritance type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    /// <summary>
    /// Defines a subtype/supertype relation between two <see cref="ObjectType"/>s.
    /// </summary>
    public sealed partial class Inheritance : MetaObject
    {

        public ObjectType Subtype;

        public ObjectType Supertype;

        internal Inheritance(Domain domain, Guid inheritanceId)
        {
            this.Domain = domain;
            this.Id = inheritanceId;

            this.Domain.OnInheritanceCreated(this);
        }

        // Domain->Inheritance
        protected Domain Domain { get; private set; }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        protected override string ValidationName
        {
            get
            {
                if (this.Supertype != null && this.Subtype != null)
                {
                    return "inheritance " + this.Subtype + "::" + this.Supertype;
                }

                return "unknown inheritance";
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
            return (this.Subtype != null ? this.Subtype.Name : string.Empty) + "::" + (this.Supertype != null ? this.Supertype.Name : string.Empty);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (this.Subtype != null && this.Supertype != null)
            {
                if (this.Subtype.IsCyclicInheritance(this.Supertype))
                {
                    var message = this.ValidationName + " has a cycle in its inheritance hierarchy";
                    validationLog.AddError(message, this, ValidationKind.Cyclic, "Inheritance.Subtype");
                }

                var inheritance = this.Subtype.FindInheritanceWhereDirectSubtype(this.Supertype);
                if (inheritance != null && !this.Equals(inheritance))
                {
                    var message = "name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "Inheritance.Supertype");
                }

                ObjectType tempQualifier = this.Supertype;
                if (!tempQualifier.IsUnit && !tempQualifier.IsInterface)
                {
                    var message = this.ValidationName + " can not have a concrete superclass";
                    validationLog.AddError(message, this, ValidationKind.Hierarchy, "Inheritance.Supertype");
                }
            }
            else
            {
                if (this.Supertype == null)
                {
                    var message = this.ValidationName + " has a missing Supertype";
                    validationLog.AddError(message, this, ValidationKind.Unique, "Inheritance.Supertype");
                }
                else
                {
                    var message = this.ValidationName + " has a missing Subtype";
                    validationLog.AddError(message, this, ValidationKind.Unique, "Inheritance.Supertype");
                }
            }
        }
    }
}