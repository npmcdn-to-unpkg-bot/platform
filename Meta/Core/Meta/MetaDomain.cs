//------------------------------------------------------------------------------------------------- 
// <copyright file="MetaDomain.cs" company="Allors bvba">
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
    using System.Text;
    using System.Xml;

    using Allors.Meta.Events;

    using AllorsGenerated;

    /// <summary>
    /// A Domain is a container for <see cref="MetaObject"/>s, <see cref="MetaRelation"/>s.
    /// </summary>
    public sealed partial class MetaDomain : IComparable
    {
        /// <summary>
        /// The version.
        /// </summary>
        public static readonly Version Version = new Version(1, 0);

        /// <summary>
        /// The default plural form.
        /// </summary>
        private const string DefaultPluralForm = "s";

        /// <summary>
        /// The session key for all objects by id.
        /// </summary>
        private const string MetaObjectByIdSessionKey = "BB103F95-7197-4082-8395-6D4DD2EC30AC";

        /// <summary>
        /// The session key for the <see cref="MetaDomain"/>
        /// </summary>
        private const string DomainSessionKey = "E0446FFC-014E-4DEC-B9FF-D8064C7DD3E7";

        /// <summary>
        /// The session key for the <see cref="MetaDomain"/>s.
        /// </summary>
        private const string DomainsSessionKey = "A2F0B18C-1980-46C6-AD78-D56E2CF8BD80";

        /// <summary>
        /// The name of the Allors Unit Domain.
        /// </summary>
        private const string AllorsUnitDomainName = "AllorsUnit";

        /// <summary>
        /// The id of the Allors Domain.
        /// </summary>
        private static readonly Guid AllorsUnitDomainId = new Guid("2d337e3a-5e9e-4705-b327-c14bd279d322");

        /// <summary>
        /// True if the type derivations are stale, false otherwise.
        /// </summary>
        private bool hasStaleObjectTypeDerivations;

        /// <summary>
        /// True if the inheritance derivations are stale, false otherwise.
        /// </summary>
        private bool hasStaleInheritanceDerivations;

        /// <summary>
        /// True if the relation derivations are stale, false otherwise.
        /// </summary>
        private bool hasStaleRelationDerivations;

        /// <summary>
        /// True if the method derivations are stale, false otherwise.
        /// </summary>
        private bool hasStaleMethodDerivations;

        /// <summary>
        /// Gets the composite types.
        /// </summary>
        /// <value>The composite types.</value>
        public MetaObject[] CompositeObjectTypes
        {
            get
            {
                this.EnsureObjectTypeDerivations();
                return DerivedCompositeObjectTypes;
            }
        }

        /// <summary>
        /// Gets the concrete composite types.
        /// </summary>
        /// <value>The concrete composite types.</value>
        public MetaObject[] ConcreteCompositeObjectTypes
        {
            get
            {
                var concreteCompositeTypeList = new List<MetaObject>(this.CompositeObjectTypes.Length);
                foreach (var compositeType in this.CompositeObjectTypes)
                {
                    if (compositeType.IsConcrete)
                    {
                        concreteCompositeTypeList.Add(compositeType);
                    }
                }

                return concreteCompositeTypeList.ToArray();
            }
        }

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        public MetaDomain[] Domains
        {
            get
            {
                this.EnsureDomainDerivations();
                return (MetaDomain[])session[DomainsSessionKey];
            }
        }

        /// <summary>
        /// Gets the inheritances.
        /// </summary>
        /// <value>The inheritances.</value>
        public MetaInheritance[] Inheritances
        {
            get
            {
                this.EnsureInheritanceDerivations();
                return DerivedInheritances;
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
        /// Gets a value indicating whether this domain is a domain.
        /// </summary>
        /// <value><c>true</c> if this domain is a domain; otherwise, <c>false</c>.</value>
        public bool IsSuperDomain
        {
            get
            {
                return this.ExistDomainsWhereDirectSuperDomain;
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
        /// Gets the saved population as an xml string.
        /// </summary>
        public string Xml
        {
            get
            {
                var stringBuilder = new StringBuilder();

                var xmlWriterSettings = new XmlWriterSettings
                    { 
                        Encoding = Encoding.UTF8, 
                        OmitXmlDeclaration = true, 
                        Indent = true 
                    };

                using (var xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
                {
                    this.Save(xmlWriter);
                    xmlWriter.Flush();
                }

                return stringBuilder.ToString();
            }
        }
        
        /// <summary>
        /// Gets or sets the inheritances that are Defined to this domain.
        /// </summary>
        /// <value>The Defined inheritances.</value>
        public override MetaInheritance[] DeclaredInheritances
        {
            get
            {
                return base.DeclaredInheritances;
            }

            set
            {
                base.DeclaredInheritances = value;
                this.Domain.StaleInheritanceDerivations();
            }
        }

        /// <summary>
        /// Gets or sets the object types that are Defined to this domain.
        /// </summary>
        /// <value>The Defined object types.</value>
        public override MetaObject[] DeclaredObjectTypes
        {
            get { return base.DeclaredObjectTypes; }
            set { throw new ArgumentException("Use Domain.AddObjectType() and ObjectType.Delete()"); }
        }

        /// <summary>
        /// Gets or sets relation types that are Defined to this domain.
        /// </summary>
        /// <value>The Defined relation types.</value>
        public override MetaRelation[] DeclaredRelationTypes
        {
            get
            {
                return base.DeclaredRelationTypes;
            }

            set
            {
                base.DeclaredRelationTypes = value;
                this.Domain.StaleRelationTypeDerivations();
            }
        }

        /// <summary>
        /// Gets all the importSuperDomain domains of this domain.
        /// </summary>
        /// <value>The importSuperDomain domain.</value>
        public MetaDomain[] SuperDomains
        {
            get
            {
                this.EnsureDomainDerivations();
                return this.DerivedSuperDomains;
            }
        }

        /// <summary>
        /// Gets or sets the direct importSuperDomain domains of this domain.
        /// </summary>
        /// <value>The direct importSuperDomain domains.</value>
        public override MetaDomain[] DirectSuperDomains
        {
            get
            {
                return base.DirectSuperDomains;
            }

            set
            {
                base.DirectSuperDomains = value;
                this.Domain.StaleDomainDerivations();
            }
        }

        /// <summary>
        /// Gets the object types.
        /// </summary>
        /// <value>The object types.</value>
        public MetaObject[] ObjectTypes
        {
            get
            {
                this.EnsureObjectTypeDerivations();
                return DerivedObjectTypes;
            }
        }

        /// <summary>
        /// Gets the relation types.
        /// </summary>
        /// <value>The relation types.</value>
        public MetaMethod[] MethodTypes
        {
            get
            {
                this.EnsureMethodTypeDerivations();
                return DerivedMethodTypes;
            }
        }

        /// <summary>
        /// Gets the relation types.
        /// </summary>
        /// <value>The relation types.</value>
        public MetaRelation[] RelationTypes
        {
            get
            {
                this.EnsureRelationTypeDerivations();
                return DerivedRelationTypes;
            }
        }

        /// <summary>
        /// Gets the unit types.
        /// </summary>
        /// <value>The unit types.</value>
        public MetaObject[] UnitObjectTypes
        {
            get
            {
                this.EnsureObjectTypeDerivations();
                return DerivedUnitObjectTypes;
            }
        }

        public string XmlVerbatimStringLiteral
        {
            get
            {
                var xml = this.Xml;
                xml = xml.Replace("\"", "\"\"");
                return xml;
            }
        }

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <value>The domains.</value>
        internal Dictionary<Guid, MetaBase> MetaObjectById
        {
            get
            {
                var objectById = (Dictionary<Guid, MetaBase>)session[MetaObjectByIdSessionKey];
                if (objectById == null)
                {
                    objectById = new Dictionary<Guid, MetaBase>();
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
        public static MetaDomain Create()
        {
            return Create(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a new instance with the specified id.
        /// </summary>
        /// <param name="id">The domain id.</param>
        /// <returns>The new domain.</returns>
        public static MetaDomain Create(Guid id)
        {
            var session = new AllorsEmbeddedSession();

            var domain = (MetaDomain)session.Create(AllorsEmbeddedDomain.Domain);
            CacheConcreteDomain(session, domain);
            domain.Id = id;

            var unitDomain = (MetaDomain)session.Create(AllorsEmbeddedDomain.Domain);
            unitDomain.Id = AllorsUnitDomainId;
            unitDomain.AddAllorsUnitPopulation();

            domain.UnitDomain = unitDomain;
            domain.AddDirectSuperDomain(unitDomain);

            return domain;
        }

        /// <summary>
        /// Get the domain for an Allors object.
        /// </summary>
        /// <param name="allorsObject">
        /// The Allors object.
        /// </param>
        /// <returns>
        /// The <see cref="MetaDomain"/>.
        /// </returns>
        public static MetaDomain GetDomain(AllorsEmbeddedObject allorsObject)
        {
            return GetDomain(allorsObject.AllorsSession);
        }

        /// <summary>
        /// Loads the <see cref="MetaDomain"/> from the xml reader.
        /// </summary>
        /// <param name="xmlReader">The xml reader.</param>
        /// <returns>The domain.</returns>
        public static MetaDomain Load(XmlReader xmlReader)
        {
            var session = new AllorsEmbeddedSession();

            session.Load(xmlReader);

            foreach (var o in session.Extent(AllorsEmbeddedDomain.Domain))
            {
                var domain = (MetaDomain)o;
                if (!domain.ExistDomainsWhereDirectSuperDomain)
                {
                    CacheConcreteDomain(session, domain);

                    domain.PurgeDerivations();

                    // Initialise MetaObjectById
                    foreach (var obj in domain.AllorsSession.Extent())
                    {
                        var metaObject = obj as MetaBase;
                        if (metaObject != null)
                        {
                            domain.MetaObjectById[metaObject.Id] = metaObject;
                        }
                    }

                    return domain;
                }
            }

            throw new ArgumentException("No domain found in the repository");
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
            var that = obj as MetaObject;
            if (that != null)
            {
                return string.CompareOrdinal(this.Name, that.Name);
            }

            return -1;
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public override void Delete()
        {
            throw new InvalidOperationException("A Domain can not be deleted");
        }

        /// <summary>
        /// Find a meta object by meta object id.
        /// </summary>
        /// <param name="metaObjectId">
        /// The meta object id.
        /// </param>
        /// <returns>
        /// The <see cref="MetaBase"/>.
        /// </returns>
        public MetaBase Find(Guid metaObjectId)
        {
            MetaBase metaObject;
            this.MetaObjectById.TryGetValue(metaObjectId, out metaObject);

            if (metaObject != null)
            {
                if (metaObject.IsDeleted)
                {
                    this.MetaObjectById.Remove(metaObjectId);
                    return null;
                }

                return metaObject;
            }

            return null;
        }

        /// <summary>
        /// Adds the <see cref="MetaInheritance"/> to this domain.
        /// </summary>
        /// <param name="inheritance">The inheritance.</param>
        public override void AddDeclaredInheritance(MetaInheritance inheritance)
        {
            base.AddDeclaredInheritance(inheritance);

            this.Domain.StaleInheritanceDerivations();
        }

        /// <summary>
        /// Adds the <see cref="MetaInheritance"/> to this domain.
        /// </summary>
        /// <param name="inheritanceId">The inheritance id.</param>
        /// <returns>The added inheritance.</returns>
        public MetaInheritance AddDeclaredInheritance(Guid inheritanceId)
        {
            var inheritance = (MetaInheritance)this.Domain.Find(inheritanceId);
            if (inheritance == null)
            {
                inheritance = MetaInheritance.Create(AllorsSession);
                inheritance.Id = inheritanceId;
            }

            this.AddDeclaredInheritance(inheritance);
            return inheritance;
        }

        /// <summary>
        /// Adds the <see cref="MetaObject"/> to this domain.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        public override void AddDeclaredObjectType(MetaObject objectType)
        {
            base.AddDeclaredObjectType(objectType);

            this.Domain.StaleObjectTypeDerivations();
        }

        /// <summary>
        /// Adds the <see cref="MetaObject"/> to this domain.
        /// </summary>
        /// <param name="objectTypeId">The object type id.</param>
        /// <returns>The object type.</returns>
        public MetaObject AddDeclaredObjectType(Guid objectTypeId)
        {
            var objectType = (MetaObject)this.Domain.Find(objectTypeId);
            if (objectType == null)
            {
                objectType = MetaObject.Create(AllorsSession);
                objectType.Id = objectTypeId;
            }

            this.AddDeclaredObjectType(objectType);
            return objectType;
        }

        /// <summary>
        /// Adds the <see cref="MetaRelation"/> to this domain.
        /// </summary>
        /// <param name="relationType">The relation type.</param>
        public override void AddDeclaredRelationType(MetaRelation relationType)
        {
            this.Domain.StaleRelationTypeDerivations();
            base.AddDeclaredRelationType(relationType);
        }

        /// <summary>
        /// Adds the <see cref="MetaRelation"/> to this domain.
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
        public MetaRelation AddDeclaredRelationType(Guid relationTypeId, Guid associationTypeId, Guid roleTypeId)
        {
            var relationType = (MetaRelation)this.Domain.Find(relationTypeId);
            if (relationType == null)
            {
                relationType = MetaRelation.Create(AllorsSession);
                relationType.Id = relationTypeId;
                relationType.AssociationType.Id = associationTypeId;
                relationType.RoleType.Id = roleTypeId;
            }

            this.AddDeclaredRelationType(relationType);
            return relationType;
        }

        /// <summary>
        /// Adds the <see cref="MetaMethod"/> to this domain.
        /// </summary>
        /// <param name="methodType">The method type.</param>
        public override void AddDeclaredMethodType(MetaMethod methodType)
        {
            this.Domain.StaleMethodTypeDerivations();
            base.AddDeclaredMethodType(methodType);
        }

        /// <summary>
        /// Adds the <see cref="MetaMethod"/> to this domain.
        /// </summary>
        /// <param name="methodTypeId">The method type id.</param>
        /// <returns>The method type.</returns>
        public MetaMethod AddDeclaredMethodType(Guid methodTypeId)
        {
            var methodType = (MetaMethod)this.Domain.Find(methodTypeId);
            if (methodType == null)
            {
                methodType = MetaMethod.Create(AllorsSession);
                methodType.Id = methodTypeId;
            }

            this.AddDeclaredMethodType(methodType);
            return methodType;
        }

        /// <summary>
        /// Adds a domain to this domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        public override void AddDirectSuperDomain(MetaDomain domain)
        {
            if (domain.Equals(this) || Array.IndexOf(domain.SuperDomains, this) > -1)
            {
                throw new Exception("Cyclic in domain inheritance");
            }

            base.AddDirectSuperDomain(domain);
            this.Domain.StaleDomainDerivations();
        }

        /// <summary>
        /// Adds the domain to this domain.
        /// </summary>
        /// <param name="domainId">The domain id.</param>
        /// <returns>The domain.</returns>
        public MetaDomain AddDirectSuperDomain(Guid domainId)
        {
            var domain = (MetaDomain)this.Domain.Domain.Find(domainId) ?? Create(this.AllorsSession, domainId);

            this.AddDirectSuperDomain(domain);
            return domain;
        }

        /// <summary>
        /// Removes the Defined domain from this domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        public override void RemoveDirectSuperDomain(MetaDomain domain)
        {
            base.RemoveDirectSuperDomain(domain);
            this.Domain.StaleDomainDerivations();
        }

        /// <summary>
        /// Removes all direct importSuperDomain domains from this domain.
        /// </summary>
        public override void RemoveDirectSuperDomains()
        {
            base.RemoveDirectSuperDomains();
            this.Domain.StaleDomainDerivations();
        }
        
        /// <summary>
        /// Import the domain and inherit from it.
        /// </summary>
        /// <param name="domain">The domain to import and inherit from.</param>
        /// <returns>The importSuperDomain domain.</returns>
        public MetaDomain Inherit(MetaDomain domain)
        {
            return this.Inherit(this, domain);
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
            if (type == typeof(MetaDomain))
            {
                return new AllorsEmbeddedObject[] { this };
            }

            if (type == typeof(MetaObject))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.ObjectType);
            }

            if (type == typeof(MetaRelation))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.RelationType);
            }

            if (type == typeof(MetaAssociation))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.AssociationType);
            }

            if (type == typeof(MetaRole))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.RoleType);
            }

            if (type == typeof(MetaInheritance))
            {
                return this.AllorsSession.Extent(AllorsEmbeddedDomain.Inheritance);
            }

            throw new ArgumentException("Unknown type");
        }

        /// <summary>
        /// Removes the Id.
        /// </summary>
        public override void RemoveId()
        {
            if (ExistId)
            {
                throw new ArgumentException("Id is write once");
            }

            base.RemoveId();
        }

        /// <summary>
        /// Removes the Defined <see cref="MetaInheritance"/> from this domain.
        /// </summary>
        /// <param name="inheritance">The inheritance.</param>
        public override void RemoveDeclaredInheritance(MetaInheritance inheritance)
        {
            base.RemoveDeclaredInheritance(inheritance);

            this.Domain.StaleInheritanceDerivations();
        }

        /// <summary>
        /// Removes all Defined <see cref="MetaInheritance"/>s from this domain.
        /// </summary>
        public override void RemoveDeclaredInheritances()
        {
            base.RemoveDeclaredInheritances();

            this.Domain.StaleInheritanceDerivations();
        }

        /// <summary>
        /// Removes the Defined <see cref="MetaObject"/> from this domain.
        /// </summary>
        /// <param name="objectType">The object type.</param>
        public override void RemoveDeclaredObjectType(MetaObject objectType)
        {
            throw new ArgumentException("Use ObjectType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Removes all Defined <see cref="MetaObject"/>s from this domain.
        /// </summary>
        public override void RemoveDeclaredObjectTypes()
        {
            throw new ArgumentException("Use ObjectType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Removes the Defined <see cref="MetaRelation"/> from this domain.
        /// </summary>
        /// <param name="relationType">The relation type.</param>
        public override void RemoveDeclaredRelationType(MetaRelation relationType)
        {
            throw new ArgumentException("Use RelationType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Removes all Defined <see cref="MetaRelation"/>s from this domain.
        /// </summary>
        public override void RemoveDeclaredRelationTypes()
        {
            throw new ArgumentException("Use RelationType.Delete() to delete ObjectTypes.");
        }

        /// <summary>
        /// Saves the <see cref="MetaDomain"/> to the specified writer.
        /// </summary>
        /// <param name="xmlWriter">The XML writer.</param>
        public void Save(XmlWriter xmlWriter)
        {
            this.PurgeDerivations();
            AllorsSession.Save(xmlWriter);
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

            var types = (MetaObject[])AllorsSession.Extent(AllorsEmbeddedDomain.ObjectType);
            foreach (var type in types)
            {
                type.Validate(validationReport);
            }

            var methodTypes = (MetaMethod[])AllorsSession.Extent(AllorsEmbeddedDomain.MethodType);
            foreach (var method in methodTypes)
            {
                method.Validate(validationReport);
            }

            var relationTypes = (MetaRelation[])AllorsSession.Extent(AllorsEmbeddedDomain.RelationType);
            foreach (var relation in relationTypes)
            {
                relation.Validate(validationReport);
            }

            var associations = (MetaAssociation[])AllorsSession.Extent(AllorsEmbeddedDomain.AssociationType);
            foreach (var association in associations)
            {
                association.Validate(validationReport);
            }

            var roles = (MetaRole[])AllorsSession.Extent(AllorsEmbeddedDomain.RoleType);
            foreach (var role in roles)
            {
                role.Validate(validationReport);
            }

            var inheritances = (MetaInheritance[])AllorsSession.Extent(AllorsEmbeddedDomain.Inheritance);
            foreach (var inheritance in inheritances)
            {
                inheritance.Validate(validationReport);
            }

            return validationReport;
        }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The domain.</returns>
        internal static MetaDomain GetDomain(AllorsEmbeddedSession session)
        {
            return (MetaDomain)session[DomainSessionKey];
        }

        /// <summary>
        /// Determines whether adding the specified domain will result in a cycle.
        /// </summary>
        /// <param name="superDomains">The importSuperDomain domains.</param>
        /// <returns>
        /// <c>true</c> if adding the specified domain will result in a cycle; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsCyclicInheritance(List<MetaDomain> superDomains)
        {
            if (superDomains.Contains(this))
            {
                return true;
            }

            superDomains.Add(this);

            foreach (MetaDomain directSuperDomain in this.DirectSuperDomains)
            {
                if (directSuperDomain.IsCyclicInheritance(superDomains))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Stales the inheritance derivations.
        /// </summary>
        internal void StaleInheritanceDerivations()
        {
            foreach (var domain in this.Domains)
            {
                domain.hasStaleInheritanceDerivations = true;
            }

            this.StaleObjectTypeDerivations();
        }

        /// <summary>
        /// Stales the object type derivations.
        /// </summary>
        internal void StaleObjectTypeDerivations()
        {
            foreach (var domain in this.Domains)
            {
                domain.hasStaleObjectTypeDerivations = true;
            }

            this.StaleMethodTypeDerivations();
            this.StaleRelationTypeDerivations();
        }

        /// <summary>
        /// Ensures that object type derivations are up to date.
        /// </summary>
        internal void EnsureObjectTypeDerivations()
        {
            if (this.hasStaleObjectTypeDerivations)
            {
                this.hasStaleObjectTypeDerivations = false;

                var objectTypes = new List<MetaObject>(this.DeclaredObjectTypes);
                foreach (var superDomain in this.SuperDomains)
                {
                    objectTypes.AddRange(superDomain.DeclaredObjectTypes);
                }

                this.DerivedObjectTypes = objectTypes.ToArray();

                // Unit & Composite ObjectTypes
                var compositeTypes = new List<MetaObject>();
                var unitTypes = new List<MetaObject>();
                foreach (var objectType in objectTypes)
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

                var sharedList = new HashSet<MetaObject>();

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

                // DirectSuperclass
                foreach (var type in DerivedCompositeObjectTypes)
                {
                    type.DeriveDirectSuperclass();
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
                    if (!type.IsInterface)
                    {
                        type.DeriveRootClassForClasses();
                    }
                }

                foreach (var type in DerivedCompositeObjectTypes)
                {
                    if (type.IsInterface)
                    {
                        type.DeriveRootClassForInterfaces(sharedList);
                    }
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
            }
        }

        /// <summary>
        /// Stales the relation type derivations.
        /// </summary>
        internal void StaleRelationTypeDerivations()
        {
            foreach (var domain in this.Domains)
            {
                domain.hasStaleRelationDerivations = true;
            }
        }
        
        /// <summary>
        /// Ensures that relation type derivations are up to date.
        /// </summary>
        internal void EnsureRelationTypeDerivations()
        {
            if (this.hasStaleRelationDerivations)
            {
                this.hasStaleRelationDerivations = false;

                var relationTypes = new List<MetaRelation>(this.DeclaredRelationTypes);
                foreach (var superDomain in this.SuperDomains)
                {
                    relationTypes.AddRange(superDomain.DeclaredRelationTypes);
                }

                this.DerivedRelationTypes = relationTypes.ToArray();

                var sharedRoleTypeList = new HashSet<MetaRole>();
                var sharedAssociationTypeList = new HashSet<MetaAssociation>();
                var sharedObjectTypeList = new HashSet<MetaObject>();

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
            }
        }

        /// <summary>
        /// Stales the method type derivations.
        /// </summary>
        internal void StaleMethodTypeDerivations()
        {
            foreach (var domain in this.Domains)
            {
                domain.hasStaleMethodDerivations = true;
            }
        }

        /// <summary>
        /// Ensures that relation type derivations are up to date.
        /// </summary>
        internal void EnsureMethodTypeDerivations()
        {
            if (this.hasStaleMethodDerivations)
            {
                this.hasStaleMethodDerivations = false;

                var methodTypes = new List<MetaMethod>(this.DeclaredMethodTypes);
                foreach (var superDomain in this.SuperDomains)
                {
                    methodTypes.AddRange(superDomain.DeclaredMethodTypes);
                }

                this.DerivedMethodTypes = methodTypes.ToArray();

                var sharedMethodTypeList = new HashSet<MetaMethod>();

                // MethodTypes
                foreach (var type in this.ObjectTypes)
                {
                    type.DeriveMethodTypes(sharedMethodTypeList);
                }
            }
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
        /// Creates the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="domainId">The domain id.</param>
        /// <returns>The new domain.</returns>
        private static MetaDomain Create(AllorsEmbeddedSession session, Guid domainId)
        {
            var domain = (MetaDomain)session.Create(AllorsEmbeddedDomain.Domain);
            domain.Id = domainId;

            var unitDomain = GetDomain(session).UnitDomain;
            domain.UnitDomain = unitDomain;
            domain.AddDirectSuperDomain(unitDomain);

            return domain;
        }

        /// <summary>
        /// Caches the concrete domain.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="concreteDomain">The concrete domain.</param>
        private static void CacheConcreteDomain(AllorsEmbeddedSession session, MetaDomain concreteDomain)
        {
            session[DomainSessionKey] = concreteDomain;
        }

        /// <summary>
        /// Purges the derivations.
        /// </summary>
        private void PurgeDerivations()
        {
            this.StaleDomainDerivations();
            this.StaleRelationTypeDerivations();
            this.StaleObjectTypeDerivations();
            this.StaleInheritanceDerivations();

            var objectTypes = (MetaObject[])AllorsSession.Extent(AllorsEmbeddedDomain.ObjectType);
            foreach (var objectType in objectTypes)
            {
                objectType.PurgeDerivations();
            }

            var relationTypes = (MetaRelation[])AllorsSession.Extent(AllorsEmbeddedDomain.RelationType);
            foreach (var relationType in relationTypes)
            {
                relationType.PurgeDerivations();
            }

            this.RemoveDerivedCompositeObjectTypes();
            this.RemoveDerivedInheritances();
            this.RemoveDerivedMethodTypes();
            this.RemoveDerivedObjectTypes();
            this.RemoveDerivedRelationTypes();
            this.RemoveDerivedUnitObjectTypes();
        }

        /// <summary>
        /// Ensures that inheritance derivations are up to date.
        /// </summary>
        private void EnsureInheritanceDerivations()
        {
            if (this.hasStaleInheritanceDerivations)
            {
                this.hasStaleInheritanceDerivations = false;

                var inheritances = new List<MetaInheritance>(this.DeclaredInheritances);
                foreach (var superDomain in this.SuperDomains)
                {
                    inheritances.AddRange(superDomain.DeclaredInheritances);
                }

                this.DerivedInheritances = inheritances.ToArray();
            }
        }

        /// <summary>
        /// Stales the domain derivations.
        /// </summary>
        private void StaleDomainDerivations()
        {
            session[DomainsSessionKey] = null;

            this.StaleInheritanceDerivations();
            this.StaleRelationTypeDerivations();
            this.StaleObjectTypeDerivations();
            this.StaleMethodTypeDerivations();
        }

        /// <summary>
        /// Ensure the domain derivations.
        /// </summary>
        private void EnsureDomainDerivations()
        {
            var domains = (MetaDomain[])session[DomainsSessionKey];
            if (domains == null)
            {
                domains = (MetaDomain[])AllorsSession.Extent(AllorsEmbeddedDomain.Domain);
                session[DomainsSessionKey] = domains;

                foreach (var domain in domains)
                {
                    domain.EnsureDerivedSuperDomains();
                }
            }
        }

        /// <summary>
        /// Ensure the derived importSuperDomain domains.
        /// </summary>
        private void EnsureDerivedSuperDomains()
        {
            var derivedSuperDomains = new List<MetaDomain>();
            this.AddDerivedSuperDomain(derivedSuperDomains);
            this.DerivedSuperDomains = derivedSuperDomains.ToArray();
        }

        /// <summary>
        /// Add a derived importSuperDomain domain.
        /// </summary>
        /// <param name="temporaryList">
        /// A temporary list of domains.
        /// </param>
        private void AddDerivedSuperDomain(List<MetaDomain> temporaryList)
        {
            temporaryList.AddRange(this.DirectSuperDomains);
            foreach (var directSuperDomain in this.DirectSuperDomains)
            {
                directSuperDomain.AddDerivedSuperDomain(temporaryList);
            }
        }
        
        /// <summary>
        /// Adds the default population.
        /// </summary>
        private void AddAllorsUnitPopulation()
        {
            Name = AllorsUnitDomainName;

            var allorsUnitTypes = new ArrayList();

            if (this.Domain.Find(MetaUnitIds.StringId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.StringId);
                objectType.SingularName = MetaUnitTags.AllorsString.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsString;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.IntegerId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.IntegerId);
                objectType.SingularName = MetaUnitTags.AllorsInteger.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsInteger;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.LongId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.LongId);
                objectType.SingularName = MetaUnitTags.AllorsLong.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsLong;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.DecimalId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.DecimalId);
                objectType.SingularName = MetaUnitTags.AllorsDecimal.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsDecimal;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.DoubleId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.DoubleId);
                objectType.SingularName = MetaUnitTags.AllorsDouble.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsDouble;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.BooleanId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.BooleanId);
                objectType.SingularName = MetaUnitTags.AllorsBoolean.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsBoolean;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.DatetimeId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.DatetimeId);
                objectType.SingularName = MetaUnitTags.AllorsDateTime.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsDateTime;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.Unique) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.Unique);
                objectType.SingularName = MetaUnitTags.AllorsUnique.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsUnique;
                allorsUnitTypes.Add(objectType);
            }

            if (this.Domain.Find(MetaUnitIds.BinaryId) == null)
            {
                var objectType = this.AddDeclaredObjectType(MetaUnitIds.BinaryId);
                objectType.SingularName = MetaUnitTags.AllorsBinary.ToString();
                objectType.PluralName = objectType.SingularName + DefaultPluralForm;
                objectType.UnitTag = (int)MetaUnitTags.AllorsBinary;
                allorsUnitTypes.Add(objectType);
            }

            foreach (MetaObject unitType in allorsUnitTypes)
            {
                unitType.IsUnit = true;
            }
        }

        /// <summary>
        /// Import the source super domain into the domain and
        /// then create a inheritance relationship in the domain.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="sourceSuperDomain">
        /// The source super domain.
        /// </param>
        /// <returns>
        /// The super domain.
        /// </returns>
        private MetaDomain Inherit(MetaDomain domain, MetaDomain sourceSuperDomain)
        {
            var superDomain = (MetaDomain)domain.Domain.Find(sourceSuperDomain.Id);
            if (superDomain == null)
            {
                superDomain = Create(AllorsSession, sourceSuperDomain.Id);
                superDomain.Name = sourceSuperDomain.Name;

                this.AddDirectSuperDomain(superDomain);

                foreach (var domainSuperDomain in sourceSuperDomain.DirectSuperDomains)
                {
                    superDomain.Inherit(domain, domainSuperDomain);
                }

                foreach (var domainObjectType in sourceSuperDomain.DeclaredObjectTypes)
                {
                    var integratedObjectType = (MetaObject)domain.Domain.Find(domainObjectType.Id) ?? MetaObject.Create(AllorsSession);
                    integratedObjectType.Copy(domainObjectType);
                    superDomain.AddDeclaredObjectType(integratedObjectType);
                }

                foreach (var domainInheritance in sourceSuperDomain.DeclaredInheritances)
                {
                    var integrateInheritance = (MetaInheritance)domain.Domain.Find(domainInheritance.Id) ?? MetaInheritance.Create(AllorsSession);
                    integrateInheritance.Copy(domainInheritance);
                    superDomain.AddDeclaredInheritance(integrateInheritance);
                }

                foreach (var domainRelationType in sourceSuperDomain.DeclaredRelationTypes)
                {
                    var integratedRelationType = (MetaRelation)domain.Domain.Find(domainRelationType.Id) ?? MetaRelation.Create(AllorsSession);
                    integratedRelationType.Copy(domainRelationType);
                    superDomain.AddDeclaredRelationType(integratedRelationType);
                }

                foreach (var domainMethodType in sourceSuperDomain.DeclaredMethodTypes)
                {
                    var integratedMethodType = MetaMethod.Create(AllorsSession);
                    integratedMethodType.Copy(domainMethodType);
                    superDomain.AddDeclaredMethodType(integratedMethodType);
                }
            }
            else
            {
                this.AddDirectSuperDomain(superDomain);
            }

            return superDomain;
        }
    }
}