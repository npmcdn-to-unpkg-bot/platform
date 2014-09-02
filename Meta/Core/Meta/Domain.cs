//------------------------------------------------------------------------------------------------- 
// <copyright file="Part.cs" company="Allors bvba">
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
// <summary>Defines the Domain type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A Part is a Domain that can be used together with other parts to form a whole.
    /// </summary>
    public sealed partial class Domain : MetaObject, IComparable
    {
        private string name;

        public Domain(Environment environment, Guid id)
        {
            this.Environment = environment;

            this.Id = id;

            this.DirectSuperdomains = new List<Domain>();
            this.DefinedUnits = new List<Unit>();
            this.DefinedInterfaces = new List<Interface>();
            this.DefinedClasses = new List<Class>();
            this.DefinedInheritances = new List<Inheritance>();
            this.DefinedRelationTypes = new List<RelationType>();
            this.DefinedMethodTypes = new List<MethodType>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.Environment.Stale();
            }
        }

        public IList<Domain> DirectSuperdomains { get; private set; }

        public IList<Unit> DefinedUnits { get; private set; }

        public IList<Interface> DefinedInterfaces { get; private set; }

        public IList<Class> DefinedClasses { get; private set; }

        public IList<Inheritance> DefinedInheritances { get; private set; }

        public IList<RelationType> DefinedRelationTypes { get; private set; }

        public IList<MethodType> DefinedMethodTypes { get; private set; }
      
        /// <summary>
        /// Gets the validation name.
        /// </summary>
        protected override string ValidationName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Name))
                {
                    return "domain " + this.Name;
                }

                return "unknown domain";
            }
        }

        public void AddDirectSuperdomain(Domain superdomain)
        {
            this.DirectSuperdomains.Add(superdomain);
            this.Environment.Stale();
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
            var that = obj as ObjectType;
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
            if (!string.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }

            return this.IdAsString;
        }
        
        internal void OnUnitCreated(Unit unit)
        {
            this.DefinedUnits.Add(unit);
            this.Environment.OnUnitCreated(unit);
        }

        internal void OnInterfaceCreated(Interface @interface)
        {
            this.DefinedInterfaces.Add(@interface);
            this.Environment.OnInterfaceCreated(@interface);
        }

        internal void OnClassCreated(Class @class)
        {
            this.DefinedClasses.Add(@class);
            this.Environment.OnClassCreated(@class);
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.DefinedInheritances.Add(inheritance);
            this.Environment.OnInheritanceCreated(inheritance);
        }

        internal void OnRelationTypeCreated(RelationType relationType)
        {
            this.DefinedRelationTypes.Add(relationType);
            this.Environment.OnRelationTypeCreated(relationType);
        }

        internal void OnMethodTypeCreated(MethodType methodType)
        {
            this.DefinedMethodTypes.Add(methodType);
            this.Environment.OnMethodTypeCreated(methodType);
        }

        /// <summary>
        /// Validates the domain.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (string.IsNullOrEmpty(this.Name))
            {
                validationLog.AddError("domain has no name", this, ValidationKind.Required, "Domain.Name");
            }
            else
            {
                if (!char.IsLetter(this.Name[0]))
                {
                    var message = this.ValidationName + " should start with an alfabetical character";
                    validationLog.AddError(message, this, ValidationKind.Format, "Domain.Name");
                }

                for (var i = 1; i < this.Name.Length; i++)
                {
                    if (!char.IsLetter(this.Name[i]) && !char.IsDigit(this.Name[i]))
                    {
                        var message = this.ValidationName + " should only contain alfanumerical characters)";
                        validationLog.AddError(message, this, ValidationKind.Format, "Domain.Name");
                        break;
                    }
                }
            }

            if (this.Id == Guid.Empty)
            {
                validationLog.AddError(this.ValidationName + " has no id", this, ValidationKind.Required, "MetaObject.Id");
            }
        }
    }
}