//------------------------------------------------------------------------------------------------- 
// <copyright file="SalesOrderItemPricingTests.cs" company="Allors bvba">
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

    
    

    using NUnit.Framework;
    using Allors.Domain;

    [TestFixture]
    public class SalesOrderItemPricingTests : DomainTest
    {
        private ProductCategory productCategory;
        private ProductCategory ancestorProductCategory;
        private ProductCategory parentProductCategory;
        private Good good;
        private Good variantGood;
        private Good variantGood2;
        private Good virtualGood;
        private FinishedGood part;
        private Colour feature1;
        private Colour feature2;
        private InternalOrganisation internalOrganisation;
        private Organisation shipToCustomer;
        private Organisation billToCustomer;
        private PartyRevenueHistory partyRevenueHistory;
        private PartyProductCategoryRevenueHistory productCategoryRevenueHistory;
        private PartyProductCategoryRevenueHistory ancestorProductCategoryRevenueHistory;
        private PartyProductCategoryRevenueHistory parentProductCategoryRevenueHistory;
        private Organisation supplier;
        private City kiev;
        private PostalAddress shipToContactMechanismMechelen;
        private PostalAddress shipToContactMechanismKiev;
        private BasePrice currentBasePriceGeoBoundary;
        private BasePrice currentGoodBasePrice;
        private BasePrice currentVirtualGoodBasePrice;
        private BasePrice currentGood1Feature1BasePrice;
        private BasePrice currentFeature2BasePrice;
        private ProductPurchasePrice goodPurchasePrice;
        private ProductPurchasePrice virtualGoodPurchasePrice;
        private SupplierOffering goodSupplierOffering;
        private SupplierOffering virtualgoodSupplierOffering;
        private SalesOrder order;
        private VatRate vatRate21;

        [SetUp]
        public override void Init()
        {
            base.Init();

            var euro = new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR");

            this.internalOrganisation = new InternalOrganisations(this.DatabaseSession).FindBy(InternalOrganisations.Meta.Name, "internalOrganisation");
            this.internalOrganisation.PreferredCurrency = euro;

            this.supplier = new OrganisationBuilder(this.DatabaseSession).WithName("supplier").Build();

            this.vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();

            var mechelen = new CityBuilder(this.DatabaseSession).WithName("Mechelen").Build();
            this.kiev = new CityBuilder(this.DatabaseSession).WithName("Kiev").Build();

            this.shipToContactMechanismMechelen = new PostalAddressBuilder(this.DatabaseSession).WithGeographicBoundary(mechelen).WithAddress1("Haverwerf 15").Build();
            this.shipToContactMechanismKiev = new PostalAddressBuilder(this.DatabaseSession).WithGeographicBoundary(this.kiev).WithAddress1("Dnieper").Build();
            this.shipToCustomer = new OrganisationBuilder(this.DatabaseSession).WithName("shipToCustomer").Build();
            this.shipToCustomer.AddPartyContactMechanism(new PartyContactMechanismBuilder(this.DatabaseSession)
                                                            .WithContactMechanism(this.shipToContactMechanismKiev)
                                                            .WithContactPurpose(new ContactMechanismPurposes(this.DatabaseSession).ShippingAddress)
                                                            .WithUseAsDefault(true)
                                                            .Build());
            this.DatabaseSession.Derive(true);

            this.billToCustomer = new OrganisationBuilder(this.DatabaseSession)
                .WithName("billToCustomer")
                .WithPreferredCurrency(euro)
                .Build();

            new CustomerRelationshipBuilder(this.DatabaseSession).WithCustomer(billToCustomer).WithInternalOrganisation(internalOrganisation).Build();
            new CustomerRelationshipBuilder(this.DatabaseSession).WithCustomer(shipToCustomer).WithInternalOrganisation(internalOrganisation).Build();

            this.part = new FinishedGoodBuilder(this.DatabaseSession).WithName("part").Build();

            this.ancestorProductCategory = new ProductCategoryBuilder(this.DatabaseSession).WithDescription("ancestor").Build();
            this.parentProductCategory = new ProductCategoryBuilder(this.DatabaseSession).WithDescription("parent").WithParent(this.ancestorProductCategory).Build();
            this.productCategory = new ProductCategoryBuilder(this.DatabaseSession).WithDescription("gizmo").Build();
            this.productCategory.AddParent(this.parentProductCategory);

            this.good = new GoodBuilder(this.DatabaseSession)
                .WithSku("10101")
                .WithVatRate(this.vatRate21)
                .WithName("good")
                .WithProductCategory(this.productCategory)
                .WithFinishedGood(this.part)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            this.partyRevenueHistory = new PartyRevenueHistoryBuilder(this.DatabaseSession)
                .WithCurrency(euro)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithRevenue(100M)
                .Build();

            this.productCategoryRevenueHistory = new PartyProductCategoryRevenueHistoryBuilder(this.DatabaseSession)
                .WithCurrency(euro)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithProductCategory(this.productCategory)
                .WithRevenue(100M)
                .WithQuantity(10)
                .Build();

            this.parentProductCategoryRevenueHistory = new PartyProductCategoryRevenueHistoryBuilder(this.DatabaseSession)
                .WithCurrency(euro)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithProductCategory(this.parentProductCategory)
                .WithRevenue(100M)
                .WithQuantity(10)
                .Build();

            this.ancestorProductCategoryRevenueHistory = new PartyProductCategoryRevenueHistoryBuilder(this.DatabaseSession)
                .WithCurrency(euro)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithProductCategory(this.ancestorProductCategory)
                .WithRevenue(100M)
                .WithQuantity(10)
                .Build();

            this.variantGood = new GoodBuilder(this.DatabaseSession)
                .WithSku("v10101")
                .WithVatRate(this.vatRate21)
                .WithName("variant good")
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .Build();

            this.variantGood2 = new GoodBuilder(this.DatabaseSession)
                .WithSku("v10102")
                .WithVatRate(this.vatRate21)
                .WithName("variant good2")
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .Build();

            this.virtualGood = new GoodBuilder(this.DatabaseSession)
                .WithVatRate(this.vatRate21)
                .WithName("virtual good")
                .WithVariant(this.variantGood)
                .WithVariant(this.variantGood2)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .Build();

            this.goodPurchasePrice = new ProductPurchasePriceBuilder(this.DatabaseSession)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .WithPrice(7)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            this.goodSupplierOffering = new SupplierOfferingBuilder(this.DatabaseSession)
                .WithProduct(this.good)
                .WithSupplier(this.supplier)
                .WithFromDate(DateTime.Now)
                .WithProductPurchasePrice(this.goodPurchasePrice)
                .Build();

            this.virtualGoodPurchasePrice = new ProductPurchasePriceBuilder(this.DatabaseSession)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .WithPrice(8)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            this.virtualgoodSupplierOffering = new SupplierOfferingBuilder(this.DatabaseSession)
                .WithProduct(this.virtualGood)
                .WithSupplier(this.supplier)
                .WithFromDate(DateTime.Now)
                .WithProductPurchasePrice(this.virtualGoodPurchasePrice)
                .Build();

            this.feature1 = new ColourBuilder(this.DatabaseSession)
                .WithVatRate(this.vatRate21)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("white")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.feature2 = new ColourBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("black")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.currentBasePriceGeoBoundary = new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current BasePriceGeoBoundary ")
                .WithGeographicBoundary(mechelen)
                .WithProduct(this.good)
                .WithPrice(8)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .Build();

            // previous basePrice for good
            new BasePriceBuilder(this.DatabaseSession).WithDescription("previous good")
                .WithSpecifiedFor(this.internalOrganisation)
                .WithProduct(this.good)
                .WithPrice(8)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(-1))
                .WithThroughDate(DateTime.Now.AddDays(-1))
                .Build();

            // future basePrice for good
            new BasePriceBuilder(this.DatabaseSession).WithDescription("future good")
                .WithSpecifiedFor(this.internalOrganisation)
                .WithProduct(this.good)
                .WithPrice(11)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(1))
                .Build();

            this.currentGoodBasePrice = new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current good")
                .WithProduct(this.good)
                .WithPrice(10)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            // previous basePrice for feature1
            new BasePriceBuilder(this.DatabaseSession).WithDescription("previous feature1")
                .WithSpecifiedFor(this.internalOrganisation)
                .WithProductFeature(this.feature1)
                .WithPrice(0.5M)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(-1))
                .WithThroughDate(DateTime.Now.AddDays(-1))
                .Build();

            // future basePrice for feature1
            new BasePriceBuilder(this.DatabaseSession).WithDescription("future feature1")
                .WithSpecifiedFor(this.internalOrganisation)
                .WithProductFeature(this.feature1)
                .WithPrice(2.5M)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(1))
                .Build();

            new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current feature1")
                .WithProductFeature(this.feature1)
                .WithPrice(2)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            // previous basePrice for feature2
            new BasePriceBuilder(this.DatabaseSession).WithDescription("previous feature2")
                .WithSpecifiedFor(this.internalOrganisation)
                .WithProductFeature(this.feature2)
                .WithPrice(2)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(-1))
                .WithThroughDate(DateTime.Now.AddDays(-1))
                .Build();

            // future basePrice for feature2
            new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("future feature2")
                .WithProductFeature(this.feature2)
                .WithPrice(4)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(1))
                .Build();

            this.currentFeature2BasePrice = new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current feature2")
                .WithProductFeature(this.feature2)
                .WithPrice(3)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            // previous basePrice for good with feature1
            new BasePriceBuilder(this.DatabaseSession).WithDescription("previous good/feature1")
                .WithSpecifiedFor(this.internalOrganisation)
                .WithProduct(this.good)
                .WithProductFeature(this.feature1)
                .WithPrice(4)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(-1))
                .WithThroughDate(DateTime.Now.AddDays(-1))
                .Build();

            // future basePrice for good with feature1
            new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("future good/feature1")
                .WithProduct(this.good)
                .WithProductFeature(this.feature1)
                .WithPrice(6)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now.AddYears(1))
                .Build();

            this.currentGood1Feature1BasePrice = new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current good/feature1")
                .WithProduct(this.good)
                .WithProductFeature(this.feature1)
                .WithPrice(5)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.currentVirtualGoodBasePrice = new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current virtual good")
                .WithProduct(this.virtualGood)
                .WithPrice(10)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .Build();

            new BasePriceBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("current variant good2")
                .WithProduct(this.variantGood2)
                .WithPrice(11)
                .WithCurrency(euro)
                .WithFromDate(DateTime.Now)
                .Build();

            this.order = new SalesOrderBuilder(this.DatabaseSession)
                .WithShipToCustomer(this.shipToCustomer)
                .WithBillToCustomer(this.billToCustomer)
                .WithTakenByInternalOrganisation(this.internalOrganisation)
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();
        }

        ////[Test]
        ////public void GivenOrderItemWithBasePriceForPartyRevenueValueBreak_WhenDeriving_ThenUseBasePriceForRevenueValueBreak()
        ////{
        ////    const decimal QuantityOrdered1 = 3;
        ////    const decimal Amount1 = 1;
        ////    const decimal Amount2 = 3;

        ////    var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
        ////    var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue break 1")
        ////        .WithRevenueValueBreak(break1)
        ////        .WithProduct(this.good)
        ////        .WithPrice(Amount1)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue break 2")
        ////        .WithRevenueValueBreak(break2)
        ////        .WithProduct(this.good)
        ////        .WithPrice(Amount2)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    this.DatabaseSession.Commit();

        ////    foreach (var session in this.Sessions)
        ////    {
        ////        session.Commit();

        ////        this.InstantiateObjects(this.DatabaseSession);

        ////        this.partyRevenueHistory.Revenue = 20M;

        ////        var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
        ////        this.order.AddSalesOrderItem(item1);
        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);

        ////        this.partyRevenueHistory.Revenue = 50M;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount1, item1.UnitBasePrice);

        ////        this.partyRevenueHistory.Revenue = 110M;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount2, item1.UnitBasePrice);

        ////        if (session is IWorkspaceSession)
        ////        {
        ////            session.DatabaseSession.Rollback();
        ////            Assert.AreEqual(0, ((IWorkspaceSession)session).Conflicts.Length);
        ////        }

        ////        session.Rollback();
        ////    }
        ////}

        ////[Test]
        ////public void GivenOrderItemWithBasePriceForPartyProductCategoryRevenueValueBreak_WhenDeriving_ThenUseBasePriceForProductCategoryRevenueValueBreak()
        ////{
        ////    const decimal QuantityOrdered1 = 3;
        ////    const decimal Amount1 = 1;
        ////    const decimal Amount2 = 3;

        ////    var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).WithProductCategory(this.productCategory).Build();
        ////    var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).WithProductCategory(this.productCategory).Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue break 1")
        ////        .WithRevenueValueBreak(break1)
        ////        .WithProductCategory(this.productCategory)
        ////        .WithPrice(Amount1)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue break 2")
        ////        .WithRevenueValueBreak(break2)
        ////        .WithProductCategory(this.productCategory)
        ////        .WithPrice(Amount2)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    this.DatabaseSession.Commit();

        ////    foreach (var session in this.Sessions)
        ////    {
        ////        session.Commit();

        ////        this.InstantiateObjects(this.DatabaseSession);

        ////        this.productCategoryRevenueHistory.Revenue = 20M;

        ////        var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
        ////        this.order.AddSalesOrderItem(item1);
        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);

        ////        this.productCategoryRevenueHistory.Revenue = 50M;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount1, item1.UnitBasePrice);

        ////        this.productCategoryRevenueHistory.Revenue = 110M;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount2, item1.UnitBasePrice);

        ////        if (session is IWorkspaceSession)
        ////        {
        ////            session.DatabaseSession.Rollback();
        ////            Assert.AreEqual(0, ((IWorkspaceSession)session).Conflicts.Length);
        ////        }

        ////        session.Rollback();
        ////    }
        ////}

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyRevenueValueBreak_WhenDeriving_ThenUseDiscountComponentsForRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueValueBreak(break1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueValueBreak(break2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.partyRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithEqualDiscountForBothPartyProductCategoryRevenueValueBreakAndRevenueQuantityBreak_WhenDeriving_ThenCalculateDiscountOnlyOnce()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var valueBreak1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var valueBreak2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            var quantityBreak1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).WithThrough(9).Build();
            var quantityBreak2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(10).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue value break 1")
                .WithRevenueValueBreak(valueBreak1)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue value break 2")
                .WithRevenueValueBreak(valueBreak2)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue quantity break 1")
                .WithRevenueQuantityBreak(quantityBreak1)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue quantity break 2")
                .WithRevenueQuantityBreak(quantityBreak2)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Revenue = 20M;
            this.productCategoryRevenueHistory.Quantity = 1;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 50M;
            this.productCategoryRevenueHistory.Quantity = 3;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 110M;
            this.productCategoryRevenueHistory.Quantity = 10;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithBetterDiscountForRevenueValueBreakThenForRevenueQuantityBreak_WhenDeriving_ThenUseDiscountForRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal ValueDiscount1 = 4;
            const decimal ValueDiscount2 = 5;
            const decimal QuantityDiscount1 = 1;
            const decimal QuantityDiscount2 = 3;

            var valueBreak1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var valueBreak2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            var quantityBreak1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).WithThrough(9).Build();
            var quantityBreak2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(10).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue value break 1")
                .WithRevenueValueBreak(valueBreak1)
                .WithProductCategory(this.productCategory)
                .WithPrice(ValueDiscount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue value break 2")
                .WithRevenueValueBreak(valueBreak2)
                .WithProductCategory(this.productCategory)
                .WithPrice(ValueDiscount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue quantity break 1")
                .WithRevenueQuantityBreak(quantityBreak1)
                .WithProductCategory(this.productCategory)
                .WithPrice(QuantityDiscount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue quantity break 2")
                .WithRevenueQuantityBreak(quantityBreak2)
                .WithProductCategory(this.productCategory)
                .WithPrice(QuantityDiscount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Revenue = 20M;
            this.productCategoryRevenueHistory.Quantity = 1;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 50M;
            this.productCategoryRevenueHistory.Quantity = 3;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(ValueDiscount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - ValueDiscount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 110M;
            this.productCategoryRevenueHistory.Quantity = 10;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(ValueDiscount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - ValueDiscount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithBetterDiscountForRevenueQuantityBreakThenForRevenueValueBreak_WhenDeriving_ThenUseDiscountForRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal ValueDiscount1 = 1;
            const decimal ValueDiscount2 = 3;
            const decimal QuantityDiscount1 = 4;
            const decimal QuantityDiscount2 = 5;

            var valueBreak1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var valueBreak2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            var quantityBreak1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).WithThrough(9).Build();
            var quantityBreak2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(10).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue value break 1")
                .WithRevenueValueBreak(valueBreak1)
                .WithProductCategory(this.productCategory)
                .WithPrice(ValueDiscount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue value break 2")
                .WithRevenueValueBreak(valueBreak2)
                .WithProductCategory(this.productCategory)
                .WithPrice(ValueDiscount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue quantity break 1")
                .WithRevenueQuantityBreak(quantityBreak1)
                .WithProductCategory(this.productCategory)
                .WithPrice(QuantityDiscount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue quantity break 2")
                .WithRevenueQuantityBreak(quantityBreak2)
                .WithProductCategory(this.productCategory)
                .WithPrice(QuantityDiscount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Revenue = 20M;
            this.productCategoryRevenueHistory.Quantity = 1;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 50M;
            this.productCategoryRevenueHistory.Quantity = 3;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(QuantityDiscount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - QuantityDiscount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 110M;
            this.productCategoryRevenueHistory.Quantity = 10;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(QuantityDiscount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - QuantityDiscount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyProductAncestorCategoryRevenueValueBreak_WhenDeriving_ThenUseDiscountComponentsForAncestorRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueValueBreak(break1)
                .WithProductCategory(this.ancestorProductCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueValueBreak(break2)
                .WithProductCategory(this.ancestorProductCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.ancestorProductCategoryRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.ancestorProductCategoryRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.ancestorProductCategoryRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyProductParentCategoryRevenueValueBreak_WhenDeriving_ThenUseDiscountComponentsForParentRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueValueBreak(break1)
                .WithProductCategory(this.parentProductCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueValueBreak(break2)
                .WithProductCategory(this.parentProductCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.parentProductCategoryRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.parentProductCategoryRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.parentProductCategoryRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyProductCategoryRevenueValueBreak_WhenDeriving_ThenUseDiscountComponentsForRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueValueBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueValueBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForPartyRevenueValueBreak_WhenDeriving_ThenUseDiscountComponentsForPartyRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithRevenueValueBreak(break1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithRevenueValueBreak(break2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.partyRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForPartyProductCategoryRevenueValueBreak_WhenDeriving_ThenUseDiscountComponentsForPartyProductCategoryRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithRevenueValueBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithRevenueValueBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForPartyRevenueValueBreak_WhenDeriving_ThenUseSurchargeComponentsForPartyRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueValueBreak(break1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueValueBreak(break2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.partyRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForPartyProductCategoryRevenueValueBreak_WhenDeriving_ThenUseSurchargeComponentsForRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueValueBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueValueBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForPartyRevenueValueBreak_WhenDeriving_ThenUseSurchargeComponentsForPartyRevenueValueBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new RevenueValueBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithRevenueValueBreak(break1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithRevenueValueBreak(break2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.DatabaseSession.Derive(true);

            this.InstantiateObjects(this.DatabaseSession);

            this.partyRevenueHistory.Revenue = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.partyRevenueHistory.Revenue = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        ////[Test]
        ////public void GivenOrderItemWithBasePriceForPartyProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseBasePriceForProductCategoryRevenueQuantityBreak()
        ////{
        ////    const decimal QuantityOrdered1 = 3;
        ////    const decimal Amount1 = 1;
        ////    const decimal Amount2 = 3;

        ////    var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(50).WithThrough(99).WithProductCategory(this.productCategory).Build();
        ////    var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(100).WithProductCategory(this.productCategory).Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue break 1")
        ////        .WithRevenueQuantityBreak(break1)
        ////        .WithProductCategory(this.productCategory)
        ////        .WithPrice(Amount1)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue break 2")
        ////        .WithRevenueQuantityBreak(break2)
        ////        .WithProductCategory(this.productCategory)
        ////        .WithPrice(Amount2)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    this.DatabaseSession.Commit();

        ////    foreach (var session in this.Sessions)
        ////    {
        ////        session.Commit();

        ////        this.InstantiateObjects(this.DatabaseSession);

        ////        this.productCategoryRevenueHistory.Quantity = 20M;

        ////        var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
        ////        this.order.AddSalesOrderItem(item1);
        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);

        ////        this.productCategoryRevenueHistory.Quantity = 50M;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount1, item1.UnitBasePrice);

        ////        this.productCategoryRevenueHistory.Quantity = 110M;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount2, item1.UnitBasePrice);

        ////        if (session is IWorkspaceSession)
        ////        {
        ////            session.DatabaseSession.Rollback();
        ////            Assert.AreEqual(0, ((IWorkspaceSession)session).Conflicts.Length);
        ////        }

        ////        session.Rollback();
        ////    }
        ////}

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyAncestorProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForAncestorRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(5).WithThrough(9).Build();
            var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(10).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueQuantityBreak(break1)
                .WithProductCategory(this.ancestorProductCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueQuantityBreak(break2)
                .WithProductCategory(this.ancestorProductCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.ancestorProductCategoryRevenueHistory.Quantity = 2;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.ancestorProductCategoryRevenueHistory.Quantity = 5;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.ancestorProductCategoryRevenueHistory.Quantity = 11M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyParentProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForParentRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(5).WithThrough(9).Build();
            var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(10).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueQuantityBreak(break1)
                .WithProductCategory(this.parentProductCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueQuantityBreak(break2)
                .WithProductCategory(this.parentProductCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.parentProductCategoryRevenueHistory.Quantity = 2;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.parentProductCategoryRevenueHistory.Quantity = 5;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.parentProductCategoryRevenueHistory.Quantity = 11;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(50).WithThrough(99).Build();
            var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueQuantityBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueQuantityBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Quantity = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForPartyProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForPartyProductCategoryRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(50).WithThrough(99).Build();
            var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithRevenueQuantityBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithRevenueQuantityBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Quantity = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForPartyProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseSurchargeComponentsForRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(50).WithThrough(99).Build();
            var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 1")
                .WithRevenueQuantityBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue break 2")
                .WithRevenueQuantityBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Quantity = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForPartyProductCategoryRevenueQuantityBreak_WhenDeriving_ThenUseSurchargeComponentsForPartyProductCategoryRevenueQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(50).WithThrough(99).Build();
            var break2 = new RevenueQuantityBreakBuilder(this.DatabaseSession).WithFrom(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithRevenueQuantityBreak(break1)
                .WithProductCategory(this.productCategory)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithRevenueQuantityBreak(break2)
                .WithProductCategory(this.productCategory)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.productCategoryRevenueHistory.Quantity = 20M;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 50M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            this.productCategoryRevenueHistory.Quantity = 110M;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitSurcharge);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        ////[Test]
        ////public void GivenOrderItemWithBasePriceForPartyPackageQuantityBreak_WhenDeriving_ThenUseBasePriceForPackageQuantityBreak()
        ////{
        ////    const decimal QuantityOrdered1 = 3;
        ////    const decimal Amount1 = 9;
        ////    const decimal Amount2 = 8;

        ////    var break1 = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).WithThrough(2).Build();
        ////    var break2 = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(3).WithThrough(3).Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue in 2 packages")
        ////        .WithPackageQuantityBreak(break1)
        ////        .WithProduct(this.good)
        ////        .WithPrice(Amount1)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    new BasePriceBuilder(this.DatabaseSession)
        ////        .WithSpecifiedFor(this.internalOrganisation)
        ////        .WithDescription("baseprice good for revenue in 3 packages")
        ////        .WithPackageQuantityBreak(break2)
        ////        .WithProduct(this.good)
        ////        .WithPrice(Amount2)
        ////        .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
        ////        .WithFromDate(DateTime.Now)
        ////        .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
        ////        .Build();

        ////    new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
        ////        .WithInternalOrganisation(this.internalOrganisation)
        ////        .WithParty(this.billToCustomer)
        ////        .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package1").Build())
        ////        .WithRevenue(100M)
        ////        .Build();

        ////    var package2RevenueHistory = new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
        ////        .WithInternalOrganisation(this.internalOrganisation)
        ////        .WithParty(this.billToCustomer)
        ////        .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package2").Build())
        ////        .WithRevenue(100M)
        ////        .Build();

        ////    var package3RevenueHistory = new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
        ////        .WithInternalOrganisation(this.internalOrganisation)
        ////        .WithParty(this.billToCustomer)
        ////        .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package3").Build())
        ////        .WithRevenue(100M)
        ////        .Build(); 

        ////    this.DatabaseSession.Commit();

        ////    foreach (var session in this.Sessions)
        ////    {
        ////        session.Commit();

        ////        this.InstantiateObjects(this.DatabaseSession);
        ////        package2RevenueHistory = (PartyPackageRevenueHistory)session.Instantiate(package2RevenueHistory);
        ////        package3RevenueHistory = (PartyPackageRevenueHistory)session.Instantiate(package3RevenueHistory);

        ////        package2RevenueHistory.Revenue = 0;
        ////        package3RevenueHistory.Revenue = 0;

        ////        var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
        ////        this.order.AddSalesOrderItem(item1);
        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);

        ////        package2RevenueHistory.Revenue = 100M;
        ////        package3RevenueHistory.Revenue = 0;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount1, item1.UnitBasePrice);

        ////        package2RevenueHistory.Revenue = 100;
        ////        package3RevenueHistory.Revenue = 100;

        ////        this.order.Derive(new DefaultDerivationLog());

        ////        Assert.AreEqual(Amount2, item1.UnitBasePrice);

        ////        if (session is IWorkspaceSession)
        ////        {
        ////            session.DatabaseSession.Rollback();
        ////            Assert.AreEqual(0, ((IWorkspaceSession)session).Conflicts.Length);
        ////        }

        ////        session.Rollback();
        ////    }
        ////}

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyPackageQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForPackageQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).WithThrough(2).Build();
            var break2 = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(3).WithThrough(3).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue in 2 packages")
                .WithPackageQuantityBreak(break1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue in 3 packages")
                .WithPackageQuantityBreak(break2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package1").Build())
                .WithRevenue(100M)
                .Build();

            var package2RevenueHistory = new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package2").Build())
                .WithRevenue(100M)
                .Build();

            var package3RevenueHistory = new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package3").Build())
                .WithRevenue(100M)
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);
            package2RevenueHistory = (PartyPackageRevenueHistory)this.DatabaseSession.Instantiate(package2RevenueHistory);
            package3RevenueHistory = (PartyPackageRevenueHistory)this.DatabaseSession.Instantiate(package3RevenueHistory);

            package2RevenueHistory.Revenue = 0;
            package3RevenueHistory.Revenue = 0;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            package2RevenueHistory.Revenue = 100;
            package3RevenueHistory.Revenue = 0;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            package2RevenueHistory.Revenue = 100;
            package3RevenueHistory.Revenue = 100;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForPartyPackageQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForPartyPackageQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).WithThrough(2).Build();
            var break2 = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(3).WithThrough(3).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue in 2 packages")
                .WithPackageQuantityBreak(break1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for revenue in 3 packages")
                .WithPackageQuantityBreak(break2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package1").Build())
                .WithRevenue(100M)
                .Build();

            var package2RevenueHistory = new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package2").Build())
                .WithRevenue(100M)
                .Build();

            var package3RevenueHistory = new PartyPackageRevenueHistoryBuilder(this.DatabaseSession)
                .WithInternalOrganisation(this.internalOrganisation)
                .WithParty(this.billToCustomer)
                .WithPackage(new PackageBuilder(this.DatabaseSession).WithName("package3").Build())
                .WithRevenue(100M)
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);
            package2RevenueHistory = (PartyPackageRevenueHistory)this.DatabaseSession.Instantiate(package2RevenueHistory);
            package3RevenueHistory = (PartyPackageRevenueHistory)this.DatabaseSession.Instantiate(package3RevenueHistory);

            package2RevenueHistory.Revenue = 0;
            package3RevenueHistory.Revenue = 0;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            package2RevenueHistory.Revenue = 100;
            package3RevenueHistory.Revenue = 0;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            package2RevenueHistory.Revenue = 100;
            package3RevenueHistory.Revenue = 100;

            // Only for test puposes: Force Order derivation, because of changed revenue data
            this.order.Derive().WithDerivation(new Derivation(this.DatabaseSession)).Execute();

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItem_WhenBuild_ThenPostBuildRelationsMustExist()
        {
            this.InstantiateObjects(this.DatabaseSession);

            var item = new SalesOrderItemBuilder(this.DatabaseSession).Build();

            Assert.AreEqual(new SalesOrderItemObjectStates(this.DatabaseSession).Created, item.CurrentObjectState);
        }

        [Test]
        public void GivenOrderItemForGood1WithRequiredMarkupPercentage_WhenDerivingPrices_ThenCalculateActualUnitPrice()
        {
            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(3).WithRequiredMarkupPercentage(45).Build();
            this.order.AddSalesOrderItem(item1);
            
            this.DatabaseSession.Derive(true);

            Assert.AreEqual(10.15M, item1.ActualUnitPrice);
            Assert.AreEqual(10M, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(10.15M, item1.CalculatedUnitPrice);
            Assert.AreEqual(2.13, item1.UnitVat);
            Assert.AreEqual(30.45M, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(30.45M, item1.TotalExVat);
            Assert.AreEqual(6.39M, item1.TotalVat);
            Assert.AreEqual(36.84M, item1.TotalIncVat);
            Assert.AreEqual(7, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(30.45M, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(30.45M, this.order.TotalExVat);
            Assert.AreEqual(6.39M, this.order.TotalVat);
            Assert.AreEqual(36.84M, this.order.TotalIncVat);
            Assert.AreEqual(10.15M, this.order.TotalListPrice);
            Assert.AreEqual(7, this.order.TotalPurchasePrice);
        }

        [Test]
        public void GivenOrderItemForGood1WithRequiredProfitMargin_WhenDerivingPrices_ThenCalculateActualUnitPrice()
        {
            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(3).WithRequiredProfitMargin(25).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(9.33M, item1.ActualUnitPrice);
            Assert.AreEqual(10M, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(9.33M, item1.CalculatedUnitPrice);
            Assert.AreEqual(1.96M, item1.UnitVat);
            Assert.AreEqual(27.99M, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(27.99M, item1.TotalExVat);
            Assert.AreEqual(5.88M, item1.TotalVat);
            Assert.AreEqual(33.87M, item1.TotalIncVat);
            Assert.AreEqual(7, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(27.99M, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(27.99M, this.order.TotalExVat);
            Assert.AreEqual(5.88M, this.order.TotalVat);
            Assert.AreEqual(33.87M, this.order.TotalIncVat);
            Assert.AreEqual(9.33M, this.order.TotalListPrice);
            Assert.AreEqual(7, this.order.TotalPurchasePrice);
        }

        [Test]
        public void GivenOrderItemForGood1WithActualPrice_WhenDerivingPrices_ThenUseActualPrice()
        {
            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(3).WithActualUnitPrice(15).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(10, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(15, item1.CalculatedUnitPrice);
            Assert.AreEqual(3.15, item1.UnitVat);
            Assert.AreEqual(45, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(45, item1.TotalExVat);
            Assert.AreEqual(9.45, item1.TotalVat);
            Assert.AreEqual(54.45, item1.TotalIncVat);
            Assert.AreEqual(7, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(45, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(45, this.order.TotalExVat);
            Assert.AreEqual(9.45, this.order.TotalVat);
            Assert.AreEqual(54.45, this.order.TotalIncVat);
            Assert.AreEqual(7, this.order.TotalPurchasePrice);
            Assert.AreEqual(15, this.order.TotalListPrice);
        }

        [Test]
        public void GivenOrderWithoutCustomer_WhenDerivingPrices_ThenUsePriceComponentsForGood()
        {
            this.InstantiateObjects(this.DatabaseSession);

            var anonymousOrder = new SalesOrderBuilder(this.DatabaseSession).Build();

            const decimal QuantityOrdered = 3;
            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            anonymousOrder.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100), 2), item1.UnitVat);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalExVat);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100), 2) * QuantityOrdered, item1.TotalVat);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, anonymousOrder.TotalBasePrice);
            Assert.AreEqual(0, anonymousOrder.TotalDiscount);
            Assert.AreEqual(0, anonymousOrder.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, anonymousOrder.TotalExVat);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100), 2) * QuantityOrdered, anonymousOrder.TotalVat);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemForGood1_WhenDerivingPrices_ThenUsePriceComponentsForGood1()
        {
            this.InstantiateObjects(this.DatabaseSession);

            const decimal QuantityOrdered = 3;
            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100), 2), item1.UnitVat);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalExVat);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100), 2) * QuantityOrdered, item1.TotalVat);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, this.order.TotalExVat);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100), 2) * QuantityOrdered, this.order.TotalVat);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        //[Test]
        //public void GivenOrderItemForGood1WithFeature1_WhenDerivingPrices_ThenUsePriceComponentsForGood1WithFeature1()
        //{
        //    foreach (var session in this.Sessions)
        //    {
        //        session.Commit();

        //        this.InstantiateObjects(this.DatabaseSession);

        //        const decimal QuantityOrdered = 3;
        //        var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
        //        var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProductFeature(this.feature1).WithQuantityOrdered(QuantityOrdered).Build();
        //        item1.AddOrderedWithFeature(item2);
        //        this.order.AddSalesOrderItem(item1);
        //        this.order.AddSalesOrderItem(item2);
        //        this.order.Derive(new DefaultDerivationLog());

        //        var expectedCalculatedUnitPrice = this.currentGoodBasePrice.Price;
        //        expectedCalculatedUnitPrice += this.currentGood1Feature1BasePrice.Price;

        //        var expectedTotalBasePrice = this.currentGoodBasePrice.Price * QuantityOrdered;
        //        expectedTotalBasePrice += this.currentGood1Feature1BasePrice.Price * QuantityOrdered;

        //        var expectedTotalExVat = this.currentGoodBasePrice.Price * QuantityOrdered;
        //        expectedTotalExVat += this.currentGood1Feature1BasePrice.Price * QuantityOrdered;

        //        Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
        //        Assert.AreEqual(0, item1.UnitDiscount);
        //        Assert.AreEqual(0, item1.UnitSurcharge);
        //        Assert.AreEqual(expectedCalculatedUnitPrice, item1.CalculatedUnitPrice);
        //        Assert.AreEqual(expectedTotalBasePrice, item1.TotalBasePrice);
        //        Assert.AreEqual(0, item1.TotalDiscount);
        //        Assert.AreEqual(0, item1.TotalSurcharge);
        //        Assert.AreEqual(expectedTotalExVat, item1.TotalExVat);

        //        Assert.AreEqual(this.currentGood1Feature1BasePrice.Price, item2.UnitBasePrice);
        //        Assert.AreEqual(0, item2.UnitDiscount);
        //        Assert.AreEqual(0, item2.UnitSurcharge);
        //        Assert.AreEqual(this.currentGood1Feature1BasePrice.Price, item2.CalculatedUnitPrice);
        //        Assert.AreEqual(this.currentGood1Feature1BasePrice.Price * QuantityOrdered, item2.TotalBasePrice);
        //        Assert.AreEqual(0, item2.TotalDiscount);
        //        Assert.AreEqual(0, item2.TotalSurcharge);
        //        Assert.AreEqual(this.currentGood1Feature1BasePrice.Price * QuantityOrdered, item2.TotalExVat);

        //        Assert.AreEqual(expectedTotalBasePrice, this.order.TotalBasePrice);
        //        Assert.AreEqual(0, this.order.TotalDiscount);
        //        Assert.AreEqual(0, this.order.TotalSurcharge);
        //        Assert.AreEqual(expectedTotalExVat, this.order.TotalExVat);

        //        if (session is IWorkspaceSession)
        //        {
        //            session.DatabaseSession.Rollback();
        //            Assert.AreEqual(0, ((IWorkspaceSession)session).Conflicts.Length);
        //        }

        //        session.Rollback();
        //    }
        //}

        [Test]
        public void GivenOrderItemForGood1WithFeature2_WhenDerivingPrices_ThenUsePriceComponentsForGood1AndFeature2()
        {
            this.InstantiateObjects(this.DatabaseSession);

            const decimal QuantityOrdered = 3;
            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProductFeature(this.feature2).WithQuantityOrdered(QuantityOrdered).Build();
            item1.AddOrderedWithFeature(item2);
            this.order.AddSalesOrderItem(item1);
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            var expectedCalculatedUnitPrice = this.currentGoodBasePrice.Price;
            expectedCalculatedUnitPrice += this.currentFeature2BasePrice.Price;

            var expectedTotalBasePrice = this.currentGoodBasePrice.Price * QuantityOrdered;
            expectedTotalBasePrice += this.currentFeature2BasePrice.Price * QuantityOrdered;

            var expectedTotalExVat = this.currentGoodBasePrice.Price * QuantityOrdered;
            expectedTotalExVat += this.currentFeature2BasePrice.Price * QuantityOrdered;

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(expectedCalculatedUnitPrice, item1.CalculatedUnitPrice);
            Assert.AreEqual(expectedTotalBasePrice, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(expectedTotalExVat, item1.TotalExVat);

            Assert.AreEqual(this.currentFeature2BasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentFeature2BasePrice.Price, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.currentFeature2BasePrice.Price * QuantityOrdered, item2.TotalBasePrice);
            Assert.AreEqual(0, item2.TotalDiscount);
            Assert.AreEqual(0, item2.TotalSurcharge);
            Assert.AreEqual(this.currentFeature2BasePrice.Price * QuantityOrdered, item2.TotalExVat);

            Assert.AreEqual(expectedTotalBasePrice, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(expectedTotalExVat, this.order.TotalExVat);
        }

        [Test]
        public void GivenProductWithMultipleBasePrices_WhenDeriving_ThenLowestUnitPriceMustBeCalculated()
        {
            this.InstantiateObjects(this.DatabaseSession);

            this.order.ShipToAddress = this.shipToContactMechanismMechelen;

            this.DatabaseSession.Derive(true);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(3).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentBasePriceGeoBoundary.Price, item1.CalculatedUnitPrice);

            var order2 = new SalesOrderBuilder(this.DatabaseSession)
                .WithTakenByInternalOrganisation(this.internalOrganisation)
                .WithShipToCustomer(this.shipToCustomer)
                .WithBillToCustomer(this.billToCustomer)
                .Build();

            item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(3).Build();
            order2.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForGeoBoundary_WhenDeriving_ThenUseDiscountComponentsForGeoBoundary()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(decimal.Round((Amount * QuantityOrdered / item1.TotalBasePrice) * 100, 2), item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForGeoBoundary_WhenDeriving_ThenUseDiscountComponentsForGeoBoundary()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount, item1.UnitDiscount);
            Assert.AreEqual(Percentage, item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAdjustment_WhenDeriving_ThenCalculateSellingPriceUsingItemSurchargeAdjustment()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;
            const decimal AdjustmentPercentage = 5;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession)
                .WithProduct(this.good)
                .WithQuantityOrdered(QuantityOrdered)
                .WithSurchargeAdjustment(new SurchargeAdjustmentBuilder(this.DatabaseSession).WithPercentage(AdjustmentPercentage).Build())
                .Build();

            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var adjustmentAmount = decimal.Round(((this.currentGoodBasePrice.Price - Amount) * AdjustmentPercentage) / 100, 2);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(adjustmentAmount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount + adjustmentAmount, item1.CalculatedUnitPrice);
            Assert.AreEqual(adjustmentAmount * QuantityOrdered, item1.TotalOrderAdjustment);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForGeoBoundary_WhenDeriving_ThenUseSurchargeComponentsForGeoBoundary()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForGeoBoundary_WhenDeriving_ThenUseSurchargeComponentsForGeoBoundary()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForPartyClassification_WhenDeriving_ThenUseDiscountComponentsForPartyClassification()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            var classification = new IndustryClassificationBuilder(this.DatabaseSession).WithName("gold customer").Build();
            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for party classification")
                .WithPartyClassification(classification)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.billToCustomer.AddPartyClassification(classification);

            this.order.ShipToCustomer = this.shipToCustomer;

            this.DatabaseSession.Derive(true);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);
            
            this.DatabaseSession.Derive(true);
            
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(decimal.Round((Amount * QuantityOrdered / item1.TotalBasePrice) * 100, 2), item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForPartyClassification_WhenDeriving_ThenUseDiscountComponentsForPartyClassification()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            var classification = new IndustryClassificationBuilder(this.DatabaseSession).WithName("gold customer").Build();
            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for party classification")
                .WithPartyClassification(classification)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.billToCustomer.AddPartyClassification(classification);

            this.order.ShipToCustomer = this.shipToCustomer;

            this.DatabaseSession.Derive(true);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount, item1.UnitDiscount);
            Assert.AreEqual(Percentage, item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForPartyClassification_WhenDeriving_ThenUseSurchargeComponentsForPartyClassification()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            var classification = new IndustryClassificationBuilder(this.DatabaseSession).WithName("gold customer").Build();
            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for party classification")
                .WithPartyClassification(classification)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.billToCustomer.AddPartyClassification(classification);

            this.order.ShipToCustomer = this.shipToCustomer;

            this.DatabaseSession.Derive(true);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForPartyClassification_WhenDeriving_ThenUseSurchargeComponentsForPartyClassification()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            var classification = new IndustryClassificationBuilder(this.DatabaseSession).WithName("gold customer").Build();
            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for party classification")
                .WithPartyClassification(classification)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.billToCustomer.AddPartyClassification(classification);

            this.order.ShipToCustomer = this.shipToCustomer;

            this.DatabaseSession.Derive(true);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForAncestorProductCategory_WhenDeriving_ThenUseDiscountComponentsForAncestorProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for ancestor category")
                .WithProductCategory(this.ancestorProductCategory)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(decimal.Round((Amount * QuantityOrdered / item1.TotalBasePrice) * 100, 2), item1.TotalDiscountAsPercentage);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForAncestorProductCategory_WhenDeriving_ThenUseDiscountComponentsForAncestorProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for ancestor category")
                .WithProductCategory(this.ancestorProductCategory)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount, item1.UnitDiscount);
            Assert.AreEqual(Percentage, item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForParentProductCategory_WhenDeriving_ThenUseDiscountComponentsForParentProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for parent category")
                .WithProductCategory(this.parentProductCategory)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(decimal.Round((Amount * QuantityOrdered / item1.TotalBasePrice) * 100, 2), item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForParentProductCategory_WhenDeriving_ThenUseDiscountComponentsForParentProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for parent category")
                .WithProductCategory(this.parentProductCategory)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount, item1.UnitDiscount);
            Assert.AreEqual(Percentage, item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForProductCategory_WhenDeriving_ThenUseDiscountComponentsForProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for product category")
                .WithProductCategory(this.productCategory)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(decimal.Round((Amount * QuantityOrdered / item1.TotalBasePrice) * 100, 2), item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForProductCategory_WhenDeriving_ThenUseDiscountComponentsForProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for product category")
                .WithProductCategory(this.productCategory)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount, item1.UnitDiscount);
            Assert.AreEqual(Percentage, item1.TotalDiscountAsPercentage);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForProductCategory_WhenDeriving_ThenUseSurchargeComponentsForProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for product category")
                .WithProductCategory(this.productCategory)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForProductCategory_WhenDeriving_ThenUseSurchargeComponentsForProductCategory()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for product category")
                .WithProductCategory(this.productCategory)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForOrderQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForOrderQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 50;
            const decimal QuantityOrdered3 = 50;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithOrderQuantityBreak(break1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithOrderQuantityBreak(break2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(Amount1, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(Amount2, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(Amount2, item3.UnitDiscount);
            Assert.AreEqual(0, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice - this.goodPurchasePrice.Price) / item3.UnitBasePrice) * 100, 2), item3.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item3.CalculatedUnitPrice) * 100, 2), item3.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForOrderQuantityBreak_WhenDeriving_ThenUseDiscountComponentsForOrderQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 50;
            const decimal QuantityOrdered3 = 50;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 1")
                .WithOrderQuantityBreak(break1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for quantity break 2")
                .WithOrderQuantityBreak(break2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(amount1, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(amount2, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(amount2, item3.UnitDiscount);
            Assert.AreEqual(0, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForOrderQuantityBreak_WhenDeriving_ThenUseSurchargeComponentsForOrderQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 50;
            const decimal QuantityOrdered3 = 50;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var break1 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for quantity break 1")
                .WithOrderQuantityBreak(break1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for quantity break 2")
                .WithOrderQuantityBreak(break2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount1, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(Amount1, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount2, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(Amount2, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(0, item3.UnitDiscount);
            Assert.AreEqual(Amount2, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice - this.goodPurchasePrice.Price) / item3.UnitBasePrice) * 100, 2), item3.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item3.CalculatedUnitPrice) * 100, 2), item3.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForOrderQuantityBreak_WhenDeriving_ThenUseSurchargeComponentsForOrderQuantityBreak()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 50;
            const decimal QuantityOrdered3 = 50;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var break1 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var break2 = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for quantity break 1")
                .WithOrderQuantityBreak(break1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for quantity break 2")
                .WithOrderQuantityBreak(break2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount1, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(amount1, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount2, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(amount2, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(0, item3.UnitDiscount);
            Assert.AreEqual(amount2, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForOrderValue_WhenDeriving_ThenUseDiscountComponentsForOrderValue()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 3;
            const decimal QuantityOrdered3 = 10;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var value1 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var value2 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for order value 1")
                .WithOrderValue(value1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for order value 1")
                .WithOrderValue(value2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(Amount1, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(Amount2, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(Amount2, item3.UnitDiscount);
            Assert.AreEqual(0, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice - this.goodPurchasePrice.Price) / item3.UnitBasePrice) * 100, 2), item3.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item3.CalculatedUnitPrice) * 100, 2), item3.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageForOrderValue_WhenDeriving_ThenUseDiscountComponentsForOrderValue()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 3;
            const decimal QuantityOrdered3 = 10;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var value1 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var value2 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for order value 1")
                .WithOrderValue(value1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for order value 1")
                .WithOrderValue(value2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount1, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(amount1, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(amount2, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(amount2, item2.UnitDiscount);
            Assert.AreEqual(0, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(amount2, item3.UnitDiscount);
            Assert.AreEqual(0, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForOrderValue_WhenDeriving_ThenUseSurchargeComponentsForOrderValue()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 3;
            const decimal QuantityOrdered3 = 10;
            const decimal Amount1 = 1;
            const decimal Amount2 = 3;

            var value1 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var value2 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for order value 1")
                .WithOrderValue(value1)
                .WithProduct(this.good)
                .WithPrice(Amount1)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for order value 1")
                .WithOrderValue(value2)
                .WithProduct(this.good)
                .WithPrice(Amount2)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount1, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(Amount1, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount2, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(Amount2, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item2.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item2.UnitBasePrice - this.goodPurchasePrice.Price) / item2.UnitBasePrice) * 100, 2), item2.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item2.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item2.CalculatedUnitPrice) * 100, 2), item2.MaintainedProfitMargin);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(0, item3.UnitDiscount);
            Assert.AreEqual(Amount2, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item3.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item3.UnitBasePrice - this.goodPurchasePrice.Price) / item3.UnitBasePrice) * 100, 2), item3.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item3.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item3.CalculatedUnitPrice) * 100, 2), item3.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForOrderValue_WhenDeriving_ThenUseSurchargeComponentsForOrderValue()
        {
            const decimal QuantityOrdered1 = 3;
            const decimal QuantityOrdered2 = 3;
            const decimal QuantityOrdered3 = 10;
            const decimal Percentage1 = 5;
            const decimal Percentage2 = 10;

            var value1 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(50).WithThroughAmount(99).Build();
            var value2 = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(100).Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for order value 1")
                .WithOrderValue(value1)
                .WithProduct(this.good)
                .WithPercentage(Percentage1)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("surcharge good for order value 1")
                .WithOrderValue(value2)
                .WithProduct(this.good)
                .WithPercentage(Percentage2)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered1).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            var item2 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered2).Build();
            this.order.AddSalesOrderItem(item2);

            this.DatabaseSession.Derive(true);

            var amount1 = decimal.Round((this.currentGoodBasePrice.Price * Percentage1) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount1, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount1, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(amount1, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount1, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            var item3 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered3).Build();
            this.order.AddSalesOrderItem(item3);

            this.DatabaseSession.Derive(true);

            var amount2 = decimal.Round((this.currentGoodBasePrice.Price * Percentage2) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount2, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item2.UnitBasePrice);
            Assert.AreEqual(0, item2.UnitDiscount);
            Assert.AreEqual(amount2, item2.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item2.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item2.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item3.UnitBasePrice);
            Assert.AreEqual(0, item3.UnitDiscount);
            Assert.AreEqual(amount2, item3.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount2, item3.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item3.UnitPurchasePrice);
        }

        [Test]
        public void GivenOrderItemWithDiscountAmountForSalesType_WhenDeriving_ThenUseDiscountComponentsForSalesType()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            var email = new SalesChannels(this.DatabaseSession).EmailChannel;
            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for sales type")
                .WithSalesChannel(email)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.order.SalesChannel = email;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscountPercentageAndItemDiscountPercentage_WhenDeriving_ThenExtraDiscountIsCalculated()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;
            const decimal AdjustmentPerc = 10;
            var discountAdjustment = new DiscountAdjustmentBuilder(this.DatabaseSession).WithPercentage(AdjustmentPerc).Build();

            var email = new SalesChannels(this.DatabaseSession).EmailChannel;
            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for sales type")
                .WithSalesChannel(email)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.order.SalesChannel = email;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession)
                .WithProduct(this.good)
                .WithQuantityOrdered(QuantityOrdered)
                .WithDiscountAdjustment(discountAdjustment)
                .Build();

            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var discount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            var discountedprice = this.currentGoodBasePrice.Price - discount;
            discount += decimal.Round((discountedprice * discountAdjustment.Percentage) / 100, 2);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(discount, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - discount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargeAmountForSalesType_WhenDeriving_ThenUseSurchargeComponentsForSalesType()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            var email = new SalesChannels(this.DatabaseSession).EmailChannel;
            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for sales type")
                .WithSalesChannel(email)
                .WithProduct(this.good)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.order.SalesChannel = email;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(Amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + Amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageForSalesType_WhenDeriving_ThenUseSurchargeComponentsForSalesType()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;

            var email = new SalesChannels(this.DatabaseSession).EmailChannel;
            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for sales type")
                .WithSalesChannel(email)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.order.SalesChannel = email;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var amount = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(amount, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + amount, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithSurchargePercentageAndItemSurchargePercentage_WhenDeriving_ThenExtraSurchargeIsCalculated()
        {
            const decimal QuantityOrdered = 3;
            const decimal Percentage = 5;
            const decimal SurchargePerc = 10;

            var surchargeAdjustment = new SurchargeAdjustmentBuilder(this.DatabaseSession).WithPercentage(SurchargePerc).Build();

            var email = new SalesChannels(this.DatabaseSession).EmailChannel;
            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for sales type")
                .WithSalesChannel(email)
                .WithProduct(this.good)
                .WithPercentage(Percentage)
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            this.order.SalesChannel = email;

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession)
                .WithProduct(this.good)
                .WithQuantityOrdered(QuantityOrdered)
                .WithSurchargeAdjustment(surchargeAdjustment)
                .Build();

            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var surcharge = decimal.Round((this.currentGoodBasePrice.Price * Percentage) / 100, 2);
            var surchargedPrice = this.currentGoodBasePrice.Price + surcharge;
            surcharge += decimal.Round((surchargedPrice * surchargeAdjustment.Percentage) / 100, 2);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(surcharge, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price + surcharge, item1.CalculatedUnitPrice);
            Assert.AreEqual(this.goodPurchasePrice.Price, item1.UnitPurchasePrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenBillToCustomerWithDifferentCurrency_WhenDerivingPrices_ThenCalculatePricesInPreferredCurrency()
        {
            var poundSterling = new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "GBP");

            const decimal Conversionfactor = 0.8553M;
            var euroToPoundStirling = new UnitOfMeasureConversionBuilder(this.DatabaseSession)
                .WithConversionFactor(Conversionfactor)
                .WithToUnitOfMeasure(poundSterling)
                .WithStartDate(DateTime.Now)
                .Build();

            var euro = new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR");
            euro.AddUnitOfMeasureConversion(euroToPoundStirling);

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            Assert.AreEqual(euro, this.order.CustomerCurrency);

            this.billToCustomer.PreferredCurrency = poundSterling;

            var newOrder = new SalesOrderBuilder(this.DatabaseSession)
                .WithShipToCustomer(this.shipToCustomer)
                .WithBillToCustomer(this.billToCustomer)
                .WithTakenByInternalOrganisation(this.internalOrganisation)
                .Build();

            const decimal QuantityOrdered = 3;
            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            newOrder.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(poundSterling, newOrder.CustomerCurrency);

            Assert.AreEqual(decimal.Round(item1.TotalBasePrice * Conversionfactor, 2), item1.TotalBasePriceCustomerCurrency);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(decimal.Round(item1.TotalExVat * Conversionfactor, 2), item1.TotalExVatCustomerCurrency);
        }

        [Test]
        public void GivenOrderItemForVariantGood_WhenDerivingPrices_ThenUsePriceComponentsForVirtualGood()
        {
            this.InstantiateObjects(this.DatabaseSession);

            const decimal QuantityOrdered = 3;
            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.variantGood).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentVirtualGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(decimal.Round((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100, 2), item1.UnitVat);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalExVat);
            Assert.AreEqual(decimal.Round((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100, 2) * QuantityOrdered, item1.TotalVat);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, this.order.TotalExVat);
            Assert.AreEqual(decimal.Round((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100, 2) * QuantityOrdered, this.order.TotalVat);
        }

        [Test]
        public void GivenOrderItemWithDiscountAdjustment_WhenDeriving_ThenCalculateSellingPriceUsingItemDiscountAdjustment()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;
            const decimal AdjustmentPercentage = 5;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession)
                .WithProduct(this.good)
                .WithQuantityOrdered(QuantityOrdered)
                .WithDiscountAdjustment(new DiscountAdjustmentBuilder(this.DatabaseSession).WithPercentage(AdjustmentPercentage).Build())
                .Build();

            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            var adjustmentAmount = decimal.Round(((this.currentGoodBasePrice.Price - Amount) * AdjustmentPercentage) / 100, 2);
                
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount + adjustmentAmount, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount - adjustmentAmount, item1.CalculatedUnitPrice);
            Assert.AreEqual((0 - adjustmentAmount) * QuantityOrdered, item1.TotalOrderAdjustment);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenOrderItemWithDiscount_WhenDeriving_ThenUseGeneralDiscountNotBoundToProduct()
        {
            const decimal QuantityOrdered = 3;
            const decimal Amount = 1;

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithSpecifiedFor(this.internalOrganisation)
                .WithDescription("discount good for geo boundary")
                .WithGeographicBoundary(this.kiev)
                .WithPrice(Amount)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithFromDate(DateTime.Now)
                .WithThroughDate(DateTime.Now.AddYears(1).AddDays(-1))
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(Amount, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price - Amount, item1.CalculatedUnitPrice);

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / this.goodPurchasePrice.Price) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - this.goodPurchasePrice.Price) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - this.goodPurchasePrice.Price) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        [Test]
        public void GivenPurchasePriceInDifferenUnitOfMeasureComparedToProduct_WhenDerivingMarkupAndProfitMargin_ThenUnitOfMeasureConversionIsPerformed()
        {
            var pair = new UnitsOfMeasure(this.DatabaseSession).Pair;
            var piece = new UnitsOfMeasure(this.DatabaseSession).Piece;

            var fromPairToPiece = new UnitOfMeasureConversionBuilder(this.DatabaseSession)
                .WithToUnitOfMeasure(piece)
                .WithConversionFactor(2).Build();

            var fromPieceToPair = new UnitOfMeasureConversionBuilder(this.DatabaseSession)
                .WithToUnitOfMeasure(pair)
                .WithConversionFactor(0.5M).Build();

            pair.AddUnitOfMeasureConversion(fromPairToPiece);
            pair.AddUnitOfMeasureConversion(fromPieceToPair);

            this.goodPurchasePrice.UnitOfMeasure = pair;
            this.good.UnitOfMeasure = piece;

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            this.InstantiateObjects(this.DatabaseSession);

            const decimal QuantityOrdered = 6;
            var item1 = new SalesOrderItemBuilder(this.DatabaseSession).WithProduct(this.good).WithQuantityOrdered(QuantityOrdered).Build();
            this.order.AddSalesOrderItem(item1);

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.UnitBasePrice);
            Assert.AreEqual(0, item1.UnitDiscount);
            Assert.AreEqual(0, item1.UnitSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price, item1.CalculatedUnitPrice);
            Assert.AreEqual(decimal.Round((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100, 2), item1.UnitVat);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalBasePrice);
            Assert.AreEqual(0, item1.TotalDiscount);
            Assert.AreEqual(0, item1.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, item1.TotalExVat);
            Assert.AreEqual(decimal.Round((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100, 2) * QuantityOrdered, item1.TotalVat);
            Assert.AreEqual(this.goodPurchasePrice.Price * 0.5M, item1.UnitPurchasePrice);

            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, this.order.TotalBasePrice);
            Assert.AreEqual(0, this.order.TotalDiscount);
            Assert.AreEqual(0, this.order.TotalSurcharge);
            Assert.AreEqual(this.currentGoodBasePrice.Price * QuantityOrdered, this.order.TotalExVat);
            Assert.AreEqual(decimal.Round((item1.CalculatedUnitPrice * this.vatRate21.Rate) / 100, 2) * QuantityOrdered, this.order.TotalVat);

            var purchasePrice = this.goodPurchasePrice.Price * 0.5M;

            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice / purchasePrice) - 1) * 100, 2), item1.InitialMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice / purchasePrice) - 1) * 100, 2), item1.MaintainedMarkupPercentage);
            Assert.AreEqual(decimal.Round(((item1.UnitBasePrice - purchasePrice) / item1.UnitBasePrice) * 100, 2), item1.InitialProfitMargin);
            Assert.AreEqual(decimal.Round(((item1.CalculatedUnitPrice - purchasePrice) / item1.CalculatedUnitPrice) * 100, 2), item1.MaintainedProfitMargin);
        }

        private void InstantiateObjects(ISession session)
        {
            this.productCategory = (ProductCategory)session.Instantiate(this.productCategory);
            this.parentProductCategory = (ProductCategory)session.Instantiate(this.parentProductCategory);
            this.ancestorProductCategory = (ProductCategory)session.Instantiate(this.ancestorProductCategory);
            this.part = (FinishedGood)session.Instantiate(this.part);
            this.good = (Good)session.Instantiate(this.good);
            this.feature1 = (Colour)session.Instantiate(this.feature1);
            this.feature2 = (Colour)session.Instantiate(this.feature2);
            this.internalOrganisation = (InternalOrganisation)session.Instantiate(this.internalOrganisation);
            this.shipToCustomer = (Organisation)session.Instantiate(this.shipToCustomer);
            this.billToCustomer = (Organisation)session.Instantiate(this.billToCustomer);
            this.supplier = (Organisation)session.Instantiate(this.supplier);
            this.kiev = (City)session.Instantiate(this.kiev);
            this.shipToContactMechanismMechelen = (PostalAddress)session.Instantiate(this.shipToContactMechanismMechelen);
            this.shipToContactMechanismKiev = (PostalAddress)session.Instantiate(this.shipToContactMechanismKiev);
            this.currentBasePriceGeoBoundary = (BasePrice)session.Instantiate(this.currentBasePriceGeoBoundary);
            this.currentGoodBasePrice = (BasePrice)session.Instantiate(this.currentGoodBasePrice);
            this.currentGood1Feature1BasePrice = (BasePrice)session.Instantiate(this.currentGood1Feature1BasePrice);
            this.currentFeature2BasePrice = (BasePrice)session.Instantiate(this.currentFeature2BasePrice);
            this.goodPurchasePrice = (ProductPurchasePrice)session.Instantiate(this.goodPurchasePrice);
            this.virtualGoodPurchasePrice = (ProductPurchasePrice)session.Instantiate(this.virtualGoodPurchasePrice);
            this.currentGoodBasePrice = (BasePrice)session.Instantiate(this.currentGoodBasePrice);
            this.goodSupplierOffering = (SupplierOffering)session.Instantiate(this.goodSupplierOffering);
            this.virtualgoodSupplierOffering = (SupplierOffering)session.Instantiate(this.virtualgoodSupplierOffering);
            this.order = (SalesOrder)session.Instantiate(this.order);
            this.vatRate21 = (VatRate)session.Instantiate(this.vatRate21);
        }
    }
}