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

        public List<UnitType> UnitTypes = new List<UnitType>();

        public List<Interface> Interfaces = new List<Interface>();
        
        public List<Class> Classes = new List<Class>(); 

        public List<RelationType> RelationTypes = new List<RelationType>();
        
        public List<MethodType> MethodTypes = new List<MethodType>();
        
        public List<Inheritance> Inheritances = new List<Inheritance>();

        public List<CompositeType> DerivedCompositeTypes = new List<CompositeType>();

        /// <summary>
        /// A lookup dictionary for meta objects.
        /// </summary>
        private readonly Dictionary<Guid, MetaObject> metaObjectById = new Dictionary<Guid, MetaObject>();

        /// <summary>
        /// The default plural form.
        /// </summary>
        private const string DefaultPluralForm = "s";

        /// <summary>
        /// Gets the composite types.
        /// </summary>
        /// <value>The composite types.</value>
        public IList<CompositeType> CompositeTypes
        {
            get
            {
                return this.DerivedCompositeTypes;
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
            this.metaObjectById.TryGetValue(metaObjectId, out metaObject);

            return metaObject;
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

            foreach (var unitType in this.UnitTypes)
            {
                unitType.Validate(validationReport);
            }

            foreach (var @interface in this.Interfaces)
            {
                @interface.Validate(validationReport);
            }

            foreach (var @class in this.Classes)
            {
                @class.Validate(validationReport);
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
            var compositeTypes = new List<CompositeType>(this.Interfaces);
            compositeTypes.AddRange(this.Classes);
            this.DerivedCompositeTypes = compositeTypes;

            var sharedObjectTypes = new HashSet<ObjectType>();
            var sharedInterfaces = new HashSet<Interface>();

            // DirectSuperinterfaces
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveDirectSuperinterface(sharedInterfaces);
            }

            // DirectSubtypes
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveDirectSubtypes(sharedObjectTypes);
            }

            // Subtypes
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveSubtypes(sharedObjectTypes);
            }

            // Superclasses
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveSuperclasses(sharedObjectTypes);
            }

            // Subclasses
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveSubclasses(sharedObjectTypes);
            }
            
            // Exclusive Superinterfaces
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveExclusiveSuperinterfaces(sharedObjectTypes);
            }

            // RootClasses
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveRootClasses();
            }

            // Exclusive Concrete Leaf Class
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveExclusiveConcreteLeafClass(sharedObjectTypes);
            }

            // Derive concrete classes
            foreach (var type in this.DerivedCompositeTypes)
            {
                type.DeriveConcreteClassesCache();
            }

            var sharedRoleTypeList = new HashSet<RoleType>();
            var sharedAssociationTypeList = new HashSet<AssociationType>();
            var sharedObjectTypeList = new HashSet<ObjectType>();

            // RoleTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveRoleTypes(sharedRoleTypeList);
            }

            // Unit RoleTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveUnitRoleTypes(sharedRoleTypeList);
            }

            // Composite RoleTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveCompositeRoleTypes(sharedRoleTypeList);
            }

            // Exclusive RoleTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveExclusiveRoleTypes(sharedRoleTypeList);
            }

            // AssociationTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveAssociationTypes(sharedAssociationTypeList);
            }

            // Exclusive AssociationTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveExclusiveAssociationTypes(sharedAssociationTypeList);
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

            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveRoleTypeIdsCache();
            }

            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveAssociationIdsCache();
            }
            
            var sharedMethodTypeList = new HashSet<MethodType>();

            // MethodTypes
            foreach (var compositeType in this.DerivedCompositeTypes)
            {
                compositeType.DeriveMethodTypes(sharedMethodTypeList);
            }
        }

        internal void OnUnitTypeCreated(UnitType unitType)
        {
            this.UnitTypes.Add(unitType);
            this.metaObjectById[unitType.Id] = unitType;
        }

        internal void OnInterfaceCreated(Interface @interface)
        {
            this.Interfaces.Add(@interface);
            this.metaObjectById[@interface.Id] = @interface;
        }

        internal void OnClassCreated(Class @class)
        {
            this.Classes.Add(@class);
            this.metaObjectById[@class.Id] = @class;
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.Inheritances.Add(inheritance);
            this.metaObjectById[inheritance.Id] = inheritance;
        }

        internal void OnRelationTypeCreated(RelationType relationType)
        {
            this.RelationTypes.Add(relationType);
            this.metaObjectById[relationType.Id] = relationType;
        }

        internal void OnMethodTypeCreated(MethodType methodType)
        {
            this.MethodTypes.Add(methodType);
            this.metaObjectById[methodType.Id] = methodType;
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