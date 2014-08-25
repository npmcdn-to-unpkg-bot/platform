//------------------------------------------------------------------------------------------------- 
// <copyright file="RoleTest.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the RoleTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.Static
{
    using System;
    using System.Collections.Generic;

    using Allors.Meta.Events;

    using NUnit.Framework;

    [TestFixture]
    public class RoleTest : AbstractTest
    {
        private readonly List<MetaObjectChangedEventArgs> metaObjectChangedEvents = new List<MetaObjectChangedEventArgs>();

        [Test]
        public void Defaults()
        {
            this.Populate();

            var roleId = Guid.NewGuid();
            var relationType = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), roleId);

            var role = relationType.RoleType;

            Assert.IsTrue(role.ExistId);
            Assert.AreEqual(roleId, role.Id);
        }

        [Test]
        public void Immutable()
        {
            var relation1 = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var relation2 = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            try
            {
                relation2.RoleType = relation1.RoleType;
                Assert.Fail();
            }
            catch
            {
            }
        }

        [Test]
        public void Name()
        {
            var relationType = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            var role = relationType.RoleType;

            role.IsMany = false;
            Assert.AreEqual(relationType.Id.ToString(), role.Name);
            role.IsMany = true;
            Assert.AreEqual(relationType.Id.ToString(), role.Name);
        }

        [Test]
        public void PluralName()
        {
            var company = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            company.SingularName = "Company";
            company.PluralName = "Companies";

            var person = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            person.SingularName = "Person";
            person.PluralName = "Persons";

            var companyPerson = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            companyPerson.AssociationType.ObjectType = company;
            companyPerson.RoleType.ObjectType = person;
            companyPerson.RoleType.IsMany = true;

            Assert.AreEqual("Persons", companyPerson.RoleType.PluralName);

            companyPerson.RoleType.AssignedSingularName = "Persoon";

            Assert.AreEqual("Persons", companyPerson.RoleType.PluralName);

            companyPerson.RoleType.AssignedPluralName = "Personen";

            Assert.AreEqual("Personen", companyPerson.RoleType.PluralName);

            person.PluralName = null;
            Assert.AreEqual("Personen", companyPerson.RoleType.PluralName);
        }

        [Test]
        public void SingularName()
        {
            var company = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            company.SingularName = "Company";
            company.PluralName = "Companies";

            var person = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            person.SingularName = "Person";
            person.PluralName = "Persons";

            var companyPerson = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            companyPerson.AssociationType.ObjectType = company;
            companyPerson.RoleType.ObjectType = person;

            Assert.AreEqual("Person", companyPerson.RoleType.SingularName);

            person.PluralName = "Persons";
            Assert.AreEqual("Person", companyPerson.RoleType.SingularName);

            companyPerson.RoleType.AssignedPluralName = "Personen";
            Assert.AreEqual("Person", companyPerson.RoleType.SingularName);

            companyPerson.RoleType.AssignedSingularName = "Persoon";
            Assert.AreEqual("Persoon", companyPerson.RoleType.SingularName);
        }

        [Test]
        public void DeriveSize()
        {
            this.Populate();

            var relationType = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            relationType.AssociationType.ObjectType = this.Population.C1;
            relationType.RoleType.ObjectType = this.Population.IntegerType;

            Assert.IsFalse(relationType.RoleType.ExistSize);

            relationType.RoleType.ObjectType = this.Population.StringType;

            Assert.IsTrue(relationType.RoleType.ExistSize);
            Assert.AreEqual(256, relationType.RoleType.Size);

            relationType.RoleType.ObjectType = this.Population.IntegerType;

            Assert.IsFalse(relationType.RoleType.ExistSize);
        }
        
        [Test]
        public void DerivePrecisionScale()
        {
            this.Populate();

            var relationType = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            relationType.AssociationType.ObjectType = this.Population.C1;
            relationType.RoleType.ObjectType = this.Population.IntegerType;

            Assert.IsFalse(relationType.RoleType.ExistPrecision);
            Assert.IsFalse(relationType.RoleType.ExistScale);

            relationType.RoleType.ObjectType = this.Population.DecimalType;

            Assert.IsTrue(relationType.RoleType.ExistPrecision);
            Assert.AreEqual(19, relationType.RoleType.Precision);
            Assert.IsTrue(relationType.RoleType.ExistScale);
            Assert.AreEqual(2, relationType.RoleType.Scale);

            relationType.RoleType.ObjectType = this.Population.IntegerType;

            Assert.IsFalse(relationType.RoleType.ExistPrecision);
            Assert.IsFalse(relationType.RoleType.ExistScale);
        }
        

        private void DomainMetaObjectChanged(object sender, MetaObjectChangedEventArgs args)
        {
            this.metaObjectChangedEvents.Add(args);
        }
    }

    public class RoleTestWithSuperDomains : RoleTest
    {
        protected override void Populate()
        {
            this.Population.PopulateWithSuperDomains();
        }
    }
}