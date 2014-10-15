//------------------------------------------------------------------------------------------------- 
// <copyright file="MetaPopulation.cs" company="Allors bvba">
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

    public sealed partial class MetaPopulation : IMetaPopulation
    {
            private readonly Dictionary<Guid, IMetaObject> metaObjectById;

        private IComposite[] derivedComposites;

        private bool isLocked;

        private bool isStale;
        private bool isDeriving;

        private IList<IDomain> domains;
        private IList<IUnit> units;
        private IList<IInterface> interfaces;
        private IList<IClass> classes;
        private IList<IInheritance> inheritances;
        private IList<IRelationType> relationTypes;
        private IList<IAssociationType> associationTypes;
        private IList<IRoleType> roleTypes;
        private IList<IMethodType> methodTypes;
        
        public MetaPopulation()
        {
            this.isStale = true;
            this.isDeriving = false;

            this.domains = new List<IDomain>();
            this.units = new List<IUnit>();
            this.interfaces = new List<IInterface>();
            this.classes = new List<IClass>();
            this.inheritances = new List<IInheritance>();
            this.relationTypes = new List<IRelationType>();
            this.associationTypes = new List<IAssociationType>();
            this.roleTypes = new List<IRoleType>();
            this.methodTypes = new List<IMethodType>();

            this.metaObjectById = new Dictionary<Guid, IMetaObject>();
        }

        public bool IsLocked
        {
            get
            {
                return this.isLocked;
            }
        }

        public IEnumerable<IDomain> Domains
        {
            get
            {
                return this.domains;
            }
        }

        public IEnumerable<IUnit> Units
        {
            get
            {
                return this.units;
            }
        }

        public IEnumerable<IInterface> Interfaces
        {
            get
            {
                return this.interfaces;
            }
        }

        public IEnumerable<IClass> Classes
        {
            get
            {
                return this.classes;
            }
        }

        public IEnumerable<IInheritance> Inheritances
        {
            get
            {
                return this.inheritances;
            }
        }

        public IEnumerable<IRelationType> RelationTypes
        {
            get
            {
                return this.relationTypes;
            }
        }

        public IEnumerable<IAssociationType> AssociationTypes
        {
            get
            {
                return this.associationTypes;
            }
        }

        public IEnumerable<IRoleType> RoleTypes
        {
            get
            {
                return this.roleTypes;
            }
        }

        public IEnumerable<IMethodType> MethodTypes
        {
            get
            {
                return this.methodTypes;
            }
        }

        public IEnumerable<IComposite> Composites
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
                if (validation.ContainsErrors)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Find a meta object by meta object id.
        /// </summary>
        /// <param name="id">
        /// The meta object id.
        /// </param>
        /// <returns>
        /// The <see cref="IMetaObject"/>.
        /// </returns>
        public IMetaObject Find(Guid id)
        {
            IMetaObject @object;
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


            var inheritancesBySubtype = new Dictionary<IComposite, List<IInheritance>>();
            foreach (var inheritance in this.Inheritances)
            {
                var subtype = inheritance.Subtype;
                if (subtype != null)
                {
                    List<IInheritance> inheritanceList;
                    if (!inheritancesBySubtype.TryGetValue(subtype, out inheritanceList))
                    {
                        inheritanceList = new List<IInheritance>();
                        inheritancesBySubtype[subtype] = inheritanceList;
                    }

                    inheritanceList.Add(inheritance);
                }
            }

            var supertypes = new HashSet<IInterface>();
            foreach (var subtype in inheritancesBySubtype.Keys)
            {
                supertypes.Clear();
                if (this.HasCycle(subtype, supertypes, inheritancesBySubtype))
                {
                    var message = subtype.ValidationName + " has a cycle in its inheritance hierarchy";
                    log.AddError(message, subtype, ValidationKind.Cyclic, "IComposite.Supertypes");
                }
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

        public void AssertUnlocked()
        {
            if (this.IsLocked)
            {
                throw new Exception("Environment is locked");
            }
        }

        public void Derive()
        {
            if (this.isStale && !this.isDeriving)
            {
                try
                {
                    this.isDeriving = true;

                    var sharedDomains = new HashSet<IDomain>();
                    var sharedCompositeTypes = new HashSet<IComposite>();
                    var sharedInterfaces = new HashSet<IInterface>();
                    var sharedClasses = new HashSet<IClass>();
                    var sharedAssociationTypes = new HashSet<IAssociationType>();
                    var sharedRoleTypes = new HashSet<IRoleType>();

                    // Domains
                    foreach (var domain in this.domains)
                    {
                        domain.DeriveSuperdomains(sharedDomains);
                    }

                    // Unit & IComposite ObjectTypes
                    var compositeTypes = new List<IComposite>(this.Interfaces);
                    compositeTypes.AddRange(this.Classes);
                    this.derivedComposites = compositeTypes.ToArray();

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

                    var sharedMethodTypeList = new HashSet<IMethodType>();

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

        public void OnDomainCreated(IDomain domain)
        {
            this.domains.Add(domain);
            this.metaObjectById.Add(domain.Id, domain);

            this.Stale();
        }

        public void OnUnitCreated(IUnit unit)
        {
            this.units.Add(unit);
            this.metaObjectById.Add(unit.Id, unit);

            this.Stale();
        }

        public void OnInterfaceCreated(IInterface @interface)
        {
            this.interfaces.Add(@interface);
            this.metaObjectById.Add(@interface.Id, @interface);

            this.Stale();
        }

        public void OnClassCreated(IClass @class)
        {
            this.classes.Add(@class);
            this.metaObjectById.Add(@class.Id, @class);
            
            this.Stale();
        }

        public void OnInheritanceCreated(IInheritance inheritance)
        {
            this.inheritances.Add(inheritance);
            this.metaObjectById.Add(inheritance.Id, inheritance);

            this.Stale();
        }

        public void OnRelationTypeCreated(IRelationType relationType)
        {
            this.relationTypes.Add(relationType);
            this.metaObjectById.Add(relationType.Id, relationType);

            this.Stale();
        }

        public void OnAssociationTypeCreated(IAssociationType associationType)
        {
            this.associationTypes.Add(associationType);
            this.metaObjectById.Add(associationType.Id, associationType);

            this.Stale();
        }

        public void OnRoleTypeCreated(IRoleType roleType)
        {
            this.roleTypes.Add(roleType);
            this.metaObjectById.Add(roleType.Id, roleType);

            this.Stale();
        }

        public void OnMethodTypeCreated(IMethodType methodType)
        {
            this.methodTypes.Add(methodType);
            this.metaObjectById.Add(methodType.Id, methodType);

            this.Stale();
        }

        public void Stale()
        {
            this.isStale = true;
        }

        private bool HasCycle(IComposite subtype, HashSet<IInterface> supertypes, Dictionary<IComposite, List<IInheritance>> inheritancesBySubtype)
        {
            foreach (var inheritance in inheritancesBySubtype[subtype])
            {
                var supertype = inheritance.Supertype;
                if (supertype != null)
                {
                    if (this.HasCycle(subtype, supertype, supertypes, inheritancesBySubtype))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasCycle(IComposite originalSubtype, IInterface currentSupertype, HashSet<IInterface> supertypes, Dictionary<IComposite, List<IInheritance>> inheritancesBySubtype)
        {
            if (supertypes.Contains(originalSubtype))
            {
                return true;
            }

            if (!supertypes.Contains(currentSupertype))
            {
                supertypes.Add(currentSupertype);

                List<IInheritance> currentSuperInheritances;
                if (inheritancesBySubtype.TryGetValue(currentSupertype, out currentSuperInheritances))
                {
                    foreach (var inheritance in currentSuperInheritances)
                    {
                        var newSupertype = inheritance.Supertype;
                        if (newSupertype != null)
                        {
                            if (this.HasCycle(originalSubtype, newSupertype, supertypes, inheritancesBySubtype))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
}
}