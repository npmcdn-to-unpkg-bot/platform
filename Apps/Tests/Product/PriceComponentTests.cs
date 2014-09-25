//------------------------------------------------------------------------------------------------- 
// <copyright file="PriceComponentTests.cs" company="Allors bvba">
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
    public class PriceComponentTests : DomainTest
    {
        [Test]
        public void GivenBasePrice_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var colorFeature = new ColourBuilder(this.DatabaseSession)
                .WithDescription("golden")
                .WithVatRate(vatRate21)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("black")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var builder = new BasePriceBuilder(this.DatabaseSession);
            var basePrice = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithPrice(1);
            basePrice = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithProduct(good);
            basePrice = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithProductFeature(colorFeature);
            basePrice = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromDate(DateTime.Now);
            basePrice = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"));
            basePrice = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenBasePrice_WhenDeriving_ThenDisplayNameIsSet()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var basePrice = new BasePriceBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(good)
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Baseprice: € 10.00 for product gizmo, SKU: 10101", basePrice.DisplayName);

            basePrice.ProductFeature = new ColourBuilder(this.DatabaseSession)
                .WithVatRate(vatRate21)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("golden")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();


            this.DatabaseSession.Derive(true); 
            
            Assert.AreEqual("Baseprice: € 10.00 for feature golden with product gizmo, SKU: 10101", basePrice.DisplayName);

            basePrice.GeographicBoundary = new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "NL");
            
            this.DatabaseSession.Derive(true);
            
            Assert.AreEqual("Baseprice: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands", basePrice.DisplayName);

            basePrice.PartyClassification = new PartyClassificationBuilder(this.DatabaseSession).WithDescription("Minority").Build();
            
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Baseprice: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority", basePrice.DisplayName);

            basePrice.ProductCategory = new ProductCategoryBuilder(this.DatabaseSession).WithDescription("Summer sale").Build();
            
            this.DatabaseSession.Derive(true);
            
            Assert.AreEqual("Baseprice: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority, with product category Summer sale", basePrice.DisplayName);

            basePrice.SalesChannel = new SalesChannels(this.DatabaseSession).EbayChannel;

            this.DatabaseSession.Derive(true);
            
            Assert.AreEqual("Baseprice: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority, with product category Summer sale, with sales channel via eBay", basePrice.DisplayName);
        }

        [Test]
        public void GivenBasePriceForVirtualProduct_WhenDeriving_ThenProductVirtualProductPriceComponentIsUpdated()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var virtualGood = new GoodBuilder(this.DatabaseSession)
                .WithName("virtual gizmo")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var physicalGood = new GoodBuilder(this.DatabaseSession)
                .WithName("real gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)                                     
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            virtualGood.AddVariant(physicalGood);
            
            this.DatabaseSession.Derive(true);

            var basePrice = new BasePriceBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(virtualGood)
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(1, physicalGood.VirtualProductPriceComponents.Count);
            Assert.Contains(basePrice, physicalGood.VirtualProductPriceComponents);
            Assert.IsFalse(virtualGood.ExistVirtualProductPriceComponents);
        }

        [Test]
        public void GivenBasePriceForNonVirtualProduct_WhenDeriving_ThenProductVirtualProductPriceComponentIsNull()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();

            var physicalGood = new GoodBuilder(this.DatabaseSession)
                .WithName("real gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            new BasePriceBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(physicalGood)
                .WithFromDate(DateTime.Now)
                .Build();

            Assert.IsFalse(physicalGood.ExistVirtualProductPriceComponents);
        }

        [Test]
        public void GivenDiscount_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var colorFeature = new ColourBuilder(this.DatabaseSession)
             .WithDescription("golden")
             .WithVatRate(vatRate21)
             .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                         .WithText("black")
                                         .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                         .Build())
             .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var builder = new DiscountComponentBuilder(this.DatabaseSession);
            var discount = builder.Build();
            
            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithPrice(1);
            discount = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromDate(DateTime.Now);
            discount = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithProduct(good);
            discount = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithProductFeature(colorFeature);
            discount = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithPercentage(10);
            discount = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenDiscountForVirtualProduct_WhenDeriving_ThenProductVirtualProductPriceComponentIsUpdated()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var virtualService = new DeliverableBasedServiceBuilder(this.DatabaseSession)
                .WithName("virtual service")
                .WithVatRate(vatRate21)
                .Build();

            var physicalService = new DeliverableBasedServiceBuilder(this.DatabaseSession)
                .WithName("real service")
                .WithVatRate(vatRate21)
                .Build();

            virtualService.AddVariant(physicalService);

            this.DatabaseSession.Derive(true); 

            var discount = new DiscountComponentBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(virtualService)
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(1, physicalService.VirtualProductPriceComponents.Count);
            Assert.Contains(discount, physicalService.VirtualProductPriceComponents);
            Assert.IsFalse(virtualService.ExistVirtualProductPriceComponents);
        }

        [Test]
        public void GivenDiscountForNonVirtualProduct_WhenDeriving_ThenProductVirtualProductPriceComponentIsNull()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var physicalService = new DeliverableBasedServiceBuilder(this.DatabaseSession)
                .WithName("real service")
                .WithVatRate(vatRate21)
                .Build();

            new DiscountComponentBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(physicalService)
                .WithFromDate(DateTime.Now)
                .Build();

            Assert.IsFalse(physicalService.ExistVirtualProductPriceComponents);
        }

        [Test]
        public void GivenSurcharge_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var colorFeature = new ColourBuilder(this.DatabaseSession)
                .WithDescription("golden")
                .WithVatRate(vatRate21)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("black")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            var builder = new SurchargeComponentBuilder(this.DatabaseSession);
            var surcharge = builder.Build();
            
            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);
            
            this.DatabaseSession.Rollback();

            builder.WithPrice(1);
            surcharge = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithFromDate(DateTime.Now);
            surcharge = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithProduct(good);
            surcharge = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithProductFeature(colorFeature);
            surcharge = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);

            builder.WithPercentage(10);
            surcharge = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenSurchargeForVirtualProduct_WhenDeriving_ThenProductVirtualProductPriceComponentIsUpdated()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var virtualService = new TimeAndMaterialsServiceBuilder(this.DatabaseSession)
                .WithName("virtual service")
                .WithVatRate(vatRate21)
                .Build();

            var physicalService = new TimeAndMaterialsServiceBuilder(this.DatabaseSession)
                .WithName("real service")
                .WithVatRate(vatRate21)
                .Build();

            virtualService.AddVariant(physicalService);

            this.DatabaseSession.Derive(true); 

            var surcharge = new SurchargeComponentBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(virtualService)
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual(1, physicalService.VirtualProductPriceComponents.Count);
            Assert.Contains(surcharge, physicalService.VirtualProductPriceComponents);
            Assert.IsFalse(virtualService.ExistVirtualProductPriceComponents);
        }

        [Test]
        public void GivenSurchargeForNonVirtualProduct_WhenDeriving_ThenProductVirtualProductPriceComponentIsNull()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var physicalService = new TimeAndMaterialsServiceBuilder(this.DatabaseSession)
                .WithName("real service")
                .WithVatRate(vatRate21)
                .Build();

            new SurchargeComponentBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(physicalService)
                .WithFromDate(DateTime.Now)
                .Build();

            Assert.IsFalse(physicalService.ExistVirtualProductPriceComponents);
        }

        [Test]
        public void GivenDiscount_WhenDeriving_ThenDisplayNameIsSet()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var discount = new DiscountComponentBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(good)
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for product gizmo, SKU: 10101", discount.DisplayName);

            discount.ProductFeature = new ColourBuilder(this.DatabaseSession)
                .WithVatRate(vatRate21)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("golden")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101", discount.DisplayName);

            discount.GeographicBoundary = new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "NL");
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands", discount.DisplayName);

            discount.PartyClassification = new PartyClassificationBuilder(this.DatabaseSession).WithDescription("Minority").Build();
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority", discount.DisplayName);

            discount.ProductCategory = new ProductCategoryBuilder(this.DatabaseSession).WithDescription("Summer sale").Build();
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority, with product category Summer sale", discount.DisplayName);

            discount.SalesChannel = new SalesChannels(this.DatabaseSession).EbayChannel;
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority, with product category Summer sale, with sales channel via eBay", discount.DisplayName);

            discount.RemoveGeographicBoundary();
            discount.RemovePartyClassification();
            discount.RemoveProductCategory();
            discount.RemoveSalesChannel();

            var quantityBreak = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();
            discount.OrderQuantityBreak = quantityBreak;
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break from 100", discount.DisplayName);

            quantityBreak.ThroughAmount = 1000;
            discount.OrderQuantityBreak = quantityBreak;
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break from 100 through 1000", discount.DisplayName);

            quantityBreak.RemoveFromAmount();
            discount.OrderQuantityBreak = quantityBreak;
            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000", discount.DisplayName);

            var orderValue = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(100).Build();
            discount.OrderValue = orderValue;

            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000, with order value from 100", discount.DisplayName);

            orderValue.ThroughAmount = 1000;
            discount.OrderValue = orderValue;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000, with order value from 100 through 1000", discount.DisplayName);

            orderValue.RemoveFromAmount();
            discount.OrderValue = orderValue;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000, with order value through 1000", discount.DisplayName);

            discount.RemoveOrderQuantityBreak();
            discount.RemoveOrderValue();

            var packageBreak = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).Build();
            discount.PackageQuantityBreak = packageBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with package count from 2", discount.DisplayName);

            packageBreak.Through = 2;
            discount.PackageQuantityBreak = packageBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with package count from 2 through 2", discount.DisplayName);

            packageBreak.RemoveFrom();
            discount.PackageQuantityBreak = packageBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Discount: € 10.00 for feature golden with product gizmo, SKU: 10101, with package count through 2", discount.DisplayName);
        }

        [Test]
        public void GivenSurcharge_WhenDeriving_ThenDisplayNameIsSet()
        {
            var vatRate21 = new VatRateBuilder(this.DatabaseSession).WithRate(21).Build();
            var good = new GoodBuilder(this.DatabaseSession)
                .WithName("gizmo")
                .WithSku("10101")
                .WithVatRate(vatRate21)
                .WithInventoryItemKind(new InventoryItemKinds(this.DatabaseSession).NonSerialized)
                .WithUnitOfMeasure(new UnitsOfMeasure(this.DatabaseSession).Piece)
                .Build();

            var surcharge = new SurchargeComponentBuilder(this.DatabaseSession)
                .WithPrice(10)
                .WithCurrency(new Currencies(this.DatabaseSession).FindBy(Currencies.Meta.IsoCode, "EUR"))
                .WithProduct(good)
                .WithFromDate(DateTime.Now)
                .Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual("Surcharge: € 10.00 for product gizmo, SKU: 10101", surcharge.DisplayName);

            surcharge.ProductFeature = new ColourBuilder(this.DatabaseSession)
                .WithVatRate(vatRate21)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("golden")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101", surcharge.DisplayName);

            surcharge.GeographicBoundary = new Countries(this.DatabaseSession).FindBy(Countries.Meta.IsoCode, "NL");
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands", surcharge.DisplayName);

            surcharge.PartyClassification = new PartyClassificationBuilder(this.DatabaseSession).WithDescription("Minority").Build();
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority", surcharge.DisplayName);

            surcharge.ProductCategory = new ProductCategoryBuilder(this.DatabaseSession).WithDescription("Summer sale").Build();
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority, with product category Summer sale", surcharge.DisplayName);

            surcharge.SalesChannel = new SalesChannels(this.DatabaseSession).EbayChannel;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with boundary Netherlands, with party classification Minority, with product category Summer sale, with sales channel via eBay", surcharge.DisplayName);

            surcharge.RemoveGeographicBoundary();
            surcharge.RemovePartyClassification();
            surcharge.RemoveProductCategory();
            surcharge.RemoveSalesChannel();

            var quantityBreak = new OrderQuantityBreakBuilder(this.DatabaseSession).WithFromAmount(100).Build();
            surcharge.OrderQuantityBreak = quantityBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break from 100", surcharge.DisplayName);

            quantityBreak.ThroughAmount = 1000;
            surcharge.OrderQuantityBreak = quantityBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break from 100 through 1000", surcharge.DisplayName);

            quantityBreak.RemoveFromAmount();
            surcharge.OrderQuantityBreak = quantityBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000", surcharge.DisplayName);

            var orderValue = new OrderValueBuilder(this.DatabaseSession).WithFromAmount(100).Build();
            surcharge.OrderValue = orderValue;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000, with order value from 100", surcharge.DisplayName);

            orderValue.ThroughAmount = 1000;
            surcharge.OrderValue = orderValue;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000, with order value from 100 through 1000", surcharge.DisplayName);

            orderValue.RemoveFromAmount();
            surcharge.OrderValue = orderValue;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with order quantity break through 1000, with order value through 1000", surcharge.DisplayName);

            surcharge.RemoveOrderQuantityBreak();
            surcharge.RemoveOrderValue();

            var packageBreak = new PackageQuantityBreakBuilder(this.DatabaseSession).WithFrom(2).Build();
            surcharge.PackageQuantityBreak = packageBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with package count from 2", surcharge.DisplayName);

            packageBreak.Through = 2;
            surcharge.PackageQuantityBreak = packageBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with package count from 2 through 2", surcharge.DisplayName);

            packageBreak.RemoveFrom();
            surcharge.PackageQuantityBreak = packageBreak;
            this.DatabaseSession.Derive(true);
            Assert.AreEqual("Surcharge: € 10.00 for feature golden with product gizmo, SKU: 10101, with package count through 2", surcharge.DisplayName);
        }
    }
}
