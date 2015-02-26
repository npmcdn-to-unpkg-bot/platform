//------------------------------------------------------------------------------------------------- 
// <copyright file="DiscountAdjustmentTests.cs" company="Allors bvba">
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
    public class DiscountAdjustmentTests : DomainTest
    {
        [Test]
        public void GivenDiscountAdjustment_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new DiscountAdjustmentBuilder(this.DatabaseSession);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback(); 
            
            builder.WithAmount(1);
            builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithPercentage(1);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenDiscountAdjustment_WhenDeriving_ThenDisplayNameIsSet()
        {
            throw new Exception("TODO");

            //var mechelen = new CityBuilder(this.DatabaseSession).WithName("Mechelen").Build();
            //var shipToContactMechanism = new PostalAddressBuilder(this.DatabaseSession).WithGeographicBoundary(mechelen).WithAddress1("Haverwerf 15").Build();

            //var customer = new OrganisationBuilder(this.DatabaseSession)
            //    .WithName("customer")
            //    .WithLocale(new Locales(this.DatabaseSession).EnglishGreatBritain)
            //    .WithPreferredCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
            //    .Build();

            //var internalOrganisation = new InternalOrganisations(this.DatabaseSession).FindBy(InternalOrganisations.Meta.Name, "internalOrganisation");

            //new CustomerRelationshipBuilder(this.DatabaseSession).WithCustomer(customer).WithInternalOrganisation(internalOrganisation).Build();

            //var amountAdjustment = new DiscountAdjustmentBuilder(this.DatabaseSession).WithAmount(1).Build();
            //var percentageAdjustment = new DiscountAdjustmentBuilder(this.DatabaseSession).WithPercentage(1).Build();

            //var order = new SalesOrderBuilder(this.DatabaseSession)
            //    .WithOrderNumber("1")
            //    .WithBillToCustomer(customer)
            //    .WithShipToCustomer(customer)
            //    .WithShipToAddress(shipToContactMechanism)
            //    .WithTakenByInternalOrganisation(internalOrganisation)
            //    .WithDiscountAdjustment(amountAdjustment)
            //    .Build();


            //this.DatabaseSession.Derive(true);

            //Assert.AreEqual("Discount: €1.00", amountAdjustment.DisplayName);

            //order.DiscountAdjustment = percentageAdjustment;

            //this.DatabaseSession.Derive(true);

            //Assert.AreEqual("Discount: 1%", percentageAdjustment.DisplayName);
        }
    }
}
