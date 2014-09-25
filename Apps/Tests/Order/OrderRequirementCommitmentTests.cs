//------------------------------------------------------------------------------------------------- 
// <copyright file="OrderRequirementCommitmentTests.cs" company="Allors bvba">
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
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class OrderRequirementCommitmentTests : DomainTest
    {
        [Test]
        public void GivenOrderRequirementCommitment_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("Gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            OrderItem goodOrderItem = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(good).WithQuantityOrdered(1).Build();
            var customerRequirement = new CustomerRequirementBuilder(this.DatabaseSession).WithDescription("100 gizmo's").Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var builder = new OrderRequirementCommitmentBuilder(this.DatabaseSession);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithOrderItem(goodOrderItem);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithRequirement(customerRequirement);
            builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithQuantity(10);
            builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenOrderRequirementCommitment_WhenDeriving_ThenDisplayNameIsSet()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithSku("10101")
                .WithName("Gizmo")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var mechelen = new CityBuilder(this.DatabaseSession).WithName("Mechelen").Build();
            var shipToContactMechanism = new PostalAddressBuilder(this.DatabaseSession).WithGeographicBoundary(mechelen).WithAddress1("Haverwerf 15").Build();

            var customer = new PersonBuilder(this.DatabaseSession).WithFirstName("Koen").Build();

            var internalOrganisation = new InternalOrganisations(this.DatabaseSession).FindBy(InternalOrganisations.Meta.Name, "internalOrganisation");

            new CustomerRelationshipBuilder(this.DatabaseSession).WithCustomer(customer).WithInternalOrganisation(internalOrganisation).Build();

            var order = new SalesOrderBuilder(this.DatabaseSession)
                .WithBillToCustomer(customer)
                .WithShipToCustomer(customer)
                .WithShipToAddress(shipToContactMechanism)
                .WithTakenByInternalOrganisation(internalOrganisation)
                .Build();

            var orderItem = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(good).WithQuantityOrdered(100).WithActualUnitPrice(10).Build();
            order.AddSalesOrderItem(orderItem);

            this.DatabaseSession.Derive(true);
    
            var customerRequirement = new CustomerRequirementBuilder(this.DatabaseSession).WithDescription("100 gizmo's").Build();

            var orderRequirementCommitment = new OrderRequirementCommitmentBuilder(this.DatabaseSession)
                .WithOrderItem(orderItem)
                .WithRequirement(customerRequirement)
                .WithQuantity(10)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("10 items from 100 Gizmo, SKU: 10101, Total: 1,000.00 are committed for requirement: 100 gizmo's", orderRequirementCommitment.DisplayName);
        }
    }
}