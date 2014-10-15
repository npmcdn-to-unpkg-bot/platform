//------------------------------------------------------------------------------------------------- 
// <copyright file="IDomain.cs" company="Allors bvba">
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

    public abstract class IDomain : IMetaObject, IComparable
    {
        private string name;

        private IList<IDomain> directSuperdomains;

        private IList<IDomain> derivedSuperdomains;

        private IList<IUnit> definedUnits;

        private IList<IInterface> definedInterfaces;

        private IList<IClass> definedClasses;

        private IList<IInheritance> definedInheritances;

        private IList<IRelationType> definedRelationTypes;

        private IList<IAssociationType> definedAssociationTypes;

        private IList<IRoleType> definedRoleTypes;

        private IList<IMethodType> definedMethodTypes;

        public IDomain(IMetaPopulation metaPopulation, Guid id)
            : base(metaPopulation, id)
        {
            this.directSuperdomains = new List<IDomain>();

            this.definedUnits = new List<IUnit>();
            this.definedInterfaces = new List<IInterface>();
            this.definedClasses = new List<IClass>();
            this.definedInheritances = new List<IInheritance>();
            this.definedRelationTypes = new List<IRelationType>();
            this.definedAssociationTypes = new List<IAssociationType>();
            this.definedRoleTypes = new List<IRoleType>();
            this.definedMethodTypes = new List<IMethodType>();

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

        public IEnumerable<IDomain> DirectSuperdomains
        {
            get
            {
                return this.directSuperdomains;
            }
        }

        public IEnumerable<IDomain> Superdomains
        {
            get
            {
                this.MetaPopulation.Derive();
                return this.derivedSuperdomains;
            }
        }

        public IEnumerable<IUnit> DefinedUnits
        {
            get
            {
                return this.definedUnits;
            }
        }

        public IEnumerable<IInterface> DefinedInterfaces
        {
            get
            {
                return this.definedInterfaces;
            }
        }

        public IEnumerable<IClass> DefinedClasses
        {
            get
            {
                return this.definedClasses;
            }
        }

        public IEnumerable<IInheritance> DefinedInheritances
        {
            get
            {
                return this.definedInheritances;
            }
        }

        public IEnumerable<IRelationType> DefinedRelationTypes
        {
            get
            {
                return this.definedRelationTypes;
            }
        }

        public IEnumerable<IAssociationType> DefinedAssociationTypes
        {
            get
            {
                return this.definedAssociationTypes;
            }
        }

        public IEnumerable<IRoleType> DefinedRoleTypes
        {
            get
            {
                return this.definedRoleTypes;
            }
        }

        public IEnumerable<IMethodType> DefinedMethodTypes
        {
            get
            {
                return this.definedMethodTypes;
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        public override string ValidationName
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

        public void AddDirectSuperdomain(IDomain superdomain)
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
            var that = obj as IObjectType;
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

        public void Lock()
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

        public void OnUnitCreated(IUnit unit)
        {
            this.definedUnits.Add(unit);
            this.MetaPopulation.OnUnitCreated(unit);
        }

        public void OnInterfaceCreated(IInterface @interface)
        {
            this.definedInterfaces.Add(@interface);
            this.MetaPopulation.OnInterfaceCreated(@interface);
        }

        public void OnClassCreated(IClass @class)
        {
            this.definedClasses.Add(@class);
            this.MetaPopulation.OnClassCreated(@class);
        }

        public void OnInheritanceCreated(IInheritance inheritance)
        {
            this.definedInheritances.Add(inheritance);
            this.MetaPopulation.OnInheritanceCreated(inheritance);
        }

        public void OnRelationTypeCreated(IRelationType relationType)
        {
            this.definedRelationTypes.Add(relationType);
            this.MetaPopulation.OnRelationTypeCreated(relationType);
        }

        public void OnAssociationTypeCreated(IAssociationType associationType)
        {
            this.definedAssociationTypes.Add(associationType);
            this.MetaPopulation.OnAssociationTypeCreated(associationType);
        }

        public void OnRoleTypeCreated(IRoleType roleType)
        {
            this.definedRoleTypes.Add(roleType);
            this.MetaPopulation.OnRoleTypeCreated(roleType);
        }

        public void OnMethodTypeCreated(IMethodType methodType)
        {
            this.definedMethodTypes.Add(methodType);
            this.MetaPopulation.OnMethodTypeCreated(methodType);
        }

        public void DeriveSuperdomains(HashSet<IDomain> sharedDomains)
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
        public override void Validate(ValidationLog validationLog)
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

        private void DeriveSuperdomains(IDomain subdomain, HashSet<IDomain> superdomains)
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