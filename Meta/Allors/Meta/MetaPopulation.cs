//------------------------------------------------------------------------------------------------- 
// <copyright file="Environment.cs" company="Allors bvba">
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

    /// <summary>
    /// A Domain is a container for <see cref="ObjectType"/>s, <see cref="RelationType"/>s.
    /// </summary>
    public sealed partial class MetaPopulation
    {
        private readonly Dictionary<Guid, MetaObject> metaObjectById;

        private Composite[] derivedComposites;

        private bool isLocked;

        private bool isStale;
        private bool isDeriving;

        private IList<Domain> domains;
        private IList<Unit> units;
        private IList<Interface> interfaces;
        private IList<Class> classes;
        private IList<Inheritance> inheritances;
        private IList<RelationType> relationTypes;
        private IList<AssociationType> associationTypes;
        private IList<RoleType> roleTypes;
        private IList<MethodType> methodTypes;
        
        public MetaPopulation()
        {
            this.isStale = true;
            this.isDeriving = false;

            this.domains = new List<Domain>();
            this.units = new List<Unit>();
            this.interfaces = new List<Interface>();
            this.classes = new List<Class>();
            this.inheritances = new List<Inheritance>();
            this.relationTypes = new List<RelationType>();
            this.associationTypes = new List<AssociationType>();
            this.roleTypes = new List<RoleType>();
            this.methodTypes = new List<MethodType>();

            this.metaObjectById = new Dictionary<Guid, MetaObject>();
        }

        public bool IsLocked
        {
            get
            {
                return this.isLocked;
            }
        }

        public IEnumerable<Domain> Domains
        {
            get
            {
                return this.domains;
            }
        }

        public IEnumerable<Unit> Units
        {
            get
            {
                return this.units;
            }
        }

        public IEnumerable<Interface> Interfaces
        {
            get
            {
                return this.interfaces;
            }
        }

        public IEnumerable<Class> Classes
        {
            get
            {
                return this.classes;
            }
        }

        public IEnumerable<Inheritance> Inheritances
        {
            get
            {
                return this.inheritances;
            }
        }

        public IEnumerable<RelationType> RelationTypes
        {
            get
            {
                return this.relationTypes;
            }
        }

        public IEnumerable<AssociationType> AssociationTypes
        {
            get
            {
                return this.associationTypes;
            }
        }

        public IEnumerable<RoleType> RoleTypes
        {
            get
            {
                return this.roleTypes;
            }
        }

        public IEnumerable<MethodType> MethodTypes
        {
            get
            {
                return this.methodTypes;
            }
        }

        public IEnumerable<Composite> Composites
        {
            get
            {
                this.Derive();
                return this.derivedComposites;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                var validation = this.Validate();
                return validation.Errors.Length == 0;
            }
        }

        /// <summary>
        /// Find a meta object by meta object id.
        /// </summary>
        /// <param name="id">
        /// The meta object id.
        /// </param>
        /// <returns>
        /// The <see cref="MetaObject"/>.
        /// </returns>
        public MetaObject Find(Guid id)
        {
            MetaObject @object;
            this.metaObjectById.TryGetValue(id, out @object);

            return @object;
        }
        
        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>The Validate.</returns>
        public ValidationLog Validate()
        {
            var log = new ValidationLog();

            foreach (var domain in this.Domains)
            {
                domain.Validate(log);
            }

            foreach (var unitType in this.Units)
            {
                unitType.Validate(log);
            }

            foreach (var @interface in this.Interfaces)
            {
                @interface.Validate(log);
            }

            foreach (var @class in this.Classes)
            {
                @class.Validate(log);
            }

            foreach (var inheritance in this.Inheritances)
            {
                inheritance.Validate(log);
            }

            foreach (var relationType in this.RelationTypes)
            {
                relationType.Validate(log);
            }

            foreach (var methodType in this.MethodTypes)
            {
                methodType.Validate(log);
            }

            return log;
        }

        public void Lock()
        {
            if (!this.IsLocked)
            {
                this.Derive();

                this.isLocked = true;

                this.domains = this.domains.ToArray();
                this.units = this.units.ToArray();
                this.interfaces = this.interfaces.ToArray();
                this.classes = this.classes.ToArray();
                this.inheritances = this.inheritances.ToArray();
                this.relationTypes = this.relationTypes.ToArray();
                this.associationTypes = this.associationTypes.ToArray();
                this.roleTypes = this.roleTypes.ToArray();
                this.methodTypes = this.methodTypes.ToArray();

                foreach (var domain in this.domains)
                {
                    domain.Lock();
                }
            }
        }

        internal void AssertUnlocked()
        {
            if (this.IsLocked)
            {
                throw new Exception("Environment is locked");
            }
        }

        internal void Derive()
        {
            if (this.isStale && !this.isDeriving)
            {
                try
                {
                    this.isDeriving = true;

                    // Unit & Composite ObjectTypes
                    var compositeTypes = new List<Composite>(this.Interfaces);
                    compositeTypes.AddRange(this.Classes);
                    this.derivedComposites = compositeTypes.ToArray();

                    var sharedCompositeTypes = new HashSet<Composite>();
                    var sharedInterfaces = new HashSet<Interface>();
                    var sharedClasses = new HashSet<Class>();

                    var sharedRoleTypes = new HashSet<RoleType>();
                    var sharedAssociationTypes = new HashSet<AssociationType>();

                    // DirectSupertypes
                    foreach (var type in this.derivedComposites)
                    {
                        type.DeriveDirectSupertypes(sharedInterfaces);
                    }

                    // DirectSubtypes
                    foreach (var type in this.Interfaces)
                    {
                        type.DeriveDirectSubtypes(sharedCompositeTypes);
                    }

                    // Supertypes
                    foreach (var type in this.derivedComposites)
                    {
                        type.DeriveSupertypes(sharedInterfaces);
                    }

                    // Subtypes
                    foreach (var type in this.Interfaces)
                    {
                        type.DeriveSubtypes(sharedCompositeTypes);
                    }

                    // Subclasses
                    foreach (var type in this.Interfaces)
                    {
                        type.DeriveSubclasses(sharedClasses);
                    }

                    // LeafClasses
                    foreach (var type in this.Interfaces)
                    {
                        type.DeriveLeafClasses();
                    }

                    // Exclusive Root Class
                    foreach (var type in this.Interfaces)
                    {
                        type.DeriveExclusiveLeafClass();
                    }

                    // RoleTypes
                    foreach (var type in this.derivedComposites)
                    {
                        type.DeriveRoleTypes(sharedRoleTypes);
                    }

                    // AssociationTypes
                    foreach (var type in this.derivedComposites)
                    {
                        type.DeriveAssociationTypes(sharedAssociationTypes);
                    }

                    // Association & RoleType
                    foreach (var relationType in this.RelationTypes)
                    {
                        relationType.AssociationType.DeriveMultiplicity();
                        relationType.RoleType.DeriveMultiplicity();
                        relationType.RoleType.DeriveScaleAndSize();
                    }

                    // RoleType Property Names
                    foreach (var relationType in this.RelationTypes)
                    {
                        relationType.RoleType.DeriveSingularPropertyName();
                        relationType.RoleType.DerivePluralPropertyName();
                    }

                    var sharedMethodTypeList = new HashSet<MethodType>();

                    // MethodTypes
                    foreach (var type in this.derivedComposites)
                    {
                        type.DeriveMethodTypes(sharedMethodTypeList);
                    }
                }
                finally
                {
                    // Ignore stale requests during a derivation
                    this.isStale = false;
                    this.isDeriving = false;
                }
            }
        }

        internal void OnDomainCreated(Domain domain)
        {
            this.domains.Add(domain);
            this.metaObjectById.Add(domain.Id, domain);

            this.Stale();
        }

        internal void OnUnitCreated(Unit unit)
        {
            this.units.Add(unit);
            this.metaObjectById.Add(unit.Id, unit);

            this.Stale();
        }

        internal void OnInterfaceCreated(Interface @interface)
        {
            this.interfaces.Add(@interface);
            this.metaObjectById.Add(@interface.Id, @interface);

            this.Stale();
        }
        
        internal void OnClassCreated(Class @class)
        {
            this.classes.Add(@class);
            this.metaObjectById.Add(@class.Id, @class);
            
            this.Stale();
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.inheritances.Add(inheritance);
            this.metaObjectById.Add(inheritance.Id, inheritance);

            this.Stale();
        }

        internal void OnRelationTypeCreated(RelationType relationType)
        {
            this.relationTypes.Add(relationType);
            this.metaObjectById.Add(relationType.Id, relationType);

            this.Stale();
        }

        internal void OnAssociationTypeCreated(AssociationType associationType)
        {
            this.associationTypes.Add(associationType);
            this.metaObjectById.Add(associationType.Id, associationType);

            this.Stale();
        }

        internal void OnRoleTypeCreated(RoleType roleType)
        {
            this.roleTypes.Add(roleType);
            this.metaObjectById.Add(roleType.Id, roleType);

            this.Stale();
        }

        internal void OnMethodTypeCreated(MethodType methodType)
        {
            this.methodTypes.Add(methodType);
            this.metaObjectById.Add(methodType.Id, methodType);

            this.Stale();
        }

        internal void Stale()
        {
            this.isStale = true;
        }
    }
}