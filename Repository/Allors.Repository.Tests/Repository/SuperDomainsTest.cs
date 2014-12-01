// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuperDomainsTest.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;

    using Allors.Meta.Events;
    using Allors.Meta.Meta;

    using NUnit.Framework;

    [TestFixture]
    public class SuperDomainsTest
    {
        private DirectoryInfo domainDirectoryInfo;
        private XmlRepository domainRepository;
        private bool domainRepositoryInvalidatedCalled;

        private DirectoryInfo superDomainDirectoryInfo;
        private XmlRepository superDomainRepository;

        private DirectoryInfo InheritancesDirectoryInfo
        {
            get
            {
                return new DirectoryInfo(Path.Combine(this.domainDirectoryInfo.FullName, "inheritances"));
            }
        }

        private DirectoryInfo ObjectTypesDirectoryInfo
        {
            get { return new DirectoryInfo(Path.Combine(this.domainDirectoryInfo.FullName, "objectTypes")); }
        }

        private DirectoryInfo RelationTypesDirectoryInfo
        {
            get { return new DirectoryInfo(Path.Combine(this.domainDirectoryInfo.FullName, "relationTypes")); }
        }

        [SetUp]
        public void SetUp()
        {
            this.domainDirectoryInfo = new DirectoryInfo("repository/domain");
            this.domainDirectoryInfo.DeleteRecursive();

            this.domainDirectoryInfo.Create();

            this.domainRepository = new XmlRepository(this.domainDirectoryInfo, true);

            this.superDomainDirectoryInfo = new DirectoryInfo("repository/superdomain");
            this.superDomainDirectoryInfo.DeleteRecursive();

            this.superDomainDirectoryInfo.Create();

            this.superDomainRepository = new XmlRepository(this.superDomainDirectoryInfo, true);
        }

        [Test]
        public void AddSameSuperDomain()
        {
            var exceptionThrown = false;
            try
            {
                this.domainRepository.AddSuper(this.domainDirectoryInfo);
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                this.domainRepository.AddSuper(this.domainDirectoryInfo);
                Assert.Fail();
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);

            exceptionThrown = false;
            try
            {
                this.domainRepository.AddSuper(this.domainDirectoryInfo);
                Assert.Fail();
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [Test]
        public void AddSuperDomain()
        {
            var superDomain = this.superDomainRepository.Domain;

            var superDomainAssociationClass = superDomain.BuildClass(Guid.NewGuid(), "SuperDomainAssociationType", "SuperDomainAssociationTypes");
            var superDomainRoleInterface = superDomain.BuildInterface(Guid.NewGuid(), "SuperDomainRoleType", "SuperDomainRoleTypes");
            
            var superDomainInterface = superDomain.BuildInterface(Guid.NewGuid(), "SuperDomainAbstractType", "SuperDomainAbstractTypes");
            var superDomainSuperInterface = superDomain.BuildInterface(Guid.NewGuid(), "SuperDomainInterfaceType", "SuperDomainInterfaceTypes");
            
            var superDomainAssociationInheritance = superDomain.BuildInheritance(Guid.NewGuid(), superDomainAssociationClass, superDomainInterface);
            var superDomainRoleInheritance = superDomain.BuildInheritance(Guid.NewGuid(), superDomainRoleInterface, superDomainInterface);

            var superDomainUnitRelationType = superDomain.BuildRelationType(Guid.NewGuid(), Guid.NewGuid(), superDomainAssociationClass, Guid.NewGuid(), (ObjectType)superDomain.MetaPopulation.Find(UnitIds.StringId));
            var superDomainCompositeRelationType = superDomain.BuildRelationType(Guid.NewGuid(), Guid.NewGuid(), superDomainAssociationClass, Guid.NewGuid(), superDomainRoleInterface);
            
            superDomainAssociationClass.SendChangedEvent();
            superDomainRoleInterface.SendChangedEvent();
            superDomainInterface.SendChangedEvent();
            superDomainSuperInterface.SendChangedEvent();
            superDomainAssociationInheritance.SendChangedEvent();
            superDomainRoleInheritance.SendChangedEvent();
            superDomainUnitRelationType.SendChangedEvent();
            superDomainCompositeRelationType.SendChangedEvent();

            this.domainRepository.AddSuper(this.superDomainDirectoryInfo);

            var domain = this.domainRepository.Domain;

            var xmlFileInfo = new FileInfo(Path.Combine(this.domainDirectoryInfo.FullName, "allors.repository"));
            var expectedXml =
@"<repository allors=""1.0"">
    <super id=""" + superDomain.Id + @""">
        <location>.." + Path.DirectorySeparatorChar + this.superDomainRepository.DirectoryInfo.Name + @"</location>
    </super>
</repository>";
            this.AssertXml(expectedXml, xmlFileInfo);

            xmlFileInfo = new FileInfo(Path.Combine(this.domainDirectoryInfo.FullName, "allors.domain"));
            expectedXml = 
@"<domain allors=""1.0"" id=""" + domain.Id + @""">
    <super idref=""" + superDomain.Id + @"""/>
</domain>";
            this.AssertXml(expectedXml, xmlFileInfo);

            var domainSuperDomain = (Domain)domain.MetaPopulation.Find(superDomain.Id);
            var domainAssociationType = (ObjectType)domain.MetaPopulation.Find(superDomainAssociationClass.Id);
            var domainRoleType = (ObjectType)domain.MetaPopulation.Find(superDomainRoleInterface.Id);
            var domainAbstractType = (ObjectType)domain.MetaPopulation.Find(superDomainInterface.Id);
            var domainInterfaceType = (ObjectType)domain.MetaPopulation.Find(superDomainSuperInterface.Id);
            var domainAbstractInheritance = (Inheritance)domain.MetaPopulation.Find(superDomainAssociationInheritance.Id);
            var domainInterfaceInheritance = (Inheritance)domain.MetaPopulation.Find(superDomainRoleInheritance.Id);
            var domainUnitRelationType = (RelationType)domain.MetaPopulation.Find(superDomainUnitRelationType.Id);
            var domainCompositeRelationType = (RelationType)domain.MetaPopulation.Find(superDomainCompositeRelationType.Id);
            
            Assert.AreEqual(0, this.ObjectTypesDirectoryInfo.GetFiles().Length);
            Assert.AreEqual(0, this.RelationTypesDirectoryInfo.GetFiles().Length);
            Assert.AreEqual(0, this.InheritancesDirectoryInfo.GetFiles().Length);
            
            Assert.AreEqual(0, this.ObjectTypesDirectoryInfo.GetFiles().Length);
            Assert.AreEqual(0, this.RelationTypesDirectoryInfo.GetFiles().Length);
            Assert.AreEqual(0, this.InheritancesDirectoryInfo.GetFiles().Length);

            Assert.AreEqual(superDomain.Id, domainSuperDomain.Id);
            Assert.AreEqual(superDomainAssociationClass.Id, domainAssociationType.Id);
            Assert.AreEqual(superDomainRoleInterface.Id, domainRoleType.Id);
            Assert.AreEqual(superDomainInterface.Id, domainAbstractType.Id);
            Assert.AreEqual(superDomainSuperInterface.Id, domainInterfaceType.Id);
            Assert.AreEqual(superDomainAssociationInheritance.Id, domainAbstractInheritance.Id);
            Assert.AreEqual(superDomainRoleInheritance.Id, domainInterfaceInheritance.Id);
            Assert.AreEqual(superDomainUnitRelationType.Id, domainUnitRelationType.Id);
            Assert.AreEqual(superDomainCompositeRelationType.Id, domainCompositeRelationType.Id);

            xmlFileInfo = new FileInfo(Path.Combine(this.domainDirectoryInfo.FullName, "allors.repository"));
            expectedXml =
@"<repository allors=""1.0"">
    <super id=""" + superDomain.Id + @""">
        <location>.." + Path.DirectorySeparatorChar + this.superDomainRepository.DirectoryInfo.Name + @"</location>
    </super>
</repository>";
            this.AssertXml(expectedXml, xmlFileInfo);

            xmlFileInfo = new FileInfo(Path.Combine(this.domainDirectoryInfo.FullName, "allors.domain"));
            expectedXml = 
@"<domain allors=""1.0"" id=""" + domain.Id + @""">
    <super idref=""" + superDomain.Id + @"""/>
</domain>";
            this.AssertXml(expectedXml, xmlFileInfo);

            var dupclicateRepository = new XmlRepository(this.domainDirectoryInfo);
            var duplicateDomain = dupclicateRepository.Domain;

            Assert.IsFalse(domain.MetaPopulation.IsValid);

            var duplicateSuperDomain = (Domain)duplicateDomain.MetaPopulation.Find(superDomain.Id);
            var duplicateAssociationType = (ObjectType)duplicateDomain.MetaPopulation.Find(superDomainAssociationClass.Id);
            var duplicateRoleType = (ObjectType)duplicateDomain.MetaPopulation.Find(superDomainRoleInterface.Id);
            var duplicateAbstractType = (ObjectType)duplicateDomain.MetaPopulation.Find(superDomainInterface.Id);
            var duplicateInterfaceType = (ObjectType)duplicateDomain.MetaPopulation.Find(superDomainSuperInterface.Id);
            var duplicateAbstractInheritance = (Inheritance)duplicateDomain.MetaPopulation.Find(superDomainAssociationInheritance.Id);
            var duplicateInterfaceInheritance = (Inheritance)duplicateDomain.MetaPopulation.Find(superDomainRoleInheritance.Id);
            var duplicateUnitRelationType = (RelationType)duplicateDomain.MetaPopulation.Find(superDomainUnitRelationType.Id);
            var duplicateCompositeRelationType = (RelationType)duplicateDomain.MetaPopulation.Find(superDomainCompositeRelationType.Id);
            
            Assert.AreEqual(0, this.ObjectTypesDirectoryInfo.GetFiles().Length);
            Assert.AreEqual(0, this.RelationTypesDirectoryInfo.GetFiles().Length);
            Assert.AreEqual(0, this.InheritancesDirectoryInfo.GetFiles().Length);

            Assert.AreEqual(superDomain.Id, duplicateSuperDomain.Id);
            Assert.AreEqual(superDomainAssociationClass.Id, duplicateAssociationType.Id);
            Assert.AreEqual(superDomainRoleInterface.Id, duplicateRoleType.Id);
            Assert.AreEqual(superDomainInterface.Id, duplicateAbstractType.Id);
            Assert.AreEqual(superDomainSuperInterface.Id, duplicateInterfaceType.Id);
            Assert.AreEqual(superDomainAssociationInheritance.Id, duplicateAbstractInheritance.Id);
            Assert.AreEqual(superDomainRoleInheritance.Id, duplicateInterfaceInheritance.Id);
            Assert.AreEqual(superDomainUnitRelationType.Id, duplicateUnitRelationType.Id);
            Assert.AreEqual(superDomainCompositeRelationType.Id, duplicateCompositeRelationType.Id);
        }

        [Test]
        public void DeleteSuperDomain()
        {
            var superDomain = this.superDomainRepository.Domain;
            
            var superDomainAssociationClass = superDomain.BuildClass(Guid.NewGuid(), "SuperDomainAssociationType", "SuperDomainAssociationTypes");
            var superDomainRoleInterface = superDomain.BuildInterface(Guid.NewGuid(), "SuperDomainRoleType", "SuperDomainRoleTypes");
            var superDomainInterface = superDomain.BuildInterface(Guid.NewGuid(), "SuperDomainAbstractType", "SuperDomainAbstractTypes");
            var superDomainSuperInterface = superDomain.BuildInterface(Guid.NewGuid(), "SuperDomainInterfaceType", "SuperDomainInterfaceTypes");
            var superDomainAssociationInheritance = superDomain.BuildInheritance(Guid.NewGuid(), superDomainAssociationClass, superDomainInterface);
            var superDomainRoleInheritance = superDomain.BuildInheritance(Guid.NewGuid(), superDomainRoleInterface, superDomainInterface);
            var superDomainUnitRelationType = superDomain.BuildRelationType(Guid.NewGuid(), Guid.NewGuid(), superDomainAssociationClass, Guid.NewGuid(), (ObjectType)superDomain.MetaPopulation.Find(UnitIds.StringId));
            var superDomainCompositeRelationType = superDomain.BuildRelationType(Guid.NewGuid(), Guid.NewGuid(), superDomainAssociationClass, Guid.NewGuid(), superDomainRoleInterface);
            
            superDomainAssociationClass.SendChangedEvent();
            superDomainRoleInterface.SendChangedEvent();
            superDomainInterface.SendChangedEvent();
            superDomainSuperInterface.SendChangedEvent();
            superDomainAssociationInheritance.SendChangedEvent();
            superDomainRoleInheritance.SendChangedEvent();
            superDomainUnitRelationType.SendChangedEvent();
            superDomainCompositeRelationType.SendChangedEvent();

            this.domainRepository.AddSuper(this.superDomainDirectoryInfo);

            var domain = this.domainRepository.Domain;
            var integratedSuperDomain = (Domain)domain.MetaPopulation.Find(superDomain.Id);

            this.domainRepository.MetaObjectChanged += this.DomainRepositoryMetaObjectChanged;

            this.domainRepository.RemoveSuper(integratedSuperDomain);

            var xmlFileInfo = new FileInfo(Path.Combine(this.domainDirectoryInfo.FullName, "allors.repository"));
            var expectedXml = @"<repository allors=""1.0""/>";
            this.AssertXml(expectedXml, xmlFileInfo);

            xmlFileInfo = new FileInfo(Path.Combine(this.domainDirectoryInfo.FullName, "allors.domain"));
            expectedXml = @"
                        <domain allors=""1.0"" id=""" + domain.Id + @"""/>";
            this.AssertXml(expectedXml, xmlFileInfo);

            Assert.IsTrue(this.domainRepositoryInvalidatedCalled);

            this.domainRepository = new XmlRepository(this.domainRepository.DirectoryInfo);

            domain = this.domainRepository.Domain;
            integratedSuperDomain = (Domain)domain.MetaPopulation.Find(superDomain.Id);

            Assert.IsNull(integratedSuperDomain);
        }

        private void AssertXml(string expectedXml, FileInfo xmlFileInfo)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);

            var actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(this.ToComparableXml(expectedXml), actualXml);
        }

        private void DomainRepositoryMetaObjectChanged(object sender, RepositoryMetaObjectChangedEventArgs args)
        {
            if (args.MetaObject is Domain)
            {
                this.domainRepositoryInvalidatedCalled = true;
            }
        }

        private string ToComparableXml(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Normalize();

            var stripWhiteSpace = new Regex(@">(\n|\s)*<");
            return stripWhiteSpace.Replace(xmlDocument.InnerXml, "><");
        }
    }
}