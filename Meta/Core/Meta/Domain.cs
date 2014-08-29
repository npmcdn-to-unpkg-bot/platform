//------------------------------------------------------------------------------------------------- 
// <copyright file="Whole.cs" company="Allors bvba">
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
    /// A Domain is a container for <see cref="ObjectType"/>s, <see cref="RelationType"/>s.
    /// </summary>
    public sealed partial class Domain
    {
        /// <summary>
        /// The default plural form.
        /// </summary>
        private const string DefaultPluralForm = "s";

        private readonly Dictionary<Guid, MetaObject> metaObjectById;

        private IList<Composite> derivedComposites;

        private bool isStale;

        private bool isDeriving;

        public Domain()
        {
            this.isStale = true;
            this.isDeriving = false;

            this.Subdomains = new List<Subdomain>();
            this.UnitTypes = new List<Unit>();
            this.Interfaces = new List<Interface>();
            this.Classes = new List<Class>();
            this.Inheritances = new List<Inheritance>();
            this.RelationTypes = new List<RelationType>();
            this.MethodTypes = new List<MethodType>();

            this.metaObjectById = new Dictionary<Guid, MetaObject>();

            this.AddUnits();
        }

        public IList<Subdomain> Subdomains { get; private set; }
        
        public IList<Unit> UnitTypes { get; private set; }

        public IList<Interface> Interfaces { get; private set; }

        public IList<Class> Classes { get; private set; }

        public IList<Inheritance> Inheritances { get; private set; }

        public IList<RelationType> RelationTypes { get; private set; }

        public IList<MethodType> MethodTypes { get; private set; }

        public IList<Composite> Composites
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
            get { return this.Validate().Errors.Length == 0; }
        }

        /// <summary>
        /// Find a meta object by meta object id.
        /// </summary>
        /// <param name="metaObjectId">
        /// The meta object id.
        /// </param>
        /// <returns>
        /// The <see cref="MetaObject"/>.
        /// </returns>
        public MetaObject Find(Guid metaObjectId)
        {
            MetaObject metaObject;
            this.metaObjectById.TryGetValue(metaObjectId, out metaObject);

            return metaObject;
        }
        
        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>The Validate.</returns>
        public ValidationLog Validate()
        {
            var log = new ValidationLog();

            foreach (var part in this.Subdomains)
            {
                part.Validate(log);
            }

            foreach (var unitType in this.UnitTypes)
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

            foreach (var methodType in this.MethodTypes)
            {
                methodType.Validate(log);
            }

            foreach (var relationType in this.RelationTypes)
            {
                relationType.Validate(log);
            }

            foreach (var inheritance in this.Inheritances)
            {
                inheritance.Validate(log);
            }

            return log;
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
                    this.derivedComposites = compositeTypes;

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
                        relationType.RoleType.DeriveMultiplicityScaleAndSize();
                    }

                    // RoleType Property Names
                    foreach (var relationType in this.RelationTypes)
                    {
                        relationType.RoleType.DeriveSingularPropertyName();
                        relationType.RoleType.DerivePluralPropertyName();
                    }

                    foreach (var type in this.Composites)
                    {
                        type.DeriveRoleTypeIdsCache();
                    }

                    foreach (var type in this.Composites)
                    {
                        type.DeriveAssociationIdsCache();
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

        internal void OnUnitCreated(Unit unit)
        {
            this.UnitTypes.Add(unit);
            this.metaObjectById.Add(unit.Id, unit);

            this.Stale();
        }

        internal void OnInterfaceCreated(Interface @interface)
        {
            this.Interfaces.Add(@interface);
            this.metaObjectById.Add(@interface.Id, @interface);

            this.Stale();
        }
        
        internal void OnClassCreated(Class @class)
        {
            this.Classes.Add(@class);
            this.metaObjectById.Add(@class.Id, @class);
            
            this.Stale();
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.Inheritances.Add(inheritance);
            this.metaObjectById.Add(inheritance.Id, inheritance);

            this.Stale();
        }

        internal void OnRelationTypeCreated(RelationType relationType)
        {
            this.RelationTypes.Add(relationType);
            this.metaObjectById.Add(relationType.Id, relationType);

            this.Stale();
        }

        internal void OnMethodTypeCreated(MethodType methodType)
        {
            this.MethodTypes.Add(methodType);
            this.metaObjectById.Add(methodType.Id, methodType);

            this.Stale();
        }

        internal void Stale()
        {
            this.isStale = true;
        }

        /// <summary>
        /// Adds the default population.
        /// </summary>
        private void AddUnits()
        {
            var coreId = new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12");
            var core = new Subdomain(this, coreId);
            {
                var objectType = new Unit(core, UnitIds.StringId);
                objectType.SingularName = UnitTags.AllorsString.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsString;
            }

            {
                var objectType = new Unit(core, UnitIds.IntegerId);
                objectType.SingularName = UnitTags.AllorsInteger.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsInteger;
            }

            {
                var objectType = new Unit(core, UnitIds.LongId);
                objectType.SingularName = UnitTags.AllorsLong.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsLong;
            }

            {
                var objectType = new Unit(core, UnitIds.DecimalId);
                objectType.SingularName = UnitTags.AllorsDecimal.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDecimal;
            }

            {
                var objectType = new Unit(core, UnitIds.DoubleId);
                objectType.SingularName = UnitTags.AllorsDouble.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDouble;
            }

            {
                var objectType = new Unit(core, UnitIds.BooleanId);
                objectType.SingularName = UnitTags.AllorsBoolean.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBoolean;
            }

            {
                var objectType = new Unit(core, UnitIds.DatetimeId);
                objectType.SingularName = UnitTags.AllorsDateTime.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDateTime;
            }

            {
                var objectType = new Unit(core, UnitIds.Unique);
                objectType.SingularName = UnitTags.AllorsUnique.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsUnique;
            }

            {
                var objectType = new Unit(core, UnitIds.BinaryId);
                objectType.SingularName = UnitTags.AllorsBinary.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBinary;
            }
        }
    }
}