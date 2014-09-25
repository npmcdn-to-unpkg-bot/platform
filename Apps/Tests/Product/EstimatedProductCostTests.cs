//------------------------------------------------------------------------------------------------- 
// <copyright file="EstimatedProductCostTests.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the MediaTests type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System;

    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class EstimatedProductCostTests : DomainTest
    {
        [Test]
        public void GivenEstimatedLaborCost_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new EstimatedLaborCostBuilder(this.DatabaseSession);
            var laborCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCost(1);
            laborCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"));
            laborCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromDate(DateTime.Now);
            laborCost = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenEstimatedLaborCost_WhenDeriving_ThenDisplayNameIsSet()
        {
            var laborCost = new EstimatedLaborCostBuilder(this.DatabaseSession)
                .WithCost(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Laborcost: € 10.00", laborCost.DisplayName);

            laborCost.Description = "cheap labor";
            
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Laborcost: cheap labor € 10.00", laborCost.DisplayName);

            laborCost.GeographicBoundary = new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "NL");
            
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Laborcost: cheap labor € 10.00, with boundary Netherlands", laborCost.DisplayName);

            laborCost.Organisation = new OrganisationBuilder(this.DatabaseSession).WithName("Supplier x").WithLocale(new Locales(this.DatabaseSession).EnglishGreatBritain).Build();
            
            this.DatabaseSession.Derive(true);
            
            Assert.AreEqual("Laborcost: cheap labor € 10.00, with boundary Netherlands, for Supplier x", laborCost.DisplayName);
        }

        [Test]
        public void GivenEstimatedMaterialCost_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new EstimatedMaterialCostBuilder(this.DatabaseSession);
            var materialCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCost(1);
            materialCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"));
            materialCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromDate(DateTime.Now);
            materialCost = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenEstimatedmaterialCost_WhenDeriving_ThenDisplayNameIsSet()
        {
            var materialCost = new EstimatedMaterialCostBuilder(this.DatabaseSession)
                .WithCost(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Materialcost: € 10.00", materialCost.DisplayName);

            materialCost.Description = "cheap material";
            
            this.DatabaseSession.Derive(true);
            
            Assert.AreEqual("Materialcost: cheap material € 10.00", materialCost.DisplayName);

            materialCost.GeographicBoundary = new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "NL");
            
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Materialcost: cheap material € 10.00, with boundary Netherlands", materialCost.DisplayName);

            materialCost.Organisation = new OrganisationBuilder(this.DatabaseSession).WithName("Supplier x").WithLocale(new Locales(this.DatabaseSession).EnglishGreatBritain).Build();
            
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Materialcost: cheap material € 10.00, with boundary Netherlands, for Supplier x", materialCost.DisplayName);
        }

        [Test]
        public void GivenEstimatedOtherCost_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new EstimatedOtherCostBuilder(this.DatabaseSession);
            var otherCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCost(1);
            otherCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"));
            otherCost = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromDate(DateTime.Now);
            otherCost = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenEstimatedOtherCost_WhenDeriving_ThenDisplayNameIsSet()
        {
            var otherCost = new EstimatedOtherCostBuilder(this.DatabaseSession)
                .WithCost(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Othercost: € 10.00", otherCost.DisplayName);

            otherCost.Description = "cheap labor";

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Othercost: cheap labor € 10.00", otherCost.DisplayName);

            otherCost.GeographicBoundary = new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "NL");

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Othercost: cheap labor € 10.00, with boundary Netherlands", otherCost.DisplayName);

            otherCost.Organisation = new OrganisationBuilder(this.DatabaseSession).WithName("Supplier x").WithLocale(new Locales(this.DatabaseSession).EnglishGreatBritain).Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Othercost: cheap labor € 10.00, with boundary Netherlands, for Supplier x", otherCost.DisplayName);
        }
    }
}
