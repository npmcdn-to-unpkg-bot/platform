//------------------------------------------------------------------------------------------------- 
// <copyright file="AssociationTest.cs" company="Allors bvba">
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
// <summary>Defines the AssociationTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.Static
{
    using System;
    using System.Collections.Generic;

    using Allors.Meta.Events;

    using NUnit.Framework;

    [TestFixture]
    public class AssociationTest : AbstractTest
    {
        private readonly List<MetaObjectChangedEventArgs> metaObjectChangedEvents = new List<MetaObjectChangedEventArgs>();

        [Test]
        public void Defaults()
        {
            this.Populate();

            var associationId = Guid.NewGuid();
            var relationType = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), associationId, Guid.NewGuid());

            var association = relationType.AssociationType;

            Assert.IsTrue(association.ExistId);
            Assert.AreEqual(associationId, association.Id);

            association.ObjectType = this.Population.C1;
            association.IsMany = !association.IsMany;
        }

        [Test]
        public void Immutable()
        {
            var relation1 = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var relation2 = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            try
            {
                relation2.AssociationType = relation1.AssociationType;
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

            var association = relationType.AssociationType;

            Assert.AreEqual(relationType.Id.ToString().ToLower(), association.SingularName);
            Assert.AreEqual(relationType.Id.ToString().ToLower(), association.PluralName);
        }

        [Test]
        public void PluralName()
        {
            var company = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            company.SingularName = "Company";
            company.PluralName = "Companies";

            var person = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            person.SingularName = "Person";

            var companyPerson = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            companyPerson.AssociationType.ObjectType = company;
            companyPerson.AssociationType.IsMany = true;
            companyPerson.RoleType.ObjectType = person;

            Assert.AreEqual("CompaniesWherePerson", companyPerson.AssociationType.PluralName);
        }
       
        [Test]
        public void SingularName()
        {
            var company = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            company.SingularName = "Company";

            var person = this.Domain.AddDeclaredObjectType(Guid.NewGuid());
            person.SingularName = "Person";

            var companyPerson = this.Domain.AddDeclaredRelationType(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            companyPerson.AssociationType.ObjectType = company;
            companyPerson.AssociationType.IsMany = false;
            companyPerson.RoleType.ObjectType = person;

            Assert.AreEqual("Company", companyPerson.AssociationType.SingularName);

            company.PluralName = "Companies";

            Assert.AreEqual("Company", companyPerson.AssociationType.SingularName);
        }

        private void DomainMetaObjectChanged(object sender, MetaObjectChangedEventArgs args)
        {
            this.metaObjectChangedEvents.Add(args);
        }
    }

    public class AssociationTestWithSuperDomains : AssociationTest
    {
        protected override void Populate()
        {
            this.Population.PopulateWithSuperDomains();
        }
    }
}