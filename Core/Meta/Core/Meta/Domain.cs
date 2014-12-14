//------------------------------------------------------------------------------------------------- 
// <copyright file="Domain.cs" company="Allors bvba">
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
    using System.Linq;

    public sealed partial class Domain : MetaObject
    {
        private string name;

        private IList<Domain> directSuperdomains;

        private IList<Domain> derivedSuperdomains;

        private IList<Unit> definedUnits;

        private IList<Interface> definedInterfaces;

        private IList<Class> definedClasses;

        private IList<Inheritance> definedInheritances;

        private IList<RelationType> definedRelationTypes;

        private IList<AssociationType> definedAssociationTypes;

        private IList<RoleType> definedRoleTypes;

        private IList<MethodType> definedMethodTypes;

        internal Domain(MetaPopulation metaPopulation, Guid id)
            : base(metaPopulation, id)
        {
            this.directSuperdomains = new List<Domain>();

            this.definedUnits = new List<Unit>();
            this.definedInterfaces = new List<Interface>();
            this.definedClasses = new List<Class>();
            this.definedInheritances = new List<Inheritance>();
            this.definedRelationTypes = new List<RelationType>();
            this.definedAssociationTypes = new List<AssociationType>();
            this.definedRoleTypes = new List<RoleType>();
            this.definedMethodTypes = new List<MethodType>();

            this.MetaPopulation.OnDomainCreated(this);
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.MetaPopulation.AssertUnlocked();
                this.name = value;
                this.MetaPopulation.Stale();
            }
        }

        public IEnumerable<Domain> DirectSuperdomains
        {
            get
            {
                return this.directSuperdomains;
            }
        }

        public IEnumerable<Domain> Superdomains
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSuperdomains;
            }
        }

        public IEnumerable<Unit> DefinedUnits
        {
            get
            {
                return this.definedUnits;
            }
        }

        public IEnumerable<Interface> DefinedInterfaces
        {
            get
            {
                return this.definedInterfaces;
            }
        }

        public IEnumerable<Class> DefinedClasses
        {
            get
            {
                return this.definedClasses;
            }
        }

        public IEnumerable<Inheritance> DefinedInheritances
        {
            get
            {
                return this.definedInheritances;
            }
        }

        public IEnumerable<RelationType> DefinedRelationTypes
        {
            get
            {
                return this.definedRelationTypes;
            }
        }

        public IEnumerable<AssociationType> DefinedAssociationTypes
        {
            get
            {
                return this.definedAssociationTypes;
            }
        }

        public IEnumerable<RoleType> DefinedRoleTypes
        {
            get
            {
                return this.definedRoleTypes;
            }
        }

        public IEnumerable<MethodType> DefinedMethodTypes
        {
            get
            {
                return this.definedMethodTypes;
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        protected internal override string ValidationName
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
            if (superdomain.Equals(this) || superdomain.Superdomains.Contains(this))
            {
                throw new Exception("Cyclic in domain inheritance");
            }

            this.directSuperdomains.Add(superdomain);
            this.MetaPopulation.Stale();
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
            var that = obj as Domain;
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

        internal void Lock()
        {
            this.directSuperdomains = this.directSuperdomains.ToArray();
            this.definedUnits = this.definedUnits.ToArray();
            this.definedInterfaces = this.definedInterfaces.ToArray();
            this.definedClasses = this.definedClasses.ToArray();
            this.definedInheritances = this.definedInheritances.ToArray();
            this.definedRelationTypes = this.definedRelationTypes.ToArray();
            this.definedAssociationTypes = this.definedAssociationTypes.ToArray();
            this.definedRoleTypes = this.definedRoleTypes.ToArray();
            this.definedMethodTypes = this.definedMethodTypes.ToArray();
        }

        internal void OnUnitCreated(Unit unit)
        {
            this.definedUnits.Add(unit);
            this.MetaPopulation.OnUnitCreated(unit);
        }

        internal void OnInterfaceCreated(Interface @interface)
        {
            this.definedInterfaces.Add(@interface);
            this.MetaPopulation.OnInterfaceCreated(@interface);
        }

        internal void OnClassCreated(Class @class)
        {
            this.definedClasses.Add(@class);
            this.MetaPopulation.OnClassCreated(@class);
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.definedInheritances.Add(inheritance);
            this.MetaPopulation.OnInheritanceCreated(inheritance);
        }

        internal void OnRelationTypeCreated(RelationType relationType)
        {
            this.definedRelationTypes.Add(relationType);
            this.MetaPopulation.OnRelationTypeCreated(relationType);
        }

        internal void OnAssociationTypeCreated(AssociationType associationType)
        {
            this.definedAssociationTypes.Add(associationType);
            this.MetaPopulation.OnAssociationTypeCreated(associationType);
        }

        internal void OnRoleTypeCreated(RoleType roleType)
        {
            this.definedRoleTypes.Add(roleType);
            this.MetaPopulation.OnRoleTypeCreated(roleType);
        }

        internal void OnMethodTypeCreated(MethodType methodType)
        {
            this.definedMethodTypes.Add(methodType);
            this.MetaPopulation.OnMethodTypeCreated(methodType);
        }

        internal void DeriveSuperdomains(HashSet<Domain> sharedDomains)
        {
            sharedDomains.Clear();
            foreach (var directSuperdomain in this.DirectSuperdomains)
            {
                directSuperdomain.DeriveSuperdomains(this, sharedDomains);
            }

            this.derivedSuperdomains = sharedDomains.ToArray();
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
                validationLog.AddError(this.ValidationName + " has no id", this, ValidationKind.Required, "IMetaObject.Id");
            }
        }

        private void DeriveSuperdomains(Domain subdomain, HashSet<Domain> superdomains)
        {
            if (this.Equals(subdomain))
            {
                // We have a cycle
                return;
            }

            superdomains.Add(this);

            foreach (var directSuperdomain in this.DirectSuperdomains)
            {
                if (!superdomains.Contains(directSuperdomain))
                {
                    directSuperdomain.DeriveSuperdomains(subdomain, superdomains);
                }
            }
        }
    }
}