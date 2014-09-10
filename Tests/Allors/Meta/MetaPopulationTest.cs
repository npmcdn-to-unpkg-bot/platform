// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetaPopulationTest.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Meta.Static
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class MetaPopulationTest : AbstractTest
    {
        [Test]
        public void Inheritances()
        {
            var domain = this.Domain;
            var superdomain = new Domain(this.MetaPopulation, Guid.NewGuid());
            domain.AddDirectSuperdomain(superdomain);

            var c1 = new ClassBuilder(domain, Guid.NewGuid()).WithSingularName("c1").WithPluralName("c1s").Build();
            var c2 = new ClassBuilder(superdomain, Guid.NewGuid()).WithSingularName("c2").WithPluralName("c2s").Build();

            var i1 = new InterfaceBuilder(domain, Guid.NewGuid()).WithSingularName("i1").WithPluralName("i1s").Build();
            var i2 = new InterfaceBuilder(superdomain, Guid.NewGuid()).WithSingularName("i2").WithPluralName("i2s").Build();

            Assert.AreEqual(0, this.MetaPopulation.Inheritances.Count());

            new InheritanceBuilder(this.Domain, Guid.NewGuid()).WithSubtype(c1).WithSupertype(i1).Build();

            Assert.AreEqual(1, this.MetaPopulation.Inheritances.Count());

            new InheritanceBuilder(this.Domain, Guid.NewGuid()).WithSubtype(c2).WithSupertype(i2).Build();

            Assert.AreEqual(2, this.MetaPopulation.Inheritances.Count());

            new InheritanceBuilder(this.Domain, Guid.NewGuid()).WithSubtype(c1).WithSupertype(i2).Build();

            Assert.AreEqual(3, this.MetaPopulation.Inheritances.Count());
        }

        [Test]
        public void Composites()
        {
            var domain = this.Domain;
            var superdomain = new Domain(this.MetaPopulation, Guid.NewGuid());
            domain.AddDirectSuperdomain(superdomain);

            Assert.AreEqual(0, this.MetaPopulation.Composites.Count());

            var c1 = new ClassBuilder(domain, Guid.NewGuid()).WithSingularName("c1").WithPluralName("c1s").Build();

            Assert.AreEqual(1, this.MetaPopulation.Composites.Count());

            var c2 = new ClassBuilder(superdomain, Guid.NewGuid()).WithSingularName("c2").WithPluralName("c2s").Build();

            Assert.AreEqual(2, this.MetaPopulation.Composites.Count());

            var i1 = new InterfaceBuilder(domain, Guid.NewGuid()).WithSingularName("i1").WithPluralName("i1s").Build();

            Assert.AreEqual(3, this.MetaPopulation.Composites.Count());

            var i2 = new InterfaceBuilder(superdomain, Guid.NewGuid()).WithSingularName("i2").WithPluralName("i2s").Build();

            Assert.AreEqual(4, this.MetaPopulation.Composites.Count());
        }

        //[Test]
        //public void Find()
        //{
        //    this.Populate();

        //    var typeId = new Guid("89ff164f-ff6c-4b0d-916c-4e4507f97250");
        //    var type = this.Population.SuperDomain.AddDeclaredObjectType(typeId);
        //    Assert.AreEqual(type, this.Domain.MetaDomain.Find(typeId));

        //    var relationId = new Guid("2B03B8A9-6BE0-4809-A536-092DBE09032A");
        //    var relationType = this.Population.SuperDomain.AddDeclaredRelationType(relationId, Guid.NewGuid(), Guid.NewGuid());

        //    Assert.AreEqual(relationType, this.Domain.MetaDomain.Find(relationId));

        //    Assert.AreEqual(this.Population.SuperDomain, this.Domain.MetaDomain.Find(this.Population.SuperDomain.Id));

        //    var inheritanceId = this.Population.C1.FindInheritanceWhereDirectSubtype(this.Population.A1).Id;
        //    var inheritance = (MetaInheritance)this.Domain.MetaDomain.Find(inheritanceId);
        //    Assert.IsNotNull(inheritance);
        //    Assert.AreEqual(this.Population.C1, inheritance.Subtype);
        //    Assert.AreEqual(this.Population.A1, inheritance.Supertype);
        //}



    }

    public class MetaPopulationTestWithSuperDomains : MetaPopulationTest
    {
        protected override void Populate()
        {
            this.Population.PopulateWithSuperDomains();
        }
    }
}