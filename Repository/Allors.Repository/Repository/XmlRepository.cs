//------------------------------------------------------------------------------------------------- 
// <copyright file="XmlRepository.cs" company="Allors bvba">
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
    using System.IO;
    using System.Xml;

    using Allors.Meta.Events;
    using Allors.Meta.Tasks;
    using Allors.Meta.Templates;
    using Allors.Meta.Meta.Xml;
    using Allors.Meta.Storage;
    using Allors.Meta.Xml;

    public partial class XmlRepository : RepositoryObject
    {
        public const string FileFilter = "Repository files (" + FileName + ")|" + FileName;
        public const string FileName = "allors.repository";

        private const string DefaultOutputDirectory = "../output";
        private const string DomainFileName = "allors.domain";
        private const string TemplatesDirectory = "templates";
        private const string Wildcard = "*";
        private const string ObjectTypeExtension = ".objectType";
        private const string ObjectTypesDirectory = "objectTypes";
        private const string ObjectTypeWildcard = Wildcard + ObjectTypeExtension;
        private const string InheritanceExtension = ".inheritance";
        private const string InheritancesDirectory = "inheritances";
        private const string InheritanceWildcard = Wildcard + InheritanceExtension;
        private const string RelationTypeExtension = ".relationType";
        private const string RelationTypesDirectory = "relationTypes";
        private const string RelationTypeWildcard = Wildcard + RelationTypeExtension;

        public static Version Version = new Version("1.0");

        private readonly DirectoryInfo directoryInfo;

        private Dictionary<Guid, XmlRepository> repositoryBySuperId;
        private List<Guid> unresolvedObjectTypeIds;
        private HashSet<RelationType> relationTypesWithNewIds;

        private DirectoryInfo objectTypesDirectoryInfo;
        private DirectoryInfo inheritancesDirectoryInfo;
        private DirectoryInfo relationsDirectoryInfo;

        private FileInfo fileInfo;
        private FileInfo domainFileInfo;

        private RepositoryXml xml;
        private DomainXml domainXml;

        private MetaPopulation metaPopulation;
        private Domain domain;

        private Dictionary<Guid, InheritanceXml> inheritanceXmlsById;

        private Dictionary<Guid, ObjectTypeXml> objectTypeXmlsById;

        private Dictionary<Guid, RelationTypeXml> relationTypeXmlsById;

        private List<Template> templates;

        public void RemoveSuper(Domain superDomainToRemove)
        {
            XmlRepository repositoryToRemove;
            if (this.repositoryBySuperId.TryGetValue(superDomainToRemove.Id, out repositoryToRemove))
            {
                this.repositoryBySuperId.Remove(superDomainToRemove.Id);
                this.domain.SyncFromMeta(this.domainXml);
                this.domainXml.Save();

                this.xml.RemoveSuperDomainLocation(superDomainToRemove.Id);
                this.xml.Save();

                this.Init(false);
            }
        }

        public XmlRepository(DirectoryInfo directoryInfo, bool create = false)
        {
            this.directoryInfo = directoryInfo;
            this.Init(create);
        }

        public event EventHandler<RepositoryObjectChangedEventArgs> ObjectChanged;

        public event EventHandler<RepositoryObjectDeletedEventArgs> ObjectDeleted;

        public event EventHandler<RepositoryMetaObjectChangedEventArgs> MetaObjectChanged;

        public event EventHandler<RepositoryMetaObjectDeletedEventArgs> MetaObjectDeleted;

        public DirectoryInfo DirectoryInfo 
        {
            get
            {
                return this.DirectoryInfo;
            }
        }

        public Domain Domain
        {
            get
            {
                return this.domain;
            }
        }

        public override Guid Id
        {
            get
            {
                return this.Domain.Id;
            }
        }

        public DirectoryInfo TemplatesDirectoryInfo
        {
            get { return new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, TemplatesDirectory)); }
        }
        
        public DirectoryInfo OutputDirectoryInfo
        {
            get
            {
                var directory = this.xml.output ?? DefaultOutputDirectory;

                return new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, directory));
            }

            set
            {
                this.xml.output = value == null ? null : value.ToString();

                this.xml.Save();
                this.SendChangedEvent(this);
            }
        }

        public Template[] Templates
        {
            get
            {
                return this.templates.ToArray();
            }
        }

        public Template AddTemplate()
        {
            var template = new Template(this);
            this.templates.Add(template);
            return template;
        }

        public override int CompareTo(object obj)
        {
            if (this.Domain != null)
            {
                var that = obj as XmlRepository;
                if (that != null)
                {
                    return this.Domain.CompareTo(that.Domain);
                }

                return 1;
            }

            return -1;
        }

        public override bool Equals(object obj)
        {
            var that = obj as XmlRepository;
            return that != null && this.DirectoryInfo != null && that.DirectoryInfo != null && this.DirectoryInfo.FullName.Equals(that.DirectoryInfo.FullName);
        }

        public override int GetHashCode()
        {
            return this.DirectoryInfo.FullName.GetHashCode();
        }

        internal static void StripXml(XmlDocument xmlDocument)
        {
            if (xmlDocument.FirstChild.NodeType.Equals(XmlNodeType.XmlDeclaration))
            {
                xmlDocument.RemoveChild(xmlDocument.FirstChild);
            }

            if (xmlDocument.DocumentElement != null)
            {
                var allorsAttributeValue = xmlDocument.DocumentElement.GetAttribute("allors");
                var idAttributeValue = xmlDocument.DocumentElement.GetAttribute("id");
                var versionAttributeValue = xmlDocument.DocumentElement.GetAttribute("version");
                xmlDocument.DocumentElement.Attributes.RemoveAll();
                if (!string.IsNullOrEmpty(allorsAttributeValue))
                {
                    xmlDocument.DocumentElement.SetAttribute("allors", allorsAttributeValue);
                }

                if (!string.IsNullOrEmpty(idAttributeValue))
                {
                    xmlDocument.DocumentElement.SetAttribute("id", idAttributeValue);
                }

                if (!string.IsNullOrEmpty(versionAttributeValue))
                {
                    xmlDocument.DocumentElement.SetAttribute("version", versionAttributeValue);
                }
            }
        }

        internal static void CheckAllorsVersion(string allors, string fileName)
        {
            if (string.IsNullOrEmpty(allors))
            {
                throw new ArgumentException(fileName + " has no Allors version");
            }

            if (!Version.Equals(new Version(allors)))
            {
                throw new ArgumentException(fileName + " has incompatible Allors version");
            }
        }

        public Domain AddSuper(DirectoryInfo directoryInfo)
        {
            var superRepository = new XmlRepository(directoryInfo);
            var superDomain = this.AddSuper(superRepository);

            this.Init(false);

            return superDomain;
        }

        public Domain AddSuper(XmlRepository superDomainRepository)
        {
            if (superDomainRepository.Equals(this))
            {
                throw new ArgumentException("Super domain repository must be a different repository");
            }

            this.repositoryBySuperId.Add(superDomainRepository.Domain.Id, superDomainRepository);
            var superDomain = superDomainRepository.Domain;
            this.Domain.Inherit(superDomain);

            this.Domain.SyncFromMeta(this.domainXml);
            this.domainXml.Save();

            var superDomainId = superDomainRepository.domainXml.id;

            var superDomainDirectoryInfo = new AllorsDirectoryInfo(superDomainRepository.DirectoryInfo);
            var superDomainPath = superDomainDirectoryInfo.GetRelativeOrFullName(this.DirectoryInfo);

            this.xml.AddOrUpdateSuperDomainLocation(superDomainId, superDomainPath);
            this.xml.Save();

            this.Domain.SendChangedEvent();

            return superDomain;
        }

        private void Init(bool create)
        {
            this.repositoryBySuperId = new Dictionary<Guid, XmlRepository>();
            this.unresolvedObjectTypeIds = new List<Guid>();
            this.relationTypesWithNewIds = new HashSet<RelationType>();

            this.DirectoryInfo.Refresh();
            this.fileInfo = new FileInfo(Path.Combine(this.DirectoryInfo.FullName, FileName));

            if (create)
            {
                if (!this.DirectoryInfo.Exists)
                {
                    this.DirectoryInfo.Create();
                }

                if (this.fileInfo.Exists)
                {
                    throw new Exception("Repository already exists");
                }

                this.xml = new RepositoryXml(this.fileInfo);
                this.xml.Save();
            }
            else
            {
                if (!this.fileInfo.Exists)
                {
                    throw new Exception("Repository not found in location " + this.fileInfo.Directory);
                }

                this.xml = RepositoryXml.Load(this.fileInfo);
            }

            this.objectTypesDirectoryInfo = new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, ObjectTypesDirectory));
            if (!this.objectTypesDirectoryInfo.Exists)
            {
                this.objectTypesDirectoryInfo.Create();
            }

            this.inheritancesDirectoryInfo = new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, InheritancesDirectory));
            if (!this.inheritancesDirectoryInfo.Exists)
            {
                this.inheritancesDirectoryInfo.Create();
            }

            this.relationsDirectoryInfo = new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, RelationTypesDirectory));
            if (!this.relationsDirectoryInfo.Exists)
            {
                this.relationsDirectoryInfo.Create();
            }

            this.metaPopulation = new MetaPopulation();

            this.domainFileInfo = new FileInfo(Path.Combine(this.DirectoryInfo.FullName, DomainFileName));
            if (this.domainFileInfo.Exists)
            {
                this.domainXml = DomainXml.Load(this.domainFileInfo);
                this.domain = new Domain(this.metaPopulation, this.domainXml.id);
                this.Domain.SyncToMeta(this.domainXml);

                foreach (IdrefXml partIdRef in this.domainXml.supers)
                {
                    var superDomainId = new Guid(partIdRef.idRef);
                    var superDomainLocationXml = this.xml.LookupSuperDomainLocation(superDomainId);
                    if (superDomainLocationXml == null)
                    {
                        throw new ArgumentException("No location for super domain with id " + superDomainId);
                    }

                    var superDomainDirectoryInfo = new DirectoryInfo(Path.Combine(this.DirectoryInfo.FullName, superDomainLocationXml.location));
                    var repository = new XmlRepository(superDomainDirectoryInfo);
                    this.AddSuper(repository);
                }
            }
            else
            {
                this.domain = new Domain(this.metaPopulation, new Guid());
            }

            this.inheritanceXmlsById = new Dictionary<Guid, InheritanceXml>();
            this.objectTypeXmlsById = new Dictionary<Guid, ObjectTypeXml>();
            this.relationTypeXmlsById = new Dictionary<Guid, RelationTypeXml>();

            // first create ObjectTypes
            foreach (var typeFileInfo in this.objectTypesDirectoryInfo.GetFiles(ObjectTypeWildcard))
            {
                var objectTypeXml = ObjectTypeXml.Load(typeFileInfo);
                this.objectTypeXmlsById[objectTypeXml.id] = objectTypeXml;

                if (!string.IsNullOrEmpty(objectTypeXml.isInterface) && bool.Parse(objectTypeXml.isInterface))
                {
                    new InterfaceBuilder(this.domain, objectTypeXml.id).Build();
                }
                else
                {
                    new ClassBuilder(this.domain, objectTypeXml.id).Build();
                }
            }

            // then sync ObjectTypes
            foreach (var objectTypeXml in this.objectTypeXmlsById.Values)
            {
                var contentsObjectType = (ObjectType)this.domain.MetaPopulation.Find(objectTypeXml.id);
                contentsObjectType.SyncToMeta(this, objectTypeXml);
            }

            foreach (var inheritanceFileInfo in this.inheritancesDirectoryInfo.GetFiles(InheritanceWildcard))
            {
                var inheritanceXml = InheritanceXml.Load(inheritanceFileInfo);
                this.inheritanceXmlsById[inheritanceXml.id] = inheritanceXml;

                var inheritance = new InheritanceBuilder(this.domain, inheritanceXml.id).Build();
                inheritance.SyncToMeta(this, inheritanceXml);
            }

            // first create RelationTypes
            foreach (var relationFileInfo in this.relationsDirectoryInfo.GetFiles(RelationTypeWildcard))
            {
                var relationTypeXml = RelationTypeXml.Load(relationFileInfo);
                this.relationTypeXmlsById[relationTypeXml.id] = relationTypeXml;

                var associationTypeId = Guid.Empty;
                var roleTypeId = Guid.Empty;

                if (relationTypeXml.associationType != null)
                {
                    associationTypeId = relationTypeXml.associationType.id;
                }

                if (relationTypeXml.roleType != null)
                {
                    roleTypeId = relationTypeXml.roleType.id;
                }

                var newIds = false;

                if (associationTypeId == Guid.Empty)
                {
                    associationTypeId = Guid.NewGuid();
                    newIds = true;
                }

                if (roleTypeId == Guid.Empty)
                {
                    roleTypeId = Guid.NewGuid();
                    newIds = true;
                }

                var relationType = new RelationTypeBuilder(this.domain, relationTypeXml.id, associationTypeId, roleTypeId).Build();

                if (newIds)
                {
                    this.relationTypesWithNewIds.Add(relationType);
                }
            }

            // then sync RelationTypes
            foreach (var relationTypeXml in this.relationTypeXmlsById.Values)
            {
                var contentsRelationType = (RelationType)this.domain.MetaPopulation.Find(relationTypeXml.id);
                contentsRelationType.SyncToMeta(this, relationTypeXml);
            }

            this.templates = new List<Template>();

            if (!this.TemplatesDirectoryInfo.Exists)
            {
                this.TemplatesDirectoryInfo.Create();
            }

            foreach (var configurationFileInfo in this.TemplatesDirectoryInfo.GetFiles(Template.TemplatesWildcard))
            {
                var template = new Template(this, configurationFileInfo);
                this.templates.Add(template);
            }

            this.domain.SendChangedEvent();
        }

        internal void SendChangedEvent(RepositoryObject repositoryObject)
        {
            var repositoryObjectChanged = this.ObjectChanged;
            if (repositoryObjectChanged != null)
            {
                repositoryObjectChanged(this, new RepositoryObjectChangedEventArgs(this, repositoryObject));
            }
        }

        internal void SendDeletedEvent(Guid repositoryObjectId)
        {
            var repositoryObjectDeleted = this.ObjectDeleted;
            if (repositoryObjectDeleted != null)
            {
                repositoryObjectDeleted(this, new RepositoryObjectDeletedEventArgs(this, repositoryObjectId));
            }
        }

        internal void RemoveTemplate(Template template)
        {
            this.templates.Remove(template);
        }

        internal void Generate(string name, GenerateLog log)
        {
            foreach (var template in this.templates)
            {
                if (name.Equals(template.Name))
                {
                    template.Generate(log);
                }
            }
        }
    }
}