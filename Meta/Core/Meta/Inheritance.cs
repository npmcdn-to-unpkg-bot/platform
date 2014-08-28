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
    using System.Linq;

    /// <summary>
    /// Defines a subtype/supertype relation between two <see cref="ObjectType"/>s.
    /// </summary>
    public sealed partial class Inheritance : MetaObject
    {
        public Inheritance(Domain domain, Guid inheritanceId)
        {
            this.Domain = domain;
            this.Id = inheritanceId;

            this.Domain.OnInheritanceCreated(this);
        }

        public CompositeType Subtype { get; set; }

        public Interface Supertype { get; set; }

        public Domain Domain { get; private set; }

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
                if (this.HasCycle(this.Subtype))
                {
                    var message = this.ValidationName + " has a cycle in its inheritance hierarchy";
                    validationLog.AddError(message, this, ValidationKind.Cyclic, "Inheritance.Subtype");
                }

                if (this.Domain.Inheritances.Count(inheritance => this.Subtype.Equals(inheritance.Subtype) && this.Supertype.Equals(inheritance.Supertype)) != 1)
                {
                    var message = "name of " + this.ValidationName + " is already in use";
                    validationLog.AddError(message, this, ValidationKind.Unique, "Inheritance.Supertype");
                }

                ObjectType tempQualifier = this.Supertype;
                if (tempQualifier is Class)
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

        private bool HasCycle(CompositeType startSubType)
        {
            if (startSubType.Equals(this.Supertype))
            {
                return true;
            }

            return this.Supertype != null && this.Domain.Inheritances
                .Where(inheritance => this.Supertype.Equals(inheritance.Subtype))
                .Any(superInheritance => superInheritance.HasCycle(startSubType));
        }
    }
}