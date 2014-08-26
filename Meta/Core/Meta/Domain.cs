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
    using System.Linq;

    using Microsoft.SqlServer.Server;

    /// <summary>
    /// A Domain is a container for <see cref="ObjectType"/>s, <see cref="RelationType"/>s.
    /// </summary>
    public sealed partial class Domain : MetaObject, IComparable
    {
        public string Name;

        public List<ObjectType> ObjectTypes = new List<ObjectType>();

        public List<RelationType> RelationTypes = new List<RelationType>();
        
        public List<MethodType> MethodTypes = new List<MethodType>();
        
        public List<Inheritance> Inheritances = new List<Inheritance>();
        
        public List<ObjectType> DerivedUnitObjectTypes = new List<ObjectType>();
        
        public List<ObjectType> DerivedCompositeObjectTypes = new List<ObjectType>();
        
        private Dictionary<Guid, MetaObject> MetaObjectById = new Dictionary<Guid, MetaObject>();

        /// <summary>
        /// The default plural form.
        /// </summary>
        private const string DefaultPluralForm = "s";

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
        public IList<ObjectType> CompositeObjectTypes
        {
            get
            {
                return this.DerivedCompositeObjectTypes;
            }
        }

        /// <summary>
        /// Gets the concrete composite types.
        /// </summary>
        /// <value>The concrete composite types.</value>
        public IList<ObjectType> ConcreteCompositeObjectTypes
        {
            get
            {
                var concreteCompositeTypeList = new List<ObjectType>(this.CompositeObjectTypes.Count);
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
        /// Gets the unit types.
        /// </summary>
        /// <value>The unit types.</value>
        public IList<ObjectType> UnitObjectTypes
        {
            get
            {
                return this.DerivedUnitObjectTypes;
            }
        }
        
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

        public Domain() : this(Guid.NewGuid())
        {
        }

        public Domain(Guid id)
        {
            this.Id = id;
            this.AddAllorsUnitPopulation();
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
        /// Adds the <see cref="MethodType"/> to this domain.
        /// </summary>
        /// <param name="methodTypeId">The method type id.</param>
        /// <returns>The method type.</returns>
        public MethodType AddMethodType(Guid methodTypeId)
        {
            var methodType = new MethodType(this, methodTypeId);
            this.MethodTypes.Add(methodType);
            this.MetaObjectById[methodTypeId] = methodType;
            return methodType;
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

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>The Validate.</returns>
        public ValidationLog Validate()
        {
            var validationReport = new ValidationLog();

            this.Validate(validationReport);

            foreach (var objectType in this.ObjectTypes)
            {
                objectType.Validate(validationReport);
            }

            foreach (var methodType in this.MethodTypes)
            {
                methodType.Validate(validationReport);
            }

            foreach (var relationType in this.RelationTypes)
            {
                relationType.Validate(validationReport);
            }

            foreach (var inheritance in this.Inheritances)
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

            this.DerivedUnitObjectTypes = unitTypes;
            this.DerivedCompositeObjectTypes = compositeTypes;

            var sharedList = new HashSet<ObjectType>();

            // DirectSupertypes
            foreach (var type in this.DerivedCompositeObjectTypes)
            {
                type.DeriveDirectSupertypes(sharedList);
            }

            // DirectSubtypes
            foreach (var type in this.DerivedCompositeObjectTypes)
            {
                type.DeriveDirectSubtypes(sharedList);
            }

            // Supertypes
            foreach (var type in this.DerivedCompositeObjectTypes)
            {
                type.DeriveSupertypes(sharedList);
            }

            // Subtypes
            foreach (var type in this.DerivedCompositeObjectTypes)
            {
                type.DeriveSubtypes(sharedList);
            }

            // DirectSuperinterfaces
            foreach (var type in this.DerivedCompositeObjectTypes)
            {
                type.DeriveDirectSuperinterface(sharedList);
            }

            // Superclasses
            foreach (var type in this.DerivedCompositeObjectTypes)
            {
                type.DeriveSuperclasses(sharedList);
            }

            // Subclasses
            foreach (var type in DerivedCompositeObjectTypes)
            {
                type.DeriveSubclasses(sharedList);
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
            foreach (var relationType in this.RelationTypes)
            {
                relationType.AssociationType.DeriveMultiplicity();
                relationType.RoleType.DeriveMultiplicityScaleAndSize();
            }

            // RoleType Root ObjectType
            foreach (var relationType in this.RelationTypes)
            {
                relationType.RoleType.DeriveRootClasses();
            }

            // RoleType Hierarchy Singular Name
            foreach (var relationType in this.RelationTypes)
            {
                relationType.RoleType.DeriveHierarchySingularName(sharedObjectTypeList);
            }

            // RoleType Hierarchy Plural Name
            foreach (var relationType in this.RelationTypes)
            {
                relationType.RoleType.DeriveHierarchyPluralName(sharedObjectTypeList);
            }

            // RoleType Root Name
            foreach (var relationType in this.RelationTypes)
            {
                relationType.RoleType.DeriveRootName();
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

        internal void OnObjectTypeCreated(ObjectType objectType)
        {
            this.ObjectTypes.Add(objectType);
            this.MetaObjectById[objectType.Id] = objectType;
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.Inheritances.Add(inheritance);
            this.MetaObjectById[inheritance.Id] = inheritance;
        }

        internal void OnRelationTypeCreate(RelationType relationType)
        {
            this.RelationTypes.Add(relationType);
            this.MetaObjectById[relationType.Id] = relationType;
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
                if (this.Name.Length == 0)
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
            }

            if (this.Id == Guid.Empty)
            {
                validationLog.AddError(this.ValidationName + " has no id", this, ValidationKind.Required, "MetaObject.Id");
            }
        }
        
        /// <summary>
        /// Adds the default population.
        /// </summary>
        private void AddAllorsUnitPopulation()
        {
            this.Name = AllorsUnitDomainName;

            var allorsUnitTypes = new ArrayList();

            {
                var objectType = new UnitType(this, UnitIds.StringId);
                objectType.SingularName = UnitTags.AllorsString.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsString;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.IntegerId);
                objectType.SingularName = UnitTags.AllorsInteger.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsInteger;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.LongId);
                objectType.SingularName = UnitTags.AllorsLong.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsLong;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.DecimalId);
                objectType.SingularName = UnitTags.AllorsDecimal.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDecimal;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.DoubleId);
                objectType.SingularName = UnitTags.AllorsDouble.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDouble;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.BooleanId);
                objectType.SingularName = UnitTags.AllorsBoolean.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBoolean;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.DatetimeId);
                objectType.SingularName = UnitTags.AllorsDateTime.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDateTime;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.Unique);
                objectType.SingularName = UnitTags.AllorsUnique.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsUnique;
                allorsUnitTypes.Add(objectType);
            }

            {
                var objectType = new UnitType(this, UnitIds.BinaryId);
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