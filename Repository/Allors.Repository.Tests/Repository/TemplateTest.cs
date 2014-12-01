// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemplateTest.cs" company="Allors bvba">
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

    using Allors.Meta.Templates;

    using NUnit.Framework;

    [TestFixture]
    public class TemplateTest
    {
        private DirectoryInfo directoryInfo;
        private Domain domain;
        private Template template;
        private XmlRepository repository;

        private DirectoryInfo TemplatesSourceDirectoryInfo
        {
            get { return new DirectoryInfo(Path.Combine(this.directoryInfo.FullName, "templatesSource")); }
        }

        [SetUp]
        public void SetUp()
        {
            this.directoryInfo = new DirectoryInfo("template");
            this.directoryInfo.DeleteRecursive();

            this.directoryInfo.Create();

            this.repository = new XmlRepository(this.directoryInfo, true);

            this.domain = this.repository.Domain;
            this.domain.Name = "MyDomain";

            this.template = this.repository.AddTemplate();
            this.template.Name = "MyTemplate";
        }

        public void LogDebug(object sender, string message)
        {
        }

        public void LogError(object sender, string message)
        {
        }

        public void LogInfo(object sender, string message)
        {
        }

        public void LogWarning(object sender, string message)
        {
        }

        [Test]
        public void Information()
        {
            this.TemplatesSourceDirectoryInfo.Create();
            var templateSourceFileInfo = new FileInfo(Path.Combine(this.TemplatesSourceDirectoryInfo.FullName, "test.stg"));
            using (var writer = templateSourceFileInfo.CreateText())
            {
                writer.Write(
                    @"group AllorsBaseReference;
domain(domain) ::= <<
<domain>$domain.Name$</domain>
>>

TemplateConfiguration(domain) ::= <<
<allors version=""1.0"">
	<generation template=""domain"" output=""test/domain.xml""/> 
</allors>
>>

TemplateId() ::= <<{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}>>
TemplateName() ::= <<MyTemplate>>
TemplateVersion() ::= <<1.0.0>>
TemplateAllors() ::= <<1.0>>
");
                writer.Close();
            }

            this.template.Source = new Uri(templateSourceFileInfo.FullName);
            var template = this.template.StringTemplate;

            Assert.AreEqual(new Guid("{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}"), template.Id);
            Assert.AreEqual("MyTemplate", template.Name);
            Assert.AreEqual("1.0.0", template.Version);
            Assert.AreEqual("1.0", template.Allors);

            this.template.Source = new Uri(templateSourceFileInfo.FullName);
            template = this.template.StringTemplate;

            Assert.AreEqual("MyTemplate", template.Name);
            Assert.AreEqual("1.0.0", template.Version);
            Assert.AreEqual("1.0", template.Allors);
            Assert.AreEqual(new Guid("{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}"), template.Id);

            this.template.Source = new Uri(templateSourceFileInfo.FullName);
            template = this.template.StringTemplate;

            Assert.AreEqual("1.0.0", template.Version);
            Assert.AreEqual("1.0", template.Allors);
            Assert.AreEqual(new Guid("{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}"), template.Id);
            Assert.AreEqual("MyTemplate", template.Name);

            this.template.Source = new Uri(templateSourceFileInfo.FullName);
            template = this.template.StringTemplate;

            Assert.AreEqual("1.0", template.Allors);
            Assert.AreEqual(new Guid("{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}"), template.Id);
            Assert.AreEqual("MyTemplate", template.Name);
            Assert.AreEqual("1.0.0", template.Version);
        }

        [Test]
        public void Maximal()
        {
            this.TemplatesSourceDirectoryInfo.Create();
            var templateSourceFileInfo = new FileInfo(Path.Combine(this.TemplatesSourceDirectoryInfo.FullName, "test.stg"));
            using (var writer = templateSourceFileInfo.CreateText())
            {
                writer.Write(
@"group AllorsBaseReference;

TemplateId() ::= <<{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}>>
TemplateVersion() ::= <<1.0>>
TemplateAllors() ::= <<1.0>>
TemplateName() ::= <<Test>>
TemplateConfiguration(domain,settings) ::= <<
<generations>
    <generation template=""domain"" output=""test//domain.xml""/>
$domain.CompositeObjectTypes:configureTypes()$
</generations>
>>

configureTypes(objectType) ::= <<
	<generation template=""objectTypes"" input=""$objectType.IdAsString$"" output=""test//$objectType.IdAsString$.xml""/> 
	$objectType.RoleTypes:{roleType | $roleType.RelationTypeWhereRoleType:configureRelations()$}$
>>

configureRelations(relationType) ::= <<
	<generation template=""relations"" input=""$relationType.IdAsString$"" output=""test//$relationType.IdAsString$.xml""/> 
>>

domain(domain,settings) ::= <<
<domain/>
>>

objectTypes(domain,settings,objectType) ::= <<
<objectType id=""$objectType.Id $""/>
>>

relations(domain,settings,relationType) ::= <<
<relations id=""$relationType.Id$"" role=""$relationType.RoleType.AssignedSingularName$"" roleId=""$relationType.RoleType.IdAsString$"" associationId=""$relationType.AssociationType.IdAsString$""/>
>>

");
                writer.Close();
            }

            this.template.Source = new Uri(templateSourceFileInfo.FullName);

            var @classId = new Guid("{07B000C7-800F-459b-A6BB-17DC9E690A06}");
            var @class = this.domain.BuildClass(@classId, "Company", "Companies");

            var relationId = new Guid("{3C48C2D5-79E2-4da2-BDB4-2EE7307DB5EF}");
            var associationId = new Guid("{F43B2971-DEB8-494B-B2D3-3A5331991A03}");
            var roleId = new Guid("{B0FDE156-1009-4A04-B61D-10F1A6326416}");
            var relationType = this.domain.BuildRelationType(relationId, associationId, @class, roleId, @class, "role", "roles");

            var relation2Id = new Guid("{ABB975B6-5374-4654-96A2-E98D5C2A1786}");
            var association2Id = new Guid("{9A1B9365-29D0-4AD4-B6C6-D2D385EABC01}");
            var role2Id = new Guid("{4F280135-E385-4C08-B06E-AD38DDAAA0D5}");
            var relation2 = this.domain.BuildRelationType(relation2Id, association2Id, @class, role2Id, @class, "role2", "roles2");

            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new DummyLog());

            var xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/domain.xml"));
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            var expectedXml = this.ToComparableXml(@"<domain/>");
            var actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + @classId.ToString().ToLower() + ".xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<objectType id=""" + @classId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relationId.ToString().ToLower() + ".xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relations id=""" + relationId.ToString().ToLower() + @""" role=""role"" roleId=""" + roleId.ToString().ToLower() + @""" associationId=""" + associationId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relation2Id.ToString().ToLower() + ".xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relations id=""" + relation2Id.ToString().ToLower() + @""" role=""role2"" roleId=""" + role2Id.ToString().ToLower() + @""" associationId=""" + association2Id.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            relationType.RoleType.AssignedSingularName = "role3";
            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new DummyLog());

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/domain.xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<domain/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + @classId.ToString().ToLower() + ".xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<objectType id=""" + @classId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relationId.ToString().ToLower() + ".xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relations id=""" + relationId.ToString().ToLower() + @""" role=""role3"" roleId=""" + roleId.ToString().ToLower() + @""" associationId=""" + associationId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relation2Id.ToString().ToLower() + ".xml"));
            xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relations id=""" + relation2Id.ToString().ToLower() + @""" role=""role2"" roleId=""" + role2Id.ToString().ToLower() + @""" associationId=""" + association2Id.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocument.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);
        }

        [Test]
        public void Minimal()
        {
            this.TemplatesSourceDirectoryInfo.Create();
            var templateSourceFileInfo = new FileInfo(Path.Combine(this.TemplatesSourceDirectoryInfo.FullName, "test.stg"));
            using (var writer = templateSourceFileInfo.CreateText())
            {
                writer.Write(
                    @"group AllorsBaseReference;

TemplateId() ::= <<{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}>>
TemplateVersion() ::= <<1.0>>
TemplateAllors() ::= <<1.0>>
TemplateName() ::= <<Test>>
TemplateConfiguration(domain,settings) ::= <<
<generations>
    <generation template=""domain"" output=""test//domain.xml""/>
$domain.CompositeObjectTypes:configureTypes()$
</generations>
>>

configureRelations(relationType) ::= <<
	<generation template=""relationTypes"" input=""$relationType.Id $"" output=""test//$relationType.Id $.xml""/> 
>>

configureTypes(objectType) ::= <<
	<generation template=""objectTypes"" input=""$objectType.Id $"" output=""test//$objectType.Id$.xml""/> 
	$objectType.RoleTypes:{roleType | $roleType.RelationTypeWhereRoleType:configureRelations()$}$
>>

relationTypes(domain,settings,relationType) ::= <<
<relationType id=""$relationType.Id $"" role=""$relationType.RoleType.AssignedSingularName$""/>
>>

objectTypes(domain,settings,objectType) ::= <<
<objectType id=""$objectType.Id $""/>
>>

domain(domain,settings) ::= <<
<domain/>
>>
");
                writer.Close();
            }

            this.template.Source = new Uri(templateSourceFileInfo.FullName);

            var @classId = new Guid("{07B000C7-800F-459b-A6BB-17DC9E690A06}");
            var @class = this.domain.BuildClass(@classId, "Company", "Companies");

            var relationId = new Guid("{3C48C2D5-79E2-4da2-BDB4-2EE7307DB5EF}");
            var associationId = new Guid("{F43B2971-DEB8-494B-B2D3-3A5331991A03}");
            var roleId = new Guid("{B0FDE156-1009-4A04-B61D-10F1A6326416}");
            var relationType = this.domain.BuildRelationType(relationId, associationId, @class, roleId, @class, "role", "roles");

            var relation2Id = new Guid("{ABB975B6-5374-4654-96A2-E98D5C2A1786}");
            var association2Id = new Guid("{9A1B9365-29D0-4AD4-B6C6-D2D385EABC01}");
            var role2Id = new Guid("{4F280135-E385-4C08-B06E-AD38DDAAA0D5}");
            var relation2 = this.domain.BuildRelationType(relation2Id, association2Id, @class, role2Id, @class, "role2", "roles2");

            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new DummyLog());

            var xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/domain.xml"));
            var xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            var expectedXml = this.ToComparableXml(@"<domain/>");
            var actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + classId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<objectType id=""" + classId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, string.Format("MyTemplate/test/{0}.xml", relationId.ToString().ToLower())));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relationId.ToString().ToLower() + @""" role=""role""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relation2Id.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relation2Id.ToString().ToLower() + @""" role=""role2""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            relationType.RoleType.AssignedSingularName = "role3";
            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new DummyLog());

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/domain.xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<domain/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + classId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<objectType id=""" + classId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relationId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relationId.ToString().ToLower() + @""" role=""role3""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/" + relation2Id.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relation2Id.ToString().ToLower() + @""" role=""role2""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);
        }
        
        [Test]
        public void Settings()
        {
            this.TemplatesSourceDirectoryInfo.Create();
            var templateSourceFileInfo = new FileInfo(Path.Combine(this.TemplatesSourceDirectoryInfo.FullName, "test.stg"));
            using (var writer = templateSourceFileInfo.CreateText())
            {
                writer.Write(
                    @"group AllorsBaseReference;

TemplateId() ::= <<{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}>>
TemplateVersion() ::= <<1.0>>
TemplateAllors() ::= <<1.0>>
TemplateName() ::= <<Test>>
TemplateConfiguration(domain,settings) ::= <<
<generations>
    <generation template=""domain"" output=""test//domain.xml""/>
</generations>
>>

domain(domain,settings) ::= <<
<domain x=""$settings.x$"" yz=""$nested()$"" />
>>

nested() ::= <%
    $settings.y.z$
%>

");
                writer.Close();
            }

            var settings = this.repository.Templates[0].Settings;
            settings["x"] = "x";
            settings["y.z"] = "yz";

            this.template.Source = new Uri(templateSourceFileInfo.FullName);

            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new ExceptionLog());

            var xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/domain.xml"));
            var xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            var expectedXml = this.ToComparableXml(@"<domain x=""x"" yz=""yz"" />");
            var actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);
        }

        [Test]
        public void NonExistingSettings()
        {
            this.TemplatesSourceDirectoryInfo.Create();
            var templateSourceFileInfo = new FileInfo(Path.Combine(this.TemplatesSourceDirectoryInfo.FullName, "test.stg"));
            using (var writer = templateSourceFileInfo.CreateText())
            {
                writer.Write(
                    @"group AllorsBaseReference;

TemplateId() ::= <<{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}>>
TemplateVersion() ::= <<1.0>>
TemplateAllors() ::= <<1.0>>
TemplateName() ::= <<Test>>
TemplateConfiguration(domain,settings) ::= <<
<generations>
    <generation template=""domain"" output=""test//domain.xml""/>
</generations>
>>

domain(domain,settings) ::= <<
<domain xy=""$if(settings.ExistX)$$settings.x.y$$endif$"" ab=""$if(settings.ExistA)$$settings.a.b$$endif$"" />
>>
");
                writer.Close();
            }

            var settings = this.repository.Templates[0].Settings;
            settings["x.y"] = "xy";

            this.template.Source = new Uri(templateSourceFileInfo.FullName);

            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new ExceptionLog());

            var xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "MyTemplate/test/domain.xml"));
            var xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            var expectedXml = this.ToComparableXml(@"<domain xy=""xy"" ab=""""/>");
            var actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);
        }

        [Test]
        public void OutputPath()
        {
            this.TemplatesSourceDirectoryInfo.Create();
            var templateSourceFileInfo = new FileInfo(Path.Combine(this.TemplatesSourceDirectoryInfo.FullName, "test.stg"));
            using (var writer = templateSourceFileInfo.CreateText())
            {
                writer.Write(
                    @"group AllorsBaseReference;

TemplateId() ::= <<{E615D7A2-4EF3-4903-BF0E-A58493EF5F7B}>>
TemplateVersion() ::= <<1.0>>
TemplateAllors() ::= <<1.0>>
TemplateName() ::= <<Test>>
TemplateConfiguration(domain,settings) ::= <<
<generations>
    <generation template=""domain"" output=""test/domain.xml""/>
$domain.CompositeObjectTypes:configureTypes()$
</generations>
>>

configureRelations(relationType) ::= <<
	<generation template=""relations"" input=""$relationType.Id $"" output=""test/$relationType.Id $.xml""/> 
>>

configureTypes(objectType) ::= <<
	<generation template=""objectTypes"" input=""$objectType.Id $"" output=""test/$objectType.Id $.xml""/> 
	$objectType.RoleTypes:{roleType | $roleType.RelationTypeWhereRoleType:configureRelations()$}$
>>

relations(domain,settings,relationType) ::= <<
<relationType id=""$relationType.Id $"" role=""$relationType.RoleType.AssignedSingularName$""/>
>>

objectTypes(domain,settings,objectType) ::= <<
<objectType id=""$objectType.Id $""/>
>>

domain(domain,settings) ::= <<
<domain/>
>>
");
                writer.Close();
            }

            this.template.Source = new Uri(templateSourceFileInfo.ToString());
            this.template.Output = this.repository.OutputDirectoryInfo.FullName;

            var @classId = new Guid("{07B000C7-800F-459b-A6BB-17DC9E690A06}");
            var @class = this.domain.BuildClass(@classId, "Company", "Companies");

            var relationId = new Guid("{3C48C2D5-79E2-4da2-BDB4-2EE7307DB5EF}");
            var associationId = new Guid("{F43B2971-DEB8-494B-B2D3-3A5331991A03}");
            var roleId = new Guid("{B0FDE156-1009-4A04-B61D-10F1A6326416}");
            var relationType = this.domain.BuildRelationType(relationId, associationId, @class, roleId, @class, "role", "roles");

            var relation2Id = new Guid("{ABB975B6-5374-4654-96A2-E98D5C2A1786}");
            var association2Id = new Guid("{9A1B9365-29D0-4AD4-B6C6-D2D385EABC01}");
            var role2Id = new Guid("{4F280135-E385-4C08-B06E-AD38DDAAA0D5}");
            var relation2 = this.domain.BuildRelationType(relation2Id, association2Id, @class, role2Id, @class, "role2", "roles2");

            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new DummyLog());

            var xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/domain.xml"));
            var xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            var expectedXml = this.ToComparableXml(@"<domain/>");
            var actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/" + classId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<objectType id=""" + classId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/" + relationId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relationId.ToString().ToLower() + @""" role=""role""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/" + relation2Id.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relation2Id.ToString().ToLower() + @""" role=""role2""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            relationType.RoleType.AssignedSingularName = "role3";
            Assert.IsTrue(this.domain.MetaPopulation.IsValid);

            this.template.Generate(new DummyLog());

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/domain.xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<domain/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/" + classId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<objectType id=""" + classId.ToString().ToLower() + @"""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/" + relationId.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relationId.ToString().ToLower() + @""" role=""role3""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);

            xmlFileInfo = new FileInfo(Path.Combine(this.repository.OutputDirectoryInfo.FullName, "test/" + relation2Id.ToString().ToLower() + ".xml"));
            xmlDocumnt = new XmlDocument();
            xmlDocumnt.Load(xmlFileInfo.FullName);
            expectedXml = this.ToComparableXml(@"<relationType id=""" + relation2Id.ToString().ToLower() + @""" role=""role2""/>");
            actualXml = this.ToComparableXml(xmlDocumnt.InnerXml);
            Assert.AreEqual(expectedXml, actualXml);
        }

        private string ToComparableXml(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            xmlDocument.Normalize();

            var stripWhiteSpace = new Regex(@">(\n|\s)*<");
            return stripWhiteSpace.Replace(xmlDocument.InnerXml, "><");
        }

        private class DummyLog : Log
        {
            public override void Error(object sender, string message)
            {
            }
        }

        private class ExceptionLog : Log
        {
            public override void Error(object sender, string message)
            {
                throw new Exception(message);
            }
        }
    }
}