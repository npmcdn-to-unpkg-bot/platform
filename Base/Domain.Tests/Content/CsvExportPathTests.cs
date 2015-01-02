// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvExportPathTests.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using Allors.Meta;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class CsvExportPathTests : DomainTest
    {
        [Test]
        public void OperandTypeConstructor()
        {
            var dutchBelgium = new Locales(this.DatabaseSession).DutchBelgium;

            new C1Builder(this.DatabaseSession).WithC1AllorsString("c1A").WithC1AllorsDecimal(10.5M).Build();
            new C1Builder(this.DatabaseSession).WithC1AllorsString("c1B").WithC1AllorsDecimal(11.5M).Build();

            this.DatabaseSession.Derive(true);

            var csvFile = new CsvExport("Test");
            csvFile.Columns.Add(new CsvExportPath(C1s.Meta.C1AllorsString));
            csvFile.Columns.Add(new CsvExportPath(C1s.Meta.C1AllorsDecimal));

            var aclMock = new Mock<IAccessControlList>();
            aclMock.Setup(acl => acl.CanRead(It.IsAny<PropertyType>())).Returns(true);
            var acls = new AccessControlListCache(null, (allorsObject, user) => aclMock.Object);

            var extent = this.DatabaseSession.Extent(C1s.Meta.ObjectType).AddSort(C1s.Meta.C1AllorsString);
            var csv = csvFile.Write(extent, dutchBelgium, acls);

            Assert.AreEqual(
@"""C1AllorsString"";""C1AllorsDecimal""
""c1A"";""10,5""
""c1B"";""11,5""",
                    csv);
        }

        [Test]
        public void PathConstructor()
        {
            var dutchBelgium = new Locales(this.DatabaseSession).DutchBelgium;

            new C1Builder(this.DatabaseSession).WithC1AllorsString("c1A").WithC1C2One2One(new C2Builder(this.DatabaseSession).WithC2AllorsString("c2A").Build()).Build();
            new C1Builder(this.DatabaseSession).WithC1AllorsString("c1B").WithC1C2One2One(new C2Builder(this.DatabaseSession).WithC2AllorsString("c2B").Build()).Build();

            this.DatabaseSession.Derive(true);

            var csvFile = new CsvExport("Test");
            csvFile.Columns.Add(new CsvExportPath(C1s.Meta.C1AllorsString));
            csvFile.Columns.Add(new CsvExportPath(new Path(C1s.Meta.C1C2One2One, C2s.Meta.C2AllorsString)));

            var extent = this.DatabaseSession.Extent(C1s.Meta.ObjectType).AddSort(C1s.Meta.C1AllorsString);

            var aclMock = new Mock<IAccessControlList>();
            aclMock.Setup(acl => acl.CanRead(It.IsAny<PropertyType>())).Returns(true);
            var acls = new AccessControlListCache(null, (allorsObject, user) => aclMock.Object);

            var csv = csvFile.Write(extent, dutchBelgium, acls);

            Assert.AreEqual(
@"""C1AllorsString"";""C2AllorsString""
""c1A"";""c2A""
""c1B"";""c2B""",
                    csv);
        }

        [Test]
        public void Locale()
        {
            var englishGreatBritain = new Locales(this.DatabaseSession).EnglishGreatBritain;
            var dutchBelgium = new Locales(this.DatabaseSession).DutchBelgium;

            new C1Builder(this.DatabaseSession).WithC1AllorsString("c1A").WithC1AllorsDecimal(10.5M).Build();
            new C1Builder(this.DatabaseSession).WithC1AllorsString("c1B").WithC1AllorsDecimal(11.5M).Build();

            this.DatabaseSession.Derive(true);

            var column1 = new CsvExportPath(C1s.Meta.C1AllorsString);
            var column2 = new CsvExportPath(C1s.Meta.C1AllorsDecimal);

            var export = new CsvExport("Test");
            export.Columns.Add(column1);
            export.Columns.Add(column2);

            var extent = this.DatabaseSession.Extent(C1s.Meta.ObjectType).AddSort(C1s.Meta.C1AllorsString);

            var user = new Users(this.DatabaseSession).GetCurrentUser();
            var acls = new AccessControlListCache(user);

            var csvEn = export.Write(extent, englishGreatBritain, acls);
            var csvNl = export.Write(extent, dutchBelgium, acls);

            Assert.AreNotEqual(csvEn, csvNl);
        }

        [Test]
        public void One2Many()
        {
            var dutchBelgium = new Locales(this.DatabaseSession).DutchBelgium;

            new C1Builder(this.DatabaseSession)
                .WithC1AllorsString("c1A")
                .WithC1C2One2Many(new C2Builder(this.DatabaseSession).WithC2AllorsString("c2A").Build())
                .Build();

            new C1Builder(this.DatabaseSession)
                .WithC1AllorsString("c1B")
                .WithC1C2One2Many(new C2Builder(this.DatabaseSession).WithC2AllorsString("c2B").Build())
                .WithC1C2One2Many(new C2Builder(this.DatabaseSession).WithC2AllorsString("c2C").Build())
                .Build();

            this.DatabaseSession.Derive(true);

            var export = new CsvExport("Test");
            export.Columns.Add(new CsvExportPath(C1s.Meta.C1AllorsString));
            export.Columns.Add(new CsvExportPath(new Path(C1s.Meta.C1C2One2Manies, C2s.Meta.C2AllorsString)));

            var extent = this.DatabaseSession.Extent(C1s.Meta.ObjectType).AddSort(C1s.Meta.C1AllorsString);

            var aclMock = new Mock<IAccessControlList>();
            aclMock.Setup(acl => acl.CanRead(It.IsAny<PropertyType>())).Returns(true);
            var acls = new AccessControlListCache(null, (allorsObject, user) => aclMock.Object);

            var csv = export.Write(extent, dutchBelgium, acls);

            Assert.AreEqual(
@"""C1AllorsString"";""C2AllorsString""
""c1A"";""c2A""
""c1B"";""c2B;c2C""", 
                    csv);
        }
    }
}