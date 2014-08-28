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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A Domain is a container for <see cref="ObjectType"/>s, <see cref="RelationType"/>s.
    /// </summary>
    public sealed partial class Domain : MetaObject, IComparable
    {
        /// <summary>
        /// The default plural form.
        /// </summary>
        private const string DefaultPluralForm = "s";

        private readonly Dictionary<Guid, MetaObject> metaObjectById;

        private IList<Composite> derivedCompositeTypes;
        
        public Domain(Guid id)
        {
            this.Id = id;
            
            this.UnitTypes = new List<Unit>();
            this.Interfaces = new List<Interface>();
            this.Classes = new List<Class>();
            this.Inheritances = new List<Inheritance>();
            this.RelationTypes = new List<RelationType>();
            this.MethodTypes = new List<MethodType>();

            this.metaObjectById = new Dictionary<Guid, MetaObject>();

            this.AddAllorsUnitPopulation();
        }

        public string Name { get; set; }

        public IList<Unit> UnitTypes { get; private set; }

        public IList<Interface> Interfaces { get; private set; }

        public IList<Class> Classes { get; private set; }

        public IList<Inheritance> Inheritances { get; private set; }

        public IList<RelationType> RelationTypes { get; private set; }

        public IList<MethodType> MethodTypes { get; private set; }
        
        /// <summary>
        /// Gets the composite types.
        /// </summary>
        /// <value>The composite types.</value>
        public IList<Composite> CompositeTypes
        {
            get
            {
                return this.derivedCompositeTypes;
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
            var compositeTypes = new List<Composite>(this.Interfaces);
            compositeTypes.AddRange(this.Classes);
            this.derivedCompositeTypes = compositeTypes;

            var sharedCompositeTypes = new HashSet<Composite>();
            var sharedInterfaces = new HashSet<Interface>();
            var sharedClasses = new HashSet<Class>();

            var sharedRoleTypes = new HashSet<RoleType>();
            var sharedAssociationTypes = new HashSet<AssociationType>();

            // DirectSupertypes
            foreach (var type in this.derivedCompositeTypes)
            {
                type.DeriveDirectSupertypes(sharedInterfaces);
            }

            // DirectSubtypes
            foreach (var type in this.Interfaces)
            {
                type.DeriveDirectSubtypes(sharedCompositeTypes);
            }

            // Supertypes
            foreach (var type in this.derivedCompositeTypes)
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

            // RootClasses
            foreach (var type in this.Interfaces)
            {
                type.DeriveRootClasses();
            }

            // Exclusive Root Class
            foreach (var type in this.Interfaces)
            {
                type.DeriveExclusiveRootClass();
            }

            // RoleTypes
            foreach (var type in this.derivedCompositeTypes)
            {
                type.DeriveRoleTypes(sharedRoleTypes);
            }

            // AssociationTypes
            foreach (var type in this.derivedCompositeTypes)
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

            foreach (var type in this.CompositeTypes)
            {
                type.DeriveRoleTypeIdsCache();
            }

            foreach (var type in this.CompositeTypes)
            {
                type.DeriveAssociationIdsCache();
            }
            
            var sharedMethodTypeList = new HashSet<MethodType>();

            // MethodTypes
            foreach (var type in this.derivedCompositeTypes)
            {
                type.DeriveMethodTypes(sharedMethodTypeList);
            }
        }

        internal void OnUnitTypeCreated(Unit unit)
        {
            this.UnitTypes.Add(unit);
            this.metaObjectById.Add(unit.Id, unit);
        }

        internal void OnInterfaceCreated(Interface @interface)
        {
            this.Interfaces.Add(@interface);
            this.metaObjectById.Add(@interface.Id, @interface);
        }
        
        internal void OnClassCreated(Class @class)
        {
            this.Classes.Add(@class);
            this.metaObjectById.Add(@class.Id, @class);
        }

        internal void OnInheritanceCreated(Inheritance inheritance)
        {
            this.Inheritances.Add(inheritance);
            this.metaObjectById.Add(inheritance.Id, inheritance);
        }

        internal void OnRelationTypeCreated(RelationType relationType)
        {
            this.RelationTypes.Add(relationType);
            this.metaObjectById.Add(relationType.Id, relationType);
        }

        internal void OnMethodTypeCreated(MethodType methodType)
        {
            this.MethodTypes.Add(methodType);
            this.metaObjectById.Add(methodType.Id, methodType);
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
            {
                var objectType = new Unit(this, UnitIds.StringId);
                objectType.SingularName = UnitTags.AllorsString.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsString;
            }

            {
                var objectType = new Unit(this, UnitIds.IntegerId);
                objectType.SingularName = UnitTags.AllorsInteger.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsInteger;
            }

            {
                var objectType = new Unit(this, UnitIds.LongId);
                objectType.SingularName = UnitTags.AllorsLong.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsLong;
            }

            {
                var objectType = new Unit(this, UnitIds.DecimalId);
                objectType.SingularName = UnitTags.AllorsDecimal.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDecimal;
            }

            {
                var objectType = new Unit(this, UnitIds.DoubleId);
                objectType.SingularName = UnitTags.AllorsDouble.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDouble;
            }

            {
                var objectType = new Unit(this, UnitIds.BooleanId);
                objectType.SingularName = UnitTags.AllorsBoolean.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBoolean;
            }

            {
                var objectType = new Unit(this, UnitIds.DatetimeId);
                objectType.SingularName = UnitTags.AllorsDateTime.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsDateTime;
            }

            {
                var objectType = new Unit(this, UnitIds.Unique);
                objectType.SingularName = UnitTags.AllorsUnique.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsUnique;
            }

            {
                var objectType = new Unit(this, UnitIds.BinaryId);
                objectType.SingularName = UnitTags.AllorsBinary.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)UnitTags.AllorsBinary;
            }
        }
    }
}