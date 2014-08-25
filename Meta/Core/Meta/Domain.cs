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
    using System.Collections;
    using System.Collections.Generic;

    using AllorsGenerated;

    /// <summary>
    /// A Domain is a container for <see cref="ObjectType"/>s, <see cref="RelationType"/>s.
    /// </summary>
    public sealed partial class Domain : IComparable
    {
        /// <summary>
        /// The default plural form.
        /// </summary>
        private const string DefaultPluralForm = "s";

        /// <summary>
        /// The session key for all objects by id.
        /// </summary>
        private const string MetaObjectByIdSessionKey = "BB103F95-7197-4082-8395-6D4DD2EC30AC";

        /// <summary>
        /// The session key for the <see cref="Domain"/>
        /// </summary>
        private const string DomainSessionKey = "E0446FFC-014E-4DEC-B9FF-D8064C7DD3E7";

        /// <summary>
        /// The name of the Allors Unit Domain.
        /// </summary>
        private const string AllorsUnitDomainName = "AllorsUnit";

        /// <summary>
        /// The id of the Allors Domain.
        /// </summary>
        private static readonly Guid AllorsUnitDomainId = new Guid("2d337e3a-5e9e-4705-b327-c14bd279d322");

        /// <summary>
        /// Gets the composite types.
        /// </summary>
        /// <value>The composite types.</value>
        public ObjectType[] CompositeObjectTypes
        {
            get
            {
                return DerivedCompositeObjectTypes;
            }
        }

        /// <summary>
        /// Gets the concrete composite types.
        /// </summary>
        /// <value>The concrete composite types.</value>
        public ObjectType[] ConcreteCompositeObjectTypes
        {
            get
            {
                var concreteCompositeTypeList = new List<ObjectType>(this.CompositeObjectTypes.Length);
                foreach (var compositeType in this.CompositeObjectTypes)
                {
                    if (!compositeType.IsInterface)
                    {
                        concreteCompositeTypeList.Add(compositeType);
                    }
                }

                return concreteCompositeTypeList.ToArray();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is the allors unit domain.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is the allors unit domain; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllorsUnitDomain
        {
            get { return AllorsUnitDomainId.Equals(this.Id); }
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
        /// Gets or sets the object types that are Defined to this domain.
        /// </summary>
        /// <value>The Defined object types.</value>
        public override ObjectType[] ObjectTypes
        {
            get { return base.ObjectTypes; }
            set { throw new ArgumentException("Use Domain.AddObjectType() and ObjectType.Delete()"); }
        }

        /// <summary>
        /// Gets the unit types.
        /// </summary>
        /// <value>The unit types.</value>
        public ObjectType[] UnitObjectTypes
        {
            get
            {
                return DerivedUnitObjectTypes;
            }
        }

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        internal Dictionary<Guid, MetaObject> MetaObjectById
        {
            get
            {
                var objectById = (Dictionary<Guid, MetaObject>)session[MetaObjectByIdSessionKey];
                if (objectById == null)
                {
                    objectById = new Dictionary<Guid, MetaObject>();
                    session[MetaObjectByIdSessionKey] = objectById;
                }

                return objectById;
            }
        }

        /// <summary>
        /// Gets the validation name.
        /// </summary>
        protected override string ValidationName
        {
            get
            {
                if (ExistName)
                {
                    return "domain " + this.Name;
                }

                return "unknown domain";
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>The new domain.</returns>
        public static Domain Create()
        {
            return Create(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a new instance with the specified id.
        /// </summary>
        /// <param name="id">The domain id.</param>
        /// <returns>The new domain.</returns>
        public static Domain Create(Guid id)
        {
            var session = new AllorsEmbeddedSession();

            var domain = (Domain)session.Create(AllorsEmbeddedDomain.Domain);
            CacheConcreteDomain(session, domain);
            domain.Id = id;

            domain.AddAllorsUnitPopulation();

            return domain;
        }

        /// <summary>
        /// Get the domain for an Allors object.
        /// </summary>
        /// <param name="allorsObject">
        /// The Allors object.
        /// </param>
        /// <returns>
        /// The <see cref="Domain"/>.
        /// </returns>
        public static Domain GetDomain(AllorsEmbeddedObject allorsObject)
        {
            return GetDomain(allorsObject.AllorsSession);
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
            this.MetaObjectById.TryGetValue(metaObjectId, out metaObject);

            return metaObject;
        }

        /// <summary>
        /// Adds the <see cref="Inheritance"/> to this domain.
        /// </summary>
        /// <param name="inheritanceId">The inheritance id.</param>
        /// <returns>The added inheritance.</returns>
        public Inheritance AddDeclaredInheritance(Guid inheritanceId)
        {
            var inheritance = (Inheritance)this.Domain.Find(inheritanceId);
            if (inheritance == null)
            {
                inheritance = Inheritance.Create(AllorsSession);
                inheritance.Id = inheritanceId;
            }

            this.AddInheritance(inheritance);
            return inheritance;
        }

        /// <summary>
        /// Adds the <see cref="ObjectType"/> to this domain.
        /// </summary>
        /// <param name="objectTypeId">The object type id.</param>
        /// <returns>The object type.</returns>
        public ObjectType AddDeclaredObjectType(Guid objectTypeId)
        {
            var objectType = (ObjectType)this.Domain.Find(objectTypeId);
            if (objectType == null)
            {
                objectType = ObjectType.Create(AllorsSession);
                objectType.Id = objectTypeId;
            }

            this.AddObjectType(objectType);
            return objectType;
        }

        /// <summary>
        /// Adds the <see cref="RelationType"/> to this domain.
        /// </summary>
        /// <param name="relationTypeId">
        /// The relation type id.
        /// </param>
        /// <param name="associationTypeId">
        /// The association Type Id.
        /// </param>
        /// <param name="roleTypeId">
        /// The role Type Id.
        /// </param>
        /// <returns>
        /// The relation type.
        /// </returns>
        public RelationType AddDeclaredRelationType(Guid relationTypeId, Guid associationTypeId, Guid roleTypeId)
        {
            var relationType = (RelationType)this.Domain.Find(relationTypeId);
            if (relationType == null)
            {
                relationType = RelationType.Create(AllorsSession);
                relationType.Id = relationTypeId;
                relationType.AssociationType.Id = associationTypeId;
                relationType.RoleType.Id = roleTypeId;
            }

            this.AddRelationType(relationType);
            return relationType;
        }

        /// <summary>
        /// Adds the <see cref="MethodType"/> to this domain.
        /// </summary>
        /// <param name="methodTypeId">The method type id.</param>
        /// <returns>The method type.</returns>
        public MethodType AddDeclaredMethodType(Guid methodTypeId)
        {
            var methodType = (MethodType)this.Domain.Find(methodTypeId);
            if (methodType == null)
            {
                methodType = MethodType.Create(AllorsSession);
                methodType.Id = methodTypeId;
            }

            this.AddMethodType(methodType);
            return methodType;
        }

        /// <summary>
        /// Create an extent for this type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The extent.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the type is not a domain type.
        /// </exception>
        public AllorsEmbeddedObject[] Extent(Type type)
        {
            if (type == typeof(Domain))
            {
                return new AllorsEmbeddedObject[] { this };
            }

            if (type == typeof(ObjectType))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.ObjectType);
            }

            if (type == typeof(RelationType))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.RelationType);
            }

            if (type == typeof(AssociationType))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.AssociationType);
            }

            if (type == typeof(RoleType))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.RoleType);
            }

            if (type == typeof(Inheritance))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.Inheritance);
            }

            throw new ArgumentException("Unknown type");
        }

        /// <summary>
        /// Removes the Defined <see cref="ObjectType"/> from this domain.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        public override void RemoveDeclaredObjectType(ObjectType objectType)
        {
            throw new ArgumentException("Use ObjectType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Removes all Defined <see cref="ObjectType"/>s from this domain.
        /// </summary>
        public override void RemoveDeclaredObjectTypes()
        {
            throw new ArgumentException("Use ObjectType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Removes the Defined <see cref="RelationType"/> from this domain.
        /// </summary>
        /// <param name="relationType">The relation type.</param>
        public override void RemoveDeclaredRelationType(RelationType relationType)
        {
            throw new ArgumentException("Use RelationType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Removes all Defined <see cref="RelationType"/>s from this domain.
        /// </summary>
        public override void RemoveDeclaredRelationTypes()
        {
            throw new ArgumentException("Use RelationType.Delete() to delete ObjectTypes.");
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (ExistName)
            {
                return Name;
            }

            if (ExistId)
            {
                return this.IdAsString;
            }

            return base.ToString();
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>The Validate.</returns>
        public ValidationLog Validate()
        {
            var validationReport = new ValidationLog();

            this.Validate(validationReport);

            var types = (ObjectType[])AllorsSession.Extent(AllorsEmbeddedDomain.ObjectType);
            foreach (var type in types)
            {
                type.Validate(validationReport);
            }

            var methodTypes = (MethodType[])AllorsSession.Extent(AllorsEmbeddedDomain.MethodType);
            foreach (var method in methodTypes)
            {
                method.Validate(validationReport);
            }

            var relationTypes = (RelationType[])AllorsSession.Extent(AllorsEmbeddedDomain.RelationType);
            foreach (var relation in relationTypes)
            {
                relation.Validate(validationReport);
            }

            var associations = (AssociationType[])AllorsSession.Extent(AllorsEmbeddedDomain.AssociationType);
            foreach (var association in associations)
            {
                association.Validate(validationReport);
            }

            var roles = (RoleType[])AllorsSession.Extent(AllorsEmbeddedDomain.RoleType);
            foreach (var role in roles)
            {
                role.Validate(validationReport);
            }

            var inheritances = (Inheritance[])AllorsSession.Extent(AllorsEmbeddedDomain.Inheritance);
            foreach (var inheritance in inheritances)
            {
                inheritance.Validate(validationReport);
            }

            return validationReport;
        }

        public void Derive()
        {
            // Unit & Composite ObjectTypes
            var compositeTypes = new List<ObjectType>();
            var unitTypes = new List<ObjectType>();
            foreach (var objectType in this.ObjectTypes)
            {
                if (objectType.IsUnit)
                {
                    unitTypes.Add(objectType);
                }
                else
                {
                    compositeTypes.Add(objectType);
                }
            }

            this.DerivedUnitObjectTypes = unitTypes.ToArray();
            this.DerivedCompositeObjectTypes = compositeTypes.ToArray();

            var sharedList = new HashSet<ObjectType>();

            // DirectSupertypes
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveDirectSupertypes(sharedList);
            }

            // Supertypes
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveSupertypes(sharedList);
            }

            // DirectSuperinterfaces
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveDirectSuperinterface(sharedList);
            }

            // Superclasses
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveSuperclasses(sharedList);
            }

            // Subclasses
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveSubclasses(sharedList);
            }

            // Superinterfaces
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveSuperinterfaces(sharedList);
            }

            // Subinterfaces
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveSubinterfaces(sharedList);
            }

            // Exclusive Superinterfaces
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveExclusiveSuperinterfaces(sharedList);
            }

            // RootClasses
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveRootClasses();
            }

            // Exclusive Concrete Leaf Class
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveExclusiveConcreteLeafClass(sharedList);
            }

            // Derive concrete classes
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveConcreteClassesCache();
            }

            var sharedRoleTypeList = new HashSet<RoleType>();
            var sharedAssociationTypeList = new HashSet<AssociationType>();
            var sharedObjectTypeList = new HashSet<ObjectType>();

            // RoleTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveRoleTypes(sharedRoleTypeList);
            }

            // Unit RoleTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveUnitRoleTypes(sharedRoleTypeList);
            }

            // Composite RoleTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveCompositeRoleTypes(sharedRoleTypeList);
            }

            // Exclusive RoleTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveExclusiveRoleTypes(sharedRoleTypeList);
            }

            // AssociationTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveAssociationTypes(sharedAssociationTypeList);
            }

            // Exclusive AssociationTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveExclusiveAssociationTypes(sharedAssociationTypeList);
            }

            // Association & RoleType
            foreach (var type in this.ObjectTypes)
            {
                foreach (var association in type.AssociationTypesWhereObjectType)
                {
                    association.DeriveMultiplicity();
                }

                foreach (var role in type.RoleTypesWhereObjectType)
                {
                    role.DeriveMultiplicityScaleAndSize();
                }
            }

            // RoleType Root ObjectType
            foreach (var type in this.ObjectTypes)
            {
                foreach (var role in type.RoleTypesWhereObjectType)
                {
                    role.DeriveRootTypes();
                }
            }

            // RoleType Hierarchy Singular Name
            foreach (var type in this.ObjectTypes)
            {
                foreach (var role in type.RoleTypesWhereObjectType)
                {
                    role.DeriveHierarchySingularName(sharedObjectTypeList);
                }
            }

            // RoleType Hierarchy Plural Name
            foreach (var type in this.ObjectTypes)
            {
                foreach (var role in type.RoleTypesWhereObjectType)
                {
                    role.DeriveHierarchyPluralName(sharedObjectTypeList);
                }
            }

            // RoleType Root Name
            foreach (var type in this.ObjectTypes)
            {
                foreach (var role in type.RoleTypesWhereObjectType)
                {
                    role.DeriveRootName();
                }
            }

            foreach (var type in this.CompositeObjectTypes)
            {
                type.DeriveRoleTypeIdsCache();
            }

            foreach (var type in this.CompositeObjectTypes)
            {
                type.DeriveAssociationIdsCache();
            }


            var sharedMethodTypeList = new HashSet<MethodType>();

            // MethodTypes
            foreach (var type in this.ObjectTypes)
            {
                type.DeriveMethodTypes(sharedMethodTypeList);
            }
        }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The domain.</returns>
        internal static Domain GetDomain(AllorsEmbeddedSession session)
        {
            return (Domain)session[DomainSessionKey];
        }

        /// <summary>
        /// Validates the domain.
        /// </summary>
        /// <param name="validationLog">The validation.</param>
        protected internal override void Validate(ValidationLog validationLog)
        {
            base.Validate(validationLog);

            if (!ExistName)
            {
                validationLog.AddError("domain has no name", this, ValidationKind.Required, AllorsEmbeddedDomain.DomainName);
            }
            else
            {
                if (Name.Length == 0)
                {
                    validationLog.AddError("domain has no name", this, ValidationKind.Required, AllorsEmbeddedDomain.DomainName);
                }
                else
                {
                    if (!char.IsLetter(Name[0]))
                    {
                        var message = this.ValidationName + " should start with an alfabetical character";
                        validationLog.AddError(message, this, ValidationKind.Format, AllorsEmbeddedDomain.DomainName);
                    }

                    for (var i = 1; i < Name.Length; i++)
                    {
                        if (!char.IsLetter(Name[i]) && !char.IsDigit(Name[i]))
                        {
                            var message = this.ValidationName + " should only contain alfanumerical characters)";
                            validationLog.AddError(message, this, ValidationKind.Format, AllorsEmbeddedDomain.DomainName);
                            break;
                        }
                    }
                }
            }

            if (!ExistId)
            {
                validationLog.AddError(this.ValidationName + " has no id", this, ValidationKind.Required, AllorsEmbeddedDomain.MetaObjectId);
            }
        }

        /// <summary>
        /// Caches the concrete domain.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="concreteDomain">The concrete domain.</param>
        private static void CacheConcreteDomain(AllorsEmbeddedSession session, Domain concreteDomain)
        {
            session[DomainSessionKey] = concreteDomain;
        }
       
        /// <summary>
        /// Adds the default population.
        /// </summary>
        private void AddAllorsUnitPopulation()
        {
            Name = AllorsUnitDomainName;

            var allorsUnitTypes = new ArrayList();

            if (this.Domain.Find(UnitIds.StringId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.StringId);
                objectType.SingularName = UnitTags.AllorsString.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsString;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.IntegerId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.IntegerId);
                objectType.SingularName = UnitTags.AllorsInteger.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsInteger;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.LongId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.LongId);
                objectType.SingularName = UnitTags.AllorsLong.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsLong;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.DecimalId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.DecimalId);
                objectType.SingularName = UnitTags.AllorsDecimal.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDecimal;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.DoubleId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.DoubleId);
                objectType.SingularName = UnitTags.AllorsDouble.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDouble;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.BooleanId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.BooleanId);
                objectType.SingularName = UnitTags.AllorsBoolean.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBoolean;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.DatetimeId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.DatetimeId);
                objectType.SingularName = UnitTags.AllorsDateTime.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDateTime;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.Unique) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.Unique);
                objectType.SingularName = UnitTags.AllorsUnique.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsUnique;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(UnitIds.BinaryId) == null)
            {
                var objectType = this.AddDeclaredObjectType(UnitIds.BinaryId);
                objectType.SingularName = UnitTags.AllorsBinary.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBinary;
                allorsUnitTypes.Add(objectType);
            }

            foreach (ObjectType unitType in allorsUnitTypes)
            {
                unitType.IsUnit = true;
            }
        }
    }
}