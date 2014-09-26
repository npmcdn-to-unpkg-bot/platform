//------------------------------------------------------------------------------------------------- 
// <copyright file="Repository.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
//-------------------------------------------------------------------------------------------------
namespace Allors.Meta
{
    using System;

    public static partial class Repository
    {
        public static Domain Apps(MetaPopulation meta)
        {
            // Imports
            var core = (Domain)meta.Find(new Guid("CA802192-8186-4C2A-8315-A8DEFAA74A12"));
            
            var allorsString = (Unit)meta.Find(UnitIds.StringId);
            var allorsInteger = (Unit)meta.Find(UnitIds.IntegerId);
            var allorsLong = (Unit)meta.Find(UnitIds.LongId);
            var allorsDecimal = (Unit)meta.Find(UnitIds.DecimalId);
            var allorsDouble = (Unit)meta.Find(UnitIds.DoubleId);
            var allorsBoolean = (Unit)meta.Find(UnitIds.BooleanId);
            var allorsDateTime = (Unit)meta.Find(UnitIds.DatetimeId);
            var allorsUnique = (Unit)meta.Find(UnitIds.Unique);
            var allorsBinary = (Unit)meta.Find(UnitIds.BinaryId);
            
            var @base = (Domain)meta.Find(new Guid("AB41FD0C-C887-4A1D-BEDA-CED69527E69A"));

            var userInterfaceable = (Interface)meta.Find(new Guid("eea17b39-8912-40b3-8403-293bd5a3316d"));
            var UniquelyIdentifiable = (Interface)meta.Find( new Guid("122ccfe1-f902-44c1-9d6c-6f6a0afa9469"));

            var Counter = (Class)meta.Find( new Guid("0568354f-e3d9-439e-baac-b7dce31b956a"));
            var Singleton = (Class)meta.Find( new Guid("313b97a5-328c-4600-9dd2-b5bc146fb13b"));

            var Enumeration = (Interface)meta.Find( new Guid("b7bcc22f-03f0-46fd-b738-4e035921d445"));

            var User = (Interface)meta.Find( new Guid("a0309c3b-6f80-4777-983e-6e69800df5be"));
            var UserGroup = (Class)meta.Find( new Guid("60065f5d-a3c2-4418-880d-1026ab607319"));
            var Role = (Class)meta.Find( new Guid("af6fe5f4-e5bc-4099-bcd1-97528af6505d"));
            var Permission = (Class)meta.Find( new Guid("7fded183-3337-4196-afb0-3266377944bc"));
            var AccessControl = (Class)meta.Find( new Guid("c4d93d5e-34c3-4731-9d37-47a8e801d9a8"));
            var AccessControlledObject = (Interface)meta.Find( new Guid("eb0ff756-3e3d-4cf9-8935-8802a73d2df2"));
            var SecurityToken = (Class)meta.Find( new Guid("a53f1aed-0e3f-4c3c-9600-dc579cccf893"));
            var SecurityTokenOwner = (Interface)meta.Find( new Guid("a69cad9c-c2f1-463f-9af1-873ce65aeea6"));

            var Login = (Class)meta.Find( new Guid("ad7277a8-eda4-4128-a990-b47fe43d120a"));

            var Period = (Interface)meta.Find( new Guid("80adbbfd-952e-46f3-a744-78e0ce42bc80"));

            var Transition = (Class)meta.Find( new Guid("a7e490c0-ce29-4298-97c4-519904bb755a"));
            var Transitional = (Interface)meta.Find( new Guid("ab2179ad-9eac-4b61-8d84-81cd777c4926"));
            var ObjectState = (Interface)meta.Find( new Guid("f991813f-3146-4431-96d0-554aa2186887"));

            var StringTemplate = (Class)meta.Find( new Guid("0c50c02a-cc9c-4617-8530-15a24d4ac969"));

            var Language = (Class)meta.Find( new Guid("4a0eca4b-281f-488d-9c7e-497de882c044"));
            var Country = (Class)meta.Find( new Guid("c22bf60e-6428-4d10-8194-94f7be396f28"));
            var Currency = (Class)meta.Find( new Guid("fd397adf-40b4-4ef8-b449-dd5a24273df3"));
            var Locale = (Class)meta.Find( new Guid("45033ae6-85b5-4ced-87ce-02518e6c27fd"));
            var Localised = (Interface)meta.Find( new Guid("7979a17c-0829-46df-a0d4-1b01775cfaac"));
            var LocalisedText = (Class)meta.Find( new Guid("020f5d4d-4a59-4d7b-865a-d72fc70e4d97"));
            var Commentable = (Interface)meta.Find( new Guid("fdd52472-e863-4e91-bb01-1dada2acc8f6"));

            var SearchFragment = (Class)meta.Find( new Guid("490d150f-3322-4616-a75c-71e4d94b3e03"));
            var SearchData = (Class)meta.Find( new Guid("56794636-cfad-47df-8567-84c8ee993ade"));
            var SearchResult = (Interface)meta.Find( new Guid("a0ac7040-6984-4267-a200-919875e08909"));
            var Searchable = (Interface)meta.Find( new Guid("ff34f3f1-6a17-404f-a9e5-5cffcdaa3d31"));
            
            var Printable = (Interface)meta.Find( new Guid("61207a42-3199-4249-baa4-9dd11dc0f5b1"));
            var PrintQueue = (Class)meta.Find( new Guid("b45705e3-0dc6-4296-824a-76bb6af223d3"));
            var Media = (Class)meta.Find( new Guid("da5b86a3-4f33-4c0d-965d-f4fbc1179374"));
            var MediaContent = (Class)meta.Find( new Guid("6c20422e-cb3e-4402-bb40-dacaf584405e"));
            var MediaType = (Class)meta.Find( new Guid("aa7d61f8-6618-47a0-9cf2-e75dd81dbd5b"));
            var Image = (Class)meta.Find( new Guid("caa2a2de-9454-4812-a69f-9d3728706345"));

            var Person = (Class)meta.Find( new Guid("c799ca62-a554-467d-9aa2-1663293bb37f"));


            // Domain
            var domain = new Domain(meta, new Guid("BCEE07A7-F5A5-4169-9711-4F205304D286")) { Name = "Apps" };
            domain.AddDirectSuperdomain(core);

            // ObjectTypes
            var ProductFeatureApplicabilityRelationship = new ClassBuilder(domain, new Guid("003433eb-a0c6-454d-8517-0c03e9be3e96")).WithSingularName("ProductFeatureApplicabilityRelationship").WithPluralName("ProductFeatureApplicabilityRelationships").Build();
            var PartSpecification = new InterfaceBuilder(domain, new Guid("0091574c-edac-4376-8d03-c7e2c2d8132f")).WithSingularName("PartSpecification").WithPluralName("PartSpecifications").Build();
            var OrderShipment = new ClassBuilder(domain, new Guid("00be6409-1ca0-491e-b0a1-3d53e17005f6")).WithSingularName("OrderShipment").WithPluralName("OrderShipments").Build();
            var ProductRequirement = new ClassBuilder(domain, new Guid("00cba2fb-feb8-4566-8898-3bde8820211f")).WithSingularName("ProductRequirement").WithPluralName("ProductRequirements").Build();
            var RequestForProposal = new ClassBuilder(domain, new Guid("0112ddd0-14de-43e2-97d3-981766dd957e")).WithSingularName("RequestForProposal").WithPluralName("RequestsForProposal").Build();
            var SalesInvoiceItemStatus = new ClassBuilder(domain, new Guid("013d3508-a663-4af5-ba01-24b7b907f581")).WithSingularName("SalesInvoiceItemStatus").WithPluralName("SalesInvoiceItemStatuses").Build();
            var QuoteItem = new ClassBuilder(domain, new Guid("01fc58a0-89b8-4dc0-97f9-5f628b9c9577")).WithSingularName("QuoteItem").WithPluralName("QuoteItems").Build();
            var SalesRepPartyProductCategoryRevenue = new ClassBuilder(domain, new Guid("01fd14a1-c852-42c9-8d16-3243ff655b8f")).WithSingularName("SalesRepPartyProductCategoryRevenue").WithPluralName("SalesRepPartyProductCategoryRevenues").Build();
            var PayGrade = new ClassBuilder(domain, new Guid("028de4a4-12d4-422f-8d82-4f1edaa471ae")).WithSingularName("PayGrade").WithPluralName("PayGrades").Build();
            var PartyProductCategoryRevenueHistory = new ClassBuilder(domain, new Guid("02dec829-d0f0-4dfe-8dea-74aeadbe4fc3")).WithSingularName("PartyProductCategoryRevenueHistory").WithPluralName("PartyProductCategoryRevenueHistories").Build();
            var DiscountAdjustment = new ClassBuilder(domain, new Guid("0346a1e2-03c7-4f1e-94ae-35fdf64143a9")).WithSingularName("DiscountAdjustment").WithPluralName("DiscountAdjustments").Build();
            var Position = new ClassBuilder(domain, new Guid("04540476-602f-456a-b300-54166b65c8b1")).WithSingularName("Position").WithPluralName("Positions").Build();
            var LetterCorrespondence = new ClassBuilder(domain, new Guid("05964e28-2c1d-4837-a887-2255f157e889")).WithSingularName("LetterCorrespondence").WithPluralName("LetterCorrespondences").Build();
            var PurchaseOrder = new ClassBuilder(domain, new Guid("062bd939-9902-4747-a631-99ea10002156")).WithSingularName("PurchaseOrder").WithPluralName("PurchaseOrders").Build();
            var Quote = new InterfaceBuilder(domain, new Guid("066bf242-2710-4a68-8ff6-ce4d7d88a04a")).WithSingularName("Quote").WithPluralName("Quotes").Build();
            var GlBudgetAllocation = new ClassBuilder(domain, new Guid("084829bc-d347-489a-9557-9ff1ac7fb5a0")).WithSingularName("GlBudgetAllocation").WithPluralName("GlBudgetAllocations").Build();
            var PartyRelationship = new InterfaceBuilder(domain, new Guid("084abb92-31fd-46e6-ab85-9a7a88c9d72b")).WithSingularName("PartyRelationship").WithPluralName("PartyRelationships").Build();
            var RateType = new ClassBuilder(domain, new Guid("096448e3-991d-481e-b323-39064387141c")).WithSingularName("RateType").WithPluralName("RateTypes").Build();
            var Brand = new ClassBuilder(domain, new Guid("0a7ac589-946b-4d49-b7e0-7e0b9bc90111")).WithSingularName("Brand").WithPluralName("Brands").Build();
            var SupplierOffering = new ClassBuilder(domain, new Guid("0ae3caca-9b4b-407f-bd98-46db03b72a43")).WithSingularName("SupplierOffering").WithPluralName("SupplierOfferings").Build();
            var SalesAccountingTransaction = new ClassBuilder(domain, new Guid("0aecacff-23d0-48ff-8934-a4e5f711c729")).WithSingularName("SalesAccountingTransaction").WithPluralName("SalesAccountingTransactions").Build();
            var Vehicle = new ClassBuilder(domain, new Guid("0b476761-ad10-4e00-88bb-0e44b4574990")).WithSingularName("Vehicle").WithPluralName("Vehicles").Build();
            var AccountingTransactionNumber = new ClassBuilder(domain, new Guid("0b9034b1-288a-48a7-9d46-3ca6dcb7ca3f")).WithSingularName("AccountingTransactionNumber").WithPluralName("AccountingTransactionNumbers").Build();
            var WorkEffortPartyAssignment = new ClassBuilder(domain, new Guid("0bdfb093-35af-4c87-9c1c-05ed9dae6df6")).WithSingularName("WorkEffortPartyAssignment").WithPluralName("WorkEffortPartyAssignments").Build();
            var ContactMechanismPurpose = new ClassBuilder(domain, new Guid("0c6880e7-b41c-47a6-ab40-83e391c7a025")).WithSingularName("ContactMechanismPurpose").WithPluralName("ContactMechanismPurposes").Build();
            var TaxDocument = new ClassBuilder(domain, new Guid("0d03a71b-c58e-405d-a995-c467a0b25d5b")).WithSingularName("TaxDocument").WithPluralName("TaxDocuments").Build();
            var Training = new ClassBuilder(domain, new Guid("0eaa8719-bbf4-408a-8226-851580556024")).WithSingularName("Training").WithPluralName("Trainings").Build();
            var PurchaseAgreement = new ClassBuilder(domain, new Guid("1032dc2f-72b7-4ba2-b47d-ba14d52a18c9")).WithSingularName("PurchaseAgreement").WithPluralName("PurchaseAgreements").Build();
            var CostCenterCategory = new ClassBuilder(domain, new Guid("11214660-3c3a-42e9-8f12-f475d823da64")).WithSingularName("CostCenterCategory").WithPluralName("CostCenterCategories").Build();
            var BasePrice = new ClassBuilder(domain, new Guid("11c608b0-4755-4e74-b720-4eb94e83c24d")).WithSingularName("BasePrice").WithPluralName("BasePrices").Build();
            var JournalEntry = new ClassBuilder(domain, new Guid("11d75a7a-2e86-4430-a6af-2916440c9ecb")).WithSingularName("JournalEntry").WithPluralName("JournalEntries").Build();
            var SubAgreement = new ClassBuilder(domain, new Guid("11e8fae8-3270-4789-a4eb-ca89cddd2859")).WithSingularName("SubAgreement").WithPluralName("SubAgreements").Build();
            var Skill = new ClassBuilder(domain, new Guid("123bfcba-0548-4637-8dfc-267d6c0ac262")).WithSingularName("Skill").WithPluralName("Skills").Build();
            var EmploymentTermination = new ClassBuilder(domain, new Guid("129e6fe8-01d0-40ad-bc6a-e5449c19274f")).WithSingularName("EmploymentTermination").WithPluralName("EmploymentTerminations").Build();
            var FinancialAccountAdjustment = new ClassBuilder(domain, new Guid("12ba6843-bae1-41d1-9ef2-c19d74b0a365")).WithSingularName("FinancialAccountAdjustment").WithPluralName("FinancialAccountAdjustments").Build();
            var Service = new InterfaceBuilder(domain, new Guid("13d519ec-468e-4fa7-9803-b95dbab4eb82")).WithSingularName("Service").WithPluralName("Services").Build();
            var OperatingCondition = new ClassBuilder(domain, new Guid("1409bf2c-3ea8-4a62-ac7e-e1e113eacb7a")).WithSingularName("OperatingCondition").WithPluralName("OperatingConditions").Build();
            var TermType = new ClassBuilder(domain, new Guid("1468c86a-4ac4-4c64-a93b-1b0c5f4b41bc")).WithSingularName("TermType").WithPluralName("TermTypes").Build();
            var LegalTerm = new ClassBuilder(domain, new Guid("14a2576c-3ea7-4016-aba2-44172fb7a952")).WithSingularName("LegalTerm").WithPluralName("LegalTerms").Build();
            var EngineeringBom = new ClassBuilder(domain, new Guid("14a85148-0d92-4869-8a94-b102f047931f")).WithSingularName("EngineeringBom").WithPluralName("EngineeringBoms").Build();
            var PurchaseInvoiceItemType = new ClassBuilder(domain, new Guid("14f7d6d1-ade6-4a3a-a3ef-f614a375180e")).WithSingularName("PurchaseInvoiceItemType").WithPluralName("PurchaseInvoiceItemTypes").Build();
            var Incentive = new ClassBuilder(domain, new Guid("150d21f7-20dd-4951-848f-f74a69dadb5b")).WithSingularName("Incentive").WithPluralName("Incentives").Build();
            var WorkEffortBilling = new ClassBuilder(domain, new Guid("15c8c72b-f551-41b0-86c8-80f02424ec4c")).WithSingularName("WorkEffortBilling").WithPluralName("WorkEffortBillings").Build();
            var PurchaseReturnStatus = new ClassBuilder(domain, new Guid("15f511a9-7a08-42e4-a690-e0d2f01c9686")).WithSingularName("PurchaseReturnStatus").WithPluralName("PurchaseReturnStatuses").Build();
            var PartyRevenueHistory = new ClassBuilder(domain, new Guid("16b7143f-d69e-402a-88af-405b5b88b1c9")).WithSingularName("PartyRevenueHistory").WithPluralName("PartyRevenueHistories").Build();
            var PartSpecificationObjectState = new ClassBuilder(domain, new Guid("17b5b8ec-cb0e-4d81-b5e5-1a99a5afff2e")).WithSingularName("PartSpecificationObjectState").WithPluralName("PartSpecificationObjectStates").Build();
            var PositionTypeRate = new ClassBuilder(domain, new Guid("17b9c8f1-ddf2-4db0-8358-ae66a02395ce")).WithSingularName("PositionTypeRate").WithPluralName("PositionTypeRates").Build();
            var RatingType = new ClassBuilder(domain, new Guid("17d7e31c-9b12-4e0b-a3a7-e687e3991e23")).WithSingularName("RatingType").WithPluralName("RatingTypes").Build();
            var PurchaseInvoiceType = new ClassBuilder(domain, new Guid("18cd7011-e0ed-4f45-a6a8-c28fbf80d95a")).WithSingularName("PurchaseInvoiceType").WithPluralName("PurchaseInvoiceTypes").Build();
            var GeneralLedgerAccount = new ClassBuilder(domain, new Guid("1a0e396b-69bd-4e77-a602-3d7f7938fd74")).WithSingularName("GeneralLedgerAccount").WithPluralName("GeneralLedgerAccounts").Build();
            var ShippingAndHandlingComponent = new ClassBuilder(domain, new Guid("1a174f59-c8cd-49ad-b0f4-a561cdcdcfb2")).WithSingularName("ShippingAndHandlingComponent").WithPluralName("ShippingAndHandlingComponents").Build();
            var PersonalTitle = new ClassBuilder(domain, new Guid("1a4166b3-9d9c-427b-a0d8-da53b0e601a2")).WithSingularName("PersonalTitle").WithPluralName("PersonalTitles").Build();
            var ReceiptAccountingTransaction = new ClassBuilder(domain, new Guid("1a5195d6-8fff-4590-afe1-3f50c4fa0c67")).WithSingularName("ReceiptAccountingTransaction").WithPluralName("ReceiptAccountingTransactions").Build();
            var TimeFrequency = new ClassBuilder(domain, new Guid("1aba0c3c-2a1c-414d-86df-5a9b8c672587")).WithSingularName("TimeFrequency").WithPluralName("TimeFrequencies").Build();
            var SubContractorAgreement = new ClassBuilder(domain, new Guid("1b2113f6-2c00-4ea7-9408-72bae667eaa3")).WithSingularName("SubContractorAgreement").WithPluralName("SubContractorAgreements").Build();
            var PackagingContent = new ClassBuilder(domain, new Guid("1c05a785-2de1-48fa-813f-6e740f6f7cec")).WithSingularName("PackagingContent").WithPluralName("PackagingContents").Build();
            var PartySkill = new ClassBuilder(domain, new Guid("1d157965-59b5-4ead-b4e4-c722495d7658")).WithSingularName("PartySkill").WithPluralName("PartySkills").Build();
            var Document = new InterfaceBuilder(domain, new Guid("1d21adf0-6008-459d-9f6a-3a026e7640bc")).WithSingularName("Document").WithPluralName("Documents").Build();
            var SerializedInventoryItemStatus = new ClassBuilder(domain, new Guid("1da3e549-47cb-4896-94ec-3f8a263bb559")).WithSingularName("SerializedInventoryItemStatus").WithPluralName("SerializedInventoryItemStatuses").Build();
            var FaxCommunication = new ClassBuilder(domain, new Guid("1e67320b-9680-4477-bf1b-70ccd24ab758")).WithSingularName("FaxCommunication").WithPluralName("FaxCommunications").Build();
            var PurchaseInvoiceItem = new ClassBuilder(domain, new Guid("1ee19062-e36d-4836-b0e6-928a3957bd57")).WithSingularName("PurchaseInvoiceItem").WithPluralName("PurchaseInvoiceItems").Build();
            var OrderItemBilling = new ClassBuilder(domain, new Guid("1f14fdb3-9e0f-4cea-b7c7-3ca2ab898f56")).WithSingularName("OrderItemBilling").WithPluralName("OrderItemsBilling").Build();
            var ProductDrawing = new ClassBuilder(domain, new Guid("1fb8d537-a870-4793-95a1-7742749e16fc")).WithSingularName("ProductDrawing").WithPluralName("ProductDrawings").Build();
            var PayHistory = new ClassBuilder(domain, new Guid("208a5af6-8dd8-4a48-acb2-2ecb89e8d322")).WithSingularName("PayHistory").WithPluralName("PayHistories").Build();
            var ShipmentValue = new ClassBuilder(domain, new Guid("20ef8456-83f2-4722-b8a8-1d8ab3129843")).WithSingularName("ShipmentValue").WithPluralName("ShipmentValues").Build();
            var InternalOrganisationAccountingPreference = new ClassBuilder(domain, new Guid("20f97398-6614-44ec-8e33-3a0b3f113e11")).WithSingularName("InternalOrganisationAccountingPreference").WithPluralName("InternalOrganisationAccountingPreferences").Build();
            var PurchaseShipmentObjectState = new ClassBuilder(domain, new Guid("21840af7-e7e7-4e8d-a720-3ea7ee5d2bfd")).WithSingularName("PurchaseShipmentObjectState").WithPluralName("PurchaseShipmentObjectStates").Build();
            var SalesOrderItemObjectState = new ClassBuilder(domain, new Guid("21f09e4c-7b3f-4152-8822-8c485011759c")).WithSingularName("SalesOrderItemObjectState").WithPluralName("SalesOrderItemObjectStates").Build();
            var BankAccount = new ClassBuilder(domain, new Guid("22bc5b67-8015-49c5-bc47-6f9e7e678943")).WithSingularName("BankAccount").WithPluralName("BankAccounts").Build();
            var ServiceEntryHeader = new ClassBuilder(domain, new Guid("22e85314-cfdf-4ead-a816-18588294fa79")).WithSingularName("ServiceEntryHeader").WithPluralName("ServiceEntryHeaders").Build();
            var PartRevision = new ClassBuilder(domain, new Guid("22f87630-11dd-480e-a721-9836af7685b1")).WithSingularName("PartRevision").WithPluralName("PartRevisions").Build();
            var PurchaseReturnObjectState = new ClassBuilder(domain, new Guid("23162c0f-f5ec-45a5-a948-13a3355d99f2")).WithSingularName("PurchaseReturnObjectState").WithPluralName("PurchaseReturnObjectStates").Build();
            var ProductConfiguration = new ClassBuilder(domain, new Guid("23503dae-02ff-4dae-950e-d699dcb12a3c")).WithSingularName("ProductConfiguration").WithPluralName("ProductConfigurations").Build();
            var OwnCreditCard = new ClassBuilder(domain, new Guid("23848955-69ae-40ce-b973-0d416ae80c78")).WithSingularName("OwnCreditCard").WithPluralName("OwnCreditCards").Build();
            var Dimension = new ClassBuilder(domain, new Guid("26981f3f-f683-4a59-91ad-7a0e4243aea6")).WithSingularName("Dimension").WithPluralName("Dimensions").Build();
            var SalesInvoiceItemType = new ClassBuilder(domain, new Guid("26f60d84-0659-4874-9c00-d6f3db11f073")).WithSingularName("SalesInvoiceItemType").WithPluralName("SalesInvoiceItemTypes").Build();
            var Model = new ClassBuilder(domain, new Guid("273e69b7-6cda-44d4-b1d6-605b32a6a70d")).WithSingularName("Model").WithPluralName("Models").Build();
            var FinancialAccount = new InterfaceBuilder(domain, new Guid("27b45d45-459a-43cb-87b0-f8842ec56445")).WithSingularName("FinancialAccount").WithPluralName("FinancialAccounts").Build();
            var PickList = new ClassBuilder(domain, new Guid("27b6630a-35d0-4352-9223-b5b6c8d7496b")).WithSingularName("PickList").WithPluralName("PickLists").Build();
            var StoreRevenue = new ClassBuilder(domain, new Guid("282e0f1a-fda0-4287-a043-65dcc1853d95")).WithSingularName("StoreRevenue").WithPluralName("StoreRevenues").Build();
            var AgreementExhibit = new ClassBuilder(domain, new Guid("2830c388-b002-44d6-91b6-b2b43fa778f3")).WithSingularName("AgreementExhibit").WithPluralName("AgreementExhibits").Build();
            var ProductCategoryRevenueHistory = new ClassBuilder(domain, new Guid("29d8d5c2-58f9-41b9-914f-5dce7b69e908")).WithSingularName("ProductCategoryRevenueHistory").WithPluralName("ProductCategoryRevenueHistories").Build();
            var SalesRepRevenueHistory = new ClassBuilder(domain, new Guid("2a79b7f0-5998-4d52-b995-8412df939098")).WithSingularName("SalesRepRevenueHistory").WithPluralName("SalesRepRevenueHistories").Build();
            var EstimatedLaborCost = new ClassBuilder(domain, new Guid("2a84fcce-91f6-4d8b-9840-2ddd5f4b3dac")).WithSingularName("EstimatedLaborCost").WithPluralName("EstimatedLaborCosts").Build();
            var CostCenter = new ClassBuilder(domain, new Guid("2ab70094-5481-4ecc-ae15-cb2131fbc2f1")).WithSingularName("CostCenter").WithPluralName("CostCenters").Build();
            var SupplierRelationship = new ClassBuilder(domain, new Guid("2b162153-f74d-4f97-b97c-48f04749b216")).WithSingularName("SupplierRelationship").WithPluralName("SupplierRelationships").Build();
            var SkillRating = new ClassBuilder(domain, new Guid("2b44d390-bdd5-43aa-91c4-25b1966c46fb")).WithSingularName("SkillRating").WithPluralName("SkillRatings").Build();
            var EventRegistration = new ClassBuilder(domain, new Guid("2b5efcb9-54ba-4d59-833b-716d321cc7cb")).WithSingularName("EventRegistration").WithPluralName("EventRegistrations").Build();
            var Building = new ClassBuilder(domain, new Guid("2ba5e05c-f1ab-4143-ae7e-4db7389ff34e")).WithSingularName("Building").WithPluralName("Buildings").Build();
            var ServiceEntryBilling = new ClassBuilder(domain, new Guid("2be4075a-c7e3-4a38-a045-7910f85b3e46")).WithSingularName("ServiceEntryBilling").WithPluralName("ServiceEntryBillings").Build();
            var PurchaseShipment = new ClassBuilder(domain, new Guid("2bf859c6-de64-476f-a437-5eb57a778262")).WithSingularName("PurchaseShipment").WithPluralName("PurchaseShipments").Build();
            var UnitOfMeasureConversion = new ClassBuilder(domain, new Guid("2e216901-eab9-42e3-9e49-7fe8e88291d3")).WithSingularName("UnitOfMeasureConversion").WithPluralName("UnitOfMeasureConversions").Build();
            var VatRateUsage = new ClassBuilder(domain, new Guid("2e245d61-7739-4dfe-b108-7c9f0f4aed17")).WithSingularName("VatRateUsage").WithPluralName("VatRateUsages").Build();
            var Project = new ClassBuilder(domain, new Guid("2e2c567e-4c1f-4729-97a1-5ae203be936c")).WithSingularName("Project").WithPluralName("Projects").Build();
            var PaymentBudgetAllocation = new ClassBuilder(domain, new Guid("2e588028-5de2-411c-ab43-b406ca735d5b")).WithSingularName("PaymentBudgetAllocation").WithPluralName("PaymentBudgetAllocations").Build();
            var Hobby = new ClassBuilder(domain, new Guid("2f18f79f-dd13-4e89-b3fa-95d789dd383e")).WithSingularName("Hobby").WithPluralName("Hobbies").Build();
            var ProductRevenueHistory = new ClassBuilder(domain, new Guid("2fb4693c-d8c8-49fb-9d99-8ae1a9f43683")).WithSingularName("ProductRevenueHistory").WithPluralName("ProductRevenueHistories").Build();
            var OrderRequirementCommitment = new ClassBuilder(domain, new Guid("2fcdaf95-c3ec-4da2-8e7e-09c55741082f")).WithSingularName("OrderRequirementCommitment").WithPluralName("OrderRequirementCommitments").Build();
            var OrganisationRollUp = new ClassBuilder(domain, new Guid("316fc0d3-2dce-43aa-9b38-a60f964d5395")).WithSingularName("OrganisationRollUp").WithPluralName("OrganisationRollUps").Build();
            var Request = new InterfaceBuilder(domain, new Guid("321a6047-2233-4bec-a1b1-9b965c0099e5")).WithSingularName("Request").WithPluralName("Requests").Build();
            var AccountingTransactionType = new ClassBuilder(domain, new Guid("3277910f-c4ee-40b6-8028-21f879e8da04")).WithSingularName("AccountingTransactionType").WithPluralName("AccountingTransactionTypes").Build();
            var RevenueValueBreak = new ClassBuilder(domain, new Guid("32f8ea23-5ef9-4d2c-86d9-b6f67529c05d")).WithSingularName("RevenueValueBreak").WithPluralName("RevenueValueBreaks").Build();
            var Activity = new ClassBuilder(domain, new Guid("339a58af-4939-4eee-8028-0fd18119ec34")).WithSingularName("Activity").WithPluralName("Activities").Build();
            var WorkEffortAssignment = new ClassBuilder(domain, new Guid("33e9355b-b3db-43e0-a250-8ebc576e6221")).WithSingularName("WorkEffortAssignment").WithPluralName("WorkEffortAssignments").Build();
            var SoftwareFeature = new ClassBuilder(domain, new Guid("34047b37-545d-420f-ae79-2e05123cd623")).WithSingularName("SoftwareFeature").WithPluralName("SoftwareFeatures").Build();
            var FiscalYearInvoiceNumber = new ClassBuilder(domain, new Guid("341fa885-0161-406b-89e6-08b1c92cd3b3")).WithSingularName("FiscalYearInvoiceNumber").WithPluralName("FiscalYearInvoiceNumbers").Build();
            var GeographicBoundary = new InterfaceBuilder(domain, new Guid("3453c2e1-77a4-4fe8-b663-02bac689883a")).WithSingularName("GeographicBoundary").WithPluralName("GeographicBoundaries").Build();
            var SalesOrderStatus = new ClassBuilder(domain, new Guid("347ee1c4-5275-4ea7-a349-6bab2de45aff")).WithSingularName("SalesOrderStatus").WithPluralName("SalesOrderStatuses").Build();
            var BillingAccount = new ClassBuilder(domain, new Guid("34d08c66-6d7a-4089-b862-c93feda67ef1")).WithSingularName("BillingAccount").WithPluralName("BillingAccounts").Build();
            var SalesChannelRevenue = new ClassBuilder(domain, new Guid("354524c8-355e-4994-b07e-91fc6bcb06cf")).WithSingularName("SalesChannelRevenue").WithPluralName("SalesChannelRevenues").Build();
            var AutomatedAgent = new ClassBuilder(domain, new Guid("3587d2e1-c3f6-4c55-a96c-016e0501d99c")).WithSingularName("AutomatedAgent").WithPluralName("AutomatedAgents").Build();
            var SalesChannelRevenueHistory = new ClassBuilder(domain, new Guid("35d5e80f-e65f-4b0d-9e81-d1604b36a5e3")).WithSingularName("SalesChannelRevenueHistory").WithPluralName("SalesChannelRevenueHistories").Build();
            var Proposal = new ClassBuilder(domain, new Guid("360cf15d-c360-4d68-b693-7d1544388169")).WithSingularName("Proposal").WithPluralName("Proposals").Build();
            var FinishedGood = new ClassBuilder(domain, new Guid("364071a2-bcda-4bdc-b0f9-0e56d28604d6")).WithSingularName("FinishedGood").WithPluralName("FinishedGoods").Build();
            var PerformanceSpecification = new ClassBuilder(domain, new Guid("37b665a5-9f73-4002-b7d2-7ed6987fe09a")).WithSingularName("PerformanceSpecification").WithPluralName("PerformanceSpecifications").Build();
            var ProductionRun = new ClassBuilder(domain, new Guid("37de59b2-ca6c-4fa9-86a2-299fd6f14812")).WithSingularName("ProductionRun").WithPluralName("ProductionRuns").Build();
            var PriceComponent = new InterfaceBuilder(domain, new Guid("383589fb-f410-4d22-ade6-aa5126fdef18")).WithSingularName("PriceComponent").WithPluralName("PriceComponents").Build();
            var Ordinal = new ClassBuilder(domain, new Guid("385a2ae6-368c-4c3f-ad34-f8d69d8ca6cd")).WithSingularName("Ordinal").WithPluralName("Ordinals").Build();
            var Citizenship = new ClassBuilder(domain, new Guid("38b0ac1b-497c-4286-976e-64b3d523ad9d")).WithSingularName("Citizenship").WithPluralName("Citizenships").Build();
            var PartyProductRevenue = new ClassBuilder(domain, new Guid("3a0364f4-d872-4c47-a3ef-73d624128693")).WithSingularName("PartyProductRevenue").WithPluralName("PartyProductRevenues").Build();
            var ShipmentMethod = new ClassBuilder(domain, new Guid("3a3e6acf-48f4-4a33-848c-0c77cb18693a")).WithSingularName("ShipmentMethod").WithPluralName("ShipmentMethods").Build();
            var Organisation = new ClassBuilder(domain, new Guid("3a5dcec7-308f-48c7-afee-35d38415aa0b")).WithSingularName("Organisation").WithPluralName("Organisations").Build();
            var Responsibility = new ClassBuilder(domain, new Guid("3aa7bf17-bd02-4587-9006-177845ae69df")).WithSingularName("Responsibility").WithPluralName("Responsibilities").Build();
            var VatReturnBoxType = new ClassBuilder(domain, new Guid("3b233161-d2a8-4d8f-a293-09d8a2bea3e2")).WithSingularName("VatReturnBoxType").WithPluralName("VatReturnBoxTypes").Build();
            var DebitCreditConstant = new ClassBuilder(domain, new Guid("3b330b42-b359-4de7-a084-cc96ce1e6420")).WithSingularName("DebitCreditConstant").WithPluralName("DebitCreditConstants").Build();
            var WorkEffortFixedAssetAssignment = new ClassBuilder(domain, new Guid("3b43da7f-5252-4824-85fe-c85d6864838a")).WithSingularName("WorkEffortFixedAssetAssignment").WithPluralName("WorkEffortFixedAssetAssignments").Build();
            var VatCalculationMethod = new ClassBuilder(domain, new Guid("3b73eea7-6455-4fe5-87c0-99c852f57e6b")).WithSingularName("VatCalculationMethod").WithPluralName("VatCalculationMethods").Build();
            var GeographicBoundaryComposite = new InterfaceBuilder(domain, new Guid("3b7ac95a-fdab-488d-b599-17ef9fcf33b0")).WithSingularName("GeographicBoundaryComposite").WithPluralName("GeographicBoundariesComposites").Build();
            var InvoiceSequence = new ClassBuilder(domain, new Guid("3b8e751c-6778-44cb-93a0-d35b86b724e0")).WithSingularName("InvoiceSequence").WithPluralName("InvoiceSequences").Build();
            var CustomerRelationship = new ClassBuilder(domain, new Guid("3b9f21f4-2f2c-47a9-9c76-15f5ef4f5e00")).WithSingularName("CustomerRelationship").WithPluralName("CustomerRelationships").Build();
            var PartyClassification = new ClassBuilder(domain, new Guid("3bb83aa5-e58a-4421-bdbc-3c9fa0b2324f")).WithSingularName("PartyClassification").WithPluralName("PartyClassifications").Build();
            var Party = new InterfaceBuilder(domain, new Guid("3bba6e5a-dc2d-4838-b6c4-881f6c8c3013")).WithSingularName("Party").WithPluralName("Parties").Build();
            var PartyProductCategoryRevenue = new ClassBuilder(domain, new Guid("3f2c4c17-ec80-44ad-b452-76cf694f3d6a")).WithSingularName("PartyProductCategoryRevenue").WithPluralName("PartyProductCategoryRevenues").Build();
            var PartyFixedAssetAssignment = new ClassBuilder(domain, new Guid("40ee178e-7564-4dfa-ab6f-8bcd4e62b498")).WithSingularName("PartyFixedAssetAssignment").WithPluralName("PartyFixedAssetAssignments").Build();
            var CapitalBudget = new ClassBuilder(domain, new Guid("41f1aa5a-5043-42bb-aaf5-7d57a9deaccb")).WithSingularName("CapitalBudget").WithPluralName("CapitalBudgets").Build();
            var AccountAdjustment = new ClassBuilder(domain, new Guid("4211ece6-a127-4359-9fa4-6537943a37a5")).WithSingularName("AccountAdjustment").WithPluralName("AccountAdjustments").Build();
            var PositionStatus = new ClassBuilder(domain, new Guid("4250a005-4fec-4118-a5b4-725886c59269")).WithSingularName("PositionStatus").WithPluralName("PositionStatuses").Build();
            var MarketingPackage = new ClassBuilder(domain, new Guid("42adee8e-5994-42e3-afe1-aa3d3089d594")).WithSingularName("MarketingPackage").WithPluralName("MarketingPackages").Build();
            var ItemIssuance = new ClassBuilder(domain, new Guid("441f6007-022d-4d77-bc2d-04c7a876e1bd")).WithSingularName("ItemIssuance").WithPluralName("ItemIssuances").Build();
            var ShipmentPackage = new ClassBuilder(domain, new Guid("444e431b-f078-46e0-9c8e-694e15e807c7")).WithSingularName("ShipmentPackage").WithPluralName("ShipmentPackages").Build();
            var CommunicationAttachment = new InterfaceBuilder(domain, new Guid("452ae775-def1-4e75-b325-2e9184eb8c1f")).WithSingularName("CommunicationAttachment").WithPluralName("CommunicationAttachments").Build();
            var PurchaseOrderObjectState = new ClassBuilder(domain, new Guid("45e4f0da-9a6b-4077-bcc4-d49d9ec4cc97")).WithSingularName("PurchaseOrderObjectState").WithPluralName("PurchaseOrderObjectStates").Build();
            var Size = new ClassBuilder(domain, new Guid("45f5a73c-34d8-4452-8f22-7a744bd6650b")).WithSingularName("Size").WithPluralName("Sizes").Build();
            var PerformanceNote = new ClassBuilder(domain, new Guid("4629c7ed-e9a4-4f31-bb46-e3f2920bd768")).WithSingularName("PerformanceNote").WithPluralName("PerformanceNotes").Build();
            var DeliverableTurnover = new ClassBuilder(domain, new Guid("48733d8e-506a-4add-a230-907221ca7a9a")).WithSingularName("DeliverableTurnover").WithPluralName("DeliverableTurnovers").Build();
            var ShipmentReceipt = new ClassBuilder(domain, new Guid("48d14522-5fa8-44a8-ba4c-e2ddfc18e069")).WithSingularName("ShipmentReceipt").WithPluralName("ShipmentReceipts").Build();
            var RequirementCommunication = new ClassBuilder(domain, new Guid("49cdc4a2-f7af-43c9-b160-4c7da9a0ca42")).WithSingularName("RequirementCommunication").WithPluralName("RequirementCommunications").Build();
            var FixedAsset = new InterfaceBuilder(domain, new Guid("4a3efb9c-1556-4e57-bb59-f09d297e607e")).WithSingularName("FixedAsset").WithPluralName("FixedAssets").Build();
            var ServiceEntry = new InterfaceBuilder(domain, new Guid("4a4a0548-b75f-4a79-89aa-f5c242121f11")).WithSingularName("ServiceEntry").WithPluralName("ServiceEntries").Build();
            var GeneralLedgerAccountGroup = new ClassBuilder(domain, new Guid("4a600c96-b813-46fc-8674-06bd3f85eae4")).WithSingularName("GeneralLedgerAccountGroup").WithPluralName("GeneralLedgerAccountGroups").Build();
            var SerializedInventoryItem = new ClassBuilder(domain, new Guid("4a70cbb3-6e23-4118-a07d-d611de9297de")).WithSingularName("SerializedInventoryItem").WithPluralName("SerializedInventoryItems").Build();
            var ItemVarianceAccountingTransaction = new ClassBuilder(domain, new Guid("4af573b7-a87f-400c-97e4-80bda17376e0")).WithSingularName("ItemVarianceAccountingTransaction").WithPluralName("ItemVarianceAccountingTransactions").Build();
            var RespondingParty = new ClassBuilder(domain, new Guid("4b1e9776-8851-4a2a-a402-1b40211d1f3b")).WithSingularName("RespondingParty").WithPluralName("RespondingParties").Build();
            var SalesInvoiceItemObjectState = new ClassBuilder(domain, new Guid("4babdd0c-52dd-4fb8-bbf5-120aa58eff50")).WithSingularName("SalesInvoiceItemObjectState").WithPluralName("SalesInvoiceItemObjectStates").Build();
            var BudgetStatus = new ClassBuilder(domain, new Guid("4c163351-b42e-4bd3-8cbf-db110eba05fc")).WithSingularName("BudgetStatus").WithPluralName("BudgetStatuses").Build();
            var Barrel = new ClassBuilder(domain, new Guid("4cd7ab57-544c-4900-a854-4aa9c5284b81")).WithSingularName("Barrel").WithPluralName("Barrels").Build();
            var PositionType = new ClassBuilder(domain, new Guid("4d599ed2-c5e3-4c1d-8128-6ff61f9072c3")).WithSingularName("PositionType").WithPluralName("PositionTypes").Build();
            var Agreement = new InterfaceBuilder(domain, new Guid("4deca253-7135-4ceb-b984-6adaf1515630")).WithSingularName("Agreement").WithPluralName("Agreements").Build();
            var ProductPurchasePrice = new ClassBuilder(domain, new Guid("4e2d5dee-1dcf-4c14-8acc-d60fd47a3400")).WithSingularName("ProductPurchasePrice").WithPluralName("ProductPurchasePrices").Build();
            var Carrier = new ClassBuilder(domain, new Guid("4f46f32a-04e6-4ccc-829b-68fb3336f870")).WithSingularName("Carrier").WithPluralName("Carriers").Build();
            var Resume = new ClassBuilder(domain, new Guid("4f7703b0-7201-4f7a-a0b4-f177d64a2c31")).WithSingularName("Resume").WithPluralName("Resumes").Build();
            var WebAddress = new ClassBuilder(domain, new Guid("5138c0e3-1b28-4297-bf45-697624ee5c19")).WithSingularName("WebAddress").WithPluralName("WebAddresses").Build();
            var ProjectRequirement = new ClassBuilder(domain, new Guid("51d0b6f6-221b-44d5-9a0b-9a880620b1ad")).WithSingularName("ProjectRequirement").WithPluralName("ProjectRequirements").Build();
            var Deposit = new ClassBuilder(domain, new Guid("52458d42-94ee-4757-bcfb-bc9c45ed6dc6")).WithSingularName("Deposit").WithPluralName("Deposits").Build();
            var LegalForm = new ClassBuilder(domain, new Guid("528cf616-6c67-42e1-af69-b5e6cb1192ea")).WithSingularName("LegalForm").WithPluralName("LegalForms").Build();
            var CostOfGoodsSoldMethod = new ClassBuilder(domain, new Guid("52ee223f-14e7-46e7-8e24-c6fdf19fa5d1")).WithSingularName("CostOfGoodsSoldMethod").WithPluralName("CostOfGoodsSoldMethods").Build();
            var StatementOfWork = new ClassBuilder(domain, new Guid("5459f555-cf6a-49c1-8015-b43cad74da17")).WithSingularName("StatementOfWork").WithPluralName("StatementsOfWork").Build();
            var FinancialAccountTransaction = new InterfaceBuilder(domain, new Guid("5500cb42-1aae-4816-9bc1-d63ff273f144")).WithSingularName("FinancialAccountTransaction").WithPluralName("FinancialAccountTransactions").Build();
            var WorkEffort = new InterfaceBuilder(domain, new Guid("553a5280-a768-4ba1-8b5d-304d7c4bb7f1")).WithSingularName("WorkEffort").WithPluralName("WorkEfforts").Build();
            var SkillLevel = new ClassBuilder(domain, new Guid("555882ea-d25a-4da2-a8ea-330469c8cd41")).WithSingularName("SkillLevel").WithPluralName("SkillLevels").Build();
            var PickListStatus = new ClassBuilder(domain, new Guid("563c9706-0b34-4bf0-a09f-72881f10fe6c")).WithSingularName("PickListStatus").WithPluralName("PickListStatuses").Build();
            var Product = new InterfaceBuilder(domain, new Guid("56b79619-d04a-4924-96e8-e3e7be9faa09")).WithSingularName("Product").WithPluralName("Products").Build();
            var TaxDue = new ClassBuilder(domain, new Guid("57b74174-1418-4307-96f7-e579638d7dd9")).WithSingularName("TaxDue").WithPluralName("TaxDues").Build();
            var OneTimeCharge = new ClassBuilder(domain, new Guid("5835aca6-214b-41cf-aefe-e361dda026d7")).WithSingularName("OneTimeCharge").WithPluralName("OneTimeCharges").Build();
            var Note = new ClassBuilder(domain, new Guid("587e017d-eb9a-412c-bd21-8ff91c42765b")).WithSingularName("Note").WithPluralName("Notes").Build();
            var PartBillOfMaterialSubstitute = new ClassBuilder(domain, new Guid("5906f4cd-3950-43ee-a3ba-84124c4180f6")).WithSingularName("PartBillOfMaterialSubstitute").WithPluralName("PartBillOfMaterialSubstitutes").Build();
            var Receipt = new ClassBuilder(domain, new Guid("592260cc-365c-4769-b067-e95dd49609f5")).WithSingularName("Receipt").WithPluralName("Receipts").Build();
            var RequirementBudgetAllocation = new ClassBuilder(domain, new Guid("5990c1d7-02d5-4e0d-8073-657b0dbfc5e1")).WithSingularName("RequirementBudgetAllocation").WithPluralName("RequirementBudgetAllocations").Build();
            var OrganisationGlAccount = new ClassBuilder(domain, new Guid("59f3100c-da48-4b4c-a302-1a75e37216a6")).WithSingularName("OrganisationGlAccount").WithPluralName("OrganisationGlAccounts").Build();
            var InternalAccountingTransaction = new InterfaceBuilder(domain, new Guid("5a783d98-845a-4784-9c92-5c75a4af3fb8")).WithSingularName("InternalAccountingTransaction").WithPluralName("InternalAccountingTransactions").Build();
            var Maintenance = new ClassBuilder(domain, new Guid("5ad24730-a81e-4160-9af9-fa25342a5e96")).WithSingularName("Maintenance").WithPluralName("Maintenances").Build();
            var NonSerializedInventoryItem = new ClassBuilder(domain, new Guid("5b294591-e20a-4bad-940a-27ae7b2f8770")).WithSingularName("NonSerializedInventoryItem").WithPluralName("NonSerializedInventoryItems").Build();
            var CreditLine = new ClassBuilder(domain, new Guid("5bdc88b6-c45f-4835-aa50-26405f1314e3")).WithSingularName("CreditLine").WithPluralName("CreditLines").Build();
            var BillOfLading = new ClassBuilder(domain, new Guid("5c5c17d1-2132-403b-8819-e3c1aa7bd6a9")).WithSingularName("BillOfLading").WithPluralName("BillOfLadings").Build();
            var UnitOfMeasure = new ClassBuilder(domain, new Guid("5cd7ea86-8bc6-4b72-a8f6-788e6453acdc")).WithSingularName("UnitOfMeasure").WithPluralName("UnitsOfMeasure").Build();
            var ElectronicAddress = new InterfaceBuilder(domain, new Guid("5cd86f69-e09b-4150-a2a6-2eed4c72b426")).WithSingularName("ElectronicAddress").WithPluralName("ElectronicAddresses").Build();
            var ServiceConfiguration = new ClassBuilder(domain, new Guid("5d4beea4-f480-460e-92ee-3e8d532ac7f9")).WithSingularName("ServiceConfiguration").WithPluralName("ServiceConfigurations").Build();
            var NeededSkill = new ClassBuilder(domain, new Guid("5e31a968-5f7d-4109-9821-b94459f13382")).WithSingularName("NeededSkill").WithPluralName("NeededSkills").Build();
            var Room = new ClassBuilder(domain, new Guid("5f16236a-0fa4-4866-9b3d-3951edbd4c81")).WithSingularName("Room").WithPluralName("Rooms").Build();
            var Plant = new ClassBuilder(domain, new Guid("616a603d-d441-4408-8c43-179a1502dc64")).WithSingularName("Plant").WithPluralName("Plants").Build();
            var SalesInvoice = new ClassBuilder(domain, new Guid("6173fc23-115f-4356-a0ce-867872c151ac")).WithSingularName("SalesInvoice").WithPluralName("SalesInvoices").Build();
            var InventoryItem = new InterfaceBuilder(domain, new Guid("61af6d19-e8e4-4b5b-97e8-3610fbc82605")).WithSingularName("InventoryItem").WithPluralName("InventoryItems").Build();
            var StandardServiceOrderItem = new ClassBuilder(domain, new Guid("622a0738-338e-454e-a8ca-4a8fa3e9d9a4")).WithSingularName("StandardServiceOrderItem").WithPluralName("StandardServiceOrderItems").Build();
            var PurchaseInvoiceStatus = new ClassBuilder(domain, new Guid("622c8a98-ec26-4f05-9a09-a9032a41e586")).WithSingularName("PurchaseInvoiceStatus").WithPluralName("PurchaseInvoiceStatuses").Build();
            var Region = new ClassBuilder(domain, new Guid("62693ee8-1fd3-4b2b-85ce-8d88df3bba0c")).WithSingularName("Region").WithPluralName("Regions").Build();
            var SalesTerritory = new ClassBuilder(domain, new Guid("62ea5285-b9d8-4a41-9c14-79c712fd3bf4")).WithSingularName("SalesTerritory").WithPluralName("SalesTerritories").Build();
            var TimeEntry = new ClassBuilder(domain, new Guid("6360b45d-3556-41c6-b183-f42a15b9424f")).WithSingularName("TimeEntry").WithPluralName("TimeEntries").Build();
            var DepreciationMethod = new ClassBuilder(domain, new Guid("63ca0535-95e5-4b2d-847d-d619a5365605")).WithSingularName("DepreciationMethod").WithPluralName("DepreciationMethods").Build();
            var AssetAssignmentStatus = new ClassBuilder(domain, new Guid("644660d4-d5d0-4bd9-8cba-17696af0b9ed")).WithSingularName("AssetAssignmentStatus").WithPluralName("AssetAssignmentStatuses").Build();
            var StoreRevenueHistory = new ClassBuilder(domain, new Guid("648d1b19-d3f8-4ace-86bc-b113827f5e8e")).WithSingularName("StoreRevenueHistory").WithPluralName("StoreRevenueHistories").Build();
            var PersonTraining = new ClassBuilder(domain, new Guid("6674e32d-c139-4c99-97c5-92354d3ccc4c")).WithSingularName("PersonTraining").WithPluralName("PersonTrainings").Build();
            var DeductionType = new ClassBuilder(domain, new Guid("66b30b62-5e6c-4747-a72e-bc4ac2cb1125")).WithSingularName("DeductionType").WithPluralName("DeductionTypes").Build();
            var DeliverableOrderItem = new ClassBuilder(domain, new Guid("66bd584c-37c4-4969-874b-7a459195fd25")).WithSingularName("DeliverableOrderItem").WithPluralName("DeliverableOrderItems").Build();
            var PackagingSlip = new ClassBuilder(domain, new Guid("66e7dcf3-90bc-4ac6-988f-54015f5bef11")).WithSingularName("PackagingSlip").WithPluralName("PackagingSlips").Build();
            var CustomerReturnObjectState = new ClassBuilder(domain, new Guid("671951f1-78fd-4b05-ac15-eafb2a35a6f8")).WithSingularName("CustomerReturnObjectState").WithPluralName("CustomerReturnObjectStates").Build();
            var OrganisationGlAccountBalance = new ClassBuilder(domain, new Guid("67a8352d-7fe0-4398-93c3-50ec8d3e8038")).WithSingularName("OrganisationGlAccountBalance").WithPluralName("OrganisationGlAccountBalances").Build();
            var InternalOrganisationRevenueHistory = new ClassBuilder(domain, new Guid("684ce40f-6950-4163-b110-e83e65c31f0a")).WithSingularName("InternalOrganisationRevenueHistory").WithPluralName("InternalOrganisationRevenueHistories").Build();
            var ManufacturingBom = new ClassBuilder(domain, new Guid("68a0c645-4671-4dda-87a5-53395934a9fc")).WithSingularName("ManufacturingBom").WithPluralName("ManufacturingBoms").Build();
            var Deliverable = new ClassBuilder(domain, new Guid("68a6803d-0e65-4141-ac51-25f4c2e49914")).WithSingularName("Deliverable").WithPluralName("Deliverables").Build();
            var EmploymentApplication = new ClassBuilder(domain, new Guid("6940c300-47e6-44f2-b93b-d70bed9de602")).WithSingularName("EmploymentApplication").WithPluralName("EmploymentApplications").Build();
            var VatRegime = new ClassBuilder(domain, new Guid("69db99bc-97f7-4e2e-903c-74afb55992af")).WithSingularName("VatRegime").WithPluralName("VatRegimes").Build();
            var PositionFulfillment = new ClassBuilder(domain, new Guid("6a03924c-914b-4660-b7e8-5174caa0dff9")).WithSingularName("PositionFulfillment").WithPluralName("PositionFulfillments").Build();
            var Employment = new ClassBuilder(domain, new Guid("6a7e45b2-36b2-4d2e-a29c-0cc13851766f")).WithSingularName("Employment").WithPluralName("Employments").Build();
            var AccountingPeriod = new ClassBuilder(domain, new Guid("6b56e13b-d075-40f1-8e33-a9a4c6cadb96")).WithSingularName("AccountingPeriod").WithPluralName("AccountingPeriods").Build();
            var EngagementRate = new ClassBuilder(domain, new Guid("6b666a30-7a55-4986-8411-b6179768e70b")).WithSingularName("EngagementRate").WithPluralName("EngagementRates").Build();
            var ExternalAccountingTransaction = new InterfaceBuilder(domain, new Guid("6bfa631c-80f4-495f-bb9a-0d3351390d64")).WithSingularName("ExternalAccountingTransaction").WithPluralName("ExternalAccountingTransactions").Build();
            var TelecommunicationsNumber = new ClassBuilder(domain, new Guid("6c255f71-ce18-4d51-b0d9-e402ec0e570e")).WithSingularName("TelecommunicationsNumber").WithPluralName("TelecommunicationsNumbers").Build();
            var SalesRepRelationship = new ClassBuilder(domain, new Guid("6c28f40a-1826-4110-83c8-7eaefc797f1a")).WithSingularName("SalesRepRelationship").WithPluralName("SalesRepRelationships").Build();
            var PurchaseInvoiceObjectState = new ClassBuilder(domain, new Guid("6c485526-bf9e-42e0-b47e-84552a72589a")).WithSingularName("PurchaseInvoiceObjectState").WithPluralName("PurchaseInvoiceObjectStates").Build();
            var ProductCategoryRevenue = new ClassBuilder(domain, new Guid("6c8503ec-3796-4861-af47-b1aa4e911292")).WithSingularName("ProductCategoryRevenue").WithPluralName("ProductCategoryRevenues").Build();
            var ChartOfAccounts = new ClassBuilder(domain, new Guid("6cf4845d-65a0-4957-95e9-f2b5327d6515")).WithSingularName("ChartOfAccounts").WithPluralName("ChartsOfAccounts").Build();
            var PartyRevenue = new ClassBuilder(domain, new Guid("6cf7d076-5c39-48b5-a27e-5e7752afee2d")).WithSingularName("PartyRevenue").WithPluralName("PartyRevenues").Build();
            var MarketingMaterial = new ClassBuilder(domain, new Guid("6d4739a9-c3c4-4570-a337-49f667c6243b")).WithSingularName("MarketingMaterial").WithPluralName("MarketingMaterials").Build();
            var PurchaseInvoiceItemStatus = new ClassBuilder(domain, new Guid("6dd3dbf4-d14d-45a3-ad52-65b31a4bb24e")).WithSingularName("PurchaseInvoiceItemStatus").WithPluralName("PurchaseInvoiceItemStatuses").Build();
            var InvoiceVatRateItem = new ClassBuilder(domain, new Guid("6e380347-21e3-4a00-819f-ed11e6882d03")).WithSingularName("InvoiceVatRateItem").WithPluralName("InvoiceVatRateItems").Build();
            var CaseObjectState = new ClassBuilder(domain, new Guid("6ea1f500-13a2-4f5a-8026-a1d5a57170ac")).WithSingularName("CaseObjectState").WithPluralName("CaseObjectStates").Build();
            var SalaryStep = new ClassBuilder(domain, new Guid("6ebf4c66-dd19-494f-8081-67d7a10a16fc")).WithSingularName("SalaryStep").WithPluralName("SalarySteps").Build();
            var DropShipmentStatus = new ClassBuilder(domain, new Guid("6fadcefe-2972-480d-9d38-d4207e199d48")).WithSingularName("DropShipmentStatus").WithPluralName("DropShipmentStatuses").Build();
            var PaymentApplication = new ClassBuilder(domain, new Guid("6fef08f0-d4cb-42f4-a10f-fb31787f65c3")).WithSingularName("PaymentApplication").WithPluralName("PaymentApplications").Build();
            var NonSerializedInventoryItemStatus = new ClassBuilder(domain, new Guid("700360b9-56be-4e51-9610-f1e5951dd765")).WithSingularName("NonSerializedInventoryItemStatus").WithPluralName("NonSerializedInventoryItemStatuses").Build();
            var SurchargeAdjustment = new ClassBuilder(domain, new Guid("70468d86-b8a0-4aff-881e-fca2386f64da")).WithSingularName("SurchargeAdjustment").WithPluralName("SurchargeAdjustments").Build();
            var Depreciation = new ClassBuilder(domain, new Guid("7107db4e-8406-4fe3-8136-271077c287f8")).WithSingularName("Depreciation").WithPluralName("Depreciations").Build();
            var Territory = new ClassBuilder(domain, new Guid("7118e029-a8b3-415b-b9e9-d48ba4ea2823")).WithSingularName("Territory").WithPluralName("Territories").Build();
            var SalesOrder = new ClassBuilder(domain, new Guid("716647bf-7589-4146-a45c-a6a3b1cee507")).WithSingularName("SalesOrder").WithPluralName("SalesOrders").Build();
            var Warehouse = new ClassBuilder(domain, new Guid("71e50a16-fc60-4177-aed0-e89c7f10f465")).WithSingularName("Warehouse").WithPluralName("Warehouses").Build();
            var AgreementPricingProgram = new ClassBuilder(domain, new Guid("72237d95-e9c0-42c1-afe3-ec34f2e6cbfb")).WithSingularName("AgreementPricingProgram").WithPluralName("AgreementPricingPrograms").Build();
            var AgreementTerm = new InterfaceBuilder(domain, new Guid("734be1c9-e6af-49b7-8fe8-331cd7036e2e")).WithSingularName("AgreementTerm").WithPluralName("AgreementTerms").Build();
            var SalesRepRevenue = new ClassBuilder(domain, new Guid("749e2a92-b397-4d36-b965-6073d45a4135")).WithSingularName("SalesRepRevenue").WithPluralName("SalesRepRevenues").Build();
            var EmploymentApplicationSource = new ClassBuilder(domain, new Guid("74cd22cf-1796-4c65-85df-9c3e09883843")).WithSingularName("EmploymentApplicationSource").WithPluralName("EmploymentApplicationSources").Build();
            var Engagement = new ClassBuilder(domain, new Guid("752a68b0-836e-4cd5-92d5-ebf2bfeda491")).WithSingularName("Engagement").WithPluralName("Engagements").Build();
            var Part = new InterfaceBuilder(domain, new Guid("75916246-b1b5-48ef-9578-d65980fd2623")).WithSingularName("Part").WithPluralName("Parts").Build();
            var InventoryItemKind = new ClassBuilder(domain, new Guid("759f97a9-3105-49b4-81a0-c94c3700397c")).WithSingularName("InventoryItemKind").WithPluralName("InventoryItemKinds").Build();
            var CustomEngagementItem = new ClassBuilder(domain, new Guid("78022da7-d11c-4ab7-96f5-099d6608c4bb")).WithSingularName("CustomEngagementItem").WithPluralName("CustomEngagementItems").Build();
            var AccountingTransaction = new InterfaceBuilder(domain, new Guid("785a36a9-4710-4f3f-bd26-dbaff5353535")).WithSingularName("AccountingTransaction").WithPluralName("AccountingTransactions").Build();
            var SalesRepPartyRevenue = new ClassBuilder(domain, new Guid("7b0e5009-eef2-4043-8794-b94663397053")).WithSingularName("SalesRepPartyRevenue").WithPluralName("SalesRepPartyRevenues").Build();
            var JournalType = new ClassBuilder(domain, new Guid("7b23440c-d26b-42f5-a94b-e26872e63e7d")).WithSingularName("JournalType").WithPluralName("JournalTypes").Build();
            var PurchaseOrderItemStatus = new ClassBuilder(domain, new Guid("7ba40817-7e42-484e-8272-29a433842054")).WithSingularName("PurchaseOrderItemStatus").WithPluralName("PurchaseOrderItemStatuses").Build();
            var Addendum = new ClassBuilder(domain, new Guid("7baa7594-6890-4e1e-8c06-fc49b3ea262d")).WithSingularName("Addendum").WithPluralName("Addenda").Build();
            var Floor = new ClassBuilder(domain, new Guid("7c0d1b2d-88bf-41dd-b19d-b6b0ed1cb179")).WithSingularName("Floor").WithPluralName("Floors").Build();
            var WorkEffortType = new ClassBuilder(domain, new Guid("7d2d9452-f250-47c3-81e0-4e1c0655cc86")).WithSingularName("WorkEffortType").WithPluralName("WorkEffortTypes").Build();
            var SalesInvoiceStatus = new ClassBuilder(domain, new Guid("7d3a207b-dbdd-48c4-9a92-8b12e4e77874")).WithSingularName("SalesInvoiceStatus").WithPluralName("SalesInvoiceStatuses").Build();
            var SalesAgreement = new ClassBuilder(domain, new Guid("7d620a47-475b-40de-a4a7-8be7994df18e")).WithSingularName("SalesAgreement").WithPluralName("SalesAgreements").Build();
            var PurchaseInvoice = new ClassBuilder(domain, new Guid("7d7e4b6d-eebd-460c-b771-a93cd8d64bce")).WithSingularName("PurchaseInvoice").WithPluralName("PurchaseInvoices").Build();
            var CustomerReturn = new ClassBuilder(domain, new Guid("7dd7114a-9e74-45d5-b904-415514af5628")).WithSingularName("CustomerReturn").WithPluralName("CustomerReturns").Build();
            var Order = new InterfaceBuilder(domain, new Guid("7dde949a-6f54-4ece-92b3-d269f50ef9d9")).WithSingularName("Order").WithPluralName("Orders").Build();
            var PartyPackageRevenueHistory = new ClassBuilder(domain, new Guid("7e9b7f2f-887a-491d-8dab-6a42c908d5a5")).WithSingularName("PartyPackageRevenueHistory").WithPluralName("PartyPackageRevenueHistories").Build();
            var OrderKind = new ClassBuilder(domain, new Guid("7f13c77f-1ef1-446d-928d-1c96f9fc8b05")).WithSingularName("OrderKind").WithPluralName("OrderKinds").Build();
            var Amortization = new ClassBuilder(domain, new Guid("7fd1760c-ee1f-4d04-8a93-dfebc82757c1")).WithSingularName("Amortization").WithPluralName("Amortizations").Build();
            var PickListItem = new ClassBuilder(domain, new Guid("7fec090e-3d4a-4ec7-895f-4b30d01f59bb")).WithSingularName("PickListItem").WithPluralName("PickListItems").Build();
            var SalesOrderItem = new ClassBuilder(domain, new Guid("80de925c-04cc-412c-83a5-60405b0e63e6")).WithSingularName("SalesOrderItem").WithPluralName("SalesOrderItems").Build();
            var SalesInvoiceType = new ClassBuilder(domain, new Guid("81c9eefa-9b8b-40c0-9f1e-e6ecc2fef119")).WithSingularName("SalesInvoiceType").WithPluralName("SalesInvoiceTypes").Build();
            var WorkEffortGoodStandard = new ClassBuilder(domain, new Guid("81ddff76-9b82-4309-9c9f-f7f9dbd2db21")).WithSingularName("WorkEffortGoodStandard").WithPluralName("WorkEffortGoodStandards").Build();
            var Passport = new ClassBuilder(domain, new Guid("827bc38b-6570-41d7-8ae1-f1bbdf4409f9")).WithSingularName("Passport").WithPluralName("Passports").Build();
            var AmountDue = new ClassBuilder(domain, new Guid("848053ee-18d8-4962-81c3-bd6c7837565a")).WithSingularName("AmountDue").WithPluralName("AmountsDue").Build();
            var WorkEffortTypeKind = new ClassBuilder(domain, new Guid("8551adf6-5a97-41fe-aff8-6bec08b09d08")).WithSingularName("WorkEffortTypeKind").WithPluralName("WorkEffortTypeKinds").Build();
            var OrderTerm = new ClassBuilder(domain, new Guid("86cf6a28-baeb-479d-ac9e-fabc7fe1994d")).WithSingularName("OrderTerm").WithPluralName("OrderTerms").Build();
            var CreditCardCompany = new ClassBuilder(domain, new Guid("86d934de-a5cf-46d3-aad3-2626c43ebc85")).WithSingularName("CreditCardCompany").WithPluralName("CreditCardCompanies").Build();
            var RequestForQuote = new ClassBuilder(domain, new Guid("874dfe70-2e50-4861-b26d-dc55bc8fa0d0")).WithSingularName("RequestForQuote").WithPluralName("RequestsForQuote").Build();
            var PurchaseShipmentStatus = new ClassBuilder(domain, new Guid("87939632-40ff-4a3a-a874-74790e810890")).WithSingularName("PurchaseShipmentStatus").WithPluralName("PurchaseShipmentStatuses").Build();
            var Cash = new ClassBuilder(domain, new Guid("87fbf592-45a1-4ef2-85ca-f47d4c51abca")).WithSingularName("Cash").WithPluralName("Cashes").Build();
            var PerformanceReview = new ClassBuilder(domain, new Guid("89c49578-bb5d-4589-b908-bf09c6495011")).WithSingularName("PerformanceReview").WithPluralName("PerformanceReviews").Build();
            var DropShipmentObjectState = new ClassBuilder(domain, new Guid("89d2037a-4bc2-4929-b333-5358ac4a14e5")).WithSingularName("DropShipmentObjectState").WithPluralName("DropShipmentObjectStates").Build();
            var InvestmentAccount = new ClassBuilder(domain, new Guid("8a06c50b-5951-465e-86b8-43e733f20b90")).WithSingularName("InvestmentAccount").WithPluralName("InvestmentAccounts").Build();
            var AgreementItem = new InterfaceBuilder(domain, new Guid("8ba98e1b-1d4d-46b1-bf27-bb2bf53501fd")).WithSingularName("AgreementItem").WithPluralName("AgreementItems").Build();
            var Colour = new ClassBuilder(domain, new Guid("8bae9154-ec37-4139-b52c-6c3df860fb20")).WithSingularName("Colour").WithPluralName("Colours").Build();
            var PackageRevenue = new ClassBuilder(domain, new Guid("8bc2d0a0-a371-4292-9fd6-ecb1db838107")).WithSingularName("PackageRevenue").WithPluralName("PackageRevenues").Build();
            var SalesOrderObjectState = new ClassBuilder(domain, new Guid("8c993e3f-59a0-42f0-a0ef-d49f9beb0af6")).WithSingularName("SalesOrderObjectState").WithPluralName("SalesOrderObjectStates").Build();
            var Benefit = new ClassBuilder(domain, new Guid("8cea6932-d589-4b5b-99b8-ffba33936f8f")).WithSingularName("Benefit").WithPluralName("Benefits").Build();
            var EngineeringDocument = new ClassBuilder(domain, new Guid("8da5bb9b-593b-4c10-91c2-1e9cc2c226d1")).WithSingularName("EngineeringDocument").WithPluralName("EngineeringDocuments").Build();
            var VatReturnBox = new ClassBuilder(domain, new Guid("8dc67774-c15a-47dd-9b8a-ce4e7139e8a3")).WithSingularName("VatReturnBox").WithPluralName("VatReturnBoxes").Build();
            var CommunicationEventPurpose = new ClassBuilder(domain, new Guid("8e3fd781-f0b5-4e02-b1f6-6364d0559273")).WithSingularName("CommunicationEventPurpose").WithPluralName("CommunicationEventPurposes").Build();
            var ShipmentRouteSegment = new ClassBuilder(domain, new Guid("8e6eaa35-85da-4c80-848c-3f1ed6cd2f8a")).WithSingularName("ShipmentRouteSegment").WithPluralName("ShipmentRouteSegments").Build();
            var VarianceReason = new ClassBuilder(domain, new Guid("8ff46109-8ae7-4da5-a1f9-f19d4cf4e27e")).WithSingularName("VarianceReason").WithPluralName("VarianceReasons").Build();
            var Phase = new ClassBuilder(domain, new Guid("90a8fa64-c9c7-4a7a-a543-d500668619eb")).WithSingularName("Phase").WithPluralName("Phases").Build();
            var WorkEffortStatus = new ClassBuilder(domain, new Guid("90df16ba-ab97-4ec1-9db3-ab20314122bc")).WithSingularName("WorkEffortStatus").WithPluralName("WorkEffortStatuses").Build();
            var Salutation = new ClassBuilder(domain, new Guid("91d1ad08-2eae-4d9e-8a2e-223eeae138af")).WithSingularName("Salutation").WithPluralName("Salutations").Build();
            var PurchaseOrderStatus = new ClassBuilder(domain, new Guid("92b62390-9bf9-432b-b81e-242a5467e10e")).WithSingularName("PurchaseOrderStatus").WithPluralName("PurchaseOrderStatuses").Build();
            var PayrollPreference = new ClassBuilder(domain, new Guid("92f48c0c-31d9-4ed5-8f92-753de6af471a")).WithSingularName("PayrollPreference").WithPluralName("PayrollPreferences").Build();
            var CustomerShipment = new ClassBuilder(domain, new Guid("9301efcb-2f08-4825-aa60-752c031e4697")).WithSingularName("CustomerShipment").WithPluralName("CustomerShipments").Build();
            var InternalOrganisationRevenue = new ClassBuilder(domain, new Guid("930565df-e12c-43c3-9679-a2b42d5a8782")).WithSingularName("InternalOrganisationRevenue").WithPluralName("InternalOrganisationRevenues").Build();
            var Package = new ClassBuilder(domain, new Guid("9371d5fc-748a-4ce4-95eb-6b21aa0ca841")).WithSingularName("Package").WithPluralName("Packages").Build();
            var GeoLocatable = new InterfaceBuilder(domain, new Guid("93960be2-f676-4e7f-9efb-f99c92303059")).WithSingularName("GeoLocatable").WithPluralName("GeoLocatables").Build();
            var HazardousMaterialsDocument = new ClassBuilder(domain, new Guid("93e3b3df-b227-479a-9b05-ec10190e7d51")).WithSingularName("HazardousMaterialsDocument").WithPluralName("HazardousMaterialsDocuments").Build();
            var EmailCommunication = new ClassBuilder(domain, new Guid("9426c214-c85d-491b-a5a6-9f573c3341a0")).WithSingularName("EmailCommunication").WithPluralName("EmailCommunications").Build();
            var CreditCard = new ClassBuilder(domain, new Guid("9492bd39-0f07-4978-a987-0393ca34b504")).WithSingularName("CreditCard").WithPluralName("CreditCards").Build();
            var OrganisationContactRelationship = new ClassBuilder(domain, new Guid("956ecb86-097d-43d4-83b5-a7f45ea75448")).WithSingularName("OrganisationContactRelationship").WithPluralName("OrganisationContactRelationships").Build();
            var OrganisationContactKind = new ClassBuilder(domain, new Guid("9570d60a-8baa-439c-99f4-472d10952165")).WithSingularName("OrganisationContactKind").WithPluralName("OrganisationContactKinds").Build();
            var CustomerReturnStatus = new ClassBuilder(domain, new Guid("959dd6ba-dfd5-4c7f-84f0-819fbef5c76a")).WithSingularName("CustomerReturnStatus").WithPluralName("CustomerReturnStatuses").Build();
            var PerformanceReviewItem = new ClassBuilder(domain, new Guid("962e5149-546b-4b18-ab09-e4de59b709ff")).WithSingularName("PerformanceReviewItem").WithPluralName("PerformanceReviewItems").Build();
            var UtilizationCharge = new ClassBuilder(domain, new Guid("96a64894-e444-4df4-9289-1b121842ac73")).WithSingularName("UtilizationCharge").WithPluralName("UtilizationCharges").Build();
            var PartyPackageRevenue = new ClassBuilder(domain, new Guid("96fe3000-606e-4f88-ba04-87544ef176ca")).WithSingularName("PartyPackageRevenue").WithPluralName("PartyPackageRevenues").Build();
            var PartyRelationshipStatus = new ClassBuilder(domain, new Guid("97e31ffb-b478-4599-a145-54880d4ffbe1")).WithSingularName("PartyRelationshipStatus").WithPluralName("PartyRelationshipStatuses").Build();
            var ServiceTerritory = new ClassBuilder(domain, new Guid("987f8328-2bfa-47cd-9521-8b7bda78f90a")).WithSingularName("ServiceTerritory").WithPluralName("ServiceTerritories").Build();
            var DeliverableBasedService = new ClassBuilder(domain, new Guid("98fc5441-2037-4134-b143-a9797af9d7f1")).WithSingularName("DeliverableBasedService").WithPluralName("DeliverableBasedServices").Build();
            var ProductModel = new ClassBuilder(domain, new Guid("99ea8125-7d86-4cb6-b453-27752c434fc7")).WithSingularName("ProductModel").WithPluralName("ProductModels").Build();
            var Shelf = new ClassBuilder(domain, new Guid("9a1d67c5-159c-41e0-9b5c-5ffdfe257b8d")).WithSingularName("Shelf").WithPluralName("Shelfs").Build();
            var RawMaterial = new ClassBuilder(domain, new Guid("9a484067-2003-42f1-b4c4-877e519bb8be")).WithSingularName("RawMaterial").WithPluralName("RawMaterials").Build();
            var EstimatedOtherCost = new ClassBuilder(domain, new Guid("9b637b39-f61a-4985-bb1b-876ed769f448")).WithSingularName("EstimatedOtherCost").WithPluralName("EstimatedOtherCosts").Build();
            var BudgetRevision = new ClassBuilder(domain, new Guid("9b6bf786-1c6c-4c4e-b940-7314d9c4ba71")).WithSingularName("BudgetRevision").WithPluralName("BudgetRevisions").Build();
            var WorkEffortFixedAssetStandard = new ClassBuilder(domain, new Guid("9b9f2a59-ae10-49df-b0b5-98b48ec99157")).WithSingularName("WorkEffortFixedAssetStandard").WithPluralName("WorkEffortFixedAssetStandards").Build();
            var Shipment = new InterfaceBuilder(domain, new Guid("9c6f4ad8-5a4e-4b6e-96b7-876f7aabcffb")).WithSingularName("Shipment").WithPluralName("Shipments").Build();
            var PostalCode = new ClassBuilder(domain, new Guid("9d0065b8-2760-4ec5-928a-9ebd128bbfdd")).WithSingularName("PostalCode").WithPluralName("PostalCodes").Build();
            var NonSerializedInventoryItemObjectState = new ClassBuilder(domain, new Guid("9dd17a3f-0e3c-4d87-b840-2f23a96dd165")).WithSingularName("NonSerializedInventoryItemObjectState").WithPluralName("NonSerializedInventoryItemObjectStates").Build();
            var ProfessionalAssignment = new ClassBuilder(domain, new Guid("9e679821-8eeb-4dce-b090-d8ade95cb47f")).WithSingularName("ProfessionalAssignment").WithPluralName("ProfessionalAssignments").Build();
            var Container = new InterfaceBuilder(domain, new Guid("9ec6dae1-439e-4b19-b4dc-885e1ed943d7")).WithSingularName("Container").WithPluralName("Containers").Build();
            var Payment = new InterfaceBuilder(domain, new Guid("9f20a35c-d814-4690-a96f-2bcd25f6c6a2")).WithSingularName("Payment").WithPluralName("Payments").Build();
            var TransferObjectState = new ClassBuilder(domain, new Guid("9f3d9ae6-cbbf-4cfb-900d-bc66edccbf95")).WithSingularName("TransferObjectState").WithPluralName("TransferObjectStates").Build();
            var PackageRevenueHistory = new ClassBuilder(domain, new Guid("9f995d6f-972a-46e4-bbe4-d1e9bedf09ef")).WithSingularName("PackageRevenueHistory").WithPluralName("PackageRevenueHistories").Build();
            var JournalEntryDetail = new ClassBuilder(domain, new Guid("9ffd634a-27b9-46a5-bf77-4ae25a9b9ebf")).WithSingularName("JournalEntryDetail").WithPluralName("JournalEntryDetails").Build();
            var TestingRequirement = new ClassBuilder(domain, new Guid("a06befc5-c347-4ffb-9391-2a099fca5145")).WithSingularName("TestingRequirement").WithPluralName("TestingRequirements").Build();
            var Case = new ClassBuilder(domain, new Guid("a0705b81-2eef-4c51-9454-a31bcedc20a3")).WithSingularName("Case").WithPluralName("Cases").Build();
            var Capitalization = new ClassBuilder(domain, new Guid("a0a753be-15ca-49e2-8f5f-f956fa132f49")).WithSingularName("Capitalization").WithPluralName("Capitalizations").Build();
            var PurchaseReturn = new ClassBuilder(domain, new Guid("a0cf565a-2dcf-4513-9110-8c34468d993f")).WithSingularName("PurchaseReturn").WithPluralName("PurchaseReturns").Build();
            var WorkEffortPartStandard = new ClassBuilder(domain, new Guid("a12e5d28-e431-48d3-bbb1-8a2f5e3c4991")).WithSingularName("WorkEffortPartStandard").WithPluralName("WorkEffortPartStandards").Build();
            var SurchargeComponent = new ClassBuilder(domain, new Guid("a18de27f-54fe-4160-b149-475bebeaf716")).WithSingularName("SurchargeComponent").WithPluralName("SurchargeComponents").Build();
            var Bank = new ClassBuilder(domain, new Guid("a24a8e12-7067-4bfb-8fc0-225a824d1a05")).WithSingularName("Bank").WithPluralName("Banks").Build();
            var ProductRevenue = new ClassBuilder(domain, new Guid("a34ca9ef-63e5-48c0-8a62-c8f43ad2d9d9")).WithSingularName("ProductRevenue").WithPluralName("ProductRevenues").Build();
            var DisbursementAccountingTransaction = new ClassBuilder(domain, new Guid("a3a5aeea-3c8b-43ab-94f1-49a1bd2d7254")).WithSingularName("DisbursementAccountingTransaction").WithPluralName("DisbursementAccountingTransactions").Build();
            var OrderValue = new ClassBuilder(domain, new Guid("a3ca36e6-960d-4e3a-96d0-6ca1d71d05d7")).WithSingularName("OrderValue").WithPluralName("OrderValues").Build();
            var VatTariff = new ClassBuilder(domain, new Guid("a3f63642-b397-4281-ba7e-8c77e9f30658")).WithSingularName("VatTariff").WithPluralName("VatTariffs").Build();
            var Obligation = new ClassBuilder(domain, new Guid("a3fe34f9-7dfb-46fe-98ec-ed9a7d14ac19")).WithSingularName("Obligation").WithPluralName("Obligations").Build();
            var SalesInvoiceObjectState = new ClassBuilder(domain, new Guid("a4092f59-2baf-4041-83e6-5d50c8338a5c")).WithSingularName("SalesInvoiceObjectState").WithPluralName("SalesInvoiceObjectStates").Build();
            var VatRate = new ClassBuilder(domain, new Guid("a5e29ca1-80de-4de4-9085-b69f21550b5a")).WithSingularName("VatRate").WithPluralName("VatRates").Build();
            var Invoice = new InterfaceBuilder(domain, new Guid("a6f4eedb-b0b5-491d-bcc0-09d2bc109e86")).WithSingularName("Invoice").WithPluralName("Invoices").Build();
            var ProfessionalServicesRelationship = new ClassBuilder(domain, new Guid("a6f772e6-8f2c-4180-bbf9-2e5ab0f0efc8")).WithSingularName("ProfessionalServicesRelationship").WithPluralName("ProfessionalServicesRelationships").Build();
            var RecurringCharge = new ClassBuilder(domain, new Guid("a71e670c-f089-4ec1-8295-dda8e7b62a19")).WithSingularName("RecurringCharge").WithPluralName("RecurringCharges").Build();
            var FinancialTerm = new ClassBuilder(domain, new Guid("a73aa458-2293-4578-be67-ad32e36a4991")).WithSingularName("FinancialTerm").WithPluralName("FinancialTerms").Build();
            var RequirementStatus = new ClassBuilder(domain, new Guid("a79b3e89-e878-45a5-9c9f-7911d259fc33")).WithSingularName("RequirementStatus").WithPluralName("RequirementStatuses").Build();
            var PurchaseInvoiceItemObjectState = new ClassBuilder(domain, new Guid("a7d98869-b51e-45b4-9403-06094bb61fcf")).WithSingularName("PurchaseInvoiceItemObjectState").WithPluralName("PurchaseInvoiceItemObjectStates").Build();
            var InvoiceTerm = new ClassBuilder(domain, new Guid("a917f763-e54a-4693-bf7b-d8e7aead8fe6")).WithSingularName("InvoiceTerm").WithPluralName("InvoiceTerms").Build();
            var DropShipment = new ClassBuilder(domain, new Guid("a981c832-dd3a-4b97-9bc9-d2dd83872bf2")).WithSingularName("DropShipment").WithPluralName("DropShipments").Build();
            var SalesInvoiceItem = new ClassBuilder(domain, new Guid("a98f8aca-d711-47e8-ac9c-25b607cbaef1")).WithSingularName("SalesInvoiceItem").WithPluralName("SalesInvoiceItems").Build();
            var EngagementItem = new InterfaceBuilder(domain, new Guid("aa3bf631-5aa5-48ab-a249-ef61f640fb72")).WithSingularName("EngagementItem").WithPluralName("EngagementItems").Build();
            var OrderQuantityBreak = new ClassBuilder(domain, new Guid("aa5898e6-71d0-4dcb-9bbd-35ae5cb0e0ef")).WithSingularName("OrderQuantityBreak").WithPluralName("OrderQuantityBreaks").Build();
            var Event = new ClassBuilder(domain, new Guid("aad26d12-9e80-410c-ab99-57064bd3dd2e")).WithSingularName("Event").WithPluralName("Events").Build();
            var ClientRelationship = new ClassBuilder(domain, new Guid("aadaf02e-0bb8-4862-a354-488f39aa8f4e")).WithSingularName("ClientRelationship").WithPluralName("ClientRelationships").Build();
            var PurchaseOrderItem = new ClassBuilder(domain, new Guid("ab648bd0-6e31-4ab0-a9ee-cf4a6f02033d")).WithSingularName("PurchaseOrderItem").WithPluralName("PurchaseOrderItems").Build();
            var WorkEffortAssignmentRate = new ClassBuilder(domain, new Guid("ac18c87b-683c-4529-9171-d23e73c583d4")).WithSingularName("WorkEffortAssignmentRate").WithPluralName("WorkEffortAssignmentRates").Build();
            var EuSalesListType = new ClassBuilder(domain, new Guid("acbe7b46-bcfe-4e8b-b8a7-7b9eeac4d6e2")).WithSingularName("EuSalesListType").WithPluralName("EuSalesListTypes").Build();
            var PurchaseOrderItemObjectState = new ClassBuilder(domain, new Guid("ad76acee-eccc-42ce-9897-8c3f0252caf4")).WithSingularName("PurchaseOrderItemObjectState").WithPluralName("PurchaseOrderItemObjectStates").Build();
            var Province = new ClassBuilder(domain, new Guid("ada24931-020a-48e8-8f8d-18ddb8f46cf7")).WithSingularName("Province").WithPluralName("Provinces").Build();
            var InventoryItemVariance = new ClassBuilder(domain, new Guid("b00e2650-283f-4326-bdd3-46a2890e2037")).WithSingularName("InventoryItemVariance").WithPluralName("InventoryItemVariances").Build();
            var ContactMechanism = new InterfaceBuilder(domain, new Guid("b033f9c9-c799-485c-a199-914a9e9119d9")).WithSingularName("ContactMechanism").WithPluralName("ContactMechanisms").Build();
            var CommunicationEvent = new InterfaceBuilder(domain, new Guid("b05371ff-0c9e-4ee3-b31d-e2edeed8649e")).WithSingularName("CommunicationEvent").WithPluralName("CommunicationEvents").Build();
            var PositionResponsibility = new ClassBuilder(domain, new Guid("b0a42c94-3d4e-47f1-86a2-cf45eeba5f0d")).WithSingularName("PositionResponsibility").WithPluralName("PositionResponsibilities").Build();
            var DeliverableType = new ClassBuilder(domain, new Guid("b1208ddd-9c28-46c3-8d05-2ea1ee29945d")).WithSingularName("DeliverableType").WithPluralName("DeliverableTypes").Build();
            var SubAssembly = new ClassBuilder(domain, new Guid("b1a10fe4-2d84-452b-b0cb-e96e55014856")).WithSingularName("SubAssembly").WithPluralName("SubAssemblies").Build();
            var RequirementObjectState = new ClassBuilder(domain, new Guid("b1ee7191-544e-4cee-bbb1-d64364eb7137")).WithSingularName("RequirementObjectState").WithPluralName("RequirementObjectStates").Build();
            var WorkFlow = new ClassBuilder(domain, new Guid("b2a169ce-4e2a-48fc-aa39-dfc783ecb401")).WithSingularName("WorkFlow").WithPluralName("WorkFlows").Build();
            var Task = new ClassBuilder(domain, new Guid("b2cf9a3d-f156-4da7-87bf-ecdeaa13e326")).WithSingularName("Task").WithPluralName("Tasks").Build();
            var ResourceRequirement = new ClassBuilder(domain, new Guid("b3753d18-0b7e-4177-92c3-81ae8ce35a8f")).WithSingularName("ResourceRequirement").WithPluralName("ResourceRequirements").Build();
            var BudgetItem = new ClassBuilder(domain, new Guid("b397c075-215a-4d5b-b962-ea48540a64fa")).WithSingularName("BudgetItem").WithPluralName("BudgetItems").Build();
            var InternalRequirement = new ClassBuilder(domain, new Guid("b46c9149-21ef-45a6-aef6-c6aa30389d7f")).WithSingularName("InternalRequirement").WithPluralName("InternalRequirements").Build();
            var PositionReportingStructure = new ClassBuilder(domain, new Guid("b50d0780-bcbf-4041-8576-164577d40c55")).WithSingularName("PositionReportingStructure").WithPluralName("PositionReportingStructures").Build();
            var Partnership = new ClassBuilder(domain, new Guid("b55d1ad5-0ef0-40f0-b8d4-b39c370d7dcf")).WithSingularName("Partnership").WithPluralName("Partnerships").Build();
            var OperatingBudget = new ClassBuilder(domain, new Guid("b5d151c7-0b18-4280-80d1-77b46162dba8")).WithSingularName("OperatingBudget").WithPluralName("OperatingBudgets").Build();
            var Bin = new ClassBuilder(domain, new Guid("b5d29ed3-b850-4607-9f49-9a920a2bffa1")).WithSingularName("Bin").WithPluralName("Bins").Build();
            var ManufacturingConfiguration = new ClassBuilder(domain, new Guid("b6c168d6-3d5c-4f5f-b6c6-d348600f1483")).WithSingularName("ManufacturingConfiguration").WithPluralName("ManufacturingConfigurations").Build();
            var IUnitOfMeasure = new InterfaceBuilder(domain, new Guid("b7215af5-97d6-42b0-9f6f-c1fccb2bc695")).WithSingularName("IUnitOfMeasure").WithPluralName("IUnitsOfMeasure").Build();
            var ProfessionalPlacement = new ClassBuilder(domain, new Guid("b83205c5-261f-4d9d-9789-55966ae8d61b")).WithSingularName("ProfessionalPlacement").WithPluralName("ProfessionalPlacements").Build();
            var SalesRepCommission = new ClassBuilder(domain, new Guid("bb5e8196-f821-4fb8-98cb-f19416d1427c")).WithSingularName("SalesRepCommission").WithPluralName("SalesRepCommissions").Build();
            var CityBound = new InterfaceBuilder(domain, new Guid("bfdd33dc-5701-41ec-a768-f745155663d3")).WithSingularName("CityBound").WithPluralName("CityBounds").Build();
            var Deduction = new ClassBuilder(domain, new Guid("c04ccfcf-ae3f-4e7f-9e19-503ba547b678")).WithSingularName("Deduction").WithPluralName("Deductions").Build();
            var CaseStatus = new ClassBuilder(domain, new Guid("c0b015e0-57a4-4fe3-984b-12e8bda25db7")).WithSingularName("CaseStatus").WithPluralName("CaseStatuses").Build();
            var DiscountComponent = new ClassBuilder(domain, new Guid("c0b927c4-7197-4295-8edf-057b6b4b3a6a")).WithSingularName("DiscountComponent").WithPluralName("DiscountComponents").Build();
            var OrganisationUnit = new ClassBuilder(domain, new Guid("c0e14757-9825-4a86-95d9-b87c68efcb9c")).WithSingularName("OrganisationUnit").WithPluralName("OrganisationUnits").Build();
            var PartSubstitute = new ClassBuilder(domain, new Guid("c0ea51d6-e9f1-4cb3-80ea-36d8ac4f8a15")).WithSingularName("PartSubstitute").WithPluralName("PartSubstitutes").Build();
            var GoodOrderItem = new ClassBuilder(domain, new Guid("c1b6fac9-8e69-4c07-8cec-e9b52c690e72")).WithSingularName("GoodOrderItem").WithPluralName("GoodOrderItems").Build();
            var VolumeUsage = new ClassBuilder(domain, new Guid("c219edcd-71dc-4f0b-afee-4b06f3d785be")).WithSingularName("VolumeUsage").WithPluralName("VolumeUsages").Build();
            var ProductQuote = new ClassBuilder(domain, new Guid("c2214ff4-d592-4f0d-9215-e431b23dc9c2")).WithSingularName("ProductQuote").WithPluralName("ProductQuotes").Build();
            var TransferStatus = new ClassBuilder(domain, new Guid("c2b88d46-c321-48c4-9493-22a886d91bf9")).WithSingularName("TransferStatus").WithPluralName("TransferStatuses").Build();
            var State = new ClassBuilder(domain, new Guid("c37f7876-51af-4748-b083-4a6e42e99597")).WithSingularName("State").WithPluralName("States").Build();
            var JournalEntryNumber = new ClassBuilder(domain, new Guid("c47bf25f-7d16-4dcd-af3b-5e893a1cdd92")).WithSingularName("JournalEntryNumber").WithPluralName("JournalEntryNumbers").Build();
            var Tolerance = new ClassBuilder(domain, new Guid("c4b51143-7e9c-4f1d-a34f-cc99f29a12e9")).WithSingularName("Tolerance").WithPluralName("Tolerances").Build();
            var OrderAdjustment = new InterfaceBuilder(domain, new Guid("c5578565-c07a-4dc1-8381-41955db364e2")).WithSingularName("OrderAdjustment").WithPluralName("OrderAdjustments").Build();
            var EngineeringChange = new ClassBuilder(domain, new Guid("c6c4537a-21f8-4d62-b584-3c609fb2210f")).WithSingularName("EngineeringChange").WithPluralName("EngineeringChanges").Build();
            var VatRatePurchaseKind = new ClassBuilder(domain, new Guid("c758f77e-e3b3-4517-831a-af1bf0e1dceb")).WithSingularName("VatRatePurchaseKind").WithPluralName("VatRatePurchaseKinds").Build();
            var EmailTemplate = new ClassBuilder(domain, new Guid("c78a49b1-9918-4f15-95f3-c537c82f59fd")).WithSingularName("EmailTemplate").WithPluralName("EmailTemplates").Build();
            var Threshold = new ClassBuilder(domain, new Guid("c7b56330-1fb6-46c7-a042-04a4cf671ec1")).WithSingularName("Threshold").WithPluralName("Thresholds").Build();
            var EmploymentApplicationStatus = new ClassBuilder(domain, new Guid("c7c24ce4-3455-4cec-a733-64a436434b3e")).WithSingularName("EmploymentApplicationStatus").WithPluralName("EmploymentApplicationStatuses").Build();
            var Qualification = new ClassBuilder(domain, new Guid("c8077ff8-f443-44b5-93f5-15ad7f4a258d")).WithSingularName("Qualification").WithPluralName("Qualifications").Build();
            var InternalOrganisation = new ClassBuilder(domain, new Guid("c81441c8-9ac9-440e-a926-c96230b2701f")).WithSingularName("InternalOrganisation").WithPluralName("InternalOrganisations").Build();
            var EstimatedProductCost = new InterfaceBuilder(domain, new Guid("c8df7ac5-4e6f-4add-981f-f0d9a8c14e24")).WithSingularName("EstimatedProductCost").WithPluralName("EstimatedProductCosts").Build();
            var OwnBankAccount = new ClassBuilder(domain, new Guid("ca008b8d-584e-4aa5-a759-895b634defc5")).WithSingularName("OwnBankAccount").WithPluralName("OwnBankAccounts").Build();
            var DeploymentUsage = new InterfaceBuilder(domain, new Guid("ca0f0654-3974-4e5e-a57e-593216c05e16")).WithSingularName("DeploymentUsage").WithPluralName("DeploymentUsages").Build();
            var PartyContactMechanism = new ClassBuilder(domain, new Guid("ca633037-ba1e-4304-9f2c-3353c287474b")).WithSingularName("PartyContactMechanism").WithPluralName("PartyContactMechanisms").Build();
            var PartyRelationshipPriority = new ClassBuilder(domain, new Guid("caa4814f-85a2-46a8-97a7-82220f8270cb")).WithSingularName("PartyRelationshipPriority").WithPluralName("PartyRelationshipPriorities").Build();
            var CostCenterSplitMethod = new ClassBuilder(domain, new Guid("cabc3b20-0456-47d9-a030-6df1d1f8ea9e")).WithSingularName("CostCenterSplitMethod").WithPluralName("CostCenterSplitMethods").Build();
            var EstimatedMaterialCost = new ClassBuilder(domain, new Guid("cb6a8e8a-04a6-437b-b952-f502cca2a2db")).WithSingularName("EstimatedMaterialCost").WithPluralName("EstimatedMaterialCosts").Build();
            var QuoteTerm = new ClassBuilder(domain, new Guid("cd60cf6d-65ba-4e31-b85d-16c19fc0978b")).WithSingularName("QuoteTerm").WithPluralName("QuoteTerms").Build();
            var Transfer = new ClassBuilder(domain, new Guid("cd66a79f-c4b8-4c33-b6ec-1928809b6b88")).WithSingularName("Transfer").WithPluralName("Transfers").Build();
            var Facility = new InterfaceBuilder(domain, new Guid("cdd79e23-a132-48b0-b88f-a03bd029f49d")).WithSingularName("Facility").WithPluralName("Facilities").Build();
            var RevenueQuantityBreak = new ClassBuilder(domain, new Guid("ce394ad6-1229-4621-8506-5f0347cd8c92")).WithSingularName("RevenueQuantityBreak").WithPluralName("RevenueQuantityBreaks").Build();
            var GeneralLedgerAccountType = new ClassBuilder(domain, new Guid("ce5c78ee-f892-4ced-9b21-51d84c77127f")).WithSingularName("GeneralLedgerAccountType").WithPluralName("GeneralLedgerAccountTypes").Build();
            var SerializedInventoryItemObjectState = new ClassBuilder(domain, new Guid("d042eeae-5c17-4936-861b-aaa9dfaed254")).WithSingularName("SerializedInventoryItemObjectState").WithPluralName("SerializedInventoryItemObjectStates").Build();
            var FaceToFaceCommunication = new ClassBuilder(domain, new Guid("d0f9fc0d-a3c5-46cc-ab00-4c724995fc14")).WithSingularName("FaceToFaceCommunication").WithPluralName("FaceToFaceCommunications").Build();
            var BudgetReview = new ClassBuilder(domain, new Guid("d12719f0-2c0e-4a9d-869b-4a209fc35a56")).WithSingularName("BudgetReview").WithPluralName("BudgetReviews").Build();
            var EngineeringChangeStatus = new ClassBuilder(domain, new Guid("d149dd80-1cdc-4a29-bb0b-b88823d718bc")).WithSingularName("EngineeringChangeStatus").WithPluralName("EngineeringChangeStatuses").Build();
            var ProductQuality = new ClassBuilder(domain, new Guid("d14fa0d2-8743-4d3c-8109-2ab9161cb310")).WithSingularName("ProductQuality").WithPluralName("ProductQualities").Build();
            var Disbursement = new ClassBuilder(domain, new Guid("d152e0a4-c76f-4945-8c0f-ad1e5f70ad07")).WithSingularName("Disbursement").WithPluralName("Disbursements").Build();
            var Research = new ClassBuilder(domain, new Guid("d1d8f99e-430d-4104-a2db-777a0f6292e3")).WithSingularName("Research").WithPluralName("Researches").Build();
            var PartBillOfMaterial = new InterfaceBuilder(domain, new Guid("d204e616-039c-40c8-81cc-18f3a7345d99")).WithSingularName("PartBillOfMaterial").WithPluralName("PartBillOfMaterials").Build();
            var Journal = new ClassBuilder(domain, new Guid("d3446420-6d2a-4d18-a6eb-0405da9f7cc5")).WithSingularName("Journal").WithPluralName("Journals").Build();
            var ShipmentItem = new ClassBuilder(domain, new Guid("d35c33c3-ca15-4b70-b20d-c51ed068626a")).WithSingularName("ShipmentItem").WithPluralName("ShipmentItems").Build();
            var ProductFeature = new InterfaceBuilder(domain, new Guid("d3c5a482-e17a-4e37-84eb-55a035e80f2f")).WithSingularName("ProductFeature").WithPluralName("ProductFeatures").Build();
            var Requirement = new InterfaceBuilder(domain, new Guid("d3f90525-b7fe-4f81-bccd-adf4f57260bc")).WithSingularName("Requirement").WithPluralName("Requirements").Build();
            var EmploymentAgreement = new ClassBuilder(domain, new Guid("d402d086-0d7a-4e98-bcb1-8f8e1cfabb99")).WithSingularName("EmploymentAgreement").WithPluralName("EmploymentAgreements").Build();
            var ManufacturerSuggestedRetailPrice = new ClassBuilder(domain, new Guid("d4cfdb68-9128-4afc-8670-192e55115499")).WithSingularName("ManufacturerSuggestedRetailPrice").WithPluralName("ManufacturerSuggestedRetailPrices").Build();
            var NewsItem = new ClassBuilder(domain, new Guid("d50ffc20-9e2d-4362-8e3f-b54d7368d487")).WithSingularName("NewsItem").WithPluralName("NewsItems").Build();
            var PartyBenefit = new ClassBuilder(domain, new Guid("d520cf1a-8d3a-4380-8b88-85cd63a5ad05")).WithSingularName("PartyBenefit").WithPluralName("PartyBenefits").Build();
            var PostalAddress = new ClassBuilder(domain, new Guid("d54b4bba-a84c-4826-85ba-7340714035c7")).WithSingularName("PostalAddress").WithPluralName("PostalAddresses").Build();
            var PackageQuantityBreak = new ClassBuilder(domain, new Guid("d551887b-8520-478d-bf2c-b0f26e3bc356")).WithSingularName("PackageQuantityBreak").WithPluralName("PackageQuantityBreaks").Build();
            var SubContractorRelationship = new ClassBuilder(domain, new Guid("d60cc44a-6491-4982-9b2d-99891e382a21")).WithSingularName("SubContractorRelationship").WithPluralName("SubContractorRelationships").Build();
            var ClientAgreement = new ClassBuilder(domain, new Guid("d726e301-4e4a-4ccb-9a6e-bc6fc4a327ab")).WithSingularName("ClientAgreement").WithPluralName("ClientAgreements").Build();
            var InvoiceItem = new InterfaceBuilder(domain, new Guid("d79f734d-4434-4710-a7ea-7d6306f3064f")).WithSingularName("InvoiceItem").WithPluralName("InvoiceItems").Build();
            var Store = new ClassBuilder(domain, new Guid("d8611e48-b0ba-4037-a992-09e3e26c6d5d")).WithSingularName("Store").WithPluralName("Stores").Build();
            var Lot = new ClassBuilder(domain, new Guid("d900e278-7add-4e90-8bea-0a65d03f4fa7")).WithSingularName("Lot").WithPluralName("Lots").Build();
            var WorkEffortSkillStandard = new ClassBuilder(domain, new Guid("da16f5ee-0e04-41a7-afd7-b12e20414135")).WithSingularName("WorkEffortSkillStandard").WithPluralName("WorkEffortSkillStandards").Build();
            var TimeAndMaterialsService = new ClassBuilder(domain, new Guid("da504b46-2fd0-4500-ae23-61fa73151077")).WithSingularName("TimeAndMaterialsService").WithPluralName("TimeAndMaterialsServices").Build();
            var Equipment = new ClassBuilder(domain, new Guid("da852ff9-0c87-4fa6-a93a-90d97d28029c")).WithSingularName("Equipment").WithPluralName("Equipments").Build();
            var RequestItem = new ClassBuilder(domain, new Guid("daf83fcc-832e-4d5e-ba71-5a08f42355db")).WithSingularName("RequestItem").WithPluralName("RequestItems").Build();
            var SalesChannel = new ClassBuilder(domain, new Guid("db1678af-6541-4a35-a3b9-cffd0f821bd2")).WithSingularName("SalesChannel").WithPluralName("SalesChannels").Build();
            var CustomerRequirement = new ClassBuilder(domain, new Guid("db24e487-daf7-4625-9073-8fd083f653dc")).WithSingularName("CustomerRequirement").WithPluralName("CustomerRequirements").Build();
            var Property = new ClassBuilder(domain, new Guid("dc54aafb-f0f2-4f72-8a81-d5b2fc792b86")).WithSingularName("Property").WithPluralName("Properties").Build();
            var ConstraintSpecification = new ClassBuilder(domain, new Guid("dc8ce136-7088-4128-8f69-4d5cb2ca2648")).WithSingularName("ConstraintSpecification").WithPluralName("ConstraintSpecifications").Build();
            var DesiredProductFeature = new ClassBuilder(domain, new Guid("dda88fe9-14b3-463b-ae66-25dd1b136e16")).WithSingularName("DesiredProductFeature").WithPluralName("DesiredProductFeatures").Build();
            var SalesOrderItemStatus = new ClassBuilder(domain, new Guid("de70746f-2c82-4f01-8de9-b4f78105426a")).WithSingularName("SalesOrderItemStatus").WithPluralName("SalesOrderItemStatuses").Build();
            var ActivityUsage = new ClassBuilder(domain, new Guid("ded168ad-b674-47ab-855c-46b3e1939e32")).WithSingularName("ActivityUsage").WithPluralName("ActivityUsages").Build();
            var Program = new ClassBuilder(domain, new Guid("dfe47c36-58b5-4438-b674-cc2e861922d6")).WithSingularName("Program").WithPluralName("Programs").Build();
            var CommunicationEventStatus = new ClassBuilder(domain, new Guid("e2c3f3fa-7b94-4315-b8dd-2f538d8e2132")).WithSingularName("CommunicationEventStatus").WithPluralName("CommunicationEventStatuses").Build();
            var AgreementSection = new ClassBuilder(domain, new Guid("e31d6dd2-b5b2-4fd8-949f-0df688ed2e9b")).WithSingularName("AgreementSection").WithPluralName("AgreementSections").Build();
            var Good = new ClassBuilder(domain, new Guid("e3e87d40-b4f0-4953-9716-db13b35d716b")).WithSingularName("Good").WithPluralName("Goods").Build();
            var EngineeringChangeObjectState = new ClassBuilder(domain, new Guid("e3f78cf6-6367-4b0f-9ac0-b887e7187c5e")).WithSingularName("EngineeringChangeObjectState").WithPluralName("EngineeringChangeObjectStates").Build();
            var AccountingTransactionDetail = new ClassBuilder(domain, new Guid("e41be1b2-715b-4bc0-b095-ac23d9950ee4")).WithSingularName("AccountingTransactionDetail").WithPluralName("AccountingTransactionDetails").Build();
            var County = new ClassBuilder(domain, new Guid("e6f97f86-6aec-4dde-b828-4de04d42c248")).WithSingularName("County").WithPluralName("Counties").Build();
            var ShippingAndHandlingCharge = new ClassBuilder(domain, new Guid("e7625d17-2485-4894-ba1a-c565b8c6052c")).WithSingularName("ShippingAndHandlingCharge").WithPluralName("ShippingAndHandlingCharges").Build();
            var PerformanceReviewItemType = new ClassBuilder(domain, new Guid("e80a9fe3-027b-4abd-acfb-99e3db9da70c")).WithSingularName("PerformanceReviewItemType").WithPluralName("PerformanceReviewItemTypes").Build();
            var PostalBoundary = new ClassBuilder(domain, new Guid("e94bf9e1-373d-49e3-a0fe-f21a8b1525d4")).WithSingularName("PostalBoundary").WithPluralName("PostalBoundaries").Build();
            var ProductCategory = new ClassBuilder(domain, new Guid("ea83087e-05cc-458c-a6ba-3ce947644a0f")).WithSingularName("ProductCategory").WithPluralName("ProductCategories").Build();
            var RequestForInformation = new ClassBuilder(domain, new Guid("eab85f26-c3f4-4f47-97dc-8f9429856c00")).WithSingularName("RequestForInformation").WithPluralName("RequestsForInformation").Build();
            var CountryBound = new InterfaceBuilder(domain, new Guid("eaebcfe7-0d65-43ab-857c-b171086a1982")).WithSingularName("CountryBound").WithPluralName("CountryBounds").Build();
            var VatForm = new ClassBuilder(domain, new Guid("eba70b57-05e3-487f-8cf1-45f14fcdc3b9")).WithSingularName("VatForm").WithPluralName("VatForms").Build();
            var BudgetRevisionImpact = new ClassBuilder(domain, new Guid("ebae3ca2-5dca-486d-bbc0-30550313f153")).WithSingularName("BudgetRevisionImpact").WithPluralName("BudgetRevisionImpacts").Build();
            var Budget = new InterfaceBuilder(domain, new Guid("ebd4da8c-b86a-4317-86b9-e90a02994dcc")).WithSingularName("Budget").WithPluralName("Budgets").Build();
            var TemplatePurpose = new ClassBuilder(domain, new Guid("ebf12996-6fc7-4f22-b9be-cedaaba7bfb2")).WithSingularName("TemplatePurpose").WithPluralName("TemplatePurposes").Build();
            var WebSiteCommunication = new ClassBuilder(domain, new Guid("ecf2996a-7f8b-45d5-afac-56c88c62136a")).WithSingularName("WebSiteCommunication").WithPluralName("WebSiteCommunications").Build();
            var Withdrawal = new ClassBuilder(domain, new Guid("edf1788a-0c75-4635-904d-db9f9a6a7399")).WithSingularName("Withdrawal").WithPluralName("Withdrawals").Build();
            var Deployment = new ClassBuilder(domain, new Guid("ee23df25-f7d7-4974-b62e-ee3cba56b709")).WithSingularName("Deployment").WithPluralName("Deployments").Build();
            var PayCheck = new ClassBuilder(domain, new Guid("ef5fb351-2f0f-454a-b7b2-104af42b2c72")).WithSingularName("PayCheck").WithPluralName("PayChecks").Build();
            var MaritalStatus = new ClassBuilder(domain, new Guid("ef6ce14d-87e9-4704-be8b-595329a6bf20")).WithSingularName("MaritalStatus").WithPluralName("MaritalStatuses").Build();
            var Manifest = new ClassBuilder(domain, new Guid("efb6f7a2-edec-40dd-a03a-d4e15abc438d")).WithSingularName("Manifest").WithPluralName("Manifests").Build();
            var ExportDocument = new ClassBuilder(domain, new Guid("efe15d5d-f07c-497e-98c2-dd64f624840f")).WithSingularName("ExportDocument").WithPluralName("ExportDocuments").Build();
            var InventoryItemConfiguration = new InterfaceBuilder(domain, new Guid("f135770b-7228-4e4b-b7ea-9307b6317fd2")).WithSingularName("InventoryItemConfiguration").WithPluralName("InventoryItemConfigurations").Build();
            var CustomerShipmentStatus = new ClassBuilder(domain, new Guid("f13976d4-b1f4-4b78-a720-beab1e0a7e4c")).WithSingularName("CustomerShipmentStatus").WithPluralName("CustomerShipmentStatuses").Build();
            var ExpenseEntry = new ClassBuilder(domain, new Guid("f15e6b0e-0222-4f9b-8ae2-20c20f3b3673")).WithSingularName("ExpenseEntry").WithPluralName("ExpenseEntries").Build();
            var ProductAssociation = new InterfaceBuilder(domain, new Guid("f194d2e1-d246-40eb-9eab-70ee2521703a")).WithSingularName("ProductAssociation").WithPluralName("ProductAssociations").Build();
            var PartSpecificationStatus = new ClassBuilder(domain, new Guid("f1ae6225-15d0-4359-8188-afb73265a617")).WithSingularName("PartSpecificationStatus").WithPluralName("PartSpecificationStatuses").Build();
            var DistributionChannelRelationship = new ClassBuilder(domain, new Guid("f278459d-6b7f-47cf-ab0e-05c548faab1e")).WithSingularName("DistributionChannelRelationship").WithPluralName("DistributionChannelRelationships").Build();
            var CustomerShipmentObjectState = new ClassBuilder(domain, new Guid("f2d5bb8b-b50f-45e5-accb-c752a4445ad2")).WithSingularName("CustomerShipmentObjectState").WithPluralName("CustomerShipmentObjectStates").Build();
            var PaymentMethod = new InterfaceBuilder(domain, new Guid("f34d5b9b-b940-4885-9744-754dd0eae08d")).WithSingularName("PaymentMethod").WithPluralName("PaymentMethods").Build();
            var GenderType = new ClassBuilder(domain, new Guid("f35745a9-a8d3-4002-a484-6f0fb00a69a2")).WithSingularName("GenderType").WithPluralName("GenderTypes").Build();
            var OrderItem = new InterfaceBuilder(domain, new Guid("f3ef0124-e867-4da2-9323-80fbe1f214c2")).WithSingularName("OrderItem").WithPluralName("OrderItems").Build();
            var Office = new ClassBuilder(domain, new Guid("f444b4be-1703-49b4-918b-2d1aaf27ce5a")).WithSingularName("Office").WithPluralName("Offices").Build();
            var EmailAddress = new ClassBuilder(domain, new Guid("f4b7ea51-eac4-479b-92e8-5109cfeceb77")).WithSingularName("EmailAddress").WithPluralName("EmailAddresses").Build();
            var WorkEffortInventoryAssignment = new ClassBuilder(domain, new Guid("f67e7755-5848-4601-ba70-4d1a39abfe4b")).WithSingularName("WorkEffortInventoryAssignment").WithPluralName("WorkEffortInventoryAssignments").Build();
            var CommunicationEventObjectState = new ClassBuilder(domain, new Guid("f6ad2546-e977-4176-b03d-d30fb101270c")).WithSingularName("CommunicationEventObjectState").WithPluralName("CommunicationEventObjectStates").Build();
            var City = new ClassBuilder(domain, new Guid("f6dceab0-f4a7-435e-abce-ac9f7bd28ae4")).WithSingularName("City").WithPluralName("Cities").Build();
            var PickListObjectState = new ClassBuilder(domain, new Guid("f7108ec0-3203-4e62-b323-2e3a6a527d66")).WithSingularName("PickListObjectState").WithPluralName("PickListObjectStates").Build();
            var MaterialsUsage = new ClassBuilder(domain, new Guid("f77787aa-66af-4d6a-bbe1-ce3d93020185")).WithSingularName("MaterialsUsage").WithPluralName("MaterialsUsages").Build();
            var EmploymentTerminationReason = new ClassBuilder(domain, new Guid("f7b039f4-10f4-4939-8059-5f190fd13b09")).WithSingularName("EmploymentTerminationReason").WithPluralName("EmploymentTerminationReasons").Build();
            var WorkEffortObjectState = new ClassBuilder(domain, new Guid("f7d24734-88d3-47e7-b718-8815914c9ad4")).WithSingularName("WorkEffortObjectState").WithPluralName("WorkEffortObjectStates").Build();
            var TimePeriodUsage = new ClassBuilder(domain, new Guid("f7e69670-1824-44ea-b2cd-fdef02ef84a7")).WithSingularName("TimePeriodUsage").WithPluralName("TimePeriodUsages").Build();
            var BudgetObjectState = new ClassBuilder(domain, new Guid("f8ae512e-bca5-498b-860b-11c06ab04d72")).WithSingularName("BudgetObjectState").WithPluralName("BudgetObjectStates").Build();
            var WorkRequirement = new ClassBuilder(domain, new Guid("fa4303c8-a09d-4dd5-97b3-76459b8e038d")).WithSingularName("WorkRequirement").WithPluralName("WorkRequirements").Build();
            var Fee = new ClassBuilder(domain, new Guid("fb3dd618-eeb5-4ef6-87ca-abfe91dc603f")).WithSingularName("Fee").WithPluralName("Fees").Build();
            var PhoneCommunication = new ClassBuilder(domain, new Guid("fcdf4f00-d6f4-493f-a430-89789a3cdef6")).WithSingularName("PhoneCommunication").WithPluralName("PhoneCommunications").Build();
            var ProductDeliverySkillRequirement = new ClassBuilder(domain, new Guid("fd342cb7-53d3-4377-acd8-ee586b924678")).WithSingularName("ProductDeliverySkillRequirement").WithPluralName("ProductDeliverySkillRequirements").Build();
            var SalesRepProductCategoryRevenue = new ClassBuilder(domain, new Guid("fd411b2a-0121-4f1f-b1db-86c187e8a089")).WithSingularName("SalesRepProductCategoryRevenue").WithPluralName("SalesRepProductCategoryRevenues").Build();
            var ServiceFeature = new ClassBuilder(domain, new Guid("fdbea721-61f8-4e75-b1dd-e3880636ee78")).WithSingularName("ServiceFeature").WithPluralName("ServiceFeatures").Build();
            var PartyProductRevenueHistory = new ClassBuilder(domain, new Guid("fdf777a8-2e6c-45c3-9385-2d53c1aa8469")).WithSingularName("PartyProductRevenueHistory").WithPluralName("PartyProductRevenueHistories").Build();
			
            // Inheritances
            // ProductFeatureApplicabilityRelationship
            new InheritanceBuilder(domain, new Guid("f3cefb71-6ffc-4d50-9458-4fcb6d235381")).WithSubtype(ProductFeatureApplicabilityRelationship).WithSupertype(userInterfaceable).Build();
			
            // PartSpecification
            new InheritanceBuilder(domain, new Guid("16398cb8-bc6d-4ffc-ae21-1bd358698faa")).WithSubtype(PartSpecification).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("5dfa15ec-52fb-450d-b58f-fe8b72182036")).WithSubtype(PartSpecification).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("ea2f5159-9333-4860-bc26-d42776ae639c")).WithSubtype(PartSpecification).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("ef6b7736-4b92-43a4-bcdf-15f974f6148b")).WithSubtype(PartSpecification).WithSupertype(userInterfaceable).Build();
			
            // ProductRequirement
            new InheritanceBuilder(domain, new Guid("7f440f68-d7ef-4ce7-b80c-8fba40b9fd4d")).WithSubtype(ProductRequirement).WithSupertype(Requirement).Build();
			
            // RequestForProposal
            new InheritanceBuilder(domain, new Guid("489dff28-2aaf-42c4-a2ec-45eb42ba34b1")).WithSubtype(RequestForProposal).WithSupertype(Request).Build();
			
            // SalesInvoiceItemStatus
            new InheritanceBuilder(domain, new Guid("91e4110a-25cc-49b3-b44d-355444140474")).WithSubtype(SalesInvoiceItemStatus).WithSupertype(userInterfaceable).Build();
			
            // QuoteItem
            new InheritanceBuilder(domain, new Guid("440f228c-b7a4-42c5-8d1e-85a58a9a3f10")).WithSubtype(QuoteItem).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("4b6d22b2-8bae-44e5-b29d-85ad3aa2bd63")).WithSubtype(QuoteItem).WithSupertype(userInterfaceable).Build();
			
            // SalesRepPartyProductCategoryRevenue
            new InheritanceBuilder(domain, new Guid("48cb2131-bdf7-4aba-8bd4-3ac204045111")).WithSubtype(SalesRepPartyProductCategoryRevenue).WithSupertype(userInterfaceable).Build();
			
            // PayGrade
            new InheritanceBuilder(domain, new Guid("021c31b8-e556-4632-92dc-74a07d4f9f3d")).WithSubtype(PayGrade).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a90c1e14-fd06-4765-b453-54191be8e875")).WithSubtype(PayGrade).WithSupertype(Commentable).Build();
			
            // PartyProductCategoryRevenueHistory
            new InheritanceBuilder(domain, new Guid("2a2ac822-d86d-4260-8340-e7077ce77e20")).WithSubtype(PartyProductCategoryRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // DiscountAdjustment
            new InheritanceBuilder(domain, new Guid("81324939-fae1-4c18-a420-28b41b1fe5a8")).WithSubtype(DiscountAdjustment).WithSupertype(OrderAdjustment).Build();
			
            // Position
            new InheritanceBuilder(domain, new Guid("38946670-4ca1-43c6-933c-ae60e1857166")).WithSubtype(Position).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("5ee8f9c6-b3e1-4dfb-9b48-3d34a2e97581")).WithSubtype(Position).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("dee7bd59-6076-4e24-909d-b6aeeb07a483")).WithSubtype(Position).WithSupertype(Searchable).Build();
			
            // LetterCorrespondence
            new InheritanceBuilder(domain, new Guid("3eb4a962-9834-4bf1-883c-9cda6af83099")).WithSubtype(LetterCorrespondence).WithSupertype(CommunicationEvent).Build();
			
            // PurchaseOrder
            new InheritanceBuilder(domain, new Guid("de9d74cb-1148-4411-9c97-a742cd57e3a4")).WithSubtype(PurchaseOrder).WithSupertype(Order).Build();
			
            // Quote
            new InheritanceBuilder(domain, new Guid("0f5351ea-f5b9-4101-841e-67ac605f3293")).WithSubtype(Quote).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("5921c341-2b71-46f1-9a54-ae32ee7e6849")).WithSubtype(Quote).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("cc6b9b4e-ae84-4511-b216-d339306b6682")).WithSubtype(Quote).WithSupertype(Searchable).Build();
			
            // GlBudgetAllocation
            new InheritanceBuilder(domain, new Guid("7c3ae79e-6137-4c65-815f-c140637c70e7")).WithSubtype(GlBudgetAllocation).WithSupertype(userInterfaceable).Build();
			
            // PartyRelationship
            new InheritanceBuilder(domain, new Guid("39652d34-eea4-4f93-a24a-74ecbc07dcdb")).WithSubtype(PartyRelationship).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("422d340c-9246-4020-90ec-c33975856499")).WithSubtype(PartyRelationship).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("a617b0ed-81db-4dbe-8f26-55438aee4e84")).WithSubtype(PartyRelationship).WithSupertype(userInterfaceable).Build();
			
            // RateType
            new InheritanceBuilder(domain, new Guid("5495321c-9ac9-4631-b3dc-042cefb5bf6f")).WithSubtype(RateType).WithSupertype(Enumeration).Build();
			
            // Brand
            new InheritanceBuilder(domain, new Guid("166b9dc2-958c-4cc4-990c-702b325fa0e2")).WithSubtype(Brand).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("742c02f6-1030-4514-bcc9-2dc70c4c77d0")).WithSubtype(Brand).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("7b151d2a-d991-4e1f-8804-a75ea0df3207")).WithSubtype(Brand).WithSupertype(Searchable).Build();
			
            // SupplierOffering
            new InheritanceBuilder(domain, new Guid("15f94cb3-467e-4362-989e-3e09f935c9dd")).WithSubtype(SupplierOffering).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("2bf4ff17-ed5e-491e-b042-f1c011c92b65")).WithSubtype(SupplierOffering).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("e60784ca-6ec9-4619-a6e3-3fb87b30e0f8")).WithSubtype(SupplierOffering).WithSupertype(userInterfaceable).Build();
			
            // SalesAccountingTransaction
            new InheritanceBuilder(domain, new Guid("39175119-6bb5-4cac-b48c-8e184134bdd7")).WithSubtype(SalesAccountingTransaction).WithSupertype(ExternalAccountingTransaction).Build();
			
            // Vehicle
            new InheritanceBuilder(domain, new Guid("4e47845e-f455-415c-9389-b8b0ab9cdf4a")).WithSubtype(Vehicle).WithSupertype(FixedAsset).Build();
			
            // AccountingTransactionNumber
            new InheritanceBuilder(domain, new Guid("2672aae9-e211-47f8-baa0-c357123dfa20")).WithSubtype(AccountingTransactionNumber).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("d1ed6054-04d9-4318-a5ed-ef11b717bbff")).WithSubtype(AccountingTransactionNumber).WithSupertype(userInterfaceable).Build();
			
            // WorkEffortPartyAssignment
            new InheritanceBuilder(domain, new Guid("8c8c0011-2cd4-4534-b6fc-2b4f39553566")).WithSubtype(WorkEffortPartyAssignment).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("8e2a810a-f464-4307-a975-30a0e7b4de41")).WithSubtype(WorkEffortPartyAssignment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d1c596c2-94c8-4331-8591-c4e02bba7be2")).WithSubtype(WorkEffortPartyAssignment).WithSupertype(Commentable).Build();
			
            // ContactMechanismPurpose
            new InheritanceBuilder(domain, new Guid("9988af68-db93-4ac5-948d-2a20ffc95bd0")).WithSubtype(ContactMechanismPurpose).WithSupertype(Enumeration).Build();
			
            // TaxDocument
            new InheritanceBuilder(domain, new Guid("7138cb8f-09c1-48c8-9ef2-5d13bfa2f94c")).WithSubtype(TaxDocument).WithSupertype(Document).Build();
			
            // Training
            new InheritanceBuilder(domain, new Guid("69d8bdc5-1630-410b-bb97-ca90afb0918c")).WithSubtype(Training).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d378605a-a49d-445d-a0f2-873b5ad3f74e")).WithSubtype(Training).WithSupertype(Searchable).Build();
			
            // PurchaseAgreement
            new InheritanceBuilder(domain, new Guid("ecc849aa-0cbb-489f-a0da-7ed86d6a2301")).WithSubtype(PurchaseAgreement).WithSupertype(Agreement).Build();
			
            // CostCenterCategory
            new InheritanceBuilder(domain, new Guid("0c6e25f1-27e3-4bdf-a00a-8cd8e0f9ec9e")).WithSubtype(CostCenterCategory).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("31584f5d-5afb-4f13-bba9-2f381ec4e642")).WithSubtype(CostCenterCategory).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("84c6b28a-e163-4b13-9546-f63170627c21")).WithSubtype(CostCenterCategory).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("a5ec5ae6-163c-4701-b2d4-bc3c87c7c769")).WithSubtype(CostCenterCategory).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("c063f83b-6847-434b-bf4c-2f96884b7a58")).WithSubtype(CostCenterCategory).WithSupertype(UniquelyIdentifiable).Build();
			
            // BasePrice
            new InheritanceBuilder(domain, new Guid("ea2621d8-86d5-4203-a8db-30fea5fc218f")).WithSubtype(BasePrice).WithSupertype(PriceComponent).Build();
			
            // JournalEntry
            new InheritanceBuilder(domain, new Guid("0b52e392-122f-40de-b7b0-280b5331695f")).WithSubtype(JournalEntry).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("2c217c25-84d0-4362-9f64-f8b4a3f8ad94")).WithSubtype(JournalEntry).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("5aa52ac0-3162-41ec-8cb1-5a60799cc478")).WithSubtype(JournalEntry).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("ce4263b9-9d12-460a-aab9-d2f3641b9282")).WithSubtype(JournalEntry).WithSupertype(Searchable).Build();
			
            // SubAgreement
            new InheritanceBuilder(domain, new Guid("e7f1378a-902e-4356-9312-24e1cb1ce9f1")).WithSubtype(SubAgreement).WithSupertype(AgreementItem).Build();
			
            // Skill
            new InheritanceBuilder(domain, new Guid("cbfaada0-9a37-4541-8e12-953ddafae04e")).WithSubtype(Skill).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("d3131e12-55c8-47dd-bda8-8ff878f6e4cd")).WithSubtype(Skill).WithSupertype(Searchable).Build();
			
            // EmploymentTermination
            new InheritanceBuilder(domain, new Guid("8408f31f-3091-4ed5-acb2-34d431ce382a")).WithSubtype(EmploymentTermination).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("fa5f0af7-4829-4571-a6b2-37e8bc51c0d7")).WithSubtype(EmploymentTermination).WithSupertype(Enumeration).Build();
			
            // FinancialAccountAdjustment
            new InheritanceBuilder(domain, new Guid("e6b3756b-51d7-4614-9681-2a8e431d59f6")).WithSubtype(FinancialAccountAdjustment).WithSupertype(FinancialAccountTransaction).Build();
			
            // Service
            new InheritanceBuilder(domain, new Guid("37717b97-25e0-4e7b-a505-0b21d97f86da")).WithSubtype(Service).WithSupertype(Product).Build();
			
            // OperatingCondition
            new InheritanceBuilder(domain, new Guid("5de004c7-10fd-42e8-9d85-c124f597660e")).WithSubtype(OperatingCondition).WithSupertype(PartSpecification).Build();
			
            // TermType
            new InheritanceBuilder(domain, new Guid("8faa4782-f340-4550-aa37-7c861eb4be67")).WithSubtype(TermType).WithSupertype(Enumeration).Build();
			
            // LegalTerm
            new InheritanceBuilder(domain, new Guid("bbde0f72-8943-4d3d-b89f-769deeffc0c4")).WithSubtype(LegalTerm).WithSupertype(AgreementTerm).Build();
			
            // EngineeringBom
            new InheritanceBuilder(domain, new Guid("23049d7d-0902-4282-a549-92e8b2d2fa6e")).WithSubtype(EngineeringBom).WithSupertype(PartBillOfMaterial).Build();
			
            // PurchaseInvoiceItemType
            new InheritanceBuilder(domain, new Guid("4066b39e-d8a2-4ecc-a04a-f2dc64bf03af")).WithSubtype(PurchaseInvoiceItemType).WithSupertype(Enumeration).Build();
			
            // Incentive
            new InheritanceBuilder(domain, new Guid("903ba144-a54d-427f-a795-7636c5e3e3cc")).WithSubtype(Incentive).WithSupertype(AgreementTerm).Build();
			
            // PurchaseReturnStatus
            new InheritanceBuilder(domain, new Guid("326e3cb6-271d-4fa1-a2de-4a92f473b86b")).WithSubtype(PurchaseReturnStatus).WithSupertype(userInterfaceable).Build();
			
            // PartyRevenueHistory
            new InheritanceBuilder(domain, new Guid("e70ab0a4-5380-4fb2-9347-7fc84de117d5")).WithSubtype(PartyRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // PartSpecificationObjectState
            new InheritanceBuilder(domain, new Guid("8726fbb2-85ea-42ce-aaba-ace565993c5f")).WithSubtype(PartSpecificationObjectState).WithSupertype(ObjectState).Build();
			
            // PositionTypeRate
            new InheritanceBuilder(domain, new Guid("43c0c7ce-dc60-4fc7-88a3-1d2544578527")).WithSubtype(PositionTypeRate).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("931e88dc-f97e-4b1d-a94a-41ee1b9ff0d1")).WithSubtype(PositionTypeRate).WithSupertype(userInterfaceable).Build();
			
            // RatingType
            new InheritanceBuilder(domain, new Guid("a7b72e98-5c02-4e0a-b57c-ccbebfbbb994")).WithSubtype(RatingType).WithSupertype(Enumeration).Build();
			
            // PurchaseInvoiceType
            new InheritanceBuilder(domain, new Guid("00a50c26-1004-4d67-a6ab-d992c57a1811")).WithSubtype(PurchaseInvoiceType).WithSupertype(Enumeration).Build();
			
            // GeneralLedgerAccount
            new InheritanceBuilder(domain, new Guid("0e82896b-a3da-4431-b6d5-cd5a3f3399ec")).WithSubtype(GeneralLedgerAccount).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("a6ebf6a0-072a-4d3a-8112-0bda7c430e42")).WithSubtype(GeneralLedgerAccount).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f1d74ed0-165e-41ed-804a-e6ce62f9b94e")).WithSubtype(GeneralLedgerAccount).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("f5468b54-bee8-4e71-bbff-19e357831c13")).WithSubtype(GeneralLedgerAccount).WithSupertype(SearchResult).Build();
			
            // ShippingAndHandlingComponent
            new InheritanceBuilder(domain, new Guid("645ac3fe-82ab-4d7b-b972-630df838ed1c")).WithSubtype(ShippingAndHandlingComponent).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("b28feb35-aec2-451c-95ee-2c0cdd5a090c")).WithSubtype(ShippingAndHandlingComponent).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("c98d9a4c-a7d5-4b39-b4b4-224b73fe1a9c")).WithSubtype(ShippingAndHandlingComponent).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("dab85876-5436-46cd-ac38-3a907a925f44")).WithSubtype(ShippingAndHandlingComponent).WithSupertype(Searchable).Build();
			
            // PersonalTitle
            new InheritanceBuilder(domain, new Guid("7acbe506-369f-4373-86ad-a6bd25df1d09")).WithSubtype(PersonalTitle).WithSupertype(Enumeration).Build();
			
            // ReceiptAccountingTransaction
            new InheritanceBuilder(domain, new Guid("f21f366f-ade9-478a-8a8b-c15354d019df")).WithSubtype(ReceiptAccountingTransaction).WithSupertype(ExternalAccountingTransaction).Build();
			
            // TimeFrequency
            new InheritanceBuilder(domain, new Guid("544d5778-7a4a-4da6-adf8-b46b8b521327")).WithSubtype(TimeFrequency).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("c7aaa04d-016a-4a7b-8f78-399ca0224752")).WithSubtype(TimeFrequency).WithSupertype(IUnitOfMeasure).Build();
			
            // SubContractorAgreement
            new InheritanceBuilder(domain, new Guid("96b23739-dc6a-4988-9ff0-84fa90804977")).WithSubtype(SubContractorAgreement).WithSupertype(Agreement).Build();
			
            // PackagingContent
            new InheritanceBuilder(domain, new Guid("d13003bb-9eee-4369-8e0e-8665fb3aa2c0")).WithSubtype(PackagingContent).WithSupertype(userInterfaceable).Build();
			
            // PartySkill
            new InheritanceBuilder(domain, new Guid("9dbd27e6-0c94-4ca8-b037-5cba9c57b4a9")).WithSubtype(PartySkill).WithSupertype(userInterfaceable).Build();
			
            // Document
            new InheritanceBuilder(domain, new Guid("38487973-8cf7-4648-b679-b8988f4f9cfb")).WithSubtype(Document).WithSupertype(Printable).Build();
            new InheritanceBuilder(domain, new Guid("38f3c0fa-2aaf-4c6b-9cc9-8ac46315cf00")).WithSubtype(Document).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("40d2852a-9226-4261-b58c-3e28d89e781a")).WithSubtype(Document).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("7162efa7-01da-4df3-a940-fd5d41a9dd7f")).WithSubtype(Document).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("d2ef94e2-99a1-40c3-84e3-cb4feb7f268b")).WithSubtype(Document).WithSupertype(SearchResult).Build();
			
            // SerializedInventoryItemStatus
            new InheritanceBuilder(domain, new Guid("aef6b599-8590-469a-902d-41192a5991f5")).WithSubtype(SerializedInventoryItemStatus).WithSupertype(userInterfaceable).Build();
			
            // FaxCommunication
            new InheritanceBuilder(domain, new Guid("c437d741-9891-43f9-b8f4-ccbbcff056ed")).WithSubtype(FaxCommunication).WithSupertype(CommunicationEvent).Build();
			
            // PurchaseInvoiceItem
            new InheritanceBuilder(domain, new Guid("7536e46d-2c16-4dfd-83ce-7f080ce32056")).WithSubtype(PurchaseInvoiceItem).WithSupertype(InvoiceItem).Build();
			
            // ProductDrawing
            new InheritanceBuilder(domain, new Guid("8f231f94-2f90-44a0-9315-272b95427ffe")).WithSubtype(ProductDrawing).WithSupertype(Document).Build();
			
            // PayHistory
            new InheritanceBuilder(domain, new Guid("97061b68-ceb2-4c1b-aaae-a193fed344d7")).WithSubtype(PayHistory).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a1aaba5c-b837-4b2a-b1bf-040302eb437c")).WithSubtype(PayHistory).WithSupertype(Period).Build();
			
            // ShipmentValue
            new InheritanceBuilder(domain, new Guid("25a23ae0-46ab-4683-8f98-8708522f0068")).WithSubtype(ShipmentValue).WithSupertype(userInterfaceable).Build();
			
            // InternalOrganisationAccountingPreference
            new InheritanceBuilder(domain, new Guid("2e117303-f4f3-40af-b6cd-a92bfeca4506")).WithSubtype(InternalOrganisationAccountingPreference).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("3ad0333f-eadb-4975-8ca9-f3610c4d371f")).WithSubtype(InternalOrganisationAccountingPreference).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("78fa1cf6-7b25-4612-8415-234e5a2642fd")).WithSubtype(InternalOrganisationAccountingPreference).WithSupertype(Searchable).Build();
			
            // PurchaseShipmentObjectState
            new InheritanceBuilder(domain, new Guid("c0c59cf9-f137-4b89-8d2e-fc82c3d63d39")).WithSubtype(PurchaseShipmentObjectState).WithSupertype(ObjectState).Build();
			
            // SalesOrderItemObjectState
            new InheritanceBuilder(domain, new Guid("84e11562-6cb1-471a-a91b-f769199d0e75")).WithSubtype(SalesOrderItemObjectState).WithSupertype(ObjectState).Build();
			
            // BankAccount
            new InheritanceBuilder(domain, new Guid("8df0a5f1-ddd5-4a20-87e5-00f369c9e6a4")).WithSubtype(BankAccount).WithSupertype(FinancialAccount).Build();
			
            // ServiceEntryHeader
            new InheritanceBuilder(domain, new Guid("71ce40ce-9bec-4793-a275-5d4eca7fefb4")).WithSubtype(ServiceEntryHeader).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("c8bd934b-b7f2-4295-bb59-55b510cf0639")).WithSubtype(ServiceEntryHeader).WithSupertype(userInterfaceable).Build();
			
            // PartRevision
            new InheritanceBuilder(domain, new Guid("946f6f5c-01ef-4db2-a285-c40c7b76b19e")).WithSubtype(PartRevision).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("d6ba631b-0e52-417d-aec7-35fb6b0f86b1")).WithSubtype(PartRevision).WithSupertype(userInterfaceable).Build();
			
            // PurchaseReturnObjectState
            new InheritanceBuilder(domain, new Guid("8ce3de24-7535-42c1-9953-a249a7d658a5")).WithSubtype(PurchaseReturnObjectState).WithSupertype(ObjectState).Build();
			
            // ProductConfiguration
            new InheritanceBuilder(domain, new Guid("0a9d4662-d1bb-46dc-b534-b8663da6cecd")).WithSubtype(ProductConfiguration).WithSupertype(ProductAssociation).Build();
			
            // OwnCreditCard
            new InheritanceBuilder(domain, new Guid("0a98daed-940f-42fb-acea-718acd0ce1a5")).WithSubtype(OwnCreditCard).WithSupertype(PaymentMethod).Build();
            new InheritanceBuilder(domain, new Guid("0ba36575-ca62-448b-b6ec-08601b5436c7")).WithSubtype(OwnCreditCard).WithSupertype(FinancialAccount).Build();
			
            // Dimension
            new InheritanceBuilder(domain, new Guid("722e2b6a-ef37-4d58-8d4e-ec247e9003df")).WithSubtype(Dimension).WithSupertype(ProductFeature).Build();
			
            // SalesInvoiceItemType
            new InheritanceBuilder(domain, new Guid("1233069b-7012-44aa-bafa-1e567361f161")).WithSubtype(SalesInvoiceItemType).WithSupertype(Enumeration).Build();
			
            // Model
            new InheritanceBuilder(domain, new Guid("906a7290-c097-465f-afb1-f2469ff440d0")).WithSubtype(Model).WithSupertype(ProductFeature).Build();
            new InheritanceBuilder(domain, new Guid("ac18467a-2bb0-49ad-a3fa-eec3de4333ad")).WithSubtype(Model).WithSupertype(Enumeration).Build();
			
            // FinancialAccount
            new InheritanceBuilder(domain, new Guid("3b80335c-4145-4276-bce3-d0894c4872a5")).WithSubtype(FinancialAccount).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("7f11f312-8c78-4d52-8a3b-9bbdbe10fb84")).WithSubtype(FinancialAccount).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("df940b2c-c10e-4e2c-b7e7-f2aed84259ca")).WithSubtype(FinancialAccount).WithSupertype(Searchable).Build();
			
            // PickList
            new InheritanceBuilder(domain, new Guid("2640c852-86a9-4dce-8081-773a2c9e77b1")).WithSubtype(PickList).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("3cb74ad0-2fa0-431a-9b16-41839b347e6c")).WithSubtype(PickList).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("45e10918-bcd3-4d20-b93a-759d0684913d")).WithSubtype(PickList).WithSupertype(Printable).Build();
            new InheritanceBuilder(domain, new Guid("4c25568f-f736-4a62-8109-93492f5de34c")).WithSubtype(PickList).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("b6b1055b-7969-4880-a6db-0ab187ebef36")).WithSubtype(PickList).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("e36c247e-3280-4ba4-bb2a-42fee831d6cd")).WithSubtype(PickList).WithSupertype(UniquelyIdentifiable).Build();
			
            // StoreRevenue
            new InheritanceBuilder(domain, new Guid("a67f5d14-585d-4e39-b117-aa80a108e2c2")).WithSubtype(StoreRevenue).WithSupertype(userInterfaceable).Build();
			
            // AgreementExhibit
            new InheritanceBuilder(domain, new Guid("008d92b7-3163-4d13-b0c1-2727fc7dd229")).WithSubtype(AgreementExhibit).WithSupertype(AgreementItem).Build();
			
            // ProductCategoryRevenueHistory
            new InheritanceBuilder(domain, new Guid("bc26c18c-fc03-4e1d-8e9a-0fc8a584b132")).WithSubtype(ProductCategoryRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // SalesRepRevenueHistory
            new InheritanceBuilder(domain, new Guid("d4f05692-ba29-4456-8879-b0999327c26c")).WithSubtype(SalesRepRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // EstimatedLaborCost
            new InheritanceBuilder(domain, new Guid("3b832558-bc71-4e9e-898a-44683176d1c3")).WithSubtype(EstimatedLaborCost).WithSupertype(EstimatedProductCost).Build();
			
            // CostCenter
            new InheritanceBuilder(domain, new Guid("8f4ddaed-6153-4a86-85d2-b66c99cdea3b")).WithSubtype(CostCenter).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("9b044f28-b018-4054-836c-2a7b88c5725d")).WithSubtype(CostCenter).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("a2823df4-6e62-488a-b6a4-e515b43c46b0")).WithSubtype(CostCenter).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("af393a89-a165-4b0a-8e49-870f1e59d5ce")).WithSubtype(CostCenter).WithSupertype(AccessControlledObject).Build();
			
            // SupplierRelationship
            new InheritanceBuilder(domain, new Guid("5f05c923-68df-432e-b7b2-0f640c46b780")).WithSubtype(SupplierRelationship).WithSupertype(PartyRelationship).Build();
			
            // SkillRating
            new InheritanceBuilder(domain, new Guid("ea19e52a-ae82-413e-8091-c698ad14a825")).WithSubtype(SkillRating).WithSupertype(Enumeration).Build();
			
            // Building
            new InheritanceBuilder(domain, new Guid("c220f671-ba63-42ed-bb83-8587168e9ee7")).WithSubtype(Building).WithSupertype(Facility).Build();
			
            // PurchaseShipment
            new InheritanceBuilder(domain, new Guid("031e2bae-0ce0-4603-bd76-ca511322e057")).WithSubtype(PurchaseShipment).WithSupertype(Shipment).Build();
			
            // UnitOfMeasureConversion
            new InheritanceBuilder(domain, new Guid("df4857ae-5481-4aa3-9b57-589d71ed6cbf")).WithSubtype(UnitOfMeasureConversion).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("ebefce63-c257-4683-817e-dc2a4d2ae1a0")).WithSubtype(UnitOfMeasureConversion).WithSupertype(userInterfaceable).Build();
			
            // VatRateUsage
            new InheritanceBuilder(domain, new Guid("5d369ee9-81f9-4e79-844a-26c59633ff05")).WithSubtype(VatRateUsage).WithSupertype(Enumeration).Build();
			
            // Project
            new InheritanceBuilder(domain, new Guid("6dd1e720-68f6-419f-8c0c-d1835dd2f571")).WithSubtype(Project).WithSupertype(WorkEffort).Build();
			
            // PaymentBudgetAllocation
            new InheritanceBuilder(domain, new Guid("ab0678a9-3b61-4a5c-a954-c860a60fe2cb")).WithSubtype(PaymentBudgetAllocation).WithSupertype(userInterfaceable).Build();
			
            // Hobby
            new InheritanceBuilder(domain, new Guid("32deba3b-37a5-4937-828d-258af57becb6")).WithSubtype(Hobby).WithSupertype(Enumeration).Build();
			
            // ProductRevenueHistory
            new InheritanceBuilder(domain, new Guid("177095e7-97f7-4e91-8111-ae5920c9361f")).WithSubtype(ProductRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // OrderRequirementCommitment
            new InheritanceBuilder(domain, new Guid("1a03b824-cbb7-439a-bbe8-7acace650db7")).WithSubtype(OrderRequirementCommitment).WithSupertype(userInterfaceable).Build();
			
            // OrganisationRollUp
            new InheritanceBuilder(domain, new Guid("a661f08f-1a37-4bda-b3f8-de0c47861cad")).WithSubtype(OrganisationRollUp).WithSupertype(PartyRelationship).Build();
			
            // Request
            new InheritanceBuilder(domain, new Guid("3ce07bc3-5a82-4993-b0e0-ef6833e8cabd")).WithSubtype(Request).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("6267fcb3-9db3-4a69-a8ae-38cc5fe228ee")).WithSubtype(Request).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("a9baff2d-bc93-432d-94ad-5f3336cc3fad")).WithSubtype(Request).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("b0313c57-0749-473a-b5d0-ddf43e30529b")).WithSubtype(Request).WithSupertype(Commentable).Build();
			
            // AccountingTransactionType
            new InheritanceBuilder(domain, new Guid("ff2fffb1-69c8-4208-8c23-4d968e670a8f")).WithSubtype(AccountingTransactionType).WithSupertype(Enumeration).Build();
			
            // RevenueValueBreak
            new InheritanceBuilder(domain, new Guid("34f457f1-b219-41e1-9f5e-4c419c91cb7e")).WithSubtype(RevenueValueBreak).WithSupertype(userInterfaceable).Build();
			
            // Activity
            new InheritanceBuilder(domain, new Guid("b19b9613-0bc5-4b88-957e-cc7e14de2166")).WithSubtype(Activity).WithSupertype(WorkEffort).Build();
			
            // WorkEffortAssignment
            new InheritanceBuilder(domain, new Guid("a3608939-4fb3-435b-81e2-96e14cf8ded3")).WithSubtype(WorkEffortAssignment).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("d6b8db6e-f84c-43f3-a9b9-76c4fac83975")).WithSubtype(WorkEffortAssignment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d9e7cf66-efa6-43cd-befc-52f8a622dcd5")).WithSubtype(WorkEffortAssignment).WithSupertype(Commentable).Build();
			
            // SoftwareFeature
            new InheritanceBuilder(domain, new Guid("0eef3897-278f-4f1c-b022-c20b9281abe8")).WithSubtype(SoftwareFeature).WithSupertype(ProductFeature).Build();
            new InheritanceBuilder(domain, new Guid("3bddf31b-b9a3-4280-ab0b-40f627ee245f")).WithSubtype(SoftwareFeature).WithSupertype(Enumeration).Build();
			
            // GeographicBoundary
            new InheritanceBuilder(domain, new Guid("4961f731-7b6d-4b40-b89a-19514516b525")).WithSubtype(GeographicBoundary).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("70787d0f-9e4c-4f9f-b102-be6269268931")).WithSubtype(GeographicBoundary).WithSupertype(GeoLocatable).Build();
            new InheritanceBuilder(domain, new Guid("869ccd69-a79e-49af-a5d1-75c7cb97f50b")).WithSubtype(GeographicBoundary).WithSupertype(userInterfaceable).Build();
			
            // SalesOrderStatus
            new InheritanceBuilder(domain, new Guid("8edb1e49-b357-4fd5-a400-8d4187ddc997")).WithSubtype(SalesOrderStatus).WithSupertype(userInterfaceable).Build();
			
            // BillingAccount
            new InheritanceBuilder(domain, new Guid("f3202cca-3bd1-4d66-a5d8-36d81121ab2c")).WithSubtype(BillingAccount).WithSupertype(userInterfaceable).Build();
			
            // SalesChannelRevenue
            new InheritanceBuilder(domain, new Guid("1f184480-e3ef-446b-96bf-eda004043685")).WithSubtype(SalesChannelRevenue).WithSupertype(userInterfaceable).Build();
			
            // AutomatedAgent
            new InheritanceBuilder(domain, new Guid("0b48e079-3553-47e8-91ac-09e6599192ef")).WithSubtype(AutomatedAgent).WithSupertype(User).Build();
            new InheritanceBuilder(domain, new Guid("9d83c4ff-0012-4f0d-a812-9c0dccb861fa")).WithSubtype(AutomatedAgent).WithSupertype(Party).Build();
			
            // SalesChannelRevenueHistory
            new InheritanceBuilder(domain, new Guid("aa17b4bd-d5cc-4ce9-a468-befb6f5d6031")).WithSubtype(SalesChannelRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // Proposal
            new InheritanceBuilder(domain, new Guid("695047c0-4c19-4bb4-a805-2978aa189935")).WithSubtype(Proposal).WithSupertype(Quote).Build();
			
            // FinishedGood
            new InheritanceBuilder(domain, new Guid("d9a201d9-7a0c-422a-ac09-b5b96c420d30")).WithSubtype(FinishedGood).WithSupertype(Part).Build();
			
            // PerformanceSpecification
            new InheritanceBuilder(domain, new Guid("0ea499b9-ee5a-4334-80fd-9881fd541825")).WithSubtype(PerformanceSpecification).WithSupertype(PartSpecification).Build();
			
            // ProductionRun
            new InheritanceBuilder(domain, new Guid("71e44ea6-322c-446d-98f6-23ea63e1687e")).WithSubtype(ProductionRun).WithSupertype(WorkEffort).Build();
			
            // PriceComponent
            new InheritanceBuilder(domain, new Guid("77daf73f-b13f-4f55-baaa-7961bd54703f")).WithSubtype(PriceComponent).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("879988ca-4405-4774-846a-ecd4565852bd")).WithSubtype(PriceComponent).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("ae531935-828e-4821-a7f3-e20614363dd7")).WithSubtype(PriceComponent).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("dc609e2b-00c7-4add-8579-5473d04be9cc")).WithSubtype(PriceComponent).WithSupertype(Commentable).Build();
			
            // Ordinal
            new InheritanceBuilder(domain, new Guid("aea7b3cb-d7c2-4ea6-ade9-c15beaf0af3a")).WithSubtype(Ordinal).WithSupertype(Enumeration).Build();
			
            // Citizenship
            new InheritanceBuilder(domain, new Guid("e0c43af5-68f7-4919-85df-3ad144b0520d")).WithSubtype(Citizenship).WithSupertype(userInterfaceable).Build();
			
            // PartyProductRevenue
            new InheritanceBuilder(domain, new Guid("ea9dfdd5-9c03-4001-b5bb-565630b41858")).WithSubtype(PartyProductRevenue).WithSupertype(userInterfaceable).Build();
			
            // ShipmentMethod
            new InheritanceBuilder(domain, new Guid("f4b3473c-64ba-40aa-92a7-f64ce7441fce")).WithSubtype(ShipmentMethod).WithSupertype(Enumeration).Build();
			
            // Organisation
            new InheritanceBuilder(domain, new Guid("1a582f73-918e-4b22-8f64-0528b8e3bee5")).WithSubtype(Organisation).WithSupertype(Party).Build();
			
            // Responsibility
            new InheritanceBuilder(domain, new Guid("4347b57b-e1a2-46bf-9208-e3d3f1177ea2")).WithSubtype(Responsibility).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("6aadd894-5933-4459-9763-3a2ee886c28d")).WithSubtype(Responsibility).WithSupertype(Searchable).Build();
			
            // VatReturnBoxType
            new InheritanceBuilder(domain, new Guid("75433f59-23e4-4189-a931-17edd101f0cd")).WithSubtype(VatReturnBoxType).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("93767ef7-084f-4f12-a5da-7a7cfed848c4")).WithSubtype(VatReturnBoxType).WithSupertype(userInterfaceable).Build();
			
            // DebitCreditConstant
            new InheritanceBuilder(domain, new Guid("832cc43a-eb35-471e-811a-9470cf653fb8")).WithSubtype(DebitCreditConstant).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("cdeaf33a-6dff-4d48-af5d-39de3877c4a7")).WithSubtype(DebitCreditConstant).WithSupertype(Enumeration).Build();
			
            // WorkEffortFixedAssetAssignment
            new InheritanceBuilder(domain, new Guid("7ecb2da8-097f-4a7d-b6b7-ddcfa4223aca")).WithSubtype(WorkEffortFixedAssetAssignment).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("8a9894fe-bfa8-41ea-bfa1-39965094a6ae")).WithSubtype(WorkEffortFixedAssetAssignment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("b3afcd5a-3106-4e61-9b1f-9e2b1fe7193b")).WithSubtype(WorkEffortFixedAssetAssignment).WithSupertype(Period).Build();
			
            // VatCalculationMethod
            new InheritanceBuilder(domain, new Guid("16cfed20-b456-4bdd-b692-0184175ce67a")).WithSubtype(VatCalculationMethod).WithSupertype(Enumeration).Build();
			
            // GeographicBoundaryComposite
            new InheritanceBuilder(domain, new Guid("65646d36-ab8f-4e94-8bf0-f2782eea1b1d")).WithSubtype(GeographicBoundaryComposite).WithSupertype(GeographicBoundary).Build();
			
            // InvoiceSequence
            new InheritanceBuilder(domain, new Guid("a3187477-ee29-4b07-9d9a-e301a2e87619")).WithSubtype(InvoiceSequence).WithSupertype(Enumeration).Build();
			
            // CustomerRelationship
            new InheritanceBuilder(domain, new Guid("295032a0-f46b-443a-82de-ecaee5c100cd")).WithSubtype(CustomerRelationship).WithSupertype(PartyRelationship).Build();
			
            // PartyClassification
            new InheritanceBuilder(domain, new Guid("ae6ca76c-ad6f-4568-a9c5-acc7784c6652")).WithSubtype(PartyClassification).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("af631191-63ce-49f2-b16e-d6e45c8b79c2")).WithSubtype(PartyClassification).WithSupertype(Searchable).Build();
			
            // Party
            new InheritanceBuilder(domain, new Guid("1923a622-1cc8-4a31-b8f3-e27b774d7d48")).WithSubtype(Party).WithSupertype(Localised).Build();
            new InheritanceBuilder(domain, new Guid("2191de50-3b5d-4e30-ae99-8c14147bb902")).WithSubtype(Party).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("2d0ced68-6122-4dc4-8ca9-14914c7fad9f")).WithSubtype(Party).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("3f8cc951-1880-43ef-a334-d7ef3ad23178")).WithSubtype(Party).WithSupertype(SecurityTokenOwner).Build();
            new InheritanceBuilder(domain, new Guid("9c34331c-4310-4927-84cd-e4f64210cd66")).WithSubtype(Party).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("e5e95be8-b90d-4334-9d2c-6d5a609b8edf")).WithSubtype(Party).WithSupertype(Searchable).Build();
			
            // PartyProductCategoryRevenue
            new InheritanceBuilder(domain, new Guid("0ffbf75e-ef5f-44cd-bd6c-fa0fc95acd69")).WithSubtype(PartyProductCategoryRevenue).WithSupertype(userInterfaceable).Build();
			
            // PartyFixedAssetAssignment
            new InheritanceBuilder(domain, new Guid("2aac3cb3-7d19-4e9c-adc4-f4a806d2d623")).WithSubtype(PartyFixedAssetAssignment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("53ed5712-25fc-4cee-8d30-ddf6c04fe2ed")).WithSubtype(PartyFixedAssetAssignment).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("7e4e0360-1112-45ac-9751-44239db196d2")).WithSubtype(PartyFixedAssetAssignment).WithSupertype(Commentable).Build();
		
            // CapitalBudget
            new InheritanceBuilder(domain, new Guid("c56806b9-4717-4362-a768-fce5acc52412")).WithSubtype(CapitalBudget).WithSupertype(Budget).Build();
			
            // AccountAdjustment
            new InheritanceBuilder(domain, new Guid("2b17c603-8f3a-49c1-a4b1-b41b750382bb")).WithSubtype(AccountAdjustment).WithSupertype(FinancialAccountTransaction).Build();
			
            // PositionStatus
            new InheritanceBuilder(domain, new Guid("a34868c5-9eda-4e23-86fc-571155e1cd40")).WithSubtype(PositionStatus).WithSupertype(Enumeration).Build();
			
            // MarketingPackage
            new InheritanceBuilder(domain, new Guid("3312261a-0613-4cc9-baff-0312e46acc6e")).WithSubtype(MarketingPackage).WithSupertype(ProductAssociation).Build();
			
            // ItemIssuance
            new InheritanceBuilder(domain, new Guid("d386f7ae-da14-4533-a99f-9304305da23c")).WithSubtype(ItemIssuance).WithSupertype(userInterfaceable).Build();
			
            // ShipmentPackage
            new InheritanceBuilder(domain, new Guid("1d16e18e-4856-47ef-a45d-f35557fe25cb")).WithSubtype(ShipmentPackage).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("7e350f10-3653-457b-a6d9-a0be0b7ab2c4")).WithSubtype(ShipmentPackage).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("81ec323e-f898-42df-bcf5-f7ea5a8368f9")).WithSubtype(ShipmentPackage).WithSupertype(Printable).Build();
			
            // CommunicationAttachment
            new InheritanceBuilder(domain, new Guid("a9835fdb-fac6-4a42-82d2-437752f4acbc")).WithSubtype(CommunicationAttachment).WithSupertype(userInterfaceable).Build();
			
            // PurchaseOrderObjectState
            new InheritanceBuilder(domain, new Guid("5b5725a5-1ece-4fe7-94c8-e99406a9b7b9")).WithSubtype(PurchaseOrderObjectState).WithSupertype(ObjectState).Build();
			
            // Size
            new InheritanceBuilder(domain, new Guid("47273951-d7ac-42c2-97da-73d6e60fc68c")).WithSubtype(Size).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("dd0db553-f0ce-4f3b-8dea-a35250c24c59")).WithSubtype(Size).WithSupertype(ProductFeature).Build();
			
            // PerformanceNote
            new InheritanceBuilder(domain, new Guid("b7006b16-03eb-49bb-a015-5e10d0c85eb5")).WithSubtype(PerformanceNote).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("bcfffb95-6ad2-44b7-abdf-52243bf1d866")).WithSubtype(PerformanceNote).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("cdb980d6-05f8-4923-bf32-95a57179fba4")).WithSubtype(PerformanceNote).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("faf81efd-9120-4803-bd34-ec48e4ec5f6b")).WithSubtype(PerformanceNote).WithSupertype(SearchResult).Build();
			
            // DeliverableTurnover
            new InheritanceBuilder(domain, new Guid("07cc4021-aeb5-408a-a743-3d144e191b7f")).WithSubtype(DeliverableTurnover).WithSupertype(ServiceEntry).Build();
			
            // ShipmentReceipt
            new InheritanceBuilder(domain, new Guid("809fc167-7b57-4799-84db-6c03dbf439d7")).WithSubtype(ShipmentReceipt).WithSupertype(userInterfaceable).Build();
			
            // RequirementCommunication
            new InheritanceBuilder(domain, new Guid("e5d50313-e66d-4e75-984a-c1c47fb5f22e")).WithSubtype(RequirementCommunication).WithSupertype(userInterfaceable).Build();
			
            // FixedAsset
            new InheritanceBuilder(domain, new Guid("7c05a810-1033-46d9-9cb9-db95e286d7e6")).WithSubtype(FixedAsset).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d9f3b938-098f-41ec-a92e-ce7ad284ab60")).WithSubtype(FixedAsset).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("f1bb74f0-99e5-49eb-becf-19550f0850fb")).WithSubtype(FixedAsset).WithSupertype(Searchable).Build();
			
            // ServiceEntry
            new InheritanceBuilder(domain, new Guid("56f55fdb-d1c2-4294-b0ff-37660c8ac68e")).WithSubtype(ServiceEntry).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("7d989675-b7f4-4e3e-921c-3715e6766192")).WithSubtype(ServiceEntry).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("8505bffe-9dde-4324-86f4-720cbdf6a229")).WithSubtype(ServiceEntry).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("b47bbbf4-4ab8-4173-8afe-06bd4c745269")).WithSubtype(ServiceEntry).WithSupertype(SearchResult).Build();
			
            // GeneralLedgerAccountGroup
            new InheritanceBuilder(domain, new Guid("8829185e-d926-444b-b96e-285bdbb703e3")).WithSubtype(GeneralLedgerAccountGroup).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("8ec9a4a4-8d39-4baf-87dd-4b216ddb33be")).WithSubtype(GeneralLedgerAccountGroup).WithSupertype(userInterfaceable).Build();
			
            // SerializedInventoryItem
            new InheritanceBuilder(domain, new Guid("fada58db-f65e-4448-9ed3-ccd76f51df06")).WithSubtype(SerializedInventoryItem).WithSupertype(InventoryItem).Build();
			
            // ItemVarianceAccountingTransaction
            new InheritanceBuilder(domain, new Guid("0fc35a47-64b1-49a6-a349-8996ccf3f3d0")).WithSubtype(ItemVarianceAccountingTransaction).WithSupertype(AccountingTransaction).Build();
			
            // RespondingParty
            new InheritanceBuilder(domain, new Guid("246c150b-81ec-4226-b5b9-b11692b2a409")).WithSubtype(RespondingParty).WithSupertype(userInterfaceable).Build();
			
            // SalesInvoiceItemObjectState
            new InheritanceBuilder(domain, new Guid("253ab32a-157e-46a9-8deb-fe7451bad374")).WithSubtype(SalesInvoiceItemObjectState).WithSupertype(ObjectState).Build();
			
            // BudgetStatus
            new InheritanceBuilder(domain, new Guid("576ca115-cb32-4251-b17b-1e11920c2fcb")).WithSubtype(BudgetStatus).WithSupertype(userInterfaceable).Build();
			
            // Barrel
            new InheritanceBuilder(domain, new Guid("cac12ca4-11a2-4f3f-8d85-eec7bf6a056d")).WithSubtype(Barrel).WithSupertype(Container).Build();
			
            // PositionType
            new InheritanceBuilder(domain, new Guid("423515f6-3a6f-4264-95e4-51bcf5be667a")).WithSubtype(PositionType).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("d746d19e-c509-4ae1-9225-677d99f303d7")).WithSubtype(PositionType).WithSupertype(userInterfaceable).Build();
			
            // Agreement
            new InheritanceBuilder(domain, new Guid("15a03fe1-2c33-4631-9c76-4187b5be4764")).WithSubtype(Agreement).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("7bdb47d6-0bdd-4cda-984d-17716320499d")).WithSubtype(Agreement).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("a563e151-93ed-46c1-8a64-ca639560b587")).WithSubtype(Agreement).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("e0cc63cf-449c-4293-8d2b-6f5c8ed0b1fe")).WithSubtype(Agreement).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("e52a8bb1-27dd-4eca-9078-a1639308381c")).WithSubtype(Agreement).WithSupertype(Period).Build();
			
            // ProductPurchasePrice
            new InheritanceBuilder(domain, new Guid("0daa0581-bf34-4f4a-b935-cbfa828a9c50")).WithSubtype(ProductPurchasePrice).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("cd95dc84-a52b-43ae-8be6-7416f660d669")).WithSubtype(ProductPurchasePrice).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("f95d6e43-ceac-4a0d-8372-8cd470f76506")).WithSubtype(ProductPurchasePrice).WithSupertype(userInterfaceable).Build();
			
            // Carrier
            new InheritanceBuilder(domain, new Guid("3288c37c-aedc-4a52-8fad-01f5f8f81c06")).WithSubtype(Carrier).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("922af5a5-6e0c-47a7-a090-d4c09fe57f73")).WithSubtype(Carrier).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("9b1ed377-de1d-4818-9382-c2e5eda99e9f")).WithSubtype(Carrier).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a389814e-829a-4b68-a0d3-1bff85d0d4ed")).WithSubtype(Carrier).WithSupertype(Searchable).Build();
			
            // Resume
            new InheritanceBuilder(domain, new Guid("072cbc11-1abe-46b1-a632-439e878c9ef5")).WithSubtype(Resume).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("5e2c3c61-23f9-44d4-949a-2d2d86bdcd05")).WithSubtype(Resume).WithSupertype(userInterfaceable).Build();
			
            // WebAddress
            new InheritanceBuilder(domain, new Guid("2b6c4d35-d6ee-4fe0-bcb1-bb0ff6286477")).WithSubtype(WebAddress).WithSupertype(ElectronicAddress).Build();
			
            // ProjectRequirement
            new InheritanceBuilder(domain, new Guid("07998871-7603-47f9-bb5f-d287d74177fe")).WithSubtype(ProjectRequirement).WithSupertype(Requirement).Build();
			
            // Deposit
            new InheritanceBuilder(domain, new Guid("f4a1146a-61e6-4709-a1f4-d8de89532b76")).WithSubtype(Deposit).WithSupertype(FinancialAccountTransaction).Build();
			
            // LegalForm
            new InheritanceBuilder(domain, new Guid("1c5e016c-494c-4fc3-b17d-c564dd7574ee")).WithSubtype(LegalForm).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f7019e47-2989-4652-a753-2899eec75e17")).WithSubtype(LegalForm).WithSupertype(Searchable).Build();
			
            // CostOfGoodsSoldMethod
            new InheritanceBuilder(domain, new Guid("8e7c0f06-34c9-4ed3-a752-121a6c7237e8")).WithSubtype(CostOfGoodsSoldMethod).WithSupertype(Enumeration).Build();
			
            // StatementOfWork
            new InheritanceBuilder(domain, new Guid("51404769-ba58-4fc0-9521-1a081c82d7cc")).WithSubtype(StatementOfWork).WithSupertype(Quote).Build();
			
            // FinancialAccountTransaction
            new InheritanceBuilder(domain, new Guid("08f7e832-3949-4971-b117-90ce166cda67")).WithSubtype(FinancialAccountTransaction).WithSupertype(userInterfaceable).Build();
			
            // WorkEffort
            new InheritanceBuilder(domain, new Guid("0e44ef39-53d5-4f7e-8fdf-09153bf46bda")).WithSubtype(WorkEffort).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("18c17d5c-5c88-463c-94fa-604fd19d862b")).WithSubtype(WorkEffort).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("26c2e9ac-9a24-4dc7-992e-ee95f34604e8")).WithSubtype(WorkEffort).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("5b92477c-aa4f-42ff-a952-f789fef97e7b")).WithSubtype(WorkEffort).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("dd2aa747-0fcc-4599-b846-1c71f814482c")).WithSubtype(WorkEffort).WithSupertype(UniquelyIdentifiable).Build();
			
            // SkillLevel
            new InheritanceBuilder(domain, new Guid("29b7511b-3ae7-4153-9e84-df8ef9b5c998")).WithSubtype(SkillLevel).WithSupertype(Enumeration).Build();
			
            // PickListStatus
            new InheritanceBuilder(domain, new Guid("4ad205c3-ea20-4c1c-8d2a-0ae25d348fc3")).WithSubtype(PickListStatus).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("66b74f11-77f4-4655-9735-c94528d99dc2")).WithSubtype(PickListStatus).WithSupertype(AccessControlledObject).Build();
			
            // Product
            new InheritanceBuilder(domain, new Guid("0db318af-44bc-4688-b4d5-60109ce860fc")).WithSubtype(Product).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("5784e220-03db-4b88-8339-50730e612e1d")).WithSubtype(Product).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("cfc61909-2859-4b9f-9f90-a3d64123d4ae")).WithSubtype(Product).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f6cbeb8f-b5fa-4b2f-a769-dd7c436b00f4")).WithSubtype(Product).WithSupertype(Searchable).Build();
			
            // TaxDue
            new InheritanceBuilder(domain, new Guid("4ee2198c-793b-462e-97ad-8baa626ad97f")).WithSubtype(TaxDue).WithSupertype(ExternalAccountingTransaction).Build();
			
            // OneTimeCharge
            new InheritanceBuilder(domain, new Guid("13436044-abce-4a9f-a4da-96d680c257d6")).WithSubtype(OneTimeCharge).WithSupertype(PriceComponent).Build();
			
            // Note
            new InheritanceBuilder(domain, new Guid("93d190cb-6d36-4a06-bc7a-15abd3eed52e")).WithSubtype(Note).WithSupertype(ExternalAccountingTransaction).Build();
			
            // PartBillOfMaterialSubstitute
            new InheritanceBuilder(domain, new Guid("38fc8949-d4ab-48b3-8199-772fa3899adc")).WithSubtype(PartBillOfMaterialSubstitute).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("3cc926c8-ca6e-49d3-ae0f-4151a9f6324e")).WithSubtype(PartBillOfMaterialSubstitute).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("4ce45877-7bd7-4ace-80df-0b73922a100f")).WithSubtype(PartBillOfMaterialSubstitute).WithSupertype(Commentable).Build();
			
            // Receipt
            new InheritanceBuilder(domain, new Guid("d9276c77-0a77-4491-ae45-99c532d8098d")).WithSubtype(Receipt).WithSupertype(Payment).Build();
			
            // RequirementBudgetAllocation
            new InheritanceBuilder(domain, new Guid("b698d6ca-6ccd-4c43-802f-50816b44d554")).WithSubtype(RequirementBudgetAllocation).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("b7fe2d2e-5466-4923-9498-ab8481561ef7")).WithSubtype(RequirementBudgetAllocation).WithSupertype(userInterfaceable).Build();
			
            // OrganisationGlAccount
            new InheritanceBuilder(domain, new Guid("1fafa85c-e1f7-4327-96c2-4b93023c4213")).WithSubtype(OrganisationGlAccount).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("fbd3fc5e-cb62-4fb4-a790-f67ddca728b7")).WithSubtype(OrganisationGlAccount).WithSupertype(Period).Build();
			
            // InternalAccountingTransaction
            new InheritanceBuilder(domain, new Guid("12498c4f-7415-4aea-8363-9682c0fc3eba")).WithSubtype(InternalAccountingTransaction).WithSupertype(AccountingTransaction).Build();
			
            // Maintenance
            new InheritanceBuilder(domain, new Guid("de4b8524-3e4d-4604-a82f-65ad51dc3c22")).WithSubtype(Maintenance).WithSupertype(WorkEffort).Build();
			
            // NonSerializedInventoryItem
            new InheritanceBuilder(domain, new Guid("b76110ea-b2f8-4cce-a6f1-82557d15a401")).WithSubtype(NonSerializedInventoryItem).WithSupertype(InventoryItem).Build();
			
            // CreditLine
            new InheritanceBuilder(domain, new Guid("043eb818-cd26-4410-b998-8ef574d46795")).WithSubtype(CreditLine).WithSupertype(ExternalAccountingTransaction).Build();
			
            // BillOfLading
            new InheritanceBuilder(domain, new Guid("3807bf27-e173-4468-b30f-23e2a3107b67")).WithSubtype(BillOfLading).WithSupertype(Document).Build();
			
            // UnitOfMeasure
            new InheritanceBuilder(domain, new Guid("76567aa7-00da-4fb0-b053-98c089c16710")).WithSubtype(UnitOfMeasure).WithSupertype(IUnitOfMeasure).Build();
            new InheritanceBuilder(domain, new Guid("772b04b1-f86d-4c27-abd9-417c6cddddec")).WithSubtype(UnitOfMeasure).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("8ee3f188-edbe-4b9b-aee7-4c7924db6091")).WithSubtype(UnitOfMeasure).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a11f3ba6-734a-4041-a43e-d15c5365c774")).WithSubtype(UnitOfMeasure).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("ac194b12-d1af-4c17-a062-fd7afdcc5df9")).WithSubtype(UnitOfMeasure).WithSupertype(Enumeration).Build();
			
            // ElectronicAddress
            new InheritanceBuilder(domain, new Guid("1d68e4f8-f901-4366-90e1-3ea84d2900be")).WithSubtype(ElectronicAddress).WithSupertype(ContactMechanism).Build();
			
            // ServiceConfiguration
            new InheritanceBuilder(domain, new Guid("cb045b0d-bd06-4f4b-8470-986bc9a46462")).WithSubtype(ServiceConfiguration).WithSupertype(InventoryItemConfiguration).Build();
			
            // NeededSkill
            new InheritanceBuilder(domain, new Guid("53972844-6d92-4ba6-a88a-ee2ac1e968e0")).WithSubtype(NeededSkill).WithSupertype(userInterfaceable).Build();
			
            // Room
            new InheritanceBuilder(domain, new Guid("02fe5f82-8646-4c48-94de-8367ba04d75c")).WithSubtype(Room).WithSupertype(Facility).Build();
            new InheritanceBuilder(domain, new Guid("909c0504-ac5f-4a10-93ae-03e638b13464")).WithSubtype(Room).WithSupertype(Container).Build();
			
            // Plant
            new InheritanceBuilder(domain, new Guid("572016a0-ee59-4c1a-a9e3-cbffa6fce56f")).WithSubtype(Plant).WithSupertype(Facility).Build();
			
            // SalesInvoice
            new InheritanceBuilder(domain, new Guid("566c8a2f-b4fb-40aa-9a8e-20672c9bda54")).WithSubtype(SalesInvoice).WithSupertype(Invoice).Build();
			
            // InventoryItem
            new InheritanceBuilder(domain, new Guid("23f9d7b5-8855-4001-a3fc-8a595e3fe2d3")).WithSubtype(InventoryItem).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("5e12e676-4176-431c-95f1-c23489b11aca")).WithSubtype(InventoryItem).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("f3013d03-0783-4065-ab02-f4757a987589")).WithSubtype(InventoryItem).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("fe91f009-d9c5-4cc2-9ff7-3963388c946d")).WithSubtype(InventoryItem).WithSupertype(userInterfaceable).Build();
			
            // StandardServiceOrderItem
            new InheritanceBuilder(domain, new Guid("18081cbb-db11-4254-9140-c7fd1242a038")).WithSubtype(StandardServiceOrderItem).WithSupertype(EngagementItem).Build();
			
            // PurchaseInvoiceStatus
            new InheritanceBuilder(domain, new Guid("13dc408c-abef-4a78-b3ea-6f149a2c6ec8")).WithSubtype(PurchaseInvoiceStatus).WithSupertype(userInterfaceable).Build();
			
            // Region
            new InheritanceBuilder(domain, new Guid("1bdcc110-08b4-45f6-8e66-7dc3d584bd92")).WithSubtype(Region).WithSupertype(GeographicBoundaryComposite).Build();
            new InheritanceBuilder(domain, new Guid("d60a6ef6-4bf3-4c31-a2cb-d7dbd0e32ad0")).WithSubtype(Region).WithSupertype(userInterfaceable).Build();
			
            // SalesTerritory
            new InheritanceBuilder(domain, new Guid("73cb9162-d218-453f-8650-74fcae6f99ce")).WithSubtype(SalesTerritory).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("fea89231-5a0c-4c36-940c-3171d35ec081")).WithSubtype(SalesTerritory).WithSupertype(GeographicBoundaryComposite).Build();
			
            // TimeEntry
            new InheritanceBuilder(domain, new Guid("635d81ad-08a8-40a0-b3e6-f037c45e1d02")).WithSubtype(TimeEntry).WithSupertype(ServiceEntry).Build();
			
            // DepreciationMethod
            new InheritanceBuilder(domain, new Guid("37a56acc-5d5d-4e41-b084-3725aa86092d")).WithSubtype(DepreciationMethod).WithSupertype(userInterfaceable).Build();
			
            // AssetAssignmentStatus
            new InheritanceBuilder(domain, new Guid("785d7bfe-9920-45be-8556-319b3ee901c4")).WithSubtype(AssetAssignmentStatus).WithSupertype(Enumeration).Build();
			
            // StoreRevenueHistory
            new InheritanceBuilder(domain, new Guid("1589840b-6f49-4f19-af9e-d714f7dc5f85")).WithSubtype(StoreRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // PersonTraining
            new InheritanceBuilder(domain, new Guid("4ed08a94-bdfe-48fe-9031-d73b4de5d0d3")).WithSubtype(PersonTraining).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("5814445e-f022-4321-8dcd-5ed4c88579ff")).WithSubtype(PersonTraining).WithSupertype(userInterfaceable).Build();
			
            // DeductionType
            new InheritanceBuilder(domain, new Guid("724dc827-ada8-4971-aa14-94667a03c3d0")).WithSubtype(DeductionType).WithSupertype(Enumeration).Build();
			
            // DeliverableOrderItem
            new InheritanceBuilder(domain, new Guid("bad734d1-49cb-4680-ad9f-59895582c957")).WithSubtype(DeliverableOrderItem).WithSupertype(EngagementItem).Build();
			
            // PackagingSlip
            new InheritanceBuilder(domain, new Guid("2ab93dc4-2bcf-47d9-93d7-aa8cea0f13c3")).WithSubtype(PackagingSlip).WithSupertype(Document).Build();
			
            // CustomerReturnObjectState
            new InheritanceBuilder(domain, new Guid("83520701-3237-4ac2-acf3-1e1296d2ddc0")).WithSubtype(CustomerReturnObjectState).WithSupertype(ObjectState).Build();
			
            // OrganisationGlAccountBalance
            new InheritanceBuilder(domain, new Guid("5353a180-6eb3-4ff4-8471-f104c944f451")).WithSubtype(OrganisationGlAccountBalance).WithSupertype(userInterfaceable).Build();
			
            // InternalOrganisationRevenueHistory
            new InheritanceBuilder(domain, new Guid("d8e5c42c-272c-4905-a3a0-3b8c2a200d28")).WithSubtype(InternalOrganisationRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // ManufacturingBom
            new InheritanceBuilder(domain, new Guid("c1e78388-bb08-4e3a-b77b-3af51833773b")).WithSubtype(ManufacturingBom).WithSupertype(PartBillOfMaterial).Build();
			
            // Deliverable
            new InheritanceBuilder(domain, new Guid("3d4e3dfa-2c22-4dd2-bbb2-2c44f1f62309")).WithSubtype(Deliverable).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("ef45a2b5-88a6-4f89-9c86-6788a2d3b71f")).WithSubtype(Deliverable).WithSupertype(userInterfaceable).Build();
			
            // EmploymentApplication
            new InheritanceBuilder(domain, new Guid("024badaf-157a-4b66-bc95-1e3e855e4e7b")).WithSubtype(EmploymentApplication).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("71f4f6dc-1497-4a86-905a-6d22b293d5fe")).WithSubtype(EmploymentApplication).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("8d5c645a-7e74-45d8-b54a-a825abc32304")).WithSubtype(EmploymentApplication).WithSupertype(Searchable).Build();
			
            // VatRegime
            new InheritanceBuilder(domain, new Guid("b4e10fae-1ce2-4c5e-a958-6df1b5c3f689")).WithSubtype(VatRegime).WithSupertype(Enumeration).Build();
			
            // PositionFulfillment
            new InheritanceBuilder(domain, new Guid("e746059d-ebfb-4589-9d33-36e0e549540e")).WithSubtype(PositionFulfillment).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("ed7a54ee-6160-41ef-a3c3-abc8e37643d2")).WithSubtype(PositionFulfillment).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("faad4a74-e9e7-45d0-aeb5-b129d59d5cd5")).WithSubtype(PositionFulfillment).WithSupertype(userInterfaceable).Build();
			
            // Employment
            new InheritanceBuilder(domain, new Guid("a4377eca-6738-41a6-9519-295c0a7e15fe")).WithSubtype(Employment).WithSupertype(PartyRelationship).Build();
			
            // AccountingPeriod
            new InheritanceBuilder(domain, new Guid("423ad94c-1a54-40ed-8fa3-62b233521839")).WithSubtype(AccountingPeriod).WithSupertype(Budget).Build();
            new InheritanceBuilder(domain, new Guid("470cda36-5554-4068-a6c9-b7816e57ebba")).WithSubtype(AccountingPeriod).WithSupertype(userInterfaceable).Build();
			
            // EngagementRate
            new InheritanceBuilder(domain, new Guid("03c5c596-5ad0-4299-a35b-bf9e852f7275")).WithSubtype(EngagementRate).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("139a1a41-615b-4739-94f4-34806bb292fc")).WithSubtype(EngagementRate).WithSupertype(userInterfaceable).Build();
			
            // ExternalAccountingTransaction
            new InheritanceBuilder(domain, new Guid("bee62d3b-c0c6-493b-a292-13723ac230f1")).WithSubtype(ExternalAccountingTransaction).WithSupertype(AccountingTransaction).Build();
			
            // TelecommunicationsNumber
            new InheritanceBuilder(domain, new Guid("64e085cc-73e5-4244-a572-7ae0a1c6ce48")).WithSubtype(TelecommunicationsNumber).WithSupertype(ContactMechanism).Build();
			
            // SalesRepRelationship
            new InheritanceBuilder(domain, new Guid("43e30091-f48f-4284-ab95-c178eea63715")).WithSubtype(SalesRepRelationship).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("4767ad27-a3b5-47b7-8b16-ba2d58fbd4b2")).WithSubtype(SalesRepRelationship).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("58c29bc1-295b-4732-974e-244b41e2538a")).WithSubtype(SalesRepRelationship).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("7282f0e7-5cc7-4e16-bed0-1a3e013bbece")).WithSubtype(SalesRepRelationship).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("ad14fe05-a2c2-4926-815e-baa620bfeed5")).WithSubtype(SalesRepRelationship).WithSupertype(PartyRelationship).Build();
			
            // PurchaseInvoiceObjectState
            new InheritanceBuilder(domain, new Guid("84e6747c-df33-40ed-aefd-9cbfa2c6627e")).WithSubtype(PurchaseInvoiceObjectState).WithSupertype(ObjectState).Build();
			
            // ProductCategoryRevenue
            new InheritanceBuilder(domain, new Guid("ae90b408-9340-474f-bdcd-d8cffda524d1")).WithSubtype(ProductCategoryRevenue).WithSupertype(userInterfaceable).Build();
			
            // ChartOfAccounts
            new InheritanceBuilder(domain, new Guid("23bd6b24-a101-4989-83e0-ee6f5fa70f62")).WithSubtype(ChartOfAccounts).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("61502cd5-1b0a-4a99-985b-47307c130486")).WithSubtype(ChartOfAccounts).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("6365b3fc-42e7-4023-8593-d86c1a4948d2")).WithSubtype(ChartOfAccounts).WithSupertype(UniquelyIdentifiable).Build();
			
            // PartyRevenue
            new InheritanceBuilder(domain, new Guid("08a36b45-c64b-4f67-a059-79da252522b8")).WithSubtype(PartyRevenue).WithSupertype(userInterfaceable).Build();
			
            // MarketingMaterial
            new InheritanceBuilder(domain, new Guid("f0579a98-3e36-4dcf-9f06-a1c0ccf46cb0")).WithSubtype(MarketingMaterial).WithSupertype(Document).Build();
			
            // PurchaseInvoiceItemStatus
            new InheritanceBuilder(domain, new Guid("988d185d-95f1-4bb3-8c9e-eff2e2828eb8")).WithSubtype(PurchaseInvoiceItemStatus).WithSupertype(userInterfaceable).Build();
			
            // InvoiceVatRateItem
            new InheritanceBuilder(domain, new Guid("5d4a5fb9-3493-4c75-bfef-c9125d079c01")).WithSubtype(InvoiceVatRateItem).WithSupertype(userInterfaceable).Build();
			
            // CaseObjectState
            new InheritanceBuilder(domain, new Guid("44765382-5b19-448a-9593-5d879041d014")).WithSubtype(CaseObjectState).WithSupertype(ObjectState).Build();
			
            // SalaryStep
            new InheritanceBuilder(domain, new Guid("1ae3eada-fdec-4e15-aee5-360aa5d05e45")).WithSubtype(SalaryStep).WithSupertype(userInterfaceable).Build();
			
            // DropShipmentStatus
            new InheritanceBuilder(domain, new Guid("b0d806e8-eb0d-4337-bceb-ab715b18d9ca")).WithSubtype(DropShipmentStatus).WithSupertype(userInterfaceable).Build();
			
            // PaymentApplication
            new InheritanceBuilder(domain, new Guid("3b7e7e9c-0c88-42de-8451-4a25ffbbcb5c")).WithSubtype(PaymentApplication).WithSupertype(userInterfaceable).Build();
			
            // NonSerializedInventoryItemStatus
            new InheritanceBuilder(domain, new Guid("ea12ac44-c492-4b3a-8367-3ffa40d506c5")).WithSubtype(NonSerializedInventoryItemStatus).WithSupertype(userInterfaceable).Build();
			
            // SurchargeAdjustment
            new InheritanceBuilder(domain, new Guid("f7e77ecc-e23e-4c1a-a8ec-9b4e3ef28bf0")).WithSubtype(SurchargeAdjustment).WithSupertype(OrderAdjustment).Build();
			
            // Depreciation
            new InheritanceBuilder(domain, new Guid("0e60bbcb-3d9d-4c78-84c6-d4ec8c3c79b9")).WithSubtype(Depreciation).WithSupertype(InternalAccountingTransaction).Build();
			
            // Territory
            new InheritanceBuilder(domain, new Guid("0047f17b-2c89-4b65-924a-15afb22d622d")).WithSubtype(Territory).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("3531c003-2489-4f8f-b4ec-a18b87bcd2d9")).WithSubtype(Territory).WithSupertype(CityBound).Build();
            new InheritanceBuilder(domain, new Guid("3bbb85de-5102-45b5-bec1-82379bd216db")).WithSubtype(Territory).WithSupertype(GeographicBoundary).Build();
            new InheritanceBuilder(domain, new Guid("a6b58e29-5814-479a-9e73-fc24ce16104c")).WithSubtype(Territory).WithSupertype(CountryBound).Build();
			
            // SalesOrder
            new InheritanceBuilder(domain, new Guid("9e4e4bf4-8671-4ac9-ab4f-fce34423f273")).WithSubtype(SalesOrder).WithSupertype(Order).Build();
			
            // Warehouse
            new InheritanceBuilder(domain, new Guid("23110a09-0bff-4b96-a3fd-d38817a73364")).WithSubtype(Warehouse).WithSupertype(Facility).Build();
			
            // AgreementPricingProgram
            new InheritanceBuilder(domain, new Guid("ae18b076-c0c8-4f03-8666-30981e2b5919")).WithSubtype(AgreementPricingProgram).WithSupertype(AgreementItem).Build();
			
            // AgreementTerm
            new InheritanceBuilder(domain, new Guid("a05aa047-e2f2-47dd-844f-4ac7ece38bf6")).WithSubtype(AgreementTerm).WithSupertype(userInterfaceable).Build();
			
            // SalesRepRevenue
            new InheritanceBuilder(domain, new Guid("67dcf947-3f5b-4377-90f1-2035e4439def")).WithSubtype(SalesRepRevenue).WithSupertype(userInterfaceable).Build();
			
            // EmploymentApplicationSource
            new InheritanceBuilder(domain, new Guid("27259086-0943-4308-8d63-e6a971d27e97")).WithSubtype(EmploymentApplicationSource).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("c4191882-6741-40d6-95ee-4c0ddbe8174f")).WithSubtype(EmploymentApplicationSource).WithSupertype(Searchable).Build();
			
            // Engagement
            new InheritanceBuilder(domain, new Guid("0549ad5b-3986-403e-9841-bd78f9283bfe")).WithSubtype(Engagement).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("1015b6c7-7cff-4d9f-a2ae-e3ae779c0c42")).WithSubtype(Engagement).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("ad28e179-7d19-4ef9-a20a-a088b45238ae")).WithSubtype(Engagement).WithSupertype(SearchResult).Build();
			
            // Part
            new InheritanceBuilder(domain, new Guid("617faf1d-b04e-4d7e-bca5-9f5a1ae96709")).WithSubtype(Part).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("bc0c10a0-e772-4efb-8cf5-409162f8a248")).WithSubtype(Part).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("cfac893e-e3e2-42e0-93a4-a02ed18b0421")).WithSubtype(Part).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("f8d158ba-49c6-4d89-a52d-2d00c4426390")).WithSubtype(Part).WithSupertype(SearchResult).Build();
			
            // InventoryItemKind
            new InheritanceBuilder(domain, new Guid("34be448f-3c38-4e96-8ae0-2603a81fd50e")).WithSubtype(InventoryItemKind).WithSupertype(Enumeration).Build();
			
            // CustomEngagementItem
            new InheritanceBuilder(domain, new Guid("caea57cd-123d-41e8-a539-83c4b5ca13f1")).WithSubtype(CustomEngagementItem).WithSupertype(EngagementItem).Build();
			
            // AccountingTransaction
            new InheritanceBuilder(domain, new Guid("221c3a50-89cb-4191-858e-ba0ffea3bfcf")).WithSubtype(AccountingTransaction).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("415ab8a7-c207-4432-8f4d-5577da58a8f1")).WithSubtype(AccountingTransaction).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("feb6250f-0bf5-4b34-ad3a-c97f26e1b1fa")).WithSubtype(AccountingTransaction).WithSupertype(Searchable).Build();
			
            // SalesRepPartyRevenue
            new InheritanceBuilder(domain, new Guid("2e608fe9-c064-4956-af3d-17426c94d621")).WithSubtype(SalesRepPartyRevenue).WithSupertype(userInterfaceable).Build();
			
            // JournalType
            new InheritanceBuilder(domain, new Guid("fc8f1271-b38e-4e9c-8abf-d94ae7313fe2")).WithSubtype(JournalType).WithSupertype(Enumeration).Build();
			
            // PurchaseOrderItemStatus
            new InheritanceBuilder(domain, new Guid("85b18023-e8ee-4d27-8863-8f73d7c7f6c1")).WithSubtype(PurchaseOrderItemStatus).WithSupertype(userInterfaceable).Build();
			
            // Addendum
            new InheritanceBuilder(domain, new Guid("ef36ea64-ca49-4ee9-b664-d131ba5e9693")).WithSubtype(Addendum).WithSupertype(userInterfaceable).Build();
			
            // Floor
            new InheritanceBuilder(domain, new Guid("0827a3a1-311d-4e59-a6a4-952637b31aca")).WithSubtype(Floor).WithSupertype(Facility).Build();
			
            // WorkEffortType
            new InheritanceBuilder(domain, new Guid("1037abcc-667c-4b37-bfc9-f7a76a7767c9")).WithSubtype(WorkEffortType).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("fd19ae88-605f-4959-8b1e-069f3df5fdaf")).WithSubtype(WorkEffortType).WithSupertype(userInterfaceable).Build();
			
            // SalesInvoiceStatus
            new InheritanceBuilder(domain, new Guid("163a2c51-d63e-426c-80d0-040c69c66f72")).WithSubtype(SalesInvoiceStatus).WithSupertype(userInterfaceable).Build();
			
            // SalesAgreement
            new InheritanceBuilder(domain, new Guid("997c9af3-8cad-481f-9dc3-3ffcb80e8d54")).WithSubtype(SalesAgreement).WithSupertype(Agreement).Build();
			
            // PurchaseInvoice
            new InheritanceBuilder(domain, new Guid("388d6f37-8e5e-49bc-86b3-440a9f9157f1")).WithSubtype(PurchaseInvoice).WithSupertype(Invoice).Build();
			
            // CustomerReturn
            new InheritanceBuilder(domain, new Guid("f6da645b-e9cb-4b14-bcf5-a97549c3cdd4")).WithSubtype(CustomerReturn).WithSupertype(Shipment).Build();
			
            // Order
            new InheritanceBuilder(domain, new Guid("462f8f2a-55d3-470b-8cfc-9ede18f39d51")).WithSubtype(Order).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a654012f-fe99-49ef-a696-447bc18d8c84")).WithSubtype(Order).WithSupertype(Printable).Build();
            new InheritanceBuilder(domain, new Guid("c0ed4427-f470-4537-8062-baacf8b08b44")).WithSubtype(Order).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("d651ba5c-67f0-4259-9683-e83ca9e9d5a1")).WithSubtype(Order).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("e674fd36-8217-4afc-adee-42924273de65")).WithSubtype(Order).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("e98ce655-ccad-497e-9834-1597fff7b677")).WithSubtype(Order).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("ed349368-ed42-4834-ba0f-e3a8030d7017")).WithSubtype(Order).WithSupertype(Localised).Build();
            new InheritanceBuilder(domain, new Guid("f20b25cc-a739-4138-b446-7ec9d28d6ced")).WithSubtype(Order).WithSupertype(SearchResult).Build();

            // PartyPackageRevenueHistory
            new InheritanceBuilder(domain, new Guid("019ee7a1-a84b-434c-81ad-82ba0424dbeb")).WithSubtype(PartyPackageRevenueHistory).WithSupertype(userInterfaceable).Build();

            // OrderKind
            new InheritanceBuilder(domain, new Guid("4506c20e-8cee-4318-968d-7ee31113624e")).WithSubtype(OrderKind).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("7b0e4178-32c1-4425-a459-f42fa4b1be1c")).WithSubtype(OrderKind).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("b974d268-a86f-46f4-bc14-c4996d9e5756")).WithSubtype(OrderKind).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("bdfdda43-b936-4481-86ff-204e76a02d1a")).WithSubtype(OrderKind).WithSupertype(AccessControlledObject).Build();
			
            // Amortization
            new InheritanceBuilder(domain, new Guid("5778f299-1689-40e6-a211-7df71b95d1e5")).WithSubtype(Amortization).WithSupertype(InternalAccountingTransaction).Build();
			
            // PickListItem
            new InheritanceBuilder(domain, new Guid("7c05dd7b-261a-407d-88fd-ae9349aa0bee")).WithSubtype(PickListItem).WithSupertype(userInterfaceable).Build();
			
            // SalesOrderItem
            new InheritanceBuilder(domain, new Guid("2e2eae5f-e584-41a9-8ca4-08d689987ff0")).WithSubtype(SalesOrderItem).WithSupertype(OrderItem).Build();
			
            // SalesInvoiceType
            new InheritanceBuilder(domain, new Guid("82fa72cf-a13d-4b53-9886-2880eb531f7d")).WithSubtype(SalesInvoiceType).WithSupertype(Enumeration).Build();
			
            // WorkEffortGoodStandard
            new InheritanceBuilder(domain, new Guid("4b60b48e-47a5-4617-bcea-cfab453a96ec")).WithSubtype(WorkEffortGoodStandard).WithSupertype(userInterfaceable).Build();
			
            // Passport
            new InheritanceBuilder(domain, new Guid("33f3efd1-8cc5-4ada-a0ae-7c0e1aef7303")).WithSubtype(Passport).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d277d05b-20a1-4386-b523-17706afada38")).WithSubtype(Passport).WithSupertype(Searchable).Build();
			
            // AmountDue
            new InheritanceBuilder(domain, new Guid("2c8c7542-e6c7-4bc3-9eab-4f746bf10f2b")).WithSubtype(AmountDue).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("5a704dc8-99d6-490a-abde-5f624c6b4895")).WithSubtype(AmountDue).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("61be5272-7ae5-4b3f-94c0-a32ce87334f0")).WithSubtype(AmountDue).WithSupertype(userInterfaceable).Build();
			
            // WorkEffortTypeKind
            new InheritanceBuilder(domain, new Guid("382118e6-119f-487f-8683-171b5e06f4a6")).WithSubtype(WorkEffortTypeKind).WithSupertype(Enumeration).Build();
			
            // OrderTerm
            new InheritanceBuilder(domain, new Guid("85848388-ae53-4603-8d55-606d78d71de9")).WithSubtype(OrderTerm).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("ad812a45-5be9-48d0-804a-d5d4e5ba2caf")).WithSubtype(OrderTerm).WithSupertype(Searchable).Build();
			
            // CreditCardCompany
            new InheritanceBuilder(domain, new Guid("b29538ad-de2d-4afa-b4a4-bedc1dbb676c")).WithSubtype(CreditCardCompany).WithSupertype(userInterfaceable).Build();
			
            // RequestForQuote
            new InheritanceBuilder(domain, new Guid("043bf681-9a91-41be-8a86-bcd4166b29d7")).WithSubtype(RequestForQuote).WithSupertype(Request).Build();
			
            // PurchaseShipmentStatus
            new InheritanceBuilder(domain, new Guid("df11f9cc-86c9-44cc-8f47-67d034e84f5b")).WithSubtype(PurchaseShipmentStatus).WithSupertype(userInterfaceable).Build();
			
            // Cash
            new InheritanceBuilder(domain, new Guid("ce34402d-e1c3-4538-8250-0d7724695be5")).WithSubtype(Cash).WithSupertype(PaymentMethod).Build();
			
            // PerformanceReview
            new InheritanceBuilder(domain, new Guid("0e790aaa-e9ec-4f7c-8477-1c5609ab9469")).WithSubtype(PerformanceReview).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("3181228d-f6de-464f-bd5b-e02b19d00b2b")).WithSubtype(PerformanceReview).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("cd9944d9-d99e-4f9d-acb4-3bac1be5ca1e")).WithSubtype(PerformanceReview).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("e56df588-0176-419b-a0e1-3cf1e907c97f")).WithSubtype(PerformanceReview).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("ec209c41-6b06-4ccc-b512-22971dfe6058")).WithSubtype(PerformanceReview).WithSupertype(Period).Build();
			
            // DropShipmentObjectState
            new InheritanceBuilder(domain, new Guid("cd55b595-bab3-4b84-9d17-4413a27ae4ff")).WithSubtype(DropShipmentObjectState).WithSupertype(ObjectState).Build();
			
            // InvestmentAccount
            new InheritanceBuilder(domain, new Guid("11f76af6-b421-4434-a53e-9a2a94f3eeec")).WithSubtype(InvestmentAccount).WithSupertype(FinancialAccount).Build();
			
            // AgreementItem
            new InheritanceBuilder(domain, new Guid("cc03fa17-9da5-4346-a98f-f9c540a848d9")).WithSubtype(AgreementItem).WithSupertype(userInterfaceable).Build();
			
            // Colour
            new InheritanceBuilder(domain, new Guid("bb9f3995-8b38-4dd9-a9ed-454c7488b05e")).WithSubtype(Colour).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("e3e4f2dd-856a-43bb-8831-deae68b71a05")).WithSubtype(Colour).WithSupertype(ProductFeature).Build();
			
            // PackageRevenue
            new InheritanceBuilder(domain, new Guid("62f73400-4eb7-4722-b78b-764167188acd")).WithSubtype(PackageRevenue).WithSupertype(userInterfaceable).Build();
			
            // SalesOrderObjectState
            new InheritanceBuilder(domain, new Guid("ca8146f3-995f-4163-9227-d1ffdb04c2c6")).WithSubtype(SalesOrderObjectState).WithSupertype(ObjectState).Build();
			
            // Benefit
            new InheritanceBuilder(domain, new Guid("a5d5ed41-ddf7-4fe9-824b-db1554b2870c")).WithSubtype(Benefit).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("e4449b83-2a10-436c-a194-24ef36df296b")).WithSubtype(Benefit).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f1db2c27-0840-49fa-a53c-4550d47092e7")).WithSubtype(Benefit).WithSupertype(Searchable).Build();
			
            // EngineeringDocument
            new InheritanceBuilder(domain, new Guid("54560811-1421-4c56-a265-e5e4253224d0")).WithSubtype(EngineeringDocument).WithSupertype(Document).Build();
			
            // VatReturnBox
            new InheritanceBuilder(domain, new Guid("4be9abc8-8aa7-4149-ae32-13646dc6d912")).WithSubtype(VatReturnBox).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("facf607b-eafd-4645-b9b3-ed77bb654c0f")).WithSubtype(VatReturnBox).WithSupertype(AccessControlledObject).Build();
			
            // CommunicationEventPurpose
            new InheritanceBuilder(domain, new Guid("f7de6f7d-1771-4eec-ac7b-49096d57512a")).WithSubtype(CommunicationEventPurpose).WithSupertype(Enumeration).Build();
			
            // ShipmentRouteSegment
            new InheritanceBuilder(domain, new Guid("b5ef9b22-d6ac-444d-ba56-d6a59aceae3d")).WithSubtype(ShipmentRouteSegment).WithSupertype(userInterfaceable).Build();
			
            // VarianceReason
            new InheritanceBuilder(domain, new Guid("d47dfece-bfcf-4ae7-a3d1-6d480609c069")).WithSubtype(VarianceReason).WithSupertype(Enumeration).Build();
			
            // Phase
            new InheritanceBuilder(domain, new Guid("86aa222a-c8b3-4b2f-aff5-8dceaafcf2b7")).WithSubtype(Phase).WithSupertype(WorkEffort).Build();
			
            // WorkEffortStatus
            new InheritanceBuilder(domain, new Guid("f1ab3d17-61cc-45eb-9590-d3847fc2c897")).WithSubtype(WorkEffortStatus).WithSupertype(userInterfaceable).Build();
			
            // Salutation
            new InheritanceBuilder(domain, new Guid("4f90717f-1cc2-4759-b9f5-4d4e9dc08e0a")).WithSubtype(Salutation).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("5bdf5e10-db96-454c-aafa-6084c6caef67")).WithSubtype(Salutation).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("6c124270-429f-41d6-b7f1-047c71c78c23")).WithSubtype(Salutation).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("7fe4104b-7d2d-4cca-9767-6e3683d94ca1")).WithSubtype(Salutation).WithSupertype(AccessControlledObject).Build();
			
            // PurchaseOrderStatus
            new InheritanceBuilder(domain, new Guid("52353481-6ced-4a85-9016-e8673449cea9")).WithSubtype(PurchaseOrderStatus).WithSupertype(userInterfaceable).Build();
			
            // PayrollPreference
            new InheritanceBuilder(domain, new Guid("4f7ee49a-b975-423f-8ec3-72d85857fdb4")).WithSubtype(PayrollPreference).WithSupertype(userInterfaceable).Build();
			
            // CustomerShipment
            new InheritanceBuilder(domain, new Guid("b2c29382-1d90-4029-837d-505e2307022f")).WithSubtype(CustomerShipment).WithSupertype(Shipment).Build();
			
            // InternalOrganisationRevenue
            new InheritanceBuilder(domain, new Guid("7584b26b-c8c6-43fa-86f3-bcbcccdb9788")).WithSubtype(InternalOrganisationRevenue).WithSupertype(userInterfaceable).Build();
			
            // Package
            new InheritanceBuilder(domain, new Guid("431bf9a6-ba7d-4fff-8f4a-628dbfef42ae")).WithSubtype(Package).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("50447208-3749-49fd-9c2a-e312118b3183")).WithSubtype(Package).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("df11f06f-6b66-4043-a7db-b15d9b301328")).WithSubtype(Package).WithSupertype(Searchable).Build();
			
            // GeoLocatable
            new InheritanceBuilder(domain, new Guid("2125982a-3d4e-44ce-9df8-b044a837c307")).WithSubtype(GeoLocatable).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("876b06fc-7ec3-486a-bbfb-1dfd8b6041c8")).WithSubtype(GeoLocatable).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("8fbc30ea-489c-49ef-ab3d-cef2f2669980")).WithSubtype(GeoLocatable).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("a1898933-cfa2-47cc-96a4-5f57a80272e3")).WithSubtype(GeoLocatable).WithSupertype(UniquelyIdentifiable).Build();
			
            // HazardousMaterialsDocument
            new InheritanceBuilder(domain, new Guid("738fa3de-875c-4a91-95de-91fddb177ef9")).WithSubtype(HazardousMaterialsDocument).WithSupertype(Document).Build();
			
            // EmailCommunication
            new InheritanceBuilder(domain, new Guid("c48e5de8-1308-4a63-8292-7d0224a5fbc9")).WithSubtype(EmailCommunication).WithSupertype(CommunicationEvent).Build();
			
            // CreditCard
            new InheritanceBuilder(domain, new Guid("27190086-6e43-4272-89e5-e7cdc1f67032")).WithSubtype(CreditCard).WithSupertype(FinancialAccount).Build();
            new InheritanceBuilder(domain, new Guid("40261335-f728-4c09-996f-c8aa143e4341")).WithSubtype(CreditCard).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("9b874ac3-fe78-42ce-ade7-f5d43d448bcb")).WithSubtype(CreditCard).WithSupertype(Searchable).Build();
			
            // OrganisationContactRelationship
            new InheritanceBuilder(domain, new Guid("a449750e-c67c-41dd-a672-45acde375fa0")).WithSubtype(OrganisationContactRelationship).WithSupertype(PartyRelationship).Build();
			
            // OrganisationContactKind
            new InheritanceBuilder(domain, new Guid("28c69e71-eb7a-4881-8ccd-e2577e0bb4b5")).WithSubtype(OrganisationContactKind).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a1e88d35-3197-4405-a511-4750d86e005e")).WithSubtype(OrganisationContactKind).WithSupertype(UniquelyIdentifiable).Build();
			
            // CustomerReturnStatus
            new InheritanceBuilder(domain, new Guid("4ff79ad9-63e4-4b01-89b7-509c5e719ed2")).WithSubtype(CustomerReturnStatus).WithSupertype(userInterfaceable).Build();
			
            // PerformanceReviewItem
            new InheritanceBuilder(domain, new Guid("649fd932-74c0-4d8a-b7c9-bd1db1f4208a")).WithSubtype(PerformanceReviewItem).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("65353de8-59e6-4dd7-a31f-a71092a7863b")).WithSubtype(PerformanceReviewItem).WithSupertype(userInterfaceable).Build();
			
            // UtilizationCharge
            new InheritanceBuilder(domain, new Guid("5cbdfe72-5c53-4679-b807-e0a8cce71de9")).WithSubtype(UtilizationCharge).WithSupertype(PriceComponent).Build();
			
            // PartyPackageRevenue
            new InheritanceBuilder(domain, new Guid("13c3a074-8ba2-4e6e-bde9-250cccf078dd")).WithSubtype(PartyPackageRevenue).WithSupertype(userInterfaceable).Build();
			
            // PartyRelationshipStatus
            new InheritanceBuilder(domain, new Guid("b31dc56f-9e56-49d1-8f55-e1d4e1f4cdc6")).WithSubtype(PartyRelationshipStatus).WithSupertype(Enumeration).Build();
			
            // ServiceTerritory
            new InheritanceBuilder(domain, new Guid("bf1c440a-3a04-46d4-80d1-cc8d2fcf2392")).WithSubtype(ServiceTerritory).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("ed6edfeb-881c-478d-8f03-242ea78d1213")).WithSubtype(ServiceTerritory).WithSupertype(GeographicBoundaryComposite).Build();
			
            // DeliverableBasedService
            new InheritanceBuilder(domain, new Guid("bb7a49f0-5781-4f8d-a94c-06a6e641f46a")).WithSubtype(DeliverableBasedService).WithSupertype(Service).Build();
			
            // ProductModel
            new InheritanceBuilder(domain, new Guid("f9a69648-a45d-446b-8af0-df8ad0caf8f2")).WithSubtype(ProductModel).WithSupertype(Document).Build();
			
            // Shelf
            new InheritanceBuilder(domain, new Guid("088872d7-e230-4372-ac3e-c5ebbd99af70")).WithSubtype(Shelf).WithSupertype(Container).Build();
			
            // RawMaterial
            new InheritanceBuilder(domain, new Guid("5ea66f88-bd74-4e76-a6f7-52e26d3f5060")).WithSubtype(RawMaterial).WithSupertype(Part).Build();
			
            // EstimatedOtherCost
            new InheritanceBuilder(domain, new Guid("11d3c48a-9a4a-4057-b0cc-a3846c40028c")).WithSubtype(EstimatedOtherCost).WithSupertype(EstimatedProductCost).Build();
			
            // BudgetRevision
            new InheritanceBuilder(domain, new Guid("4e00b3a3-2ebd-45e1-be04-69beb8a67303")).WithSubtype(BudgetRevision).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("841d7ec2-3e82-4537-9e0f-d59883b5dffc")).WithSubtype(BudgetRevision).WithSupertype(userInterfaceable).Build();
			
            // WorkEffortFixedAssetStandard
            new InheritanceBuilder(domain, new Guid("aa9eaf74-292a-4749-9dae-d0b10b1c6970")).WithSubtype(WorkEffortFixedAssetStandard).WithSupertype(userInterfaceable).Build();
			
            // Shipment
            new InheritanceBuilder(domain, new Guid("0accd0a0-e992-4d27-8062-69ab1b86c0e4")).WithSubtype(Shipment).WithSupertype(Printable).Build();
            new InheritanceBuilder(domain, new Guid("16fe212f-3a42-4652-a030-8eeefd865fea")).WithSubtype(Shipment).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("19f92b4a-79fe-4ee7-91f6-a6dc324a469f")).WithSubtype(Shipment).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("39fd146b-fc2b-4b8c-b10c-ecefa0734f1b")).WithSubtype(Shipment).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("57ce90bb-b166-4c44-8efe-9d2e8df2151c")).WithSubtype(Shipment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("898a18a2-39b0-4776-9a04-bb0f15cfacc4")).WithSubtype(Shipment).WithSupertype(SearchResult).Build();
			
            // PostalCode
            new InheritanceBuilder(domain, new Guid("a2554a5d-d5a9-44ed-bcfc-11cd3988a562")).WithSubtype(PostalCode).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d40416e6-70a4-480c-96f3-9d7aa4f03d0e")).WithSubtype(PostalCode).WithSupertype(GeographicBoundary).Build();
			
            // NonSerializedInventoryItemObjectState
            new InheritanceBuilder(domain, new Guid("338dad9b-67f8-4d3d-ad23-e2c8eb16825c")).WithSubtype(NonSerializedInventoryItemObjectState).WithSupertype(ObjectState).Build();
			
            // ProfessionalAssignment
            new InheritanceBuilder(domain, new Guid("3d3e9d8d-2274-404e-9773-76f32100fbe1")).WithSubtype(ProfessionalAssignment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("b4b9a7b6-a99a-4f0e-a67d-3e58070c12a9")).WithSubtype(ProfessionalAssignment).WithSupertype(Period).Build();
			
            // Container
            new InheritanceBuilder(domain, new Guid("a56f8892-2891-4441-9cb7-2aa97707853a")).WithSubtype(Container).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("b43232f1-9ce4-4dc9-949b-cccf1f3be675")).WithSubtype(Container).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("cd81edd7-f12f-4d61-b17b-8e4484c0b1c2")).WithSubtype(Container).WithSupertype(userInterfaceable).Build();
			
            // Payment
            new InheritanceBuilder(domain, new Guid("283d020a-0d54-4764-8f5e-8364200618ee")).WithSubtype(Payment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("3428eac9-c5fc-4526-b182-420dd7be6d42")).WithSubtype(Payment).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("46df5954-42b4-421a-8a15-dd33dee248f9")).WithSubtype(Payment).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("4e7c1f4e-8b99-4a88-86cd-304b25a5ba82")).WithSubtype(Payment).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("b826c23e-3a10-41bc-895b-f517ca4ac373")).WithSubtype(Payment).WithSupertype(UniquelyIdentifiable).Build();
			
            // TransferObjectState
            new InheritanceBuilder(domain, new Guid("4960682f-d940-4da2-8923-f6d3be0e1ecf")).WithSubtype(TransferObjectState).WithSupertype(ObjectState).Build();
			
            // PackageRevenueHistory
            new InheritanceBuilder(domain, new Guid("b59dfeee-6cea-4afa-8af5-1dab709647b9")).WithSubtype(PackageRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // JournalEntryDetail
            new InheritanceBuilder(domain, new Guid("6c4d04e0-6a2f-422b-b192-767cc6b384df")).WithSubtype(JournalEntryDetail).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("fc04d791-3039-48e5-a0b2-dfe38246d51e")).WithSubtype(JournalEntryDetail).WithSupertype(userInterfaceable).Build();
			
            // TestingRequirement
            new InheritanceBuilder(domain, new Guid("f552ae41-9920-43b7-8eca-b4c4341933e1")).WithSubtype(TestingRequirement).WithSupertype(PartSpecification).Build();
			
            // Case
            new InheritanceBuilder(domain, new Guid("20325650-daf0-40a1-978d-e1ca1f4cf52a")).WithSubtype(Case).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("7864bb96-de3f-414a-9974-76806e59b4b1")).WithSubtype(Case).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("7fec8535-43d5-462a-b964-a4bd0b9cc9b6")).WithSubtype(Case).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("ae8ea6fb-6aa5-4c3f-ba77-e6e5d4e8575f")).WithSubtype(Case).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("b576eeac-71b3-4e49-8e6f-f59977d2127c")).WithSubtype(Case).WithSupertype(SearchResult).Build();
			
            // Capitalization
            new InheritanceBuilder(domain, new Guid("37790eef-b705-49da-943a-8784ecf5d0c8")).WithSubtype(Capitalization).WithSupertype(InternalAccountingTransaction).Build();
			
            // PurchaseReturn
            new InheritanceBuilder(domain, new Guid("f5828301-6945-437d-b32e-17379544216c")).WithSubtype(PurchaseReturn).WithSupertype(Shipment).Build();
			
            // WorkEffortPartStandard
            new InheritanceBuilder(domain, new Guid("6522e002-ed7a-49fd-ba17-ae0d790547a4")).WithSubtype(WorkEffortPartStandard).WithSupertype(userInterfaceable).Build();
			
            // SurchargeComponent
            new InheritanceBuilder(domain, new Guid("5e784996-809e-4fe8-a5d1-9dfacc617911")).WithSubtype(SurchargeComponent).WithSupertype(PriceComponent).Build();
			
            // Bank
            new InheritanceBuilder(domain, new Guid("cdaee2a7-a131-41cf-8115-ac65eb18809f")).WithSubtype(Bank).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("fe7a699e-0493-460d-8023-4a3a4483c0c2")).WithSubtype(Bank).WithSupertype(Searchable).Build();
			
            // ProductRevenue
            new InheritanceBuilder(domain, new Guid("8cfffd6e-32aa-40b6-ad03-6ffaa72a188a")).WithSubtype(ProductRevenue).WithSupertype(userInterfaceable).Build();
			
            // DisbursementAccountingTransaction
            new InheritanceBuilder(domain, new Guid("090bd181-4da5-4c8c-8ea3-a003e413beae")).WithSubtype(DisbursementAccountingTransaction).WithSupertype(ExternalAccountingTransaction).Build();
			
            // OrderValue
            new InheritanceBuilder(domain, new Guid("ea3a438f-6c57-48a0-9e04-5b84d1c7d8dc")).WithSubtype(OrderValue).WithSupertype(userInterfaceable).Build();
			
            // VatTariff
            new InheritanceBuilder(domain, new Guid("b6f8389b-1eda-491a-912a-20d1e7983c85")).WithSubtype(VatTariff).WithSupertype(Enumeration).Build();
			
            // Obligation
            new InheritanceBuilder(domain, new Guid("52eb01f7-d844-43b8-a038-42760fcf39a3")).WithSubtype(Obligation).WithSupertype(ExternalAccountingTransaction).Build();
			
            // SalesInvoiceObjectState
            new InheritanceBuilder(domain, new Guid("fae00f24-ac20-4be5-83bf-0812d7e5f7f5")).WithSubtype(SalesInvoiceObjectState).WithSupertype(ObjectState).Build();
			
            // VatRate
            new InheritanceBuilder(domain, new Guid("0c56a15f-507d-4cc8-a0f0-f3b2f0c623e3")).WithSubtype(VatRate).WithSupertype(userInterfaceable).Build();
			
            // Invoice
            new InheritanceBuilder(domain, new Guid("0e8b30cb-83f9-44c5-b2af-217aff5cbc66")).WithSubtype(Invoice).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("0fdd44b5-be17-486b-bf90-6412bc8aa618")).WithSubtype(Invoice).WithSupertype(Localised).Build();
            new InheritanceBuilder(domain, new Guid("3001673a-65d0-4a3d-b6b1-25582d2923f8")).WithSubtype(Invoice).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("3cd70c92-f983-4f61-8897-1a32f5107d59")).WithSubtype(Invoice).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("595dc7f5-8075-4293-b177-505010a5a237")).WithSubtype(Invoice).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("9077d87a-e65e-4fd6-9747-d289b7540e7e")).WithSubtype(Invoice).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("a445c494-a0ee-427b-95e2-7dbae1e44769")).WithSubtype(Invoice).WithSupertype(Printable).Build();
            new InheritanceBuilder(domain, new Guid("c1fe0a2d-0713-4182-b7c0-be95dae607a7")).WithSubtype(Invoice).WithSupertype(UniquelyIdentifiable).Build();
			
            // ProfessionalServicesRelationship
            new InheritanceBuilder(domain, new Guid("564e4930-ae12-451c-b7ad-2ad91068f536")).WithSubtype(ProfessionalServicesRelationship).WithSupertype(PartyRelationship).Build();
			
            // RecurringCharge
            new InheritanceBuilder(domain, new Guid("9a7ee068-61ca-4f9c-90bc-1d13f52d73ff")).WithSubtype(RecurringCharge).WithSupertype(PriceComponent).Build();
			
            // FinancialTerm
            new InheritanceBuilder(domain, new Guid("f30e8ae5-b6e4-4172-baa3-72cb34291fbb")).WithSubtype(FinancialTerm).WithSupertype(AgreementTerm).Build();
			
            // RequirementStatus
            new InheritanceBuilder(domain, new Guid("af35bb1b-a54e-44b7-868a-12148f0c9f33")).WithSubtype(RequirementStatus).WithSupertype(userInterfaceable).Build();
			
            // PurchaseInvoiceItemObjectState
            new InheritanceBuilder(domain, new Guid("713b2d95-c1e7-4104-ad99-77e6f982a302")).WithSubtype(PurchaseInvoiceItemObjectState).WithSupertype(ObjectState).Build();
			
            // InvoiceTerm
            new InheritanceBuilder(domain, new Guid("1cd2c00f-e602-46b3-90c9-68f5e6236985")).WithSubtype(InvoiceTerm).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("af0da516-b300-455d-933a-02d15a97af65")).WithSubtype(InvoiceTerm).WithSupertype(AgreementTerm).Build();
			
            // DropShipment
            new InheritanceBuilder(domain, new Guid("db665e4e-4045-400e-9856-3087936ef35e")).WithSubtype(DropShipment).WithSupertype(Shipment).Build();
			
            // SalesInvoiceItem
            new InheritanceBuilder(domain, new Guid("1f3ecb9f-2d4d-4233-bd27-be434220d092")).WithSubtype(SalesInvoiceItem).WithSupertype(InvoiceItem).Build();
			
            // EngagementItem
            new InheritanceBuilder(domain, new Guid("aaaa31e8-961a-487c-9714-4635c4b095ca")).WithSubtype(EngagementItem).WithSupertype(userInterfaceable).Build();
			
            // OrderQuantityBreak
            new InheritanceBuilder(domain, new Guid("4d3ec119-2fcb-44ba-9a02-2b3c787b9cf1")).WithSubtype(OrderQuantityBreak).WithSupertype(userInterfaceable).Build();
			
            // ClientRelationship
            new InheritanceBuilder(domain, new Guid("3298699d-9ef8-47e3-99c4-ef3528f0371d")).WithSubtype(ClientRelationship).WithSupertype(PartyRelationship).Build();
			
            // PurchaseOrderItem
            new InheritanceBuilder(domain, new Guid("26c91392-7d50-4e33-9b48-24c6870d9adb")).WithSubtype(PurchaseOrderItem).WithSupertype(OrderItem).Build();
			
            // WorkEffortAssignmentRate
            new InheritanceBuilder(domain, new Guid("1a280507-2607-4747-a889-8d506e17e876")).WithSubtype(WorkEffortAssignmentRate).WithSupertype(userInterfaceable).Build();
			
            // EuSalesListType
            new InheritanceBuilder(domain, new Guid("f3e92d18-614f-4d5f-8a22-7b166b81bb7a")).WithSubtype(EuSalesListType).WithSupertype(Enumeration).Build();
			
            // PurchaseOrderItemObjectState
            new InheritanceBuilder(domain, new Guid("6f6446e0-d564-4829-8eba-906de6a2493a")).WithSubtype(PurchaseOrderItemObjectState).WithSupertype(ObjectState).Build();
			
            // Province
            new InheritanceBuilder(domain, new Guid("25f3a694-8e42-465d-abda-46f3eaec0ffc")).WithSubtype(Province).WithSupertype(CityBound).Build();
            new InheritanceBuilder(domain, new Guid("72e6b9c9-955d-461c-9484-a67310d1f10c")).WithSubtype(Province).WithSupertype(GeographicBoundary).Build();
            new InheritanceBuilder(domain, new Guid("8d1c0ebf-fbca-447e-9223-ffd768a798bd")).WithSubtype(Province).WithSupertype(CountryBound).Build();
            new InheritanceBuilder(domain, new Guid("b11f19b3-d1cf-4236-9ef9-c99ae09e9a81")).WithSubtype(Province).WithSupertype(userInterfaceable).Build();
			
            // InventoryItemVariance
            new InheritanceBuilder(domain, new Guid("1f606062-9291-4a31-b06c-bd3dc492655e")).WithSubtype(InventoryItemVariance).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("844b4149-350f-4e88-a276-cedf6ff5d0f5")).WithSubtype(InventoryItemVariance).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("a02db81a-f572-4772-b2ea-ddf49db6c9dc")).WithSubtype(InventoryItemVariance).WithSupertype(Commentable).Build();
			
            // ContactMechanism
            new InheritanceBuilder(domain, new Guid("087eeb41-e633-4a19-b84d-f95256d1d046")).WithSubtype(ContactMechanism).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("642b4d3b-593c-46b2-a379-9a534f30a25c")).WithSubtype(ContactMechanism).WithSupertype(Searchable).Build();
			
            // CommunicationEvent
            new InheritanceBuilder(domain, new Guid("02cc53fa-a8a5-4f92-8eb8-9877beee2939")).WithSubtype(CommunicationEvent).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("2fb97694-7587-408e-8501-f8dea1ec806a")).WithSubtype(CommunicationEvent).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("8c33cf93-9117-48f5-8e8c-4043991e8f93")).WithSubtype(CommunicationEvent).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("8d604885-fede-42ea-8463-862715411b57")).WithSubtype(CommunicationEvent).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("90bb4163-2b8b-4010-b721-93d171dc7aa0")).WithSubtype(CommunicationEvent).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("ba87c159-eb1d-4473-a2db-d0c166a09cd8")).WithSubtype(CommunicationEvent).WithSupertype(UniquelyIdentifiable).Build();
			
            // PositionResponsibility
            new InheritanceBuilder(domain, new Guid("48f9e69e-f213-4f36-b626-dc00b41abf20")).WithSubtype(PositionResponsibility).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("8c870657-a56f-4ac2-ac34-ad860ca47a54")).WithSubtype(PositionResponsibility).WithSupertype(userInterfaceable).Build();
			
            // DeliverableType
            new InheritanceBuilder(domain, new Guid("234b5bf8-943d-4b68-8288-87f6a2f1ec2f")).WithSubtype(DeliverableType).WithSupertype(Enumeration).Build();
			
            // SubAssembly
            new InheritanceBuilder(domain, new Guid("60762a02-1f40-4747-9529-af82858c8716")).WithSubtype(SubAssembly).WithSupertype(Part).Build();
			
            // RequirementObjectState
            new InheritanceBuilder(domain, new Guid("6b750f14-ce59-4012-a56e-6f1d521f2c56")).WithSubtype(RequirementObjectState).WithSupertype(ObjectState).Build();
			
            // WorkFlow
            new InheritanceBuilder(domain, new Guid("d5b1361c-35c4-453f-821d-5ec4c20e4eb9")).WithSubtype(WorkFlow).WithSupertype(WorkEffort).Build();
			
            // Task
            new InheritanceBuilder(domain, new Guid("ae16f89f-9f41-4cc1-9c49-9f9c6a68300f")).WithSubtype(Task).WithSupertype(WorkEffort).Build();
			
            // ResourceRequirement
            new InheritanceBuilder(domain, new Guid("8d1816ed-6264-43a1-b145-71b37d0a8c09")).WithSubtype(ResourceRequirement).WithSupertype(Requirement).Build();
			
            // BudgetItem
            new InheritanceBuilder(domain, new Guid("91e31260-9ad2-472c-af3b-bcfa0414f0e2")).WithSubtype(BudgetItem).WithSupertype(userInterfaceable).Build();
			
            // InternalRequirement
            new InheritanceBuilder(domain, new Guid("03fc88be-bb81-4bf8-b75b-0966b4fb5f8a")).WithSubtype(InternalRequirement).WithSupertype(Requirement).Build();
			
            // PositionReportingStructure
            new InheritanceBuilder(domain, new Guid("6133fcb9-1730-453c-a7ed-d2869ea7ba56")).WithSubtype(PositionReportingStructure).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("eadb791c-71b6-47f2-a850-8facf5073d1b")).WithSubtype(PositionReportingStructure).WithSupertype(Commentable).Build();
			
            // Partnership
            new InheritanceBuilder(domain, new Guid("90c19f86-5e2d-494c-ac53-66b18084badc")).WithSubtype(Partnership).WithSupertype(PartyRelationship).Build();
			
            // OperatingBudget
            new InheritanceBuilder(domain, new Guid("f0706b19-e5ce-427f-a04a-9cd483094bee")).WithSubtype(OperatingBudget).WithSupertype(Budget).Build();
			
            // Bin
            new InheritanceBuilder(domain, new Guid("25d53895-67ae-4ac8-ac11-50b08c587f09")).WithSubtype(Bin).WithSupertype(Container).Build();
			
            // ManufacturingConfiguration
            new InheritanceBuilder(domain, new Guid("5bec61f8-8388-4c69-8eb9-feaad55c9621")).WithSubtype(ManufacturingConfiguration).WithSupertype(InventoryItemConfiguration).Build();
			
            // IUnitOfMeasure
            new InheritanceBuilder(domain, new Guid("299a1d33-7872-4b6c-90bf-28104250a076")).WithSubtype(IUnitOfMeasure).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("7831779b-588f-41d2-8032-30aef235dcef")).WithSubtype(IUnitOfMeasure).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("aa33a66e-f371-40b9-9b36-81f18585a0aa")).WithSubtype(IUnitOfMeasure).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("c23ba038-9b7e-4ba4-821b-3d9881ee02ec")).WithSubtype(IUnitOfMeasure).WithSupertype(Searchable).Build();
			
            // ProfessionalPlacement
            new InheritanceBuilder(domain, new Guid("276b2fea-b262-46d0-b515-059a916abf27")).WithSubtype(ProfessionalPlacement).WithSupertype(EngagementItem).Build();
			
            // SalesRepCommission
            new InheritanceBuilder(domain, new Guid("249559c8-899d-4295-a87f-3a0da8cc6300")).WithSubtype(SalesRepCommission).WithSupertype(userInterfaceable).Build();
			
            // CityBound
            new InheritanceBuilder(domain, new Guid("5f139646-056d-4703-8063-57dc2cdab656")).WithSubtype(CityBound).WithSupertype(userInterfaceable).Build();
			
            // Deduction
            new InheritanceBuilder(domain, new Guid("f4c5b547-5ab2-47b0-bdbd-0deb7438d80e")).WithSubtype(Deduction).WithSupertype(userInterfaceable).Build();
			
            // CaseStatus
            new InheritanceBuilder(domain, new Guid("5ec68337-990f-4705-b6ff-e180490fe22b")).WithSubtype(CaseStatus).WithSupertype(userInterfaceable).Build();
			
            // DiscountComponent
            new InheritanceBuilder(domain, new Guid("a51c71e9-f77c-48fa-988b-2f43acc354ea")).WithSubtype(DiscountComponent).WithSupertype(PriceComponent).Build();
			
            // OrganisationUnit
            new InheritanceBuilder(domain, new Guid("cb76daef-3746-42ae-b74a-cd5aeab2fae2")).WithSubtype(OrganisationUnit).WithSupertype(Enumeration).Build();
			
            // PartSubstitute
            new InheritanceBuilder(domain, new Guid("c6a9283c-d874-46a3-aad7-ceaba2e1b9c3")).WithSubtype(PartSubstitute).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("de32c7a2-770c-4512-b30b-38fed846a5cc")).WithSubtype(PartSubstitute).WithSupertype(userInterfaceable).Build();
			
            // GoodOrderItem
            new InheritanceBuilder(domain, new Guid("43d93bb8-900c-4c6e-892c-f750aaf40155")).WithSubtype(GoodOrderItem).WithSupertype(EngagementItem).Build();
			
            // VolumeUsage
            new InheritanceBuilder(domain, new Guid("29ba7c85-7dd8-4db2-92f3-5b59120fa2be")).WithSubtype(VolumeUsage).WithSupertype(DeploymentUsage).Build();
			
            // ProductQuote
            new InheritanceBuilder(domain, new Guid("ab31e3cd-1574-4295-b754-0116d5e021ab")).WithSubtype(ProductQuote).WithSupertype(Quote).Build();
			
            // TransferStatus
            new InheritanceBuilder(domain, new Guid("75a44fc8-0c6f-45fa-a7a0-41bf1910386b")).WithSubtype(TransferStatus).WithSupertype(userInterfaceable).Build();
			
            // State
            new InheritanceBuilder(domain, new Guid("29223f21-4db9-4d6d-a4e9-36269054890d")).WithSubtype(State).WithSupertype(CityBound).Build();
            new InheritanceBuilder(domain, new Guid("699447ab-3c2b-4540-9bb1-14efa7e10dbc")).WithSubtype(State).WithSupertype(GeographicBoundary).Build();
            new InheritanceBuilder(domain, new Guid("a6b836fb-ed20-4080-b981-f5fd16bc2760")).WithSubtype(State).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("f490eb23-5d9d-464d-bc7a-84a96bd8da2a")).WithSubtype(State).WithSupertype(CountryBound).Build();
			
            // JournalEntryNumber
            new InheritanceBuilder(domain, new Guid("121835f2-05a9-46a4-a196-5a88191dc511")).WithSubtype(JournalEntryNumber).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("3ea0f7b5-88c6-42fb-ba1f-7b2aa5e8ef7e")).WithSubtype(JournalEntryNumber).WithSupertype(AccessControlledObject).Build();
			
            // Tolerance
            new InheritanceBuilder(domain, new Guid("cc3843ff-2229-49ea-8a33-d052a899bdeb")).WithSubtype(Tolerance).WithSupertype(PartSpecification).Build();
			
            // OrderAdjustment
            new InheritanceBuilder(domain, new Guid("12188fb3-7099-4c62-b121-7d3d1372f38f")).WithSubtype(OrderAdjustment).WithSupertype(userInterfaceable).Build();
			
            // EngineeringChange
            new InheritanceBuilder(domain, new Guid("0d3f0935-c051-4db2-85e3-003699c4f58f")).WithSubtype(EngineeringChange).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("34a35d8d-3b4b-4084-a375-c522dd4daaea")).WithSubtype(EngineeringChange).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("4a710591-8145-49e9-a22b-ac1faf7756b3")).WithSubtype(EngineeringChange).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("ee561549-7550-4fb4-83aa-b876a244dad8")).WithSubtype(EngineeringChange).WithSupertype(SearchResult).Build();
			
            // VatRatePurchaseKind
            new InheritanceBuilder(domain, new Guid("91620728-4b9f-4b22-809f-c2ff741cb6a5")).WithSubtype(VatRatePurchaseKind).WithSupertype(Enumeration).Build();
			
            // EmailTemplate
            new InheritanceBuilder(domain, new Guid("cc785e19-1084-4051-bd42-e600592616b2")).WithSubtype(EmailTemplate).WithSupertype(userInterfaceable).Build();
			
            // Threshold
            new InheritanceBuilder(domain, new Guid("d7b7beac-40a8-40c1-85ad-ee4e0241c9a7")).WithSubtype(Threshold).WithSupertype(AgreementTerm).Build();
			
            // EmploymentApplicationStatus
            new InheritanceBuilder(domain, new Guid("da5e9500-3c3e-4f69-bab9-41005031f1bc")).WithSubtype(EmploymentApplicationStatus).WithSupertype(Enumeration).Build();
			
            // Qualification
            new InheritanceBuilder(domain, new Guid("31865435-41e3-4c20-8b70-b5dfeb19168e")).WithSubtype(Qualification).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("b2b5a0ce-337f-4082-8bdf-422793769cf6")).WithSubtype(Qualification).WithSupertype(Searchable).Build();
			
            // InternalOrganisation
            new InheritanceBuilder(domain, new Guid("df1da5df-b70a-4197-9f39-6d370434186c")).WithSubtype(InternalOrganisation).WithSupertype(Party).Build();
			
            // EstimatedProductCost
            new InheritanceBuilder(domain, new Guid("1c1b9eaa-ed99-452e-b9d3-f67758c3f3d7")).WithSubtype(EstimatedProductCost).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("35740ffe-ac5f-44c4-bd82-5e54fd3680cc")).WithSubtype(EstimatedProductCost).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("6977d631-a960-433e-a70d-591ca94d0c4a")).WithSubtype(EstimatedProductCost).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("91836dc5-ea42-4653-a952-be60a95d7258")).WithSubtype(EstimatedProductCost).WithSupertype(userInterfaceable).Build();
			
            // OwnBankAccount
            new InheritanceBuilder(domain, new Guid("4b8297cd-df3b-45e9-94cc-1f7fa5a4a9d0")).WithSubtype(OwnBankAccount).WithSupertype(PaymentMethod).Build();
            new InheritanceBuilder(domain, new Guid("7787a118-b68b-4951-8938-c0f7a650b698")).WithSubtype(OwnBankAccount).WithSupertype(FinancialAccount).Build();
			
            // DeploymentUsage
            new InheritanceBuilder(domain, new Guid("0ab66ee3-76ed-4362-bbc3-185b137fd3cc")).WithSubtype(DeploymentUsage).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("44af2ca2-0ca1-496d-ab60-5a884072ee9f")).WithSubtype(DeploymentUsage).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("4aa2f7ac-c15e-4082-82b6-40e774212ffc")).WithSubtype(DeploymentUsage).WithSupertype(Period).Build();
			
            // PartyContactMechanism
            new InheritanceBuilder(domain, new Guid("c676f2d8-dc38-4612-ab6b-791fd1bdae7e")).WithSubtype(PartyContactMechanism).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("eef159fd-7362-40d6-a25d-a247586bf58a")).WithSubtype(PartyContactMechanism).WithSupertype(userInterfaceable).Build();
			
            // PartyRelationshipPriority
            new InheritanceBuilder(domain, new Guid("782f003e-4cee-4a01-8653-71f433666f76")).WithSubtype(PartyRelationshipPriority).WithSupertype(Enumeration).Build();
			
            // CostCenterSplitMethod
            new InheritanceBuilder(domain, new Guid("4acfefd8-f422-4e32-8195-c7c3e3b6d9c2")).WithSubtype(CostCenterSplitMethod).WithSupertype(Enumeration).Build();
			
            // EstimatedMaterialCost
            new InheritanceBuilder(domain, new Guid("91f13395-5918-4d7e-b1f8-2337738efc47")).WithSubtype(EstimatedMaterialCost).WithSupertype(EstimatedProductCost).Build();
			
            // QuoteTerm
            new InheritanceBuilder(domain, new Guid("1bfde88d-f4a5-4387-9c01-0a0032a9f15e")).WithSubtype(QuoteTerm).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("951e4e94-0e03-4093-bdba-cae9423836b7")).WithSubtype(QuoteTerm).WithSupertype(userInterfaceable).Build();
			
            // Transfer
            new InheritanceBuilder(domain, new Guid("b518e61f-c7a6-44bc-98ac-03714798c077")).WithSubtype(Transfer).WithSupertype(Shipment).Build();
			
            // Facility
            new InheritanceBuilder(domain, new Guid("1b415273-d11c-4602-9815-a5eee6bf61d5")).WithSubtype(Facility).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("38810684-1938-4471-b35b-dbcc7088bd30")).WithSubtype(Facility).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("573927ab-fbb3-4179-b78b-0af6221e3512")).WithSubtype(Facility).WithSupertype(GeoLocatable).Build();
            new InheritanceBuilder(domain, new Guid("5e8578f4-7ab5-4d8f-9752-f46fce818c0c")).WithSubtype(Facility).WithSupertype(Searchable).Build();
			
            // RevenueQuantityBreak
            new InheritanceBuilder(domain, new Guid("4d1f8de6-850f-432e-95ec-be9df5b69991")).WithSubtype(RevenueQuantityBreak).WithSupertype(userInterfaceable).Build();
			
            // GeneralLedgerAccountType
            new InheritanceBuilder(domain, new Guid("2227342f-f3db-4337-bc6d-a3ece423e669")).WithSubtype(GeneralLedgerAccountType).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f167ec73-9b87-46d1-96d2-447a16603698")).WithSubtype(GeneralLedgerAccountType).WithSupertype(AccessControlledObject).Build();
			
            // SerializedInventoryItemObjectState
            new InheritanceBuilder(domain, new Guid("d06322a6-d6ef-498b-a750-3ae5aaf590c5")).WithSubtype(SerializedInventoryItemObjectState).WithSupertype(ObjectState).Build();
			
            // FaceToFaceCommunication
            new InheritanceBuilder(domain, new Guid("95e81c3f-b0f9-48ff-8d0c-f00792e605d9")).WithSubtype(FaceToFaceCommunication).WithSupertype(CommunicationEvent).Build();
			
            // BudgetReview
            new InheritanceBuilder(domain, new Guid("4dcd6c28-8815-4538-bb12-f00e44aacf03")).WithSubtype(BudgetReview).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("6fec2c6b-b5e7-43cb-91b6-3b4a2557e5ae")).WithSubtype(BudgetReview).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("eb26333f-4b0b-473b-b693-bb3b2192a8ed")).WithSubtype(BudgetReview).WithSupertype(userInterfaceable).Build();
			
            // EngineeringChangeStatus
            new InheritanceBuilder(domain, new Guid("d4e598d0-8004-42aa-a739-9605802e54a4")).WithSubtype(EngineeringChangeStatus).WithSupertype(userInterfaceable).Build();
			
            // ProductQuality
            new InheritanceBuilder(domain, new Guid("81d5d0d7-de6b-4d92-b640-0dc28501a0e5")).WithSubtype(ProductQuality).WithSupertype(ProductFeature).Build();
            new InheritanceBuilder(domain, new Guid("8a4c1d35-dd2f-4e09-bfef-343826460d4a")).WithSubtype(ProductQuality).WithSupertype(Enumeration).Build();
			
            // Disbursement
            new InheritanceBuilder(domain, new Guid("b21b73e4-bc5c-4d32-9500-ea23b69649b6")).WithSubtype(Disbursement).WithSupertype(Payment).Build();
			
            // Research
            new InheritanceBuilder(domain, new Guid("e51829c5-5818-4961-9687-ed69dd2ada46")).WithSubtype(Research).WithSupertype(WorkEffort).Build();
			
            // PartBillOfMaterial
            new InheritanceBuilder(domain, new Guid("127a50a4-2eee-40cd-bd2b-24637b96ea30")).WithSubtype(PartBillOfMaterial).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("16793d0d-1ef8-4456-92df-cec2a8a47b12")).WithSubtype(PartBillOfMaterial).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("26becead-16d9-4208-a81e-73f806d54033")).WithSubtype(PartBillOfMaterial).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("7e5073e6-d6de-4e13-92c8-da6c48cf246e")).WithSubtype(PartBillOfMaterial).WithSupertype(Searchable).Build();
			
            // Journal
            new InheritanceBuilder(domain, new Guid("e45dce0d-21aa-4f7c-945b-f4ba0ed8a045")).WithSubtype(Journal).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("e7836aea-8ed8-4d0a-8aeb-cbfb560ff5c6")).WithSubtype(Journal).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f3c715d6-28d2-48e1-8519-bce52f05baa7")).WithSubtype(Journal).WithSupertype(AccessControlledObject).Build();
			
            // ShipmentItem
            new InheritanceBuilder(domain, new Guid("b3a4fa91-dedc-4aaa-b29d-21b03e738114")).WithSubtype(ShipmentItem).WithSupertype(userInterfaceable).Build();
			
            // ProductFeature
            new InheritanceBuilder(domain, new Guid("51bda3ee-be82-477f-9743-05564c16f739")).WithSubtype(ProductFeature).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("610427e6-6d1e-4e52-bd2f-c2a1d1538213")).WithSubtype(ProductFeature).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("e6525f8c-ac60-47c1-ba33-a9f207236679")).WithSubtype(ProductFeature).WithSupertype(userInterfaceable).Build();
			
            // Requirement
            new InheritanceBuilder(domain, new Guid("6993cc96-73ba-4a55-83a7-2e17afe3b3c8")).WithSubtype(Requirement).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("8eda8772-f755-4579-9603-ecc228dcc92f")).WithSubtype(Requirement).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("cf84fcbd-4c5e-4a5e-a4e1-9c2433ba674f")).WithSubtype(Requirement).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("e381bbb0-ceaa-4246-8880-7627ef2391a4")).WithSubtype(Requirement).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("fc8b3224-8fd9-42ca-a295-b71478da2261")).WithSubtype(Requirement).WithSupertype(Searchable).Build();
			
            // EmploymentAgreement
            new InheritanceBuilder(domain, new Guid("2e88e842-8cc5-4766-ac13-3fc8f9d31085")).WithSubtype(EmploymentAgreement).WithSupertype(Agreement).Build();
			
            // ManufacturerSuggestedRetailPrice
            new InheritanceBuilder(domain, new Guid("d227a1be-e97d-449e-bee5-736af19d6efb")).WithSubtype(ManufacturerSuggestedRetailPrice).WithSupertype(PriceComponent).Build();
			
            // NewsItem
            new InheritanceBuilder(domain, new Guid("2792bb76-0b5a-43ab-af86-7194038ef109")).WithSubtype(NewsItem).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("3ef4db3f-13e3-4c83-acde-fe64fbdf9ae2")).WithSubtype(NewsItem).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("b7fc290f-12a4-4911-8598-a9f37166b2d0")).WithSubtype(NewsItem).WithSupertype(userInterfaceable).Build();
			
            // PartyBenefit
            new InheritanceBuilder(domain, new Guid("b54a3516-3bd2-46df-9fa4-7f005d3102ba")).WithSubtype(PartyBenefit).WithSupertype(userInterfaceable).Build();
			
            // PostalAddress
            new InheritanceBuilder(domain, new Guid("0bbbbd48-c792-493d-beda-77ead5c71c1f")).WithSubtype(PostalAddress).WithSupertype(ContactMechanism).Build();
            new InheritanceBuilder(domain, new Guid("9c016254-ee52-49e6-bd4f-b136d90b66c5")).WithSubtype(PostalAddress).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("f2b48fa8-e085-4584-8288-6ac3d4291408")).WithSubtype(PostalAddress).WithSupertype(GeoLocatable).Build();
			
            // PackageQuantityBreak
            new InheritanceBuilder(domain, new Guid("166be787-3caa-4c90-85a5-2b1f3169f065")).WithSubtype(PackageQuantityBreak).WithSupertype(userInterfaceable).Build();
			
            // SubContractorRelationship
            new InheritanceBuilder(domain, new Guid("f592bd31-57f0-4072-b780-1375c650dc65")).WithSubtype(SubContractorRelationship).WithSupertype(PartyRelationship).Build();
			
            // ClientAgreement
            new InheritanceBuilder(domain, new Guid("41595676-5fbd-4231-b881-285605d9ab33")).WithSubtype(ClientAgreement).WithSupertype(Agreement).Build();
			
            // InvoiceItem
            new InheritanceBuilder(domain, new Guid("26fa7b34-09ef-4565-aa53-0dc70414d612")).WithSubtype(InvoiceItem).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("71701ec1-1fff-4381-8f3f-7f4141c5fd91")).WithSubtype(InvoiceItem).WithSupertype(Transitional).Build();
			
            // Store
            new InheritanceBuilder(domain, new Guid("7372e836-6f57-4821-8aea-8ac6baefb407")).WithSubtype(Store).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("ef4d3d8b-b865-4afc-b8a3-5a27eba9c3a7")).WithSubtype(Store).WithSupertype(userInterfaceable).Build();
			
            // Lot
            new InheritanceBuilder(domain, new Guid("463d5bab-3819-490e-b358-fd63e2835c7e")).WithSubtype(Lot).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("8873ecce-9760-47c5-a37a-a2d33877c556")).WithSubtype(Lot).WithSupertype(userInterfaceable).Build();
			
            // WorkEffortSkillStandard
            new InheritanceBuilder(domain, new Guid("7730be15-f57f-4f9a-af73-51d2a2dd3452")).WithSubtype(WorkEffortSkillStandard).WithSupertype(userInterfaceable).Build();
			
            // TimeAndMaterialsService
            new InheritanceBuilder(domain, new Guid("841252fa-7848-4cc2-ad67-c92678053088")).WithSubtype(TimeAndMaterialsService).WithSupertype(Service).Build();
			
            // Equipment
            new InheritanceBuilder(domain, new Guid("1fbe9695-fbc7-4036-aecc-1df5d2a8acdb")).WithSubtype(Equipment).WithSupertype(FixedAsset).Build();
			
            // RequestItem
            new InheritanceBuilder(domain, new Guid("b9eb6a4f-34a0-4d61-9093-91eaf3285f75")).WithSubtype(RequestItem).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("ba236a19-a844-481e-a2b0-1dd4e9523e88")).WithSubtype(RequestItem).WithSupertype(Commentable).Build();
			
            // SalesChannel
            new InheritanceBuilder(domain, new Guid("d0662c1a-8593-4385-927b-ac29280c8f9d")).WithSubtype(SalesChannel).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("e27b8ef6-9429-413b-9ef3-d970f6e30b1e")).WithSubtype(SalesChannel).WithSupertype(Enumeration).Build();
			
            // CustomerRequirement
            new InheritanceBuilder(domain, new Guid("cd637d35-6c79-4609-a983-2d7fb5a54d51")).WithSubtype(CustomerRequirement).WithSupertype(Requirement).Build();
			
            // Property
            new InheritanceBuilder(domain, new Guid("9a080b3c-d894-433d-a062-a8abbf0c1ebf")).WithSubtype(Property).WithSupertype(FixedAsset).Build();
			
            // ConstraintSpecification
            new InheritanceBuilder(domain, new Guid("6feb4aed-e903-434c-b7ba-bd9a9f9f1f59")).WithSubtype(ConstraintSpecification).WithSupertype(PartSpecification).Build();
			
            // DesiredProductFeature
            new InheritanceBuilder(domain, new Guid("59bd142a-f956-4f27-b24c-16fa4fefbef2")).WithSubtype(DesiredProductFeature).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d7c53f41-af96-4c8a-8fca-f8fb6c09ccab")).WithSubtype(DesiredProductFeature).WithSupertype(Searchable).Build();
			
            // SalesOrderItemStatus
            new InheritanceBuilder(domain, new Guid("dd102abe-831c-46d1-9e54-54ea955756e2")).WithSubtype(SalesOrderItemStatus).WithSupertype(userInterfaceable).Build();
			
            // ActivityUsage
            new InheritanceBuilder(domain, new Guid("1c0760af-1eda-4877-a27f-5cb6113e09bb")).WithSubtype(ActivityUsage).WithSupertype(DeploymentUsage).Build();
			
            // Program
            new InheritanceBuilder(domain, new Guid("fcb61246-0139-4ad2-bbf5-bf7e54539d7e")).WithSubtype(Program).WithSupertype(WorkEffort).Build();
			
            // CommunicationEventStatus
            new InheritanceBuilder(domain, new Guid("ed429bde-4cc2-4341-8daf-0270c1c7e32a")).WithSubtype(CommunicationEventStatus).WithSupertype(userInterfaceable).Build();
			
            // AgreementSection
            new InheritanceBuilder(domain, new Guid("c8f1b504-22cb-4c9d-90ce-9d3f2fbcabee")).WithSubtype(AgreementSection).WithSupertype(AgreementItem).Build();
			
            // Good
            new InheritanceBuilder(domain, new Guid("0064f173-9ab7-4291-9d28-cbfec04f8e72")).WithSubtype(Good).WithSupertype(Product).Build();
			
            // EngineeringChangeObjectState
            new InheritanceBuilder(domain, new Guid("5ed6c227-75ee-477f-a235-68c76a7b4af7")).WithSubtype(EngineeringChangeObjectState).WithSupertype(ObjectState).Build();
			
            // AccountingTransactionDetail
            new InheritanceBuilder(domain, new Guid("ec4c9420-7cb4-4311-9eff-5e6592102701")).WithSubtype(AccountingTransactionDetail).WithSupertype(userInterfaceable).Build();
			
            // County
            new InheritanceBuilder(domain, new Guid("4f9d201a-e84b-4c34-8363-09abb27245f3")).WithSubtype(County).WithSupertype(GeographicBoundary).Build();
            new InheritanceBuilder(domain, new Guid("d6ae4dff-bcb9-4662-9f19-bd140fddf414")).WithSubtype(County).WithSupertype(CityBound).Build();
            new InheritanceBuilder(domain, new Guid("f062668d-5f0f-4753-b9ce-f5c1076c3a9f")).WithSubtype(County).WithSupertype(userInterfaceable).Build();
			
            // ShippingAndHandlingCharge
            new InheritanceBuilder(domain, new Guid("c479419d-be07-42b8-9b0b-3cbc38a37a4a")).WithSubtype(ShippingAndHandlingCharge).WithSupertype(OrderAdjustment).Build();
			
            // PerformanceReviewItemType
            new InheritanceBuilder(domain, new Guid("fd429483-6625-4dd7-be18-76add0bf3870")).WithSubtype(PerformanceReviewItemType).WithSupertype(Enumeration).Build();
			
            // PostalBoundary
            new InheritanceBuilder(domain, new Guid("db2b6a67-26d1-4d11-9ed9-8202d937ff76")).WithSubtype(PostalBoundary).WithSupertype(userInterfaceable).Build();
			
            // ProductCategory
            new InheritanceBuilder(domain, new Guid("02319e82-f200-4195-ab85-6b6ba8eb3485")).WithSubtype(ProductCategory).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("167b6df3-1024-4004-bee5-16748b6842f1")).WithSubtype(ProductCategory).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("729974a5-fae6-4af8-acb9-7d03c5afbff0")).WithSubtype(ProductCategory).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("c850b268-59f5-4517-bc32-c7b377f64eb8")).WithSubtype(ProductCategory).WithSupertype(UniquelyIdentifiable).Build();
			
            // RequestForInformation
            new InheritanceBuilder(domain, new Guid("ef1c92bf-6afe-43cf-9107-6c13db7086fa")).WithSubtype(RequestForInformation).WithSupertype(Request).Build();
			
            // CountryBound
            new InheritanceBuilder(domain, new Guid("49bed169-35f0-4f7c-94ff-dbafcbec5984")).WithSubtype(CountryBound).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("d17fa144-2d00-4692-a52c-fca727e6fe49")).WithSubtype(CountryBound).WithSupertype(Searchable).Build();
			
            // VatForm
            new InheritanceBuilder(domain, new Guid("0e45a8cc-dbf1-4855-8941-d5b99f43691a")).WithSubtype(VatForm).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("601a6c95-67a7-4a53-8c83-48737b46e7f1")).WithSubtype(VatForm).WithSupertype(userInterfaceable).Build();
			
            // BudgetRevisionImpact
            new InheritanceBuilder(domain, new Guid("e999e0ef-c250-496a-a16b-26058e1ed187")).WithSubtype(BudgetRevisionImpact).WithSupertype(userInterfaceable).Build();
			
            // Budget
            new InheritanceBuilder(domain, new Guid("440cafac-e072-4f7b-88d6-cb8691bdaff8")).WithSubtype(Budget).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("4a8a466c-f53d-45c6-b3af-7db13fa54bbd")).WithSubtype(Budget).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("5055e253-025b-4583-bea0-7d360a66ccae")).WithSubtype(Budget).WithSupertype(SearchResult).Build();
            new InheritanceBuilder(domain, new Guid("69c12833-dd88-4898-9559-e8a0429e4b03")).WithSubtype(Budget).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("d445b9eb-0699-4207-806e-522bfcd6d3a2")).WithSubtype(Budget).WithSupertype(Transitional).Build();
            new InheritanceBuilder(domain, new Guid("e70b3210-1dbf-4111-9b62-2013cfd8cba5")).WithSubtype(Budget).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("f632d01f-a2f5-4126-b717-87605303f079")).WithSubtype(Budget).WithSupertype(Searchable).Build();
			
            // TemplatePurpose
            new InheritanceBuilder(domain, new Guid("33304215-276e-4294-958f-a5a88dbe2e5c")).WithSubtype(TemplatePurpose).WithSupertype(Enumeration).Build();
			
            // WebSiteCommunication
            new InheritanceBuilder(domain, new Guid("40929272-a254-4661-a193-865e4277dd0a")).WithSubtype(WebSiteCommunication).WithSupertype(CommunicationEvent).Build();
			
            // Withdrawal
            new InheritanceBuilder(domain, new Guid("ca136431-b312-4f23-b53e-bffe69df757f")).WithSubtype(Withdrawal).WithSupertype(FinancialAccountTransaction).Build();
			
            // Deployment
            new InheritanceBuilder(domain, new Guid("3208ce08-681d-4446-bb92-72ac3be6ff86")).WithSubtype(Deployment).WithSupertype(Searchable).Build();
            new InheritanceBuilder(domain, new Guid("48b7bc22-694d-4337-aa8c-c6a0f35d649f")).WithSubtype(Deployment).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("5f8f6068-3fcf-4de6-9cb7-6ac5c99fdc13")).WithSubtype(Deployment).WithSupertype(Period).Build();
            new InheritanceBuilder(domain, new Guid("f5c49245-e32b-4377-811e-60618f5a4ad8")).WithSubtype(Deployment).WithSupertype(SearchResult).Build();
			
            // PayCheck
            new InheritanceBuilder(domain, new Guid("7dd4d8e9-8385-433d-87d3-c9635a819179")).WithSubtype(PayCheck).WithSupertype(Payment).Build();
			
            // MaritalStatus
            new InheritanceBuilder(domain, new Guid("9b6090c4-69a4-410b-a6d5-cc7e5039dfbd")).WithSubtype(MaritalStatus).WithSupertype(Enumeration).Build();
			
            // Manifest
            new InheritanceBuilder(domain, new Guid("40727f08-14dd-48b9-81e3-fc79c5a1eb86")).WithSubtype(Manifest).WithSupertype(Document).Build();
			
            // ExportDocument
            new InheritanceBuilder(domain, new Guid("111a7813-2a05-448f-92fe-bc77207ce6f6")).WithSubtype(ExportDocument).WithSupertype(Document).Build();
			
            // InventoryItemConfiguration
            new InheritanceBuilder(domain, new Guid("3a5a4422-8a6b-4273-a9cb-c247121cfb94")).WithSubtype(InventoryItemConfiguration).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("caf0d9c0-1f3e-4e85-bcea-2ce39738bb26")).WithSubtype(InventoryItemConfiguration).WithSupertype(userInterfaceable).Build();
			
            // CustomerShipmentStatus
            new InheritanceBuilder(domain, new Guid("5f1b571d-8dc6-4795-a65f-e4ee7b0d33e7")).WithSubtype(CustomerShipmentStatus).WithSupertype(userInterfaceable).Build();
			
            // ExpenseEntry
            new InheritanceBuilder(domain, new Guid("ae2d6032-e4a7-479a-bb4c-65220bb7fe79")).WithSubtype(ExpenseEntry).WithSupertype(ServiceEntry).Build();
			
            // ProductAssociation
            new InheritanceBuilder(domain, new Guid("24de7a14-73c3-4c6b-ae23-1803dec23a33")).WithSubtype(ProductAssociation).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("8657ba58-04b7-4639-b882-9ede76f02871")).WithSubtype(ProductAssociation).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("b40ca219-e0da-44e3-88d1-8b93f6ff2df0")).WithSubtype(ProductAssociation).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("bb38455b-0bca-430a-8dae-4eb897b3e52a")).WithSubtype(ProductAssociation).WithSupertype(Period).Build();
			
            // PartSpecificationStatus
            new InheritanceBuilder(domain, new Guid("0c4da82d-c2f7-4056-92ca-cc527dfcad35")).WithSubtype(PartSpecificationStatus).WithSupertype(userInterfaceable).Build();
			
            // DistributionChannelRelationship
            new InheritanceBuilder(domain, new Guid("72cbe8f0-2465-4fe7-9a73-fcbe73288399")).WithSubtype(DistributionChannelRelationship).WithSupertype(PartyRelationship).Build();
			
            // CustomerShipmentObjectState
            new InheritanceBuilder(domain, new Guid("d04d6bde-7dea-4e4d-a9f4-f1d94ebea9ab")).WithSubtype(CustomerShipmentObjectState).WithSupertype(ObjectState).Build();
			
            // PaymentMethod
            new InheritanceBuilder(domain, new Guid("4d44c91a-6d37-40a1-823c-aad06810c1ff")).WithSubtype(PaymentMethod).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("91fb2e41-2a62-4bb5-8c54-8a0bea14ecba")).WithSubtype(PaymentMethod).WithSupertype(UniquelyIdentifiable).Build();
            new InheritanceBuilder(domain, new Guid("b7ca4728-c568-43a0-a6b3-7d7e0dc8da7c")).WithSubtype(PaymentMethod).WithSupertype(AccessControlledObject).Build();
            new InheritanceBuilder(domain, new Guid("cc3c7d14-952e-494d-b422-b5a32bb0c107")).WithSubtype(PaymentMethod).WithSupertype(Searchable).Build();
			
            // GenderType
            new InheritanceBuilder(domain, new Guid("63c4f9df-07de-48e5-9579-e03729225505")).WithSubtype(GenderType).WithSupertype(Enumeration).Build();
			
            // OrderItem
            new InheritanceBuilder(domain, new Guid("09df78f6-a170-4320-8630-d6dc864ebcfd")).WithSubtype(OrderItem).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("73b21f03-9940-4567-b545-7cd1fb5f45a7")).WithSubtype(OrderItem).WithSupertype(Commentable).Build();
            new InheritanceBuilder(domain, new Guid("7b9105ed-c840-43bc-8843-b823ded8b45f")).WithSubtype(OrderItem).WithSupertype(Transitional).Build();
			
            // Office
            new InheritanceBuilder(domain, new Guid("85b38ece-b53e-4453-aa34-9319dbc57906")).WithSubtype(Office).WithSupertype(Facility).Build();
			
            // EmailAddress
            new InheritanceBuilder(domain, new Guid("3f8a6f57-483e-4b9f-b13e-7186aa9dcdf3")).WithSubtype(EmailAddress).WithSupertype(ElectronicAddress).Build();
			
            // WorkEffortInventoryAssignment
            new InheritanceBuilder(domain, new Guid("cd995c65-f0cf-4b95-8719-89d62b04ef99")).WithSubtype(WorkEffortInventoryAssignment).WithSupertype(userInterfaceable).Build();
			
            // CommunicationEventObjectState
            new InheritanceBuilder(domain, new Guid("46b8bc38-d5a2-4741-ae58-77c215cba4b8")).WithSubtype(CommunicationEventObjectState).WithSupertype(ObjectState).Build();
			
            // City
            new InheritanceBuilder(domain, new Guid("35fd7593-85e6-4c75-acaf-23c027373f7a")).WithSubtype(City).WithSupertype(GeographicBoundary).Build();
            new InheritanceBuilder(domain, new Guid("6e203504-dfa2-45c2-b651-54a7fda9254a")).WithSubtype(City).WithSupertype(userInterfaceable).Build();
            new InheritanceBuilder(domain, new Guid("c5f137ac-bf20-4142-bbe2-f2a7af930772")).WithSubtype(City).WithSupertype(CountryBound).Build();
			
            // PickListObjectState
            new InheritanceBuilder(domain, new Guid("00ec9b9b-f576-4576-ae02-0c0746948d56")).WithSubtype(PickListObjectState).WithSupertype(ObjectState).Build();
			
            // MaterialsUsage
            new InheritanceBuilder(domain, new Guid("4218e630-225f-4032-9609-4f67628702d2")).WithSubtype(MaterialsUsage).WithSupertype(ServiceEntry).Build();
			
            // EmploymentTerminationReason
            new InheritanceBuilder(domain, new Guid("5c52265e-ea77-4746-9b1f-0046d6622263")).WithSubtype(EmploymentTerminationReason).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("6148d53b-2e1c-4611-b0eb-46fdcd97da65")).WithSubtype(EmploymentTerminationReason).WithSupertype(Searchable).Build();
			
            // WorkEffortObjectState
            new InheritanceBuilder(domain, new Guid("d66bff4f-9586-4bbd-8fbc-526a0ec7cca0")).WithSubtype(WorkEffortObjectState).WithSupertype(ObjectState).Build();
			
            // TimePeriodUsage
            new InheritanceBuilder(domain, new Guid("adf08681-f036-4394-81d4-fc2ea2eb19ae")).WithSubtype(TimePeriodUsage).WithSupertype(DeploymentUsage).Build();
			
            // BudgetObjectState
            new InheritanceBuilder(domain, new Guid("7e58e071-1c8a-4482-9768-321dd887789f")).WithSubtype(BudgetObjectState).WithSupertype(ObjectState).Build();
			
            // WorkRequirement
            new InheritanceBuilder(domain, new Guid("0be4cde1-613d-4718-b9fc-a1c9594ff618")).WithSubtype(WorkRequirement).WithSupertype(Requirement).Build();
			
            // Fee
            new InheritanceBuilder(domain, new Guid("9e8e17a2-0e9d-46e2-b0b3-26befed0edc1")).WithSubtype(Fee).WithSupertype(OrderAdjustment).Build();
			
            // PhoneCommunication
            new InheritanceBuilder(domain, new Guid("1c3b3492-3020-4d56-a3af-f4b82d5af5f1")).WithSubtype(PhoneCommunication).WithSupertype(CommunicationEvent).Build();
			
            // ProductDeliverySkillRequirement
            new InheritanceBuilder(domain, new Guid("3c46ad73-9cf9-4d0a-928e-e87d3da6a75e")).WithSubtype(ProductDeliverySkillRequirement).WithSupertype(userInterfaceable).Build();
			
            // SalesRepProductCategoryRevenue
            new InheritanceBuilder(domain, new Guid("467836cd-8b9f-475a-b72c-338474df554c")).WithSubtype(SalesRepProductCategoryRevenue).WithSupertype(userInterfaceable).Build();
			
            // ServiceFeature
            new InheritanceBuilder(domain, new Guid("044d8ced-1500-4eb9-9963-5f2697fa5d06")).WithSubtype(ServiceFeature).WithSupertype(Enumeration).Build();
            new InheritanceBuilder(domain, new Guid("26f4f853-8a37-46a9-ad5a-bddd2c723324")).WithSubtype(ServiceFeature).WithSupertype(ProductFeature).Build();
			
            // PartyProductRevenueHistory
            new InheritanceBuilder(domain, new Guid("56ace0ca-39ce-48ad-8eaf-aa8ac4bb9020")).WithSubtype(PartyProductRevenueHistory).WithSupertype(userInterfaceable).Build();
			
            // Country
            new InheritanceBuilder(domain, new Guid("e5295ca5-8795-4761-ab74-ef761f9242ef")).WithSubtype(Country).WithSupertype(GeographicBoundary).Build();
            new InheritanceBuilder(domain, new Guid("f90c3356-75e6-4e4f-803d-ef660e46cc9f")).WithSubtype(Country).WithSupertype(CityBound).Build();
			
            // Person
            new InheritanceBuilder(domain, new Guid("8ecbd248-e524-4bb7-9ea7-854391c91046")).WithSubtype(Person).WithSupertype(Party).Build();
			
            // Currency
            new InheritanceBuilder(domain, new Guid("f78b17e7-a29a-4bd6-b15b-6d1ef70263c2")).WithSubtype(Currency).WithSupertype(IUnitOfMeasure).Build();
			
            // ProductFeatureApplicabilityRelationship
            new RelationTypeBuilder(domain, new Guid("3198ade4-8080-4584-9b67-b00af681c5cf"), new Guid("d0f5e3af-01ea-44fc-8921-a7eec052ed22"), new Guid("73ff3323-7903-42c7-8278-b5f36f547463")).WithObjectTypes(ProductFeatureApplicabilityRelationship, Product).WithSingularName("AvailableFor")  .WithPluralName("AvailableFor")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c17d3bde-ebbc-463c-b9cb-b0a5a700c6a1"), new Guid("323a85e8-ee5c-4967-9f3d-64e8e5b04d7c"), new Guid("22a7598e-6862-4627-b380-06804e263871")).WithObjectTypes(ProductFeatureApplicabilityRelationship, ProductFeature).WithSingularName("UsedToDefine")  .WithPluralName("UsedToDefines")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartSpecification
            new RelationTypeBuilder(domain, new Guid("202bc60e-5702-4dce-a41a-8dc5e198090c"), new Guid("854fcf78-d6fe-40c8-a988-54df5fb5933c"), new Guid("e60cafba-ec5a-4578-83e4-a4a63d4e49a6")).WithObjectTypes(PartSpecification, PartSpecificationStatus).WithSingularName("PartSpecificationStatus")  .WithPluralName("PartSpecificationStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4bfcdcc0-d6d3-4335-92ce-a8b1271f4124"), new Guid("792ce48c-749e-4bd3-b0a9-3ab93e802d8d"), new Guid("1ef186ee-e996-4e79-bb81-7f7c406702d1")).WithObjectTypes(PartSpecification, PartSpecificationObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6a83ef4b-1ef5-4782-b9fd-19e3231c29c5"), new Guid("93f4241d-23ea-46ad-bcaa-fd1f5c909c43"), new Guid("c2b4a79f-c245-40d5-834e-5939c7748462")).WithObjectTypes(PartSpecification, allorsDateTime).WithSingularName("DocumentationDate")  .WithPluralName("DocumentationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("79f03090-e058-439c-9398-738f08be2be1"), new Guid("8d5e16e5-2a18-4779-ad87-537db639c94e"), new Guid("f5451e3d-67ed-416f-aeeb-45daf876fd0d")).WithObjectTypes(PartSpecification, PartSpecificationStatus).WithSingularName("CurrentPartSpecificationStatus")  .WithPluralName("CurrenPartSpecificationStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b31753c5-6983-4753-8016-be389a71504b"), new Guid("702aad92-4bd9-461d-87c5-b438ecaa1387"), new Guid("f7307e39-1cce-4e70-a51a-d155a602526b")).WithObjectTypes(PartSpecification, PartSpecificationObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e20b0fd5-f10a-44df-8bef-f454e7d23bce"), new Guid("0c7ad60f-57c9-469b-b8e4-dabeae4398ee"), new Guid("6a208020-712c-4ce8-b69b-ea4523ba2e85")).WithObjectTypes(PartSpecification, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // OrderShipment
            new RelationTypeBuilder(domain, new Guid("1494758b-f763-48e5-a5a9-cd5c83a8af95"), new Guid("5aa8e3aa-cc9c-4b12-9126-5ab6f160d661"), new Guid("d49541c8-7cf6-439f-84e0-c8a1d73e5f3c")).WithObjectTypes(OrderShipment, SalesOrderItem).WithSingularName("SalesOrderItem")  .WithPluralName("SalesOrderItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("261a25f4-672a-44a0-ad2d-1c62ba383006"), new Guid("cfaa2021-233c-4b55-b33d-65b9344adb67"), new Guid("69f35130-996e-4a55-b6be-90199a2548d0")).WithObjectTypes(OrderShipment, allorsBoolean).WithSingularName("Picked")  .WithPluralName("Pickeds")      .Build();
            new RelationTypeBuilder(domain, new Guid("b55bbdb8-af05-4008-a6a7-b4eea78096bd"), new Guid("a4d6f79e-c204-44ca-b7db-3a0a3eacff69"), new Guid("a6a0d0ac-15c6-489f-ab15-197314f4f52c")).WithObjectTypes(OrderShipment, ShipmentItem).WithSingularName("ShipmentItem")  .WithPluralName("ShipmentItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d4725e9c-b72c-4cdf-95f9-70f9c4b57b11"), new Guid("4f4c74fc-44d8-445e-aa2e-1e79c2fd6b87"), new Guid("c9ce4f17-3bef-4b0b-a5e0-4fc38641f8ed")).WithObjectTypes(OrderShipment, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d6c35df9-dad3-4e4c-b66e-5ccda26093d5"), new Guid("b8ea1ed0-ba19-44c3-9e8d-5228734b3bc4"), new Guid("78bddfef-0bbe-4185-8a5a-78e5a3ba42a0")).WithObjectTypes(OrderShipment, PurchaseOrderItem).WithSingularName("PurchaseOrderItem")  .WithPluralName("PurchaseOrderItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ProductRequirement
            new RelationTypeBuilder(domain, new Guid("48ce0470-5738-4d9b-ab23-ea244e90091d"), new Guid("af379058-8ac3-4d0e-8eb4-715fcdda5e44"), new Guid("9237556e-d3c2-4404-a39c-11660471d23d")).WithObjectTypes(ProductRequirement, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a72274b6-2767-4cb9-8f3d-dc1e367c6f1b"), new Guid("e991712f-bbed-4cb9-98ef-e7ff2506fc11"), new Guid("57ed8d56-0c40-47a8-9e49-be7a35294800")).WithObjectTypes(ProductRequirement, DesiredProductFeature).WithSingularName("DesiredProductFeature")  .WithPluralName("DesiredProductFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // SalesInvoiceItemStatus
            new RelationTypeBuilder(domain, new Guid("2d0de395-ccfe-46b4-9391-020c276ddafd"), new Guid("a4d34c93-13ee-449f-b016-47707c7ae72d"), new Guid("054b768e-78ae-44e9-939f-765fe5c4ccf4")).WithObjectTypes(SalesInvoiceItemStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("ea2f0286-38d6-42ad-825a-a692e51cd209"), new Guid("0b1f32c0-e8de-4d3e-9de8-e172c1477ada"), new Guid("54b5248a-d692-4c78-9cd9-b6a9e7bc3e34")).WithObjectTypes(SalesInvoiceItemStatus, SalesInvoiceItemObjectState).WithSingularName("SalesInvoiceItemObjectState")  .WithPluralName("SalesInvoiceItemObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // QuoteItem
            new RelationTypeBuilder(domain, new Guid("05c69ae6-e671-4520-87c7-5fa24a92c44d"), new Guid("3f668e84-81dc-479a-a26f-b4fbc1cd79ee"), new Guid("e47f270a-f3d9-4c7b-968f-395bbf8e7e68")).WithObjectTypes(QuoteItem, Party).WithSingularName("Authorizer")  .WithPluralName("Authorizers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1214acee-1b91-4c16-b6d0-84f865b6a43a"), new Guid("b9120662-ebae-4f52-a913-4a3f9a91398e"), new Guid("d008f8e2-a378-4e50-a9dd-32ffa427708c")).WithObjectTypes(QuoteItem, Deliverable).WithSingularName("Deliverable")  .WithPluralName("Deliverables")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("20a5f3d3-8b12-4717-874f-eb62ad0a1654"), new Guid("10c5839d-c046-4b43-919b-d647c70bd94f"), new Guid("56e57558-988c-4b1a-a6f8-7f93f621bd06")).WithObjectTypes(QuoteItem, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("262a458d-0b38-4123-b210-576633297f44"), new Guid("e252b457-9fac-429d-a337-0c48a46c2bf0"), new Guid("a7ae793d-d315-4ac1-93c7-783391b2d294")).WithObjectTypes(QuoteItem, allorsDateTime).WithSingularName("EstimatedDeliveryDate")  .WithPluralName("EstimatedDeliveryDate")      .Build();
            new RelationTypeBuilder(domain, new Guid("28c0e280-16ce-48fc-8bc4-734e1ea0cd36"), new Guid("49bd248e-a34f-43ce-b2fd-9db0d5b01db4"), new Guid("6eb4000d-559d-42b2-b02b-452370fa15b4")).WithObjectTypes(QuoteItem, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("28f5767e-16fa-40aa-89d9-c23ee29572d1"), new Guid("4d7a3080-b3f9-47e8-8363-474a94699772"), new Guid("1da894ac-53bb-4414-b582-9bc6717f369a")).WithObjectTypes(QuoteItem, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("73ecd49f-9614-4902-8ec6-9b503bffe9f2"), new Guid("7ee32e78-a214-4e4a-bb43-d2f6642e997a"), new Guid("e64fb7aa-75de-41a6-a76c-f25f22dfcf47")).WithObjectTypes(QuoteItem, allorsDecimal).WithSingularName("UnitPrice")  .WithPluralName("UnitPrices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8b1280eb-0fef-450e-afc8-dbdc6fc65abb"), new Guid("8a93a23b-6be9-44db-8c92-4ad4c2cc405b"), new Guid("1961e2a8-ecf5-4c7b-8815-8ee4b2461820")).WithObjectTypes(QuoteItem, Skill).WithSingularName("Skill")  .WithPluralName("Skill")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8be8dc07-a358-4b8d-a84c-01bd3efea6fb"), new Guid("803fc0c9-ad84-4679-8906-4f9536c7ff6d"), new Guid("a997bb36-f534-4d90-9a90-947cc2a56a64")).WithObjectTypes(QuoteItem, WorkEffort).WithSingularName("WorkEffort")  .WithPluralName("WorkEfforts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d1f7f2cb-cbc8-42b4-a3f0-198ff35957de"), new Guid("0f429c19-5cb8-459a-b95a-9e3ec1e045f3"), new Guid("0750e77a-40bd-4a0b-89a6-6e6fbb797cc4")).WithObjectTypes(QuoteItem, QuoteTerm).WithSingularName("QuoteTerm")  .WithPluralName("QuoteTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d7805656-dd9c-4144-a11f-efbb32e6ecb3"), new Guid("a1d818f2-8e1a-4984-b2d7-4b1f34558568"), new Guid("3a3442f4-26af-407d-90c6-38c4d5d40bae")).WithObjectTypes(QuoteItem, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("dc00905b-bb4f-4a47-88d6-1ae6ce0855f7"), new Guid("f9a2cdde-485c-46a0-8f06-9f9687328737"), new Guid("e3308741-e48e-4b91-81ef-de38dcb5d80d")).WithObjectTypes(QuoteItem, RequestItem).WithSingularName("RequestItem")  .WithPluralName("RequestItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesRepPartyProductCategoryRevenue
            new RelationTypeBuilder(domain, new Guid("192f7c27-fd25-45a2-8de2-4101b7ce42f9"), new Guid("8e411184-ed58-4bb2-bb06-771cc93b8f53"), new Guid("06ddf58f-379e-4c8e-9679-c6677fe124e8")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("25bd83fe-0ff9-4241-b3e6-d0f1d06ab4be"), new Guid("fa5dfed6-df70-4316-a2ab-f9b3499dd987"), new Guid("fb2b7da7-d226-40fd-a638-d7cb906afa14")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2a3346f4-ea7f-4be0-a69e-fcfb88cba88a"), new Guid("f6b88a6a-a906-4002-a2bf-d9007750bc0d"), new Guid("3e6ee104-2371-48be-aac1-f4f7d752c2e8")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3ec97b92-b4d9-456a-8a38-6f129fa8f963"), new Guid("67b70d49-9dad-4d20-9388-1cd98e2af413"), new Guid("589a1da1-1add-441b-8cfc-55f0fa0b2242")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("46deffbe-e2b7-46d8-9669-27ade84de02c"), new Guid("58fd5226-a4f7-4971-a3e2-54a5b41c2eb7"), new Guid("ea0e2bc7-8df9-4380-abd9-588e6805e84f")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("689ea9aa-082f-43e5-be65-554df3b0f8dc"), new Guid("f3a151f6-3d05-4401-9925-401aff21d437"), new Guid("93a71bd3-c6e1-42a8-9425-9704698c0f1c")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("77715cdd-a0c6-47da-8072-6a23be984cad"), new Guid("fb61b669-ebf2-46de-9178-a5ded1d03930"), new Guid("c00dfdba-f353-4e2f-a875-70d16e96daaf")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ee15e022-a420-4bbf-84f4-75b380cea7bb"), new Guid("fece3c35-cbf8-495c-820b-bad9f6dd02eb"), new Guid("eac4e8b1-fd89-4d16-86bd-3be5cb1178e2")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f378a0a3-0ffc-4761-8ccc-d906b257c2f2"), new Guid("1f9500ce-38dc-4b90-a638-c2a457978cc4"), new Guid("a7b8f672-2e7e-4413-a3ff-a3a9ac7b3452")).WithObjectTypes(SalesRepPartyProductCategoryRevenue, allorsString).WithSingularName("SalesRepName")  .WithPluralName("SalesRepNames")      .WithSize(256).Build();
			
            // PayGrade
            new RelationTypeBuilder(domain, new Guid("88ba9ad4-e7de-42d9-89d7-9292d34d308b"), new Guid("36e42e9c-a623-493f-a29b-a34cdf485612"), new Guid("64944205-252c-49d7-8a59-771b4a8a4318")).WithObjectTypes(PayGrade, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f7e52596-8814-48ff-a703-d80255110c5f"), new Guid("7ff0bc91-cc37-468d-b5ed-ae2de433acc8"), new Guid("dc165e1f-88d2-4fb3-af0d-10d229f93528")).WithObjectTypes(PayGrade, SalaryStep).WithSingularName("SalaryStep")  .WithPluralName("SalarySteps")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // PartyProductCategoryRevenueHistory
            new RelationTypeBuilder(domain, new Guid("045cbf8e-1fef-4d3b-a111-1eaccfceba3b"), new Guid("6b752901-eecb-4f07-bf49-73ef66fd11a8"), new Guid("4c5aa074-4d26-476e-bd2f-ff80e348dec1")).WithObjectTypes(PartyProductCategoryRevenueHistory, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("06ae29c3-7375-47ee-8e0f-9eaa62874adc"), new Guid("d1a9033b-14cd-4bd6-aaea-0edff9c28ac3"), new Guid("ef5995f9-61c8-49cc-afa8-28bea455e573")).WithObjectTypes(PartyProductCategoryRevenueHistory, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4211bfb3-5162-448a-878c-79e107af79e9"), new Guid("3023fbcd-d4ab-4d59-9250-332ed7dd45a3"), new Guid("c3659cb8-de68-401a-b206-a58bdc94dd27")).WithObjectTypes(PartyProductCategoryRevenueHistory, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4e4845c9-9c37-49ec-9b5c-1cd5f247edd4"), new Guid("f311eb9c-0aa9-4a8d-9283-ab8f32760519"), new Guid("bd33990b-5cf6-4e53-85c2-bfc743f1dfb8")).WithObjectTypes(PartyProductCategoryRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("785561cc-1c88-4ac2-811c-4fd304c1c0c1"), new Guid("5c235877-9214-45d6-956b-a7e0d2cdafa1"), new Guid("aec14320-5fa6-405b-b02d-dadef054e952")).WithObjectTypes(PartyProductCategoryRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9154e239-b23b-43f5-b77d-e5bb81c0bcc2"), new Guid("71b45408-43d1-457f-aba1-30c6641fe996"), new Guid("bdc40f4d-2bbe-485f-ae7e-1e8942a339cf")).WithObjectTypes(PartyProductCategoryRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Position
            new RelationTypeBuilder(domain, new Guid("137841cd-fa69-4704-a6e3-cd710c51af43"), new Guid("834d2485-8aac-4e7e-86ad-0b7c5c21b368"), new Guid("7f6522ca-1f5b-4c97-99b4-1f4ac6670d8e")).WithObjectTypes(Position, Organisation).WithSingularName("Organisation")  .WithPluralName("Organisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2806ca00-0b79-45e5-835e-b11f45b05f15"), new Guid("144fcdd3-d66c-4ad3-9c68-a6f8c96afdc5"), new Guid("cc54251d-b913-41f7-ba48-982e5829c0f0")).WithObjectTypes(Position, allorsBoolean).WithSingularName("Temporary")  .WithPluralName("Temporaries")      .Build();
            new RelationTypeBuilder(domain, new Guid("39298cc2-0869-4dc9-b0ff-bea8269ba958"), new Guid("7ca00aff-ad0b-4195-902b-39b3d5cc2c25"), new Guid("949968c0-dc95-44d3-a8f0-65829a884c3b")).WithObjectTypes(Position, allorsDateTime).WithSingularName("EstimatedThroughDate")  .WithPluralName("EstimatedThroughDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("6ede43f7-87a5-429c-8fc0-6441ca8753f1"), new Guid("c2100e41-9586-485c-8110-693de5479a9e"), new Guid("5f2fa20d-f4c8-468e-b9bb-d9a3cd777b70")).WithObjectTypes(Position, allorsDateTime).WithSingularName("EstimatedFromDate")  .WithPluralName("EstimatedFromDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("8166d3b6-cc9d-486a-9321-5cd97ff49ddc"), new Guid("2c5ea5b2-9bea-4181-8c4e-ae903e93c8f8"), new Guid("03b86a68-6063-4299-aa94-ed3f1850f115")).WithObjectTypes(Position, PositionType).WithSingularName("PositionType")  .WithPluralName("PositionTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bf81174e-1105-4313-8d42-4a7b03bfc308"), new Guid("679e6db2-ffd5-47db-a601-624d9f852057"), new Guid("a345fe4c-caa6-4d40-a168-e97d315bc37d")).WithObjectTypes(Position, allorsBoolean).WithSingularName("Fulltime")  .WithPluralName("Fulltimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("cb040fe9-8cdb-4e3a-9a32-e6700f1a8867"), new Guid("0ee703ac-5647-4402-aee8-bfc1eaad2b7c"), new Guid("ec97642c-97dc-423d-b379-e3dce90d0d0d")).WithObjectTypes(Position, allorsBoolean).WithSingularName("Salary")  .WithPluralName("Salaries")      .Build();
            new RelationTypeBuilder(domain, new Guid("db94dd2c-5f39-4f64-ad6d-ce80bf7a4c22"), new Guid("1e391ccb-da94-4b69-8dc7-b0659eaaf201"), new Guid("c3123ffe-f6d2-4d46-87be-77e184ec8adb")).WithObjectTypes(Position, PositionStatus).WithSingularName("PositionStatus")  .WithPluralName("PositionStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e1f8d2a3-83a7-4357-9451-858c314dbefc"), new Guid("5d53246b-9497-476e-b68a-e8e5bea2c851"), new Guid("a026d5da-a2dd-4443-956f-2c6d8c73a894")).WithObjectTypes(Position, BudgetItem).WithSingularName("ApprovedBudgetItem")  .WithPluralName("ApprovedBudgetItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ec8beecc-9e28-4103-94d3-249aed76c934"), new Guid("c68b7794-0379-4542-8f1b-24311e2358a4"), new Guid("3a72f3f0-0476-4629-9831-ed43ebaa8cf5")).WithObjectTypes(Position, allorsDateTime).WithSingularName("ActualFromDate")  .WithPluralName("ActualFromDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("fc328a1a-4f62-42de-96b2-a61c612a1602"), new Guid("f815e446-05a5-4fa3-b3b4-4c7a94b7ca1f"), new Guid("c8cb5709-08d5-4b3b-9598-15289ba9d689")).WithObjectTypes(Position, allorsDateTime).WithSingularName("ActualThroughDate")  .WithPluralName("ActualThroughDates")      .Build();
			
            // LetterCorrespondence
            new RelationTypeBuilder(domain, new Guid("3e0f1be5-0685-48d6-922f-6e971110b414"), new Guid("d063c86e-bbee-41b9-9823-10e96c69c5a0"), new Guid("14ca37a9-7ce4-4d2a-b7ba-1a43bccc1664")).WithObjectTypes(LetterCorrespondence, PostalAddress).WithSingularName("PostalAddress")  .WithPluralName("PostalAddresses")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e8fd2c39-bcb7-4914-8cd3-6dcc6a7a9997"), new Guid("d5ed6948-f657-4d47-89c8-d860e2971138"), new Guid("b65552b5-99c7-4b91-b9b6-a70ec35c3ae2")).WithObjectTypes(LetterCorrespondence, Party).WithSingularName("Originator")  .WithPluralName("Originators")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ece02647-000a-4373-8f01-f4b7d1c75dd5"), new Guid("e580ed8f-a7a4-40c3-9c0a-4cdbe95354a6"), new Guid("dde368dc-c198-4744-b3b2-1a2e0d2976e4")).WithObjectTypes(LetterCorrespondence, Party).WithSingularName("Receiver")  .WithPluralName("Receivers")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // PurchaseOrder
            new RelationTypeBuilder(domain, new Guid("15ea478f-b71d-412f-8ee4-abe554b9a7d8"), new Guid("e48c8211-2539-41ba-9250-27a08799b31b"), new Guid("6ef2d258-4291-4a9f-b7f0-9f154b789775")).WithObjectTypes(PurchaseOrder, PurchaseOrderItem).WithSingularName("PurchaseOrderItem")  .WithPluralName("PurchaseOrderItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1638a432-3a4f-4cca-906e-660b9164838b"), new Guid("04f4151a-1adf-426a-9fb1-a0f8cc782b0e"), new Guid("20131db5-50af-42a8-9ac8-fd250c1aa8b6")).WithObjectTypes(PurchaseOrder, Party).WithSingularName("PreviousTakenViaSupplier")  .WithPluralName("PreviousTakenViaSuppliers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("169ea375-cb46-44b9-a0b8-e0c9c37d6eb5"), new Guid("259aaa98-e60f-4c2e-a08d-ae3e25c44434"), new Guid("c442da88-7371-4305-96e0-a2771502fefc")).WithObjectTypes(PurchaseOrder, PurchaseOrderStatus).WithSingularName("PaymentStatus")  .WithPluralName("PaymentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2a4ce670-53f4-4c31-9e46-96437f4a80e1"), new Guid("82e04803-a492-45ef-b7ce-9eca4f521c51"), new Guid("e52f200c-d14c-442b-a4cc-1670ec47efe1")).WithObjectTypes(PurchaseOrder, PurchaseOrderStatus).WithSingularName("CurrentPaymentStatus")  .WithPluralName("CurrentPaymentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("36607a9e-d411-4726-a63c-7622b928bfe8"), new Guid("a8573588-3898-4422-92a2-056448200216"), new Guid("31a6a1a2-92ee-4ffd-9eb8-d69e8f2183fd")).WithObjectTypes(PurchaseOrder, Party).WithSingularName("TakenViaSupplier")  .WithPluralName("TakenViaSuppliers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3b1f04bd-c9ad-4fca-981c-2ca243fdc292"), new Guid("e50e5e9b-f312-4520-81d1-1dbd7f856d0f"), new Guid("64e08e15-4d4d-465b-9d9e-c9e7c971a56d")).WithObjectTypes(PurchaseOrder, PurchaseOrderObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3edb1f6e-8497-4730-8144-e64f6c8d4446"), new Guid("c5628e77-b2cf-42c0-a583-930731aa8474"), new Guid("4c8c8e44-84d7-450a-bfcc-158e1879b189")).WithObjectTypes(PurchaseOrder, PurchaseOrderStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4830cfc5-0375-4996-8cd8-27e36c102b65"), new Guid("efa439f8-787e-43d7-bd1b-400cba7e3a62"), new Guid("583bfc51-0bb7-4ea5-914c-33a5c2d64196")).WithObjectTypes(PurchaseOrder, ContactMechanism).WithSingularName("TakenViaContactMechanism")  .WithPluralName("TakenViaContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6d0f4867-3237-4c3e-b00b-96f4ad456c55"), new Guid("ac51d7e6-abd1-4d07-9b25-34888b4f830d"), new Guid("e146ab05-15d4-4f29-a89c-c97b5a75d0cf")).WithObjectTypes(PurchaseOrder, PurchaseOrderStatus).WithSingularName("OrderStatus")  .WithPluralName("OrderStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7eceb1b6-1395-4655-a558-6d72ad4b380e"), new Guid("b6e1159c-fcb7-47f1-822b-4ab75e5dac14"), new Guid("ab3ee3c7-dc02-4acf-a34e-6b25783e11fc")).WithObjectTypes(PurchaseOrder, ContactMechanism).WithSingularName("BillToContactMechanism")  .WithPluralName("BillToContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("87a2439e-83e1-4b51-97b8-9c21cc743e07"), new Guid("711e264a-74eb-4229-a4d1-82ddef1bb597"), new Guid("0316aafc-f5b2-41b2-bf39-7c2b7e4e0afc")).WithObjectTypes(PurchaseOrder, PurchaseOrderStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b390a733-e322-4ada-9ead-75a8c9976337"), new Guid("6082b0af-f5ed-493c-bb2b-ad4764053819"), new Guid("c725b348-df8f-4a64-adc2-c3d8b3b986b5")).WithObjectTypes(PurchaseOrder, InternalOrganisation).WithSingularName("ShipToBuyer")  .WithPluralName("ShipToBuyers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cbcf2101-3e38-4c69-b5ea-a6860ea056f0"), new Guid("72fdb437-3f79-454a-9294-883901f7ffd5"), new Guid("9c2cd7fa-6279-4bab-a2d3-c2eaf6ef7cff")).WithObjectTypes(PurchaseOrder, PurchaseOrderStatus).WithSingularName("CurrentOrderStatus")  .WithPluralName("CurrentOrderStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ccf88515-6441-4d0f-a2e7-8f5ed7c0533e"), new Guid("ce230886-53a7-4360-b545-a20d3cf47f1f"), new Guid("2f7e7d1b-6a61-41a6-a05f-375e8a5feeb2")).WithObjectTypes(PurchaseOrder, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d74bd1fd-f243-4b5d-8061-1eafe7c25beb"), new Guid("5465663b-6757-4b1d-9f91-233bfd86bc5d"), new Guid("35c28c9f-852a-4ebb-bc2b-1dce9e3812fa")).WithObjectTypes(PurchaseOrder, PostalAddress).WithSingularName("ShipToAddress")  .WithPluralName("ShipToAdresses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dd73dbd1-aa0e-4a19-a31d-5ebcdafccd45"), new Guid("f824cb43-7bc9-47f2-b754-36dc16c43076"), new Guid("8b52f909-abe4-4e77-85fa-6cf37198f179")).WithObjectTypes(PurchaseOrder, PurchaseOrderObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f05e0ba5-4321-4d88-8f2c-8994de5b44b7"), new Guid("38d76559-6a9c-48c7-bde5-1a2e685b9a40"), new Guid("a0b2ec91-5b7e-4abb-91fb-91836cb88490")).WithObjectTypes(PurchaseOrder, InternalOrganisation).WithSingularName("BillToPurchaser")  .WithPluralName("BillToPurchasers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Quote
            new RelationTypeBuilder(domain, new Guid("033df6dd-fdf7-44e4-84ca-5c7e100cb3f5"), new Guid("4b19f443-0d27-447d-8186-e5361a094460"), new Guid("fa17ef86-c074-414e-b223-b62522d68280")).WithObjectTypes(Quote, allorsDateTime).WithSingularName("ValidFromDate")  .WithPluralName("ValidFromDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("05e3454a-0a7a-488d-b4b1-f0fd41392ddf"), new Guid("ca3f0d26-9ead-4691-8f7f-f79272065251"), new Guid("92e46228-ad44-4b9b-b727-23159a59bca3")).WithObjectTypes(Quote, QuoteTerm).WithSingularName("QuoteTerm")  .WithPluralName("QuoteTerms")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("20880670-0496-4d24-8c97-69b83867c09e"), new Guid("c2bfd7fd-7956-4c28-960e-539f8159e46a"), new Guid("3242cf4f-589c-457b-9ecd-59110041ab34")).WithObjectTypes(Quote, Party).WithSingularName("Issuer")  .WithPluralName("Issuers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2140e106-2ef3-427a-be94-458c2b8e154d"), new Guid("9d81ada4-a4f3-44bb-9098-bc1a3e61de19"), new Guid("60581583-2536-4b09-acae-f0f877169dae")).WithObjectTypes(Quote, allorsDateTime).WithSingularName("ValidThroughDate")  .WithPluralName("ValidThroughDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("3da51ccc-24b9-4b03-9218-7da06492224d"), new Guid("602c70c9-ddc4-4cf5-a79f-0abcc0beba15"), new Guid("d4d93ad0-c59d-40e7-a82c-4fb1e54a85f2")).WithObjectTypes(Quote, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9119c598-cd98-43da-bfdf-1e6573112c9e"), new Guid("d48cd46d-889b-4e2d-a6d6-ee26f30fb3e5"), new Guid("56f5d5ee-1ab5-48f2-a413-7b80dd2c283e")).WithObjectTypes(Quote, Party).WithSingularName("Receiver")  .WithPluralName("Receivers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b5bcf357-ef14-424d-ad8d-01a8e3ff478c"), new Guid("b9338369-9081-4fa7-91c2-140a46ea7d27"), new Guid("984b073d-0213-4539-8d3c-a35a81a71bd5")).WithObjectTypes(Quote, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d7dc81e8-76e7-4c68-9843-a2aaf8293510"), new Guid("6fbc80d1-e72b-4484-a9b1-e606f15d2435"), new Guid("219cb27f-20b5-48b3-9d89-4b119798b092")).WithObjectTypes(Quote, allorsDateTime).WithSingularName("IssueDate")  .WithPluralName("IssueDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("e250154a-77c5-4a0b-ae3d-28668a9037d1"), new Guid("b5ba8cfd-2b16-4a50-89cd-46927d59b97a"), new Guid("f5b6881b-c4d5-42e3-a024-0ae4564cb970")).WithObjectTypes(Quote, QuoteItem).WithSingularName("QuoteItem")  .WithPluralName("QuoteItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e76cbd73-78b7-4ef8-a24c-9ac0db152f7f"), new Guid("057ad29f-c245-44b2-8a95-71bd6607830b"), new Guid("218e3a6e-b530-41f7-a60e-7587f8072c8c")).WithObjectTypes(Quote, allorsString).WithSingularName("QuoteNumber")  .WithPluralName("QuoteNumbers")      .WithSize(256).Build();
			
            // GlBudgetAllocation
            new RelationTypeBuilder(domain, new Guid("b09babba-1379-44fe-9e5f-89ec75c65a9c"), new Guid("9531b256-5424-4c34-9b3c-4348fb1e1672"), new Guid("8908be29-0ab1-446a-b4e0-e251fcb546d2")).WithObjectTypes(GlBudgetAllocation, GeneralLedgerAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dddccd24-864c-48bb-b1ac-35b8a201cd65"), new Guid("babbcdc0-7d4b-4679-a937-cbf6f5632c8b"), new Guid("2ee2162b-6936-4322-8d5b-1175d29f1308")).WithObjectTypes(GlBudgetAllocation, BudgetItem).WithSingularName("BudgetItem")  .WithPluralName("BudgetItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("eb1e7e03-8b88-4a69-b1cc-46dc77b44a8b"), new Guid("cd06b83b-3b19-4b6d-bed8-90c5ece3c600"), new Guid("fb5e417d-b1b1-4e23-90d4-01b0464e3a1b")).WithObjectTypes(GlBudgetAllocation, allorsDecimal).WithSingularName("AllocationPercentage")  .WithPluralName("AllocationPercentages")      .WithPrecision(19).WithScale(2).Build();
			
            // PartyRelationship
            new RelationTypeBuilder(domain, new Guid("1da069bb-5e29-49e0-93a8-b869a7f2d61a"), new Guid("5a6aeb4c-b76a-4044-b43a-2d226225bac1"), new Guid("86b546d6-d286-4048-bf5c-2de020b39690")).WithObjectTypes(PartyRelationship, PartyRelationshipStatus).WithSingularName("PartyRelationshipStatus")  .WithPluralName("PartyRelationshipStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("296628f5-ad97-47b0-8865-6221a43f45e9"), new Guid("c04bb236-5c81-445d-a363-125002b01cea"), new Guid("0a2a7b62-da4d-4099-b89e-7bf451ed9009")).WithObjectTypes(PartyRelationship, Agreement).WithSingularName("Agreement")  .WithPluralName("Agreements")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("37273d64-63d7-4878-a5c0-1d4a834ebc9f"), new Guid("72d44569-cb28-42a4-8b70-a3298ef36dfd"), new Guid("426a2ea4-2335-4a9c-8e1d-3258c4f58639")).WithObjectTypes(PartyRelationship, PartyRelationshipPriority).WithSingularName("PartyRelationshipPriority")  .WithPluralName("PartyRelationshipPriorities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9a0effb1-eff9-402a-9864-470569be9e7b"), new Guid("fbb79a3b-013d-4ec9-93ae-3b80b759f6eb"), new Guid("f4e6833d-da52-484e-8c6f-b1f55730178e")).WithObjectTypes(PartyRelationship, allorsDecimal).WithSingularName("SimpleMovingAverage")  .WithPluralName("SimpleMovingAverages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("9dc11de2-ddf4-4f38-8cbe-776d9fad599d"), new Guid("f4b2e224-e024-471e-b145-4d2f819d7e8b"), new Guid("f2d86632-03c8-4724-bf11-3aad09bea789")).WithObjectTypes(PartyRelationship, CommunicationEvent).WithSingularName("CommunicationEvent")  .WithPluralName("CommunicationEvents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // Brand
            new RelationTypeBuilder(domain, new Guid("08b0dfc6-2e2f-4e40-96d1-851e26b38e8d"), new Guid("8b22eb1a-ecc3-4e0d-898c-4a5c651f1d2c"), new Guid("2f85b937-569a-4317-ac4a-aa1e89541a20")).WithObjectTypes(Brand, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("25729ffa-4f97-464a-9b34-fe1661e0d932"), new Guid("ae59e80c-289d-487f-996c-c83615d8750d"), new Guid("f02d54d1-8ed9-4f63-96b5-397cb4b761d2")).WithObjectTypes(Brand, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // SupplierOffering
            new RelationTypeBuilder(domain, new Guid("44e38ad4-833c-4da9-894d-bbe57d0f784e"), new Guid("c5769d37-d236-4ab6-9cab-dcc861dfbade"), new Guid("68ab327e-4ad4-460a-8b9f-f740a19670e0")).WithObjectTypes(SupplierOffering, RatingType).WithSingularName("Rating")  .WithPluralName("Rating")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("74895df9-e416-41cb-ab36-24694dc63334"), new Guid("b81877b2-f7cd-4951-b02e-e60722ca0d72"), new Guid("80326eaa-5546-490e-b433-9ff57f42f85e")).WithObjectTypes(SupplierOffering, allorsInteger).WithSingularName("StandardLeadTime")  .WithPluralName("StandardLeadTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("806da6e8-b58d-46cf-b703-7e67aa7dfcf9"), new Guid("05a12a65-920d-4d6e-9490-1a5d8ae651c3"), new Guid("ce335230-1191-484e-93f1-8bf0533090d4")).WithObjectTypes(SupplierOffering, ProductPurchasePrice).WithSingularName("ProductPurchasePrice")  .WithPluralName("ProductPurchasePrices")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9c3458aa-7062-4c4c-9160-2f978b088082"), new Guid("2efde592-4a60-4c79-bc20-f389c5df5966"), new Guid("99b85157-6b6a-4556-a910-af955802b6da")).WithObjectTypes(SupplierOffering, Ordinal).WithSingularName("Preference")  .WithPluralName("Preferences")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b4cdcc85-583a-49e7-ba35-8985936c7f64"), new Guid("2133d78d-9f26-46bf-b706-e01e032402df"), new Guid("12dd7fcb-0777-43a6-9524-b2b79c92c40c")).WithObjectTypes(SupplierOffering, allorsDecimal).WithSingularName("MinimalOrderQuantity")  .WithPluralName("MinimalOrderQuantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("cd1ce1c1-222f-461b-8d9c-7d58f997d129"), new Guid("f719728f-7def-44d7-8c68-0996f3834887"), new Guid("9204493f-f47d-4fb6-bef7-de91fb2cd53f")).WithObjectTypes(SupplierOffering, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d2de1e9e-196f-43d7-903e-566a4858bc02"), new Guid("a78c953d-0feb-463a-a7c6-e00640db9e44"), new Guid("4dfd5ba4-ebdf-4ea1-b4d4-ecff642525cb")).WithObjectTypes(SupplierOffering, Party).WithSingularName("Supplier")  .WithPluralName("Suppliers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d741765d-d17e-4e6a-88fd-9eee70c82bcf"), new Guid("3e237d3b-6d44-4afd-a248-f9d15e7822d7"), new Guid("79affcb8-28b2-4629-a918-c863089f1dbc")).WithObjectTypes(SupplierOffering, allorsString).WithSingularName("ReferenceNumber")  .WithPluralName("ReferenceNumber")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ea5e3f12-417c-40c4-97e0-d8c7dd41300c"), new Guid("ba708825-f930-445c-8eaf-29221a405edf"), new Guid("b43787c4-8d38-425a-ab87-b5d3b80f9a5d")).WithObjectTypes(SupplierOffering, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesAccountingTransaction
            new RelationTypeBuilder(domain, new Guid("9b376e18-7cf8-43f7-ac89-ef4b32a1c8fd"), new Guid("ee71978e-2085-48d2-81ad-571cfcec8264"), new Guid("3fe3be8d-563d-4455-8f1a-7771ff97005f")).WithObjectTypes(SalesAccountingTransaction, Invoice).WithSingularName("Invoice")  .WithPluralName("Invoices")    .WithIsIndexed(true)  .Build();
			
            // AccountingTransactionNumber
            new RelationTypeBuilder(domain, new Guid("1a7eda6e-7b1c-4faf-8635-05bc233c5dd8"), new Guid("ad6df638-67d2-4d41-9695-01c6adf3f251"), new Guid("e2178198-6bbd-4caa-9402-40137b2bd529")).WithObjectTypes(AccountingTransactionNumber, allorsLong).WithSingularName("Number")  .WithPluralName("Numbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("e01605b0-ba04-4775-ad15-cac1281cec9e"), new Guid("75523554-b713-433b-8916-c70278649b52"), new Guid("dcaeed3f-1bbe-40a0-aacc-9c26db3f984f")).WithObjectTypes(AccountingTransactionNumber, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("f3bcec3b-b08e-4eab-812e-bb5b31fe6a4d"), new Guid("e447318b-ab6a-4259-8fb0-fb0ae81b15f7"), new Guid("018e866e-3593-45d2-a10e-45bd79a0faeb")).WithObjectTypes(AccountingTransactionNumber, AccountingTransactionType).WithSingularName("AccountingTransactionType")  .WithPluralName("AccountingTransactionTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // WorkEffortPartyAssignment
            new RelationTypeBuilder(domain, new Guid("2723be72-6775-4f39-9bf6-e95abc2c0b24"), new Guid("9014f6ec-c005-43ab-861c-b150474b9dca"), new Guid("ea3986d9-81c8-4353-bc13-7d03255ed9f8")).WithObjectTypes(WorkEffortPartyAssignment, WorkEffort).WithSingularName("Assignment")  .WithPluralName("Assignments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("92081ae5-3e2a-4b13-98b8-0fc45403b877"), new Guid("2b11931c-a007-4fec-ab78-ecc8388b2a77"), new Guid("90617602-f9c1-4f71-bc6c-c5e4987d008f")).WithObjectTypes(WorkEffortPartyAssignment, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f88ae79d-7605-4be9-972e-541489bdb72b"), new Guid("b527fed9-c720-45aa-b8c5-fe5336f43f5c"), new Guid("92716fbf-aae3-433f-85f1-9fcfeb68568c")).WithObjectTypes(WorkEffortPartyAssignment, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Training
            new RelationTypeBuilder(domain, new Guid("7d2e7956-fb60-4a1b-8e5f-ee88b1b8e3b7"), new Guid("ff4c2753-ce42-4aa8-b1b1-3486e6aa11d9"), new Guid("ee47ec51-a1d0-4d12-97cc-5a089869caa6")).WithObjectTypes(Training, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // CostCenterCategory
            new RelationTypeBuilder(domain, new Guid("15eade6f-f540-4916-9d66-30f4bd0f260a"), new Guid("f67c26e6-73e2-490a-aaf5-b66cd8e30972"), new Guid("b0767ddc-1b97-4289-afec-0519182982d0")).WithObjectTypes(CostCenterCategory, CostCenterCategory).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("45b0b049-e047-4490-9dde-c48fb1e7bfc3"), new Guid("130462ef-9d1d-48d9-b0f5-40c82ccea0a2"), new Guid("1fd21431-34f6-4f5c-ad54-abecb5e717e1")).WithObjectTypes(CostCenterCategory, CostCenterCategory).WithSingularName("Ancestor")  .WithPluralName("Ancestors")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b20dc3d5-5067-4697-becf-0e8d44f117c7"), new Guid("d88647c8-b367-48e0-aef9-2af923a17b6f"), new Guid("0a65a2da-f091-4ed1-9af9-80ff63123adf")).WithObjectTypes(CostCenterCategory, CostCenterCategory).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fcb56761-342b-4d62-ba5b-27e0a0f405dd"), new Guid("4804ef05-ddb6-4f15-940a-cd663a7bef55"), new Guid("c1d56a33-314d-4aa7-a202-77ae675092ab")).WithObjectTypes(CostCenterCategory, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // JournalEntry
            new RelationTypeBuilder(domain, new Guid("09202ffd-6b78-455b-a140-a354a771d761"), new Guid("f4b83ae6-6dc6-495a-8081-a5137434bc7f"), new Guid("0dda1ff5-1420-454b-ad8d-be6e5ee68b91")).WithObjectTypes(JournalEntry, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("1452d159-857a-4fff-b1d6-6d27772e54bc"), new Guid("eb122f1d-8615-4342-8beb-2a197677947a"), new Guid("bf8485f3-d5dd-4236-b542-61674c2298db")).WithObjectTypes(JournalEntry, allorsLong).WithSingularName("EntryNumber")  .WithPluralName("EntryNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("1b5f8acd-872d-498e-9c2d-ded4b7d31efe"), new Guid("9bb9541a-f0fc-4ed8-bc3f-13e1d7901395"), new Guid("0b9dd5eb-10a1-470b-a119-158c66c558f1")).WithObjectTypes(JournalEntry, allorsDateTime).WithSingularName("EntryDate")  .WithPluralName("EntryDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("4eca8284-cc27-4440-8b5f-adeffd3b078b"), new Guid("b7897efa-b2f5-4807-8385-3da4936998c7"), new Guid("f8bcd82b-5209-45d1-a6ee-5452ca9cf11b")).WithObjectTypes(JournalEntry, allorsDateTime).WithSingularName("JournalDate")  .WithPluralName("JournalDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("e81fe73b-1486-4a9d-ab2b-2d49dfcbb777"), new Guid("77a3f9d6-814b-438e-b424-e63763bb4213"), new Guid("9afbe2d8-116b-4e35-bbb5-e35085697b30")).WithObjectTypes(JournalEntry, JournalEntryDetail).WithSingularName("JournalEntryDetail")  .WithPluralName("JournalEntryDetails")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // WorkEffortBilling
            new RelationTypeBuilder(domain, new Guid("3c83ca1d-b20e-4e8c-aa23-3bb03f421ba7"), new Guid("506b220c-7965-4d51-8413-feabfef71c07"), new Guid("4d2f7ed8-881f-49e4-944a-ba291ec671d0")).WithObjectTypes(WorkEffortBilling, WorkEffort).WithSingularName("WorkEffort")  .WithPluralName("WorkEfforts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("91d38ce9-bf06-4272-bdd8-13401084223d"), new Guid("d0189269-2f90-46c5-a1ff-48bad8712b34"), new Guid("e2a7d998-78bb-4d21-b4a8-d6fbddc4b089")).WithObjectTypes(WorkEffortBilling, allorsDecimal).WithSingularName("Percentage")  .WithPluralName("Percentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c6ed6a42-6889-4ad9-b76a-22bd45e02e75"), new Guid("99eb5187-9c6b-48bf-a587-81a5d1603cb1"), new Guid("977e55a2-1592-42ff-b7a2-9f1630b36714")).WithObjectTypes(WorkEffortBilling, InvoiceItem).WithSingularName("InvoiceItem")  .WithPluralName("InvoiceItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PurchaseReturnStatus
            new RelationTypeBuilder(domain, new Guid("1485de84-2719-4543-9250-807f1a9e60bf"), new Guid("9694eb68-d7c4-40db-b15d-deacb698e976"), new Guid("48bd4dce-ecae-4acf-b820-70352958c04b")).WithObjectTypes(PurchaseReturnStatus, PurchaseReturnObjectState).WithSingularName("PurchaseReturnObjectState")  .WithPluralName("PurchaseReturnObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("46004f49-9d2e-49a6-870e-c81f44458c59"), new Guid("3b2a6ce5-a5c8-45c3-ba3b-3fa5326e4293"), new Guid("bd72c728-57ef-4e68-9bd1-2517bfe7e972")).WithObjectTypes(PurchaseReturnStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // PartyRevenueHistory
            new RelationTypeBuilder(domain, new Guid("00cc3ce5-d0d8-4082-a1af-8ee850bc76b3"), new Guid("dc4ec595-5121-4b69-bb2b-203d13ffdf75"), new Guid("b2bff84f-9e85-4b2d-bcb5-3860378aa6a0")).WithObjectTypes(PartyRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("415b03ac-658e-4a1d-b137-ca45ddb89943"), new Guid("2c225add-0f0a-4269-9ea2-cb89b808050a"), new Guid("e3030338-fd1f-4db2-8a94-39fc9b0597b8")).WithObjectTypes(PartyRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("63cba080-c950-4956-99ee-e380d482a272"), new Guid("fe3a4195-64ac-4ee7-9c27-07cb125489a5"), new Guid("3dbf35cc-5e84-40b0-bac5-c388a8e9b241")).WithObjectTypes(PartyRevenueHistory, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c4610b89-4e21-4e03-a1d5-7487dce9ad42"), new Guid("0ebd138c-0a0f-4a6e-b792-0cc8e88a614e"), new Guid("cacac289-9986-49a5-a0da-764af0dd5a9c")).WithObjectTypes(PartyRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PositionTypeRate
            new RelationTypeBuilder(domain, new Guid("ab942018-51fd-4135-9005-c81443b72a96"), new Guid("c35de10d-2f22-4be8-b1e0-6a0e8e3b0922"), new Guid("a443b5af-ae94-4a8b-9c56-2bd9459d9fd8")).WithObjectTypes(PositionTypeRate, allorsDecimal).WithSingularName("Rate")  .WithPluralName("Rates")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c49a44b8-dff1-471c-8309-cf9c7e9188c2"), new Guid("7de3e158-9900-40c4-a015-c62947c0248a"), new Guid("651d72f5-61af-4800-af6a-704159998bfa")).WithObjectTypes(PositionTypeRate, RateType).WithSingularName("RateType")  .WithPluralName("RateTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f49e4e9e-2e8f-49f6-9c10-4aefb4bb61bf"), new Guid("6f36fb29-7820-45fa-9dca-888c11d8b0a3"), new Guid("135731d2-4120-45dd-b36c-36c8c93ea99e")).WithObjectTypes(PositionTypeRate, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // GeneralLedgerAccount
            new RelationTypeBuilder(domain, new Guid("0144834d-c5a9-42e7-bf22-af46ff95ee5f"), new Guid("01f0ef35-7ecd-44a9-9366-bad1c213246c"), new Guid("76129210-c674-43d8-9864-39ac497f7a48")).WithObjectTypes(GeneralLedgerAccount, Product).WithSingularName("DefaultCostUnit")  .WithPluralName("DefaultCostUnits")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("01c49e6f-087a-494d-902d-12811442470e"), new Guid("839d77a1-7307-4aeb-921b-9aa8832ef853"), new Guid("6a616950-edba-4ed1-8419-7ed4ab3a8fcd")).WithObjectTypes(GeneralLedgerAccount, CostCenter).WithSingularName("DefaultCostCenter")  .WithPluralName("DefaultCostCenters")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("08bb53f7-9b27-4079-bb9b-d8ff96f89b42"), new Guid("032959df-bd6a-4043-9703-1ed9ce1ca0ee"), new Guid("671e880a-e52a-45ed-8712-c0b6280de422")).WithObjectTypes(GeneralLedgerAccount, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("27ba2d5b-9e0b-4b20-9b34-f007a0f2e2f2"), new Guid("b05953d9-4d24-4fc6-8ee6-eeda14d519ca"), new Guid("c6cb1d95-1734-4d37-ad1c-d4cb19546b03")).WithObjectTypes(GeneralLedgerAccount, GeneralLedgerAccountType).WithSingularName("GeneralLedgerAccountType")  .WithPluralName("GeneralLedgerAccountTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2e6545f8-5fcf-4129-99f6-1f41280cd02d"), new Guid("559a7346-4ec8-449c-ae3a-2e9360933196"), new Guid("62f62635-b19e-4985-98f7-55c677badb26")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("CashAccount")  .WithPluralName("CashAccounts")      .Build();
            new RelationTypeBuilder(domain, new Guid("3fc28997-124c-4e16-9c4d-128314e6395c"), new Guid("f42c835a-4325-4d25-a2e4-ea621d9bab6b"), new Guid("c8224cdd-85c3-4dd7-85cc-3eee6b0ee010")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("CostCenterAccount")  .WithPluralName("CostCenterAccounts")      .Build();
            new RelationTypeBuilder(domain, new Guid("4877e61b-443f-4bef-820f-5c93f8d42b8a"), new Guid("6f44b333-9270-4215-842e-12520d4fc5f6"), new Guid("e48f4a4a-bf66-49b2-b457-76b48e3cab21")).WithObjectTypes(GeneralLedgerAccount, DebitCreditConstant).WithSingularName("Side")  .WithPluralName("Sides")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5f797e0d-05aa-4dfb-a826-157ac6cdb0a9"), new Guid("b7e05caa-992f-4676-ab9d-f3ba8d678032"), new Guid("674a2d4f-62b2-41ec-8de8-b1fad7d05686")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("BalanceSheetAccount")  .WithPluralName("BalanceSheetAccounts")      .Build();
            new RelationTypeBuilder(domain, new Guid("7f2e28ea-124a-45fa-9ed3-e3c2b0bb1822"), new Guid("c5ad5e4e-9437-4838-8b23-88a72a3f51f0"), new Guid("1a860600-659d-410e-91c7-21682ba9002c")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("ReconciliationAccount")  .WithPluralName("ReconciliationsAccount")      .Build();
            new RelationTypeBuilder(domain, new Guid("8616e916-a3e2-4cfe-84a4-778fd4b50d87"), new Guid("ef7a784f-0f35-4973-b8d0-e732e3a9a741"), new Guid("d758a087-f381-4a5c-ab9f-e433ba786166")).WithObjectTypes(GeneralLedgerAccount, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9b679f99-d678-4ec0-8ab1-e02eaabe6658"), new Guid("bd544bfa-d073-4534-8c11-de070571f5cb"), new Guid("8a1191c3-3ef7-4932-b6b0-d52f2eb604fb")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("CostCenterRequired")  .WithPluralName("CostCenterRequireds")      .Build();
            new RelationTypeBuilder(domain, new Guid("a3aa445f-2aae-41be-8024-7b4a7e0a76ed"), new Guid("eae74634-5d98-482f-bfc5-c81e9731c2e0"), new Guid("5d60a6f0-eb91-4048-99fd-285c9685c555")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("CostUnitRequired")  .WithPluralName("CostUnitsRequired")      .Build();
            new RelationTypeBuilder(domain, new Guid("aa569c0a-597d-4b75-a527-25c6ef339547"), new Guid("73a33f92-462b-46c6-83d1-41d6f170aaee"), new Guid("b32aa8cb-b590-4302-8a30-66eacd2ec4f7")).WithObjectTypes(GeneralLedgerAccount, GeneralLedgerAccountGroup).WithSingularName("GeneralLedgerAccountGroup")  .WithPluralName("GeneralLedgerAccountGroups")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("beda5c75-e1a0-493a-85ec-a943214cec8d"), new Guid("319a12b6-4ee9-4bbd-9026-480b02e71255"), new Guid("be03ce0d-4023-4d62-8f2e-918e46d1f097")).WithObjectTypes(GeneralLedgerAccount, CostCenter).WithSingularName("CostCenterAllowed")  .WithPluralName("CostCentersAllowed")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bfe446ee-f9ff-462f-bb45-9bf52d61daa4"), new Guid("dca53c27-9157-4f61-a3e5-eac272b764cd"), new Guid("e1b47d11-4212-4594-b405-3eff0ba3ef82")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("CostUnitAccount")  .WithPluralName("CostUnitsAccount")      .Build();
            new RelationTypeBuilder(domain, new Guid("cedccf34-0386-4be3-aa77-6ec0a9032c15"), new Guid("06fdbd7c-2693-4f23-ab53-965bb40aa79c"), new Guid("1ed39598-1583-401d-8a8e-4a34bf342001")).WithObjectTypes(GeneralLedgerAccount, allorsString).WithSingularName("AccountNumber")  .WithPluralName("AccountNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d2078f49-9745-48e5-bdd2-7d7738f25d4e"), new Guid("276120e0-5e26-415c-bdc0-b1da8790d7f5"), new Guid("42f7e330-3be1-4eb7-8649-289c3907fb8f")).WithObjectTypes(GeneralLedgerAccount, Product).WithSingularName("CostUnitAllowed")  .WithPluralName("CostUnitsAllowed")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e433abed-8f41-4a23-8e5b-e597bb6a14d2"), new Guid("9d56bae8-0cf9-4a5f-81db-a9fcc6aea183"), new Guid("da4c5d7b-2f0b-4c18-8f6a-850064c81b9e")).WithObjectTypes(GeneralLedgerAccount, allorsBoolean).WithSingularName("Protected")  .WithPluralName("Protecteds")      .Build();
			
            // ShippingAndHandlingComponent
            new RelationTypeBuilder(domain, new Guid("0021e1ff-bfc3-4d0b-8168-a8f5789121f7"), new Guid("f1c6cb2b-7c7a-4ca5-b594-152238131cb2"), new Guid("09d4c34a-b5b8-490c-85e3-00470bb8270e")).WithObjectTypes(ShippingAndHandlingComponent, allorsDecimal).WithSingularName("Cost")  .WithPluralName("Costs")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4dfb4bda-1add-45d5-92c7-6393186301f0"), new Guid("44088ee8-b84a-494c-a59c-3164c511176c"), new Guid("eac922da-3beb-41b2-a3ca-f91120f927bd")).WithObjectTypes(ShippingAndHandlingComponent, ShipmentMethod).WithSingularName("ShipmentMethod")  .WithPluralName("ShipmentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a029fb4c-4f80-4216-8fc9-9d9b44997816"), new Guid("9e7b4c12-5168-4fe3-adaf-f8d14f7be01f"), new Guid("b0e26bbb-aef7-40ca-9a64-d78bc02affb9")).WithObjectTypes(ShippingAndHandlingComponent, Carrier).WithSingularName("Carrier")  .WithPluralName("Carriers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ab4377d4-69c6-4b0c-b9d4-e3a01c1a6a94"), new Guid("2ad57105-a5c4-4e7f-a6df-79af9cddf9ca"), new Guid("742dcf46-5fa5-44b4-bf02-582681b0f6aa")).WithObjectTypes(ShippingAndHandlingComponent, ShipmentValue).WithSingularName("ShipmentValue")  .WithPluralName("ShipmentValues")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("df4727ab-29a8-448c-97b4-c16033e03dcf"), new Guid("a57b1bd3-a060-41c1-91bd-ecc428dd9b55"), new Guid("a554ced2-84e0-41bb-97d9-d0b04ef56679")).WithObjectTypes(ShippingAndHandlingComponent, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("eb0b4419-e09c-43c6-a221-27e54872026e"), new Guid("699e4c3c-5cb2-44ab-8e8c-187d0006e4e9"), new Guid("6c6e0c2f-6475-415d-b916-c4489f7f4fc5")).WithObjectTypes(ShippingAndHandlingComponent, InternalOrganisation).WithSingularName("SpecifiedFor")  .WithPluralName("SpecifiedFors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f2bfd9d5-01b2-4bec-8dc2-018cc2187037"), new Guid("cf282301-2e6c-43ed-8447-cc09edcb9810"), new Guid("a3ee85b7-be6a-4e2b-a15d-57872bb57783")).WithObjectTypes(ShippingAndHandlingComponent, GeographicBoundary).WithSingularName("GeographicBoundary")  .WithPluralName("GeographicBoundaries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ReceiptAccountingTransaction
            new RelationTypeBuilder(domain, new Guid("a69440e0-34f3-42ce-9f21-38ea22c5762e"), new Guid("0d841c4a-1f7b-443d-95a6-29a1205f203c"), new Guid("52a40ec0-a108-491a-9f41-94885fcb09b5")).WithObjectTypes(ReceiptAccountingTransaction, Receipt).WithSingularName("Receipt")  .WithPluralName("Receipts")    .WithIsIndexed(true)  .Build();
			
            // PackagingContent
            new RelationTypeBuilder(domain, new Guid("316a8ff4-1073-486e-ad62-5bee3d3504d2"), new Guid("c2970739-17c4-488a-8f12-9e35ad72d311"), new Guid("536e372d-5062-418a-b17e-752ebf32d430")).WithObjectTypes(PackagingContent, ShipmentItem).WithSingularName("ShipmentItem")  .WithPluralName("ShipmentItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ca8bcf75-c40e-4d73-8d0c-f35d1005f73b"), new Guid("a97a1fd4-6d74-424c-aab4-909bdd198856"), new Guid("db47bbd5-e9d8-4dea-801f-bae1c49fe67c")).WithObjectTypes(PackagingContent, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
			
            // PartySkill
            new RelationTypeBuilder(domain, new Guid("3254f43d-7b3a-49c8-8c1b-19fa0e4f6901"), new Guid("a25f511d-a4f9-4360-9150-304ed62d411f"), new Guid("a6c023ba-549c-4895-bd32-ed70f05ef121")).WithObjectTypes(PartySkill, allorsDecimal).WithSingularName("YearsExperience")  .WithPluralName("YearsExperiences")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7ed819c8-78ef-4fe3-b499-b381c246711f"), new Guid("4a88ee23-2c4a-41d2-9891-77e5086db97d"), new Guid("ecb7eb99-dc8f-4ca0-9744-fb87a708430a")).WithObjectTypes(PartySkill, allorsDateTime).WithSingularName("StartedUsingDate")  .WithPluralName("StartedUsingDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("a341478c-503c-49ee-8c9a-e85b777e9ff4"), new Guid("0a9d48d6-e307-461d-b30f-14deae3d5bd8"), new Guid("cd88bbe3-aa6a-4051-a800-57e685d85587")).WithObjectTypes(PartySkill, SkillRating).WithSingularName("SkillRating")  .WithPluralName("SkillRatings")      .Build();
            new RelationTypeBuilder(domain, new Guid("eb3e02dc-6ee5-4aca-9f35-68edafed6dd2"), new Guid("9223e489-7115-4765-88fd-b18f0d7e8c28"), new Guid("5d9c639a-7c94-4771-ad37-be6e4b68dd06")).WithObjectTypes(PartySkill, SkillLevel).WithSingularName("SkillLevel")  .WithPluralName("SkillLevels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fec11de5-a33c-4dd7-9af9-b32c3889c8a3"), new Guid("9c16c4b8-b80f-478f-96b0-a534f9de5663"), new Guid("9728a273-f8d7-4edd-94ff-7a91d178fe82")).WithObjectTypes(PartySkill, Skill).WithSingularName("Skill")  .WithPluralName("Skills")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Document
            new RelationTypeBuilder(domain, new Guid("484d082e-b3e4-4915-a355-43315f466e6d"), new Guid("47509fbb-ba47-4ca7-8689-51b9c5b46746"), new Guid("b8a938cf-302c-4aa0-8e1c-e23752dee601")).WithObjectTypes(Document, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6f6ef875-2b0b-4a03-8b2e-bf242b48c843"), new Guid("de6d7bb7-0e38-4ed0-b881-8c1cf99dc101"), new Guid("0d58ab92-ffed-4c8b-942e-f9b1780d150f")).WithObjectTypes(Document, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("d579e6e7-6791-4b9b-bf20-43ab1a701866"), new Guid("771bee4a-2e75-4826-8b15-43bfa140830b"), new Guid("feeb5f20-6b25-40df-b2e0-d6c21753ea8a")).WithObjectTypes(Document, allorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("e97710fe-8def-44a8-8516-18d4eae8433b"), new Guid("e73dc13b-9f24-4f09-8039-52fbddb54664"), new Guid("9388eba9-d3b7-4c8a-8e03-d8d2d31b476b")).WithObjectTypes(Document, allorsString).WithSingularName("DocumentLocation")  .WithPluralName("DocumentLocations")      .WithSize(256).Build();
			
            // SerializedInventoryItemStatus
            new RelationTypeBuilder(domain, new Guid("aabb931a-38ee-4568-af8c-5f8ed98ed7b9"), new Guid("85598163-c71c-4bdc-942b-5ad461943e01"), new Guid("87e945cc-864b-42b6-ad9b-c3d447d96073")).WithObjectTypes(SerializedInventoryItemStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("d2c2fff8-73ec-4748-9c8f-29304abbdb0d"), new Guid("ee25cfd7-7389-4db7-9bb2-ee388e57f6d1"), new Guid("584017a5-99b5-414a-b32e-c64f7f2a0d4e")).WithObjectTypes(SerializedInventoryItemStatus, SerializedInventoryItemObjectState).WithSingularName("SerializedInventoryItemObjectState")  .WithPluralName("SerializedInventoryItemObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // FaxCommunication
            new RelationTypeBuilder(domain, new Guid("3c4bea84-e00e-4ab3-8d40-5de7f394e835"), new Guid("30a33d23-6c06-45cc-8cef-25a2d02cfc5f"), new Guid("c3ad4d30-c9ef-41da-b7de-f71c625b8549")).WithObjectTypes(FaxCommunication, Party).WithSingularName("Originator")  .WithPluralName("Originators")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("79ec572e-b4a2-4a33-90c3-65c9f9e4012c"), new Guid("2a477a7f-bc36-437c-97df-dfca39236eb5"), new Guid("2e213178-fe72-4258-a8f5-ff926f8e5591")).WithObjectTypes(FaxCommunication, Party).WithSingularName("Receiver")  .WithPluralName("Receivers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8797fd5b-0d89-420f-b656-aff35b50e75c"), new Guid("42e2cb18-3596-443c-876c-3e557189ef2a"), new Guid("7c820d65-87d3-4be3-be2e-8fa6a8b13a97")).WithObjectTypes(FaxCommunication, TelecommunicationsNumber).WithSingularName("OutgoingFaxNumber")  .WithPluralName("OutgoingFaxNumbers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PurchaseInvoiceItem
            new RelationTypeBuilder(domain, new Guid("40e7bdfa-4c3e-420c-a705-70fdc822e9e9"), new Guid("5a23a9e8-afc3-4cc5-91c8-cc98b7ebf8a9"), new Guid("8980c5f1-7cda-4486-b203-4e82e039aab6")).WithObjectTypes(PurchaseInvoiceItem, PurchaseInvoiceItemObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("56e47122-faaa-4211-806c-1c19695fe434"), new Guid("826db2b1-3048-4237-8e83-0c472a166d49"), new Guid("893de8bc-93eb-4864-89ba-efdb66b32fd5")).WithObjectTypes(PurchaseInvoiceItem, PurchaseInvoiceItemType).WithSingularName("PurchaseInvoiceItemType")  .WithPluralName("PurchaseInvoiceItemTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("65eebcc4-d5ef-4933-8640-973b67c65127"), new Guid("40703e06-25f8-425d-aa95-3c73fafbfa81"), new Guid("05f86785-08d8-4282-9734-6230e807181b")).WithObjectTypes(PurchaseInvoiceItem, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("99b3395c-bb6a-4a4f-b22f-900e76e22520"), new Guid("7ed44fc9-fc12-4a68-8938-1573ec28da2f"), new Guid("7b17b8a8-fda9-4707-a7a3-e263b51bcd4f")).WithObjectTypes(PurchaseInvoiceItem, PurchaseInvoiceItemStatus).WithSingularName("CurrentInvoiceItemStatus")  .WithPluralName("CurrentInvoiceItemStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c850d9db-682d-4a05-b62e-ab67eb19bd0d"), new Guid("b79e9e5b-f4a5-4bd0-bc46-ab55eea2f027"), new Guid("136970e5-2ec7-4036-9abc-c84747d59d54")).WithObjectTypes(PurchaseInvoiceItem, PurchaseInvoiceItemStatus).WithSingularName("InvoiceItemStatus")  .WithPluralName("InvoiceItemStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dbe5c72f-63e0-47a5-a5f5-f8a3ff83fd57"), new Guid("f8082d94-30fa-4a58-8bb0-bc5bb4f045ef"), new Guid("69360188-077f-49f0-ba88-abb1f546d72c")).WithObjectTypes(PurchaseInvoiceItem, PurchaseInvoiceItemObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // OrderItemBilling
            new RelationTypeBuilder(domain, new Guid("214988fc-b5a2-4944-9c83-93a645a96853"), new Guid("2007bddd-e78c-40a8-9015-5d3f027586c0"), new Guid("624c3c0b-faac-4542-aeb2-466952cbf832")).WithObjectTypes(OrderItemBilling, OrderItem).WithSingularName("OrderItem")  .WithPluralName("OrderItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("23a0d52d-3ec7-4ddf-a300-c0ee46edf41a"), new Guid("03ac8386-6706-4e3f-9ad2-a64e67edf08f"), new Guid("61e3ad81-395e-46e9-837f-e48257141164")).WithObjectTypes(OrderItemBilling, SalesInvoiceItem).WithSingularName("SalesInvoiceItem")  .WithPluralName("SalesInvoiceItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2f75bdee-46f9-4dd0-b349-00a497462fdb"), new Guid("86dc1660-7719-4dae-93d8-ce4ca7a00f2a"), new Guid("bc0b7bb6-c77b-451d-bf95-c32967766c49")).WithObjectTypes(OrderItemBilling, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("cfff23f0-1f3c-48a1-b4a7-85bc2254dbff"), new Guid("ed09cee4-3c01-4a2a-ab3d-6f9e8de16466"), new Guid("6193e84f-882a-4b7a-b51c-8ec93f09aff2")).WithObjectTypes(OrderItemBilling, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
			
            // PayHistory
            new RelationTypeBuilder(domain, new Guid("2f14e234-c808-4059-bb29-48e6d9493b7b"), new Guid("e44919c4-eac2-4be0-a244-b2cacdf1c4c4"), new Guid("0441ebf6-3607-44e7-98ca-831e146cf9d7")).WithObjectTypes(PayHistory, Employment).WithSingularName("Employment")  .WithPluralName("Employments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6d26369b-eea2-4712-a7d1-56884a3cc715"), new Guid("6e23ddf7-9766-4f56-bd4f-587bb6f00e00"), new Guid("9d1f6129-281c-413d-ba78-fdb99c84a8b2")).WithObjectTypes(PayHistory, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b3f1071f-7e71-4ef1-aa9b-545ad694f44c"), new Guid("717107b5-fafc-4cca-b85d-364d819a7529"), new Guid("3f7535b3-76dc-47c8-9668-895596bafc16")).WithObjectTypes(PayHistory, SalaryStep).WithSingularName("SalaryStep")  .WithPluralName("SalarySteps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b7ef1bf8-b16b-400e-903e-d0a7454572a0"), new Guid("9717c46c-8c64-477a-916a-98594dd21039"), new Guid("fcae3d2d-fe78-4501-8c8e-bda78822c6f2")).WithObjectTypes(PayHistory, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // ShipmentValue
            new RelationTypeBuilder(domain, new Guid("a87bb4e3-ca1c-4887-9305-19febfc531fd"), new Guid("dd6f8067-ae66-41d1-b211-9d9b68459bcc"), new Guid("48754c2d-fc0c-47ac-af53-bc4b2f9adc20")).WithObjectTypes(ShipmentValue, allorsDecimal).WithSingularName("ThroughAmount")  .WithPluralName("ThroughAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d637b8ab-c6ac-4855-81db-f0a1f1584219"), new Guid("af4f35a2-6f4b-4e99-9d5d-271eafc5db17"), new Guid("3f7500ff-afc7-4dc8-a454-e35838380a0c")).WithObjectTypes(ShipmentValue, allorsDecimal).WithSingularName("FromAmount")  .WithPluralName("FromAmounts")      .WithPrecision(19).WithScale(2).Build();
			
            // InternalOrganisationAccountingPreference
            new RelationTypeBuilder(domain, new Guid("0ac44c21-6a2c-4162-9d77-fe1b16b60b73"), new Guid("4d61b711-7aab-4162-bb31-74db09f666fe"), new Guid("0da9c561-a6fc-4fea-aee3-5c24a2b08aea")).WithObjectTypes(InternalOrganisationAccountingPreference, GeneralLedgerAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7279a067-a219-478a-8573-4a212448328b"), new Guid("c86e149d-dba6-4928-ac01-66f74cb7f102"), new Guid("0dd6f18e-4ec3-491e-b6d0-fbfb7fceb37d")).WithObjectTypes(InternalOrganisationAccountingPreference, InventoryItemKind).WithSingularName("InventoryItemKind")  .WithPluralName("InventoryItemKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bd24005a-4391-417b-83f3-da7fa0324cf1"), new Guid("588aff14-7523-47be-bf96-5a81630754a7"), new Guid("f4799bf2-32bc-4f3e-b337-dfca87a58b21")).WithObjectTypes(InternalOrganisationAccountingPreference, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bdd72700-8be8-4db4-8d1c-3fa3fdb8548f"), new Guid("d27bfc28-e617-438c-a8d6-c36ce7cd22b6"), new Guid("e9b60088-c318-4755-a3ac-d25737e0a21b")).WithObjectTypes(InternalOrganisationAccountingPreference, Receipt).WithSingularName("Receipt")  .WithPluralName("Receipts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bf3732f2-2c6e-4931-9f35-ab46b8b26e63"), new Guid("28945e7e-6cce-43e5-afb6-13c224a3bf34"), new Guid("f94c3b98-53ec-4622-96f3-d8d7d8baa383")).WithObjectTypes(InternalOrganisationAccountingPreference, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")    .WithIsIndexed(true)  .Build();
			
            // BankAccount
            new RelationTypeBuilder(domain, new Guid("52677328-d903-4e97-83c1-b55668ced66d"), new Guid("6895f657-2e32-4a12-af0c-bb2d5d633174"), new Guid("ddf52c63-b6d5-4bae-9d54-f1c71e76c289")).WithObjectTypes(BankAccount, Bank).WithSingularName("Bank")  .WithPluralName("Banks")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("53bb9d62-a8e5-417c-9392-c54cf99bc24b"), new Guid("65fc437f-ae06-4d85-a300-3508edeec4c1"), new Guid("2d1f34f5-6a15-4b4d-901e-f8b8dcf1df01")).WithObjectTypes(BankAccount, allorsString).WithSingularName("NameOnAccount")  .WithPluralName("NamesOnAccount")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("93447a57-a049-4eaa-98ec-6fec60bdb64c"), new Guid("68e37671-a29b-44fa-9f19-2efe76a409f3"), new Guid("f22eb146-8e3f-4ea6-85f5-3a2b0d08ecc5")).WithObjectTypes(BankAccount, ContactMechanism).WithSingularName("ContactMechanism")  .WithPluralName("ContactMechanisms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a7d242b4-4d39-4254-beb2-914eb556f7b7"), new Guid("2911fab2-a04f-4afc-961d-4fac26f01ae3"), new Guid("ab751b97-c9d9-46bc-9209-6a0b3c191ea0")).WithObjectTypes(BankAccount, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ac2d58e5-ad74-4afe-b9f0-aeb9dfdcd4b3"), new Guid("55e4252b-7543-4384-8fe5-65aff3648744"), new Guid("3783ab93-3ca8-4a04-be01-5831b7f3ab02")).WithObjectTypes(BankAccount, allorsString).WithSingularName("Iban")  .WithPluralName("Ibans")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b06a858d-a8ee-41b8-a747-7fd46336ae4f"), new Guid("00656807-27c8-4803-a1e3-aad812af2f9e"), new Guid("08604620-9d2f-4b98-bba6-16147c0d9978")).WithObjectTypes(BankAccount, allorsString).WithSingularName("Branch")  .WithPluralName("Branches")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ecaedf71-98a2-425d-8046-cc8865fdbe73"), new Guid("9174bacb-3955-462a-a53f-ec251466da1b"), new Guid("7d0da5fc-29a4-4a53-a834-9c58662145d0")).WithObjectTypes(BankAccount, Person).WithSingularName("ContactPerson")  .WithPluralName("ContactPersons")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // ServiceEntryHeader
            new RelationTypeBuilder(domain, new Guid("6b29a626-04f6-423f-8ae5-cb49e8f9211d"), new Guid("9f14e67f-328b-44e6-8c80-707441848265"), new Guid("21500c76-8a3e-4737-aa69-e348e06440e2")).WithObjectTypes(ServiceEntryHeader, ServiceEntry).WithSingularName("ServiceEntry")  .WithPluralName("ServiceEntries")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7e160fbc-1339-433c-9dcb-9b3ad58ad400"), new Guid("a9d0cbd8-bb20-45e1-b109-6620b23fa1b7"), new Guid("ef8b435e-e354-45e2-89bc-3d452cc84f5a")).WithObjectTypes(ServiceEntryHeader, allorsDateTime).WithSingularName("SubmittedDate")  .WithPluralName("SubmittedDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("902235fe-a6c5-47bb-936b-8b6ce54b3d15"), new Guid("1f93dde3-a9bd-4e10-8ec6-38edaec6ffb5"), new Guid("3b27dd30-5452-480f-ae19-6937c422b541")).WithObjectTypes(ServiceEntryHeader, Person).WithSingularName("SubmittedBy")  .WithPluralName("SubmittedBys")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartRevision
            new RelationTypeBuilder(domain, new Guid("6d1b4cec-abff-46db-a446-0f8889426b28"), new Guid("82cf09e8-535f-45fe-876e-484dfb3ea102"), new Guid("946a84d0-36f8-4805-bbdd-a0779c9d008c")).WithObjectTypes(PartRevision, allorsString).WithSingularName("Reason")  .WithPluralName("Reasons")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("84561abd-08bc-4d28-b25c-22787d8bd7f0"), new Guid("4f700281-794b-4250-8bbe-f4fbbbcf8243"), new Guid("8c408bc0-82f2-4343-93d2-87047c024ef9")).WithObjectTypes(PartRevision, Part).WithSingularName("SupersededByPart")  .WithPluralName("SupersededByParts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8a064340-def3-4d9f-89d6-3325b8a41f4d"), new Guid("6c674199-8f5f-469c-8f94-f35d64304968"), new Guid("190e180b-cf6f-485d-80b2-9042e0fe04a7")).WithObjectTypes(PartRevision, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ProductConfiguration
            new RelationTypeBuilder(domain, new Guid("463f9523-62e0-4f33-a0fd-29b42f4af046"), new Guid("4c878c18-a3d4-4928-a675-4c0940f05c41"), new Guid("acf16223-8095-4133-89e9-841e11447a63")).WithObjectTypes(ProductConfiguration, Product).WithSingularName("ProductUsedIn")  .WithPluralName("ProductsUsedIn")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("528afcdf-09c2-4b3a-89b0-4da8fd732e83"), new Guid("774872d1-6b99-4e5e-8f00-f791a11ea337"), new Guid("b2fe3235-8886-4ed1-b8df-edb36e9c8e17")).WithObjectTypes(ProductConfiguration, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9e6d3782-2f32-4155-a1bf-62c02e8cbe82"), new Guid("aba19375-c6c7-4955-b764-bf731822b4f8"), new Guid("53546606-dc80-4224-a6ce-45acff613fb9")).WithObjectTypes(ProductConfiguration, allorsDecimal).WithSingularName("QuantityUsed")  .WithPluralName("QuantitiesUsed")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("caabfae5-6cff-41df-a267-9f4bde0b4808"), new Guid("d76092e1-40e4-45ab-ada5-cbe9206dcf84"), new Guid("3afc48ad-7897-4741-9d32-78f857b414fb")).WithObjectTypes(ProductConfiguration, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // OwnCreditCard
            new RelationTypeBuilder(domain, new Guid("7ca9a38c-4318-4bb6-8bc6-50e5dfe9c701"), new Guid("3dc97f13-b6b7-47eb-ae6c-b57b45a2f129"), new Guid("0bfa9940-e320-4e52-903a-b6765830069a")).WithObjectTypes(OwnCreditCard, Person).WithSingularName("Owner")  .WithPluralName("Owners")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e2514c8b-5980-4e58-a75f-20890ed79516"), new Guid("2f572644-647a-4d4e-b085-400ba3a88f7a"), new Guid("81d792be-5f29-415e-8290-66b98a95e9e3")).WithObjectTypes(OwnCreditCard, CreditCard).WithSingularName("CreditCard")  .WithPluralName("CreditCards")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Dimension
            new RelationTypeBuilder(domain, new Guid("6901f550-4470-4acf-8234-96c1b1bd0bc6"), new Guid("094356ad-e8d6-4f6b-b1c6-910a3d9fc518"), new Guid("1863b99e-415e-42a0-acef-613f7b3e3315")).WithObjectTypes(Dimension, allorsDecimal).WithSingularName("Unit")  .WithPluralName("Units")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c4fa3792-9784-43ea-91f1-1533f1d12765"), new Guid("ea393d05-73c8-4b52-b578-c02cc718f076"), new Guid("fae40aa7-15ea-4b37-8d33-86df26b14b54")).WithObjectTypes(Dimension, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // FinancialAccount
            new RelationTypeBuilder(domain, new Guid("f90475c7-4a2d-42fd-bafd-96557c217c19"), new Guid("5566f06a-feb0-45f0-9a84-673b758b6af9"), new Guid("29bda327-86c2-4fa4-af63-8e870cc736b5")).WithObjectTypes(FinancialAccount, FinancialAccountTransaction).WithSingularName("FinancialAccountTransaction")  .WithPluralName("FinancialAccountTransactions")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // PickList
            new RelationTypeBuilder(domain, new Guid("0bdfcd8a-af37-41a7-be2c-db7848d4fd05"), new Guid("88919577-6835-4c84-9e3d-a1ec50fc5c2b"), new Guid("6042abcd-a859-42bb-818d-9409f7b08d7a")).WithObjectTypes(PickList, CustomerShipment).WithSingularName("CustomerShipmentCorrection")  .WithPluralName("CustomerShipmentsCorrection")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1176ffe1-efff-4c02-b4df-5bba9052f6da"), new Guid("dcb3602c-f60e-4798-b32d-2a69f9e1056b"), new Guid("920c6a7e-b8b8-4155-9209-4c8ed24a023a")).WithObjectTypes(PickList, allorsDateTime).WithSingularName("CreationDate")  .WithPluralName("CreationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("3bb68c85-4e2b-42b8-b5fb-18a66c58c283"), new Guid("11fddfe2-9b04-4b53-a4ff-6f571e73c32a"), new Guid("a139b102-f8a9-43f1-9b14-d3c76f7be294")).WithObjectTypes(PickList, PickListItem).WithSingularName("PickListItem")  .WithPluralName("PickListItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4231c38e-e54c-480d-9e0f-2fe8bd101da1"), new Guid("b4d28461-6b82-4843-90ee-a5c3c0cddfc0"), new Guid("11fa5c06-67ce-44e0-b205-e60be00e9922")).WithObjectTypes(PickList, PickListObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("62239709-cd1f-4582-99f7-8f18e875e241"), new Guid("61ae7eeb-259c-44bb-9de7-aff577a66669"), new Guid("fe4d009e-1ea4-43d2-b4ce-96a1d9af5cf7")).WithObjectTypes(PickList, PickListStatus).WithSingularName("CurrentPickListStatus")  .WithPluralName("CurrentPickListStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6572f862-31b2-4be9-b7dc-7fff5d21f620"), new Guid("2a502d47-1319-45a4-ad52-70dd41435732"), new Guid("76ddffff-4968-4b4b-8b52-58a1a05a774d")).WithObjectTypes(PickList, Person).WithSingularName("Picker")  .WithPluralName("Pickers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7b5e6ef5-e5c0-4e7c-8955-b6c18f136fee"), new Guid("ede5efc3-a840-44b5-8389-611c05ae4df2"), new Guid("ec323cf6-acad-4e8b-bb73-0323e9aee277")).WithObjectTypes(PickList, PickListStatus).WithSingularName("PickListStatus")  .WithPluralName("PickListStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8ffdbeab-618f-42fb-8ec8-4bb34c65f489"), new Guid("50ffbba5-5584-411c-98a8-61320be9ab15"), new Guid("e623f810-2bf7-4dda-8d83-c0d0bc17c379")).WithObjectTypes(PickList, PickListObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ae75482e-2c41-46d4-ab73-f3aac368fd50"), new Guid("6b8acd68-6aba-4092-8c87-cdc62d9a4c82"), new Guid("61785577-8ab7-457c-870f-69ecb7c41f8b")).WithObjectTypes(PickList, Party).WithSingularName("ShipToParty")  .WithPluralName("ShipToParties")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e334e938-35e7-4217-91fa-efb231f71a37"), new Guid("0706d8f1-764d-4ab9-b63a-1b0213cc9dbd"), new Guid("4c3d2de1-6735-40fc-bfe9-65a64aaf966c")).WithObjectTypes(PickList, Store).WithSingularName("Store")  .WithPluralName("Stores")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // StoreRevenue
            new RelationTypeBuilder(domain, new Guid("0074524a-afb6-4acd-b8d7-34dcc988a34a"), new Guid("dfaa2eeb-c42f-44e2-89ba-69ab1e484093"), new Guid("dc9a6bd0-9250-4e2a-91bf-22ae91d992c0")).WithObjectTypes(StoreRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("0bb50dce-d7dc-49a7-8af6-f9a75232e1f4"), new Guid("1fe6427d-19fa-44d9-a116-92b3cc9ebca5"), new Guid("02c4a390-69d3-4133-a29b-82779566d79e")).WithObjectTypes(StoreRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("4686c8fa-0637-4159-8cb6-ff738f686eb0"), new Guid("b1557f78-f07c-417b-b0b0-c4f26f08574e"), new Guid("3daa636c-e6bc-461d-9db7-7a890622e506")).WithObjectTypes(StoreRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("620bebb6-89b9-4c37-b9c6-a80605dab7dd"), new Guid("bcaa6464-f7b9-4cec-91c1-8a35b7f5889c"), new Guid("5bcdf844-f377-4439-84f3-191a79346fcb")).WithObjectTypes(StoreRevenue, Store).WithSingularName("Store")  .WithPluralName("Stores")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("870630fe-7bc0-4442-befe-10a013cb4dcd"), new Guid("dd9cc663-1f43-44be-8942-63ba5740ffaf"), new Guid("452d7d3f-5028-4526-b7cc-17cc24457dd7")).WithObjectTypes(StoreRevenue, allorsString).WithSingularName("StoreName")  .WithPluralName("StoreNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b1305cde-30aa-4573-a227-d333eed87713"), new Guid("f51e254a-62a8-4670-9a39-2ecaa09cad53"), new Guid("2b002d48-54b3-453d-bbdd-5debeaebce55")).WithObjectTypes(StoreRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e20a98fc-50d9-4bb4-97a4-6754886fd0a2"), new Guid("3b527ed6-a340-47ee-b294-76b50b122c16"), new Guid("bd11a8a5-be1c-4c87-8c90-8cac07e6b8df")).WithObjectTypes(StoreRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")    .WithIsIndexed(true)  .Build();
			
            // ProductCategoryRevenueHistory
            new RelationTypeBuilder(domain, new Guid("19307e7c-c703-4d76-af03-53add0ad0dec"), new Guid("9e03ab90-cde9-439a-94ef-7807a1735248"), new Guid("b4027d52-49b4-4991-9294-110e914743ab")).WithObjectTypes(ProductCategoryRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4db15d5b-129f-412e-9f76-9573a66603c0"), new Guid("f615a26c-9fb3-44d1-b481-f89fa8440945"), new Guid("57681daa-0588-43cb-a9b0-c1d982fb2408")).WithObjectTypes(ProductCategoryRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("51f81397-a0da-49f7-9f33-96d3aa85f000"), new Guid("fdee71e1-8c89-4042-ad5d-cab81c88f54d"), new Guid("0d668529-e9e7-4a2c-8451-f4e8b80ec4cc")).WithObjectTypes(ProductCategoryRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("72354b14-fce1-4a41-868b-0e315775e506"), new Guid("03e4a65f-3084-4a8c-993b-8a136d93fcbe"), new Guid("273cd4fa-0a35-4c21-935e-9e01b010e001")).WithObjectTypes(ProductCategoryRevenueHistory, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesRepRevenueHistory
            new RelationTypeBuilder(domain, new Guid("0d2277e4-470d-487a-9276-3972d57c8512"), new Guid("a86b2a21-c5d1-4ee5-9039-e5b3fcfd3d2d"), new Guid("8ff9fb8e-a85b-4a47-b591-c882614c767f")).WithObjectTypes(SalesRepRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5dc09d22-d541-4088-a715-9fc65d24453f"), new Guid("2a2dccee-642d-4336-9447-8304da2f8c79"), new Guid("b4774899-e3cc-4990-9974-7638b9e60d9b")).WithObjectTypes(SalesRepRevenueHistory, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a38139e6-a227-478b-8924-7d62b73c06d8"), new Guid("78157141-6c25-44ce-bd6d-f64f07ae2baa"), new Guid("3ec55f87-0dde-48d0-878c-834add5f2a9c")).WithObjectTypes(SalesRepRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b3e41872-5488-4ca6-b3c1-3b1ff058b6bb"), new Guid("96ab29d6-1aca-48a3-b4d9-4a1123d50f01"), new Guid("13e42a5b-76e7-4c0a-a3bd-0fe8e42c52a0")).WithObjectTypes(SalesRepRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
			
            // CostCenter
            new RelationTypeBuilder(domain, new Guid("2a2125fd-c715-4a0f-8c1a-c1207f02a494"), new Guid("9a61f338-9bf3-45cf-abc0-89eb1cecf9c0"), new Guid("f3ec0b58-3245-4b95-9f01-8a01f333750c")).WithObjectTypes(CostCenter, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("76947134-0cae-4244-a8f3-fbb018930fd3"), new Guid("dfb847f1-46fe-4adc-94a1-c2d57d911348"), new Guid("7e795eb5-b79d-455f-919b-bdfed4d926c3")).WithObjectTypes(CostCenter, OrganisationGlAccount).WithSingularName("InternalTransferGlAccount")  .WithPluralName("InternalTransferGlAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("83a7ca20-8a73-4f8e-9729-731d25f70313"), new Guid("e4ccdfcc-790f-41d2-a225-0b46862aed11"), new Guid("28b04874-6757-4cf9-a290-ed35ecba9d14")).WithObjectTypes(CostCenter, CostCenterCategory).WithSingularName("CostCenterCategory")  .WithPluralName("CostCenterCategories")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("975003f1-203e-4cbe-97d2-7f6ccc95f75a"), new Guid("8487030b-f156-4a0b-bcf0-22c880ded449"), new Guid("341c226e-3da2-4976-9552-97e5b5796b1f")).WithObjectTypes(CostCenter, OrganisationGlAccount).WithSingularName("RedistributedCostsGlAccount")  .WithPluralName("RedistributedCostsGlAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a3168a59-38ea-4359-b258-c9cbd656ce35"), new Guid("1f62b015-938d-4c36-9d96-879b28c237e0"), new Guid("a401120c-42dd-4237-8afb-dcaa1e8e19f5")).WithObjectTypes(CostCenter, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d7e01e38-d271-4c9c-847e-d26d9d4957af"), new Guid("89c07010-e93a-49c7-9a53-bb5588c38808"), new Guid("922adffb-ad26-445c-b431-b19e9ee79842")).WithObjectTypes(CostCenter, allorsBoolean).WithSingularName("Active")  .WithPluralName("Actives")      .Build();
            new RelationTypeBuilder(domain, new Guid("e6332140-65e7-4475-aea1-a80424640696"), new Guid("acd62c99-b86d-426a-8d7c-baca21d30665"), new Guid("9cfa8fb1-fa68-4bb3-8440-5fe030a71c9d")).WithObjectTypes(CostCenter, allorsBoolean).WithSingularName("UseGlAccountOfBooking")  .WithPluralName("UseGlAccountsOfBooking")      .Build();
			
            // SupplierRelationship
            new RelationTypeBuilder(domain, new Guid("1546c9f0-84ce-4795-bcea-634d6a78e867"), new Guid("56c5ff64-f67b-4830-a1e4-11661b0ff898"), new Guid("a0e757a2-d780-43a1-8b21-ab2fc4d75e7e")).WithObjectTypes(SupplierRelationship, Organisation).WithSingularName("Supplier")  .WithPluralName("Suppliers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("17aa6ceb-0cbd-45fa-9f6d-848ce4a365b1"), new Guid("b5057208-9823-4a54-8394-6100d18dbe4a"), new Guid("6ddbeb86-39ea-4bd8-ae17-af4b9a1968ce")).WithObjectTypes(SupplierRelationship, allorsInteger).WithSingularName("SubAccountNumber")  .WithPluralName("SubAccountNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("b12a68f6-0eaa-4a8a-a741-398a0be43f62"), new Guid("01adc720-91a4-47c6-9235-d21a3215ee6f"), new Guid("0363121b-d92e-4722-81dc-47032aae5440")).WithObjectTypes(SupplierRelationship, allorsDateTime).WithSingularName("LastReminderDate")  .WithPluralName("LastReminderDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("e96a79e7-c161-4ed9-a5cc-8de4f67bf954"), new Guid("1a406db2-268a-4669-a629-e0e15fdbd826"), new Guid("aa91e2ad-89c0-411e-9271-56fbf20489f6")).WithObjectTypes(SupplierRelationship, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ee871786-8840-404d-9b41-932a9f59be13"), new Guid("5b98959d-5589-4958-a86f-4c9b465c1632"), new Guid("056ca61a-1ab4-4e53-8d5f-328ada5f3b11")).WithObjectTypes(SupplierRelationship, allorsDateTime).WithSingularName("BlockedForDunning")  .WithPluralName("BlockedsForDunning")      .Build();
			
            // EventRegistration
            new RelationTypeBuilder(domain, new Guid("af4b8828-bea1-43e5-b109-9934311cc2df"), new Guid("bd962b63-dff5-4b09-bd30-ef11755a381e"), new Guid("dcfcbf03-1cac-4533-a3b7-a235f167645c")).WithObjectTypes(EventRegistration, Person).WithSingularName("Person")  .WithPluralName("Persons")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ed542026-7020-43e3-ab72-c3f4dd991a4b"), new Guid("cba93e20-e78d-4c86-8b5b-2daa930fde35"), new Guid("b2c0c98f-5ed7-4329-bfcb-ab0f3e6e169c")).WithObjectTypes(EventRegistration, Event).WithSingularName("Event")  .WithPluralName("Events")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f9805362-2bd2-46d4-b9b2-d38cd0a76f78"), new Guid("e2dcb678-d8ba-45ed-9f25-5a87aae2d18f"), new Guid("19f9c2a6-5e2c-4bfb-bc63-b13d998e92e3")).WithObjectTypes(EventRegistration, allorsDateTime).WithSingularName("AllorsDateTime")  .WithPluralName("AllorsDateTimes")      .Build();
			
            // ServiceEntryBilling
            new RelationTypeBuilder(domain, new Guid("2fb9d650-0a28-4a39-8427-8c12bc4a20a1"), new Guid("c91e3796-6b23-4aa7-992b-ac15da334eae"), new Guid("7ca6affa-7d4f-45bd-88e2-a0fa1cad4ad7")).WithObjectTypes(ServiceEntryBilling, ServiceEntry).WithSingularName("ServiceEntry")  .WithPluralName("ServiceEntries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a8c707fb-98c1-43b1-99a3-9464cb25ea5f"), new Guid("284bf54c-8305-4892-ad00-f4975e155522"), new Guid("570acec5-b62e-4abc-bb1d-000fb70bc2fe")).WithObjectTypes(ServiceEntryBilling, InvoiceItem).WithSingularName("InvoiceItem")  .WithPluralName("InvoiceItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PurchaseShipment
            new RelationTypeBuilder(domain, new Guid("400d42cd-bb10-406d-a448-9d9d53ccb5ca"), new Guid("9133d8c2-1988-42ef-a006-d02693e322e4"), new Guid("2f25f5a9-d1ef-45e3-8317-8bbcc65ff2ff")).WithObjectTypes(PurchaseShipment, PurchaseShipmentObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("40277d59-6ab8-40b0-acee-c95ba759e2c8"), new Guid("d7feb989-dd2d-4619-b079-8a059129f8ed"), new Guid("068d5263-18d7-40e4-80c1-4f9a8e88d10a")).WithObjectTypes(PurchaseShipment, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("63363873-1dba-4044-8793-0196fc58f6ab"), new Guid("184515ec-10fc-49f9-8822-6a53eb899700"), new Guid("a3a49f0c-243b-4656-97f5-66fd012874eb")).WithObjectTypes(PurchaseShipment, PurchaseShipmentObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8be328c9-0688-469b-901d-c9c290b30e88"), new Guid("df4fe9a9-3043-44a6-a4da-96465e63ba07"), new Guid("3726d5dd-a927-48ac-83a5-04d5c2e46b85")).WithObjectTypes(PurchaseShipment, PurchaseShipmentStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("944c8d81-db22-469b-beb3-d31c045b5af0"), new Guid("e070f627-4b02-4f43-998b-5b4d8ccbfe80"), new Guid("9e7f1f5d-ed7f-43d8-bb16-45b7f9adc43e")).WithObjectTypes(PurchaseShipment, PurchaseShipmentStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ef34543c-6194-4f27-87d7-a54285bc0a15"), new Guid("33b1069f-7be2-4f41-b502-8689256706d9"), new Guid("33dce90e-2a2a-482b-bee5-fcea55e59160")).WithObjectTypes(PurchaseShipment, PurchaseOrder).WithSingularName("PurchaseOrder")  .WithPluralName("PurchaseOrders")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // UnitOfMeasureConversion
            new RelationTypeBuilder(domain, new Guid("3ae94702-ee60-4057-a649-f655ff4e2865"), new Guid("1ab7d188-af19-4742-a0e6-11043b666bd4"), new Guid("5372ec1c-9b57-4ed5-b665-cdad8a13d933")).WithObjectTypes(UnitOfMeasureConversion, IUnitOfMeasure).WithSingularName("ToUnitOfMeasure")  .WithPluralName("ToUnitOfMeasures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5d7ed801-4a2e-4abc-a32d-d869210132af"), new Guid("a3467a5f-8c7d-453a-9a33-18d742f20d06"), new Guid("4b8a465d-9334-427f-b799-d08b7c84200a")).WithObjectTypes(UnitOfMeasureConversion, allorsDateTime).WithSingularName("StartDate")  .WithPluralName("StartDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("835118da-148a-4c42-ab07-75b213a8e1f7"), new Guid("f9f78e34-6fe1-4863-b831-cabe46cbc764"), new Guid("c06dd0a5-dabe-46fa-97f7-62f6f4b47983")).WithObjectTypes(UnitOfMeasureConversion, allorsDecimal).WithSingularName("ConversionFactor")  .WithPluralName("ConversionFactors")      .WithPrecision(19).WithScale(9).Build();
			
            // PaymentBudgetAllocation
            new RelationTypeBuilder(domain, new Guid("28f23032-b81c-4dbb-aa6c-24740ae3bb26"), new Guid("451ff770-7f33-4251-b78b-907ad95a9c38"), new Guid("1b82d64e-aeff-4150-b388-1144bef8b2ee")).WithObjectTypes(PaymentBudgetAllocation, Payment).WithSingularName("Payment")  .WithPluralName("Payments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3245613c-71d3-4a5e-a687-6f5ac306d9df"), new Guid("3cf6b4ce-56df-44c9-9348-3a419a226edc"), new Guid("e672774d-e11d-46df-9387-ef9f52315148")).WithObjectTypes(PaymentBudgetAllocation, BudgetItem).WithSingularName("BudgetItem")  .WithPluralName("BudgetItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a982dfa7-4c81-41d6-93ec-ea380a526ad0"), new Guid("cb680a9c-ba32-4ceb-b9fb-127041e509e5"), new Guid("69f570b8-7f72-4ef5-934a-f7e7d0b0465d")).WithObjectTypes(PaymentBudgetAllocation, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // ProductRevenueHistory
            new RelationTypeBuilder(domain, new Guid("c5df5a04-12b0-4b86-87c9-6318bbb05078"), new Guid("53287e78-440a-4789-a706-a456e5267f0c"), new Guid("94f656f9-e6d0-4fae-b7ee-f6ef5378fd83")).WithObjectTypes(ProductRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d7d60054-4b62-428f-a223-0b0852841953"), new Guid("dd7f995b-7262-466a-b669-789dd5c3b774"), new Guid("cf211c4a-03fd-461b-b0b9-787d7b245ca2")).WithObjectTypes(ProductRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e967f657-6092-473f-9772-552b3372f847"), new Guid("7af0962e-9d48-4b9f-aed4-80f16c2592c8"), new Guid("678a15bf-f006-439a-8b96-dc8dc6fdd64d")).WithObjectTypes(ProductRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ef4c7d18-192b-4880-beb3-645911d6b21a"), new Guid("b28db2ad-2f30-41b4-8a33-ba1d888c8e2e"), new Guid("af53ba3e-d2cb-4f2c-810b-a3f4c32dbc3f")).WithObjectTypes(ProductRevenueHistory, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // OrderRequirementCommitment
            new RelationTypeBuilder(domain, new Guid("a03b08be-82d9-4678-803a-0463c658d4c4"), new Guid("2ed48b3d-1c77-49f9-a970-836d066cc00f"), new Guid("4f5be1db-964c-4c09-86ec-5b7bd06a4008")).WithObjectTypes(OrderRequirementCommitment, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("a9020377-d721-4329-868d-33ab63aed074"), new Guid("5654ce5d-3453-404c-86cb-dfc1cc175345"), new Guid("85a19592-2e58-4d45-8463-2119658fa0b7")).WithObjectTypes(OrderRequirementCommitment, OrderItem).WithSingularName("OrderItem")  .WithPluralName("OrderItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e36224d2-cc6f-43b0-82e1-e300710f6407"), new Guid("5f56109c-0578-4db7-9c8a-de9617d374d8"), new Guid("2f69978e-bd92-48b2-a711-58b4cf728d96")).WithObjectTypes(OrderRequirementCommitment, Requirement).WithSingularName("Requirement")  .WithPluralName("Requirements")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // OrganisationRollUp
            new RelationTypeBuilder(domain, new Guid("1ed8bd41-7552-44bd-bcb0-f24c47cf84ca"), new Guid("924282be-62b0-4a94-814e-04ef94bbeaac"), new Guid("09c50cf9-87b3-4280-80d3-b793b392d168")).WithObjectTypes(OrganisationRollUp, Organisation).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4301bb17-43b6-4bf3-a874-7441dd419dd0"), new Guid("5b6d83a4-a7f5-4097-bc9b-7ba91e3b96ee"), new Guid("269ea202-bffb-42ff-a497-4a2fa1afbaad")).WithObjectTypes(OrganisationRollUp, OrganisationUnit).WithSingularName("RollupKind")  .WithPluralName("RollupKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("92ebf310-72ea-468b-a880-7268b48df41a"), new Guid("71b8ea7b-5316-42df-adc0-2aded71c9eaf"), new Guid("e005418e-9180-4146-b55a-81ff9fb06078")).WithObjectTypes(OrganisationRollUp, Organisation).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Request
            new RelationTypeBuilder(domain, new Guid("1bb3a4b8-224a-47ab-b05b-c0c8a87ec09c"), new Guid("57109e48-b116-4ea5-b636-73816c0dda68"), new Guid("d63a2e09-95e1-4c90-83a1-a5366a3d5ca3")).WithObjectTypes(Request, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("208f711f-5d9d-4dc3-89ad-b1583ad06582"), new Guid("d91ef645-f5ef-4f09-9d6b-c023d02978f5"), new Guid("c1467dbc-9b64-49a0-8715-90ad277b02c9")).WithObjectTypes(Request, allorsDateTime).WithSingularName("RequiredResponseDate")  .WithPluralName("RequiredResponseDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("25332874-3ec6-41d8-ac6a-77dd4328e503"), new Guid("acae3045-3612-4cac-9994-ca81d350da74"), new Guid("576e5797-b3d3-41ab-a788-2b3eeba36f18")).WithObjectTypes(Request, RequestItem).WithSingularName("RequestItem")  .WithPluralName("RequestItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8ac90ec6-9d3e-45fe-aaba-27d0c1c058a1"), new Guid("438f6e6a-292b-4579-bf87-7478c48b9159"), new Guid("c16a1509-cfd6-4f9d-87ce-4b903365b9e5")).WithObjectTypes(Request, allorsString).WithSingularName("RequestNumber")  .WithPluralName("RequestNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c3389cec-ee8e-45e2-a1eb-01c9a87b2df0"), new Guid("b5aaad5b-568c-405d-9018-3ff0fcde7dd2"), new Guid("934585ce-6dc2-46cd-a227-24a1cb85fa60")).WithObjectTypes(Request, RespondingParty).WithSingularName("RespondingParty")  .WithPluralName("RespondingParties")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f1a50d9d-2e79-45ac-8f23-8f38fab985c1"), new Guid("fe8fd88b-8b7d-4998-bf59-56b4e8d44571"), new Guid("2e871e31-a702-4955-8922-ed49a41d5ef1")).WithObjectTypes(Request, Party).WithSingularName("Originator")  .WithPluralName("Originators")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();

            // RevenueValueBreak
            new RelationTypeBuilder(domain, new Guid("96391ee1-5ba2-48db-95c9-cec6e73758b7"), new Guid("846a94f9-72cd-48a7-be94-9e8f146e245a"), new Guid("8ea60dde-6149-4389-bb94-b94e7bcc81b2")).WithObjectTypes(RevenueValueBreak, allorsDecimal).WithSingularName("ThroughAmount")  .WithPluralName("ThroughAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("cf544df2-3ccb-42b5-b009-c355fcf88ed6"), new Guid("dbdb3b16-701c-4f45-9d38-6b3e21f66ab3"), new Guid("44217cbb-1f44-4c04-bb66-e8bf597df3f6")).WithObjectTypes(RevenueValueBreak, allorsDecimal).WithSingularName("FromAmount")  .WithPluralName("FromAmounts")      .WithPrecision(19).WithScale(2).Build();
			
            // WorkEffortAssignment
            new RelationTypeBuilder(domain, new Guid("54bbdb5d-74b9-4ac7-b638-b1ef4a210b6e"), new Guid("91713efa-721d-43b7-99dd-ec7681456781"), new Guid("dafa5322-4905-40fa-ae14-ae5ee80f0f1c")).WithObjectTypes(WorkEffortAssignment, Person).WithSingularName("Professional")  .WithPluralName("Professionals")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("93cb0818-2599-4652-addd-4a1032d5dde9"), new Guid("2d5c955f-4bd5-43d2-a8f4-3df03ef6b78b"), new Guid("c42be8db-6e5a-459c-afbc-39bcac3e1eb2")).WithObjectTypes(WorkEffortAssignment, WorkEffort).WithSingularName("Assignment")  .WithPluralName("Assignments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // FiscalYearInvoiceNumber
            new RelationTypeBuilder(domain, new Guid("14f064a8-461c-4726-93c4-91bc34c9c443"), new Guid("02716f0b-8fef-4791-85ae-7c15a5581433"), new Guid("5377c7e0-8bc0-4621-83c8-0829c3fae3f2")).WithObjectTypes(FiscalYearInvoiceNumber, allorsInteger).WithSingularName("NextSalesInvoiceNumber")  .WithPluralName("NextSalesInvoiceNumbers")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("c1b0dcb6-8627-4a47-86d0-2866344da3f1"), new Guid("3d1c515f-a52f-4038-9820-794f44927beb"), new Guid("ba7329de-0176-4782-92e1-1cd932823ec0")).WithObjectTypes(FiscalYearInvoiceNumber, allorsInteger).WithSingularName("FiscalYear")  .WithPluralName("FiscalYears")      .Build();
			
            // GeographicBoundary
            new RelationTypeBuilder(domain, new Guid("28e43fe9-cdf1-4671-af95-ead40ecbef15"), new Guid("97f83f4c-d7ea-4928-b0a2-7e001a66b7d2"), new Guid("940ce144-a48d-4128-b110-ffcc4d578295")).WithObjectTypes(GeographicBoundary, allorsString).WithSingularName("Abbreviation")  .WithPluralName("Abbreviations")      .WithSize(10).Build();
			
            // SalesOrderStatus
            new RelationTypeBuilder(domain, new Guid("4c0986f4-c033-4646-b062-da9699bd8455"), new Guid("96c8b409-1ac9-4f31-a6a2-191bee4a1a82"), new Guid("99a005bb-6961-4a19-bedc-5bdce1829cb9")).WithObjectTypes(SalesOrderStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("e61dabc2-729c-41cc-8d89-aea6e6557914"), new Guid("f3b9f2c8-18b5-4334-99e5-7f4f4eee7571"), new Guid("2e1c48fe-536b-4b2a-8e49-7320c961d42c")).WithObjectTypes(SalesOrderStatus, SalesOrderObjectState).WithSingularName("SalesOrderObjectState")  .WithPluralName("SalesOrderObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // BillingAccount
            new RelationTypeBuilder(domain, new Guid("408019e5-6b8a-4a50-be0a-0b3de3cd55d9"), new Guid("af54fecc-d537-4611-8324-fbe426063dd0"), new Guid("ef0e1a32-4873-4d22-b037-16afb00e7fce")).WithObjectTypes(BillingAccount, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8a550d4b-d881-495b-9326-f2494a50fb5f"), new Guid("3562b1e4-0acc-4a94-a111-f1afb8d889d4"), new Guid("7746a7a0-3dee-4279-8114-639c5f106d4d")).WithObjectTypes(BillingAccount, ContactMechanism).WithSingularName("ContactMechanism")  .WithPluralName("ContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesChannelRevenue
            new RelationTypeBuilder(domain, new Guid("5b5d13cd-fc96-4a9d-826c-a47a21188717"), new Guid("400cc1d3-5e23-4067-a49e-ad0158801a8e"), new Guid("9db9bfe7-e5df-44b7-b651-f0c0fba31806")).WithObjectTypes(SalesChannelRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("9c1afcc0-d16b-4d39-bc6a-7d59e5c5487f"), new Guid("d2caaef3-8d0e-4423-b36c-074652f648aa"), new Guid("55574345-6243-4647-b1cd-eec78926d5ad")).WithObjectTypes(SalesChannelRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("a2ace411-df41-4191-b867-5f73ae5a1be7"), new Guid("1786e0dc-0483-4b80-942c-0c7d7ee8e913"), new Guid("44f58270-a4c7-49ca-838f-8a70a3ebe6d1")).WithObjectTypes(SalesChannelRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b79bed4e-c20f-448d-8748-090bfbfd803c"), new Guid("468dce88-9789-47fb-874c-bc501c9cbcea"), new Guid("f8154bd8-0d12-4e70-86d1-f16d627bdb4d")).WithObjectTypes(SalesChannelRevenue, SalesChannel).WithSingularName("SalesChannel")  .WithPluralName("SalesChannels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c86138a3-6cdd-4e97-96ca-08f015f13e78"), new Guid("43473687-dc7e-4a4b-8745-5e98747c3731"), new Guid("7d2869b9-d5e9-48ff-a12c-f32c7667d764")).WithObjectTypes(SalesChannelRevenue, allorsString).WithSingularName("SalesChannelName")  .WithPluralName("SalesChannelNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e650c7f3-3be0-4625-93fb-d0c1e72be9d0"), new Guid("ed420ba0-89a1-42b2-a353-bfb99f05fe63"), new Guid("efce65ee-43a1-4a3e-b9ec-be7b0e273f7f")).WithObjectTypes(SalesChannelRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("f0fb8f34-e639-4c48-a1b7-66197520572d"), new Guid("9c2f6135-d7c7-400b-9450-b65abc402a8c"), new Guid("936d33cb-b303-4663-90cb-0bd64c864d21")).WithObjectTypes(SalesChannelRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // AutomatedAgent
            new RelationTypeBuilder(domain, new Guid("4e158d75-d0b5-4cb7-ad41-e8ed3002d175"), new Guid("6f2a83eb-17e9-408e-b18b-9bb2b9a3e812"), new Guid("4fac2dd3-8711-4115-96b9-a38f62e2d093")).WithObjectTypes(AutomatedAgent, allorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("58870c93-b066-47b7-95f7-5411a46dbc7e"), new Guid("31925ed6-e66c-4718-963f-c8a71d566fe8"), new Guid("eee42775-b172-4fde-9042-a0f9b2224ec3")).WithObjectTypes(AutomatedAgent, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // SalesChannelRevenueHistory
            new RelationTypeBuilder(domain, new Guid("5093d76d-920b-454a-951b-ba123e1ee001"), new Guid("3aad24b2-c4d8-4aa1-a293-64aa9af82dbc"), new Guid("7f891912-fbaa-409a-816d-6cd85553aeab")).WithObjectTypes(SalesChannelRevenueHistory, SalesChannel).WithSingularName("SalesChannel")  .WithPluralName("SalesChannels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6f6af8ac-a4db-46e2-8bfd-a55161b12b66"), new Guid("345ec7c5-d2c5-489c-99eb-404659c7abba"), new Guid("119c0e16-d47a-46d4-bf57-4ece6cca2bf3")).WithObjectTypes(SalesChannelRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b98b4de3-e2ad-45b4-bbf7-c3b90261979c"), new Guid("f0e38eaa-8470-451a-ae6f-52b7822ff05f"), new Guid("2119608f-9fd4-48ef-af0f-279abbbc0d4e")).WithObjectTypes(SalesChannelRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d1fe6003-0415-4826-ac5b-6fdaff587410"), new Guid("b87bb3d9-07d4-4178-99eb-77be7baba818"), new Guid("b392d765-f353-4c20-9410-f73f73978eae")).WithObjectTypes(SalesChannelRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ProductionRun
            new RelationTypeBuilder(domain, new Guid("108bb811-ece8-42b4-89e2-7a394f848f4d"), new Guid("8eeef339-d38c-45c4-8300-61bbb33cb205"), new Guid("abbbac9a-74f3-4e7d-a56e-f5ba0c967530")).WithObjectTypes(ProductionRun, allorsInteger).WithSingularName("QuantityProduced")  .WithPluralName("QuantitiesProduced")      .Build();
            new RelationTypeBuilder(domain, new Guid("407b8671-79ea-4998-b5ed-188dd4a9f43c"), new Guid("7358c0de-4918-4998-afb8-ecd122e04e3a"), new Guid("56da6402-ddd9-4bbb-83be-3c368de22d09")).WithObjectTypes(ProductionRun, allorsInteger).WithSingularName("QuantityRejected")  .WithPluralName("QuantitiesRejected")      .Build();
            new RelationTypeBuilder(domain, new Guid("558dfd44-26a5-4d64-9317-a121fabaecf1"), new Guid("69036ddb-1a7b-4bce-8ee2-2610715e47c0"), new Guid("a708d61a-08d1-45db-877b-3eb4514a9069")).WithObjectTypes(ProductionRun, allorsInteger).WithSingularName("QuantityToProduce")  .WithPluralName("QuantitiesToProduce")      .Build();
			
            // PriceComponent
            new RelationTypeBuilder(domain, new Guid("18cda5a7-6720-4133-a71b-ce23e9ebc1bb"), new Guid("bfdd0a69-e69f-49e0-b756-e6b3307a2bd2"), new Guid("5934c3f4-3f64-4f5a-b455-70590ae02328")).WithObjectTypes(PriceComponent, GeographicBoundary).WithSingularName("GeographicBoundary")  .WithPluralName("GeographicBoundaries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1cddef96-0be9-487a-bdb3-df024656214a"), new Guid("deb9575e-c8a2-48e3-9930-33a5c2afad2d"), new Guid("3189b81e-100e-45cf-b308-d1f5e34a3c16")).WithObjectTypes(PriceComponent, allorsDecimal).WithSingularName("Rate")  .WithPluralName("Rates")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1da8c258-fb73-4cce-88f3-3c39d21a7996"), new Guid("191fa580-ad8b-4151-90e1-58c4c512ab68"), new Guid("c3519e5a-981d-453f-ab7b-edf9f29556cc")).WithObjectTypes(PriceComponent, RevenueValueBreak).WithSingularName("RevenueValueBreak")  .WithPluralName("RevenueValueBreaks")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3230c33b-42ac-4eb4-b0c9-9791cc5604d7"), new Guid("0d248d95-468e-4a19-9e84-45065dfc0006"), new Guid("8e87149c-0c44-4e02-b784-eeef963a4333")).WithObjectTypes(PriceComponent, PartyClassification).WithSingularName("PartyClassification")  .WithPluralName("PartyClassifications")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("50d6ddf3-47d9-4de1-954e-a4fae881edd0"), new Guid("ebe67168-92de-45f8-96b0-b373c47e7ff3"), new Guid("24ff9932-4fb4-4eab-b490-c397602f5820")).WithObjectTypes(PriceComponent, OrderQuantityBreak).WithSingularName("OrderQuantityBreak")  .WithPluralName("OrderQuantityBreaks")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("52883992-8e1b-472a-9cc7-67d4824a2cd4"), new Guid("5a14b240-2eb3-4051-b9cb-8eefb0fd1722"), new Guid("ac8e7099-f0bd-492f-bbd9-5890060c56eb")).WithObjectTypes(PriceComponent, PackageQuantityBreak).WithSingularName("PackageQuantityBreak")  .WithPluralName("PackageQuantityBreaks")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("55c43896-ba79-4752-8fd4-7fd8501d64b6"), new Guid("38475cd5-ee15-4507-a810-f77ef0fb5cab"), new Guid("9e905572-d1bf-435b-8238-3a00da09f243")).WithObjectTypes(PriceComponent, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5b91ebce-6ebe-4d5f-a8bc-22cd7e7d688a"), new Guid("de72aa4b-17f1-4037-99e5-95f30c1f8f90"), new Guid("78d118ed-dcb7-4b8a-84ae-d1a9fdf11643")).WithObjectTypes(PriceComponent, RevenueQuantityBreak).WithSingularName("RevenueQuantityBreak")  .WithPluralName("RevenueQuantityBreaks")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5cab507a-bf96-40c9-89cd-86d59ad07c33"), new Guid("bbeb2003-75ee-415e-a841-bab8af7153bc"), new Guid("6f1ba75a-c271-45d7-bebb-601889291784")).WithObjectTypes(PriceComponent, Party).WithSingularName("SpecifiedFor")  .WithPluralName("SpecifiedFors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6c0744ee-b730-490d-bb0c-b6be95211371"), new Guid("1eb6f686-586e-47cd-b23a-bcbc75430e7c"), new Guid("54726272-92ba-494c-9c08-d19e3248e2f3")).WithObjectTypes(PriceComponent, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("80379d5a-1831-4eed-abd3-a9574e3edd1d"), new Guid("d1a93eac-7143-43fc-897e-be6b198e69ec"), new Guid("cf38c778-74c9-4da2-8619-f3c87cee5f19")).WithObjectTypes(PriceComponent, AgreementPricingProgram).WithSingularName("AgreementPricingProgram")  .WithPluralName("AgreementPricingPrograms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8c32a2ca-a0c7-4c92-9b65-91d8b5ccee94"), new Guid("70ef8bda-aefc-458f-8f6e-5c0088bbad6b"), new Guid("090325e4-2ada-4978-afcd-04db2793c02a")).WithObjectTypes(PriceComponent, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c2b2b046-9e62-4065-8f2d-10624f7565aa"), new Guid("6e44c9e4-cf29-4303-8217-e01b5ce9dcb7"), new Guid("6fbee15a-bcba-46b6-9ea0-ff8c9a3256c4")).WithObjectTypes(PriceComponent, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cb552b8b-f251-4c57-8cc7-8cc299631022"), new Guid("087b6d0d-399d-408e-af06-84bd70a0eff6"), new Guid("e968a3a1-7394-465a-829a-f8f33ea3fc4c")).WithObjectTypes(PriceComponent, OrderKind).WithSingularName("OrderKind")  .WithPluralName("OrderKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dc1cf3af-2f22-43e6-863d-346e91aa2240"), new Guid("7a5ec695-a56a-40dc-8898-667276078d3d"), new Guid("6dfffe42-670a-4076-b240-9e8ffb43f243")).WithObjectTypes(PriceComponent, OrderValue).WithSingularName("OrderValue")  .WithPluralName("OrderValues")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dc5ad82b-c18d-4971-9689-e81475ed6a54"), new Guid("40276e1c-d9eb-4ef8-b828-387f67f1a337"), new Guid("9698ebe8-509d-493c-9d73-77028f60f2c7")).WithObjectTypes(PriceComponent, allorsDecimal).WithSingularName("Price")  .WithPluralName("Prices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("de59dbb7-996a-45be-ae2a-a7b5a0ff3d94"), new Guid("47ad080f-0e8f-4529-8470-ea4cd26d424d"), new Guid("62d6d8df-de31-4995-a3ba-af9e5c06215c")).WithObjectTypes(PriceComponent, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f8976686-bd76-4435-8ed8-f5e2490eeb94"), new Guid("38d58e93-0b17-4170-8f41-dbfb97bb11da"), new Guid("8f9625b2-d7fd-482d-8880-386dbeb74773")).WithObjectTypes(PriceComponent, SalesChannel).WithSingularName("SalesChannel")  .WithPluralName("SalesChannels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Citizenship
            new RelationTypeBuilder(domain, new Guid("45d0dd4b-6d8c-4727-b38b-f7ed850023c1"), new Guid("3944907d-5815-46a3-b380-08a78b637995"), new Guid("d60e6859-26fd-4d01-8458-221e845b75da")).WithObjectTypes(Citizenship, Passport).WithSingularName("Passport")  .WithPluralName("Passports")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ca2b2d3e-ba3c-4e92-a86f-92d5d47b8e01"), new Guid("f2b5857f-064b-4b4d-bf7f-877a46e015e3"), new Guid("c477d58e-d187-4c8c-af20-5f845e143898")).WithObjectTypes(Citizenship, Country).WithSingularName("Country")  .WithPluralName("Countries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartyProductRevenue
            new RelationTypeBuilder(domain, new Guid("3f8b0163-f038-4f3e-b426-e72ddeee3581"), new Guid("c4f6804f-46db-4007-8873-ef37652ce8b7"), new Guid("51febfc1-c684-47d5-b1cb-9e43405c14d8")).WithObjectTypes(PartyProductRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5428d606-6bd8-4090-aea0-25e042afad5c"), new Guid("052f574b-477c-488c-87c0-6c2edd882b1a"), new Guid("cc9d93ce-a2aa-409d-951e-a929dea264d7")).WithObjectTypes(PartyProductRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("5b22f74d-19eb-46f4-ac0b-1288bd00538c"), new Guid("bdb224cb-a727-4598-9ed8-7f8cb575cb47"), new Guid("5be79e8e-c413-43ae-b524-e1a70641ddc3")).WithObjectTypes(PartyProductRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8cb4b613-88f7-4767-90c2-1a4fd4e8a368"), new Guid("3ba77a3f-1fac-441b-b498-5a7187c386a0"), new Guid("3b2b5aff-ab2c-4c5e-b24f-42f4463f00a7")).WithObjectTypes(PartyProductRevenue, allorsString).WithSingularName("PartyProductName")  .WithPluralName("PartyProductNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8edddcd2-07f2-47dc-8b5a-c63401ea5042"), new Guid("fc01ed7e-157b-4d5a-824c-8406530f5cf7"), new Guid("559fc116-0a38-431e-acbc-1281bef6b503")).WithObjectTypes(PartyProductRevenue, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("98ad9944-62a5-4045-b4ba-0317240f5a61"), new Guid("297ff711-7ebd-45e0-87ed-2c68e8c71fce"), new Guid("8bb7005b-3d03-4ecd-b3d0-413a814f682a")).WithObjectTypes(PartyProductRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b2bad0dc-c3d2-498d-a434-dfbe2c29d903"), new Guid("8bc50d0b-dacb-499c-92ed-a27f0bced17c"), new Guid("eae57a6c-8efe-4e55-b18e-10d2ca0e3296")).WithObjectTypes(PartyProductRevenue, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bfca3b6f-a394-4f06-b04f-a29bcd4bade7"), new Guid("e048ce53-554c-4303-a13d-c27d8ea73c6d"), new Guid("f3ddc5c1-5281-4b42-bb2c-5f4ddbcea002")).WithObjectTypes(PartyProductRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ff1b20d3-602b-4f43-92c7-d3f412950672"), new Guid("3bdf82fa-e7c4-4ab4-b766-af73c6a4ce27"), new Guid("a1587822-ec41-4bdc-8655-491f5012ac1b")).WithObjectTypes(PartyProductRevenue, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Organisation
            new RelationTypeBuilder(domain, new Guid("1c8bf2e3-6794-47c8-990c-f124d47653fb"), new Guid("d60f70d2-a17e-47d9-bccc-7971f5ef776d"), new Guid("d0f185d6-1ae2-40bf-a95e-6fde7ae10fa9")).WithObjectTypes(Organisation, LegalForm).WithSingularName("LegalForm")  .WithPluralName("LegalForms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2cc74901-cda5-4185-bcd8-d51c745a8437"), new Guid("896a4589-4caf-4cd2-8365-c4200b12f519"), new Guid("baa30557-79ff-406d-b374-9d32519b2de7")).WithObjectTypes(Organisation, allorsString).WithSingularName("Name")  .WithPluralName("Names")    .WithIsIndexed(true)  .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("4cc8bc02-8305-4bd3-b0c7-e9b3ecaf4bd2"), new Guid("c2be4896-2eae-40fa-9300-b548741407f2"), new Guid("a26de636-8efa-4df4-b56d-225ac25f31a8")).WithObjectTypes(Organisation, UserGroup).WithSingularName("CustomerContactUserGroup")  .WithPluralName("CustomerContactUserGroups")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("59500ed1-2de5-45ff-bec7-275c1941d153"), new Guid("bd699a2c-e1dc-48dd-9d0a-c1aec3b18f44"), new Guid("9501b51f-92e1-4ab8-862b-c6b6fd469b68")).WithObjectTypes(Organisation, Person).WithSingularName("CurrentContact")  .WithPluralName("CurrentContacts")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("786a74b0-015a-47db-8d3a-c790b326cc7d"), new Guid("6f7363d4-46c5-4bcb-b19c-314733af9e9e"), new Guid("1c339b5d-6f97-41bd-952a-3706d383c3d8")).WithObjectTypes(Organisation, Media).WithSingularName("LogoImage")  .WithPluralName("LogoImages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("78837f12-05d3-49f1-a607-43e96120bcf0"), new Guid("0df49189-f6a1-49cf-97c5-ab40e3087b6e"), new Guid("d03e4b6a-6741-4290-a590-18e32b4a6e43")).WithObjectTypes(Organisation, UserGroup).WithSingularName("PartnerContactUserGroup")  .WithPluralName("PartnerContactGroups")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("813633df-c6cb-44a6-9fdf-579aa8180ebd"), new Guid("4e4c1ca5-43e1-4567-8f1e-636197ca72b7"), new Guid("e5c40212-c5c5-44a1-8f18-f5d3dbeec9ca")).WithObjectTypes(Organisation, allorsString).WithSingularName("TaxNumber")  .WithPluralName("TaxNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("af80efaf-7ef1-4625-9717-564eef0504c4"), new Guid("ff2bb57b-4aaf-4c61-b282-6ce0852e8546"), new Guid("844af39b-fae2-4d94-9e67-ff6d97152736")).WithObjectTypes(Organisation, UserGroup).WithSingularName("SupplierContactUserGroup")  .WithPluralName("SupplierContactUserGroups")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // Responsibility
            new RelationTypeBuilder(domain, new Guid("a570dd47-5bb6-4a37-b73e-3a9f7b3f37ee"), new Guid("0f98ce04-447c-497c-b63b-f943eb818c84"), new Guid("9ccfe2ef-4980-43d8-9c5b-247c93c902b7")).WithObjectTypes(Responsibility, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // VatReturnBoxType
            new RelationTypeBuilder(domain, new Guid("95935a8e-fac5-4798-ba2d-1408d231f97b"), new Guid("d40e1048-b97b-4bae-9319-f4c05ec40484"), new Guid("44678a1f-9af2-404f-8eec-b50fb62737cb")).WithObjectTypes(VatReturnBoxType, allorsString).WithSingularName("Type")  .WithPluralName("Types")      .WithSize(256).Build();
			
            // WorkEffortFixedAssetAssignment
            new RelationTypeBuilder(domain, new Guid("2b6eca80-294c-4a2d-a15c-a57c0c815aa1"), new Guid("1c5736df-6218-45ce-8f86-f668d9dc7fe2"), new Guid("cfb5334c-5843-45f4-90f7-f7ea813e7ec4")).WithObjectTypes(WorkEffortFixedAssetAssignment, AssetAssignmentStatus).WithSingularName("AssetAssignmentStatus")  .WithPluralName("AssetAssignmentStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2d7dd4b3-a0bd-45aa-9d1a-a0ffa4a98061"), new Guid("6d66eb02-1eea-4b2e-8712-be6e1dde98be"), new Guid("e2327e4a-dd69-4e90-983b-dcf29b799201")).WithObjectTypes(WorkEffortFixedAssetAssignment, WorkEffort).WithSingularName("Assignment")  .WithPluralName("Assignment")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a2816fd1-babb-480c-8e29-0f7192aaff71"), new Guid("c02a3dc0-c977-4893-b7e1-691cfe0c1b03"), new Guid("c097dea3-8f6f-456c-b4cc-7c87c5441517")).WithObjectTypes(WorkEffortFixedAssetAssignment, allorsDecimal).WithSingularName("AllocatedCost")  .WithPluralName("AllocatedCosts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e90cb555-e6d9-4d7d-8d98-6f9c28c4bc14"), new Guid("739ac865-7c8c-45e3-b240-349e4092a56b"), new Guid("4087a04c-3f31-4c82-b7b1-bd5c7818981f")).WithObjectTypes(WorkEffortFixedAssetAssignment, FixedAsset).WithSingularName("FixedAsset")  .WithPluralName("FixedAssets")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // GeographicBoundaryComposite
            new RelationTypeBuilder(domain, new Guid("77d5f129-6096-45da-8b9f-39ef19276f1d"), new Guid("7484e00e-de39-4fbe-981a-aff3e693cf89"), new Guid("03ef822a-e2d3-43ba-9051-2c663593fb31")).WithObjectTypes(GeographicBoundaryComposite, GeographicBoundary).WithSingularName("Association")  .WithPluralName("Associations")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // CustomerRelationship
            new RelationTypeBuilder(domain, new Guid("009d073e-8c1b-4da5-8780-bd5bff43db0d"), new Guid("f7a9d8ed-4efa-4d39-a79d-ab1e3acda26c"), new Guid("bfdae8de-2880-47c5-afd1-93c9b9b24dab")).WithObjectTypes(CustomerRelationship, allorsBoolean).WithSingularName("BlockedForDunning")  .WithPluralName("BlockedsForDunning")      .Build();
            new RelationTypeBuilder(domain, new Guid("35f92e67-aedd-4e62-aa1b-57f6489c0083"), new Guid("995ccfb4-f1ca-4894-9fd9-fbf19e2226eb"), new Guid("96eaeb9a-8047-4068-9185-765fc0e48342")).WithObjectTypes(CustomerRelationship, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("42e3b2c4-376d-4e8b-bb49-2af031881ed0"), new Guid("bcdd31e8-8101-4b6b-8f13-a4397c43adfa"), new Guid("a9ddfe04-e5fd-4b22-9a9a-702dc0533731")).WithObjectTypes(CustomerRelationship, allorsDecimal).WithSingularName("AmountOverDue")  .WithPluralName("AmountsOverDue")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5c7c79e1-6b61-4f64-b8d1-608984f91268"), new Guid("9ce91d5f-12af-44a5-97a9-16c1b9986f67"), new Guid("74a36a15-f48a-4794-ac10-2c0860cc4ca1")).WithObjectTypes(CustomerRelationship, Party).WithSingularName("Customer")  .WithPluralName("Customers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("894f4ff2-9c41-4201-ad36-ac10dafd65dd"), new Guid("c8a336f0-4fae-4ce6-a900-283066052ffd"), new Guid("11fa6c6e-c528-452c-adca-75f474d2f95b")).WithObjectTypes(CustomerRelationship, allorsDecimal).WithSingularName("AmountDue")  .WithPluralName("AmountsDue")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a484eb38-4beb-495c-9c54-522238e0e639"), new Guid("03c7d5c4-4c80-4511-9ab2-2745f3f17596"), new Guid("f4a2fdef-d91a-4c7e-94d5-ebe13ab94338")).WithObjectTypes(CustomerRelationship, allorsDecimal).WithSingularName("YTDRevenue")  .WithPluralName("YTDRevenues")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("af50ade8-5964-4963-819d-c87689c6434e"), new Guid("a06dda1c-d91d-4e27-b293-05cb53de65ec"), new Guid("7f6da6ca-b069-47f6-983c-6e33d65ffd0e")).WithObjectTypes(CustomerRelationship, allorsDateTime).WithSingularName("LastReminderDate")  .WithPluralName("LastReminderDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("dd59ed76-b6da-49a3-8c3e-1edf4d1d0900"), new Guid("e2afe553-7bbd-4f81-97e8-7279defb49ca"), new Guid("b5e30743-6adc-4bf0-b547-72b17b79879c")).WithObjectTypes(CustomerRelationship, allorsDecimal).WithSingularName("CreditLimit")  .WithPluralName("CreditLimits")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e3a06a1c-998a-4871-8f0e-2f166eac6c7b"), new Guid("08dfdeb5-1a62-42d6-b8f3-16025960b09f"), new Guid("9400c681-2a68-4842-89fd-3c9ccb3f2a96")).WithObjectTypes(CustomerRelationship, allorsInteger).WithSingularName("SubAccountNumber")  .WithPluralName("SubAccountNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("e924ea41-ae61-4cf1-9bf4-4661497289c1"), new Guid("b6d5c0a6-f5b4-43df-952f-0a5f82b68b1f"), new Guid("d4bbb472-cf06-4569-9d91-00ee3a98eb41")).WithObjectTypes(CustomerRelationship, allorsDecimal).WithSingularName("LastYearsRevenue")  .WithPluralName("LastYearsRevenues")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
			
            // PartyClassification
            new RelationTypeBuilder(domain, new Guid("4f35ae7e-fe06-4a3b-abe1-adb78fcf2e6b"), new Guid("fd171d61-90ae-4169-8286-6054b82569a1"), new Guid("654f2aca-2eb7-495c-a739-82c38a629130")).WithObjectTypes(PartyClassification, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // Party
            new RelationTypeBuilder(domain, new Guid("01771db8-e79c-4ce4-9d81-db3675e8708a"), new Guid("c6dbe58e-fa09-408b-9324-21fcec3b1900"), new Guid("aebbe259-2619-45bb-9751-68f61a230159")).WithObjectTypes(Party, allorsDecimal).WithSingularName("YTDRevenue")  .WithPluralName("YTDRevenues")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("04bc4912-cd23-4b2e-973c-76bbf2f2de8d"), new Guid("c369193b-d01b-4f82-83f3-27ecaa3d8d58"), new Guid("ef73d811-7d6a-4168-819f-1588b01979e8")).WithObjectTypes(Party, allorsDecimal).WithSingularName("LastYearsRevenue")  .WithPluralName("LastYearsRevenues")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("130d6e94-51e2-45f9-82d7-380ae7c8aa44"), new Guid("68c1c826-9915-4f7b-8a44-dc62e215b260"), new Guid("e47aa296-12fa-45f1-8deb-0f151aaaba60")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("BillingInquiriesFax")  .WithPluralName("BillingInquiriesFaxes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("19c8a5a0-9567-4fc2-bfad-94a549cfa191"), new Guid("b8622d0f-ba18-4a76-b1d9-25115378c01c"), new Guid("6656341b-4b2a-41a3-abad-9aece1294b79")).WithObjectTypes(Party, Qualification).WithSingularName("Qualification")  .WithPluralName("Qualifications")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1ad85fce-f2f8-45aa-bf1e-8f5ade34153c"), new Guid("20dd50d2-06c8-48e8-883d-5f894c973834"), new Guid("e3834580-66fc-4b4d-b0fa-58e19f660316")).WithObjectTypes(Party, ContactMechanism).WithSingularName("HomeAddress")  .WithPluralName("HomeAddresses")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1d4e59a6-253f-470e-b9a7-c2c73b67cf2f"), new Guid("996ea544-3d27-410d-aa23-25457532e3b1"), new Guid("90f0a491-c7c7-4ff5-9910-77d430f6292a")).WithObjectTypes(Party, ContactMechanism).WithSingularName("SalesOffice")  .WithPluralName("SalesOffices")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("29da9212-a70f-4ee6-98d7-508687faa2b4"), new Guid("6798142d-fefe-40a1-86c2-7788e1961fcb"), new Guid("895e8823-ae01-41d9-b0d1-055fadf45c71")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("OrderInquiriesFax")  .WithPluralName("OrderInquiriesFaxes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("420a7279-ba09-4660-bf5d-7242be07bfb1"), new Guid("3bb65209-69d2-40e5-890b-c8a9e06da1ac"), new Guid("8f1be044-6b43-4861-b995-fdc080656670")).WithObjectTypes(Party, Person).WithSingularName("CurrentSalesRep")  .WithPluralName("CurrentSalesReps")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("42ab0c4b-52b2-494e-b6a9-cacf55fb002e"), new Guid("32d52b42-f5cc-4fd0-959c-045ff0c02520"), new Guid("977a3626-85af-47a8-bfe8-ed2e8daa1d9e")).WithObjectTypes(Party, PartyContactMechanism).WithSingularName("PartyContactMechanism")  .WithPluralName("PartyContactMechanisms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("436f0ef1-a3ea-439c-9ffd-211c177f5ed1"), new Guid("1b9df170-befb-46e9-ba07-5a1b4b77e150"), new Guid("a1f5ff98-c126-47f8-b5f6-72180319a847")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("ShippingInquiriesFax")  .WithPluralName("ShippingInquiriesFaxes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4444b0d1-4ade-4fed-88bf-ce9ef275a978"), new Guid("94602440-bdea-4b49-9fe3-15b0d483c632"), new Guid("9d65c05b-562b-4b31-b717-4247b8086f5b")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("ShippingInquiriesPhone")  .WithPluralName("ShippingInquiriesPhones")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4a46f6aa-d4f9-4e5e-ac17-d77ab0e99c3f"), new Guid("ba75f426-3a2a-4341-ac95-3562c608d83b"), new Guid("9dd1757a-f31e-4fe1-9195-0a8403f0108a")).WithObjectTypes(Party, BillingAccount).WithSingularName("BillingAccount")  .WithPluralName("BillingAccounts")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4d742fa8-f10b-423e-9341-f8a526838eba"), new Guid("bd9d5e4f-8c3a-4787-8c5a-1e3f9f49db97"), new Guid("f9bcbb5a-6c10-4fa9-8601-82c6fb941f3b")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("OrderInquiriesPhone")  .WithPluralName("OrderInquiriesPhones")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4e725bd6-2280-48a2-be89-836b4bd7d002"), new Guid("9d7f6130-f2ba-4da0-9b74-91b6205e42be"), new Guid("eb6079ed-489b-4673-8508-7a9a6e33573f")).WithObjectTypes(Party, PartySkill).WithSingularName("PartySkill")  .WithPluralName("PartySkills")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4e787cf8-9b92-4ab2-8f88-c08bdb90a376"), new Guid("66778fc1-8d7c-4976-afe1-e07fd4567c46"), new Guid("766900b8-646c-4b59-b022-5143cf5e5ce9")).WithObjectTypes(Party, PartyClassification).WithSingularName("PartyClassification")  .WithPluralName("PartyClassifications")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("52863081-34b7-48e2-a7ff-c6bd67172350"), new Guid("7ab0f4b0-f4ae-45d4-8c9e-a576f36e4f1a"), new Guid("09d4533e-d118-4395-a7f1-358aad00f6e4")).WithObjectTypes(Party, allorsBoolean).WithSingularName("ExcludeFromDunning")  .WithPluralName("ExcludesFromDunning")      .Build();
            new RelationTypeBuilder(domain, new Guid("52dd7bf8-bb7e-47bd-85b3-f35fba964e5c"), new Guid("3eac7011-d5ed-46ce-a678-b1e3a6c02962"), new Guid("fb2c26d4-c23c-4817-94ee-5f2acebb4e41")).WithObjectTypes(Party, BankAccount).WithSingularName("BankAccount")  .WithPluralName("BankAccounts")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("70ada4aa-c51c-4f1d-a3d2-ea6de31cb988"), new Guid("9f1ea588-8dd9-4f48-a905-0271e694f1fe"), new Guid("f2455f15-83f5-4599-9b2e-c1b8d9b92995")).WithObjectTypes(Party, ContactMechanism).WithSingularName("BillingAddress")  .WithPluralName("BillingAddresses")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("79a5c25a-91e9-4a80-8649-c8abe86e47dd"), new Guid("39d03d8f-8fbc-4131-8e97-7f5fcf73871c"), new Guid("711fc18b-b5f8-4235-8a51-22f91e4c194e")).WithObjectTypes(Party, ShipmentMethod).WithSingularName("DefaultShipmentMethod")  .WithPluralName("DefaultShipmentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7dc1e326-76ef-4bac-aae1-d6a26da9d40a"), new Guid("5b8c7f22-121d-473f-83e0-41f20740b912"), new Guid("468e863c-79f9-48a1-a28e-ad6159940b01")).WithObjectTypes(Party, Resume).WithSingularName("Resume")  .WithPluralName("Resumes")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("89971e75-61e5-4a0c-b7fc-6f4c15866175"), new Guid("ef2f1c0e-ecc2-4949-aec9-88460c0d5b0b"), new Guid("d80cc262-207a-462e-b8ed-ee58f04cf98b")).WithObjectTypes(Party, ContactMechanism).WithSingularName("HeadQuarter")  .WithPluralName("HeadQuarters")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("90590830-da80-4afd-ac37-e9fafb59493a"), new Guid("79b4d3ba-70cc-4914-82ee-d06e11ac7b2c"), new Guid("71133938-89e1-45f6-8e5e-6ef699d44db1")).WithObjectTypes(Party, ElectronicAddress).WithSingularName("PersonalEmailAddress")  .WithPluralName("PersonalEmailAddresses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("92c99262-30ed-4265-975b-07140c46af6e"), new Guid("71b74bf9-8b50-4f81-9f52-0a06cc223ba9"), new Guid("bc0d1d88-3811-4fdf-b1c7-4ad2d82230cf")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("CellPhoneNumber")  .WithPluralName("CellPhoneNumbers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("95f6db56-0dcf-4d5e-8e81-43e0d72faa85"), new Guid("d47edd54-4d98-428d-9cb9-d57e0e7816f1"), new Guid("14ed3b75-2787-4abf-be44-408ca2945384")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("BillingInquiriesPhone")  .WithPluralName("BillingInquiriesPhones")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9d361ab3-c93a-41e0-bbca-0cde08bcff37"), new Guid("4e3e530b-456a-405e-8b22-8691647d1258"), new Guid("4d210b02-9045-4be4-a49d-c728b9b0d2ed")).WithObjectTypes(Party, allorsString).WithSingularName("PartyName")  .WithPluralName("PartyNames")  .WithIsDerived(true)    .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a7720655-a6c1-4f54-a093-b77da985ac5f"), new Guid("4f9183c0-bac1-4738-97e3-15c2906759e8"), new Guid("d1e7a633-f097-4030-b3c3-9167c022fe05")).WithObjectTypes(Party, ContactMechanism).WithSingularName("OrderAddress")  .WithPluralName("OrderAddresses")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ac5a48dc-4115-489a-aa8c-f43268b6bfe3"), new Guid("97686b93-4c5f-4544-af6a-acacca008060"), new Guid("bf8f9ba5-7a88-4ad4-b154-09b5efae9912")).WithObjectTypes(Party, ElectronicAddress).WithSingularName("InternetAddress")  .WithPluralName("InternetAddresses")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("acf731ab-c856-4553-a2fc-9f88e3ccc258"), new Guid("c75a6014-98bd-4e2f-b526-1e2cfda9534c"), new Guid("87d94438-3756-42cd-9356-9d169ce42817")).WithObjectTypes(Party, Media).WithSingularName("Content")  .WithPluralName("Contents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("aecedf16-9e42-4e49-b7ec-e92187262405"), new Guid("41d4ebe2-dcf3-4517-9ce5-2c1dcc45400d"), new Guid("af648b7c-4407-46b8-8070-76d86a48c605")).WithObjectTypes(Party, CreditCard).WithSingularName("CreditCard")  .WithPluralName("CreditCards")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c20f82fa-3ba2-4e84-beef-52ba30c92695"), new Guid("0c9edf90-b6fd-476b-86e8-ca1b845ee62b"), new Guid("5da6410e-1311-4664-a0b3-ee2fca4b9ad1")).WithObjectTypes(Party, PostalAddress).WithSingularName("ShippingAddress")  .WithPluralName("ShippingAddresses")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d05ee314-57be-4852-a3b5-62710df4d4b7"), new Guid("87821f12-6fed-4376-b239-6d2296457b88"), new Guid("a3a1df78-5469-41ae-bdc5-24c340abc378")).WithObjectTypes(Party, allorsDecimal).WithSingularName("OpenOrderAmount")  .WithPluralName("OpenOrderAmounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d562d1f0-1f8f-40c5-a346-ae32e498f332"), new Guid("8dab565f-7386-4037-843f-bfc3603b27ab"), new Guid("10c1c77e-4b1b-4fd5-b77f-95e8897a4b38")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("GeneralFaxNumber")  .WithPluralName("GeneralFaxNumbers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d97ab83b-85dc-4877-8b49-1e552489bcb0"), new Guid("4af97ea0-bb6b-4fdb-9e0d-798805ccad53"), new Guid("9c644a11-4239-49df-b603-489c547e2085")).WithObjectTypes(Party, PaymentMethod).WithSingularName("DefaultPaymentMethod")  .WithPluralName("DefaultPaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e2017090-fa3f-420e-a5c5-6a2f5aaacd2f"), new Guid("84c30383-6d26-4abe-92a3-d750e41d2561"), new Guid("51170ba2-d717-41dc-9d6b-18967c37e751")).WithObjectTypes(Party, TelecommunicationsNumber).WithSingularName("GeneralPhoneNumber")  .WithPluralName("GeneralPhoneNumbers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f0de022f-b94e-4d29-8cdf-99d39ad9add6"), new Guid("81236f57-51e8-4863-b796-419685199990"), new Guid("a736d5be-33ec-4449-a23d-b4a83a0f4bc3")).WithObjectTypes(Party, Currency).WithSingularName("PreferredCurrency")  .WithPluralName("PreferredCurrencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fafa35a1-7762-47f7-a9c2-28d3d0623e7c"), new Guid("ef3ddd5a-7f11-4191-8098-18fa958f7f93"), new Guid("68f80581-9c1f-4f02-88dc-e6119ab6d135")).WithObjectTypes(Party, VatRegime).WithSingularName("VatRegime")  .WithPluralName("VatRegimes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartyProductCategoryRevenue
            new RelationTypeBuilder(domain, new Guid("05de234d-6f00-49b2-802d-fcc590cc1aec"), new Guid("93a67619-2960-49e8-97e1-813614df8a32"), new Guid("fba6ebaf-efbd-465d-b142-b754c34af161")).WithObjectTypes(PartyProductCategoryRevenue, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("068c3c16-2827-4574-9fe6-323e74634db0"), new Guid("2948c509-a13f-4f3b-9cad-b39ce9bdb3c7"), new Guid("337d69c4-0565-42bc-a0cf-3c76dde9115f")).WithObjectTypes(PartyProductCategoryRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1f41a4c2-a9af-42e1-92e4-a9069bc024b1"), new Guid("42f9808e-94c3-4fda-b822-4a02e4a7648c"), new Guid("6a6ca0a9-f262-4891-98c5-f6abb1adf93f")).WithObjectTypes(PartyProductCategoryRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("4584c4f9-12a5-4435-a6b8-2b2e6bb932b9"), new Guid("852fe55f-9f03-4542-90f4-ef4fceb560a3"), new Guid("013068f6-131a-4d0c-9847-d772ba9d3596")).WithObjectTypes(PartyProductCategoryRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4fff87cc-3232-4174-9a91-9d9ee0192360"), new Guid("57a27625-0f61-4026-aabf-9a7de257d133"), new Guid("38fd22de-76e6-4887-9148-f99015e4816b")).WithObjectTypes(PartyProductCategoryRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("5b77f9ce-15c5-4fb3-95d6-11d2cc1aca95"), new Guid("5609dc7f-0354-4ed0-a6cf-120fd41d3eb9"), new Guid("3d418a0d-f737-4bf8-856e-a94f8f7af774")).WithObjectTypes(PartyProductCategoryRevenue, allorsString).WithSingularName("PartyProductCategoryName")  .WithPluralName("PartyProductCategoryNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7f6972f9-16dc-4795-9e4b-7095738e80ed"), new Guid("20ecc315-ee8c-4bd2-9320-f16d258db9bc"), new Guid("9cf8972a-71d1-465e-bf08-6713c695a29a")).WithObjectTypes(PartyProductCategoryRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8e0338d5-5fab-4024-9e37-05afd05aa514"), new Guid("a9c15cc8-1066-4c43-bd85-a2a306a2b5d1"), new Guid("e4393c39-4047-46ba-8335-68644a69413b")).WithObjectTypes(PartyProductCategoryRevenue, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b0b50eec-0a88-41e8-895a-270517240b7b"), new Guid("0af942bf-8bd7-4297-93ff-11971dbd12ce"), new Guid("53055a2e-dbf8-40a9-a1d4-50297bfe38c1")).WithObjectTypes(PartyProductCategoryRevenue, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
			
            // PartyFixedAssetAssignment
            new RelationTypeBuilder(domain, new Guid("28afdc0d-ebc7-4f53-b5a1-0cc0eb377887"), new Guid("8d6a5121-c704-4f04-95de-7e2ab8faecea"), new Guid("e9058932-6beb-4698-89b9-c70e98b30b7f")).WithObjectTypes(PartyFixedAssetAssignment, FixedAsset).WithSingularName("FixedAsset")  .WithPluralName("FixedAssets")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("59187015-4689-4ef8-942f-c36ff4c74e64"), new Guid("4f0c5035-bfd2-4843-8d6e-d3df15a7f5dd"), new Guid("38f3a7f5-53b5-4572-bcb0-347fa3a543f3")).WithObjectTypes(PartyFixedAssetAssignment, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("70c38a47-79c4-4ec8-abfd-3c40ef4239ea"), new Guid("874b5fdc-a8b9-4b7c-9785-15661917b57a"), new Guid("f243ed6d-eabc-4363-ba37-cf147a166081")).WithObjectTypes(PartyFixedAssetAssignment, AssetAssignmentStatus).WithSingularName("AssetAssignmentStatus")  .WithPluralName("AssetAssignmentStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c70f014b-345b-48ad-8075-2a1835a19f57"), new Guid("95b448b4-4fc5-4bd5-b789-e967de001bbe"), new Guid("aa4aca33-b94c-4527-97db-558fab6805a5")).WithObjectTypes(PartyFixedAssetAssignment, allorsDecimal).WithSingularName("AllocatedCost")  .WithPluralName("AllocatedCosts")      .WithPrecision(19).WithScale(2).Build();
			
            // MarketingPackage
            new RelationTypeBuilder(domain, new Guid("29cb7841-1793-43c3-bcbe-3d69a8e651b5"), new Guid("49e615c2-afec-4d3a-90d9-eb19840e2bf0"), new Guid("1a8a5695-acad-4cfb-a228-1f05610d56fa")).WithObjectTypes(MarketingPackage, allorsString).WithSingularName("Instruction")  .WithPluralName("Instructions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("70c7d06c-2086-4a60-b2b9-aba2c6f07669"), new Guid("23ec81db-27f0-4965-bf25-4f0150fd4281"), new Guid("02896b5c-8c38-40d3-89e3-9b3a0d209d3f")).WithObjectTypes(MarketingPackage, Product).WithSingularName("ProductUsedIn")  .WithPluralName("ProductsUsedIn")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a687e8ff-624c-4794-866f-f4cc653d874c"), new Guid("7d1b384b-4730-4e61-8a80-8d18ea8e2ae4"), new Guid("ea685b4f-3063-47a9-ba82-980247e903af")).WithObjectTypes(MarketingPackage, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ccabc13b-63cc-4cdf-909d-411edc26d648"), new Guid("31ac20b1-d41d-4aa5-881b-708e38849017"), new Guid("179d1a3b-8325-49da-9009-f104802f189d")).WithObjectTypes(MarketingPackage, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("dc3c4217-5c42-4ac3-ad16-33f50653bcfc"), new Guid("82eaf783-4f29-4ede-a285-a7540d0d5f62"), new Guid("fe881a55-eafb-4f83-985f-bb39cff3d2bc")).WithObjectTypes(MarketingPackage, allorsInteger).WithSingularName("QuantityUsed")  .WithPluralName("QuantitiesUsed")      .Build();
			
            // ItemIssuance
            new RelationTypeBuilder(domain, new Guid("60089b34-e9aa-4b09-9a5c-4523ce60152f"), new Guid("ddf8eba9-8821-490f-9d9d-adc6ebd32ddb"), new Guid("ee8e4f06-63e8-4281-a010-9f9212244cf1")).WithObjectTypes(ItemIssuance, allorsDateTime).WithSingularName("IssuanceDateTime")  .WithPluralName("IssuanceDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("6d0e1669-1583-4004-a0dd-6481faaa4803"), new Guid("2deb9c3e-6e3e-462c-88bf-df682a4af6e0"), new Guid("d8e7874c-a162-440a-8e99-4dd7b07216cd")).WithObjectTypes(ItemIssuance, InventoryItem).WithSingularName("InventoryItem")  .WithPluralName("InventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("72872b29-69e3-4408-ad61-80201c46421b"), new Guid("f191b03b-fb03-4c5b-9455-57d241160e3b"), new Guid("69dca6e4-7d13-481c-8a77-ff4c365df923")).WithObjectTypes(ItemIssuance, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("83de0bfa-98ca-4299-a529-f8ba8a02cb90"), new Guid("467ce53a-969b-4537-b51c-998ac64afbe9"), new Guid("1766b9c8-436d-427c-8c54-4f10a6accf02")).WithObjectTypes(ItemIssuance, ShipmentItem).WithSingularName("ShipmentItem")  .WithPluralName("ShipmentItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("af4fbe17-bbdc-4f05-bf2e-398ee18598a5"), new Guid("6744410c-6f9c-49db-b73c-ed723592fee6"), new Guid("938bb734-f18c-4756-9c68-54cad2377639")).WithObjectTypes(ItemIssuance, PickListItem).WithSingularName("PickListItem")  .WithPluralName("PickListItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ShipmentPackage
            new RelationTypeBuilder(domain, new Guid("293eb102-b098-4e5d-8cef-d5e0b4f1ca5d"), new Guid("24a2efe7-c10e-4cb0-807b-3c3ae7d4361f"), new Guid("82196a58-d9ba-4508-bdbf-84964f2d2590")).WithObjectTypes(ShipmentPackage, PackagingContent).WithSingularName("PackagingContent")  .WithPluralName("PackagingContents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7f009302-d4f4-4b06-9e18-fb1c35bd79e7"), new Guid("30cfc1be-1131-4914-888f-30f29e770332"), new Guid("7d4a4b20-3424-43b5-a7cf-7e9422c5870d")).WithObjectTypes(ShipmentPackage, Document).WithSingularName("Document")  .WithPluralName("Documents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("afd7e182-d201-4eee-803c-9fb4dff0feed"), new Guid("5b2b0551-afcb-4cc3-863e-ba351492da45"), new Guid("d00256d2-fbc8-4935-bfe9-0b0843622936")).WithObjectTypes(ShipmentPackage, allorsDateTime).WithSingularName("CreationDate")  .WithPluralName("CreationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("d767222a-b528-4a3f-ac3f-333de19f7ae1"), new Guid("d1d55767-7b92-49fa-891e-8b701bd56213"), new Guid("a6e84f4d-ebde-4ca8-9cee-57642f0dc41e")).WithObjectTypes(ShipmentPackage, allorsInteger).WithSingularName("SequenceNumber")  .WithPluralName("SequenceNumbers")  .WithIsDerived(true)    .Build();
			
            // PerformanceNote
            new RelationTypeBuilder(domain, new Guid("1b8f0ada-bb5c-4226-8e35-5f1c40b06fc8"), new Guid("e4ae1691-22f8-4304-8e04-73ae41420b43"), new Guid("1d396f6f-279d-4b83-9d95-6ece6089f6a0")).WithObjectTypes(PerformanceNote, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2f6ed687-4200-4a27-bfb2-922d9ce2e38f"), new Guid("5f2b047e-2cb0-4d2a-9cce-77846ad35f45"), new Guid("f21bbf2d-0780-4bbf-92e6-2c6676b4893d")).WithObjectTypes(PerformanceNote, allorsDateTime).WithSingularName("CommunicationDate")  .WithPluralName("CommunicationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("5bf234d2-8486-47b2-a770-eca36b44bb67"), new Guid("cc9f9a6f-54fc-4786-9d83-2769d8d921ce"), new Guid("0467f9fa-17e9-4fdc-b74a-39d074e55b16")).WithObjectTypes(PerformanceNote, Person).WithSingularName("GivenByManager")  .WithPluralName("GivenByManagers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a8cd7bf6-6bea-44ad-9e89-1bd63ffca459"), new Guid("c4a4e475-613b-4e38-bb79-b5bd12f73332"), new Guid("06b721ea-20ec-4b18-bd5c-d6d3e86610bd")).WithObjectTypes(PerformanceNote, Person).WithSingularName("Employee")  .WithPluralName("Employees")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // DeliverableTurnover
            new RelationTypeBuilder(domain, new Guid("5c9b7809-0cb0-4282-ae2b-20407126384d"), new Guid("8e050223-57c1-47b2-b5b4-bdb93840f527"), new Guid("8d3abfcb-f4de-4d6b-9427-b7906430a178")).WithObjectTypes(DeliverableTurnover, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // ShipmentReceipt
            new RelationTypeBuilder(domain, new Guid("0c4eee66-ff66-49fa-9a06-4ce3848a6d3c"), new Guid("d67a1bb9-802a-47a9-97bd-28809cd5c85a"), new Guid("89d49ef1-a3b6-4404-97d9-024c66e0a1f6")).WithObjectTypes(ShipmentReceipt, allorsString).WithSingularName("ItemDescription")  .WithPluralName("ItemDescriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2bbc4476-7a06-4c36-9985-68a60b72eacd"), new Guid("c8ca8009-f3e9-4154-a94a-9e60f6165f3a"), new Guid("5e776569-8dd4-4dd2-993b-5bbccc15ca58")).WithObjectTypes(ShipmentReceipt, NonSerializedInventoryItem).WithSingularName("InventoryItem")  .WithPluralName("InventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("87f84720-1233-4779-be9d-4b0a12ba19cd"), new Guid("77a773f7-e649-4dd1-9dd9-d7a5eb09ae95"), new Guid("9cbd890b-c0b5-4a0c-a931-fc5601b5ef0d")).WithObjectTypes(ShipmentReceipt, allorsString).WithSingularName("RejectionReason")  .WithPluralName("RejectionReasons")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9a76f8ba-ae96-4040-81ce-59330392e77a"), new Guid("ca64ae22-fc6c-4747-a04b-ac77911c0c5e"), new Guid("1d77d632-e552-4745-a5d6-ebefc3f0ec06")).WithObjectTypes(ShipmentReceipt, OrderItem).WithSingularName("OrderItem")  .WithPluralName("OrderItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9a9cce59-f45c-4da0-adb6-9583a1694921"), new Guid("8cd7d5ad-ca46-4fb2-9df0-edd213680dd6"), new Guid("1f523e25-d883-4207-8550-d8d2c95c2ac6")).WithObjectTypes(ShipmentReceipt, allorsDecimal).WithSingularName("QuantityRejected")  .WithPluralName("QuantitiesRejected")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ccd41d3d-2be8-47ca-8217-4e2aa1d1c03b"), new Guid("e823098b-5333-4466-b845-fe4a4f1b09f5"), new Guid("7ec7aeb3-abdf-4bdc-bee2-535d8a722a6b")).WithObjectTypes(ShipmentReceipt, ShipmentItem).WithSingularName("ShipmentItem")  .WithPluralName("ShipmentItems")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ecdd6b27-3bcf-4f61-8e21-f829503aeeb0"), new Guid("b326cf9d-8770-4686-a7f5-2061d1683bb4"), new Guid("82ef73a5-8d4e-44a0-a551-b0c1dee958ca")).WithObjectTypes(ShipmentReceipt, allorsDateTime).WithSingularName("ReceivedDateTime")  .WithPluralName("ReceivedDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("f057b89e-3688-4172-9efa-102298c7e0e4"), new Guid("5d2edcc9-1f42-44b1-8680-fd2cd02761a0"), new Guid("b6f78ebd-95a9-4649-b19f-bf965dc60150")).WithObjectTypes(ShipmentReceipt, allorsDecimal).WithSingularName("QuantityAccepted")  .WithPluralName("QuantitiesAccepted")      .WithPrecision(19).WithScale(2).Build();
			
            // RequirementCommunication
            new RelationTypeBuilder(domain, new Guid("5a4d9541-4a8a-4661-bec3-e65db5298857"), new Guid("d7103ab4-c796-4efd-83bd-256e90c40a14"), new Guid("8edb2d05-b8aa-4d09-90ef-79ce9051df66")).WithObjectTypes(RequirementCommunication, CommunicationEvent).WithSingularName("CommunicationEvent")  .WithPluralName("CommunicationEvents")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b65140b1-8dc4-4836-9ad8-fe01f43dad7a"), new Guid("b2ddd7e5-fa91-4257-9400-f776787fffb7"), new Guid("09fb424a-eece-4617-bc65-9fb6861eeb3b")).WithObjectTypes(RequirementCommunication, Requirement).WithSingularName("Requirement")  .WithPluralName("Requirements")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cdb72b3f-9920-4082-83a7-a0211a29cf77"), new Guid("f0743736-d40a-4831-a075-8bdd33cb68f6"), new Guid("208ee5d1-7f60-4c12-888f-04f25c38bc46")).WithObjectTypes(RequirementCommunication, Person).WithSingularName("AssociatedProfessional")  .WithPluralName("AssociatedProfessionals")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // FixedAsset
            new RelationTypeBuilder(domain, new Guid("354107ce-4eb6-4b9a-83c8-5cfe5e3adb22"), new Guid("e0f80027-f068-4ff8-a351-b3199f92735f"), new Guid("6806756e-a152-42a9-b32b-b14269e712e2")).WithObjectTypes(FixedAsset, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("51133e4d-5135-4991-9f2f-8df9762fac78"), new Guid("fc2144b7-4a88-412d-9792-ba6f6c93c637"), new Guid("1cc0737e-a810-48d3-b048-7e3077d3db5c")).WithObjectTypes(FixedAsset, allorsDateTime).WithSingularName("LastServiceDate")  .WithPluralName("LastServiceDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("54cf9225-9204-43ee-9984-7fd8b2cbf8bc"), new Guid("efb718b5-7d70-4696-81c8-961582ed01f2"), new Guid("99c0a722-af34-4008-b7f5-dc4315c7fa1a")).WithObjectTypes(FixedAsset, allorsDateTime).WithSingularName("AcquiredDate")  .WithPluralName("AcquiredDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("725c6b7d-68ed-4576-8b17-eac4e9f4db83"), new Guid("ce93a11b-7c87-4d9c-9d79-a9703a9fd86d"), new Guid("96524022-ff94-482a-a17c-6c3c96f79127")).WithObjectTypes(FixedAsset, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("913cc338-f844-49ae-886a-2e32db190b78"), new Guid("276b6fca-d2bb-4e43-af51-378c599c80f6"), new Guid("f409664f-5c7e-4f3b-809c-acd43c36b3bc")).WithObjectTypes(FixedAsset, allorsDecimal).WithSingularName("ProductionCapacity")  .WithPluralName("ProductionCapacities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ead0e86a-dfc7-45b0-9865-b973175c4567"), new Guid("6be614a2-0511-4ca0-9b1c-c8a3d0b0a998"), new Guid("47d9d93c-8ba3-4f28-a8a5-6a4cb02853e6")).WithObjectTypes(FixedAsset, allorsDateTime).WithSingularName("NextServiceDate")  .WithPluralName("NextServiceDates")      .Build();
			
            // ServiceEntry
            new RelationTypeBuilder(domain, new Guid("385eac5a-a588-4f30-b4df-a4b07be43d88"), new Guid("36477b8a-7c51-4fe6-bd6f-44e6205fb1bd"), new Guid("ed9b3483-c2a2-4572-9346-35ed621500b9")).WithObjectTypes(ServiceEntry, allorsDateTime).WithSingularName("ThroughDateTime")  .WithPluralName("ThroughDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("74fc8f9b-62f3-4921-bce1-ca10eed33204"), new Guid("987c6fb3-b512-4797-933d-28424500649e"), new Guid("1bbf98fb-fb84-45e7-b3f3-c6d5bb9b155c")).WithObjectTypes(ServiceEntry, EngagementItem).WithSingularName("EngagementItem")  .WithPluralName("EngagementItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9b04b715-376f-4c39-b78b-f92af6b4ffc1"), new Guid("2c25dc8f-c253-471e-87fb-fe6934cf2b15"), new Guid("b80138a0-0a0b-4a3a-8fbb-5bca2dc8c84c")).WithObjectTypes(ServiceEntry, allorsBoolean).WithSingularName("IsBillable")  .WithPluralName("AreBillable")      .Build();
            new RelationTypeBuilder(domain, new Guid("a4246c12-e77c-41e0-9f00-995fab17c13c"), new Guid("eef2f215-f262-4f7e-b87b-a8229b1d5d4b"), new Guid("f1ff8c32-0f88-49b9-83c1-b0754d65700e")).WithObjectTypes(ServiceEntry, allorsDateTime).WithSingularName("FromDateTime")  .WithPluralName("FromDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("a6ae42bd-babf-44e1-bdc0-cc403e56e43e"), new Guid("47acb5ae-b805-494e-9a44-10e2ddccec80"), new Guid("04df18b1-b92d-437d-a666-852c85e64330")).WithObjectTypes(ServiceEntry, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b9bb6409-c6b9-4a4b-9d46-02c62b4b3304"), new Guid("c4b7a55c-d0d9-429f-9577-d32de5b6f0cd"), new Guid("f624973f-1a6a-4cd6-930f-ecfb4d3772ec")).WithObjectTypes(ServiceEntry, WorkEffort).WithSingularName("WorkEffort")  .WithPluralName("WorkEfforts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // GeneralLedgerAccountGroup
            new RelationTypeBuilder(domain, new Guid("3ab2ad60-3560-4817-9862-7f60c55bbc32"), new Guid("5ab6a428-e5e3-4265-8263-0e4ead0cb5f5"), new Guid("b8f88fa3-9f8e-4e2c-be79-df02a37cfa40")).WithObjectTypes(GeneralLedgerAccountGroup, GeneralLedgerAccountGroup).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a48c3601-3d4c-43af-9502-d6beda764118"), new Guid("04b08f63-a2ac-43c2-889d-dbc8ebe86483"), new Guid("7bd5e5e8-8605-46b2-b174-f345feb60f31")).WithObjectTypes(GeneralLedgerAccountGroup, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // SerializedInventoryItem
            new RelationTypeBuilder(domain, new Guid("91567156-36e6-4a41-abc0-b039b9503840"), new Guid("27610bef-70f7-436a-a606-1e2aa043e8f6"), new Guid("0b8cd4fd-40da-453c-8dbc-6e7742cdfd7e")).WithObjectTypes(SerializedInventoryItem, SerializedInventoryItemObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a07e8bbb-7bf3-42e1-bcc2-d922a180f5e0"), new Guid("035a8f39-9b2f-403c-ae64-c43299d59ac2"), new Guid("e53e4d41-6518-4008-a419-522145e712af")).WithObjectTypes(SerializedInventoryItem, SerializedInventoryItemStatus).WithSingularName("InventoryItemStatus")  .WithPluralName("InventoryItemStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("de9caf09-6ae7-412e-b9bc-19ece66724da"), new Guid("ba630eb8-3087-43c6-9082-650094a0226e"), new Guid("c0ada954-d86e-46c3-9a99-09209fb812a5")).WithObjectTypes(SerializedInventoryItem, allorsString).WithSingularName("SerialNumber")  .WithPluralName("SerialNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e0fe2033-85a9-428d-9918-f543fbcf3ed7"), new Guid("49e8ccb2-8a3f-4846-8067-9f68d005e44f"), new Guid("9d19f214-3ed9-4e2d-a924-2d513ca01934")).WithObjectTypes(SerializedInventoryItem, SerializedInventoryItemObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fdc2607c-1081-4836-8aa5-1efb96e38da4"), new Guid("dc285060-57aa-4941-9335-c1b6e273f162"), new Guid("82b912e8-34f9-4a11-a33b-4fdeb7e54ffc")).WithObjectTypes(SerializedInventoryItem, SerializedInventoryItemStatus).WithSingularName("CurrentInventoryItemStatus")  .WithPluralName("CurrentInventoryItemStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // RespondingParty
            new RelationTypeBuilder(domain, new Guid("13f84c6c-d44a-4cc2-8898-bc2cbaed04f4"), new Guid("88a8016f-ecd7-4085-82d0-a9698d078184"), new Guid("72177a66-0459-4b6e-a8ea-2b5786e09f31")).WithObjectTypes(RespondingParty, allorsDateTime).WithSingularName("SendingDate")  .WithPluralName("SendingDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("1d220b47-44de-4ab9-9219-b3acf78bdaf2"), new Guid("5d99c05d-6fea-456e-a6a5-9ba6b6a7ab7f"), new Guid("90f6944e-1b82-4fdf-8594-02149a063d7e")).WithObjectTypes(RespondingParty, ContactMechanism).WithSingularName("ContactMechanism")  .WithPluralName("ContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8e4080f7-40b7-437c-aff2-0fb6b809797a"), new Guid("8f61bcf0-a51c-4c02-95a8-99376824f5ab"), new Guid("79384094-3720-418c-8b87-66084af7fa11")).WithObjectTypes(RespondingParty, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // BudgetStatus
            new RelationTypeBuilder(domain, new Guid("070418ab-f9aa-4286-9395-879b06cf832a"), new Guid("ee3be6af-f2b5-411a-a07b-24eb676bd923"), new Guid("ceee8ab2-a8da-45d8-be09-61e353e8b1a3")).WithObjectTypes(BudgetStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("125c0c29-4f69-4e0b-b885-76c1e908737e"), new Guid("f5e1e19d-2c13-4163-b796-a8e0b7a80fcc"), new Guid("554bd320-adce-40ac-83b0-5710e69a0b25")).WithObjectTypes(BudgetStatus, BudgetObjectState).WithSingularName("BudgetObjectState")  .WithPluralName("BudgetObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PositionType
            new RelationTypeBuilder(domain, new Guid("08ca7d83-ca74-4cc1-9d8a-6cc254c7bd5b"), new Guid("9c14fc30-8b9c-4aaa-8e85-e635c0191111"), new Guid("692c0d2f-0e62-4601-b7d4-21e496596f5d")).WithObjectTypes(PositionType, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("44d5c360-a82d-40ca-a56c-e377327a4858"), new Guid("0588e142-76ff-43a7-ae6e-63427fc18b43"), new Guid("6c00b475-38d9-4f2a-a53b-5a82434db39a")).WithObjectTypes(PositionType, Responsibility).WithSingularName("Responsibility")  .WithPluralName("Responsibilities")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("520649d5-7775-43d0-ab4b-762b2ec6557e"), new Guid("e63e57e3-ae72-456a-9dd4-881ac8c07525"), new Guid("a56a7a77-1233-46c4-86b1-f6ac24d7a1f8")).WithObjectTypes(PositionType, allorsDecimal).WithSingularName("BenefitPercentage")  .WithPluralName("BenefitPercentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8e8e40ff-d11d-4805-abde-845a1b3f1241"), new Guid("f20d568c-3bd8-4383-9cae-052e10065c8e"), new Guid("169055d1-d2ec-4a10-8792-574d8577b273")).WithObjectTypes(PositionType, allorsString).WithSingularName("Title")  .WithPluralName("Titles")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("aa3886a5-a407-4598-900c-8fc3bcfc0604"), new Guid("bb552053-d6e7-470b-a8d9-81ed85950b19"), new Guid("827e2eda-b1bf-4040-9c3a-ff728a44f4c3")).WithObjectTypes(PositionType, PositionTypeRate).WithSingularName("PositionTypeRate")  .WithPluralName("PositionTypeRates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Agreement
            new RelationTypeBuilder(domain, new Guid("2ddce7b3-c763-45ea-8e1b-5ef8a0ea8e4a"), new Guid("d27ed7da-6a94-40ee-b790-8754282a2a1b"), new Guid("f199641e-5574-4733-b4e9-42f6ccb713a8")).WithObjectTypes(Agreement, allorsDateTime).WithSingularName("AgreementDate")  .WithPluralName("AgreementDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("34f0e272-7c56-4d92-a187-c40d9d907110"), new Guid("537bbe1f-ab09-4cbe-92d6-21e199dfcbf5"), new Guid("3fdf6e81-1581-40ca-a1ba-647f33ede850")).WithObjectTypes(Agreement, Addendum).WithSingularName("Addendum")  .WithPluralName("Addenda")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6bdc1767-2bbf-40de-9c2c-a84d1b376a6e"), new Guid("cc8c0485-68bb-46fb-b5e5-d9a970f33ad1"), new Guid("14384f18-d46b-4f01-9414-9c6568b35e80")).WithObjectTypes(Agreement, allorsString).WithSingularName("Description")  .WithPluralName("Description")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9d0e9ea7-31d7-4c01-96f2-97c3e17b3f18"), new Guid("2d2697e9-bbd2-4146-b96f-1bc36dca274c"), new Guid("5ef5b4ca-6faa-4cf2-bfad-fa2f2902dbde")).WithObjectTypes(Agreement, AgreementTerm).WithSingularName("AgreementTerm")  .WithPluralName("AgreementTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9f4db098-c486-4d88-9df9-cd7c79294575"), new Guid("89a37bf1-7c48-428e-b44c-113793c663aa"), new Guid("3469f600-8da7-4d56-b58a-e525487149fc")).WithObjectTypes(Agreement, allorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("d5c90527-cae6-4a4f-9fd7-96f93dad59c7"), new Guid("ceb36b51-89ef-4335-a29f-c3c0f0fc3c06"), new Guid("061d6861-26d9-4008-8612-b72e78bae14f")).WithObjectTypes(Agreement, AgreementItem).WithSingularName("AgreementItem")  .WithPluralName("AgreementItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("daff1ce2-4d60-426c-a45c-a82b63751657"), new Guid("5a11ccff-0d68-4b2c-a7b3-7ba90d9818b0"), new Guid("da9244c2-9225-4448-b2ec-f3ee83d3ef15")).WithObjectTypes(Agreement, allorsString).WithSingularName("AgreementNumber")  .WithPluralName("AgreementNumbers")      .WithSize(256).Build();
			
            // ProductPurchasePrice
            new RelationTypeBuilder(domain, new Guid("a59d91cc-610f-46b6-8935-e95a42edc31e"), new Guid("668c50de-36ba-4ba4-9e89-5319a466d5b0"), new Guid("728b18f7-cfaf-4bc0-84d4-5f2c8d0e8b8c")).WithObjectTypes(ProductPurchasePrice, allorsDecimal).WithSingularName("Price")  .WithPluralName("Prices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("aa7af527-e616-4d01-86b4-e116c3087a37"), new Guid("54e165e0-61ac-46cb-bf92-7aa5d62493d0"), new Guid("4a60cdad-817e-4ae8-801a-13dce2d2c772")).WithObjectTypes(ProductPurchasePrice, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c16a7bec-e1fc-4034-8eb7-0223b776db7a"), new Guid("64d0db60-e291-4113-b471-8ac78f9f381d"), new Guid("cc93b5e0-d7f3-4ae4-910e-f7b2539049e0")).WithObjectTypes(ProductPurchasePrice, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Carrier
            new RelationTypeBuilder(domain, new Guid("8defc9c0-6cc8-4e8a-b892-dad6ff908b85"), new Guid("9a0673e4-8c79-4677-a542-e17f4211d74d"), new Guid("cde2981f-9ba6-4c85-a0cc-b98bd3b7a8a2")).WithObjectTypes(Carrier, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // Resume
            new RelationTypeBuilder(domain, new Guid("5ebf789b-f66a-40c9-99d6-bfaedc581c78"), new Guid("f90810ba-d62d-4e51-b9c7-5aac4e7d4d87"), new Guid("62592457-6263-4e92-b45d-b929245fa750")).WithObjectTypes(Resume, allorsDateTime).WithSingularName("ResumeDate")  .WithPluralName("ResumeDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("f2330d10-d7da-4085-8eff-f0b77cb91763"), new Guid("d38024ac-2e0b-40f1-a6e4-252c5ffc0bcc"), new Guid("b83ee648-06c2-40c6-a907-5d477d57d7db")).WithObjectTypes(Resume, allorsString).WithSingularName("ResumeText")  .WithPluralName("ResumeTexts")      .WithSize(-1).Build();
			
            // ProjectRequirement
            new RelationTypeBuilder(domain, new Guid("75d89129-9aa9-491c-894b-feb86b33bf52"), new Guid("e83f19ff-1441-4d6e-912f-ca56301e3621"), new Guid("80b74f53-e962-4988-a1c1-2860a08ca6b3")).WithObjectTypes(ProjectRequirement, Deliverable).WithSingularName("NeededDeliverable")  .WithPluralName("NeededDeliverables")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // Deposit
            new RelationTypeBuilder(domain, new Guid("2a41dcff-72f9-4225-8a92-1955f10b8ae2"), new Guid("3a24349b-c31d-4ba1-bb95-616852f07c93"), new Guid("04ff9dbf-60cd-4062-b61a-c26b78cf1c48")).WithObjectTypes(Deposit, Receipt).WithSingularName("Receipt")  .WithPluralName("Receipts")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // LegalForm
            new RelationTypeBuilder(domain, new Guid("2867d3b0-5def-4fc6-880a-be4bfe1d2597"), new Guid("ee4e44e3-2f9b-45fc-8b79-f2ac8e2da434"), new Guid("7aa44ba6-a0b4-403b-aabb-7622ddd2db30")).WithObjectTypes(LegalForm, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // FinancialAccountTransaction
            new RelationTypeBuilder(domain, new Guid("04411b65-a0a1-4e2c-9d10-a0ecfcf6c3d2"), new Guid("340a61a7-3458-47ea-b41d-4c559fd8b1d2"), new Guid("1c6950b1-b5dc-4204-878a-f10029dcc4ab")).WithObjectTypes(FinancialAccountTransaction, allorsString).WithSingularName("Description")  .WithPluralName("Descriptons")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("07b3745c-581c-476b-a4a9-beacaa3bd700"), new Guid("7878206b-b4f9-4ddd-b69e-a041402844dd"), new Guid("2e77d783-9cda-41e6-be8b-1bf96520a385")).WithObjectTypes(FinancialAccountTransaction, allorsDateTime).WithSingularName("EntryDate")  .WithPluralName("EntryDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("8f777804-597a-4604-a553-251e2e9d6502"), new Guid("f74151d5-ad2e-4418-b3a1-3772afbdaf52"), new Guid("3135d67e-7290-4eb2-aec8-e783d9325a02")).WithObjectTypes(FinancialAccountTransaction, allorsDateTime).WithSingularName("TransactionDate")  .WithPluralName("TransactionDates")      .Build();
			
            // WorkEffort
            new RelationTypeBuilder(domain, new Guid("039032fa-478b-443f-a58f-0f128e044a4e"), new Guid("48a38f55-f593-405a-b716-cc1eab4ee18c"), new Guid("f9c76b7a-ff9c-4948-a84e-0d8ae1d22740")).WithObjectTypes(WorkEffort, WorkEffortStatus).WithSingularName("CurrentWorkEffortStatus")  .WithPluralName("CurrentWorkEffortStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("092a296d-6f15-4fdd-aed6-25185e6e10b1"), new Guid("95a67913-5914-4705-b76d-6eed73704fab"), new Guid("ff1fade9-aa0b-4058-b8e0-8d993eb841cb")).WithObjectTypes(WorkEffort, WorkEffort).WithSingularName("Precendency")  .WithPluralName("Precendencies")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("0db9b217-c54f-4a7b-a1c0-9592eeabd51f"), new Guid("c918d8f5-77f0-4c0d-b02a-7695a7109cf2"), new Guid("ae8f325d-31e5-473a-8caf-d378ba571025")).WithObjectTypes(WorkEffort, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1a3705c0-0e77-4d6d-a368-ef5141a6c908"), new Guid("b22db3e0-68aa-477c-b86b-96a1b2bb8d20"), new Guid("3f80745d-6a22-4322-b349-ca2a7e441692")).WithObjectTypes(WorkEffort, Deliverable).WithSingularName("DeliverableProduced")  .WithPluralName("DeliverablesProduced")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2e7494ed-6df4-424e-907b-3b900aabf4c5"), new Guid("c6a502b8-5867-4ac9-8356-60155c1950ae"), new Guid("45733b43-f02c-498d-8e77-fe882526268c")).WithObjectTypes(WorkEffort, WorkEffortInventoryAssignment).WithSingularName("InventoryItemNeeded")  .WithPluralName("InventoryItemsNeeded")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2efd427f-daeb-4b84-9f86-857ed1bdb1b7"), new Guid("0e92f113-f607-46bb-85c1-eb3cddb317ef"), new Guid("40e23b5c-8943-4e27-86a1-d0a0140068e6")).WithObjectTypes(WorkEffort, WorkEffort).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3081fa56-272c-43d6-a54c-ad70cb233034"), new Guid("171d3338-5b58-4776-87de-a0b934e15a0a"), new Guid("3c24f9fa-1ada-42f8-8fe1-90c244189254")).WithObjectTypes(WorkEffort, OrderItem).WithSingularName("OrderItemFulfillment")  .WithPluralName("OrderItemFulfillments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("39c8b84f-6925-41f7-aecf-2d73481746cc"), new Guid("736b0da0-facc-48ed-a2a2-2d67257c733b"), new Guid("05694786-18d6-4694-9e37-90f804bab984")).WithObjectTypes(WorkEffort, WorkEffortStatus).WithSingularName("WorkEffortStatus")  .WithPluralName("WorkEffortStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3bebd379-65a9-445e-898e-8913c26b94e6"), new Guid("d12425ed-2676-419e-bfae-674810fde5a8"), new Guid("f4b0fb7e-8e84-43ca-88b0-44242216ee7e")).WithObjectTypes(WorkEffort, WorkEffortType).WithSingularName("WorkEffortType")  .WithPluralName("WorkEffortTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("62b84e6e-1b2f-46cb-825f-57f586e6cb92"), new Guid("81a938c3-1b27-4c24-993a-9bf616f06582"), new Guid("dc2dc942-1210-4fdd-ad95-fe5b4dbd674d")).WithObjectTypes(WorkEffort, InventoryItem).WithSingularName("InventoryItemProduced")  .WithPluralName("InventoryItemsProduced")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a60c8797-320d-471f-9755-d3ef20a4feac"), new Guid("dd8b0f11-0443-4120-be2f-9a43125ccd62"), new Guid("7693cd03-9b2c-4f10-9826-0335371e893d")).WithObjectTypes(WorkEffort, Requirement).WithSingularName("RequirementFulfillment")  .WithPluralName("RequirementFulfillments")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a6fa6291-501a-4b5e-992d-ee5b9a291700"), new Guid("c5cbd6e4-8a61-4e7b-9219-55170ef79f3e"), new Guid("8e8c2f0e-562f-4cb8-9b3f-6a255df820a3")).WithObjectTypes(WorkEffort, allorsString).WithSingularName("SpecialTerms")  .WithPluralName("SpecialTermsPlural")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("add2f3d5-d83a-4734-ad69-9f86eb116f06"), new Guid("d5f050e0-d662-4ac7-90d5-16625fd4afff"), new Guid("18fac5c8-2ba6-43cb-ad3b-d82facc17590")).WithObjectTypes(WorkEffort, WorkEffort).WithSingularName("Concurrency")  .WithPluralName("Concurrencies")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b6213705-ed58-4597-9939-a058b89610f8"), new Guid("4ad69693-3a44-4403-abed-43fd6f208348"), new Guid("21381f45-898c-4622-9e26-039cb49a9eaa")).WithObjectTypes(WorkEffort, allorsDecimal).WithSingularName("ActualHours")  .WithPluralName("ActualHoursPlural")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("bac1939b-8cf8-4b18-862c-4c2dc0a591e5"), new Guid("7172728e-29d2-498f-bea9-da8ab04a1ae5"), new Guid("60306059-f537-4fd6-9d31-7b502f39662e")).WithObjectTypes(WorkEffort, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c71e0d84-b943-43b8-8802-2a95a0b26dc6"), new Guid("16dcc490-612c-4ce1-843e-1c6d3701e4ad"), new Guid("ec31faca-707c-4ba5-b8e8-84b9f644f7e3")).WithObjectTypes(WorkEffort, WorkEffortObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d71aaad8-20ba-4e7f-a4f8-da43e372e202"), new Guid("a1a70f42-fba3-451c-8241-a854a4dba7e2"), new Guid("e6d3f9cb-5465-44e2-92bf-0844c6dfe806")).WithObjectTypes(WorkEffort, WorkEffortObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ebd0daa8-ab45-4390-89f7-3bc89faecdfb"), new Guid("db761f6b-63e2-41fc-a5d9-1d80daa12fbe"), new Guid("149d4820-8630-42a0-9458-18671fb09071")).WithObjectTypes(WorkEffort, allorsDecimal).WithSingularName("EstimatedHours")  .WithPluralName("EstimatedHoursPlural")      .WithPrecision(19).WithScale(2).Build();
			
            // PickListStatus
            new RelationTypeBuilder(domain, new Guid("e1187cc2-9518-4387-986a-e989b303035f"), new Guid("b47b4537-e686-4f86-b45f-5366f05de7d3"), new Guid("67ffe9b3-3916-48e3-9c64-d1427f350737")).WithObjectTypes(PickListStatus, PickListObjectState).WithSingularName("PickListObjectState")  .WithPluralName("PickListObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f87a3dcf-742c-4a3c-afbb-af1969164db9"), new Guid("153a2b44-da58-4db4-9b57-bfa9992c0353"), new Guid("52862edb-477c-4522-85c8-bcedb6affcdd")).WithObjectTypes(PickListStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // Product
            new RelationTypeBuilder(domain, new Guid("039a9481-940b-4953-a1b5-6c56f35a238b"), new Guid("ee6d841a-78f4-47c7-be8a-d4bd7ed81609"), new Guid("922b63dc-1714-4cf2-aa0c-cb81831e59b1")).WithObjectTypes(Product, ProductCategory).WithSingularName("PrimaryProductCategory")  .WithPluralName("PrimaryProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("05a2e95a-e5f1-45bc-a8ca-4ebfad3290b5"), new Guid("1674a9e0-00de-45fa-bde4-63a716a31557"), new Guid("594503f3-c081-46b3-9695-92b921c15a6b")).WithObjectTypes(Product, allorsDateTime).WithSingularName("SupportDiscontinuationDate")  .WithPluralName("SupportDiscontinuationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("0b283eb9-2972-47ae-80d8-1a7aa8f77673"), new Guid("aa3ccdc9-7286-4a82-912a-dd2e53c7410b"), new Guid("487e408f-d55b-4273-bbe9-b0291069ae42")).WithObjectTypes(Product, allorsDateTime).WithSingularName("SalesDiscontinuationDate")  .WithPluralName("SalesDiscontinuationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("0cbb9d37-20cf-4e0c-9099-07f1fcb88590"), new Guid("6ed33681-defd-4003-85e4-79b5ddce888f"), new Guid("cf55a72e-6ca5-4315-af71-ad45ab17fdf3")).WithObjectTypes(Product, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("28f34f5d-c98c-45f8-9534-ce9191587ac8"), new Guid("7c676669-52b3-4665-8212-e2e14dde5cf9"), new Guid("5931ff6f-0972-4e9b-9dc3-dd072ed935a3")).WithObjectTypes(Product, PriceComponent).WithSingularName("VirtualProductPriceComponent")  .WithPluralName("VirtualProductPriceComponents")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("345aaf52-424a-4573-b77b-64708665822f"), new Guid("be3a7b3a-bf77-407e-895a-3609bbf05e24"), new Guid("be85293b-25b0-4856-b9cf-19fe7f0e6a3d")).WithObjectTypes(Product, allorsString).WithSingularName("IntrastatCode")  .WithPluralName("IntrastatCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("438f00fe-750a-414d-a498-a03095c086fb"), new Guid("62a5b5f3-0572-4f17-8f1b-10c9ee9048f4"), new Guid("e051a24d-f2de-439c-923a-39cf6c47a0e4")).WithObjectTypes(Product, ProductCategory).WithSingularName("ProductCategoryExpanded")  .WithPluralName("ProductCategoriesExpanded")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4632101d-09d6-4a89-8bba-e02ac791f9ad"), new Guid("3aed43b7-3bad-44f9-a2d9-8f865de71156"), new Guid("de3785d8-0143-4339-bf49-310c13de385a")).WithObjectTypes(Product, Product).WithSingularName("ProductComplement")  .WithPluralName("ProductComplements")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5735f671-6c52-474b-83a9-3dd8765af241"), new Guid("4abbf18f-1f97-4fec-8e85-805432e65e53"), new Guid("c485cfba-d3fe-46c2-8495-ddb63c8b4f56")).WithObjectTypes(Product, ProductFeature).WithSingularName("OptionalFeature")  .WithPluralName("OptionalFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5f727bd9-9c3e-421e-93eb-646c4fdf73d3"), new Guid("210976bb-e440-44ee-b2b5-39bcee04965b"), new Guid("3165a365-a0db-4ce6-b194-7636cc9c015a")).WithObjectTypes(Product, Party).WithSingularName("ManufacturedBy")  .WithPluralName("ManufacturedBys")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("60bd113a-d6b9-4de9-bbff-2b5094ec4803"), new Guid("b5198a54-72bc-4972-aded-b8eaf0f304a0"), new Guid("1c2134b2-d7ce-469a-a6e4-7e2cc741e07c")).WithObjectTypes(Product, Product).WithSingularName("Variant")  .WithPluralName("Variants")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7423a3e3-3619-4afa-ab67-e605b2a62e02"), new Guid("153ce3b0-0969-40d7-a766-1320ecaef8ac"), new Guid("62228e49-a697-4f1f-8a85-6f1976afd7bb")).WithObjectTypes(Product, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("74fc9be0-8677-463c-b3b6-f0e7bb7478ba"), new Guid("23a3e0bb-a2f9-48d5-b57b-40376e68b0ba"), new Guid("c977306e-8738-4e30-88c1-3c545fdb4e93")).WithObjectTypes(Product, allorsDateTime).WithSingularName("IntroductionDate")  .WithPluralName("IntroductionDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("7c41deee-b270-4810-abaa-6d00e6507b9b"), new Guid("72d6f463-2335-44bc-830f-816ee635101b"), new Guid("8926c093-d513-44dd-9324-3accc051cb06")).WithObjectTypes(Product, Document).WithSingularName("Document")  .WithPluralName("Documents")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8645a62d-b230-4378-b4a2-f7ab64c99e58"), new Guid("f9d855e4-d16b-4d63-9654-a1b455aaa0db"), new Guid("63a361ba-030b-4c95-91a6-ce9131dede95")).WithObjectTypes(Product, ProductFeature).WithSingularName("StandardFeature")  .WithPluralName("StandardFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9b66342e-48ac-4761-b375-b9b60d94b005"), new Guid("fcb1a5ad-544f-4613-a160-077d9130732f"), new Guid("76542b1d-9085-451c-9110-85bfac863016")).WithObjectTypes(Product, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c018edeb-54e0-43d5-9bbd-bf68df1364de"), new Guid("2ad88d44-a7f6-41f7-bcf7-fee094f20e22"), new Guid("cd7f09d5-8c4b-46b7-98d1-108f5e910cc3")).WithObjectTypes(Product, EstimatedProductCost).WithSingularName("EstimatedProductCost")  .WithPluralName("EstimatedProductCosts")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e6f084e9-e6fe-49b8-940e-cda85e1dc1e0"), new Guid("7eb974af-86a6-4d26-a07f-7dd01b80d3ac"), new Guid("3918335f-7cde-4fd2-b168-fb422ab5ee1a")).WithObjectTypes(Product, Product).WithSingularName("ProductObsolescence")  .WithPluralName("ProductObsolescences")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ecc755c1-9a64-42a2-88b6-0278c3598498"), new Guid("d7b3ed79-4733-4d16-9b88-8c05ff684d2a"), new Guid("825e5e8f-d0ac-490e-8511-0596e2952482")).WithObjectTypes(Product, ProductFeature).WithSingularName("SelectableFeature")  .WithPluralName("SelectableFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f26e4376-4e3f-4d7d-8814-54d19c977a76"), new Guid("7da35b67-dbf4-46ce-9f53-d6af8b4e208d"), new Guid("2c8e75e5-e030-4108-b528-c16aaeea40b8")).WithObjectTypes(Product, VatRate).WithSingularName("VatRate")  .WithPluralName("VatRates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f2abc02c-67a1-42b7-83f5-195841e58a6a"), new Guid("dae3b48d-0dde-4c71-bbd3-4f7743d20a9f"), new Guid("fe8dd3c4-0540-49d9-a18a-905fe0259ca1")).WithObjectTypes(Product, PriceComponent).WithSingularName("BasePrice")  .WithPluralName("BasePrices")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f8cc75cb-d328-42ac-a1e7-c490435ed7a4"), new Guid("61f71101-6877-4751-aad1-d3ab194dc6ce"), new Guid("1dbceee7-811b-4bfe-8cd4-177f41cb6d17")).WithObjectTypes(Product, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ff1ebc03-de68-4b52-944f-7cc10f79539b"), new Guid("a23f63e7-1870-44a6-b62c-87834f542d55"), new Guid("e5a720c4-8d70-4583-9547-04f676f1b35f")).WithObjectTypes(Product, InternalOrganisation).WithSingularName("SoldBy")  .WithPluralName("SoldBys")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartBillOfMaterialSubstitute
            new RelationTypeBuilder(domain, new Guid("3d84d60f-c8b7-4e33-847a-9720d6570dd1"), new Guid("6124b5f7-ad97-44d6-8b7d-98694e385792"), new Guid("e2423696-2855-4ac1-8a90-aba5e8413acc")).WithObjectTypes(PartBillOfMaterialSubstitute, PartBillOfMaterial).WithSingularName("SubstitutionPartBillOfMaterial")  .WithPluralName("SubstitutionPartBillOfMaterials")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9bff7f7d-c35c-426d-95f3-6a681d283914"), new Guid("c9fd9c9c-f57d-413a-ac69-7983f5d51dd6"), new Guid("c7596ec0-5e1d-4d8e-8707-f276c01d1e5f")).WithObjectTypes(PartBillOfMaterialSubstitute, Ordinal).WithSingularName("Preference")  .WithPluralName("Preferences")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a5273118-61c9-43de-9754-22555332cc27"), new Guid("3de9b9ee-a96c-43b7-984a-86f6d0d20a52"), new Guid("0aff8adf-0487-4beb-b5f9-2062fa37ec9f")).WithObjectTypes(PartBillOfMaterialSubstitute, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("ef45301b-415a-417f-a952-fd71704a05e5"), new Guid("589cd7f5-a89e-48d2-adbe-8c6307ab3585"), new Guid("aa0e3719-cbc3-4cb0-b83a-3ff5489771f3")).WithObjectTypes(PartBillOfMaterialSubstitute, PartBillOfMaterial).WithSingularName("PartBillOfMaterial")  .WithPluralName("PartBillOfMaterials")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // RequirementBudgetAllocation
            new RelationTypeBuilder(domain, new Guid("4d5cfc89-068f-4cf5-ae51-0b3efe426499"), new Guid("cf5c7a91-3579-458b-8337-ed0ad9474fc4"), new Guid("26f1d7ab-cda3-4b47-a422-293aa6c1f57f")).WithObjectTypes(RequirementBudgetAllocation, BudgetItem).WithSingularName("BudgetItem")  .WithPluralName("BudgetItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9b0f81f1-8df5-4c42-aa10-a05caf777d57"), new Guid("9c7ea874-5d6d-4076-a4a4-7b6596f1ebd4"), new Guid("06e6d247-68b5-42d4-a474-0c1ace36f21c")).WithObjectTypes(RequirementBudgetAllocation, Requirement).WithSingularName("Requirement")  .WithPluralName("Requirements")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f4f64ec3-5e56-45d8-8112-9a32c4f8d6da"), new Guid("95b80fdf-9e3f-424d-b03a-ede9268eb545"), new Guid("82960011-47b7-43c5-ace3-d3830cd93d39")).WithObjectTypes(RequirementBudgetAllocation, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amount")      .WithPrecision(19).WithScale(2).Build();
			
            // OrganisationGlAccount
            new RelationTypeBuilder(domain, new Guid("8e20ce74-a772-45c8-a76a-a8ca0d4d7ebd"), new Guid("948a2115-8780-46c6-83cf-dd4d27a1771b"), new Guid("a3d68e22-e492-4d1e-8386-ea45ad67ee3a")).WithObjectTypes(OrganisationGlAccount, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9337f791-56aa-4086-b661-2043cf02c662"), new Guid("59fb9b8f-4d0a-4f97-b4d6-b3a5708de269"), new Guid("f264e9da-aa7f-4d81-aa1b-741f020c2bef")).WithObjectTypes(OrganisationGlAccount, OrganisationGlAccount).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9af20c76-200c-4aed-8154-99fd88907a15"), new Guid("7d9f9cad-0685-4b7d-b12d-770f046465f3"), new Guid("817a7322-724b-4239-8261-a9c683f1ea4a")).WithObjectTypes(OrganisationGlAccount, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a1608d47-9fa7-4dc4-9736-c59f28221842"), new Guid("61d6a380-171a-41c2-bda9-6cd8638ba442"), new Guid("de2ad2c4-bf0e-4092-9611-b23c3e613429")).WithObjectTypes(OrganisationGlAccount, allorsBoolean).WithSingularName("HasBankStatementTransactions")  .WithPluralName("HasBankStatementsTransactions")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("c0de2fbb-9e70-4094-8279-fb46734e920e"), new Guid("92c29de0-8454-4ae1-8bf9-ed4c5ec0d313"), new Guid("8b65fb70-4905-4c1b-a1b1-51470ce58599")).WithObjectTypes(OrganisationGlAccount, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d5332edf-3cc4-4f26-b0d1-da7ce1182dbc"), new Guid("1fb5ddfd-930a-4c81-8e9e-ac9d94840864"), new Guid("30c0c848-5361-4bd3-9c0a-8b9aeaccdadc")).WithObjectTypes(OrganisationGlAccount, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f1d3e642-2844-4c5a-a053-4dcfce461902"), new Guid("b0706892-9a04-4e5a-8caa-bd015f3d81f9"), new Guid("6cb69b76-2852-43eb-bff6-a10bc44503a3")).WithObjectTypes(OrganisationGlAccount, GeneralLedgerAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // InternalAccountingTransaction
            new RelationTypeBuilder(domain, new Guid("96a1901c-a17a-43d7-8d84-76e1586787f2"), new Guid("5f58fb32-15d9-47e0-9ace-9eb4c1cd2eda"), new Guid("03f1ae5e-0644-47c7-b31e-345e92085a9c")).WithObjectTypes(InternalAccountingTransaction, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // NonSerializedInventoryItem
            new RelationTypeBuilder(domain, new Guid("0958b237-ba88-48d3-b662-90328801b197"), new Guid("72957576-5146-4578-8526-8b7a50025526"), new Guid("ebd546cd-7341-496d-86ca-27a1b8fc253e")).WithObjectTypes(NonSerializedInventoryItem, NonSerializedInventoryItemObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2959a4d0-5945-4231-8a12-a2d1bdb9be04"), new Guid("d48f3a6f-915f-42fe-a508-8cddc3cf3fbc"), new Guid("bd3e6dd7-c339-4ac4-bdce-31526ed7fa1a")).WithObjectTypes(NonSerializedInventoryItem, allorsDecimal).WithSingularName("QuantityCommittedOut")  .WithPluralName("QuantitiesCommittedOut")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2d07e267-a0dc-452d-8166-a376ee38700d"), new Guid("87520701-7447-46b2-8bff-c8a4e23092ae"), new Guid("29532211-2b5b-4e8a-a27e-c7a1afb68370")).WithObjectTypes(NonSerializedInventoryItem, NonSerializedInventoryItemStatus).WithSingularName("NonSerializedInventoryItemStatus")  .WithPluralName("NonSerializedInventoryItemStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("41d804d4-588b-4bea-b013-665f1a6974b9"), new Guid("c82ea702-7035-4293-a8ef-32f0e70e4763"), new Guid("6e17e218-c04d-4c78-9a01-32d16eec6692")).WithObjectTypes(NonSerializedInventoryItem, NonSerializedInventoryItemObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("981acef5-652b-41c1-88f2-e06052bab7e3"), new Guid("3772d6b0-c994-4240-b8de-054b2c72b25f"), new Guid("25a16b8b-3f26-4cf3-8452-c7933d54af2a")).WithObjectTypes(NonSerializedInventoryItem, NonSerializedInventoryItemStatus).WithSingularName("CurrentInventoryItemStatus")  .WithPluralName("CurrentInventoryItemStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a6b78e16-6aef-4478-b426-9429c1a01059"), new Guid("9bcc50ce-a070-4cdd-802f-4296908b75f7"), new Guid("a44947f1-b7e2-4f0c-97d6-2fd32ecae097")).WithObjectTypes(NonSerializedInventoryItem, allorsDecimal).WithSingularName("QuantityOnHand")  .WithPluralName("QuantitiesOnHand")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ba5e2476-abdd-4d61-8a14-5d99a36c4544"), new Guid("f1e3216e-1af7-4354-b8ac-258bfa9222ac"), new Guid("4d41e84c-ee79-4ce2-874e-a000e30c1120")).WithObjectTypes(NonSerializedInventoryItem, allorsDecimal).WithSingularName("PreviousQuantityOnHand")  .WithPluralName("PreviousQuantitiesOnHand")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("dfbd2b04-306c-415c-af67-895810b01044"), new Guid("c1ec09e8-2c1e-4e4a-9496-8c081dee23d9"), new Guid("9a56d091-f6a8-4db1-bd65-10d84eaaaa05")).WithObjectTypes(NonSerializedInventoryItem, allorsDecimal).WithSingularName("AvailableToPromise")  .WithPluralName("AvailablesToPromise")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("eb32d183-9c7b-47a7-ab38-e4966d745161"), new Guid("a7512a69-d27e-47dc-9da5-8713489cc2e5"), new Guid("9aaf1a36-04b9-4cc5-9a22-691b3b3c4633")).WithObjectTypes(NonSerializedInventoryItem, allorsDecimal).WithSingularName("QuantityExpectedIn")  .WithPluralName("QuantitiesExpectedIn")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
			
            // ElectronicAddress
            new RelationTypeBuilder(domain, new Guid("90288ea6-cb3b-47ad-9bb1-aa71d7c65926"), new Guid("8b7e4656-a33b-4d75-8721-106c6f7f2c4e"), new Guid("f04e16be-007f-43dc-974c-92c1423a5426")).WithObjectTypes(ElectronicAddress, allorsString).WithSingularName("ElectronicAddressString")  .WithPluralName("ElectronicAddressStrings")      .WithSize(256).Build();
			
            // NeededSkill
            new RelationTypeBuilder(domain, new Guid("079ef934-26e1-4dba-a69a-73fcc22d380e"), new Guid("f2afa9e5-239d-46c8-94c7-57dd23cb645a"), new Guid("90f27ec7-03b8-491b-862c-3c18a37d4dbc")).WithObjectTypes(NeededSkill, SkillLevel).WithSingularName("SkillLevel")  .WithPluralName("SkillLevels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("21207c09-22b0-469a-84a7-6edd300c73f7"), new Guid("a2c931e4-8200-4cdd-9d26-bedbaf529c29"), new Guid("1984780a-81fa-4391-af4e-20f707550a3d")).WithObjectTypes(NeededSkill, allorsDecimal).WithSingularName("YearsExperience")  .WithPluralName("YearsExperiences")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("590d749a-52d4-448a-8f95-8412c0115825"), new Guid("3e6cc798-dae0-4381-abfd-bcba0b449d03"), new Guid("09e6d6b8-8a89-46af-99fa-f332fea7ab6c")).WithObjectTypes(NeededSkill, Skill).WithSingularName("Skill")  .WithPluralName("Skills")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesInvoice
            new RelationTypeBuilder(domain, new Guid("06d05f50-42ad-426f-9cd7-72e3eb155656"), new Guid("2286307f-4981-4518-b66b-55d27a8455ed"), new Guid("93f5dffc-d5d1-4e08-8ccf-c4be74e3ca00")).WithObjectTypes(SalesInvoice, SalesInvoiceObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("07077530-4e0c-49a8-9fb6-ded142c628a4"), new Guid("b262f707-f942-4372-a0e7-175fd89aa757"), new Guid("a4818ef6-6e55-4edd-9bc0-774ded9d8bd5")).WithObjectTypes(SalesInvoice, SalesInvoiceObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("09064adb-7094-48e9-992c-2eab319d640f"), new Guid("5ade34c0-1f3c-4ecf-933d-72360173f03d"), new Guid("17bb6982-04c0-42e8-9ae3-56bd50736cbb")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("TotalListPrice")  .WithPluralName("TotalListPrices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("21ee2eb5-f20b-41cc-80d2-f533a53a2a2b"), new Guid("d52491dd-3da8-44dc-bf55-0b15553b3b1a"), new Guid("1fadb364-9e2a-4008-a36f-69a3233a9430")).WithObjectTypes(SalesInvoice, InternalOrganisation).WithSingularName("BilledFromInternalOrganisation")  .WithPluralName("BilledFromInternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("27faaa2c-d4db-4cab-aa04-8ec4997d73d2"), new Guid("2e9fab52-2029-4ee3-8eba-ffd9764bcf67"), new Guid("9dd23ce4-d760-45af-94e4-c2ac94b0aea3")).WithObjectTypes(SalesInvoice, ContactMechanism).WithSingularName("BillToContactMechanism")  .WithPluralName("BillToContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2d0e924b-ff24-4630-9151-ac9bfc844c0c"), new Guid("0a159385-7570-494e-976d-4ee493235cb3"), new Guid("239e91ee-5606-4131-a351-ebbd5908d9be")).WithObjectTypes(SalesInvoice, Party).WithSingularName("PreviousBillToCustomer")  .WithPluralName("PreviousBillToCustomers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3eb16102-21cc-4b71-a8e2-4f016da4cfb0"), new Guid("d6e7328a-c306-4649-a7cc-d6b53535845a"), new Guid("35ae04c4-8a23-4531-8736-370ce29c970f")).WithObjectTypes(SalesInvoice, SalesInvoiceType).WithSingularName("SalesInvoiceType")  .WithPluralName("SalesInvoiceTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("401d58f3-286e-4fe4-88a0-e0bf9e245599"), new Guid("c0b50430-9566-42b0-b533-ec48b8cfd355"), new Guid("5c382076-deb8-4456-8cbd-e1f45bb4e5e3")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("InitialProfitMargin")  .WithPluralName("InitialProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4a7695a8-c649-4122-9336-8a1e2e5665ea"), new Guid("fc3ab94b-20e1-4156-aa69-381bb6e8a0b6"), new Guid("550b5478-6929-47b5-b124-2e529ca59cf3")).WithObjectTypes(SalesInvoice, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5c1f4c88-f67d-4f82-a7de-28868a5f030d"), new Guid("32125426-057d-441f-b9c9-2162d58fea83"), new Guid("801d63a0-31ae-4000-802a-b827e4122c62")).WithObjectTypes(SalesInvoice, SalesOrder).WithSingularName("SalesOrder")  .WithPluralName("SalesOrders")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5c3903fa-105b-4c57-8281-1486b0411a3a"), new Guid("2d1495cc-54f2-4ff7-bbfc-6e3aafb2e319"), new Guid("dc40bbae-ac9b-468b-add4-35dfb53a469b")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("InitialMarkupPercentage")  .WithPluralName("InitialMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("67f49b02-f129-4e18-9411-b8b3d17f151b"), new Guid("faffb97a-02d7-4e1d-97c6-fc9275ee5fe6"), new Guid("5a4b5008-2fdd-43a9-a92b-d7d8b3e6678f")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("MaintainedMarkupPercentage")  .WithPluralName("MaintainedMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6cb5e21c-6344-46a9-bab5-355cdfbead81"), new Guid("8e8100ae-dbaa-425c-9dfe-4dccb1d2335a"), new Guid("9f01863e-afc8-47d6-adf1-7c861cd97229")).WithObjectTypes(SalesInvoice, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6e2b9a8a-9d59-4041-a9ea-f3f8286f110c"), new Guid("ee7aba21-39d6-4a4c-8b18-c7c141c8abdc"), new Guid("12db3958-c666-475e-85db-124c6549664d")).WithObjectTypes(SalesInvoice, Shipment).WithSingularName("Shipment")  .WithPluralName("Shipments")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("76982824-9c87-4f93-b2c1-ae312b200bdb"), new Guid("a2832845-c225-4c46-8ce5-c17b9cdcb04b"), new Guid("d097a56f-225e-46be-9474-b35872532e52")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("MaintainedProfitMargin")  .WithPluralName("MaintainedProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7eb3ee0e-43ff-4534-b6f0-c2dba20d4ed6"), new Guid("5a2b19b5-624a-4c2d-99a8-334502a1ca5e"), new Guid("f9e155ae-80e3-42a7-a9be-a3a76dc72545")).WithObjectTypes(SalesInvoice, SalesInvoiceStatus).WithSingularName("InvoiceStatus")  .WithPluralName("InvoiceStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7f833ad2-3146-4660-a9d4-8a70d3ce01db"), new Guid("b466881e-156a-488f-9f26-c2850b7dd7fc"), new Guid("aa621b67-049a-44e8-9f70-07e2a0c696b8")).WithObjectTypes(SalesInvoice, Party).WithSingularName("PreviousShipToCustomer")  .WithPluralName("PreviousShipToCustomers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("816d66a7-7cab-4ce3-9912-c7cc9d6c294c"), new Guid("8b3c78de-7281-4f94-aeda-1dc6bd345df3"), new Guid("056822e6-4333-44ae-8479-d05c1b1b2974")).WithObjectTypes(SalesInvoice, Party).WithSingularName("BillToCustomer")  .WithPluralName("BillToCustomers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8766886a-1efd-412a-9198-1fce2d7478ee"), new Guid("3551ceb8-ce23-4240-af01-d2174a3b0dc1"), new Guid("1bc96a84-4f08-4148-a5c4-123ab290e6a0")).WithObjectTypes(SalesInvoice, SalesInvoiceStatus).WithSingularName("CurrentInvoiceStatus")  .WithPluralName("CurrentInvoiceStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("89557826-c9d1-4aa1-8789-79fb425cdb87"), new Guid("7d157e5a-efbb-453e-bd95-27a9b0ab305f"), new Guid("751ada5f-ff41-43ae-8609-0c1457642375")).WithObjectTypes(SalesInvoice, SalesInvoiceItem).WithSingularName("SalesInvoiceItem")  .WithPluralName("SalesInvoiceItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ab59d448-e9a4-48c3-9288-5a9b7c524870"), new Guid("0b3fb144-b9bf-4651-b227-2f00a5c95c38"), new Guid("124b784c-0b1d-46c6-8369-ae3886b51a47")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("TotalListPriceCustomerCurrency")  .WithPluralName("TotalListPricesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("af0a72c8-003c-44a6-8c6f-086f26542e3d"), new Guid("d434a95b-9053-4471-864b-3d139b78915d"), new Guid("6c44f465-7d50-4a1b-bffa-9693f9afbde2")).WithObjectTypes(SalesInvoice, Party).WithSingularName("ShipToCustomer")  .WithPluralName("ShipToCustomers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ddd9b372-4687-4a6e-b62b-4e0521f8c4b7"), new Guid("3e5b5599-82bc-4bc3-8ef0-9b2301a1ad40"), new Guid("33265997-e42c-4955-839c-d2ce054b2d33")).WithObjectTypes(SalesInvoice, ContactMechanism).WithSingularName("BilledFromContactMechanism")  .WithPluralName("BilledFromContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("deb1b4ad-39a4-480a-8ef2-3f05c6505077"), new Guid("98bd67fc-c675-425a-800d-79cea6a4a193"), new Guid("1ed1e917-2729-4d14-8b28-686991e11d6c")).WithObjectTypes(SalesInvoice, allorsDecimal).WithSingularName("TotalPurchasePrice")  .WithPluralName("TotalPurchasePrices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ed091c3c-1f38-498a-8ca5-ca8b8ddfc5c4"), new Guid("2531dbb0-e34e-41c2-b6e2-95e3a39cf54d"), new Guid("e279aec5-e503-46c5-9563-b13f58274f71")).WithObjectTypes(SalesInvoice, SalesChannel).WithSingularName("SalesChannel")  .WithPluralName("SalesChannels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f2f85b74-b28f-4627-9dca-94142789c0bc"), new Guid("e1bf6299-0009-44ad-84d3-725df91d5f63"), new Guid("e64f29b4-aa97-463f-acf1-fc9bd2a2bd8f")).WithObjectTypes(SalesInvoice, Party).WithSingularName("Customer")  .WithPluralName("Customers")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f808aafb-3c7d-4a26-af5c-44b76ee45e86"), new Guid("d487d63e-8094-4085-bb73-d2f24e586c26"), new Guid("462acdc2-69e1-42e5-ba10-6f74f04da7a5")).WithObjectTypes(SalesInvoice, PostalAddress).WithSingularName("ShipToAddress")  .WithPluralName("ShipToAddresses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fd12507e-96b7-4b15-a43d-ab418d4795d6"), new Guid("b8044f1e-b8fa-42fc-995d-06ac47423b8e"), new Guid("8dd43185-e3a9-44d7-ab1e-2a1222a234cf")).WithObjectTypes(SalesInvoice, Store).WithSingularName("Store")  .WithPluralName("Stores")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            // InventoryItem
            new RelationTypeBuilder(domain, new Guid("0f4e5107-cf1e-4fc2-9be5-c3235ce9a7af"), new Guid("4552144a-0d6e-4a4a-94c7-cafcdd280350"), new Guid("dbb34993-e385-4702-8a50-4cc26193b862")).WithObjectTypes(InventoryItem, InventoryItemVariance).WithSingularName("InventoryItemVariance")  .WithPluralName("InventoryItemVariances")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("374e9e57-e878-40ac-9021-d830dbf1efdc"), new Guid("fb58bcd8-d263-4563-b7d1-d62b6036e8bb"), new Guid("99a9dadc-ac9e-4662-91d0-41804e70101f")).WithObjectTypes(InventoryItem, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("39ee9493-b628-4cc6-a31c-239f306e8497"), new Guid("04da42d7-6e24-409e-b384-2283dc95ac35"), new Guid("b08500e2-feee-4115-90ec-c65d719d1d29")).WithObjectTypes(InventoryItem, Container).WithSingularName("Container")  .WithPluralName("Containers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("57bd3950-8477-44c3-9c16-dd894d774c51"), new Guid("758170e9-123d-47aa-96f2-ecff7f90c3e0"), new Guid("2618fdd7-d513-4c0d-be0a-12c73225777d")).WithObjectTypes(InventoryItem, allorsString).WithSingularName("Name")  .WithPluralName("Names")  .WithIsDerived(true)    .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5f8fa5ee-a638-4222-9865-518e220e7299"), new Guid("b6412daa-ac8d-4a73-8b87-cbc91981488c"), new Guid("eb553284-25ab-4cc0-ba20-5b5ef5c74313")).WithObjectTypes(InventoryItem, Lot).WithSingularName("Lot")  .WithPluralName("Lots")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("64887d2f-3017-4804-afb4-5e46eec23491"), new Guid("cdb384fc-b2af-49df-a478-c3152c7386ea"), new Guid("4a17bd4c-57a2-4602-b263-6bc9bd0e66aa")).WithObjectTypes(InventoryItem, allorsString).WithSingularName("Sku")  .WithPluralName("Skus")  .WithIsDerived(true)    .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6efb2763-ce7e-4b43-afc1-e4e37af814f0"), new Guid("68c84889-9cb5-43d4-8406-88c3dcfea7aa"), new Guid("e4d19331-72c6-4b4a-bb20-e3beffe3a46e")).WithObjectTypes(InventoryItem, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ab7b1a91-4756-4806-a5d3-ed8b392c6fe7"), new Guid("32ee6a64-bc23-4cb8-a839-2a83d4a68c46"), new Guid("8455bf8a-247f-41d9-b6fc-c30cda0f10f4")).WithObjectTypes(InventoryItem, ProductCategory).WithSingularName("DerivedProductCategory")  .WithPluralName("DerivedProductCategories")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b4d944f5-7376-4096-a34a-4571f537c5fc"), new Guid("78fe1b1f-b4e9-4b75-b16f-6647ef8080ee"), new Guid("6cda2856-203d-409a-b9dd-3ae0c91d7443")).WithObjectTypes(InventoryItem, Good).WithSingularName("Good")  .WithPluralName("Goods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f99da732-7c31-4c67-a610-2147a2f29e44"), new Guid("300cc6b2-7f50-4399-a3f6-e492d3858524"), new Guid("e1f8b0ea-6b2f-44f1-bfe0-504b8e06f96d")).WithObjectTypes(InventoryItem, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PurchaseInvoiceStatus
            new RelationTypeBuilder(domain, new Guid("049dd047-7fa7-46e3-900b-84b87f960412"), new Guid("51ddc06d-aa92-49e1-b410-a7f69a474bdf"), new Guid("bf01b58a-465e-4dec-a451-de4601d28850")).WithObjectTypes(PurchaseInvoiceStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("1f5b8db1-58bd-4006-8878-f1609055149c"), new Guid("f63d8052-8f2f-47b5-a5f8-585b0b2587ae"), new Guid("444ada74-4d76-4c78-a7c8-dc2c448dd7eb")).WithObjectTypes(PurchaseInvoiceStatus, PurchaseInvoiceObjectState).WithSingularName("PurchaseInvoiceObjectState")  .WithPluralName("PurchaseInvoiceObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Region
            new RelationTypeBuilder(domain, new Guid("2b0f6297-9056-4c51-a898-e5bf09e67941"), new Guid("9e062953-c2a0-44da-b6bf-5669b11fe4ab"), new Guid("e3e9d99b-7780-4528-91dd-d75298bf2437")).WithObjectTypes(Region, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // SalesTerritory
            new RelationTypeBuilder(domain, new Guid("d904af24-887c-40b0-a5d0-7dce40ec4db3"), new Guid("0e172e31-8896-42c9-b1f2-2ff8bc1065c1"), new Guid("bcf4d240-258b-43f3-ac94-4314685019ea")).WithObjectTypes(SalesTerritory, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // TimeEntry
            new RelationTypeBuilder(domain, new Guid("1b07c419-42af-480b-87ba-1c001995dc51"), new Guid("2c605991-8d65-4b8f-9daf-e085af5b12c0"), new Guid("90872970-372a-4f8d-9c53-c753aca9f99f")).WithObjectTypes(TimeEntry, allorsDecimal).WithSingularName("Cost")  .WithPluralName("Costs")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1bb9affa-1390-4f54-92b5-64997e55525e"), new Guid("0f0341bb-d719-4989-a39b-02b1c1ce98b9"), new Guid("ff8087ac-403d-46e4-b799-316bbdb6616e")).WithObjectTypes(TimeEntry, allorsDecimal).WithSingularName("GrossMargin")  .WithPluralName("GrossMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("258a33cc-06ea-45a0-9b15-1b6d58385910"), new Guid("4909a04f-fd14-46ce-9c4c-bc7a2cc21914"), new Guid("cff49ef3-5b51-4501-a5c8-59b4d5714f4e")).WithObjectTypes(TimeEntry, QuoteTerm).WithSingularName("QuoteTerm")  .WithPluralName("QuoteTerms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2c33de6e-b4fd-47e4-b254-2991f33f01f1"), new Guid("c8b7e4be-fbc5-414c-8e30-3947925c24b8"), new Guid("1cca252a-d6a1-4945-991a-dd85090bb41d")).WithObjectTypes(TimeEntry, allorsDecimal).WithSingularName("BillingRate")  .WithPluralName("BillingRates")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("409ff1fb-1531-4829-9d6b-7b3e7113594a"), new Guid("54a57392-59ed-4583-99f1-1f2a97ca65c5"), new Guid("724e2645-553a-4810-a62d-4c7595877042")).WithObjectTypes(TimeEntry, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c163457c-6a36-45ab-8c62-e555128afbfc"), new Guid("01112e75-888e-4dac-93e0-185afe6988af"), new Guid("56c9d8a5-45d0-4bb4-8809-43740938b824")).WithObjectTypes(TimeEntry, allorsDecimal).WithSingularName("AmountOfTime")  .WithPluralName("AmountsOfTime")      .WithPrecision(19).WithScale(2).Build();
			
            // DepreciationMethod
            new RelationTypeBuilder(domain, new Guid("a87fd42b-7be3-4cd4-9393-64b1cf03c050"), new Guid("9957bc91-53a9-431c-8eea-2e0dc04adde7"), new Guid("67ecfd2b-4fc4-4474-81f8-cb8b720b30c4")).WithObjectTypes(DepreciationMethod, allorsString).WithSingularName("Formula")  .WithPluralName("Formulas")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b0a81d90-f6bc-4169-b76c-497a3a1f04bf"), new Guid("6af9db7e-6d96-4b91-9a7f-0f1005e49f65"), new Guid("2d1ef7fc-bd11-4380-a917-a29fa14fa89d")).WithObjectTypes(DepreciationMethod, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // StoreRevenueHistory
            new RelationTypeBuilder(domain, new Guid("4c44c10c-7577-424a-9361-43d9b264e297"), new Guid("dbe1fab7-525b-4824-bb9d-959b9e2c8afd"), new Guid("09e6e626-d5e4-4ed1-acce-271cd26ccdcf")).WithObjectTypes(StoreRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5165e08b-97cc-457b-ba65-6592c31360e5"), new Guid("67272022-b5fb-4d33-aada-2df814418b64"), new Guid("f15e82a9-563c-4fda-bbb3-ba9c3d542d2c")).WithObjectTypes(StoreRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("80a3da61-9df3-4100-b81b-323f293194a7"), new Guid("189a3f1e-1878-4e85-921d-9ae9d7ce520e"), new Guid("0f864ab4-599f-451f-9088-d7380981a46f")).WithObjectTypes(StoreRevenueHistory, Store).WithSingularName("Store")  .WithPluralName("Stores")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a23658cb-80aa-4880-ae11-b0584856f1f8"), new Guid("db5e7707-20cf-49ef-9aaa-473e192e86eb"), new Guid("0a79fe7c-68ac-4484-9054-026fb4dc556c")).WithObjectTypes(StoreRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
			
            // PersonTraining
            new RelationTypeBuilder(domain, new Guid("023864ad-41e1-41cb-8ded-ad2bfa98afe3"), new Guid("04f1d7c4-1012-4b0e-b38e-02d6abc328be"), new Guid("91bba22d-82b7-4425-ba55-2862f803088d")).WithObjectTypes(PersonTraining, Training).WithSingularName("Training")  .WithPluralName("Trainings")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // DeliverableOrderItem
            new RelationTypeBuilder(domain, new Guid("f9e13dab-0081-4d25-8021-f5ed5bef5f0e"), new Guid("86376834-b792-425e-a21d-30065dca6dd4"), new Guid("fb6ba6e4-2f9f-4230-b536-df8e305797f9")).WithObjectTypes(DeliverableOrderItem, allorsDecimal).WithSingularName("AgreedUponPrice")  .WithPluralName("AgreedUponPrices")      .WithPrecision(19).WithScale(2).Build();
			
            // OrganisationGlAccountBalance
            new RelationTypeBuilder(domain, new Guid("347426a0-8678-4eaa-9733-4bf719bad0c2"), new Guid("754539d8-c07f-420b-a8c1-6201b6015147"), new Guid("c8fb5d7a-b351-49d0-83ad-c0a8b797af36")).WithObjectTypes(OrganisationGlAccountBalance, OrganisationGlAccount).WithSingularName("OrganisationGlAccount")  .WithPluralName("OrganisationGlAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("94c5bafb-29ef-4268-846e-5fda5c62af5c"), new Guid("a3f8a8a3-f837-4ae9-a718-8ab30149086e"), new Guid("c5f7c8d8-a654-4f20-a0b3-a2013e964158")).WithObjectTypes(OrganisationGlAccountBalance, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("f7325700-87e9-4753-8b0b-de459a6926e7"), new Guid("58379bfa-a272-4877-98ce-5e46063bc1c2"), new Guid("f7278113-6da8-49af-b205-615cf8df50fd")).WithObjectTypes(OrganisationGlAccountBalance, AccountingPeriod).WithSingularName("AccountingPeriod")  .WithPluralName("AccountingPeriods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // InternalOrganisationRevenueHistory
            new RelationTypeBuilder(domain, new Guid("22b47e4a-5657-4bf6-acff-d923ef5ef8e2"), new Guid("4f48cd93-af57-4e7a-b54b-3aef374444e7"), new Guid("2aea8ef1-d353-43e4-8f1a-09b2052603e2")).WithObjectTypes(InternalOrganisationRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4fe311fd-b793-43ce-b3de-c6606ea53b34"), new Guid("08a439a9-cb0d-4910-818f-11b9819bff86"), new Guid("383ed00b-7c75-40cb-b0d7-2bcc6b9f5e62")).WithObjectTypes(InternalOrganisationRevenueHistory, allorsDecimal).WithSingularName("AllorsDecimal")  .WithPluralName("AllorsDecimals")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a8ab7445-38b2-4e1b-bee2-15d1a91fc239"), new Guid("cc5041b1-da31-472d-ab66-105561bcb2de"), new Guid("1d88f2e2-6a81-42f8-8e60-242918c82e21")).WithObjectTypes(InternalOrganisationRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b32232d6-6a5b-438b-bfb5-a7495335f9c9"), new Guid("14c55588-7e1f-4ce2-a080-f14592052442"), new Guid("57afc02e-c13e-41fa-a1cd-0dfe2358649b")).WithObjectTypes(InternalOrganisationRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
			
            // Deliverable
            new RelationTypeBuilder(domain, new Guid("d7322009-e68f-4635-bc0e-1c0b5a46de62"), new Guid("953cd640-51dd-4543-a751-242c7e39b596"), new Guid("38bd223e-54ee-455d-8da5-3106029e1fbe")).WithObjectTypes(Deliverable, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("dfd5fb95-50ee-48a5-942b-75752f78a615"), new Guid("fea5e2c3-b8fa-488d-aba6-641176652430"), new Guid("50499eba-a2b0-4ad2-8dc6-72eb2d1997a7")).WithObjectTypes(Deliverable, DeliverableType).WithSingularName("DeliverableType")  .WithPluralName("DeliverableTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // EmploymentApplication
            new RelationTypeBuilder(domain, new Guid("528de310-3268-4b17-ab42-49dea27d5aee"), new Guid("ca9bf054-52cf-40f1-995f-0e504b5bee9b"), new Guid("9b07c065-678f-4d21-878f-4ac2029dddc5")).WithObjectTypes(EmploymentApplication, allorsDateTime).WithSingularName("ApplicationDate")  .WithPluralName("ApplicationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("75cc1a7c-6bf7-4798-9ddc-fd1b283aed19"), new Guid("edffb19c-1b3d-45fc-bc52-d44bd51fc6e2"), new Guid("502bf1bc-596d-44e7-b9f3-148433028740")).WithObjectTypes(EmploymentApplication, Position).WithSingularName("Position")  .WithPluralName("Positions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7d3147e2-9709-42bc-a6cd-5b922bfc143d"), new Guid("e8ec31ed-ebd4-4a2c-8948-7170adf61572"), new Guid("af280063-af23-4afb-9dd6-8f44141c275e")).WithObjectTypes(EmploymentApplication, EmploymentApplicationStatus).WithSingularName("EmploymentApplicationStatus")  .WithPluralName("EmploymentApplicationStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a4c14261-14a2-404c-814f-6475368d685a"), new Guid("f9cf5e5a-d262-4898-91f3-a69b3612f0a8"), new Guid("6d7984e5-d1c7-4a53-99a1-49e125db39b9")).WithObjectTypes(EmploymentApplication, Person).WithSingularName("Person")  .WithPluralName("Persons")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b0799b22-bff3-49d7-8f9a-3ea41c540778"), new Guid("90dc458e-243e-42d6-950d-3994f7617981"), new Guid("4222311d-cb11-4cec-a547-eb45cfe94732")).WithObjectTypes(EmploymentApplication, EmploymentApplicationSource).WithSingularName("EmploymentApplicationSource")  .WithPluralName("EmploymentApplicationSources")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // VatRegime
            new RelationTypeBuilder(domain, new Guid("2071cc28-c8bf-43dc-a5e5-ec5735756dfa"), new Guid("fca4a435-bd82-496b-ab1d-c2b6cb10494f"), new Guid("baf416cf-3222-4c93-8fb7-f4257b2b9ef9")).WithObjectTypes(VatRegime, VatRate).WithSingularName("VatRate")  .WithPluralName("VatRates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a037f9f0-1aff-4ad0-8ee9-36ae4609d398"), new Guid("25db54a8-873d-4736-8408-f1d9e65c49e4"), new Guid("238996a2-ec4f-47f4-8336-8fee91383649")).WithObjectTypes(VatRegime, OrganisationGlAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PositionFulfillment
            new RelationTypeBuilder(domain, new Guid("30631f6e-3e70-4394-9540-0572230cd461"), new Guid("ebcfbd12-ea78-4dd1-b102-05110c7d4a95"), new Guid("3fc029a2-3465-4518-830a-348bd2235a71")).WithObjectTypes(PositionFulfillment, Position).WithSingularName("Position")  .WithPluralName("Positions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4de369bb-6fa3-4fd4-9056-0e70a72c9b9f"), new Guid("23fa9951-ceb1-44b2-af36-f3e4955018d1"), new Guid("76c3f430-bf53-4e6f-89af-ea91afbd6795")).WithObjectTypes(PositionFulfillment, Person).WithSingularName("Person")  .WithPluralName("Persons")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Employment
            new RelationTypeBuilder(domain, new Guid("776e5cb7-6926-4455-89ed-c1f916018a25"), new Guid("9d997658-68ca-41a3-9551-9cc793811a4e"), new Guid("28191884-d18f-400b-96df-7da1a328d88e")).WithObjectTypes(Employment, InternalOrganisation).WithSingularName("Employer")  .WithPluralName("Employers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a243feb0-e5f0-41b4-9b13-a09bb8413fb3"), new Guid("03bac42d-dcbc-40f3-a130-7b4f3b37f523"), new Guid("1fb50b4b-2a1b-4139-a376-48f1c72c4645")).WithObjectTypes(Employment, Person).WithSingularName("Employee")  .WithPluralName("Employees")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ba6d2658-9c07-4254-a664-21df0e2fcb6a"), new Guid("f512d8bd-5ea3-461c-9310-6ab93696763d"), new Guid("3c2fae70-49b5-407f-823c-db9b9052fb1e")).WithObjectTypes(Employment, PayrollPreference).WithSingularName("PayrollPreference")  .WithPluralName("PayrollPreferences")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c8fd6c79-f909-414e-b9e3-5e911e2e2080"), new Guid("da451dab-03db-4bc5-8641-93ec74570f4f"), new Guid("0bef74ad-3eb2-494e-846e-6ca3bbfb057b")).WithObjectTypes(Employment, EmploymentTerminationReason).WithSingularName("EmploymentTerminationReason")  .WithPluralName("EmploymentTerminationReasons")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e79807d4-dcf8-47e2-b510-e8535f1ec436"), new Guid("6b4896d8-8bf6-4908-acb9-dc2438263fb7"), new Guid("96ff4ce3-5e0b-408e-9641-edf2e06dc508")).WithObjectTypes(Employment, EmploymentTermination).WithSingularName("EmploymentTermination")  .WithPluralName("EmploymentTerminations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // AccountingPeriod
            new RelationTypeBuilder(domain, new Guid("0fd97106-1e39-4629-a7bd-ad263bc2d296"), new Guid("816f8a0b-3c3a-4dd2-a50e-5c3cd197c592"), new Guid("2d803ef5-2e9a-46fa-8690-5d5ef00f6785")).WithObjectTypes(AccountingPeriod, AccountingPeriod).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("93b16073-8196-40c2-8777-5719fe1e6360"), new Guid("50a13e06-7df7-4d56-b498-5eea8415bb48"), new Guid("e6b86e57-d8d2-41aa-b238-2fe027d74813")).WithObjectTypes(AccountingPeriod, allorsBoolean).WithSingularName("Active")  .WithPluralName("Actives")      .Build();
            new RelationTypeBuilder(domain, new Guid("babffef0-47ad-44ad-9a55-ffefb0fec783"), new Guid("b490215a-8185-40c8-bb31-087906d10911"), new Guid("9fdaab7a-5e4a-4ec1-85bb-0610c0d0493b")).WithObjectTypes(AccountingPeriod, allorsInteger).WithSingularName("PeriodNumber")  .WithPluralName("PeriodNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("d776c4f4-9408-4083-8eb4-a4f940f6066f"), new Guid("8789a4bf-fd21-48d1-ae0b-26ebd100c0ea"), new Guid("98fec7aa-6357-4b8e-baf6-0a8ef3d221dc")).WithObjectTypes(AccountingPeriod, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // EngagementRate
            new RelationTypeBuilder(domain, new Guid("0c2c005b-f652-47b2-a42b-7cd511382dd3"), new Guid("653e795b-52b5-4f76-b1a7-dd34dcc7fc0e"), new Guid("9770c0a9-f8bb-4fd2-ae33-8513e9dcd24b")).WithObjectTypes(EngagementRate, allorsDecimal).WithSingularName("BillingRate")  .WithPluralName("BillingRate")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1df6f7fe-6cb9-4c1b-b664-e7ee1e2cec6f"), new Guid("62d1d3a9-cda9-4036-8cf9-eb0d58bbc29e"), new Guid("5d912d73-b973-40f6-931d-9689674c7e55")).WithObjectTypes(EngagementRate, RatingType).WithSingularName("RatingType")  .WithPluralName("RatingTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a920a2c5-021e-4fc9-b38b-21be0003e40f"), new Guid("6004b01b-26e0-44de-8e2f-6e90532d5070"), new Guid("ffc748e3-9e10-4ad6-bd60-9b747ee5ad93")).WithObjectTypes(EngagementRate, allorsDecimal).WithSingularName("Cost")  .WithPluralName("Costs")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c54c15ad-0b9b-490c-bdbb-90a49c728b94"), new Guid("35b7e6dd-5cd0-4aa3-b12c-db10c44b0606"), new Guid("c3e184bf-e863-45d5-b991-b7274757a28e")).WithObjectTypes(EngagementRate, PriceComponent).WithSingularName("GoverningPriceComponent")  .WithPluralName("GoverningPriceComponents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d030a71e-10ba-48cc-9964-456518b705de"), new Guid("a25d3578-feb9-4eb6-853f-673b2300dc7e"), new Guid("4d655601-37c1-4c31-83a7-406cce05ed4c")).WithObjectTypes(EngagementRate, allorsString).WithSingularName("ChangeReason")  .WithPluralName("ChangeReasons")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e7dafa85-712a-4ea4-abe9-82ddd9afc80c"), new Guid("e4462db8-7b15-473f-9aca-3fd01d9dba2e"), new Guid("17f43a3b-5772-4413-9df4-5c3250c94bf8")).WithObjectTypes(EngagementRate, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ExternalAccountingTransaction
            new RelationTypeBuilder(domain, new Guid("327fc2cb-9589-4e9d-b9e6-7429cbe14746"), new Guid("5fdf05a4-933c-42d9-897c-b68c6671f785"), new Guid("df92000b-768e-41db-addc-1e2ca5c8baee")).WithObjectTypes(ExternalAccountingTransaction, Party).WithSingularName("FromParty")  .WithPluralName("FromParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("681312d3-63cd-45a2-883c-4a907d379f52"), new Guid("bfe64bcc-8832-4d02-92cb-7f4b0681fc81"), new Guid("2359aaec-7150-4a84-82af-7dc4ef677c9b")).WithObjectTypes(ExternalAccountingTransaction, Party).WithSingularName("ToParty")  .WithPluralName("ToParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // TelecommunicationsNumber
            new RelationTypeBuilder(domain, new Guid("2eabf6bb-48f9-431a-b05b-b892c88db821"), new Guid("2260b0c0-3a19-43cb-a2f1-22098d428a35"), new Guid("5d7ad31b-a29d-4b3f-8411-744a172bf6a9")).WithObjectTypes(TelecommunicationsNumber, allorsString).WithSingularName("AreaCode")  .WithPluralName("AreaCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("31ccabaf-1d31-4b35-93a4-8c18c813c3cd"), new Guid("3d5d091c-0b5a-421e-bbe8-1c64b35d19b0"), new Guid("f8c81a88-4d53-461a-960d-32325ebc177a")).WithObjectTypes(TelecommunicationsNumber, allorsString).WithSingularName("CountryCode")  .WithPluralName("CountryCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9b587eba-53ee-417c-8324-5c19ec90b745"), new Guid("7ea12a2f-a018-422b-8a03-a683e2bad699"), new Guid("fd07dae1-2e46-48a7-956d-7b881e6c271a")).WithObjectTypes(TelecommunicationsNumber, allorsString).WithSingularName("ContactNumber")  .WithPluralName("ContactNumbers")      .WithSize(40).Build();
			
            // SalesRepRelationship
            new RelationTypeBuilder(domain, new Guid("24e8c07a-2fca-496a-8c21-165f29a6733d"), new Guid("5cd1d447-85b5-4d28-8296-05e356046f62"), new Guid("7d8a933f-1d36-4247-bf13-580aa24bd645")).WithObjectTypes(SalesRepRelationship, Person).WithSingularName("SalesRepresentative")  .WithPluralName("SalesRepresentatives")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4ffa4073-8359-48c0-8562-9c30e6426ad2"), new Guid("ba0a1788-bd88-4d93-91c9-a51af7831ba2"), new Guid("6a59c35a-9ffb-4311-b0fa-43ea42b61fd1")).WithObjectTypes(SalesRepRelationship, allorsDecimal).WithSingularName("LastYearsCommission")  .WithPluralName("LastYearsCommissions")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("61a10565-62ac-4529-a3b1-709f3b5da306"), new Guid("8a3b5d2e-3be7-4c54-9571-d2466f5323ff"), new Guid("6f9f29f3-0f9e-458a-aea6-27dd7e76adfe")).WithObjectTypes(SalesRepRelationship, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7dc11a0c-72af-4296-94a4-068edae0021a"), new Guid("8f82d5f9-8f9e-4f57-bb39-4bab9f9813a3"), new Guid("fac65a32-5fcb-4304-9f7d-3ae36da914ff")).WithObjectTypes(SalesRepRelationship, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("98dab364-0db6-438f-ac12-9b0238e81afd"), new Guid("eb32c549-bb3e-4789-abdb-9073905077bb"), new Guid("f213a48e-d351-41c4-91df-48edd0043017")).WithObjectTypes(SalesRepRelationship, allorsDecimal).WithSingularName("YTDCommission")  .WithPluralName("YTDCommissions")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b770e679-2da6-45e7-b8e0-2ee39ab67f1e"), new Guid("95817787-34eb-42f5-82a0-d28bfa93cd88"), new Guid("b86c35e3-b512-4d5e-9f08-29fe8d5a7b43")).WithObjectTypes(SalesRepRelationship, Party).WithSingularName("Customer")  .WithPluralName("Customers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ProductCategoryRevenue
            new RelationTypeBuilder(domain, new Guid("3e748cda-d69d-43f9-be75-c942bd432bc7"), new Guid("164d084d-339d-4d3c-8b64-5a985b7b12f1"), new Guid("89301283-a980-4a46-b113-c3b45f6ef3a3")).WithObjectTypes(ProductCategoryRevenue, allorsString).WithSingularName("ProductCategoryName")  .WithPluralName("ProductCategoryNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("458c4900-00d6-4ad8-a8bc-45a61364ca3d"), new Guid("5d066c0f-4cab-47ce-aaf5-8b3557ba11f2"), new Guid("032207f1-426c-4022-92ac-4042f18cce0c")).WithObjectTypes(ProductCategoryRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("558ed9e0-81a0-4e6c-abd3-4e27e665deee"), new Guid("e1e94fa4-ac8f-44c2-9982-a05ad1eb3f8e"), new Guid("125f6b43-2204-43cb-819b-4b2b940630cc")).WithObjectTypes(ProductCategoryRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6127124a-c07d-49b7-8ecd-fb42d50c4c69"), new Guid("6e095e71-bcdf-4b94-8880-b6a888eec2bf"), new Guid("51cfff06-36f5-4b1f-9c01-2ea2bfb015f1")).WithObjectTypes(ProductCategoryRevenue, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a0fef77b-3d7d-4338-b095-1a69a8cbfda4"), new Guid("0a2754a2-5492-4bd7-acf9-65a424e2d870"), new Guid("fad7d065-5545-4d85-b2f9-259708822626")).WithObjectTypes(ProductCategoryRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e97efb8e-a61e-4710-a576-75e540f2ec1f"), new Guid("b5a6f45b-ee80-4661-ba07-aafaf8676794"), new Guid("a763b52d-022b-472d-89ca-982e573053c9")).WithObjectTypes(ProductCategoryRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f2f39bba-8f9e-4dac-ba98-542eb19aebab"), new Guid("590fa663-383d-42d7-8a4c-37ba7e9c6030"), new Guid("76e05eb3-fa65-4efd-8f9a-330b811e5dfe")).WithObjectTypes(ProductCategoryRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
			
            // ChartOfAccounts
            new RelationTypeBuilder(domain, new Guid("65f44f44-a613-4cbf-a924-1098c9876f20"), new Guid("d4bd5e5f-e973-489c-879d-31b0023de770"), new Guid("0f6c3b14-f165-41df-aa8d-f49f53c53e05")).WithObjectTypes(ChartOfAccounts, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("71d503fb-ebb9-45b3-af62-1b233677adce"), new Guid("ca0820dd-e0b2-4714-8e2f-f3613dcdbd15"), new Guid("d855adc2-f70e-48d3-a185-957bf27d3d58")).WithObjectTypes(ChartOfAccounts, GeneralLedgerAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // PartyRevenue
            new RelationTypeBuilder(domain, new Guid("166cc4a8-4f7f-411e-9a43-9c3f44357691"), new Guid("2cfc0f11-a730-4992-9917-1f6830a534fc"), new Guid("7c8121e5-b9b9-4f4f-9b15-d47fdea0e661")).WithObjectTypes(PartyRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1b98381e-534b-4f65-9fc2-1638698da6fe"), new Guid("59387dc8-1742-4593-82f6-c74b361d4b35"), new Guid("19e3cb2e-0170-437f-9c4e-ef7765d674d2")).WithObjectTypes(PartyRevenue, allorsString).WithSingularName("PartyName")  .WithPluralName("PartyNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("3b750587-6361-4359-8fb3-d4119a91340d"), new Guid("93a555e3-6f22-470b-92d6-e84134564621"), new Guid("2e60687a-c01b-4055-8310-44e2aeea2118")).WithObjectTypes(PartyRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("ca4a5658-e964-4ae5-b5b6-c4e0747d9001"), new Guid("69e54844-d8e1-4f7b-be89-cde6e4c34431"), new Guid("7e9a8b8f-f98d-402f-baba-a7d22b3a1525")).WithObjectTypes(PartyRevenue, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d3a7f72d-e112-4da4-971f-69016bedf814"), new Guid("aa0ac3ab-3db4-4f5f-bd6a-8d50190bad8f"), new Guid("fcd1e476-6420-4e78-8da6-18c8a6e5f103")).WithObjectTypes(PartyRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("da71126e-5599-4c2e-9046-cf238501c61e"), new Guid("9da4cbdf-3010-40fc-bd80-e9d89c412cd9"), new Guid("8aaf333f-3122-419d-b7cc-5a0703cb1615")).WithObjectTypes(PartyRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f12eef62-4f05-4de5-878f-75a2353ae3b5"), new Guid("4f4fb02a-a5dc-4e73-a121-47195ec0c793"), new Guid("d2060993-9551-4b5c-96e8-96852d52fe03")).WithObjectTypes(PartyRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
			
            // PurchaseInvoiceItemStatus
            new RelationTypeBuilder(domain, new Guid("36ee6713-cf38-4ce1-9173-9d7f8aa75ea4"), new Guid("7a4e3240-4f48-4c76-ab15-177066f5cbdb"), new Guid("81879f79-834a-4f00-aad5-ddeae84ddf99")).WithObjectTypes(PurchaseInvoiceItemStatus, PurchaseInvoiceItemObjectState).WithSingularName("PurchaseInvoiceItemObjectState")  .WithPluralName("PurchaseInvoiceItemObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("45a00bdb-cbe4-4170-88e2-20bb55dff33f"), new Guid("5db6dcfb-c4a9-49ea-84f3-f136a184a4fe"), new Guid("e6b78152-6511-4620-81a3-f31709434fc1")).WithObjectTypes(PurchaseInvoiceItemStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // InvoiceVatRateItem
            new RelationTypeBuilder(domain, new Guid("55659771-7862-4fb1-b30c-92a867a6c051"), new Guid("de919633-8651-479c-b3dc-5f510a6d2c4a"), new Guid("d000219c-6ff5-42d2-a65f-da0a7897a00a")).WithObjectTypes(InvoiceVatRateItem, allorsDecimal).WithSingularName("BaseAmount")  .WithPluralName("BaseAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d3300a5d-8e35-4106-9df9-d1bb25bb0352"), new Guid("14cc26ba-1c75-42dd-8a72-1f20f8692cb7"), new Guid("f83c9130-b540-4b25-b7cf-699333395a9d")).WithObjectTypes(InvoiceVatRateItem, VatRate).WithSingularName("VatRate")  .WithPluralName("VatRates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d7f6ed3a-cd81-418c-81c9-2bba827fe956"), new Guid("644237c5-167a-49f8-887f-5d10a725fd80"), new Guid("d5cd524d-b5ba-4d32-b9ab-03aa08e202b9")).WithObjectTypes(InvoiceVatRateItem, allorsDecimal).WithSingularName("VatAmount")  .WithPluralName("VatAmounts")      .WithPrecision(19).WithScale(2).Build();
			
            // SalaryStep
            new RelationTypeBuilder(domain, new Guid("162b31b7-78fd-4ec5-95f7-3913be0662e2"), new Guid("c00111ef-5eb8-4155-a621-fd09d0aa1a6c"), new Guid("2872381c-833b-4dce-83f4-a56bbbd416b3")).WithObjectTypes(SalaryStep, allorsDateTime).WithSingularName("ModifiedDate")  .WithPluralName("ModifiedDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("7cb593b7-48ac-4049-b78c-1e84bdd2fa3a"), new Guid("39c58f18-a640-4c5e-9878-2f82ea90bd0a"), new Guid("553fe45b-2c69-432d-9686-c2f049610eaa")).WithObjectTypes(SalaryStep, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // DropShipmentStatus
            new RelationTypeBuilder(domain, new Guid("2c667e0e-f489-470d-9f48-09a443588286"), new Guid("4f08a754-40ac-4806-bf34-fcc4b0d473af"), new Guid("bd4fb4a3-c2ba-4ed6-8f09-4d7e8e8e3a9a")).WithObjectTypes(DropShipmentStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("3fe0bb83-1d87-4a7c-bda8-796cdaea9ac3"), new Guid("5098fab9-4d58-43e1-93b5-b95e090952e1"), new Guid("09b78c50-2eef-4f01-9277-8a14a657f6b9")).WithObjectTypes(DropShipmentStatus, DropShipmentObjectState).WithSingularName("DropShipmentObjectState")  .WithPluralName("DropShipmentObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PaymentApplication
            new RelationTypeBuilder(domain, new Guid("1147413e-9b57-45b3-a15c-44923e83001a"), new Guid("fba37a60-bd3c-4218-a0b8-64d3f60a7057"), new Guid("23382708-15b9-4d96-8c87-599c80fd2f74")).WithObjectTypes(PaymentApplication, allorsDecimal).WithSingularName("AmountApplied")  .WithPluralName("AmountsApplied")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b5f00552-5976-4368-9f38-dc4734b1c4af"), new Guid("c51f9be5-aee5-43db-b986-78e076ded8bf"), new Guid("df69429e-b21d-4b28-83b3-17f365ac444d")).WithObjectTypes(PaymentApplication, InvoiceItem).WithSingularName("InvoiceItem")  .WithPluralName("InvoiceItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d2a02ce6-569d-41ae-b54d-4a2347b84835"), new Guid("6a5043e4-be26-42fb-80c5-f60ac6af0284"), new Guid("8dc0f6e8-a4fd-44d0-93f5-963c3265033b")).WithObjectTypes(PaymentApplication, Invoice).WithSingularName("Invoice")  .WithPluralName("Invoices")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("deb07a2f-6344-4888-bd1a-97413e82700a"), new Guid("1c722ac2-b579-4707-8e27-0b0a23510293"), new Guid("345cb457-a401-4590-b21c-de3a213a5626")).WithObjectTypes(PaymentApplication, BillingAccount).WithSingularName("BillingAccount")  .WithPluralName("BillingAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // NonSerializedInventoryItemStatus
            new RelationTypeBuilder(domain, new Guid("590f1d9b-a805-4b0e-a2bd-8e274608fe3c"), new Guid("bcc05955-7c14-45f9-b2ea-ab36feca7287"), new Guid("30a81620-9871-414c-9b09-c2fad0358bb4")).WithObjectTypes(NonSerializedInventoryItemStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("959aa0a9-a197-4eb4-bc9e-e40da8892dd0"), new Guid("d3b7eb35-52d2-48fc-a416-a2185ae347ee"), new Guid("78059f92-3345-4e5a-8d04-d30d15eee05a")).WithObjectTypes(NonSerializedInventoryItemStatus, NonSerializedInventoryItemObjectState).WithSingularName("NonSerializedInventoryItemObjectState")  .WithPluralName("NonSerializedInventoryItemObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Depreciation
            new RelationTypeBuilder(domain, new Guid("83ae8e4e-c4cd-4f27-b5fd-b468e4603295"), new Guid("031bc098-9f75-4ced-bcca-0f35519887b2"), new Guid("9e2be493-0100-474c-a49b-00b69c8d8ce1")).WithObjectTypes(Depreciation, FixedAsset).WithSingularName("FixedAsset")  .WithPluralName("FixedAssets")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Territory
            new RelationTypeBuilder(domain, new Guid("9e3780d3-887f-458c-937c-379b22205e2f"), new Guid("241f1107-e802-4c2f-b0e5-80f42b3f916b"), new Guid("3b19bc32-8d8e-404c-80c5-9671408a630e")).WithObjectTypes(Territory, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // SalesOrder
            new RelationTypeBuilder(domain, new Guid("108a1136-feaa-45b8-a899-d455718090d1"), new Guid("a2df509a-6923-4121-9159-8d55b91fd407"), new Guid("7cf7c405-f20c-4416-84f5-a4ff05412162")).WithObjectTypes(SalesOrder, ContactMechanism).WithSingularName("TakenByContactMechanism")  .WithPluralName("TakenByContactMechanism")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("19b2f705-c5c7-4440-9b44-96783802ead0"), new Guid("9ce4d748-c7a2-445e-adf1-35ab41edbfe8"), new Guid("4a78e297-5ede-41a9-a487-43509450be2f")).WithObjectTypes(SalesOrder, SalesOrderStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("19dc0809-46cb-4c37-923c-6bc29a357ba8"), new Guid("bb864baf-b34d-48ab-87e4-e26d82cd4149"), new Guid("2bc32ce8-1202-4091-934a-c89ef338a5fe")).WithObjectTypes(SalesOrder, SalesOrderStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("209f99fb-2e14-45fe-9533-a56573a8c115"), new Guid("f00c8458-3fc7-4279-b3b0-58902ff3bf1d"), new Guid("49399720-aa62-4f69-a20a-1d1e0b8d3978")).WithObjectTypes(SalesOrder, SalesOrderStatus).WithSingularName("CurrentPaymentStatus")  .WithPluralName("CurrentPaymentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("28359bf8-506e-41db-a86b-a1eee3d50198"), new Guid("9a1a8d51-904d-480e-869f-66f5edae0ccd"), new Guid("de181822-ac8e-4a85-af28-b217aa9fcfcd")).WithObjectTypes(SalesOrder, Party).WithSingularName("ShipToCustomer")  .WithPluralName("ShipToCustomers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2bd27b4c-37fd-4f82-bd43-4301ac704749"), new Guid("39389068-26bb-4e3b-b816-ef5730761301"), new Guid("97e7045d-cf35-46ee-acfc-34f6b2572096")).WithObjectTypes(SalesOrder, Party).WithSingularName("BillToCustomer")  .WithPluralName("BillToCustomers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2d097a42-0cfd-43d7-a683-2ae94b9ddaf1"), new Guid("2921dfd5-e57c-4686-b95d-54da85af6604"), new Guid("683dcf30-f20f-44fa-947b-e8b1901b5165")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("TotalPurchasePrice")  .WithPluralName("TotalPurchasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2ee793c8-512e-4358-b28a-f364280db93f"), new Guid("fce2bfd3-8f68-4c9f-a1a3-dce309767458"), new Guid("d123ca45-1afb-4403-9b88-2a5a135d0e60")).WithObjectTypes(SalesOrder, ShipmentMethod).WithSingularName("ShipmentMethod")  .WithPluralName("ShipmentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("30abf0e0-08a3-441a-a91e-09ab14199689"), new Guid("009552df-953c-4170-bbbf-495c8746d6c0"), new Guid("403e22eb-805b-4fff-9b1f-0243d215d9fd")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("TotalListPriceCustomerCurrency")  .WithPluralName("TotalListPricesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("30ddd003-9055-4c1b-8bbb-af75a54da66d"), new Guid("aed47f4f-411d-49ee-9327-5543761d16b5"), new Guid("2b2ee710-ba86-4c7b-ba0e-443e229bec23")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("MaintainedProfitMargin")  .WithPluralName("MaintainedProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("3a2be2f2-2608-46e0-b1f1-1da7e372b8f8"), new Guid("11b71189-8551-467d-9c50-07afe152bdc0"), new Guid("86ca98fe-6bc1-44ce-984e-23ed2f51e9b1")).WithObjectTypes(SalesOrder, PostalAddress).WithSingularName("ShipToAddress")  .WithPluralName("ShipToAddresses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3d01b9c9-5f37-40a8-9305-8ee9e98cc192"), new Guid("e93969bc-b73c-4fec-aec2-aa557c57e844"), new Guid("0100d1ae-c1a6-4b4d-b904-59a2f337e158")).WithObjectTypes(SalesOrder, Party).WithSingularName("PreviousShipToCustomer")  .WithPluralName("PreviousShipToCustomers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("469084d5-acc5-4fc9-910b-ead4d8d4d021"), new Guid("0cc79ccb-af0e-4025-a4f1-3ec5d4f16b96"), new Guid("9640b6a4-f926-48d7-96a2-6c8a0e54cd6b")).WithObjectTypes(SalesOrder, ContactMechanism).WithSingularName("BillToContactMechanism")  .WithPluralName("BillToContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4958ae32-6bc0-451d-bacc-8b7244a9dc56"), new Guid("bf8525ec-1fdf-4bae-9fd9-85bb4aa54400"), new Guid("6281e7d9-d7b8-4611-83f4-e1bdb44cc5f9")).WithObjectTypes(SalesOrder, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4f3cf098-b9d8-4c10-8317-ea2c05ebc4b0"), new Guid("6d3492d0-dda6-41a0-a7e4-32bbccb237f5"), new Guid("4ab44f50-c591-47ec-b6ce-130b5e8791f8")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("InitialProfitMargin")  .WithPluralName("InitialProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6a34d3be-5b38-476e-8894-24db55d44ca6"), new Guid("d814f8bc-2650-4873-b32a-b17a42c3378a"), new Guid("3321bcfc-4de1-44be-9574-3a4b2130ff57")).WithObjectTypes(SalesOrder, SalesOrderObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7c5206f5-391d-485d-a030-513450f4dd2f"), new Guid("1086a778-17dd-4984-b73b-a5629a9b8e7c"), new Guid("1020ff0a-e353-418a-9111-c61a5216032d")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("TotalListPrice")  .WithPluralName("TotalListPrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8f27c21b-ac66-4851-90d8-e955ef31bbec"), new Guid("bfa36bda-784b-4540-a7da-813d37e24c56"), new Guid("fc27baa3-1bdb-44a7-9848-431fbc8ef91e")).WithObjectTypes(SalesOrder, allorsBoolean).WithSingularName("PartiallyShip")  .WithPluralName("PartiallyShips")      .Build();
            new RelationTypeBuilder(domain, new Guid("a012c48a-823a-4a4f-a251-33cfd3056ae2"), new Guid("0b2de73e-2fa1-4fa3-8df4-3c840c58babd"), new Guid("a5fab0ee-f955-4b36-a914-10479ee16b82")).WithObjectTypes(SalesOrder, SalesOrderStatus).WithSingularName("PaymentStatus")  .WithPluralName("PaymentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a1d8e768-0a81-409d-ac13-7c7b8f5081f0"), new Guid("e2de9a21-d93a-4668-9991-1cda6dcab18e"), new Guid("464890a7-d099-4d3d-9ffb-66d79858a579")).WithObjectTypes(SalesOrder, Party).WithSingularName("Customer")  .WithPluralName("Customers")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a54ff0dc-5adb-4314-8081-66522431b11d"), new Guid("9af36608-dc4a-4197-a7a6-77a2cc3bdfd4"), new Guid("d40d841d-a4f0-4e14-b726-3a66f3628ead")).WithObjectTypes(SalesOrder, Store).WithSingularName("Store")  .WithPluralName("Stores")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a5746883-0ad8-4efb-931c-799b8f33ce63"), new Guid("a6333d69-b7e9-4694-8c97-63742a532c28"), new Guid("90d4d0ec-7a65-417b-9b63-05e0fa73070a")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("MaintainedMarkupPercentage")  .WithPluralName("MaintainedMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("aa416d3e-0f75-4fa5-97e0-ef0bc4327ea9"), new Guid("6bd25de8-38a0-4005-baae-fe1339c24bbd"), new Guid("d99b70e4-1ad2-46a3-b362-26880a843ff8")).WithObjectTypes(SalesOrder, ContactMechanism).WithSingularName("BillFromContactMechanism")  .WithPluralName("BillFromContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b9f315a5-22dc-4cba-a19f-fe71fe56ca49"), new Guid("9b59abe7-e3ae-4899-a233-71e9df67555a"), new Guid("44f187d7-afed-47c8-b318-454a3982c8af")).WithObjectTypes(SalesOrder, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ba592fc9-78bb-4102-b9b5-fa692210dc38"), new Guid("207a2983-64c8-41c1-a97f-eb1e8bb78919"), new Guid("0a0596d8-8717-466a-9321-02fc8f3410d3")).WithObjectTypes(SalesOrder, ContactMechanism).WithSingularName("PlacingContactMechanism")  .WithPluralName("PlacingContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c4d3ceff-ccca-47e7-9749-cfae1c1154bf"), new Guid("7c6cff28-d2b9-434c-acae-1151d1d9dcea"), new Guid("579807ac-8c19-4323-905a-068bb0bc7f9f")).WithObjectTypes(SalesOrder, SalesOrderStatus).WithSingularName("CurrentOrderStatus")  .WithPluralName("CurrentOrderStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c90e107b-6b47-4337-9937-391eacd1b1c5"), new Guid("f496f8ec-b2f8-4264-96bc-6d6567b46d11"), new Guid("87824813-0d60-4e7e-af9c-5ad441913820")).WithObjectTypes(SalesOrder, Party).WithSingularName("PreviousBillToCustomer")  .WithPluralName("PreviousBillToCustomers")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ce771472-d789-4077-80bb-25622624e1df"), new Guid("6d50f1f0-8e69-4fca-960e-31c48bddadea"), new Guid("f7da8b79-e2cd-492d-a721-88d3c8fc530c")).WithObjectTypes(SalesOrder, SalesChannel).WithSingularName("SalesChannel")  .WithPluralName("SalesChannels")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d6714c09-dce1-4182-aa2f-bbc887edc89a"), new Guid("9d679860-d975-4a0a-aef4-08975f45d855"), new Guid("23fc03a8-a44c-431f-bdfa-75905691764b")).WithObjectTypes(SalesOrder, Party).WithSingularName("PlacingCustomer")  .WithPluralName("PlacingCustomers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d85bdcb7-cfce-4bfd-9fd3-dfe039138be1"), new Guid("38f0331a-3a80-4f11-b19a-cfedfe89d520"), new Guid("51aab220-38bd-42f9-b33b-ce384f0e4471")).WithObjectTypes(SalesOrder, SalesOrderStatus).WithSingularName("OrderStatus")  .WithPluralName("OrderStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("da5a63d2-33bb-4da3-a1bf-064280cac0fa"), new Guid("05da158d-3f90-4c1f-9bdf-22263b285ed1"), new Guid("d2390ce6-ea3d-43da-9a48-966e9274bcc2")).WithObjectTypes(SalesOrder, SalesInvoice).WithSingularName("ProformaInvoice")  .WithPluralName("ProformaInvoices")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("eb5a3564-996d-4bbe-b592-6205adad93b8"), new Guid("37612ba0-d689-49ca-9005-3b3bf21cd272"), new Guid("bd5d13f3-c1e7-4eea-9c33-09f4b47289f3")).WithObjectTypes(SalesOrder, SalesOrderItem).WithSingularName("SalesOrderItem")  .WithPluralName("SalesOrderItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f20ac339-0761-410b-bbb6-6fb393bcba8a"), new Guid("cf9fcfa0-a862-4cb2-88b4-c4dcd6d0034d"), new Guid("6ec955c9-6d25-45b3-a4d1-5e4270d28750")).WithObjectTypes(SalesOrder, SalesOrderObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f7b7b4d2-fd9e-4d29-99be-f69b2967cc3b"), new Guid("ae9335e4-4d72-40fc-b028-dcfd7ea67cfa"), new Guid("d70334f4-c9f6-4804-a887-2969d75c8644")).WithObjectTypes(SalesOrder, allorsDecimal).WithSingularName("InitialMarkupPercentage")  .WithPluralName("InitialMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ff972b6c-ab12-4596-a1bc-18f93127ac31"), new Guid("556771a7-0f67-4061-8c88-c8401bf0b1c1"), new Guid("6d7ee57e-35b3-4a19-ad1f-4a1850c41568")).WithObjectTypes(SalesOrder, InternalOrganisation).WithSingularName("TakenByInternalOrganisation")  .WithPluralName("TakenByInternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // AgreementTerm
            new RelationTypeBuilder(domain, new Guid("85cd1bbd-f2ad-454f-8f04-cdea48ce6196"), new Guid("c28f6487-83a7-49b3-911d-21f691ae7d02"), new Guid("7eb5e9b4-834f-4774-ac40-11bd455a6ea8")).WithObjectTypes(AgreementTerm, allorsString).WithSingularName("TermValue")  .WithPluralName("TermValues")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("b38a35f7-3bf5-4c9c-b2ea-6b220de43e20"), new Guid("a2786ad1-c4b0-4394-8841-ad14de467bc4"), new Guid("7e1163db-78d1-4c63-b10a-d1315ccb223c")).WithObjectTypes(AgreementTerm, TermType).WithSingularName("TermType")  .WithPluralName("TermTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d9a68cc0-8fea-4610-9853-f1fca33cbc9a"), new Guid("35b4a0af-89c6-44e0-981f-439b632a6d51"), new Guid("9b734740-dc58-428e-8031-de5341e5aae7")).WithObjectTypes(AgreementTerm, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // SalesRepRevenue
            new RelationTypeBuilder(domain, new Guid("0bf9f020-7704-4e4e-92f6-06e747dc9463"), new Guid("7ca286bb-a26d-4b7a-bfbe-8305d885d035"), new Guid("7cfa4dad-e2a4-4c95-b25f-476a8a2b7521")).WithObjectTypes(SalesRepRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("70b2fc04-ce4e-4af7-b287-02883fe660e9"), new Guid("48f05073-776f-4465-9763-ca71c785c058"), new Guid("d5009f5b-b990-465d-b868-0bf977b33a4c")).WithObjectTypes(SalesRepRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("730e4a76-6af4-441e-8d82-1f3e5807d5a5"), new Guid("6ad611d9-a2a1-46cc-9ac2-28832723f063"), new Guid("5a92ec5a-4128-46ba-9a75-112592f2662d")).WithObjectTypes(SalesRepRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("89ff9736-f2d1-4609-ac99-b60f5b37f406"), new Guid("86e77422-4347-4e97-99ba-1c3b7cf57220"), new Guid("5c19cc4e-4599-48b7-ab74-a1be3509317d")).WithObjectTypes(SalesRepRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("b1aa9e43-5ccc-4e1d-821e-39af02321a79"), new Guid("092108de-fb58-4fb2-b844-443fd476a383"), new Guid("d1667908-4149-45dd-949e-ab00fbf3c7c4")).WithObjectTypes(SalesRepRevenue, allorsString).WithSingularName("SalesRepName")  .WithPluralName("SalesRepNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b72d2ab7-ad47-41dd-8dab-4b6364efc342"), new Guid("6160b809-a3a4-434a-b05a-cfdb1a3a1dd4"), new Guid("6e712c98-649e-49cc-9484-0a0d407f02a7")).WithObjectTypes(SalesRepRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("be530b0c-6ab9-43a2-a974-f06015ae3480"), new Guid("cd786202-3d96-4f6b-94af-27ffb92608e3"), new Guid("0bebaa7b-9332-44a6-abff-454175d2f2a5")).WithObjectTypes(SalesRepRevenue, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Engagement
            new RelationTypeBuilder(domain, new Guid("135792e4-42ad-4bf5-914b-ffc154330cd1"), new Guid("d0975157-1342-4a01-9fec-2a38f68a6080"), new Guid("3cd0c697-6767-429a-8f34-e648b4fda46c")).WithObjectTypes(Engagement, Agreement).WithSingularName("Agreement")  .WithPluralName("Agreements")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1fb112f4-9628-40a8-9531-2d9ad24103ff"), new Guid("ad13c054-b721-4941-ad25-cc43936fed36"), new Guid("9c189e14-b734-41b5-9012-8651986039d7")).WithObjectTypes(Engagement, ContactMechanism).WithSingularName("PlacingContactMechanism")  .WithPluralName("PlacingContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2e703224-c40a-45ee-8703-cafe11eda70a"), new Guid("f50ccbe1-9277-44a3-bb9b-5837124ddb6c"), new Guid("468b2214-51d3-47e0-9207-9711f71f45ca")).WithObjectTypes(Engagement, allorsDecimal).WithSingularName("MaximumAmount")  .WithPluralName("MaximumAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4afffb12-3a70-4903-af99-ed814fd6a444"), new Guid("1bf53c71-0b09-4053-8bab-73d783fdfd62"), new Guid("dc2b8ab4-6eff-488c-955b-6329c1e1bfc3")).WithObjectTypes(Engagement, ContactMechanism).WithSingularName("BillToContactMechanism")  .WithPluralName("BillToContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("55102c87-3cea-4c53-bafb-eb94bdda2b44"), new Guid("7453a2d2-368d-42dc-9304-c4706215361f"), new Guid("97a86549-607b-48f0-bd92-4b3079a9f659")).WithObjectTypes(Engagement, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5b758dbc-11f6-4ac4-8f2b-639937814cae"), new Guid("9257851c-ab25-41c2-bbe5-6fb6ff8942af"), new Guid("16370a01-5eaf-4c19-ad27-f0ede8f1ffef")).WithObjectTypes(Engagement, Party).WithSingularName("BillToParty")  .WithPluralName("BillToParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5f524932-4787-4bb4-8785-9a4f67dbb6ed"), new Guid("8dee87d0-f7ae-4f0c-b890-63e8642c4c90"), new Guid("553bf637-0527-40cb-a298-4b41530f0950")).WithObjectTypes(Engagement, Party).WithSingularName("PlacingParty")  .WithPluralName("PlacingParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("63b73232-d81c-448f-92b0-eac314fcf41d"), new Guid("fce9a377-1b5a-4b23-bf1f-3f82a984cc4f"), new Guid("882c84e0-c101-42cf-bb98-4939d68a5011")).WithObjectTypes(Engagement, InternalOrganisation).WithSingularName("TakenViaInternalOrganisation")  .WithPluralName("TakenViaInternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6ca9444e-3e1c-4631-ad4e-1025fc85c1a4"), new Guid("9e82a268-e421-42f4-8b6f-460b3b1ce8aa"), new Guid("108359bc-bb0a-426e-a6eb-7ab6de874721")).WithObjectTypes(Engagement, allorsDateTime).WithSingularName("StartDate")  .WithPluralName("StartDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("83acc3c0-e87a-48fd-9c10-5070cd7c2a3d"), new Guid("4c014a78-e093-46e7-9c92-6404b3351f63"), new Guid("e443cf26-a947-4bee-b0a9-3588434edb09")).WithObjectTypes(Engagement, ContactMechanism).WithSingularName("TakenViaContactMechanism")  .WithPluralName("TakenViaContactMechanism")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9bc7fbff-11dd-434e-91ff-7cef4e225bb3"), new Guid("950e4e27-42a4-4feb-bc7c-88a1045f6cc6"), new Guid("f7234a3a-cb7c-4d92-903a-710ef833773b")).WithObjectTypes(Engagement, allorsDecimal).WithSingularName("EstimatedAmount")  .WithPluralName("EstimatedAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d9df5d5e-e0cc-4c9e-9e0a-dc5423561774"), new Guid("4fec3a1a-28d4-4984-a82a-aee949ba79d5"), new Guid("abcb554b-44f8-424c-b991-1e56f15c5412")).WithObjectTypes(Engagement, allorsDateTime).WithSingularName("EndDate")  .WithPluralName("EndDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("e1081976-b7e4-4e8e-85de-bd6ff096b39b"), new Guid("947ccaf8-9264-4703-86a8-58818128ff84"), new Guid("5670bb8d-64a0-4492-9bb7-383b27144b31")).WithObjectTypes(Engagement, allorsDateTime).WithSingularName("ContractDate")  .WithPluralName("ContractDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("ec51db38-d0de-430d-82cc-1105f336977b"), new Guid("f1a3abd2-636b-441a-90fc-9ac4cd9d0936"), new Guid("237743f7-01c5-4762-b034-c43854f22157")).WithObjectTypes(Engagement, EngagementItem).WithSingularName("EngagementItem")  .WithPluralName("EngagementItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f444a4cf-9722-420b-8ba1-0591492929e5"), new Guid("2aaf6afe-5fee-460f-ab37-75aa27be40f1"), new Guid("ffb337c1-e7d0-4b86-b694-43b927efad34")).WithObjectTypes(Engagement, allorsString).WithSingularName("ClientPurchaseOrderNumber")  .WithPluralName("ClientPurchaseOrderNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f7e10109-a4f2-4a54-b810-b00edf8a9330"), new Guid("5b22ae85-d5ae-41f7-a461-201ae48db339"), new Guid("bdcce46f-e800-43fa-8cbc-2d4c5f65a7b2")).WithObjectTypes(Engagement, OrganisationContactRelationship).WithSingularName("TakenViaOrganisationContactRelationship")  .WithPluralName("TakenViaOrganisationContactRelationships")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Part
            new RelationTypeBuilder(domain, new Guid("424cdae9-af7b-4b6f-9e9e-54ac6104873d"), new Guid("54857740-7d0b-4c7d-b71a-9b93719643c5"), new Guid("501dcfd1-143a-46a6-9c04-9ce141702a27")).WithObjectTypes(Part, InternalOrganisation).WithSingularName("OwnedByParty")  .WithPluralName("OwnedByParty")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5239147e-0829-4250-bdbc-8115e9c19206"), new Guid("6f267a60-802b-454f-9ac7-762a92746255"), new Guid("a9efc713-6574-4b82-b20e-0fc22747566a")).WithObjectTypes(Part, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("527c0d02-7723-4715-b975-ec9474d0d22d"), new Guid("b8cce82f-8555-4d15-8012-3b122ad47b3d"), new Guid("72e60215-a8fb-40a1-ac9b-0204421adde0")).WithObjectTypes(Part, PartSpecification).WithSingularName("PartSpecification")  .WithPluralName("PartSpecifications")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("610f6c8c-0d1d-4c8e-9d3d-a98e17d181b5"), new Guid("00a2efd5-0a43-4b86-8ce3-2196c2ad7c3d"), new Guid("f843b974-81bf-48a1-9397-8708da48e39c")).WithObjectTypes(Part, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("773e731d-47f7-4742-b8c6-81dec0a09f29"), new Guid("183113ef-8420-444d-8a80-61580a9f95dc"), new Guid("05f1428a-26cd-4f08-9f1d-dec02edf6fe1")).WithObjectTypes(Part, Document).WithSingularName("Document")  .WithPluralName("Documents")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("89a29d59-b56f-4846-a9d2-cf7d094826dc"), new Guid("8527c099-3ea0-486c-b288-ebf7e642952e"), new Guid("84e90f5a-ce0f-4f88-b964-829154e682dd")).WithObjectTypes(Part, allorsString).WithSingularName("ManufacturerId")  .WithPluralName("ManufacturerIds")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8dc701e0-1f66-44ee-acc6-9726aa7d5853"), new Guid("2b9103c7-7ff8-4733-aa02-53800bb6e9bc"), new Guid("6d60fb2f-1893-48ac-9e7d-9aa2a9a89431")).WithObjectTypes(Part, allorsInteger).WithSingularName("ReorderLevel")  .WithPluralName("ReorderLevels")      .Build();
            new RelationTypeBuilder(domain, new Guid("a093c852-cba8-43ff-9572-fd8c6cd53638"), new Guid("8c3d3a61-4d3a-477c-9701-a292435112e3"), new Guid("f2ffce75-82d5-460f-83cc-621d63211d18")).WithObjectTypes(Part, allorsInteger).WithSingularName("ReorderQuantity")  .WithPluralName("ReorderQuantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("a202a540-dc0d-4032-9963-d0aa1511c990"), new Guid("0dd915a3-d517-46c5-8664-e59c56623564"), new Guid("ab316ee2-bf84-4501-a798-94832c55e73f")).WithObjectTypes(Part, PriceComponent).WithSingularName("PriceComponent")  .WithPluralName("PriceComponents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f2c3407e-ab62-4f3e-94e5-7e9e65b89d6e"), new Guid("9bf78bcd-319c-4767-8053-4307577559ff"), new Guid("319781e8-c83c-41ea-a8e7-b7224e8240e0")).WithObjectTypes(Part, InventoryItemKind).WithSingularName("InventoryItemKind")  .WithPluralName("InventoryItemKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // CustomEngagementItem
            new RelationTypeBuilder(domain, new Guid("71a3ed63-922f-44ae-8e89-6425759b3eb3"), new Guid("00621849-ee7b-4a7e-b5c3-7ca2e2d40b3a"), new Guid("2b2d9ceb-cce9-4edd-bbaa-2829b3e5e32f")).WithObjectTypes(CustomEngagementItem, allorsString).WithSingularName("DescriptionOfWork")  .WithPluralName("DescriptionsOfWork")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f0b91526-924e-4f11-b27c-187010e1dff7"), new Guid("21f41aa4-9417-4822-afba-6e424dd936f2"), new Guid("9f787d7c-663d-4856-a3cb-8d65b4802744")).WithObjectTypes(CustomEngagementItem, allorsDecimal).WithSingularName("AgreedUponPrice")  .WithPluralName("AgreedUponPrices")      .WithPrecision(19).WithScale(2).Build();
			
            // AccountingTransaction
            new RelationTypeBuilder(domain, new Guid("4e4cb94c-424c-4824-ad44-5bb1c7312a52"), new Guid("2ed212a9-6c8b-443f-a842-391aa0b6a265"), new Guid("fc36b63a-7fae-414d-adb0-50a86d9fb238")).WithObjectTypes(AccountingTransaction, AccountingTransactionDetail).WithSingularName("AccountingTransactionDetail")  .WithPluralName("AccountingTransactionDetails")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("657f2688-4af0-4580-add2-c8a30b32e016"), new Guid("e7a6ced6-1397-484a-b4c0-5bb7ebbaf9e0"), new Guid("b4b2d735-b895-4112-8d6c-690b0d6f2cc1")).WithObjectTypes(AccountingTransaction, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("77910a3f-3547-4d6b-92e0-f1fc136e22da"), new Guid("97cc6287-9dc0-404a-ad92-bfd2c3927d30"), new Guid("83cfb29d-4311-4b16-9331-1c00d54b70c7")).WithObjectTypes(AccountingTransaction, allorsDateTime).WithSingularName("TransactionDate")  .WithPluralName("TransactionDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("a29cb739-8d2f-4e7d-a652-af8d2e190658"), new Guid("5f295cc2-a884-427b-8fb3-056af4f58b7b"), new Guid("d6cc2527-7a3f-4bac-bf4b-991f484c51a7")).WithObjectTypes(AccountingTransaction, allorsDecimal).WithSingularName("DerivedTotalAmount")  .WithPluralName("DerivedTotalAmounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a7fb7e5a-287a-41a1-b6b9-bd56601732f3"), new Guid("6f71e42e-1fa8-4905-93ef-ff5a417aff46"), new Guid("d5d3a903-748a-4b0e-8da6-c23b304eb62c")).WithObjectTypes(AccountingTransaction, AccountingTransactionNumber).WithSingularName("AccountingTransactionNumber")  .WithPluralName("AccountingTransactionNumbers")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("be061dda-bb8f-4bc1-b386-dc0c05dc6eaf"), new Guid("8943c9e2-3c6f-49c4-aa87-397af24e8073"), new Guid("75b6517f-6e4a-4218-8ca1-de230c69a02e")).WithObjectTypes(AccountingTransaction, allorsDateTime).WithSingularName("EntryDate")  .WithPluralName("EntryDates")      .Build();
			
            // SalesRepPartyRevenue
            new RelationTypeBuilder(domain, new Guid("1671b689-b431-48e3-aa9c-04b67d35645d"), new Guid("0bd0f873-b179-4d95-b1c9-f9b32db5a58e"), new Guid("673851a4-435d-441e-8bc5-9ebae678a9a2")).WithObjectTypes(SalesRepPartyRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2f2c05ed-09b0-4058-9dec-69d2e7b0bdce"), new Guid("3de0784e-21d8-40cc-ad93-699c67b1d996"), new Guid("96a797ef-2830-448d-b08f-1fb54b3a76f2")).WithObjectTypes(SalesRepPartyRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("44124d06-9bbd-4c55-85f6-7036b49ffbcd"), new Guid("b7e31e2e-2e76-4049-870b-ff5848dc4ebc"), new Guid("37984d70-d086-461f-8879-710ecef10778")).WithObjectTypes(SalesRepPartyRevenue, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("85d4036b-dfaf-42e7-935a-cff6858b4b57"), new Guid("a9d39829-f661-4f34-90f4-c6e6f8a81cda"), new Guid("a40e4494-9fcf-42bf-b5f9-5977dc3d3dd7")).WithObjectTypes(SalesRepPartyRevenue, allorsString).WithSingularName("SalesRepName")  .WithPluralName("SalesRepNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9ebbb60b-b198-404b-b16b-1f6848f07c65"), new Guid("ce0e6e38-d0a5-4363-9184-8a14ee71c4e9"), new Guid("827927b3-8d15-473a-8cad-b61b97e3322a")).WithObjectTypes(SalesRepPartyRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a9a0868b-e96d-4da4-9fd8-4cc19fdf4bc1"), new Guid("ab409a8b-dca5-446a-ad27-d255b946555c"), new Guid("3af187a4-a5ce-45e5-9e47-00e66d2e6791")).WithObjectTypes(SalesRepPartyRevenue, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cab7ab56-2068-4090-bdae-b4e42a68ec36"), new Guid("e5f5eefd-0276-450b-815e-4bde07fee1d6"), new Guid("99d895a8-12c9-468c-9081-f600f64a9117")).WithObjectTypes(SalesRepPartyRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d2c46b6c-5aef-4172-be33-1fe4ea7fdce0"), new Guid("f9917c36-de1e-4a42-ae37-6883372873c9"), new Guid("f298e963-b2b1-4cc2-b7aa-2d07fb4e1662")).WithObjectTypes(SalesRepPartyRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
			
            // PurchaseOrderItemStatus
            new RelationTypeBuilder(domain, new Guid("8d0f607b-221b-4f42-8ef8-242e3c35d9ba"), new Guid("2dbd22c3-b84d-4d65-a176-eecd61da3a89"), new Guid("26ed3c20-bad4-4651-9a6e-d6a27616503e")).WithObjectTypes(PurchaseOrderItemStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("d2fad7ff-945e-4bed-b7a7-39238357eaf3"), new Guid("db25aff8-16fe-4277-afc2-843d81ace875"), new Guid("2f2af9c6-9ed4-475b-a9fd-c16c12454109")).WithObjectTypes(PurchaseOrderItemStatus, PurchaseOrderItemObjectState).WithSingularName("PurchaseOrderItemObjectState")  .WithPluralName("PurchaseOrderItemObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Addendum
            new RelationTypeBuilder(domain, new Guid("2aaa6623-6f1a-4b40-91f0-4014108549d6"), new Guid("071735c4-bfbe-4f30-87a4-fbb4accc540c"), new Guid("d9dea2e1-6582-4ce4-863f-4819d2cffe96")).WithObjectTypes(Addendum, allorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("30b99ed6-cb44-4401-b5bd-76c0099153d4"), new Guid("002ba83d-d60f-4365-90e0-4df952697ae7"), new Guid("cfa04c20-ecc5-4942-b898-2966bf5052aa")).WithObjectTypes(Addendum, allorsDateTime).WithSingularName("EffictiveDate")  .WithPluralName("EffictiveDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("45a9d28e-f131-44a8-aea5-1a9776be709e"), new Guid("4b41aff4-1882-4771-a85b-358cabdb6e3c"), new Guid("8b37c47b-3dec-46e6-b669-6497cfdaf14b")).WithObjectTypes(Addendum, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f14af73d-8d7d-4c5b-bc6a-957830fd0a80"), new Guid("5430d382-14ff-4af1-8e1b-3b11142612e4"), new Guid("51fc58ba-e9fb-4919-94e8-c8594f6e4ea5")).WithObjectTypes(Addendum, allorsDateTime).WithSingularName("CreationDate")  .WithPluralName("CreationDates")  .WithIsDerived(true)    .Build();
			
            // WorkEffortType
            new RelationTypeBuilder(domain, new Guid("5ce1a600-62a9-4d2c-bfb5-bfe374b2099f"), new Guid("4d892148-3583-46a2-b68d-895274b9ea7a"), new Guid("fa8657ae-5132-4f37-aba0-4f95c4b1df1e")).WithObjectTypes(WorkEffortType, WorkEffortFixedAssetStandard).WithSingularName("WorkEffortFixedAssetStandard")  .WithPluralName("WorkEffortFixedAssetStandards")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("776839ee-f6cb-4334-a017-4ffdfddd152a"), new Guid("db3c9fba-5ca8-4296-b1a2-d306ad42dbcc"), new Guid("764e51c4-8a6f-403d-849a-1bf3a1a64911")).WithObjectTypes(WorkEffortType, WorkEffortGoodStandard).WithSingularName("WorkEffortGoodStandard")  .WithPluralName("WorkEffortGoodStandards")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("89eef4e3-eda7-4336-91cb-ce7a7e96521f"), new Guid("4b61f74f-db7c-4733-b6f5-d485e432a16e"), new Guid("8b9f019b-e79e-4282-9ee1-9bc652fd6817")).WithObjectTypes(WorkEffortType, WorkEffortType).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8d9f51b5-2c8d-4a25-a45e-c79542a09434"), new Guid("687e9909-0efa-4a04-b705-96d93547458a"), new Guid("da4648c1-b495-4555-8fa3-c4ba8141e67d")).WithObjectTypes(WorkEffortType, FixedAsset).WithSingularName("FixedAssetToRepair")  .WithPluralName("FixedAssetsToRepair")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("93cfed3d-ae24-4a07-becf-34cdc3cdef3e"), new Guid("958d3cdc-0dbe-4ce6-81c7-492825727ada"), new Guid("d0fadb8a-4891-4caf-a010-6197676cfd54")).WithObjectTypes(WorkEffortType, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b6d68eff-8a3a-473f-bb4e-9bc46808bde0"), new Guid("1996225b-a372-44ad-b00a-b257a355d756"), new Guid("2c704b66-922a-4c5c-81fb-973545230501")).WithObjectTypes(WorkEffortType, WorkEffortType).WithSingularName("Dependency")  .WithPluralName("Dependencies")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ccf22455-c42a-4f9c-8975-813431bcdd8b"), new Guid("f70821d5-2a7f-4fdd-ae45-b8c7966710fc"), new Guid("abe54968-9a36-43c5-a57c-b8d1cde032ea")).WithObjectTypes(WorkEffortType, WorkEffortTypeKind).WithSingularName("WorkEffortTypeKind")  .WithPluralName("WorkEffortTypeKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d51d620e-250e-4492-8926-c8535fad19ec"), new Guid("e26db451-eb86-44b1-b3cb-eb29d4311157"), new Guid("2a4de99b-9544-4c67-b936-431622654f09")).WithObjectTypes(WorkEffortType, WorkEffortPartStandard).WithSingularName("WorkEffortPartStandard")  .WithPluralName("WorkEffortPartStandards")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("df104ec4-6247-4199-bce1-635978fa8ad4"), new Guid("0826559c-11ef-4075-ad8e-28c7ed693f1c"), new Guid("073f09b7-1502-4bac-b344-76f4cb6f3907")).WithObjectTypes(WorkEffortType, WorkEffortSkillStandard).WithSingularName("WorkEffortSkillStandard")  .WithPluralName("WorkEffortSkillStandards")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("df1fa89e-25e2-4b72-a928-67fa2c95ad70"), new Guid("361d313b-8313-43bd-8a98-9b2516ca25f7"), new Guid("3d4d3fd1-28dd-4349-bb57-b869687f5f82")).WithObjectTypes(WorkEffortType, allorsDecimal).WithSingularName("StandardWorkHours")  .WithPluralName("StandardWorkHoursPlural")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ee521062-a2bf-4a7f-80e4-8da6f63439fe"), new Guid("fc63a85e-7bc4-49ec-89f5-66fef934f11a"), new Guid("5d9c847c-f2a5-4353-8558-880f60e75925")).WithObjectTypes(WorkEffortType, Product).WithSingularName("ProductToProduce")  .WithPluralName("ProductsToProduce")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f8766ab1-b0ed-42fa-806c-c40a2e68d72a"), new Guid("7e7a9632-76a8-48c3-ada3-fcc3aa06a511"), new Guid("9a3b5dd0-a399-43ef-9607-32136bb5f3cd")).WithObjectTypes(WorkEffortType, Deliverable).WithSingularName("DeliverableToProduce")  .WithPluralName("DeliverablesToProduce")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesInvoiceStatus
            new RelationTypeBuilder(domain, new Guid("22daba0b-1f86-4a00-ba83-c541e65822c6"), new Guid("d28b067f-bd90-45c5-9213-b231ff3bb03f"), new Guid("eb1505fb-6caa-40a3-a09c-1b18fe7dc3ee")).WithObjectTypes(SalesInvoiceStatus, SalesInvoiceObjectState).WithSingularName("SalesInvoiceObjectState")  .WithPluralName("SalesInvoiceObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("74c60d54-b75f-4baa-b1d6-5a33e8ab3944"), new Guid("ea6bf951-414e-48e6-a579-a2ce8627f635"), new Guid("22405cc3-c402-4236-9517-bdb381d3285f")).WithObjectTypes(SalesInvoiceStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // PurchaseInvoice
            new RelationTypeBuilder(domain, new Guid("3e06d6c9-50b1-4cf6-95f9-0278b62c0c30"), new Guid("8e2f7243-738f-4e1c-9531-7160c627365a"), new Guid("f596e5ab-59e1-4b0a-9c56-0d22a23c4b0f")).WithObjectTypes(PurchaseInvoice, PurchaseInvoiceObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4cf09eb7-820f-4677-bfc0-92a48d0a938b"), new Guid("5a71ca58-db28-4edc-9065-32396380bd80"), new Guid("fa280c8d-ac7b-4d99-80dd-fba155d4aef9")).WithObjectTypes(PurchaseInvoice, PurchaseInvoiceItem).WithSingularName("PurchaseInvoiceItem")  .WithPluralName("PurchaseInvoiceItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("86859b7b-e627-43fe-ba75-711d4c104807"), new Guid("ba1aeb33-0351-4fbf-b80c-881cdf4ded5c"), new Guid("7caa47ab-1f54-4fad-87b8-639b37269635")).WithObjectTypes(PurchaseInvoice, InternalOrganisation).WithSingularName("BilledToInternalOrganisation")  .WithPluralName("BilledToInternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8f9e98b7-c87c-47c7-a267-3044c7414534"), new Guid("1b4b2f6b-7294-428f-b0ea-beb43050557a"), new Guid("bea637e9-c320-4bdb-ac4b-d571e3fa0c8d")).WithObjectTypes(PurchaseInvoice, PurchaseInvoiceStatus).WithSingularName("CurrentInvoiceStatus")  .WithPluralName("CurrentInvoiceStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bc059d0f-e9bd-41e8-82ff-9615a01ec24a"), new Guid("770c0376-8552-4d0c-b45f-b759018c3c85"), new Guid("5658422f-4097-49db-b97c-79bab6f337b4")).WithObjectTypes(PurchaseInvoice, PurchaseInvoiceObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d4bbc5ed-08a4-4d89-ad53-7705ae71d029"), new Guid("8ce81b66-22e5-4195-a270-5e9f761ff51e"), new Guid("58245287-7a75-45c4-a000-d3944ec9319a")).WithObjectTypes(PurchaseInvoice, Party).WithSingularName("BilledFromParty")  .WithPluralName("BilledFromParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e444b5e7-0128-49fc-86cb-a6fe39c280ae"), new Guid("d6240de5-9b99-4525-b7d0-ef28a3381821"), new Guid("6c911870-2737-4997-87a6-65ca55c17c55")).WithObjectTypes(PurchaseInvoice, PurchaseInvoiceType).WithSingularName("PurchaseInvoiceType")  .WithPluralName("PurchaseInvoiceTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ed3987d4-3dd1-4483-bcbb-8f1f0b18ff84"), new Guid("1c1d90ff-5910-4f39-b6ad-aa12a6e6c60e"), new Guid("d23c55ff-857b-40bc-b041-15f0ceb910a5")).WithObjectTypes(PurchaseInvoice, PurchaseInvoiceStatus).WithSingularName("InvoiceStatus")  .WithPluralName("InvoiceStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // CustomerReturn
            new RelationTypeBuilder(domain, new Guid("29a65898-2f91-4163-a5ed-ccb8cd5b17cb"), new Guid("145f4e1b-b26d-4e44-8cc9-3afb537c58b2"), new Guid("71416b87-4614-4d87-886c-4fe2eb936f40")).WithObjectTypes(CustomerReturn, CustomerReturnStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b28765aa-3529-449a-9ef1-539323b7f3b2"), new Guid("475243a3-8d87-4521-bbb6-b5388292268c"), new Guid("0084281a-3bcc-4de2-b581-037c3724259a")).WithObjectTypes(CustomerReturn, CustomerReturnObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e7586be1-f751-4ac6-940b-a65b50834619"), new Guid("ca71aca7-fa06-44d1-830a-3eaf5e2355a2"), new Guid("3fb0c486-6e24-4f53-b7cf-f98596402d55")).WithObjectTypes(CustomerReturn, CustomerReturnStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fe3fd846-2d69-4d62-941b-dabc40a15e1f"), new Guid("82695003-f47f-4324-9a7c-d89949981354"), new Guid("b2d65c28-fbff-430b-a7ca-39201ce655ad")).WithObjectTypes(CustomerReturn, CustomerReturnObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // Order
            new RelationTypeBuilder(domain, new Guid("03e5ee2e-91b1-4891-aa14-0afb2459d733"), new Guid("66a7b612-b226-4608-8aaf-1866ee0b5e79"), new Guid("10f74736-c20f-4618-9c80-931c2f428aa8")).WithObjectTypes(Order, Currency).WithSingularName("CustomerCurrency")  .WithPluralName("CustomerCurrencies")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1e94a270-3780-43fc-9b62-611f259b04fd"), new Guid("edabe0af-99a5-4a67-b6e1-66c341ad945d"), new Guid("8138ed27-5520-458a-bc18-8d684daa4649")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalBasePriceCustomerCurrency")  .WithPluralName("TotalBasePricesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("24520951-4088-4c60-adaa-dd7bf00257de"), new Guid("6c4e690b-0f91-4fa6-bfa1-755048304dbb"), new Guid("ca53ee57-213a-4449-8fc8-f04f2737a8a6")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalIncVatCustomerCurrency")  .WithPluralName("TotalIncVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("35451f53-5a0e-443d-a9f7-36620a832b02"), new Guid("b93f7956-ffe3-4793-a5d5-ee60bf197e6e"), new Guid("6b2890e7-f8e2-479d-8155-51adeade6799")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalDiscountCustomerCurrency")  .WithPluralName("TotalDiscountsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("45b3b293-b746-4d6d-9da7-e2378694f734"), new Guid("5e1ba42d-9325-45b5-9c41-cc9b12d0929a"), new Guid("019d8b7a-79db-4100-a690-ae7587e30d8e")).WithObjectTypes(Order, allorsString).WithSingularName("CustomerReference")  .WithPluralName("CustomerReferences")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("615a233a-659a-44cc-b056-fe02643cbeed"), new Guid("847041e8-d780-4640-803d-927b23f7932f"), new Guid("18ff7372-059c-4717-9d9f-a3a20ea5a7ba")).WithObjectTypes(Order, Fee).WithSingularName("Fee")  .WithPluralName("Fees")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6509263c-a11e-4554-b13d-4fa075fa8ed9"), new Guid("21bd72d4-b309-452c-a73c-49c7b926aca7"), new Guid("2ba85811-2046-407d-a3a9-a53e05afe3ed")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalExVat")  .WithPluralName("TotalExVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("73521788-7e0e-4ea2-9961-1a58f68cde5c"), new Guid("8e7ad6ef-7a40-472f-b7b9-f53a77e51548"), new Guid("af64e731-adfb-414a-9520-51d4ea2c8f81")).WithObjectTypes(Order, OrderTerm).WithSingularName("OrderTerm")  .WithPluralName("OrderTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7374e62f-0f0b-49de-8c70-9ef224a706b1"), new Guid("cc79b674-ec5d-48d7-b296-c172f372b2b4"), new Guid("29126171-cae2-4b50-89c2-7df91ab71444")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalVat")  .WithPluralName("TotalVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("751cb60a-b8ba-473a-ab95-0909bd2bc61c"), new Guid("1fb281bd-40cd-45f4-bf37-b7b15ec646d7"), new Guid("24a7b556-4674-42ed-97d9-0e0c466f5fd0")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalSurcharge")  .WithPluralName("TotalSurcharges")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7c04f907-4254-4b59-861a-7b545c12b3d3"), new Guid("6e8ff513-f6e2-411f-b679-1eda15e0f577"), new Guid("795117b2-8b5a-4562-9acc-d77d2f93256a")).WithObjectTypes(Order, OrderItem).WithSingularName("ValidOrderItem")  .WithPluralName("ValidOrderItems")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7db0e5f7-8a23-4be8-beba-8ddfd1972856"), new Guid("084ad016-6eaf-4cc9-aedd-80a4ba161067"), new Guid("4acb8c07-e132-4b35-8e0c-416cdf4da35b")).WithObjectTypes(Order, allorsString).WithSingularName("OrderNumber")  .WithPluralName("OrderNumbers")  .WithIsDerived(true)    .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7f25a14f-c32f-4a86-ae2d-9f087f8b8214"), new Guid("5a227b86-4ea3-4ce9-89df-a672337ecd1d"), new Guid("4ba525e4-7e31-4b9e-9fc4-8a26e9e352a1")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalVatCustomerCurrency")  .WithPluralName("TotalVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8592f390-a9fb-4275-93c2-b7e73afa2307"), new Guid("703bf4a7-8949-46ea-b7d3-092ab62c9bdd"), new Guid("1420978d-4e64-434e-926e-e26bbce2dd1f")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalDiscount")  .WithPluralName("TotalDiscounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8c972fae-b3ba-4e88-b769-d59c14325b00"), new Guid("a42b384a-3ec0-4c79-af3b-cccc510c019f"), new Guid("d16485cb-983f-4526-b695-01d0d09f3742")).WithObjectTypes(Order, allorsString).WithSingularName("Message")  .WithPluralName("Messages")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("9fd3ea50-280e-4d0d-a9db-450991248a53"), new Guid("5c3af601-e6f8-4adc-af83-88058d86975a"), new Guid("7badb3f0-51a2-4e9b-bdc7-3d3bfbf7865f")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalShippingAndHandlingCustomerCurrency")  .WithPluralName("TotalShippingAndHandlingsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a5875c41-9f08-49d0-9961-19a656c7e0cc"), new Guid("c6604ee5-e9f2-4b5a-9f08-fbfa1d126402"), new Guid("6a783dbf-0f8d-4249-8e1e-6c0c2a61a97e")).WithObjectTypes(Order, allorsDateTime).WithSingularName("EntryDate")  .WithPluralName("EntryDates")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("addf2b1e-a7c1-4ba8-94f0-13c99d2b8f63"), new Guid("c9ccf0b5-b3a2-4a46-a035-4567215ce48a"), new Guid("2ad9cab5-25ea-49d2-9f70-145de25b2170")).WithObjectTypes(Order, DiscountAdjustment).WithSingularName("DiscountAdjustment")  .WithPluralName("DiscountAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b205a525-fc61-436d-a66a-1a18bcfb5aff"), new Guid("142ba77b-066d-4514-a663-1859be50e29e"), new Guid("fc9546af-4e2d-485f-806f-cbfce23a7314")).WithObjectTypes(Order, OrderKind).WithSingularName("OrderKind")  .WithPluralName("OrderKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ba6e8dd3-ad74-4ead-96df-d9ba2e067bfc"), new Guid("a75c25e4-c88d-4d2b-981a-c5b561264e87"), new Guid("77ce4873-d2fa-4f0b-ba8d-0403886d613c")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalIncVat")  .WithPluralName("TotalIncVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c2c10a98-2518-4243-8d58-4bee6316abc5"), new Guid("31d26b09-fde7-49b0-b800-718614f216d9"), new Guid("61b0d93a-a36d-470f-b25d-089a7b209457")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalSurchargeCustomerCurrency")  .WithPluralName("TotalSurchargesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c6f86f31-d254-4001-94fa-273d041df31a"), new Guid("0716922b-d051-459c-83c9-4390fa7723d0"), new Guid("58d1bcc3-332a-4830-b346-702efedaa010")).WithObjectTypes(Order, VatRegime).WithSingularName("VatRegime")  .WithPluralName("VatRegimes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c80447e8-4fc8-4491-8857-3129a007a267"), new Guid("a8210460-e40e-4fab-8adb-bb8d57af0ad6"), new Guid("21b64676-ffa0-4e8c-9850-89096f7f5eff")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalFeeCustomerCurrency")  .WithPluralName("TotalFeesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d0730f9e-3217-45b3-a5f8-6ae3a5174050"), new Guid("e575035f-9953-499b-b657-2cde6dd53349"), new Guid("49aaea6a-b3b6-484f-8c49-f61e51a6b71a")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalShippingAndHandling")  .WithPluralName("TotalShippingAndHandlings")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d5d2ec87-064b-4743-9a5e-55b68a84caf6"), new Guid("fec8b5fd-bf0f-4579-9af0-5a590b2b5b94"), new Guid("1fba3917-63b1-472e-96bf-08fc5068a7b6")).WithObjectTypes(Order, ShippingAndHandlingCharge).WithSingularName("ShippingAndHandlingCharge")  .WithPluralName("ShippingAndHandlingCharges")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e039e94d-db89-4a17-a692-e82fdb53bfea"), new Guid("f1eed6f2-fb70-4fd8-8e7a-0962759b00a7"), new Guid("e5e2710b-f662-4a50-8203-d0d7c0789e3e")).WithObjectTypes(Order, allorsDateTime).WithSingularName("OrderDate")  .WithPluralName("OrderDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("e39720f8-2e56-4231-9ce0-fb8b8e0efd7e"), new Guid("b58cd94b-59c1-46ca-8c8e-93b3a45ff5dc"), new Guid("62ca6d9d-6d19-4197-847b-7909faf28295")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalExVatCustomerCurrency")  .WithPluralName("TotalExVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("f38b3c7d-ac20-49be-a115-d7e83557f49a"), new Guid("f4ff4e74-0bff-4a2a-b4bd-3a08310c6ce2"), new Guid("6d52e55f-2adb-4ec6-8b13-e8611dfcd38a")).WithObjectTypes(Order, allorsDateTime).WithSingularName("DeliveryDate")  .WithPluralName("DeliveryDate")      .Build();
            new RelationTypeBuilder(domain, new Guid("f636599a-9c61-4952-abcf-963e6f6bdcd8"), new Guid("94feb243-bb62-4bf4-9947-76d54df2f13c"), new Guid("5955d3b6-b5cd-4878-a71f-070ce9a343cf")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalBasePrice")  .WithPluralName("TotalBasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("faa16c88-2ca0-4eea-847e-793ab84d7dea"), new Guid("457ee3dc-c239-4053-a769-f7b50a10879c"), new Guid("e2b42530-507e-4463-8c41-d8ff3886c5cf")).WithObjectTypes(Order, allorsDecimal).WithSingularName("TotalFee")  .WithPluralName("TotalFees")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("fc6cb229-6c94-4c80-a4a6-697d1d752997"), new Guid("2481df7c-624c-44ce-a201-7d7c4d339780"), new Guid("f2540864-d417-482b-a7bf-0f01c1f185eb")).WithObjectTypes(Order, SurchargeAdjustment).WithSingularName("SurchargeAdjustment")  .WithPluralName("SurchargeAdjustments")    .WithIsIndexed(true)  .Build();
			
            // PartyPackageRevenueHistory
            new RelationTypeBuilder(domain, new Guid("06fdef2c-73f1-4365-9c74-4a994c3d8f97"), new Guid("901f16e6-2007-4da0-abb5-b8d36adceb16"), new Guid("2eca8cef-2790-42a7-8d28-8fb4024c9230")).WithObjectTypes(PartyPackageRevenueHistory, Package).WithSingularName("Package")  .WithPluralName("Packages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c503c916-7e4b-4a2f-98ca-e46141d6b63a"), new Guid("b9a80dee-d5bb-4a3f-9540-3b9b27cb47c6"), new Guid("e03d5835-0741-4710-a8d7-b1f317ed09a8")).WithObjectTypes(PartyPackageRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c8ed0a7f-62c9-44e4-a7c5-57a06f9aaedd"), new Guid("e9398597-8748-41dd-89b9-7f0cf7366d7a"), new Guid("188c541c-7d81-4dbf-8f89-17ce44744f8b")).WithObjectTypes(PartyPackageRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e3aeeaa7-3407-45cd-98da-6f36de3587b2"), new Guid("0c896c5b-06a6-44ec-8b0a-b038cc560988"), new Guid("bc19a2d0-2efd-4e01-97bd-ea97fa020c3b")).WithObjectTypes(PartyPackageRevenueHistory, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f1f0c7f4-fba8-4388-b5ca-a59a251152f9"), new Guid("7a7bbe2b-26af-4f05-8e5f-1de7cffa858b"), new Guid("e8797e06-4e7b-453b-9f1d-4f43c779c437")).WithObjectTypes(PartyPackageRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
			
            // OrderKind
            new RelationTypeBuilder(domain, new Guid("cb4c5cfa-5c2c-4cdf-898b-9afcd28229c4"), new Guid("6b9b043f-c629-439d-be92-e825177d8c29"), new Guid("8f85bba3-1eaa-4352-bfd4-68f29ce4f71c")).WithObjectTypes(OrderKind, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e35c295c-a4a8-4441-af9a-bd2d3e96bab3"), new Guid("c2158f51-489a-4618-b289-dff18a05afb5"), new Guid("c07c051a-e204-46dd-bac2-1ce957c8c6d9")).WithObjectTypes(OrderKind, allorsBoolean).WithSingularName("ScheduleManually")  .WithPluralName("ScheduleManuallies")      .Build();
			
            // PickListItem
            new RelationTypeBuilder(domain, new Guid("6e89daf6-f07f-4a7d-8032-cc3c08d443c2"), new Guid("73671086-9f60-489a-a77b-69ab508862cc"), new Guid("50ef0257-edfd-44c4-ab76-230432d1be7d")).WithObjectTypes(PickListItem, allorsDecimal).WithSingularName("RequestedQuantity")  .WithPluralName("RequestedQuantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8a8ad2c2-e301-40be-8c7e-3c8291c3bbe9"), new Guid("1b28c7b7-f770-4e49-acbf-ade8e67ba939"), new Guid("eb4da206-6f39-4615-84e7-afb12f1cf486")).WithObjectTypes(PickListItem, InventoryItem).WithSingularName("InventoryItem")  .WithPluralName("InventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f32d100b-a6e8-4cb2-98b4-c06264789c76"), new Guid("3b75e3a8-7580-4f07-bf75-ae7541a00609"), new Guid("a1017517-e337-4c43-891e-c716ad615b07")).WithObjectTypes(PickListItem, allorsDecimal).WithSingularName("ActualQuantity")  .WithPluralName("ActualQuantities")      .WithPrecision(19).WithScale(2).Build();
			
            // SalesOrderItem
            new RelationTypeBuilder(domain, new Guid("0229942b-e102-4e97-af8d-97ee8383203e"), new Guid("e1e640d4-4096-42df-9c12-6bf54e5314db"), new Guid("16e9993a-6604-41e9-9ed0-053480d45d46")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("InitialProfitMargin")  .WithPluralName("InitialProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("14d596e8-adec-46bc-b260-af37d24a1035"), new Guid("4f846efe-5546-4709-8f31-2470a3e3650e"), new Guid("ad606374-6b77-4ddc-b907-e190faf7da0e")).WithObjectTypes(SalesOrderItem, SalesOrderItemStatus).WithSingularName("CurrentPaymentStatus")  .WithPluralName("CurrentPaymentStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1e1ed439-ae25-4446-83e6-295d8627a7b5"), new Guid("67bc37d9-0d6f-4227-81c9-8f03a1e0da47"), new Guid("d8ab230a-92d2-44cb-8e45-502285dd9a5e")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityShortFalled")  .WithPluralName("QuantitiesShortFalled")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1ea02a2c-280a-4a48-9ffb-1517789c56f1"), new Guid("851f33e4-6c43-468d-ab0d-0f5f83bdb179"), new Guid("213d2b36-dbfd-4e2d-a854-82ba271f0d94")).WithObjectTypes(SalesOrderItem, OrderItem).WithSingularName("OrderedWithFeature")  .WithPluralName("OrderedWithFeatures")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1edd9008-537a-43ba-b4a1-56d3c3211c36"), new Guid("db9987b4-b71c-4ece-b4c1-53bb27a02dff"), new Guid("ae3bd8e0-1f58-45e1-a6dc-191d7668e358")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("MaintainedProfitMargin")  .WithPluralName("MaintainedProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2bd8163c-b2cd-49bc-922a-b8c859c24031"), new Guid("3f66929d-a2f1-4e9b-a701-4364e3a25e1d"), new Guid("1fbf819e-b7fe-4ce3-86af-efea369db2fa")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("RequiredProfitMargin")  .WithPluralName("RequiredProfitMargins")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("3072d12f-e8de-43ba-a63c-f557744a1d5d"), new Guid("9b642b30-ec44-4877-a191-974704f6d8df"), new Guid("1eb0ad30-68f6-4d63-9e07-96f7e90005ee")).WithObjectTypes(SalesOrderItem, SalesOrderItemStatus).WithSingularName("OrderItemStatus")  .WithPluralName("OrderItemStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3dbd9a9b-8cda-4cf3-890d-2e6af4e47018"), new Guid("fba228cb-4b3f-4f50-8b6a-f16572ba3977"), new Guid("9d69d9af-7c23-4a1d-a4f2-09ef58881ce8")).WithObjectTypes(SalesOrderItem, SalesOrderItemStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3e798309-d5d5-4860-87ec-ba3766e96c9e"), new Guid("4626b586-07e1-468c-877a-d1a8f1b196c5"), new Guid("b2aef5ac-45f7-41aa-8e1b-f2d79d3d9fad")).WithObjectTypes(SalesOrderItem, NonSerializedInventoryItem).WithSingularName("PreviousReservedFromInventoryItem")  .WithPluralName("PreviousReservedFromInventoryItems")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("48e40504-bb22-4b11-949d-569b3a556416"), new Guid("979f6fa2-29f4-43c0-86d7-761509719112"), new Guid("6b6dab9b-1583-4f3d-8d97-1a8a53af9e75")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("RequiredMarkupPercentage")  .WithPluralName("RequiredMarkupPercentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("545eb094-63d8-4d25-a069-7c3e91f26eb7"), new Guid("686d5956-c2dc-46d5-b812-52020d392f0f"), new Guid("3a8adaf6-82e6-45a6-bd5f-61860125d77b")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityShipped")  .WithPluralName("QuantitiesShipped")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5bf138bd-27c1-4291-91da-b543170bf160"), new Guid("c4fab99d-b408-437a-aea3-05cf32afa5d4"), new Guid("76f3c438-e027-492d-bae4-932d81f455df")).WithObjectTypes(SalesOrderItem, SalesOrderItemStatus).WithSingularName("CurrentOrderItemStatus")  .WithPluralName("CurrentOrderItemStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5cc50f26-361b-46d7-a8e6-a9f53f7d2722"), new Guid("0d8906e9-3bfd-4d9b-8b24-8526fdfb2e33"), new Guid("000b641f-00be-4b9c-84aa-a8c968024ece")).WithObjectTypes(SalesOrderItem, PostalAddress).WithSingularName("ShipToAddress")  .WithPluralName("ShipToAddresses")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("64edd3e6-0b78-4b34-8a11-aa9c0a1b1f35"), new Guid("667a1304-05ae-410a-94a1-5e87a40dc53b"), new Guid("d15456ab-66c1-4ecc-bfc5-910b7d9c4869")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityPicked")  .WithPluralName("QuantitiesPicked")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6826e05e-eb9a-4dc4-a653-0230dec934a9"), new Guid("aa2b8b0a-672c-423b-9ca8-2fd40f8d1306"), new Guid("793f4946-ed12-49ca-9764-8df534941cca")).WithObjectTypes(SalesOrderItem, Product).WithSingularName("PreviousProduct")  .WithPluralName("PreviousProducts")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("710e0b05-01d1-4592-b652-f0fada3dfa45"), new Guid("9ab86597-c31b-46d7-b546-89ebfd1411cd"), new Guid("9533bbb6-359f-49a3-959b-98fcdd5cc2a7")).WithObjectTypes(SalesOrderItem, SalesOrderItemObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("75a13fdc-90b2-4550-9b2f-fc0a9387d569"), new Guid("94922e2d-1570-4667-af8f-5d4415fd6d78"), new Guid("39d62b69-520c-456d-b3f1-6ca640ffc4cb")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("UnitPurchasePrice")  .WithPluralName("UnitPurchasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7a8255f5-4283-4803-9f96-60a9adc2743b"), new Guid("2c9b2182-7b93-46c9-86ac-d13add6d52b5"), new Guid("7596f471-e54c-4491-8af6-02f0e8d7d015")).WithObjectTypes(SalesOrderItem, Party).WithSingularName("ShipToParty")  .WithPluralName("ShipToParties")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7ae1b939-b387-4e6e-9da2-bc0364e04f7b"), new Guid("808f88ba-3866-4785-812c-c062c5f268a4"), new Guid("64639736-a7d0-47cb-8afb-fa751a19670d")).WithObjectTypes(SalesOrderItem, PostalAddress).WithSingularName("AssignedShipToAddress")  .WithPluralName("AssignedShipToAddresses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8145fbd3-a30f-44a0-9520-6b72ac20a82d"), new Guid("59383e9d-690e-46aa-9cc0-1dd39db14f60"), new Guid("31087f2f-10e8-4558-9e0a-a5dbceb3204a")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityReturned")  .WithPluralName("QuantitiesReturned")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("85d11891-5ffe-488f-9f23-5b2c7bc1c480"), new Guid("283cdb9a-e7e3-4486-92da-5e94653505a8"), new Guid("fd06dd18-c1d4-40c7-b62e-273a8522f580")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityReserved")  .WithPluralName("QuantitiesReserved")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("911abda0-2eb0-477e-80be-e9e7d358205e"), new Guid("23af5657-ed05-43c2-aeed-d268204528d2"), new Guid("42a88fb9-84bc-4e35-83ff-6cb5c0cf3c96")).WithObjectTypes(SalesOrderItem, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ae30b852-d1d9-4966-a65a-6f16120652f6"), new Guid("c3a3e068-8683-44b7-a255-47e49a63c453"), new Guid("7a4a9d1b-2cff-4f9c-8b7c-94f08fb68c46")).WithObjectTypes(SalesOrderItem, SalesOrderItemStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b2d2645e-0d3f-473e-b277-6f890b9b911e"), new Guid("68281397-74f8-4356-b9fc-014f792ab914"), new Guid("1292e876-1c61-42cb-8f01-8b3eb6cf0fa0")).WithObjectTypes(SalesOrderItem, Party).WithSingularName("AssignedShipToParty")  .WithPluralName("AssignedShipToParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b2f7dabb-8b87-41bc-8903-166d77bba1c5"), new Guid("ad7dfb12-d00d-4a93-a011-7cb09c1e9aa9"), new Guid("ba9a9c6c-4df0-4488-b5fa-6181e45c6f18")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityPendingShipment")  .WithPluralName("QuantitiesPendingShipment")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b8d116ca-dbab-4119-84ca-c0e196d9c018"), new Guid("3f2cc31e-84e9-4e49-bfbe-0a436e2236be"), new Guid("7cee282f-ff61-42fc-9a2e-54164e8b6390")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("MaintainedMarkupPercentage")  .WithPluralName("MaintainedMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c43d151f-3b6e-498e-b588-0a7cd6d746dd"), new Guid("916832a5-16d8-4705-9571-9631f6c1c0c0"), new Guid("094aafd5-7e41-41f4-b49f-7b8589414452")).WithObjectTypes(SalesOrderItem, SalesOrderItemObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d5639e07-37b8-46db-9e35-fa98301d31dd"), new Guid("43ee44b6-2e51-4ade-8bb7-9b10a780ba2e"), new Guid("38e21291-b24a-4331-b781-f7950df3f501")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("InitialMarkupPercentage")  .WithPluralName("InitialMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d7c25b48-d82f-4250-b09d-1e935eab665b"), new Guid("67e9a9d9-74ff-4b04-9aa1-dd08c5348a3e"), new Guid("4bfc1720-a2f6-4204-974b-42ca42c0d2e1")).WithObjectTypes(SalesOrderItem, NonSerializedInventoryItem).WithSingularName("ReservedFromInventoryItem")  .WithPluralName("ReservedFromInventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e8980105-2c4d-41de-bd67-802a8c0720f1"), new Guid("8b747457-bf7a-4274-b245-d04607b2a5ba"), new Guid("90d69cb4-d485-418f-9608-44063f116ff4")).WithObjectTypes(SalesOrderItem, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ed586b2f-c687-4d97-9416-52f7156b7b11"), new Guid("cb5c31c4-2daa-405b-8dc9-5ea6c87f66b3"), new Guid("c5b07ead-1a71-407e-91f8-4ec39853888a")).WithObjectTypes(SalesOrderItem, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f148e660-1e09-4e76-97fb-de62a7ee7482"), new Guid("0105885d-f722-44bd-9f57-6231c38191b5"), new Guid("9132a260-1b35-4b5a-b14c-8dceb6383581")).WithObjectTypes(SalesOrderItem, allorsDecimal).WithSingularName("QuantityRequestsShipping")  .WithPluralName("QuantitiesRequestsShipping")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("f2ccd5d6-95e3-4d72-938b-9f430f36ae59"), new Guid("77472c36-b500-4788-b1b3-22741adec0c0"), new Guid("748df737-edab-476d-a2f2-0f362828c0e7")).WithObjectTypes(SalesOrderItem, SalesOrderItemStatus).WithSingularName("PaymentStatus")  .WithPluralName("PaymentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // WorkEffortGoodStandard
            new RelationTypeBuilder(domain, new Guid("086907b1-97c2-47c1-ade4-f7749f615ae1"), new Guid("f3cf9c9b-2d69-4ef7-8240-44d1ca53bc6f"), new Guid("cd4b1f0a-425f-43d3-bc00-d64a0c4e84df")).WithObjectTypes(WorkEffortGoodStandard, Good).WithSingularName("Good")  .WithPluralName("Goods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("28b3b976-3354-4095-b928-7c1474e8c492"), new Guid("3b07f539-a06c-4cdc-8790-98c05e097aa6"), new Guid("211ae475-665a-4677-a9eb-376ed9c4d886")).WithObjectTypes(WorkEffortGoodStandard, allorsDecimal).WithSingularName("EstimatedCost")  .WithPluralName("EstimatedCosts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c94d5e97-ec2b-4d32-ae8d-145595f0ad91"), new Guid("3ddc2478-34ba-45fa-aa21-a11c856fbfe0"), new Guid("33be021f-3194-4e54-b69e-844814ca0bbd")).WithObjectTypes(WorkEffortGoodStandard, allorsInteger).WithSingularName("EstimatedQuantity")  .WithPluralName("EstimatedQuantities")      .Build();
			
            // Passport
            new RelationTypeBuilder(domain, new Guid("85036007-8e01-4d90-9cfe-7b9c25e43537"), new Guid("b1235d10-b895-40dc-bc99-680d08b4cef2"), new Guid("9a1e96ae-0a56-4812-ac52-1c142afd61c2")).WithObjectTypes(Passport, allorsDateTime).WithSingularName("IssueDate")  .WithPluralName("IssueDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("dd30acd3-2e7b-49e6-9fcd-04cfdafb62d0"), new Guid("cb5a7d75-b938-4451-9896-b661b1828fab"), new Guid("bc010471-bb69-4735-8a86-25d1a8528d34")).WithObjectTypes(Passport, allorsDateTime).WithSingularName("ExpiriationDate")  .WithPluralName("ExpiriationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("eb3cdf1a-d577-46ff-9d0e-d709c6e7d9d9"), new Guid("8d5d9376-24e0-486f-84fb-f242bfaee585"), new Guid("b5ea6bb3-6498-46c6-b579-3839f5effbe1")).WithObjectTypes(Passport, allorsString).WithSingularName("Number")  .WithPluralName("Numbers")      .WithSize(256).Build();
			
            // AmountDue
            new RelationTypeBuilder(domain, new Guid("0274d4d3-3f07-408c-89e3-f5367acd5fab"), new Guid("9c7d4eeb-36fc-47f9-8a88-ea3e9cc0ce77"), new Guid("d961ac61-9dc2-4ce0-87aa-24a69cc8fbc4")).WithObjectTypes(AmountDue, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("3856c988-32d3-455d-89d8-aa1eaa80dcce"), new Guid("896a030b-a038-4b05-8af5-982f7050c0ea"), new Guid("636344f0-e795-448a-8f37-53d1ebd87e9b")).WithObjectTypes(AmountDue, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("39d2f4f2-0c16-40f5-990b-38bad15fae99"), new Guid("cada8a73-b732-4789-aa7b-a4caeaea20e2"), new Guid("341bd110-6126-476c-9d35-1069c207dc1b")).WithObjectTypes(AmountDue, allorsDateTime).WithSingularName("TransactionDate")  .WithPluralName("TransactionDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("3ca978b2-8c0a-4fec-8b98-88e9ea3b2966"), new Guid("90befe3a-0821-4ec9-bac1-f580ebdaab9e"), new Guid("6b0f0eed-a757-4668-97bb-9d82ed4ff983")).WithObjectTypes(AmountDue, allorsDateTime).WithSingularName("BlockedForDunning")  .WithPluralName("BlockedForDunnings")      .Build();
            new RelationTypeBuilder(domain, new Guid("43193cac-15ad-4a1a-8945-f4ecb7d93291"), new Guid("96a1c683-052a-446e-8d82-c1943caaf53f"), new Guid("fc8ed608-7c2a-4eaf-9240-0d233184b5b3")).WithObjectTypes(AmountDue, allorsDecimal).WithSingularName("AmountVat")  .WithPluralName("AmountsVat")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5cb888fd-bcff-4eef-8ad6-efab3434364d"), new Guid("3c54dbfa-3dbd-419c-b341-4a127bd1387b"), new Guid("e9635cd0-13f0-43fc-ad12-5f65b40c6923")).WithObjectTypes(AmountDue, BankAccount).WithSingularName("BankAccount")  .WithPluralName("BankAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("90b4eaea-21cd-4a04-a64b-3c3dce0718d9"), new Guid("87f05475-29aa-4cb7-a5d9-865a47995cd6"), new Guid("71271fdc-4a24-4650-a57a-9cbc2973cc04")).WithObjectTypes(AmountDue, allorsDateTime).WithSingularName("ReconciliationDate")  .WithPluralName("ReconciliationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("953877d2-055c-4274-afa5-91fd425b5449"), new Guid("9e20dc87-aa15-4854-b0d4-1ecefb22621e"), new Guid("e5a11de2-fa90-4fe0-87f3-5f4ea0629f8d")).WithObjectTypes(AmountDue, allorsString).WithSingularName("InvoiceNumber")  .WithPluralName("InvoiceNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("98ec45be-fea4-4df7-91fb-6643edf74784"), new Guid("102ff721-0f77-4e05-9252-974b0e2a1505"), new Guid("e422bd7e-2998-4022-88cf-8de0a84621c4")).WithObjectTypes(AmountDue, allorsInteger).WithSingularName("DunningStep")  .WithPluralName("DunningSteps")      .Build();
            new RelationTypeBuilder(domain, new Guid("a40ae239-df13-47e1-8fa2-cfcb4946b966"), new Guid("3fca92c6-8996-4f5b-867a-7ad209f8e44e"), new Guid("8d211ac4-115f-47e4-be98-784ca9f7409a")).WithObjectTypes(AmountDue, allorsInteger).WithSingularName("SubAccountNumber")  .WithPluralName("SubAccountNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("a6693763-e246-4ae8-bd37-74313d32883b"), new Guid("e74e0621-56ec-4bf9-920d-919b8cebd2f9"), new Guid("9761e619-7ce8-466c-b154-743590b1bc46")).WithObjectTypes(AmountDue, allorsString).WithSingularName("TransactionNumber")  .WithPluralName("TransactionNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("acedb9ed-b0de-464f-86ec-621022938ad7"), new Guid("7a8823ee-ce46-4f99-8b15-217476244273"), new Guid("64f6065c-b8b1-4b91-9128-b1a17c109cc5")).WithObjectTypes(AmountDue, DebitCreditConstant).WithSingularName("Side")  .WithPluralName("Sides")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b0570264-3211-4444-a69f-1cdb2eb6e783"), new Guid("8586c543-e997-48e1-8302-a5ef7e1ad6ab"), new Guid("c7da91ea-2071-45b8-a4c7-957d7bd9579b")).WithObjectTypes(AmountDue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d227669f-8052-4962-8ccf-a775355691f1"), new Guid("a0b14293-269d-46d3-b297-a149a5090b1d"), new Guid("a6620318-c985-4e60-bdea-204993e65217")).WithObjectTypes(AmountDue, allorsBoolean).WithSingularName("BlockedForPayment")  .WithPluralName("BlockedForPayments")      .Build();
            new RelationTypeBuilder(domain, new Guid("def3c00e-f065-48e5-97a2-22497f1800b3"), new Guid("278bb3d9-da67-44ca-a78b-8c047da3b2d4"), new Guid("3b867d6f-f7e7-4223-99cf-052ac43e139b")).WithObjectTypes(AmountDue, allorsDateTime).WithSingularName("DateLastReminder")  .WithPluralName("DatesLastReminder")      .Build();
            new RelationTypeBuilder(domain, new Guid("e9b2fc3f-c6ed-4e67-a634-9ee78f824ad8"), new Guid("7d7e867b-e22e-4ca9-8751-e8af845fc206"), new Guid("8fd5d417-fc56-4b5c-9a9c-a156dff25006")).WithObjectTypes(AmountDue, allorsString).WithSingularName("YourReference")  .WithPluralName("YourReferences")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("edad9c25-ef5e-4326-aba6-535deb6a8a7e"), new Guid("deeaff94-26ec-4dbf-815b-c3105024459e"), new Guid("8fb086f5-2dba-46fb-b86e-2b81e17fa996")).WithObjectTypes(AmountDue, allorsString).WithSingularName("OurReference")  .WithPluralName("OurReferences")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f027183b-d8a1-4909-bedc-5d16a62d6bc2"), new Guid("bc7040ea-f230-42e5-ab35-458a0d3fc52e"), new Guid("3760cfff-b831-4767-9346-9d37ce76172b")).WithObjectTypes(AmountDue, allorsString).WithSingularName("ReconciliationNumber")  .WithPluralName("ReconciliationNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f18c665b-4f88-4e97-950c-08a38d9f0d93"), new Guid("8707951a-3285-44a9-b3d8-7c51cc9977ec"), new Guid("9caf74b7-48c5-4d1b-81fb-15bc22518156")).WithObjectTypes(AmountDue, allorsDateTime).WithSingularName("DueDate")  .WithPluralName("DueDates")      .Build();
			
            // OrderTerm
            new RelationTypeBuilder(domain, new Guid("04cd1dd4-6f4f-4cd5-8ca0-5d3ccae06400"), new Guid("13b304b8-a945-4302-bd45-6a51f03aa8c9"), new Guid("059b0064-a361-48d5-8340-f1ae43db454b")).WithObjectTypes(OrderTerm, allorsString).WithSingularName("TermValue")  .WithPluralName("TermValues")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("0540ccac-4970-4026-9529-be62db0350a0"), new Guid("d5bc8696-24d9-408f-ba50-c20a2c43dec1"), new Guid("76541960-6f11-4cd3-bc78-3018480cf742")).WithObjectTypes(OrderTerm, TermType).WithSingularName("TermType")  .WithPluralName("TermTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // CreditCardCompany
            new RelationTypeBuilder(domain, new Guid("05860987-77be-4d8d-823d-99dd0e2cc822"), new Guid("002eff4d-2bcc-40bb-b311-7ae86207bdc7"), new Guid("c9fe6f93-933e-4859-aaa2-ef3f5e2c8b44")).WithObjectTypes(CreditCardCompany, allorsString).WithSingularName("Name")  .WithPluralName("Name")      .WithSize(256).Build();
			
            // PurchaseShipmentStatus
            new RelationTypeBuilder(domain, new Guid("01d6a244-e174-4a91-8f27-5af54401bed1"), new Guid("09311427-0d20-4c65-85eb-371d1bcfb23f"), new Guid("125cbf28-2721-4e1b-8cb5-ce3f5a6a464e")).WithObjectTypes(PurchaseShipmentStatus, PurchaseShipmentObjectState).WithSingularName("PurchaseShipmentObjectState")  .WithPluralName("PurchaseShipmentObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a243d65e-81ac-49e7-af1b-1b97faa7360e"), new Guid("9d74270a-7197-43ee-92c9-8f06bd1b48db"), new Guid("fac16474-a909-4566-b55e-5849927aa431")).WithObjectTypes(PurchaseShipmentStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // Cash
            new RelationTypeBuilder(domain, new Guid("39c8beda-d284-442b-886a-6d6b2fb51cc8"), new Guid("f90e529a-8303-4a66-9622-144cfaed3bf3"), new Guid("90ee494e-2194-4972-bdde-7a3a30aff736")).WithObjectTypes(Cash, Person).WithSingularName("PersonResponsible")  .WithPluralName("PersonResponsible")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PerformanceReview
            new RelationTypeBuilder(domain, new Guid("22ec2f64-1099-49aa-908b-abb2703ccf33"), new Guid("2b66e451-52c1-4e83-97e0-a59784862660"), new Guid("d5a94f8a-e657-406a-a9ff-64fec9e5b67c")).WithObjectTypes(PerformanceReview, Person).WithSingularName("Manager")  .WithPluralName("Managers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3704d6ac-52c1-4af0-ad6e-151defc2fa05"), new Guid("840545c0-3f1e-44e0-96cd-48a0dc34e937"), new Guid("dbd0ecc2-ba54-45d5-a4c8-7c3476e64ce1")).WithObjectTypes(PerformanceReview, PayHistory).WithSingularName("PayHistory")  .WithPluralName("PayHistories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a16503ae-6371-4e97-9d34-f21a0f52002f"), new Guid("7002201c-53f7-457f-8c8c-4990fc4ed175"), new Guid("220d3993-fbca-4082-887c-ab7e9261d4da")).WithObjectTypes(PerformanceReview, PayCheck).WithSingularName("BonusPayCheck")  .WithPluralName("BonusPayChecks")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a5057413-950e-4825-8036-7f398c4b5d39"), new Guid("86796848-4a49-43c1-879e-1e77063af4e0"), new Guid("7bbb3e7e-c3a0-4b63-84e7-4bb923425ec1")).WithObjectTypes(PerformanceReview, PerformanceReviewItem).WithSingularName("PerformanceReviewItem")  .WithPluralName("PerformanceReviewItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ddeb9c39-9bfc-437d-8f5a-434028d8ad6f"), new Guid("1e857746-32cb-44af-9e05-3fb7568def9a"), new Guid("77390fa9-3f73-41f8-8adc-558c7839400e")).WithObjectTypes(PerformanceReview, Person).WithSingularName("Employee")  .WithPluralName("Employees")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f3210e4a-a8ee-442c-85a5-34290deffe2a"), new Guid("b91a9331-cc16-401f-9ee7-d697389431f7"), new Guid("9aeadbaf-24ad-4ced-96a0-1f4ee2ea0859")).WithObjectTypes(PerformanceReview, Position).WithSingularName("ResultingPosition")  .WithPluralName("ResultingPositions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // InvestmentAccount
            new RelationTypeBuilder(domain, new Guid("9eefdec1-48db-4f91-9eac-928b6a42d4e4"), new Guid("2759ed05-afa4-49ea-91d1-20b8d2ff527c"), new Guid("1d337bb7-2b33-4c8a-aeb3-d37c3ea72690")).WithObjectTypes(InvestmentAccount, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // AgreementItem
            new RelationTypeBuilder(domain, new Guid("3ad6eaac-8cc3-4738-9a5b-617386e296c8"), new Guid("caf115e0-d7b8-43af-a183-f4df4c573e2c"), new Guid("34bb4854-7f4f-4b27-8dd7-a2b9cbcf2331")).WithObjectTypes(AgreementItem, allorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("49d6363c-6006-4850-8a96-d87b9336ae59"), new Guid("aab2420d-b105-4328-a71c-c2cdce2712a3"), new Guid("16e3ac4d-1641-417f-bb72-1167ae809ef9")).WithObjectTypes(AgreementItem, Addendum).WithSingularName("Addendum")  .WithPluralName("Addenda")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4bd76d2c-383e-460f-a597-4283addcbef8"), new Guid("a8ea8f52-bbe2-4723-85c6-8277904c3d93"), new Guid("894d0335-40d3-4476-a87f-c3f11021862e")).WithObjectTypes(AgreementItem, AgreementItem).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9431dbfa-c620-445a-914f-8f12d4734b8b"), new Guid("b31b913d-82cc-418d-8167-df26ce483473"), new Guid("c883859c-f67b-482e-b6f1-0a81fae1d927")).WithObjectTypes(AgreementItem, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("cfa9c54b-4e9f-4bd2-897d-baf8fb32fa9c"), new Guid("08518361-7f3d-4a44-9377-f407d6946668"), new Guid("d4becca2-e702-42f8-beb7-f652e086ce83")).WithObjectTypes(AgreementItem, AgreementTerm).WithSingularName("AgreementTerm")  .WithPluralName("AgreementTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // PackageRevenue
            new RelationTypeBuilder(domain, new Guid("1b941728-0511-45e3-8460-f49c6868ebaa"), new Guid("1d3aa935-9e18-4cb0-a7ad-e24447743bf3"), new Guid("8d3b1810-f982-4ebd-b3ea-ea7647410c5f")).WithObjectTypes(PackageRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2763fe2b-9d15-49a7-92c4-89994699cf05"), new Guid("fabbf131-374d-4f5d-801d-a449378b3ba8"), new Guid("a8a3d6c2-7f15-42c7-a523-b3737b843e34")).WithObjectTypes(PackageRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("7674a37a-173d-4958-b73e-258773ef4277"), new Guid("39ed2c02-672e-42ba-ab9e-0b9b7b73be39"), new Guid("f89627f5-2e66-4aed-9de1-3447dc281b15")).WithObjectTypes(PackageRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("b9acfd44-5228-4fcf-88c6-625110cb394e"), new Guid("0f7f8a0b-20b5-4fb8-abb4-cc727cab1337"), new Guid("e6aa2dbf-0d8a-41d2-94ef-2268c847e1b7")).WithObjectTypes(PackageRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c61a3dce-880f-403c-ad1a-bffabe59a57e"), new Guid("c9336998-8145-4a7d-95de-9d278487d205"), new Guid("24266040-04dd-4388-a9cf-130ecac89094")).WithObjectTypes(PackageRevenue, allorsString).WithSingularName("PackageName")  .WithPluralName("PackageNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e6df8560-e85f-4e7a-a527-f7fb8f94cbb6"), new Guid("07b77c6b-09d0-4e4f-8cb4-69cbebc14e54"), new Guid("17b95dec-dc59-4abc-817e-26eb0e9d5ac0")).WithObjectTypes(PackageRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e952d6d6-d388-4e80-ba6b-8a612973aca6"), new Guid("a1b71548-06f0-40cd-a619-8b9c080dfe99"), new Guid("c61b90e7-ea4d-4c7f-bfdb-ef87ba911c9d")).WithObjectTypes(PackageRevenue, Package).WithSingularName("Package")  .WithPluralName("Packages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Benefit
            new RelationTypeBuilder(domain, new Guid("0c3efe28-a934-467d-a361-293175330b62"), new Guid("d9d8872d-3b77-48e6-9902-74560a60c3ef"), new Guid("98a07703-261f-4a9a-8c1b-02af1a4a4e0b")).WithObjectTypes(Benefit, allorsDecimal).WithSingularName("EmployerPaidPercentage")  .WithPluralName("EmployerPaidPercentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6239a2cc-97ce-49cb-b5aa-23e1e9ff7e71"), new Guid("759c76cd-01ba-4be6-a1d1-3f9f305e69b5"), new Guid("97ebeb12-9ae4-4364-8f85-c4cfd565180b")).WithObjectTypes(Benefit, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6e1e0ef1-0e2a-406f-afa4-a6c97657801f"), new Guid("de7199dd-6a61-41d3-b3dc-847a1a1eb596"), new Guid("f9a3fee7-4b05-4bef-af33-064cac668021")).WithObjectTypes(Benefit, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("89460288-d09e-43f9-960a-86b6c1e2e0be"), new Guid("97cf596f-bed3-4309-9b88-50be9b82f7a1"), new Guid("4554ecdf-95a8-4d3a-9415-fe397d14831e")).WithObjectTypes(Benefit, allorsDecimal).WithSingularName("AvailableTime")  .WithPluralName("AvailableTimes")      .WithPrecision(19).WithScale(2).Build();
			
            // VatReturnBox
            new RelationTypeBuilder(domain, new Guid("3bcc4fc9-5646-4ceb-b48b-bb1d7fbcba64"), new Guid("69f949c3-f5c1-4cb4-a907-ce3673496628"), new Guid("ec126f8a-4d48-4c1e-bdb6-ad66ab529580")).WithObjectTypes(VatReturnBox, allorsString).WithSingularName("HeaderNumber")  .WithPluralName("HeaderNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("78e114b4-ec1d-49ce-ab32-40a3184dea31"), new Guid("98920876-b4f8-4d41-90f1-115164441836"), new Guid("9a8717dd-0713-458b-8d84-9758f4ddfb03")).WithObjectTypes(VatReturnBox, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // ShipmentRouteSegment
            new RelationTypeBuilder(domain, new Guid("02ef1727-e135-4af3-9d76-02bad7b122f3"), new Guid("c4fd1dd3-ddef-4f2c-bc22-388d3f979798"), new Guid("1c1e24d0-5635-4d17-bd8b-5e0513b7f024")).WithObjectTypes(ShipmentRouteSegment, allorsDecimal).WithSingularName("EndKilometers")  .WithPluralName("EndKilometers")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2a697cc1-cdeb-4e40-a929-2a8df593877e"), new Guid("f2e40a37-c722-4608-9ed5-0b6f49819efc"), new Guid("54e17ef2-abae-4c76-9d93-87a6545cfa87")).WithObjectTypes(ShipmentRouteSegment, Facility).WithSingularName("FromFacility")  .WithPluralName("FromFacilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3f46506d-ea90-4103-b986-965194037cef"), new Guid("b0468fca-5eb7-4251-b935-2f18891e9a8f"), new Guid("e8aea1c9-ca9b-4b77-b2c2-8f3e4f2d900b")).WithObjectTypes(ShipmentRouteSegment, allorsDecimal).WithSingularName("StartKilometers")  .WithPluralName("StartKilometers")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4a30a93c-d50b-44cf-b0a2-c29c970e6290"), new Guid("9754042f-3f58-42dd-b160-9c4339a6169d"), new Guid("6be3b17d-03f1-4731-b17e-5956260d1d9a")).WithObjectTypes(ShipmentRouteSegment, ShipmentMethod).WithSingularName("ShipmentMethod")  .WithPluralName("ShipmentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("57f25750-a517-47a8-a6a0-feb160cd5f3e"), new Guid("0eb0c608-4d72-4aa2-b9c1-46d508a3ff32"), new Guid("44572277-5c54-4d92-8916-1ad2afd13da2")).WithObjectTypes(ShipmentRouteSegment, allorsDateTime).WithSingularName("EstimatedStartDateTime")  .WithPluralName("EstimatedStartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("591427f6-b61c-4c19-9f82-e97570d9bead"), new Guid("352996f3-ffa9-4453-a602-938c7543a7c1"), new Guid("bd604c1a-3540-472a-90f5-69ed94a82f03")).WithObjectTypes(ShipmentRouteSegment, Facility).WithSingularName("ToFacility")  .WithPluralName("ToFacilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6b3d4c25-823c-4197-8c05-80aeb887eb8b"), new Guid("f77e2ce0-97fb-4ccd-a6a3-dac8c09d5295"), new Guid("85fe4467-bfde-41ac-9a40-e482a2f800a0")).WithObjectTypes(ShipmentRouteSegment, allorsDateTime).WithSingularName("EstimatedArrivalDateTime")  .WithPluralName("EstimatedArrivalDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("6bf54f85-7781-4fd3-a87f-6e7103042ecb"), new Guid("09234af6-ece2-403f-81ce-8c5a8e814135"), new Guid("e9d8d7f8-5408-4bb8-85d2-6ce02a400796")).WithObjectTypes(ShipmentRouteSegment, Vehicle).WithSingularName("Vehicle")  .WithPluralName("Vehicles")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("928b9d1e-903b-4d56-aa72-b7aeaf3ba340"), new Guid("ace8a50d-e396-4e47-b13c-f02fa018f652"), new Guid("2b85c556-8726-41d4-a236-197816b2824b")).WithObjectTypes(ShipmentRouteSegment, allorsDateTime).WithSingularName("ActualArrivalDateTime")  .WithPluralName("ActualArrivalDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("b080fe6b-382e-475d-be81-8632ddedb183"), new Guid("39dade5b-be0e-43ed-8368-00f24cfd3ce6"), new Guid("e749db0f-c95e-4c81-87d0-f5932a31816c")).WithObjectTypes(ShipmentRouteSegment, allorsDateTime).WithSingularName("ActualStartDateTime")  .WithPluralName("ActualStartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("c04769b1-f8dc-40c7-87d2-1e55a4702e71"), new Guid("7d5a3fa4-50bb-45b6-b355-2bad4485b9d1"), new Guid("c8ad9159-10eb-4c38-8e5b-1339fa082406")).WithObjectTypes(ShipmentRouteSegment, Organisation).WithSingularName("Carrier")  .WithPluralName("Carriers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // WorkEffortStatus
            new RelationTypeBuilder(domain, new Guid("5dd27f4b-032d-4b45-86ad-ba288c26fa7c"), new Guid("2743e797-731b-404f-866a-5b9249309f60"), new Guid("99022ef3-d4f2-4635-b27a-c02b554ad8ae")).WithObjectTypes(WorkEffortStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("f9e60388-f0da-45d9-94c2-5fe2d5ff581a"), new Guid("9bb24455-11ed-4dba-820c-fa6b03aae9a6"), new Guid("6d2efe75-9b3f-449b-95f7-cbc552a2ca3c")).WithObjectTypes(WorkEffortStatus, WorkEffortObjectState).WithSingularName("WorkEffortObjectState")  .WithPluralName("WorkEffortObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PurchaseOrderStatus
            new RelationTypeBuilder(domain, new Guid("1f6f2902-0b17-4537-970d-a72454b91410"), new Guid("b3e0bc80-0b37-4946-a6c1-e84cb522e949"), new Guid("95d7759c-aa52-4b38-8337-0c59287441aa")).WithObjectTypes(PurchaseOrderStatus, PurchaseOrderObjectState).WithSingularName("PurchaseOrderObjectState")  .WithPluralName("PurchaseOrderObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fe949e5f-b717-4cda-8f40-5b0db57d43dd"), new Guid("890b82dc-5a8b-463b-bcc4-cccee90b8dfb"), new Guid("e505854a-67de-4cdf-8404-a0495178ed74")).WithObjectTypes(PurchaseOrderStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // PayrollPreference
            new RelationTypeBuilder(domain, new Guid("2cb969f7-6415-4d5b-be55-7e691c2254e1"), new Guid("c2040e80-7608-4b9d-8e1e-c244b7155a81"), new Guid("6f5e623e-b365-402c-aea0-09a386bb0377")).WithObjectTypes(PayrollPreference, allorsDecimal).WithSingularName("Percentage")  .WithPluralName("Percentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("802de3ea-0cb9-4815-bc56-497e75f487ae"), new Guid("75568752-3a42-412f-bf76-be6705bd441c"), new Guid("fc4e22ba-ad4d-4f6e-b65f-d0aef6ff47ee")).WithObjectTypes(PayrollPreference, allorsString).WithSingularName("AccountNumber")  .WithPluralName("AccountNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a37e2763-6d8c-46c3-a69f-148458d2981b"), new Guid("4255cc8c-c97c-48a2-9111-f8658f478042"), new Guid("7f79e26c-e5ef-45d4-88e9-8dcce8ffc2ba")).WithObjectTypes(PayrollPreference, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b576883f-0cfd-4973-aa49-479b6e712c75"), new Guid("f93aac27-8f9d-4b9e-a55d-5fad0efc6e86"), new Guid("162fccce-f98a-4b2b-b840-72368a87b043")).WithObjectTypes(PayrollPreference, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c71eb13a-8053-4d56-a3e3-dcd38a1e4f29"), new Guid("8955caa1-cfdb-4463-a6d2-80ce0f775470"), new Guid("4851823e-72f4-4531-b505-bae6d70688e8")).WithObjectTypes(PayrollPreference, DeductionType).WithSingularName("DeductionType")  .WithPluralName("DeductionTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ded05ab7-351b-4b05-9e0a-010e6b4fbd0f"), new Guid("feb46721-492d-4508-9d28-5b6496f517cd"), new Guid("ddfd9dac-42d5-42d0-909a-ebf6c8869c73")).WithObjectTypes(PayrollPreference, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // CustomerShipment
            new RelationTypeBuilder(domain, new Guid("15e8f37c-3963-490c-8f22-7fb1e40209df"), new Guid("30b4e232-dd11-4ee6-b1dd-ef1e05b54d92"), new Guid("a282ae7a-2280-4ea8-a8c8-cf170f0714ac")).WithObjectTypes(CustomerShipment, CustomerShipmentStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatus")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("30b48576-1a91-4b75-8503-c83313db6d98"), new Guid("a8733a1e-232d-4773-a18f-351b35979dd4"), new Guid("8e4f6c02-d388-4a69-acb9-c6b9eed9fcaa")).WithObjectTypes(CustomerShipment, CustomerShipmentObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4f7c79be-9f0d-4646-9488-dc86761866cd"), new Guid("06ff523b-b43d-424e-b54a-c184c5d3ac5f"), new Guid("526cb9db-f5d7-42bc-a37d-c1ae680d1f92")).WithObjectTypes(CustomerShipment, allorsBoolean).WithSingularName("ReleasedManually")  .WithPluralName("ReleasedsManually")      .Build();
            new RelationTypeBuilder(domain, new Guid("7b1b6b60-9678-4a52-aee8-33bad04eeb40"), new Guid("8cf76b47-a09f-4112-8bec-733a30abc323"), new Guid("6c812e1e-204b-4e85-8cfb-5dae89fb2bf2")).WithObjectTypes(CustomerShipment, CustomerShipmentObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7b6a8a4f-574f-494f-b43b-7c5b7428d685"), new Guid("83787439-402b-4d57-8e70-aa157aa8d1fa"), new Guid("0022a581-9823-4b8d-a3f5-ce068ab60fe8")).WithObjectTypes(CustomerShipment, CustomerShipmentStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("897bcb4f-fa89-4d9b-8666-49bb061a69ae"), new Guid("d2945852-755a-45ef-b6dc-914767d3d2e5"), new Guid("a3ab7835-d97e-4221-831d-0ba1ffe3c9d0")).WithObjectTypes(CustomerShipment, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a754a290-571f-4c25-bd1c-d96a9765eec6"), new Guid("6d117db4-ef4d-483a-a68d-75c69e325bba"), new Guid("66a18574-7b90-4e36-9d5d-a4f31bc6eba1")).WithObjectTypes(CustomerShipment, allorsBoolean).WithSingularName("WithoutCharges")  .WithPluralName("WithoutChargess")      .Build();
            new RelationTypeBuilder(domain, new Guid("b94fa6e5-cfdf-4545-8eb3-43d03aceffc5"), new Guid("2d9a286e-95d5-4adb-ab29-7a9d95f83146"), new Guid("33382f4f-5ebc-4589-b906-a8a2a3be28d2")).WithObjectTypes(CustomerShipment, allorsBoolean).WithSingularName("HeldManually")  .WithPluralName("HeldsManually")      .Build();
            new RelationTypeBuilder(domain, new Guid("f0fe5bc1-74d1-4fee-8039-b6952edecc92"), new Guid("c11d0979-373c-4c27-94d2-4d7350afc1c4"), new Guid("2348278f-bf03-4133-b34c-2da5955a0a41")).WithObjectTypes(CustomerShipment, allorsDecimal).WithSingularName("ShipmentValue")  .WithPluralName("ShipmentValues")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
			
            // InternalOrganisationRevenue
            new RelationTypeBuilder(domain, new Guid("0f1c3ee2-de89-4828-982c-8168c9d8cf7c"), new Guid("ffbaa100-a74b-46c1-bc93-d84f48918d88"), new Guid("2f8002ef-0f3b-46af-a930-cd426a2ee1a8")).WithObjectTypes(InternalOrganisationRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("284b35b3-583b-4843-8f65-0abafc493eb7"), new Guid("d5e828bc-e39d-44d7-9e82-ab9471fd5d75"), new Guid("d6d12ab5-4272-4095-b9ad-7222ec3071c1")).WithObjectTypes(InternalOrganisationRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5a982cc9-01c5-41f1-83ba-97747299205b"), new Guid("c0f705d6-4ad1-4e19-ae3c-3b12b4f2a6ec"), new Guid("7a66c118-e965-430d-abbb-7a9c19a401e1")).WithObjectTypes(InternalOrganisationRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8e250efc-f571-4567-a747-cefe30118ddc"), new Guid("48e4e136-0006-4c83-8616-246fb432346e"), new Guid("7e1086df-5289-48a5-8ee3-d8f14b39d4c7")).WithObjectTypes(InternalOrganisationRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e618c592-229d-4135-b26b-d57a3d1802ac"), new Guid("10f7837f-9025-4284-8821-04fe9291c726"), new Guid("c0de4d35-c58e-4477-866a-d018a7ea7c7c")).WithObjectTypes(InternalOrganisationRevenue, allorsString).WithSingularName("PartyName")  .WithPluralName("PartyNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f1e4b78b-5166-46fc-8a9f-b009da84a3df"), new Guid("6e0495e8-cfac-49cb-89d4-7aae57b01aaa"), new Guid("105af49d-ba84-457a-8a73-d6fcab787d38")).WithObjectTypes(InternalOrganisationRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Package
            new RelationTypeBuilder(domain, new Guid("88b49c23-0c4c-4a2d-94aa-c6c8a12ac267"), new Guid("d1a984e7-2f57-43a0-8cca-e8682407498b"), new Guid("cffa7e90-1c5b-459c-adbe-8fa008b36151")).WithObjectTypes(Package, allorsString).WithSingularName("Name")  .WithPluralName("Name")      .WithSize(256).Build();
			
            // GeoLocatable
            new RelationTypeBuilder(domain, new Guid("b0aba482-63eb-4482-a232-3863f089f4d9"), new Guid("340069b9-a00b-420d-8f8d-52e627729db3"), new Guid("bab847eb-ff35-49dd-ae44-ccf4e1ee6743")).WithObjectTypes(GeoLocatable, allorsDecimal).WithSingularName("Latitude")  .WithPluralName("Latitudes")  .WithIsDerived(true)    .WithPrecision(8).WithScale(6).Build();
            new RelationTypeBuilder(domain, new Guid("c51b6be6-5678-4664-b2c9-874cc46deb2e"), new Guid("0d7f48c7-84e5-4ea6-8242-22e4cb35e8cd"), new Guid("66d37e99-b7aa-42c7-8b03-0d4bee43a1e7")).WithObjectTypes(GeoLocatable, allorsDecimal).WithSingularName("Longitude")  .WithPluralName("Longitudes")  .WithIsDerived(true)    .WithPrecision(9).WithScale(6).Build();
			
            // EmailCommunication
            new RelationTypeBuilder(domain, new Guid("25b8aa5e-e7c5-4689-b1ed-d9a0ba47b8eb"), new Guid("11649936-a5fa-488e-8d17-e80619c4d634"), new Guid("6219fd3b-4f38-4f8f-8a5a-783f908ef55a")).WithObjectTypes(EmailCommunication, EmailAddress).WithSingularName("Originator")  .WithPluralName("Originators")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4026fcf7-3fc2-494b-9c4a-3e19eed74134"), new Guid("f2febf7f-7917-4499-8546-cae1e53d6791"), new Guid("50439b5a-2251-469c-8512-f9dc65b0d9f6")).WithObjectTypes(EmailCommunication, EmailAddress).WithSingularName("Addressee")  .WithPluralName("Addressees")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4f696f91-e185-4d3d-bf40-40e6c2b02eb4"), new Guid("a19fe8f6-a3b9-4d59-b2e6-cfc19cc01a58"), new Guid("661f4ae9-684b-4b56-9ec6-7bf9fbfea4ab")).WithObjectTypes(EmailCommunication, EmailAddress).WithSingularName("CarbonCopy")  .WithPluralName("CarbonCopies")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dd7506bb-4daa-4da7-8f20-3f607c944959"), new Guid("42fb79f1-c891-41bf-be4b-a2717bd94e69"), new Guid("6d75e51a-7994-43bb-9e99-cd0a88d9d8f2")).WithObjectTypes(EmailCommunication, EmailAddress).WithSingularName("BlindCopy")  .WithPluralName("BlindCopies")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e12818ad-4ffd-4d91-8142-4ac9bfcbc146"), new Guid("a44a8d84-2510-45fd-add1-646f84be072d"), new Guid("ae354426-6273-4b09-aabf-3f6d25f86e56")).WithObjectTypes(EmailCommunication, EmailTemplate).WithSingularName("EmailTemplate")  .WithPluralName("EmailTemplates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // CreditCard
            new RelationTypeBuilder(domain, new Guid("07d663c5-4716-4e76-a280-ec635216791f"), new Guid("e8db5958-e57e-4860-adc9-831c4e513c41"), new Guid("73942abf-a46a-4be4-868b-7c5d195504aa")).WithObjectTypes(CreditCard, allorsString).WithSingularName("NameOnCard")  .WithPluralName("NamesOnCard")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("0916d4d2-5f82-46da-967e-7b48012e4019"), new Guid("21cc3945-4cc1-43c7-a0a3-0fc9af562c5a"), new Guid("bfba8caa-0f75-4e18-8d97-9427e3b5df97")).WithObjectTypes(CreditCard, CreditCardCompany).WithSingularName("CreditCardCompany")  .WithPluralName("CreditCardCompanies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4dfa0fda-0001-4635-b8d1-4fd4ce723ed2"), new Guid("d7ac25b9-d7ec-4f88-82c2-680422891bd7"), new Guid("0ad6e1c1-845d-446a-8557-342c95eee357")).WithObjectTypes(CreditCard, allorsInteger).WithSingularName("ExpirationYear")  .WithPluralName("ExpirationYears")      .Build();
            new RelationTypeBuilder(domain, new Guid("7fa0d04e-b2df-49f8-8aa2-2d546ca843d6"), new Guid("adee3f7d-ded7-469b-9f43-6ed23f3893de"), new Guid("d8e31f7d-a381-438d-9d16-0fff5ab60139")).WithObjectTypes(CreditCard, allorsInteger).WithSingularName("ExpirationMonth")  .WithPluralName("ExpirationMonths")      .Build();
            new RelationTypeBuilder(domain, new Guid("b5484c11-52d4-45f7-b25a-bf4c05e2c9a0"), new Guid("15df289b-6c03-4fc4-8d8b-31edc394de8d"), new Guid("683f29d5-cc38-4165-8fd9-f97483130bac")).WithObjectTypes(CreditCard, allorsString).WithSingularName("CardNumber")  .WithPluralName("CardNumbers")      .WithSize(256).Build();
			
            // OrganisationContactRelationship
            new RelationTypeBuilder(domain, new Guid("0ca367d2-0ce2-440d-a4e7-cbf089c1efed"), new Guid("738d9d62-4823-4045-9e2f-082b91127f3f"), new Guid("fa8ca2da-c75f-4ba9-9c22-6953008c3ba2")).WithObjectTypes(OrganisationContactRelationship, Person).WithSingularName("Contact")  .WithPluralName("Contacts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("96f4c9af-eeff-477f-8a93-1168cc383b4c"), new Guid("a34e218b-26c0-4c88-a202-0353e693833a"), new Guid("3af5a227-4470-4a4e-a66c-245ac0d12be5")).WithObjectTypes(OrganisationContactRelationship, Organisation).WithSingularName("Organisation")  .WithPluralName("Organisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("af7e007e-c325-453a-923e-55299eda2a8c"), new Guid("337e305a-da68-42da-b508-d9f010138a09"), new Guid("2399e636-f267-4299-b2c7-747497487d63")).WithObjectTypes(OrganisationContactRelationship, OrganisationContactKind).WithSingularName("ContactKind")  .WithPluralName("ContactKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // OrganisationContactKind
            new RelationTypeBuilder(domain, new Guid("5d3446a3-ab54-4c49-89bb-928b082bb4b7"), new Guid("a1b7eec7-d13f-47da-b028-4db580da07a4"), new Guid("291d3e15-301a-4865-9097-5407dadd65ff")).WithObjectTypes(OrganisationContactKind, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // CustomerReturnStatus
            new RelationTypeBuilder(domain, new Guid("7f02b626-26e6-43c7-af6f-44db32a9748a"), new Guid("88941e26-55e1-400a-925c-8b40977e8141"), new Guid("6a362a6a-930f-4202-b9a7-07f6062c9dde")).WithObjectTypes(CustomerReturnStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("871dc477-7231-4180-b479-e66c0d2dbe58"), new Guid("b65cdd1a-423c-41b7-8978-a7cc4420166d"), new Guid("3153feb0-9213-496e-b1ac-2ed5d6b431a8")).WithObjectTypes(CustomerReturnStatus, ShipmentReceipt).WithSingularName("ShipmentReceipt")  .WithPluralName("ShipmentReceipts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("eb96d9f9-cbbb-4c2e-84b6-6e4b17cc162f"), new Guid("2cf7c3d8-3915-4c41-b619-b317d3de7842"), new Guid("f995313e-0317-4587-9f6e-456be2134f44")).WithObjectTypes(CustomerReturnStatus, CustomerReturnObjectState).WithSingularName("CustomerReturnObjectState")  .WithPluralName("CustomerReturnObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PerformanceReviewItem
            new RelationTypeBuilder(domain, new Guid("6d7bb4b2-885d-4f7b-9d31-d517c3d03ac2"), new Guid("4c8cd6fe-acea-43ae-90e9-41ae1b84f269"), new Guid("1a5977eb-1914-4b02-a0d3-feaad843465d")).WithObjectTypes(PerformanceReviewItem, RatingType).WithSingularName("RatingType")  .WithPluralName("RatingTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d62d7236-458f-4e30-8df4-27eb877d0931"), new Guid("7056f19c-c67e-4b54-a08c-c49155326a5e"), new Guid("cac7ce59-1969-43b8-99a9-a90af638558d")).WithObjectTypes(PerformanceReviewItem, PerformanceReviewItemType).WithSingularName("PerformanceReviewItemType")  .WithPluralName("PerformanceReviewItemTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // UtilizationCharge
            new RelationTypeBuilder(domain, new Guid("3a371680-fc37-44dc-b3be-cdd76b6dd1e4"), new Guid("15d9f938-5a5c-472c-92a6-6769a37f652c"), new Guid("a1e57ec7-561d-4c8e-8652-aea06598fb1b")).WithObjectTypes(UtilizationCharge, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4f933f12-1337-453c-9cfd-6babaf9189d5"), new Guid("b49286b4-db2a-4025-8fb2-9390514b69dc"), new Guid("037bba17-d291-40ea-920b-f09995ef04fb")).WithObjectTypes(UtilizationCharge, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartyPackageRevenue
            new RelationTypeBuilder(domain, new Guid("3fc82b94-ce74-42d7-91d8-e97a79117f4f"), new Guid("917ecd65-8097-4e6b-93ce-662b18ccf424"), new Guid("33bf1c47-0d26-43e4-841d-bb5d85da1bdf")).WithObjectTypes(PartyPackageRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("646382fa-3794-46be-81a0-28a1609e65b0"), new Guid("0bb2b710-90c3-42c9-8eb4-5bffb06cb705"), new Guid("0ec8beac-06c9-4f7f-a39b-fb9d7bfcae0f")).WithObjectTypes(PartyPackageRevenue, Package).WithSingularName("Package")  .WithPluralName("Packages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8896e95d-80e3-42dc-8ba7-ad3fdef665f9"), new Guid("5d061264-d9c8-471c-b5be-3251502e24e1"), new Guid("d1757181-9a09-4075-99ac-5c2a13ad85d3")).WithObjectTypes(PartyPackageRevenue, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("af3eeba0-867c-4484-b593-1815b38c8bf4"), new Guid("450fc3e5-f6ca-4f96-ab52-afb71421b6b5"), new Guid("80615a8d-b2ff-4671-b9bb-0667413cd74c")).WithObjectTypes(PartyPackageRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b9648327-4521-4daf-b68f-52cd78095998"), new Guid("e65e0fb0-26a8-4640-ae6a-c8402889dc8e"), new Guid("b3788737-41da-4480-8223-bc398e021561")).WithObjectTypes(PartyPackageRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("e8384ea1-cb9d-43a0-8409-68dc86ca8def"), new Guid("f50f2ba1-9d0b-4eee-87d2-626fd89422c7"), new Guid("bf38c186-30a1-40e5-90eb-a326865a2d19")).WithObjectTypes(PartyPackageRevenue, allorsString).WithSingularName("PartyPackageName")  .WithPluralName("PartyPackageNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e93042c4-1e6c-47c5-9004-f6ddcbfdbb33"), new Guid("39c71cdb-847a-474f-92b4-827e2eb95c22"), new Guid("d498a399-c183-433c-8260-396c4e2b997d")).WithObjectTypes(PartyPackageRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ed2b5e8c-2c74-4ae2-b467-d41baf9f41db"), new Guid("1b51addf-6a5d-4611-b23c-1a16fa413259"), new Guid("2734ebff-1038-4945-98c5-d1da0a11265b")).WithObjectTypes(PartyPackageRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ServiceTerritory
            new RelationTypeBuilder(domain, new Guid("a268313d-db1e-44e1-9fb1-7135d1157083"), new Guid("348c497e-7907-4409-b7b1-d77ebfd46258"), new Guid("a23c1a3d-2a76-46b3-a26c-d5c5a66ebe0a")).WithObjectTypes(ServiceTerritory, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // BudgetRevision
            new RelationTypeBuilder(domain, new Guid("5124634a-dc8b-477a-8ae2-d4ad577a13bb"), new Guid("fa00944b-f6a3-4c61-9739-6a8a109d32d5"), new Guid("a1230395-837b-4021-8075-642fdf1d7d2c")).WithObjectTypes(BudgetRevision, allorsDateTime).WithSingularName("RevisionDate")  .WithPluralName("RevisionDates")      .Build();
			
            // WorkEffortFixedAssetStandard
            new RelationTypeBuilder(domain, new Guid("5aca8d2b-0073-4890-b02a-f4c9a5fc8a2b"), new Guid("b20e8fbd-4493-4a13-ade6-a42ecc8e6793"), new Guid("1ef2d6ea-7662-4a8b-83e3-712f8b7bda9a")).WithObjectTypes(WorkEffortFixedAssetStandard, allorsDecimal).WithSingularName("EstimatedCost")  .WithPluralName("EstimatedCosts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("73900f38-242a-4aac-ba8e-d8ffa57a125f"), new Guid("87bc4caf-7953-4805-816a-e6e6af4cfc19"), new Guid("10eed02a-ae4b-483e-b912-170ec39bb92b")).WithObjectTypes(WorkEffortFixedAssetStandard, allorsDecimal).WithSingularName("EstimatedDuration")  .WithPluralName("EstimatedDurations")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("98ca7e1a-8f15-4533-9de7-819b6c868788"), new Guid("da3497fc-7c30-4760-bfec-2bbc8d5ebf5b"), new Guid("792772c6-0c04-417c-a22a-479f4c5cf35f")).WithObjectTypes(WorkEffortFixedAssetStandard, FixedAsset).WithSingularName("FixedAsset")  .WithPluralName("FixedAssets")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b9d782af-1f4c-4639-bd11-fda3651982df"), new Guid("c17dbf4f-504f-4b9e-ba5c-25545e1386d0"), new Guid("7a4ecdb9-2b88-4f41-9fe4-fe1016b12ad8")).WithObjectTypes(WorkEffortFixedAssetStandard, allorsInteger).WithSingularName("EstimatedQuantity")  .WithPluralName("EstimatedQuantities")      .Build();
			
            // Shipment
            new RelationTypeBuilder(domain, new Guid("05221b28-9c80-4d3b-933f-12a8a17bc261"), new Guid("c59ef057-da9a-433f-90d3-5ff657aa1e48"), new Guid("6fe551cd-0808-466b-9ec9-833098ebad79")).WithObjectTypes(Shipment, ShipmentMethod).WithSingularName("ShipmentMethod")  .WithPluralName("ShipmentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("05b0841b-d546-4fd6-9305-492b0ce20f8a"), new Guid("26be1e2b-ee3c-4c37-9ccc-07a916e6af29"), new Guid("313a2875-bafc-430a-b7c4-1aa45e825233")).WithObjectTypes(Shipment, ContactMechanism).WithSingularName("BillToContactMechanism")  .WithPluralName("BillToContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("165b529f-df1c-45b6-bbed-d19ffcb375f2"), new Guid("c71e40be-1f55-483d-9bfa-0d2dfb26c7d9"), new Guid("18a5331e-120e-45e6-8ef4-35a1f48237e0")).WithObjectTypes(Shipment, ShipmentPackage).WithSingularName("ShipmentPackage")  .WithPluralName("ShipmentPackages")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("17234c66-6b61-4ac9-a23b-4388e19f4888"), new Guid("bc2164ec-5d7e-4dff-8db6-4d1eeab970e6"), new Guid("f939af72-bcb4-44bc-b47d-758c27304a7d")).WithObjectTypes(Shipment, allorsString).WithSingularName("ShipmentNumber")  .WithPluralName("ShipmentNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("18808545-f941-4c5a-8809-0f1fb0cca2d8"), new Guid("44940303-b210-42bd-8791-906004294261"), new Guid("a65dbc06-f659-4e34-bf2d-af4b4717972e")).WithObjectTypes(Shipment, Document).WithSingularName("Document")  .WithPluralName("Documents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("50f36218-ae61-4d67-af4d-d05cc8b2266d"), new Guid("a8ff4824-3ccd-49a8-82e6-e7723ccb8348"), new Guid("b7ead377-a5d4-4eab-98d9-e9527177090a")).WithObjectTypes(Shipment, Party).WithSingularName("BillToParty")  .WithPluralName("BillToParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5891b368-89cd-4a0e-aaef-439f442909c8"), new Guid("5fef9e9f-bd3d-454a-8aa1-10b262a34a4b"), new Guid("dd5e8d80-0395-413d-addb-ca66f36c50e8")).WithObjectTypes(Shipment, Party).WithSingularName("ShipToParty")  .WithPluralName("ShipToParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6a568bea-6718-414a-b822-d8304502be7b"), new Guid("499bb422-b2f0-48cf-bf09-0544e768b5de"), new Guid("b8724e90-9888-4f81-b70d-1eceb93af3d3")).WithObjectTypes(Shipment, ShipmentItem).WithSingularName("ShipmentItem")  .WithPluralName("ShipmentItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6b90885f-9421-442a-b517-b85c6fe3c60d"), new Guid("b4df53d0-3970-45bf-bcfb-251dc18ebb46"), new Guid("215a7b54-93d9-455c-9979-759b116677cd")).WithObjectTypes(Shipment, InternalOrganisation).WithSingularName("BillFromInternalOrganisation")  .WithPluralName("BillFromInternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("78e7e7a5-2d8c-4184-b917-10095dc033b1"), new Guid("f924c450-6c83-4853-9449-b34efb52cc78"), new Guid("b9c80d27-7278-4883-b1f3-d01712463109")).WithObjectTypes(Shipment, ContactMechanism).WithSingularName("ReceiverContactMechanism")  .WithPluralName("ReceiverContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7e1325e0-a072-46da-adb5-b997dde9980a"), new Guid("f73c3f6d-cc9c-4bda-a4c6-ef4f406a491d"), new Guid("14f6385d-4e20-4ffe-89e7-f7a261eda78e")).WithObjectTypes(Shipment, PostalAddress).WithSingularName("ShipToAddress")  .WithPluralName("ShipToAddresses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("894ecdf3-1322-4513-bf94-63882c5c29bf"), new Guid("da1adb58-e2be-4018-97b0-fb2ef107a661"), new Guid("7e28940e-6039-4698-b1f5-b31769aa7bbb")).WithObjectTypes(Shipment, allorsDecimal).WithSingularName("EstimatedShipCost")  .WithPluralName("EstimatedShipCosts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("97788e21-ec31-4fb2-9ef7-0b7b5a7367a1"), new Guid("227f8e47-58af-44be-bcaf-0da60e2c13d4"), new Guid("338e2be0-6eb5-42ad-b51c-83dd9b7f0194")).WithObjectTypes(Shipment, allorsDateTime).WithSingularName("EstimatedShipDate")  .WithPluralName("EstimatedShipDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("a74391e5-bd03-4247-93b8-e7081d939823"), new Guid("41060c75-fb34-4391-96f3-d0d267344ba3"), new Guid("eb3f084c-9d59-4fff-9fc3-186d7b9a19b3")).WithObjectTypes(Shipment, allorsDateTime).WithSingularName("LatestCancelDate")  .WithPluralName("LatestCancelDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("b37c7c90-0287-4f12-b000-025e2505499c"), new Guid("13e8d5af-43ff-431b-85d8-5e7706dc2f75"), new Guid("81367cbd-4713-46bd-8f4d-0df30c3daf96")).WithObjectTypes(Shipment, Carrier).WithSingularName("Carrier")  .WithPluralName("Carriers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b5dabbcc-508a-4998-a21a-6b86d7193688"), new Guid("43d9bbc8-319c-4971-a651-11f246fafa97"), new Guid("ebf2b41f-a922-4689-83d4-51569a3d85d3")).WithObjectTypes(Shipment, ContactMechanism).WithSingularName("InquireAboutContactMechanism")  .WithPluralName("InquireAboutContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b69c6812-bdc4-4a06-a782-fa8ff4a71aca"), new Guid("988cafce-2323-4c0d-b1cd-026045764ba4"), new Guid("cd02effa-d176-4f6e-8407-ec12d23b9f2a")).WithObjectTypes(Shipment, allorsDateTime).WithSingularName("EstimatedReadyDate")  .WithPluralName("EstimatedReadyDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("c8b0eff8-4dff-449c-9d44-a7235cd24928"), new Guid("556c0ae6-045e-4f35-8f63-ffb41f57dc44"), new Guid("5c34f5ee-5f25-42dd-97a8-7aa3aeb9973e")).WithObjectTypes(Shipment, PostalAddress).WithSingularName("ShipFromAddress")  .WithPluralName("ShipFromAddresses")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ea57219b-217e-444d-9741-c1c4e7aee9f7"), new Guid("2d0935d0-cdb5-4c3e-9726-e27ea731c43b"), new Guid("d7184821-3b9c-4800-874f-32d7cd9b72e3")).WithObjectTypes(Shipment, ContactMechanism).WithSingularName("BillFromContactMechanism")  .WithPluralName("BillFromContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ee49c6ca-bb03-40d3-97f1-004cc5a31132"), new Guid("167b541c-d2dd-4d9b-9fe1-6cd8d1a5f727"), new Guid("39a0ed41-436e-44bd-afc7-5d848397433b")).WithObjectTypes(Shipment, allorsString).WithSingularName("HandlingInstruction")  .WithPluralName("HandlingInstructions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("f1059139-6664-43d5-801f-79a4cc4288a6"), new Guid("92807e93-ed03-4dbc-9296-c508c879705b"), new Guid("3f2699b9-9652-4af4-98d7-2ff803677692")).WithObjectTypes(Shipment, Store).WithSingularName("Store")  .WithPluralName("Stores")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f1e92d31-db63-419c-8ed7-49f5db66c63d"), new Guid("fffbc8b5-a541-402d-8df6-3134cc52b306"), new Guid("566b9c3a-3fec-455f-a40d-b23338d3508c")).WithObjectTypes(Shipment, Party).WithSingularName("ShipFromParty")  .WithPluralName("ShipFromParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f403ab39-cc81-4e09-8794-a45db9ef178f"), new Guid("78c8d202-0277-4c3a-9e24-74041cc56872"), new Guid("8086c3d5-1577-4c32-bf73-abe72aac725c")).WithObjectTypes(Shipment, ShipmentRouteSegment).WithSingularName("ShipmentRouteSegment")  .WithPluralName("ShipmentRouteSegments")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fdac3beb-edf8-4d1b-80d4-21b643ef43ce"), new Guid("63d8adfc-6afb-499f-bd27-2f1d3f78bee6"), new Guid("8f56ce24-500e-4db9-abce-c7a301c38fe6")).WithObjectTypes(Shipment, allorsDateTime).WithSingularName("EstimatedArrivalDate")  .WithPluralName("EstimatedArrivalDates")      .Build();
			
            // PostalCode
            new RelationTypeBuilder(domain, new Guid("20267bfe-b651-4ed7-bd22-f4300022e39c"), new Guid("48a9b292-452c-48be-9cb3-2b20f23a510e"), new Guid("12e48856-88e9-4e97-aa32-fd532d2f050d")).WithObjectTypes(PostalCode, allorsString).WithSingularName("Code")  .WithPluralName("Codes")      .WithSize(256).Build();
			
            // ProfessionalAssignment
            new RelationTypeBuilder(domain, new Guid("18af73aa-336f-4120-8508-a59a9acf17bc"), new Guid("31da78aa-5e06-48f8-90e4-018ef021a280"), new Guid("cf515b68-b198-4348-881c-fd9a0bcf22bf")).WithObjectTypes(ProfessionalAssignment, Person).WithSingularName("Professional")  .WithPluralName("Professionals")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a75d3ec2-c4f8-4de6-a10c-fe5e3897e663"), new Guid("70e8f936-27c8-42cb-9459-9a823aaa6318"), new Guid("bb592768-a6f0-47fb-bc74-a15fc5867b34")).WithObjectTypes(ProfessionalAssignment, EngagementItem).WithSingularName("EngagementItem")  .WithPluralName("EngagementItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Container
            new RelationTypeBuilder(domain, new Guid("a8279f40-4624-4aa9-9e61-fc01f880ca17"), new Guid("15f3df8c-c20e-4162-b5f8-1e031001f11f"), new Guid("33da1e6a-60bd-4c50-9fe6-30946254a5f7")).WithObjectTypes(Container, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e4ca9708-8c0c-451a-b63c-126a96b2ad72"), new Guid("780fde92-4842-4366-afe8-09ef9bde95f6"), new Guid("54113d74-1620-4eb6-8c16-50531af1be17")).WithObjectTypes(Container, allorsString).WithSingularName("ContainerDescription")  .WithPluralName("ContainerDescriptions")      .WithSize(256).Build();
			
            // Payment
            new RelationTypeBuilder(domain, new Guid("4c8b7a4f-f151-419e-8365-ce0da0b3a709"), new Guid("32007a7b-e849-41c3-96f5-61d253607f98"), new Guid("5d06b93d-b58e-48ba-b1b8-215b2e84bf4d")).WithObjectTypes(Payment, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5be2e66e-4714-4dc1-a0f2-a9f600815e41"), new Guid("321a8622-1b74-4b48-bfe2-7a9478879f06"), new Guid("e877dc1e-18ba-4286-888f-831e0433544d")).WithObjectTypes(Payment, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7afc9649-43c9-4a60-a298-27361ba59765"), new Guid("41547bdb-9d10-42fb-a75f-b0c8d9b8d09e"), new Guid("6038fc56-abb9-41e6-965c-d71648d9f5ce")).WithObjectTypes(Payment, allorsDateTime).WithSingularName("EffectiveDate")  .WithPluralName("EffectiveDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("a80a1ed7-473b-493b-a9ab-23a682c6ae44"), new Guid("3e95c4c2-6164-486a-a483-e0552a142e13"), new Guid("495d6adc-8fff-4754-99cf-b4f2a65e6b44")).WithObjectTypes(Payment, Party).WithSingularName("SendingParty")  .WithPluralName("SendingParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b0c79092-c5d0-426b-b06d-ccec574bb7d9"), new Guid("d85e6a8c-5fa9-455d-bc94-6d02b47e7cd8"), new Guid("967768b1-8ec5-4b58-8e8c-2513e5528bb2")).WithObjectTypes(Payment, PaymentApplication).WithSingularName("PaymentApplication")  .WithPluralName("PaymentApplications")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f49e4d28-12a9-4575-818b-b475bec0c9d1"), new Guid("9760d670-085b-4573-85c2-96356d362d4e"), new Guid("779b1e2c-4be5-4324-b141-192cca8a7b56")).WithObjectTypes(Payment, allorsString).WithSingularName("ReferenceNumber")  .WithPluralName("ReferenceNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("faafa75e-496c-4220-ae3f-ab7d1e317484"), new Guid("c5800c84-707e-443c-a8ae-bc4e5598bc08"), new Guid("46c1c87e-c4af-4383-814d-5452b2faae94")).WithObjectTypes(Payment, Party).WithSingularName("ReceivingParty")  .WithPluralName("ReceivingParties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PackageRevenueHistory
            new RelationTypeBuilder(domain, new Guid("10487ca8-f973-4be7-b2ae-91857fe0486c"), new Guid("33beebef-7ccd-4ce3-a64b-262380d58728"), new Guid("6e01962d-36d5-4b16-8dc7-4290e3f8095e")).WithObjectTypes(PackageRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3d753279-fce6-4d44-a712-08fc21a562f5"), new Guid("91ab2864-0e91-40bc-bac1-293b7f696f5f"), new Guid("498418d5-938c-4a6f-bf7e-ad683faf0bea")).WithObjectTypes(PackageRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("662d235e-1d36-48d1-a945-f0b60e579ca1"), new Guid("ffc5e375-a778-459e-978c-e542952cd0fa"), new Guid("771e3c35-cb1a-4fb7-8c0d-01f6bbb9f4e3")).WithObjectTypes(PackageRevenueHistory, Package).WithSingularName("Package")  .WithPluralName("Packages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7e7e4510-d1fb-4389-8a8c-d444f1c87daa"), new Guid("89879f07-2f9a-4c6c-bf3a-8d2ec8742d27"), new Guid("28f27ac4-af47-41a1-b264-1c58bb73a852")).WithObjectTypes(PackageRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // JournalEntryDetail
            new RelationTypeBuilder(domain, new Guid("9e273a44-b68f-4379-b2cd-f6ac1d524c4a"), new Guid("003c293d-650f-422b-a5a4-aa8caff4ce3d"), new Guid("a1639744-d44f-472d-821b-ec9eaf5a8530")).WithObjectTypes(JournalEntryDetail, OrganisationGlAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b51ddcf7-ae36-4fbc-b8b5-3b2befa4a720"), new Guid("9a4e561c-df1e-4d5f-8b31-384959d56e4f"), new Guid("e49734eb-3232-4fcb-b923-d873804545c9")).WithObjectTypes(JournalEntryDetail, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("bc59e72d-935c-46fd-a595-4de24369fc12"), new Guid("d157af85-88b6-4a13-b142-ba7d176bbb40"), new Guid("539cedbf-3d80-45b3-8d3c-2e154f541ce9")).WithObjectTypes(JournalEntryDetail, allorsBoolean).WithSingularName("Debit")  .WithPluralName("Debits")      .Build();
			
            // Case
            new RelationTypeBuilder(domain, new Guid("2286f83b-7992-4aa0-80fe-ad19e3c8c572"), new Guid("484381bd-6dbc-4a78-bc59-c21422b942b2"), new Guid("c9951d63-5b1a-4053-9756-16b46a336288")).WithObjectTypes(Case, CaseStatus).WithSingularName("CurrentCaseStatus")  .WithPluralName("CurrentCaseStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("51bfbe94-46a5-411f-ac10-8623bfc4472c"), new Guid("b8b5d65b-14c9-4ab0-89b9-4124d60cfeb7"), new Guid("b49c43fd-798c-4608-a055-af04d97aa72d")).WithObjectTypes(Case, CaseStatus).WithSingularName("CaseStatus")  .WithPluralName("CaseStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("65e310b5-1358-450c-aec2-985dcc724cdd"), new Guid("d815e7c2-fe40-470c-9ab9-007f7bc0465b"), new Guid("fee6ebfb-3ce6-473b-9142-ea70ade93709")).WithObjectTypes(Case, allorsDateTime).WithSingularName("StartDate")  .WithPluralName("StartDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("6f68ff93-e70c-4581-b0c7-94b2f75e6860"), new Guid("8fb16366-5def-43eb-a68b-2bac5169564b"), new Guid("546330b8-f772-45f0-b2f0-66cf4a9ebffc")).WithObjectTypes(Case, CaseObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("87f64957-53f9-446e-ac1f-323a00da027f"), new Guid("289d52aa-fb69-4e7d-ba49-e4521614e19b"), new Guid("dec26736-f037-48c1-a4b2-0247b9abf92b")).WithObjectTypes(Case, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("be264aef-f259-494c-bfbf-20d20afde14c"), new Guid("d76227b9-0a0d-4f88-a169-99f5e681d374"), new Guid("b05ebe9f-e108-4dfe-b1e2-afb6dc9c3f55")).WithObjectTypes(Case, CaseObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // PurchaseReturn
            new RelationTypeBuilder(domain, new Guid("01d0a8b8-0361-440f-8d96-967578262318"), new Guid("9a79ad26-180a-45fd-8b50-ca8c641e9f77"), new Guid("e44876b6-c198-493a-8efc-4a4d09bd2b00")).WithObjectTypes(PurchaseReturn, PurchaseReturnStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatus")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("91b10295-d8d6-4240-914c-9ee8a6c21b96"), new Guid("47441947-8d72-4730-ab25-077dc80b4ca1"), new Guid("ba9b3f52-0a0e-46ec-b3fb-d9330ebd5269")).WithObjectTypes(PurchaseReturn, PurchaseReturnObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a5f3cf87-1730-4841-9df4-2638b10f3222"), new Guid("b1cb7246-2417-4618-bb03-decb38a0fc9f"), new Guid("5ede1e3f-bed7-4603-adfe-f576e23a2e2f")).WithObjectTypes(PurchaseReturn, PurchaseReturnStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("efdd7208-9662-4207-b484-e6d8fdc708e1"), new Guid("5cf89807-6553-402e-b34c-b8dd6d0baa60"), new Guid("b02b25b8-d004-44c9-8dee-d4ddd285e853")).WithObjectTypes(PurchaseReturn, PurchaseReturnObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // WorkEffortPartStandard
            new RelationTypeBuilder(domain, new Guid("4d4913e2-649d-4589-86ee-93cfa6c426a7"), new Guid("9228803e-089c-4ee6-9a42-18503d12f663"), new Guid("abb46361-be39-4668-8bbb-26de268a654c")).WithObjectTypes(WorkEffortPartStandard, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("68d4af49-a55f-416c-8097-d93da90e1132"), new Guid("f7423733-f8ec-41f6-85a5-fd528d9291fc"), new Guid("0748dd9e-6ea8-4eea-87f8-c40605e06d0c")).WithObjectTypes(WorkEffortPartStandard, allorsDecimal).WithSingularName("EstimatedCost")  .WithPluralName("EstimatedCosts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ec3e9aee-c39b-46a1-9968-af914f9057f3"), new Guid("5e99179e-4abd-409b-b091-263037554a6a"), new Guid("c63106ff-fe33-40fb-acb6-e7fb9907eb18")).WithObjectTypes(WorkEffortPartStandard, allorsInteger).WithSingularName("EstimatedQuantity")  .WithPluralName("EstimatedQuantities")      .Build();
			
            // SurchargeComponent
            new RelationTypeBuilder(domain, new Guid("0e9d10cd-6905-42ca-9db3-aed9b123eb2a"), new Guid("79b7473b-d65d-469b-9061-bb344da42c7e"), new Guid("f5d669b0-89d3-4605-ae6e-dcee6c673c50")).WithObjectTypes(SurchargeComponent, allorsDecimal).WithSingularName("Percentage")  .WithPluralName("Percentages")      .WithPrecision(19).WithScale(2).Build();
			
            // Bank
            new RelationTypeBuilder(domain, new Guid("28723704-3a61-445a-b14e-c757ebbf8d66"), new Guid("86b555ec-72f1-4ed6-b161-56d23508cf99"), new Guid("17dc868b-9a11-41c1-9366-b10f47d1fe3f")).WithObjectTypes(Bank, Media).WithSingularName("Logo")  .WithPluralName("Logos")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("354e114f-5d6b-4883-8e58-5c7a39878b6d"), new Guid("5c30c485-8f98-4a6d-8e05-3774331d9e7a"), new Guid("ed87ce26-a306-4590-8901-7b4fca4e2f57")).WithObjectTypes(Bank, allorsString).WithSingularName("Bic")  .WithPluralName("Bics")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a7851af8-38cd-4785-b81c-fb2fa403d9f6"), new Guid("f7460d4e-1094-46af-b04a-46115c2fee6a"), new Guid("92429a4b-9166-4e40-a356-caedaf296e23")).WithObjectTypes(Bank, allorsString).WithSingularName("SwiftCode")  .WithPluralName("SwiftCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d3a11d21-0232-48a0-b784-c111ad05f5da"), new Guid("0c8f4f92-50c5-4440-ae4e-e1734d7fdc60"), new Guid("627538dd-fac1-44a0-83c0-220b65440365")).WithObjectTypes(Bank, Country).WithSingularName("Country")  .WithPluralName("Countries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d4191223-d9be-4cbb-b2ad-ee0844dcae87"), new Guid("dc80650c-b20f-468f-8d3f-5410a7632961"), new Guid("85b6a787-7c26-42a6-aef9-d8b685ff97f6")).WithObjectTypes(Bank, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // ProductRevenue
            new RelationTypeBuilder(domain, new Guid("1ae30294-1cb3-4d85-a587-4d347ea6eac3"), new Guid("dbb2dab9-e5a7-49a8-8479-a9190f536b72"), new Guid("5cae7798-695c-4618-b5b0-088bf2333b56")).WithObjectTypes(ProductRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2e046fac-7803-49ba-a477-ee2cc889e3f1"), new Guid("da9d4c62-184a-4cc1-bd5c-4708a6daf71b"), new Guid("799d36ff-3411-401a-bdd5-60ff0b7f210a")).WithObjectTypes(ProductRevenue, allorsString).WithSingularName("ProductName")  .WithPluralName("ProductNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5f87a322-6943-4b20-b667-11bdf2f29244"), new Guid("e4cc94a8-3282-4a52-9e7e-1291105c3269"), new Guid("4e6ed630-8d5d-4b1c-8934-4332d15426ad")).WithObjectTypes(ProductRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8ee78120-6ec6-4470-835a-fdff01d3625e"), new Guid("7e4511b3-bd4e-4e4b-9f6c-c4254c47d258"), new Guid("ca9e8bf5-56c0-4795-a79d-9189fcc7ebcc")).WithObjectTypes(ProductRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c2570065-9bd8-403e-be43-884d31d4ccce"), new Guid("9d6ab03e-c52b-431b-aaee-f8116cf3a64c"), new Guid("d84acfc6-0df5-4d50-89bf-b36e2a0e5e9c")).WithObjectTypes(ProductRevenue, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ce487eb1-8a85-4999-a322-def96f9134d8"), new Guid("ebb2943e-4a43-4271-8348-486f74761826"), new Guid("d6135f1f-464a-4e00-914c-7185b281d86d")).WithObjectTypes(ProductRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("e33476e4-a420-4fa5-a367-72ff75c4d5c4"), new Guid("e13c2b60-30b8-48d4-b0d7-443ac8e493e5"), new Guid("15aa589a-9169-4644-8256-b8a644a6b5cf")).WithObjectTypes(ProductRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // DisbursementAccountingTransaction
            new RelationTypeBuilder(domain, new Guid("62c15bc4-42fd-45b8-ae5d-5cdf92c0b414"), new Guid("920ffcd4-6085-4d22-8d81-caf2dde21e70"), new Guid("33b6e056-e1e2-4173-97db-485593bf9e36")).WithObjectTypes(DisbursementAccountingTransaction, Disbursement).WithSingularName("Disbursement")  .WithPluralName("Disbursements")    .WithIsIndexed(true)  .Build();
			
            // OrderValue
            new RelationTypeBuilder(domain, new Guid("077a33bc-a822-4a23-918c-7fcaacdc61d1"), new Guid("f38c5851-7187-4f53-8eaf-85edee7e733d"), new Guid("7ee1e68b-5bb5-4e72-b63d-83132346a503")).WithObjectTypes(OrderValue, allorsDecimal).WithSingularName("ThroughAmount")  .WithPluralName("ThroughAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b25816e8-4b0c-4857-907f-7a391df2c55e"), new Guid("017aab24-a93c-4654-bc89-96e075d13c08"), new Guid("eedd52f8-0713-428e-b10a-a7da99b967aa")).WithObjectTypes(OrderValue, allorsDecimal).WithSingularName("FromAmount")  .WithPluralName("FromAmounts")      .WithPrecision(19).WithScale(2).Build();
			
            // VatRate
            new RelationTypeBuilder(domain, new Guid("0d6bd6c4-7220-45b4-891c-719f4bd141ce"), new Guid("f04be7c9-5f36-4cc2-8ad0-cad7386114da"), new Guid("2f66e429-ac12-4d6c-9f06-a5986b0667fc")).WithObjectTypes(VatRate, VatCalculationMethod).WithSingularName("VatCalculationMethod")  .WithPluralName("VatCalculationMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("36b9d86d-4e2e-4ff5-b167-8ea6c81dd6cc"), new Guid("8e37c73a-5508-432f-94c9-77d0159b0cc2"), new Guid("955c2b54-1aab-4eb0-b71d-3b6e4664e3b3")).WithObjectTypes(VatRate, VatReturnBox).WithSingularName("VatReturnBox")  .WithPluralName("VatReturnBoxes")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3f1ca41a-8443-4d81-a112-48fa1e28728b"), new Guid("f95d3157-469c-454d-8e5b-57e52ac2c89c"), new Guid("abf7d332-7a32-4b1f-91dd-3bd3802b8efa")).WithObjectTypes(VatRate, allorsDecimal).WithSingularName("Rate")  .WithPluralName("Rate")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("46cf5d68-cceb-4b49-933c-875e9614eb8b"), new Guid("deaf1d2c-6590-460e-9e16-0eaf68af6b3d"), new Guid("b4fdf839-5b01-432c-b165-9664a199d0bf")).WithObjectTypes(VatRate, OrganisationGlAccount).WithSingularName("VatPayableAccount")  .WithPluralName("VatPayableAccounts")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5418fdea-366c-4e0b-b2e0-d49cfb12cbe5"), new Guid("b63a4251-c297-46cb-a2a3-b0d619abe398"), new Guid("52fc90c1-2de7-4076-8cbf-44174ebd25a2")).WithObjectTypes(VatRate, Organisation).WithSingularName("TaxAuthority")  .WithPluralName("TaxAuthorities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5551f4ce-858f-4f29-9e92-3c2c893bb44b"), new Guid("bf0f6d49-1753-42f4-99e6-649e64bb0629"), new Guid("be2f8700-bd7d-4dd1-91ff-637ddc6a07a6")).WithObjectTypes(VatRate, VatRateUsage).WithSingularName("VatRateUsage")  .WithPluralName("VatRateUsages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("821df580-26d4-415c-b2ea-3e96a08c2f62"), new Guid("5678eef7-6892-4c47-900d-85b5c5c08940"), new Guid("bb7c23b8-0a01-4afa-91a6-7816bbaa803c")).WithObjectTypes(VatRate, VatRatePurchaseKind).WithSingularName("VatRatePurchaseKind")  .WithPluralName("VatRatePurchaseKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8b37058f-49bd-4cc6-8c26-9a9e7c6700ad"), new Guid("71231b78-14e7-41c3-8691-745f4dd9c919"), new Guid("00945490-ff10-440b-be3b-de563caf892f")).WithObjectTypes(VatRate, VatTariff).WithSingularName("VatTariff")  .WithPluralName("VatTariffs")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("958c1fda-0126-4b0a-8967-5d9df3ba50dc"), new Guid("edcb9612-a4d4-4ddc-971c-48b7dfa6b03c"), new Guid("9e77b9ad-9031-4766-a2f3-33c875395a79")).WithObjectTypes(VatRate, TimeFrequency).WithSingularName("PaymentFrequency")  .WithPluralName("PaymentFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b2aa3989-8e65-4fdb-9654-46ae615fd73a"), new Guid("74a0152e-f989-4bf6-8164-7e515876a65a"), new Guid("5b899c69-af8c-4655-9809-b4158738e1db")).WithObjectTypes(VatRate, OrganisationGlAccount).WithSingularName("VatToPayAccount")  .WithPluralName("VatToPayAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b628964e-5139-4c32-a2c1-239deaff70e8"), new Guid("229bebd5-4899-4f7d-bebd-266ee211f72a"), new Guid("c51def22-77d1-4dc5-bc3e-b55095ae5af1")).WithObjectTypes(VatRate, EuSalesListType).WithSingularName("EuSalesListType")  .WithPluralName("EuSalesListTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cbd85372-08d1-4c6d-81a9-02d76c874c46"), new Guid("235cfb9b-1cea-4e37-8e35-f9993ca175b6"), new Guid("41b72bc0-f2b6-4895-808b-76a5b6fb9035")).WithObjectTypes(VatRate, OrganisationGlAccount).WithSingularName("VatToReceiveAccount")  .WithPluralName("VatToReceiveAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cf879781-9f52-438c-b0e0-fd23f336bead"), new Guid("524372df-0707-4a60-b5f6-1305d197da36"), new Guid("c8831b10-b22f-49d4-a551-05b0c9f6ade2")).WithObjectTypes(VatRate, OrganisationGlAccount).WithSingularName("VatReceivableAccount")  .WithPluralName("VatReceivableAccounts")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e6242c51-98f9-408d-9dd8-07e3c639c82e"), new Guid("11486112-1786-4d73-aee7-cdc1e8b271e3"), new Guid("5224e2d8-7d48-4f63-97ff-796232781f81")).WithObjectTypes(VatRate, allorsBoolean).WithSingularName("ReverseCharge")  .WithPluralName("ReverseCharges")      .Build();
			
            // Invoice
            new RelationTypeBuilder(domain, new Guid("19019399-963f-4075-8754-16e1e5a4c496"), new Guid("43580c50-7831-48c7-afdf-5c4e23bcec93"), new Guid("371d3a45-0283-4c8d-850f-acc9c2395464")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalShippingAndHandlingCustomerCurrency")  .WithPluralName("TotalShippingAndHandlingsCustomerCurrency")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1c535b3f-bb97-43a8-bd29-29c4dc267814"), new Guid("d3155310-1267-4780-b69d-4dd47ef15e73"), new Guid("2603b50f-78b9-429c-be30-38949bdec59a")).WithObjectTypes(Invoice, Currency).WithSingularName("CustomerCurrency")  .WithPluralName("CustomerCurrencies")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2d82521d-30bd-4185-84c7-4dfe08b5ddef"), new Guid("aa6230a9-7a9e-4d42-a14a-49b1c3b382ab"), new Guid("5c1fbd73-39e2-4a4a-b58b-2e6c7a110755")).WithObjectTypes(Invoice, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("2f7d19a8-75e1-4c95-b60f-60343f2dd4bc"), new Guid("31c04533-c845-4b71-bf88-79e4c3ad8ec4"), new Guid("6bdaa705-8ea0-4df2-936a-0b392556a21d")).WithObjectTypes(Invoice, ShippingAndHandlingCharge).WithSingularName("ShippingAndHandlingCharge")  .WithPluralName("ShippingAndHandlingCharges")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3aa6c5be-ae74-401e-8d03-420230c4ea42"), new Guid("3d5e1fcc-a9d7-4c30-9e3b-90091f5ec63e"), new Guid("133d6dcb-5429-42aa-b788-2eccd2633139")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalFeeCustomerCurrency")  .WithPluralName("TotalFeesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("3b1a0c47-dd3e-406c-a1e7-bc88f7a10794"), new Guid("7ce35340-fbb1-4689-a4e5-2a7f17455d37"), new Guid("72db7683-8659-441f-ae23-0407e4e11c11")).WithObjectTypes(Invoice, Fee).WithSingularName("Fee")  .WithPluralName("Fees")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3b6e0ea5-d9b9-4673-89e9-1491b8e6a691"), new Guid("cd0ba793-77fb-44b4-bf1c-4bf32a98d254"), new Guid("d677c967-568a-4c32-a822-caba4ea5990c")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalExVatCustomerCurrency")  .WithPluralName("TotalExVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4b2eedbb-ec59-4e18-949f-f467e41f6401"), new Guid("b41474a8-482f-458f-b70d-b11e97129ea0"), new Guid("5bab4dea-3566-4421-96c5-27b774b6542a")).WithObjectTypes(Invoice, allorsString).WithSingularName("CustomerReference")  .WithPluralName("CustomerReferences")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("4b3a3ad0-d624-46f1-a53c-f79980b50793"), new Guid("72a0c734-9199-45fd-8264-a80721a016f2"), new Guid("95e67307-5e1b-451a-ab4a-c93079b25c76")).WithObjectTypes(Invoice, DiscountAdjustment).WithSingularName("DiscountAdjustment")  .WithPluralName("DiscountAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4d3f69a0-6e9d-4ba3-acd8-e5dab2a7f401"), new Guid("4ac19707-3c95-4b7d-b281-2f9d86c3eeb9"), new Guid("15779f7b-07ce-4373-a9cd-1ee5690ddbfc")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("AmountPaid")  .WithPluralName("AmountsPaid")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6b474ddd-c2fd-4db1-bf18-44c86a309d53"), new Guid("01576aed-1f77-47db-bf04-40aa5dcae63a"), new Guid("f0bd433a-f5cc-4a6d-be5f-8f09594aa566")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalDiscount")  .WithPluralName("TotalDiscounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6ea961d5-89fc-4526-922a-80538ecb5654"), new Guid("66c5cfdd-6af4-4d75-b826-843be3b01bca"), new Guid("f560bb3d-f855-4ec3-a5e7-4bd6c4da2595")).WithObjectTypes(Invoice, BillingAccount).WithSingularName("BillingAccount")  .WithPluralName("BillingAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7b6ab1ed-845d-4671-bda2-43ad2327ea53"), new Guid("d0994e3f-4741-4f9e-9f4f-8923ed3afdf3"), new Guid("4b4902c0-780d-4de2-97ff-54a5f3bdc521")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalIncVat")  .WithPluralName("TotalsIncVat")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7e8de8bd-f1c0-4fa5-a629-34d9d5f71b85"), new Guid("483b0b71-a4a8-4606-a432-d98d8bd262a2"), new Guid("32e8201c-9e71-48e7-ae20-972e14ea4aeb")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalSurcharge")  .WithPluralName("TotalSurcharges")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7fda150d-44c8-45a9-8048-dfe38d936c3e"), new Guid("e2199200-562f-474d-a822-094fba167dc6"), new Guid("09cba9f7-d85f-4c54-a857-c28f22f0eaae")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalBasePrice")  .WithPluralName("TotalBasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8247ec6f-e4b2-424c-922b-a6a5d6b05654"), new Guid("5dd3f140-ff9d-4f87-930c-94b135dd9b7f"), new Guid("d8cbb429-edc1-4d2c-90d3-99b7a4caabb8")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalVatCustomerCurrency")  .WithPluralName("TotalVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("82541f62-bf0e-4e33-9971-15a5a4fa4469"), new Guid("b3579af4-1c8e-46c5-bc1c-a9d7711a4a48"), new Guid("d54fdbf9-c580-4a49-b058-28aab77d81e0")).WithObjectTypes(Invoice, allorsDateTime).WithSingularName("InvoiceDate")  .WithPluralName("InvoiceDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("8798a760-de3d-4210-bd22-165582728f36"), new Guid("d0d6a00a-2d79-4798-b51f-7e6dfb8551d5"), new Guid("c1f88c71-2415-4928-ae3b-16c7f85af30c")).WithObjectTypes(Invoice, allorsDateTime).WithSingularName("EntryDate")  .WithPluralName("EntryDates")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("8e1b6a86-eceb-412e-a246-bd95b564c87b"), new Guid("8e08972a-f735-4a67-b941-7136e29434be"), new Guid("bd40de52-c9c7-48e0-9129-b1d471a03097")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalIncVatCustomerCurrency")  .WithPluralName("TotalIncVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("94029787-f838-47bb-9617-807a8514a350"), new Guid("92badbf6-7d16-46b2-b214-1ea26855970d"), new Guid("2dc528f7-451e-4570-922b-649d9448ed11")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalShippingAndHandling")  .WithPluralName("TotalsShippingAndHandling")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("96baf5df-43b2-4f4b-aca9-bdd2432318a7"), new Guid("52eba17e-91db-4a52-8b4e-98a5eadbde84"), new Guid("1461fa7e-cc65-4006-a8be-cc6220291b7d")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalBasePriceCustomerCurrency")  .WithPluralName("TotalBasePricesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("982949e0-87ac-400c-8830-a779b75e10ad"), new Guid("0892c266-1b04-4d66-b344-1e29ddf09bd4"), new Guid("f1fb8739-1cb1-4080-ac63-b78512218d3a")).WithObjectTypes(Invoice, SurchargeAdjustment).WithSingularName("SurchargeAdjustment")  .WithPluralName("SurchargeAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9eec85a4-e41a-4ca2-82fa-2dc0aa45c9d5"), new Guid("26c9285b-4c0e-443e-914b-ceb95d37a8fe"), new Guid("4d9bb0e9-23b1-429e-bf61-2fa3b9afb2b8")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalExVat")  .WithPluralName("TotalsExVat")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("9ff2d65b-0478-41cc-b70b-0df90cdbe190"), new Guid("38654202-df58-4f2a-9c8d-094fb511a19a"), new Guid("a12bdf85-5c6d-43e4-92b2-8f2fefc03e3e")).WithObjectTypes(Invoice, InvoiceTerm).WithSingularName("InvoiceTerm")  .WithPluralName("InvoiceTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a9504981-4b3e-406c-9fc8-64204efb1deb"), new Guid("409180f3-1b30-457d-86dd-44d4ee834b3e"), new Guid("740953b9-578e-4d0e-916d-5e283a1825be")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalSurchargeCustomerCurrency")  .WithPluralName("TotalSurchargesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ab342937-1e58-4cd7-99b5-c8a5e7afe317"), new Guid("0cd0981d-d26b-42e4-a50d-9747a1171b12"), new Guid("431bbc5d-4de6-4cee-aa2d-f1f5c6e7e745")).WithObjectTypes(Invoice, allorsString).WithSingularName("InvoiceNumber")  .WithPluralName("InvoiceNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b298c12c-620b-4cf2-b47e-df17afc65552"), new Guid("4eff42a0-dfe5-440c-a2d2-7612ece8ff11"), new Guid("92365fb1-d257-4fbd-81e4-097ef6d2405e")).WithObjectTypes(Invoice, allorsString).WithSingularName("Message")  .WithPluralName("Messages")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("c2ecfd15-7662-45b4-99bd-9093ca108d23"), new Guid("32efeb84-a275-4b14-ba1f-aa99ba1bc776"), new Guid("4e4351e1-7174-4337-b448-bd3f79e3aaa4")).WithObjectTypes(Invoice, VatRegime).WithSingularName("VatRegime")  .WithPluralName("VatRegimes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c6a896be-d9e1-40b1-9f85-52dbf2886a58"), new Guid("a6369011-976f-49eb-bd82-69f82ab580f0"), new Guid("7032574d-2cb1-4e08-8d4d-1eb102502c63")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalDiscountCustomerCurrency")  .WithPluralName("TotalDiscountsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c7350047-9282-41c8-8d82-4e1f86369e9c"), new Guid("0468ccd7-0e03-4bff-8812-ee1f979a6a3f"), new Guid("09a4e368-3d7e-4dd5-8708-fa9ff5bddc4b")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalVat")  .WithPluralName("TotalsVat")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("fa826458-5423-43dd-b02f-fe2673a2d0f3"), new Guid("ac559656-d5c1-4325-a267-9775136a25af"), new Guid("837d36ee-f23f-45bc-87a9-9760d08f29c4")).WithObjectTypes(Invoice, allorsDecimal).WithSingularName("TotalFee")  .WithPluralName("TotalsFee")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
			
            // ProfessionalServicesRelationship
            new RelationTypeBuilder(domain, new Guid("62edaaeb-bcef-4c3c-955a-30d708bc4a3c"), new Guid("af3829d6-137c-4453-b705-60b7dfa8c045"), new Guid("29b1fec5-de9c-4fe2-bdfc-fc9d33ca90b5")).WithObjectTypes(ProfessionalServicesRelationship, Person).WithSingularName("Professional")  .WithPluralName("Professionals")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a587695e-a9b3-4b5b-b211-a19096b88815"), new Guid("d3fc269c-debf-4ada-b1be-b2f48d2ae027"), new Guid("c6b955f2-20ed-4164-8f11-2c5d24fa0443")).WithObjectTypes(ProfessionalServicesRelationship, Organisation).WithSingularName("ProfessionalServicesProvider")  .WithPluralName("ProfessionalServicesProviders")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // RecurringCharge
            new RelationTypeBuilder(domain, new Guid("f95e774f-239e-4136-a964-c3d1841a43ba"), new Guid("46b2864f-5c9b-43b9-a6b0-0bcf907adbc8"), new Guid("97a9949b-6266-4fa2-a33a-3b13eaf21a93")).WithObjectTypes(RecurringCharge, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // RequirementStatus
            new RelationTypeBuilder(domain, new Guid("03542f1b-23ac-4bfc-add5-bad028295b4e"), new Guid("9000b234-a6cf-4707-8a33-b90f6ee7b869"), new Guid("7baebeac-8013-4acc-bef7-508eed0eb1c3")).WithObjectTypes(RequirementStatus, RequirementObjectState).WithSingularName("RequirementObjectState")  .WithPluralName("RequirementObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("49e2a03a-aeba-4ae2-9f75-47639334bde6"), new Guid("a3ac3a93-90ed-4bfb-8a8c-e1fbd8fe743e"), new Guid("265a7954-c3be-420b-98b5-ff70076cefdf")).WithObjectTypes(RequirementStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // DropShipment
            new RelationTypeBuilder(domain, new Guid("0984f98c-fc64-4c86-aeb6-1d804d1506db"), new Guid("f7de3d8b-e404-4652-8eb1-dc58f8307e14"), new Guid("9ac05629-f7ae-422e-8131-78389ba7ecf9")).WithObjectTypes(DropShipment, DropShipmentStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("44230591-89df-46ec-882c-09bbac7fd5d2"), new Guid("fa5c5391-6bf5-435c-ba35-08d5315216db"), new Guid("a3b29fd7-cf97-4cbf-9329-681542e8de75")).WithObjectTypes(DropShipment, DropShipmentStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatus")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6f2e27fb-5a64-43fe-916c-f559cc1eddee"), new Guid("834a0334-bd03-4a18-bfcd-6a79e2d86533"), new Guid("422f7610-0d21-406c-9ce4-e6d8beec6624")).WithObjectTypes(DropShipment, DropShipmentObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a7d6815b-9d6c-44c4-a80f-bc2fd8aa1ea7"), new Guid("14c83374-67ae-480b-a67d-597e8614480e"), new Guid("9b4e523e-215a-4b2a-bd99-1540113e5fc3")).WithObjectTypes(DropShipment, DropShipmentObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // SalesInvoiceItem
            new RelationTypeBuilder(domain, new Guid("0854aece-6ca1-4b8d-99a9-6d424de8dfd4"), new Guid("cebb5430-809a-4d46-bc7b-563ee72f0848"), new Guid("f1f68b89-b95f-43c9-82d5-cb9eec635869")).WithObjectTypes(SalesInvoiceItem, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("0a93f639-a456-4318-a8fa-8d3c2a107379"), new Guid("f9476899-7bd7-472a-ae64-0a7f4610cb87"), new Guid("56ce0901-621f-407f-81be-9921ad6d19be")).WithObjectTypes(SalesInvoiceItem, SalesInvoiceItemObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("103d42a5-fdee-4689-af19-2ea4c8060de3"), new Guid("ee01bcc4-b926-444d-8982-8c56158327f1"), new Guid("a1643b4c-c95e-427c-a6b8-44860bc79d6e")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("RequiredProfitMargin")  .WithPluralName("RequiredProfitMargins")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1a18b2f1-a31e-4ec3-8981-5f65af2ff907"), new Guid("398e3c8d-1b7f-40c5-a4f1-4a086d369199"), new Guid("514101cb-6833-4935-81e7-79c64b417a26")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("InitialMarkupPercentage")  .WithPluralName("InitialMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2f6e0b52-d37c-4caf-91d0-862666195247"), new Guid("898628e9-2191-4a2f-b05d-517b5ac90e5c"), new Guid("6a71a9f7-f572-4cd2-b8ca-86e3c85a5d71")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("MaintainedMarkupPercentage")  .WithPluralName("MaintainedMarkupPercentages")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("4daa5c18-85c6-49c0-8f23-8e419e44471c"), new Guid("061348dc-59a2-41d1-92bb-ccf16a1f31aa"), new Guid("a037ec30-f0f4-4dda-8eb5-80a042b26399")).WithObjectTypes(SalesInvoiceItem, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4f9e110d-fca8-4956-9d2f-178843eb9b9f"), new Guid("95aa4883-8bd0-4cd7-a060-4efabaef6530"), new Guid("02e0ee54-d063-4b00-87be-c2d3747ef3a6")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("UnitPurchasePrice")  .WithPluralName("UnitPurchasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5a2c6c32-f7b6-40da-859f-a430edc27a43"), new Guid("31bd5084-75e8-4781-a7f0-d4a82f391066"), new Guid("c627c330-7381-4363-be05-3c80cac5b8af")).WithObjectTypes(SalesInvoiceItem, SalesInvoiceItemStatus).WithSingularName("InvoiceItemStatus")  .WithPluralName("InvoiceItemStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5bdae88b-856d-4746-8645-9bded2a4a3bd"), new Guid("2b93a791-124c-45ac-8f3c-bf33f2dcfc13"), new Guid("b303f168-96d8-478a-b42c-6b7594b8db42")).WithObjectTypes(SalesInvoiceItem, SalesOrderItem).WithSingularName("SalesOrderItem")  .WithPluralName("SalesOrderItems")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6dd4e8ee-48ed-400d-a129-99a3a651586a"), new Guid("f99e5e01-943c-4de9-862c-c472d2d873f2"), new Guid("6cb182c2-b481-4e26-869e-609990ea68b3")).WithObjectTypes(SalesInvoiceItem, SalesInvoiceItemType).WithSingularName("SalesInvoiceItemType")  .WithPluralName("SalesInvoiceItemTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("90866201-03a1-44b2-9318-5048639b58c8"), new Guid("0618fddc-dee4-4cd4-9d4d-b7356be9dc65"), new Guid("d61277d3-b916-4783-9de0-48f9eb6808c4")).WithObjectTypes(SalesInvoiceItem, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a04f506f-7ac9-4ab9-8f3f-1aba1ae76a67"), new Guid("7774b9a7-e842-4b3d-b608-5d039b0811fb"), new Guid("b7b589f5-59f2-4004-862f-0fb6c790137d")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("InitialProfitMargin")  .WithPluralName("InitialProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a9f8629d-bb0d-4f73-8ccb-81b6d64b23a0"), new Guid("0237658e-7d41-44a8-b75d-4e9dea506eda"), new Guid("d72a1dd5-5c83-4848-9c58-901fae551bb8")).WithObjectTypes(SalesInvoiceItem, SalesInvoiceItemStatus).WithSingularName("CurrentInvoiceItemStatus")  .WithPluralName("CurrentInvoiceItemStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ba9acc7e-635d-4387-98eb-67ea26e9e2db"), new Guid("0198d048-f14e-419d-ac2f-1f7f8e2d0bbc"), new Guid("7d6f6274-24bb-47e4-892b-ce95cd197d77")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("MaintainedProfitMargin")  .WithPluralName("MaintainedProfitMargins")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("bd485f1f-6937-4270-8695-6f9a50e671c3"), new Guid("4314e405-2692-4cda-9617-804b43d7090f"), new Guid("b8ab5103-31c0-41cb-b6a0-e8f3e18a7945")).WithObjectTypes(SalesInvoiceItem, TimeEntry).WithSingularName("TimeEntry")  .WithPluralName("TimeEntries")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bfd8c2d5-57f9-4650-97ae-2f2b1819b3a9"), new Guid("6dbc805e-2360-49ef-bdd5-644a454cae40"), new Guid("b6e9179b-b7d8-4ad8-9aee-0ca3adef40af")).WithObjectTypes(SalesInvoiceItem, allorsDecimal).WithSingularName("RequiredMarkupPercentage")  .WithPluralName("RequiredMarkupPercentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e8121ef7-e1f1-4245-a003-bae93e076a09"), new Guid("7986627b-3328-4d54-9064-052a81fec92d"), new Guid("dab55577-afea-4030-ad75-2c12873c2785")).WithObjectTypes(SalesInvoiceItem, SalesInvoiceItemObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // EngagementItem
            new RelationTypeBuilder(domain, new Guid("141333b6-2cc9-487e-acc1-86d314f2b30a"), new Guid("17fbbe0c-7d74-46ba-b5dd-a115536dd1a6"), new Guid("10b8af44-2efd-4549-981c-8471860dfb55")).WithObjectTypes(EngagementItem, QuoteItem).WithSingularName("QuoteItem")  .WithPluralName("QuoteItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2a187dcd-5004-4722-a0ec-e571cd5b5bc6"), new Guid("f733d61f-a981-4a80-9816-dc10e0d1e2c9"), new Guid("a8912656-740c-4216-93f6-8fff119c385e")).WithObjectTypes(EngagementItem, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("33fe3f86-8b73-4a70-b9c0-62ac27531ac3"), new Guid("24a3d499-1f30-4b0e-8a27-a42808c4b1a2"), new Guid("5e4915f7-955d-41a9-9c38-d8b6f7837ea4")).WithObjectTypes(EngagementItem, allorsDateTime).WithSingularName("ExpectedStartDate")  .WithPluralName("ExpectedStartDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("3635cb84-2d4f-4fa1-ac18-4c8a6cc129c5"), new Guid("b58461be-8138-42e1-9e4b-e095ae66fc90"), new Guid("afc29589-892c-41ca-94b3-92a775009a6e")).WithObjectTypes(EngagementItem, allorsDateTime).WithSingularName("ExpectedEndDate")  .WithPluralName("ExpectedEndDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("40b24df7-6834-401a-a598-82203af63f99"), new Guid("04cbacfd-910f-4707-b952-ffdaaab28c60"), new Guid("3345748e-d859-47f4-bb45-1920469b1cfc")).WithObjectTypes(EngagementItem, WorkEffort).WithSingularName("EngagementWorkFulfillment")  .WithPluralName("EngagementWorkFulfillments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9133f59e-048d-4020-88e4-5a4bc36d663b"), new Guid("46ad58c7-3125-4307-93ae-58c386e98899"), new Guid("3065d420-15ec-47d3-9fa6-56a79d4c315b")).WithObjectTypes(EngagementItem, EngagementRate).WithSingularName("EngagementRate")  .WithPluralName("EngagementRates")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9e1f4da4-41af-4030-b67f-79f1f49fa076"), new Guid("b5361ebf-2809-4fe7-8f24-bd68ec61c9b8"), new Guid("124043c0-dd7e-4d94-9c0c-a3804c343f11")).WithObjectTypes(EngagementItem, EngagementRate).WithSingularName("CurrentEngagementRate")  .WithPluralName("CurrentEngagementRates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b445f2d6-55a6-4cb4-9550-5be8863eddb6"), new Guid("21509869-1643-402a-a5eb-9657f1f01af9"), new Guid("8844d711-33d6-4d19-ad21-edcd60851f1d")).WithObjectTypes(EngagementItem, EngagementItem).WithSingularName("OrderedWith")  .WithPluralName("OrderedWiths")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c2ec3c6b-af56-4c6b-bdaf-76d3ea340bf7"), new Guid("d9a53328-0414-4403-bd54-37b48ec05823"), new Guid("a2dd5921-6ec7-4d7b-8aae-9a7e685688d1")).WithObjectTypes(EngagementItem, Person).WithSingularName("CurrentAssignedProfessional")  .WithPluralName("CurrentAssignedProfessionals")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c7204c16-67b1-4e6d-b787-ce8ab9c6c111"), new Guid("d417f454-c1fa-41da-8b00-653b27d875a4"), new Guid("eaa02501-f6d8-4d12-b11e-523bf70805a4")).WithObjectTypes(EngagementItem, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dbb3d0c5-836d-477b-a42f-b260f3316458"), new Guid("888670c7-e42c-41eb-994f-91af9d2d93f3"), new Guid("ce43a83c-0289-42b5-9330-0341fa847809")).WithObjectTypes(EngagementItem, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // OrderQuantityBreak
            new RelationTypeBuilder(domain, new Guid("6d20ad83-150b-44d7-940c-725554175ba9"), new Guid("8d3c682c-a6fa-4ff9-9734-1a0fb21342fe"), new Guid("88caf998-c922-437c-84a2-fa9370c6fb28")).WithObjectTypes(OrderQuantityBreak, allorsDecimal).WithSingularName("ThroughAmount")  .WithPluralName("ThroughAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("9ac69278-fef8-4f82-8dfa-dcc192274e23"), new Guid("9a9c5ef7-d3d0-4787-a653-b2c8893bd737"), new Guid("16547884-680e-45fe-a85a-7aa77e896f50")).WithObjectTypes(OrderQuantityBreak, allorsDecimal).WithSingularName("FromAmount")  .WithPluralName("FromAmounts")      .WithPrecision(19).WithScale(2).Build();
			
            // Event
            new RelationTypeBuilder(domain, new Guid("189505d9-434f-4d12-a6ab-44edcf44801c"), new Guid("edd0f108-0d6c-414a-8460-2a6f2e4c8f6b"), new Guid("ea95aeb1-2d78-4d96-b725-cb5bc7268176")).WithObjectTypes(Event, allorsBoolean).WithSingularName("RegistrationRequired")  .WithPluralName("RegistrationsRequired")      .Build();
            new RelationTypeBuilder(domain, new Guid("1a4f5119-23c5-4cbe-afdb-565c0e8f9e80"), new Guid("1ba99ae5-2e3b-4e41-ba52-75f724860ee3"), new Guid("1f2c258c-30c3-4e2d-a1ad-263fe0680381")).WithObjectTypes(Event, allorsString).WithSingularName("Link")  .WithPluralName("Links")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6eb8fbc4-7fbd-4eb6-8944-01737b1182cc"), new Guid("a3aa3fe3-8d70-435b-b567-823d4771d3fa"), new Guid("f7d30205-c1fa-4cfa-9194-21301e5812fb")).WithObjectTypes(Event, allorsString).WithSingularName("Location")  .WithPluralName("Locations")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("78cfaf88-c3c4-41d1-b9f0-f69a82646930"), new Guid("c23f6022-4df9-46ce-9eed-7dabf1f1f502"), new Guid("2eaa85f3-e70f-4a4c-96a1-64e68457261c")).WithObjectTypes(Event, allorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("79b05cf2-2175-4724-acdd-88bc05f15881"), new Guid("7276942a-8c26-466f-aa32-698454184454"), new Guid("def8b1dc-c837-40a1-bbcc-4bb00b0250e0")).WithObjectTypes(Event, allorsString).WithSingularName("AnnouncementText")  .WithPluralName("AnnouncementTexts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("7a66f2bc-bfb1-420a-a383-acf3092ca48b"), new Guid("d3943099-a5ec-413b-9079-239c67bdc696"), new Guid("1c1aead6-f157-4d23-a9f0-0565e2b7ff82")).WithObjectTypes(Event, allorsDateTime).WithSingularName("From")  .WithPluralName("Froms")      .Build();
            new RelationTypeBuilder(domain, new Guid("7d73d60c-bcb2-4be6-bc60-e4420a8d0417"), new Guid("09cdba21-c34e-465e-847b-8062232c6d85"), new Guid("f80dabde-e6f0-4044-9468-f96641bdd49a")).WithObjectTypes(Event, Locale).WithSingularName("Locale")  .WithPluralName("Locales")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b044d498-2995-41d2-8487-0ec323b011bc"), new Guid("b517e0c7-6b49-4f27-bbb3-3cd291fd14fd"), new Guid("6f371186-d82b-42ac-a7f1-a8382454a332")).WithObjectTypes(Event, allorsString).WithSingularName("Title")  .WithPluralName("Titles")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("cbc5a9f6-cd08-41aa-a4aa-dac9a8a802ac"), new Guid("ec42a541-030f-4fbe-9fba-145c8fbc8e87"), new Guid("ecc373d4-636f-4114-8635-55a97e629607")).WithObjectTypes(Event, Media).WithSingularName("Photo")  .WithPluralName("Photos")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d9d15920-705f-4ca3-bfa1-47bd5d5b7238"), new Guid("a5a2ab0f-d7c9-44c7-9fd5-be9cc9ea1666"), new Guid("79827257-f70d-4961-8fa0-4798a4f4a28d")).WithObjectTypes(Event, allorsBoolean).WithSingularName("Announce")  .WithPluralName("Announces")      .Build();
            new RelationTypeBuilder(domain, new Guid("de61dd0d-1f8e-4a55-9fe4-f44cf35b6a31"), new Guid("90352035-7b90-414f-be38-7f3e4d5fbd95"), new Guid("b8c6fe1f-7c7e-41ae-8f03-32a18e4920e5")).WithObjectTypes(Event, allorsDateTime).WithSingularName("To")  .WithPluralName("Tos")      .Build();
			
            // ClientRelationship
            new RelationTypeBuilder(domain, new Guid("d611f21a-1045-40ea-b05b-0c29913d5f1c"), new Guid("115baf34-414a-4cfa-8d1f-dfbeb7264077"), new Guid("69161130-4a2e-430e-92a2-b8ab0e6ef8dc")).WithObjectTypes(ClientRelationship, Party).WithSingularName("Client")  .WithPluralName("Clients")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e081884c-3db2-4be3-9c85-9167528d751b"), new Guid("32544879-3730-449a-9835-8decbfe9f4fc"), new Guid("2f9c92b5-7cf2-42ba-924d-4b5d0c73956c")).WithObjectTypes(ClientRelationship, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PurchaseOrderItem
            new RelationTypeBuilder(domain, new Guid("0d6cc324-fa0e-4a8c-8afd-802a6301a6c7"), new Guid("68ad7777-1d14-4635-8f36-1c1e68bd1989"), new Guid("ddbd34f4-264a-4465-b57c-a3f9c76e6a52")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemStatus).WithSingularName("OrderItemStatus")  .WithPluralName("OrderItemStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("43035995-bea3-488b-9e81-e85e929faa57"), new Guid("f9d773a8-772b-4981-a360-944f14a5ef94"), new Guid("f7034bc1-6cc0-4e03-ab3c-da64d427df9b")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("47af92f0-f773-40e2-b0ed-4b3677eddbb7"), new Guid("6eb5977f-2a79-49e1-ac87-16a53de7e40b"), new Guid("e2ee216b-ae28-4ddf-b354-aa7a75f4cc4e")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5e2f5c1a-99e7-4906-8cdd-e78ac4f4bce0"), new Guid("de791292-84df-4297-959f-d3bc61a2e137"), new Guid("5f9865d9-b7b2-42e3-b13d-013b8945e843")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemStatus).WithSingularName("PaymentStatus")  .WithPluralName("PaymentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("64e30c56-a77d-4ecf-b21e-e480dd5a25d8"), new Guid("448695c9-c23b-4ae0-98d7-802a8ae4e9f8"), new Guid("9586b58f-8ae0-4b26-81b6-085a9e28aa77")).WithObjectTypes(PurchaseOrderItem, allorsDecimal).WithSingularName("QuantityReceived")  .WithPluralName("QuantitiesReceived")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6c187e2c-d7ab-4d3d-b8d9-732af7310e7e"), new Guid("50d321f7-fa51-4d08-a12d-e7b8702d2c33"), new Guid("0aab6049-05b6-494a-ac11-df251374f8f4")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("adfe14e7-fbf6-465f-b6e5-1eb3e8583179"), new Guid("682538a3-d3e7-432b-9264-38197462cee1"), new Guid("fecc85a0-871b-4846-b8f1-c2a5728fbbd2")).WithObjectTypes(PurchaseOrderItem, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bbe10173-c24c-4514-86ec-96bd0741efa6"), new Guid("d12015c4-7462-4dec-95b6-2c233cbb8607"), new Guid("75c95f93-74c1-47b9-9bcc-457edc48a4b3")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemStatus).WithSingularName("CurrentOrderItemStatus")  .WithPluralName("CurrentOrderItemStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cca92fe0-8711-46fd-b08d-bf313cc585a6"), new Guid("db50db5b-59d8-46b9-9c59-d1b9a93fec11"), new Guid("425b29d1-4001-46e0-821c-6da18051d3ee")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemStatus).WithSingularName("CurrentPaymentStatus")  .WithPluralName("CurrentPaymentStatus")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("de9fac15-23af-46be-9083-de57c71d3866"), new Guid("5c2e6357-2016-4302-bfe7-b0721c5ef00a"), new Guid("eed3684b-8f57-41e6-b247-1aaf07d2bee6")).WithObjectTypes(PurchaseOrderItem, PurchaseOrderItemObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e2dc0027-220b-4935-bc5a-cb2e2b6be248"), new Guid("3d24da0d-fdd6-46e3-909b-7710e84e2d68"), new Guid("76ed288c-be72-44e2-8b83-0a0f5a616e52")).WithObjectTypes(PurchaseOrderItem, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // WorkEffortAssignmentRate
            new RelationTypeBuilder(domain, new Guid("3a4e3614-cb04-4014-826b-78a2b87e6a1f"), new Guid("0548e8ad-5ba7-462a-b3ef-6be2014bad65"), new Guid("26ee13eb-79f5-46a1-8749-485f43a3ee8c")).WithObjectTypes(WorkEffortAssignmentRate, RateType).WithSingularName("RateType")  .WithPluralName("RateTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("627da684-d501-4221-97c2-81329e2b5e8c"), new Guid("4b9c1fd3-acf0-4e5b-8cb5-d32f94bff10b"), new Guid("e6409680-f8e1-4c61-8bd3-b9ec42435741")).WithObjectTypes(WorkEffortAssignmentRate, WorkEffortPartyAssignment).WithSingularName("WorkEffortPartyAssignment")  .WithPluralName("WorkEffortPartyAssignments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Province
            new RelationTypeBuilder(domain, new Guid("e04bddba-a014-4793-8787-d9cb83ba7d60"), new Guid("da01d60d-4b8a-4472-9a6a-c21af0963a0b"), new Guid("211c25b7-ecc2-4bdb-a73c-f37090eb165c")).WithObjectTypes(Province, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // InventoryItemVariance
            new RelationTypeBuilder(domain, new Guid("57bdf1d7-84b8-4c7c-a470-396f6facd3bd"), new Guid("6f8706cd-f005-4ab1-8deb-db5d00b72403"), new Guid("1a45c449-2b0d-4f64-be40-0858018b9cf6")).WithObjectTypes(InventoryItemVariance, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("58ead8d2-c9c3-4092-b5d1-79af4811f43c"), new Guid("82f2636f-738d-45b8-bdc0-5136ad8d8382"), new Guid("eee9f92b-5649-467f-8a99-4318c24cc002")).WithObjectTypes(InventoryItemVariance, ItemVarianceAccountingTransaction).WithSingularName("ItemVarianceAccountingTransaction")  .WithPluralName("ItemVarianceAccountingTransactions")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("af9fa5bc-a392-473d-b077-7f06ee24390b"), new Guid("9a0f9ecd-9954-4c2f-bb0e-e94f9cc3c19a"), new Guid("5665d533-cd9c-4328-b422-66a94d77b19b")).WithObjectTypes(InventoryItemVariance, allorsDateTime).WithSingularName("InventoryDate")  .WithPluralName("InventoryDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("e422efc4-4d17-46d8-bba4-6e78e7761f93"), new Guid("468307f7-5033-4e77-9482-5df34ca9a4f1"), new Guid("7e0b8650-0d19-4ecc-b6e6-3c78dfe8c2aa")).WithObjectTypes(InventoryItemVariance, VarianceReason).WithSingularName("Reason")  .WithPluralName("Reasons")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ContactMechanism
            new RelationTypeBuilder(domain, new Guid("3c4ab373-8ff4-44ef-a97d-d8a27513f69c"), new Guid("0edba6dd-606a-4751-bd86-b98822d4b1f2"), new Guid("ab370eaa-ede5-4bee-a9ab-6f6e52889f77")).WithObjectTypes(ContactMechanism, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e2bd1f50-f891-4e3f-bac0-e9582b89e64c"), new Guid("9bb26e51-9acf-4fa1-8a24-3d0b1b2f7103"), new Guid("d85aba30-7aee-4bc4-9026-26ba4f355d70")).WithObjectTypes(ContactMechanism, ContactMechanism).WithSingularName("FollowTo")  .WithPluralName("FollowTo")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // CommunicationEvent
            new RelationTypeBuilder(domain, new Guid("01665c57-a343-441d-9760-53763badce51"), new Guid("82c1dad0-6d6d-440c-8bf0-f20d35ab0863"), new Guid("0dd9728e-0887-4029-af20-dd69371fbba0")).WithObjectTypes(CommunicationEvent, allorsDateTime).WithSingularName("ScheduledStart")  .WithPluralName("ScheduledStarts")      .Build();
            new RelationTypeBuilder(domain, new Guid("250911c2-1f8d-4946-8c7f-3e3fa47d66a5"), new Guid("6537fd2e-e4a7-4cee-9494-e0b54d717b62"), new Guid("4cd91320-82a3-4379-b589-cc834a713591")).WithObjectTypes(CommunicationEvent, CommunicationEventStatus).WithSingularName("CommunicationEventStatus")  .WithPluralName("CommunicationEventStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("28874ffe-f3b3-4aba-9f28-ba7c15b0cb65"), new Guid("544164cd-43e9-4e3c-a0b2-a33574accd7c"), new Guid("cbf3c355-cf99-4bd4-8f8b-e0dca835b9d2")).WithObjectTypes(CommunicationEvent, Party).WithSingularName("InvolvedParty")  .WithPluralName("InvolvedParties")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2fa315f8-6208-495c-bcc4-2ccda734cc09"), new Guid("6b5d29f8-7016-4cdb-9af9-8320b1c7304d"), new Guid("8e7c8bab-063d-4f77-99ae-6e7979b63ce4")).WithObjectTypes(CommunicationEvent, allorsDateTime).WithSingularName("InitialScheduledStartDate")  .WithPluralName("InitialScheduledStartDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("3657016d-01c5-43db-bd03-5203c1aef14d"), new Guid("4c2863c0-dc44-42e6-a330-a5c82a37151d"), new Guid("69227618-628c-4a54-8ad9-bc20d087413d")).WithObjectTypes(CommunicationEvent, CommunicationEventObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3a5658bd-b1b9-47e3-b542-ea9de348a44e"), new Guid("6086288c-6880-4b98-a0ef-7b4a7ecd0af9"), new Guid("d55ec601-4f3f-4834-baec-1675234e7ba5")).WithObjectTypes(CommunicationEvent, CommunicationEventPurpose).WithSingularName("EventPurpose")  .WithPluralName("EventPurposes")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("51f3e08a-7b1b-4d5b-989c-ad2c734a1b2f"), new Guid("4f409a5c-1de8-4c4f-a157-02f79bef3efb"), new Guid("1fce36c5-aa88-443b-a3a3-c8bd2fd032dd")).WithObjectTypes(CommunicationEvent, WorkEffort).WithSingularName("WorkEffort")  .WithPluralName("WorkEfforts")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("52adc5f3-d6ef-4804-8755-b86532d8b6fe"), new Guid("3c7ad2b5-b1c0-4509-b1e3-6e902778bee6"), new Guid("8722394b-3873-4eb2-8bf4-d70abaf0a77a")).WithObjectTypes(CommunicationEvent, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("79e945d3-1200-4a90-8e80-eba298bcda40"), new Guid("da2c6684-c940-439b-a4b0-76bb1c3cfc12"), new Guid("22204173-7328-4fe4-a1a6-c394b5908a54")).WithObjectTypes(CommunicationEvent, allorsString).WithSingularName("Subject")  .WithPluralName("Subjects")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("7ad2db94-8251-4fb6-9ef0-be9ef3b6b521"), new Guid("20c1124c-12ad-4983-a4d9-9622b3822f9a"), new Guid("98d28f23-7c1b-45f4-b807-8477ae91a7a1")).WithObjectTypes(CommunicationEvent, CommunicationEventObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("91a1555b-a126-4727-86a4-e57e20ebb5da"), new Guid("38c18c13-4e90-459e-8595-60f1b070cd2a"), new Guid("767e994e-523c-4f2d-a974-470bedb64087")).WithObjectTypes(CommunicationEvent, Media).WithSingularName("Document")  .WithPluralName("Documents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9e52b6a3-3f94-43d6-9fda-879f57499c05"), new Guid("9dd6ccef-f816-40d6-9bb4-e1e88b2e0c06"), new Guid("7c309ce2-dd9a-4299-b462-b506b8ca54f4")).WithObjectTypes(CommunicationEvent, Case).WithSingularName("Case")  .WithPluralName("Cases")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c43b6f6f-0fda-4794-9199-84b39373ecb3"), new Guid("f8f85fd4-3b97-4a67-8b42-12e17938c802"), new Guid("bc58b136-9b36-4065-babb-934ede99aefd")).WithObjectTypes(CommunicationEvent, Person).WithSingularName("Owner")  .WithPluralName("Owners")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d515f7b6-50d5-447f-b69a-2d1c78b465d3"), new Guid("dbcacd62-f6d0-4c5e-ae39-d3943042c1eb"), new Guid("ff52f71d-40fe-4d0c-9334-800bf9bde1f1")).WithObjectTypes(CommunicationEvent, CommunicationEventStatus).WithSingularName("CurrentCommunicationEventStatus")  .WithPluralName("CurrentCommunicationEventStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ecc20a6a-ef70-4a09-8a3b-c8dce88eaa27"), new Guid("abdb3a26-ae86-4500-a9d9-d9546fb6f856"), new Guid("406f48d7-a0be-48c9-81f5-7b506b41e114")).WithObjectTypes(CommunicationEvent, allorsDateTime).WithSingularName("ActualStart")  .WithPluralName("ActualStarts")      .Build();
			
            // PositionResponsibility
            new RelationTypeBuilder(domain, new Guid("493412a4-c29c-4e1c-9167-6c0c0dca831f"), new Guid("030fa5c5-e41f-4141-a91e-02b37a20e685"), new Guid("fe87742c-4238-4be0-9f58-70ae3f01c96b")).WithObjectTypes(PositionResponsibility, Position).WithSingularName("Position")  .WithPluralName("Positions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9c8794b9-2c7b-4afa-86a6-21fb48fc902f"), new Guid("7613dcb8-0c6f-4c65-96c0-75d2cc9db16e"), new Guid("70d2a311-d09b-406c-89d4-3adbbc0a8fe2")).WithObjectTypes(PositionResponsibility, Responsibility).WithSingularName("Responsibility")  .WithPluralName("Responsibilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ResourceRequirement
            new RelationTypeBuilder(domain, new Guid("0655305f-5658-45de-b901-a908a4887a0f"), new Guid("10db5470-e9cb-464a-bcd4-65dd4434b6fe"), new Guid("b1a4dec7-156a-4c16-94b7-966dba1faef1")).WithObjectTypes(ResourceRequirement, allorsString).WithSingularName("Duties")  .WithPluralName("DutiesPlural")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("46f13bbb-430e-47e3-a8e2-edf3ae190417"), new Guid("48167ab3-671a-4a2c-b5c9-e8964f35448b"), new Guid("841659d8-deb9-4902-855b-22b9c9822331")).WithObjectTypes(ResourceRequirement, allorsDecimal).WithSingularName("NumberOfPositions")  .WithPluralName("NumbersOfPositions")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a0a42e5c-3106-4709-aa7b-c916a0ba8508"), new Guid("dc0b4ab7-19d1-4d15-938a-07ce77ba3b23"), new Guid("eeec43e2-dcb4-4ae1-aceb-afbb9da25f68")).WithObjectTypes(ResourceRequirement, allorsDateTime).WithSingularName("RequiredStartDate")  .WithPluralName("RequiredStartDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("d1c048c9-eb05-4cd2-a06c-a8dacf993ab2"), new Guid("88727cd2-46fd-44c0-a5bf-08698256592d"), new Guid("602edcd9-9446-4bbd-9133-1fbb45f423db")).WithObjectTypes(ResourceRequirement, NeededSkill).WithSingularName("NeededSkill")  .WithPluralName("NeededSkills")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ffd07bff-38a2-4284-958d-18b1296f6112"), new Guid("ff6383a4-8502-4f12-8337-6a6ead2f3f0f"), new Guid("1aea6980-6914-4587-98ad-93d9164ebd63")).WithObjectTypes(ResourceRequirement, allorsDateTime).WithSingularName("RequiredEndDate")  .WithPluralName("RequiredEndDates")      .Build();
			
            // BudgetItem
            new RelationTypeBuilder(domain, new Guid("24645d36-9f98-4d08-a7e0-51c1132a110d"), new Guid("27704145-5c5c-4267-b1aa-27f8a64284bb"), new Guid("ef36805b-89db-4195-9848-234e3adf9ba8")).WithObjectTypes(BudgetItem, allorsString).WithSingularName("Purpose")  .WithPluralName("Puposes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6b313789-9a6d-47ca-adad-def39af1e11f"), new Guid("bab74221-37b5-424d-895b-6e79e54fbf0d"), new Guid("013505ff-07c0-4e13-9efe-6347111c2ce8")).WithObjectTypes(BudgetItem, allorsString).WithSingularName("Justification")  .WithPluralName("Justifications")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a4e584cc-7cf6-4590-83e4-a827a7a06624"), new Guid("76a35c5f-4f20-4520-ba02-f68a0dd61e0d"), new Guid("528620b6-e0a7-424a-8c8a-a9fa9c8ed84c")).WithObjectTypes(BudgetItem, BudgetItem).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("cced2368-6a7d-4aea-8112-57dead05f7b4"), new Guid("3af60015-58d7-4bcb-8776-b209875d44ba"), new Guid("b2badaf8-f9fb-4869-a0ab-d5c8fb9a7f51")).WithObjectTypes(BudgetItem, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // PositionReportingStructure
            new RelationTypeBuilder(domain, new Guid("23b91508-508f-4afe-8259-a17f16381833"), new Guid("71f3915d-e412-40fa-abb3-c083e8b2488b"), new Guid("b658e2b8-0929-4770-b231-e532653d0841")).WithObjectTypes(PositionReportingStructure, allorsBoolean).WithSingularName("Primary")  .WithPluralName("Primaries")      .Build();
            new RelationTypeBuilder(domain, new Guid("5fbc72bf-2153-4b91-83f9-6fd057e4b1d6"), new Guid("c06de12f-bf0e-4d91-b8f6-9f6b250b107c"), new Guid("26944bd3-762b-4436-ba19-5e5c34c1247f")).WithObjectTypes(PositionReportingStructure, Position).WithSingularName("ManagedByPosition")  .WithPluralName("ManagedByPositions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e2e60d09-ebfa-4bf3-94e9-759279b00919"), new Guid("1e94dba5-c7d3-41ca-ae79-80b0d2b2ce3c"), new Guid("7b375d54-2364-422c-a264-dd6438d53d33")).WithObjectTypes(PositionReportingStructure, Position).WithSingularName("Position")  .WithPluralName("Positions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Partnership
            new RelationTypeBuilder(domain, new Guid("c8eafc73-9fb3-4a7b-8349-1dd1e9f64520"), new Guid("4d6ee3e0-4c0c-4387-b140-e2296c8bcbd4"), new Guid("386770df-4089-482e-9b54-af375c37319f")).WithObjectTypes(Partnership, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c9a60b88-e525-4bcd-94bd-3fca8989319f"), new Guid("309ffb3e-7cd3-4958-9177-e7f25a272579"), new Guid("f77b776b-b957-418b-acfb-a4aad51f7a8a")).WithObjectTypes(Partnership, Organisation).WithSingularName("Partner")  .WithPluralName("Partners")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // IUnitOfMeasure
            new RelationTypeBuilder(domain, new Guid("22d65b11-5d96-4632-9e95-72e30b885942"), new Guid("873998c2-8c2e-415a-a3c3-6406b21febd8"), new Guid("0543bd39-be9a-49cb-ae23-5df243ee7ea5")).WithObjectTypes(IUnitOfMeasure, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("65c75f72-3bb4-415c-8aa7-b291d96dd157"), new Guid("9225dd82-fdb4-451f-a1cf-000fa37268f1"), new Guid("d202f3f6-2f04-4b2e-8c66-d630be77d76d")).WithObjectTypes(IUnitOfMeasure, UnitOfMeasureConversion).WithSingularName("UnitOfMeasureConversion")  .WithPluralName("UnitOfMeasureConversions")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9b0e7410-6201-420c-9efc-0689edb33d42"), new Guid("2e153d1b-3e03-4ff6-84a6-39c9186999f8"), new Guid("60ebb0ec-bcb8-46c5-8293-36b3b0ad3bdb")).WithObjectTypes(IUnitOfMeasure, allorsString).WithSingularName("Abbreviation")  .WithPluralName("Abbreviations")      .WithSize(256).Build();
			
            // SalesRepCommission
            new RelationTypeBuilder(domain, new Guid("24a89a6a-dc83-431c-b94c-b2c976bc1784"), new Guid("f7da0085-eeb3-4a4c-bc5c-2b0b32340d80"), new Guid("3927fecb-c2fc-442e-b84e-1922a197acde")).WithObjectTypes(SalesRepCommission, allorsDecimal).WithSingularName("Commission")  .WithPluralName("Commisisons")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("af3ad6bb-70a1-4682-945c-4cad1407ecf2"), new Guid("cf6d2b69-9646-46d2-a7a1-d02f8b8ec954"), new Guid("6361ff04-7d8e-45f3-b3a5-efe1f6c983be")).WithObjectTypes(SalesRepCommission, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d1afca2c-802a-4e66-8c76-42a2e6a1e0a6"), new Guid("5215dffe-8b1d-43d2-8f88-c16d3e328456"), new Guid("803e015d-6022-4397-a7be-2dd1c2556cca")).WithObjectTypes(SalesRepCommission, allorsString).WithSingularName("SalesRepName")  .WithPluralName("SalesRepNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("d3cb9001-ff8c-47e0-b91d-4d3d3fd245ed"), new Guid("120813ab-5471-4fb6-aa85-417bfbf18559"), new Guid("c17a6b73-52de-41da-9d4d-f00faa10f60a")).WithObjectTypes(SalesRepCommission, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("e7d499e6-9a91-41ea-837c-ede30bb19333"), new Guid("f8469819-f8fc-4762-a3fc-0117bb186269"), new Guid("51481c72-d94f-469f-a22e-57dbe9c1c8ca")).WithObjectTypes(SalesRepCommission, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("ea8de270-d03a-44cf-9b97-c233e1615d9e"), new Guid("7c2854ac-f4db-41e5-8b2f-7a9e9b376dff"), new Guid("bd39c031-3391-4b20-a57f-86c6e4c5b130")).WithObjectTypes(SalesRepCommission, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f6d1ce52-7d8e-42e2-a76e-acc35e2b5dd8"), new Guid("743000d8-a71b-4638-9b3e-a5be4e45fd29"), new Guid("7e4d3cd9-4066-4186-a311-b1cd5155d632")).WithObjectTypes(SalesRepCommission, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // CityBound
            new RelationTypeBuilder(domain, new Guid("7723a00d-8764-40e2-99a8-a790401689b5"), new Guid("bb222d51-4e32-4182-8c45-8ce6db2f2cea"), new Guid("4aa9efbb-fc9d-44f3-b713-3b1493637467")).WithObjectTypes(CityBound, City).WithSingularName("City")  .WithPluralName("Cities")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // Deduction
            new RelationTypeBuilder(domain, new Guid("0deb347e-22c7-4b48-b461-aa579e156398"), new Guid("aced036b-04ba-41da-a3fd-fb3d0782b8c6"), new Guid("b09c7a91-bcca-4f80-b68c-309fdf1e80b0")).WithObjectTypes(Deduction, DeductionType).WithSingularName("DeductionType")  .WithPluralName("DeductionTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("abaece2a-d56d-4af9-8421-1d587cd9dda2"), new Guid("b8d4b48b-292a-4348-8dba-15f89d573dd5"), new Guid("1077f672-905b-4198-ada5-e52fb34c986e")).WithObjectTypes(Deduction, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // CaseStatus
            new RelationTypeBuilder(domain, new Guid("28ef5fa2-7e2a-4ebb-b5a2-fe8cf7f18d04"), new Guid("9a1d40d3-0c58-4088-9a62-d7a35b787bf6"), new Guid("1acf2d5a-2631-48da-8e9a-222ff1293e83")).WithObjectTypes(CaseStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("332b3322-ef2e-4457-8503-045aa99061c9"), new Guid("76b6d6d5-e406-43aa-be3f-90a685a3f8dc"), new Guid("8fbd70e2-fc8c-4584-ac6c-82bc432f9326")).WithObjectTypes(CaseStatus, CaseObjectState).WithSingularName("CaseObjectState")  .WithPluralName("CaseObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // DiscountComponent
            new RelationTypeBuilder(domain, new Guid("1101cd39-852b-4eac-8649-de1a3f080703"), new Guid("ff284a40-cfa1-4b5b-90ec-c42b4dc35ef5"), new Guid("88c08616-c1e6-4c53-b1e8-74fa33bc310d")).WithObjectTypes(DiscountComponent, allorsDecimal).WithSingularName("Percentage")  .WithPluralName("Percentages")      .WithPrecision(19).WithScale(2).Build();
			
            // PartSubstitute
            new RelationTypeBuilder(domain, new Guid("23f8fda9-9109-4826-988f-74e115a430f4"), new Guid("9f43aa6a-68d0-44f9-aecb-977b6f0d66ea"), new Guid("1a0b6784-dcbe-4f86-acb1-1a0408f00465")).WithObjectTypes(PartSubstitute, Part).WithSingularName("SubstitutionPart")  .WithPluralName("SubstitutionParts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("510f8f4c-ff09-4d32-8c1c-e905dbbd684b"), new Guid("25d0c7ec-767e-4509-9164-67dbec0d66f4"), new Guid("d2c04285-03ab-4391-9338-d158630793b0")).WithObjectTypes(PartSubstitute, Ordinal).WithSingularName("Preference")  .WithPluralName("Preferences")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9cd198eb-2c25-425e-a23b-c321938f2512"), new Guid("8f8c0254-8bb0-4e61-83b5-38b0b80d0b97"), new Guid("6939df10-1c96-4a64-aae4-201392e9fd59")).WithObjectTypes(PartSubstitute, allorsDateTime).WithSingularName("FromDate")  .WithPluralName("FromDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("ccb0a290-b3f4-4e55-b52c-67ca70d67439"), new Guid("0d0e7982-f7cb-4c6b-bff7-e59f81296d6b"), new Guid("ab2549fe-dd9a-45dd-a9b0-7e3ff1f6a68f")).WithObjectTypes(PartSubstitute, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("e7d4ae25-175a-4e2a-88c2-9d8d5a468d1a"), new Guid("4986253b-2d85-45d1-8809-dcaab09e22f4"), new Guid("b85ed877-cd74-40cd-b2cf-aee6b24c3eeb")).WithObjectTypes(PartSubstitute, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // GoodOrderItem
            new RelationTypeBuilder(domain, new Guid("de65b7a6-b2b3-4d77-9cb4-94720adb43f0"), new Guid("3ed4dffc-09eb-4285-a31c-ba3af0666451"), new Guid("2f1173ef-1723-4ee5-9ff3-a01b6216584a")).WithObjectTypes(GoodOrderItem, allorsDecimal).WithSingularName("Price")  .WithPluralName("Prices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("f7399ebd-64f0-4bfa-a063-e75389d6a7cc"), new Guid("30b12a84-e2cc-4d24-aca3-71568961f9ee"), new Guid("bf1eeede-db39-4996-a2da-b3da503c2415")).WithObjectTypes(GoodOrderItem, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
			
            // VolumeUsage
            new RelationTypeBuilder(domain, new Guid("52e7e94c-3df5-46b5-97f7-27977fc82940"), new Guid("9b4f98c0-206b-4324-8f58-9adacead03c8"), new Guid("9c2c4c4e-ed7c-467f-8a35-65beee383a9d")).WithObjectTypes(VolumeUsage, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("db33fa70-1a64-4f4a-97a8-ee1103b44e62"), new Guid("2f3b8c14-8eb0-41d5-9fc8-76d29c81d329"), new Guid("03a0c297-8d28-475e-88b7-ffad88d852e8")).WithObjectTypes(VolumeUsage, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // TransferStatus
            new RelationTypeBuilder(domain, new Guid("05a4a5a6-cdaf-4ec8-9a34-cbd40753789b"), new Guid("a259de23-8d18-4d51-81e3-42796a144b5b"), new Guid("b3fd264c-91a5-425b-b9a0-48eb5cc765fd")).WithObjectTypes(TransferStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("a08cde84-30e0-4f99-b6b5-35b45c3fa2b8"), new Guid("0fb9e813-bd7d-40c8-a1c2-10a569e873c8"), new Guid("63627877-78be-4ffc-aa0d-740049add137")).WithObjectTypes(TransferStatus, TransferObjectState).WithSingularName("TransferObjectState")  .WithPluralName("TransferObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // State
            new RelationTypeBuilder(domain, new Guid("35ee6ba1-e75f-43f4-b33e-593748b5e359"), new Guid("040f516a-f173-44ba-b12c-a768e3216aec"), new Guid("250129ac-caf9-486a-ae89-f47634738376")).WithObjectTypes(State, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
			
            // JournalEntryNumber
            new RelationTypeBuilder(domain, new Guid("8fd6ce7a-0b08-4af4-9b7f-05a7e12445ed"), new Guid("0d39f242-de6a-4192-88e7-a78e5ddfcdb1"), new Guid("f5564eaa-202c-43c2-9dda-2e1500f0606d")).WithObjectTypes(JournalEntryNumber, JournalType).WithSingularName("JournalType")  .WithPluralName("JournalTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("99719445-24e6-445e-8ce1-60c0b5911723"), new Guid("4d278d9b-a817-4311-ba52-d1bd14db8cc2"), new Guid("2d669167-ac38-4dd1-a846-ba0f1b724bd2")).WithObjectTypes(JournalEntryNumber, allorsLong).WithSingularName("Number")  .WithPluralName("Number")      .Build();
            new RelationTypeBuilder(domain, new Guid("a47d5af5-21a8-4d4f-a2be-956ae7da8819"), new Guid("fd990275-4217-46fd-9f2d-e7af28ff5598"), new Guid("863f988b-ffab-4896-bd0a-02daaabc6fc0")).WithObjectTypes(JournalEntryNumber, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
			
            // OrderAdjustment
            new RelationTypeBuilder(domain, new Guid("4e7cbdda-9f19-44dd-bbef-6cab5d92a8a3"), new Guid("5ccd492c-cf29-468b-b99d-126a9573e573"), new Guid("7388d1a3-f24a-4c41-b57c-938160b3d1a6")).WithObjectTypes(OrderAdjustment, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("78d6de86-0f4d-4d8e-a9a6-4730668fa754"), new Guid("51d96df2-1e92-4ea2-8ec7-e918d5781ae7"), new Guid("933a70e0-0fa0-42cd-a4d5-b3eb10b57802")).WithObjectTypes(OrderAdjustment, VatRate).WithSingularName("VatRate")  .WithPluralName("VatRates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bc1ad594-88b6-4176-994c-a52be672f06d"), new Guid("ebc960bf-dd8c-4854-afec-185b260315e9"), new Guid("9d2f66e2-0bbd-46ab-b65b-43e6b38383b9")).WithObjectTypes(OrderAdjustment, allorsDecimal).WithSingularName("Percentage")  .WithPluralName("Percentages")      .WithPrecision(19).WithScale(2).Build();
			
            // EngineeringChange
            new RelationTypeBuilder(domain, new Guid("1a5edba2-6fda-4eb1-9e37-7a0e368ccff0"), new Guid("1858c16c-47e0-4707-ba58-acd34378d25e"), new Guid("3cdaec27-9203-4ed3-8b9d-a4995db9210d")).WithObjectTypes(EngineeringChange, Person).WithSingularName("Requestor")  .WithPluralName("Requestors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1b65b18b-c930-49b4-85e4-bb4b07dfdca2"), new Guid("a34d8a88-50c9-4ece-920c-a1d95388b5ab"), new Guid("5aa1e795-726a-4459-9c1a-e4efb82e807f")).WithObjectTypes(EngineeringChange, Person).WithSingularName("Authorizer")  .WithPluralName("Authorizers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4487e364-4c5e-4b84-8847-a6ec1f1a0e6f"), new Guid("79d6a20e-6bc9-49a4-bc81-c10c73871076"), new Guid("85ad4eb9-58e5-422b-a14d-767c7a07414d")).WithObjectTypes(EngineeringChange, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("4662a33f-24e2-48b1-a3a3-8c288ff3f523"), new Guid("93d71cc4-593a-4b04-bb78-1461b04aa9a0"), new Guid("ead6bb80-05d1-4552-93e9-d6278409629f")).WithObjectTypes(EngineeringChange, EngineeringChangeObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8d123834-364e-47d7-9d1e-63f4ef19f8c0"), new Guid("b42a9f7c-5032-44e6-97ed-ac4d1ff48445"), new Guid("b3943779-7867-4d29-b562-f67aeb595512")).WithObjectTypes(EngineeringChange, Person).WithSingularName("Designer")  .WithPluralName("Designers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9caba64b-4959-43f9-a6a6-c76dff62dc02"), new Guid("4709e3c9-c5cc-457c-a6ff-5eb981b3ef2e"), new Guid("9b113390-7966-4078-9651-e2c80143cee5")).WithObjectTypes(EngineeringChange, PartSpecification).WithSingularName("PartSpecification")  .WithPluralName("PartSpecifications")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b076cdcc-7e3f-46c8-b127-98d29a4c9e4e"), new Guid("d0506d24-4cab-4030-be18-59dd879b4bef"), new Guid("a385c549-0dcf-4ed9-b6c5-f264bba435a9")).WithObjectTypes(EngineeringChange, PartBillOfMaterial).WithSingularName("PartBillOfMaterial")  .WithPluralName("PartBillOfMaterials")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c360a1d9-5d8c-4295-aaae-2d50410dd293"), new Guid("2e3c4504-2130-45dd-b9bf-4e50abb021c0"), new Guid("91e1016b-0724-420f-9a6f-00294e61314a")).WithObjectTypes(EngineeringChange, EngineeringChangeObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("caf244e2-f61d-436e-978c-1d0af118949f"), new Guid("a77aa2de-44a6-4ee1-aa13-45cf8c4da853"), new Guid("f94b94ed-6f33-43c2-b7e5-241823e59a4f")).WithObjectTypes(EngineeringChange, EngineeringChangeStatus).WithSingularName("EngineeringChangeStatus")  .WithPluralName("EngineeringChangeStatuses")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d18955d3-1fce-46c9-bb44-5830bfdc09fd"), new Guid("078d9017-3d7a-4ba5-9c9b-58f778893a15"), new Guid("11d5ba0d-8c70-40a3-873d-ab27e1b8e4bf")).WithObjectTypes(EngineeringChange, Person).WithSingularName("Tester")  .WithPluralName("Testers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f56d7ad0-430d-482d-a298-5c41ffb082b4"), new Guid("30ec7448-b167-4273-bb00-cb87a604bb52"), new Guid("013964b5-022e-4b39-89ba-1cfb466fc3ff")).WithObjectTypes(EngineeringChange, EngineeringChangeStatus).WithSingularName("CurrentEngineeringChangeStatus")  .WithPluralName("CurrentEngineeringChangeStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // EmailTemplate
            new RelationTypeBuilder(domain, new Guid("21bbeaa8-f4cf-4b09-9fcd-af72a70e6f15"), new Guid("18d3ed19-fcac-4010-9bcb-2c0f6f41acc1"), new Guid("27ade42e-f19f-444a-9134-db74add756b3")).WithObjectTypes(EmailTemplate, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8bb431b6-a6ea-48d0-ad78-975ec26b470f"), new Guid("15e1b022-709b-4443-a85c-c1b2956c14e9"), new Guid("8ce6a6a6-2387-4dd7-8bea-dec068aec152")).WithObjectTypes(EmailTemplate, allorsString).WithSingularName("BodyTemplate")  .WithPluralName("BodyTemplates")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("f05fc608-5dcd-4d7d-b472-5b84c2a195a4"), new Guid("c00233a0-c9a2-4c01-88fc-9ea5eb7fd564"), new Guid("c39a94b3-455b-4602-8d55-abb2fca560ed")).WithObjectTypes(EmailTemplate, allorsString).WithSingularName("SubjectTemplate")  .WithPluralName("SubjectTemplates")      .WithSize(-1).Build();
			
            // InternalOrganisation
            new RelationTypeBuilder(domain, new Guid("00bf781c-c874-44fe-ae60-d6609075b1c0"), new Guid("3b99af1e-e6c3-498b-aabd-78d6e82c8819"), new Guid("b9a508e2-2931-4ddc-ab34-947d19c2d742")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("PurchaseOrderNumberPrefix")  .WithPluralName("PurchaseOrderNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("01d4f5d8-da57-4524-b35f-69a1a4adfa1c"), new Guid("84ff4f9a-b1d3-4e2c-aff6-52a9f75e874a"), new Guid("ee1b0251-ba57-490a-bdc9-c4e8fd6142ce")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("TransactionReferenceNumber")  .WithPluralName("TransactionReferenceNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("03aed479-8035-4747-9af6-266496b12e27"), new Guid("873eb507-a116-47ab-815d-fd7b1b14c8b9"), new Guid("90b1c344-94b9-4f47-ae56-9139a58f346e")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("NextPurchaseInvoiceNumber")  .WithPluralName("NextPurchaseInvoiceNumbers")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("08816f1e-752c-48b0-970b-4897eb15dc53"), new Guid("2b60a9e6-d5d3-458c-8454-e2341c36494b"), new Guid("cd14d576-2d7d-4b64-bc5b-4adc41dd8ec2")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("NextQuoteNumber")  .WithPluralName("NextQuoteNumbers")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("0994b73e-8d4c-4fa4-aca2-287449b22ca7"), new Guid("17a9138e-76c8-42e1-85b8-7af73b551a22"), new Guid("09fbb64d-c32e-4734-8df9-6e741a5070a5")).WithObjectTypes(InternalOrganisation, JournalEntryNumber).WithSingularName("JournalEntryNumber")  .WithPluralName("JournalEntryNumbers")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1a2533cb-9b75-4597-83ab-9bbfc49e0103"), new Guid("b4cb12ba-0ea1-41e5-945a-030503bf2c7b"), new Guid("031ba0aa-32e4-470c-9a79-fae65cace2f2")).WithObjectTypes(InternalOrganisation, Country).WithSingularName("EuListingState")  .WithPluralName("EuListingState")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("219a1d97-9615-47c5-bc4d-20a7d37313bd"), new Guid("4fd6a9e2-174c-41db-b519-44c317de0f96"), new Guid("45f4d069-5faf-45cf-b097-21f58fab4097")).WithObjectTypes(InternalOrganisation, AccountingPeriod).WithSingularName("ActualAccountingPeriod")  .WithPluralName("ActualAccountingPeriods")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("23aee857-9cea-481c-a4a3-72dd8b808d71"), new Guid("61c64e66-4647-439d-9efa-28500319e8ca"), new Guid("4d68b2fe-d5b4-45f8-9e68-377d75f3401d")).WithObjectTypes(InternalOrganisation, InvoiceSequence).WithSingularName("InvoiceSequence")  .WithPluralName("InvoiceSequences")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("293758d7-cc0a-4f1c-b122-84f609a828c2"), new Guid("afe9d80b-2984-4e46-b45c-e5a25af3bccd"), new Guid("97aa32a6-69c0-4cc5-9e42-98907ff6c45f")).WithObjectTypes(InternalOrganisation, PaymentMethod).WithSingularName("ActivePaymentMethod")  .WithPluralName("ActivePaymentMethods")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("33a47048-277a-40e8-bfe0-c8090eb179b2"), new Guid("81bc7c99-b328-4b81-9391-fe7659146924"), new Guid("403008e6-8c8a-437e-8b09-02573198e319")).WithObjectTypes(InternalOrganisation, StringTemplate).WithSingularName("PurchaseShipmentTemplate")  .WithPluralName("PurchaseShipmentTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("37b4bf2c-5b09-42b0-84d9-59b57793cf37"), new Guid("22aff7e0-1b45-4f06-b281-19cbf0d1c511"), new Guid("dcd31b64-e449-4833-91cb-8237bdb71b78")).WithObjectTypes(InternalOrganisation, allorsDecimal).WithSingularName("MaximumAllowedPaymentDifference")  .WithPluralName("MaximumAllowedPaymentDifferences")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("39a09487-dcf4-4bc8-8494-859d7a8cc3dd"), new Guid("da98358b-fb19-4b40-ad32-ffc0b48583fe"), new Guid("9b20db3b-ac0e-4374-8262-3ee22f8067ee")).WithObjectTypes(InternalOrganisation, Media).WithSingularName("LogoImage")  .WithPluralName("LogoImages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3b32c442-9cbc-41d8-8eb2-2ae41beca2c4"), new Guid("eda352cd-00cc-4d04-99cf-f7ad667cb20a"), new Guid("b8211575-f4c9-44c8-89b2-bd122704f098")).WithObjectTypes(InternalOrganisation, CostCenterSplitMethod).WithSingularName("CostCenterSplitMethod")  .WithPluralName("CostCenterSplitMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("496f6d33-2259-442e-924f-636d73cec52f"), new Guid("4ac05596-41d8-402a-8308-d9f458d604e0"), new Guid("0739dac1-6507-4306-95c9-78f10532a78e")).WithObjectTypes(InternalOrganisation, LegalForm).WithSingularName("LegalForm")  .WithPluralName("LegalForms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("49b087e2-9f55-463e-8e77-2500149ad771"), new Guid("1da60a2b-5359-4688-b925-edee515a2427"), new Guid("a0e7966c-1096-4491-a712-1dc38b58b67c")).WithObjectTypes(InternalOrganisation, AccountingPeriod).WithSingularName("AccountingPeriod")  .WithPluralName("AccountingPeriods")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4fc741ef-fe95-49a8-8bcd-8ff43092db88"), new Guid("0e58cd98-94e6-42ab-8501-394f8b8d3624"), new Guid("d5096d5d-a443-4667-95dd-7184f348e55c")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("SalesPaymentDifferencesAccount")  .WithPluralName("SalesPaymentDifferencesAccounts")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("538fc59e-42da-471a-96a4-d8a93b2de229"), new Guid("d0f6dc1f-a056-4a8c-b138-d68a5cf10247"), new Guid("948f1ebc-a637-4c1b-ae35-dcc7462c95d0")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5732ad66-024d-4207-8ce6-47e90542f12a"), new Guid("88d8e49c-e756-4211-8d35-d5c982119d69"), new Guid("aa4ebf71-6459-42c1-adf0-8fd5e74093ec")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("NextPurchaseOrderNumber")  .WithPluralName("NextPurchaseOrderNumbers")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("5b64038f-5ad9-46a6-9af6-b95819ac9830"), new Guid("753f6fa9-ff10-402c-9812-d2c738d35dbb"), new Guid("8411c910-14d4-4629-aa39-c58602a799d4")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("PurchaseTransactionReferenceNumber")  .WithPluralName("PurchaseTransactionReferenceNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5b64cf9d-e990-491e-b009-3481d73db67e"), new Guid("40296302-a559-4014-ba68-929d4238f4d8"), new Guid("5b966f3d-cc80-44f4-b255-87182aa796d4")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("FiscalYearStartMonth")  .WithPluralName("FiscalYearStartMonths")      .Build();
            new RelationTypeBuilder(domain, new Guid("5ca9eda8-278b-4466-99f3-8c61d0383ef4"), new Guid("5dd8f968-1ebf-4b94-be2d-11005d24aeb5"), new Guid("9e72477e-4729-4371-8f6b-80ff618705f7")).WithObjectTypes(InternalOrganisation, StringTemplate).WithSingularName("PurchaseOrderTemplate")  .WithPluralName("PurchaseOrderTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6389a192-9c78-416d-892d-c7c430c0a6ec"), new Guid("9b6baec6-0f85-4c7a-b80d-becf925322b9"), new Guid("30465967-841c-4bbf-90ca-5bcaab4b18e2")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("NextIncomingShipmentNumber")  .WithPluralName("NextIncomingShipmentNumbers")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("63c9ceb1-d583-41e1-a9a9-0c2576e9adfc"), new Guid("fd1ac2ff-6869-44bd-9b4e-05a3b14fbad9"), new Guid("41058049-5c44-47b7-bbb9-34ab0bdcfbcb")).WithObjectTypes(InternalOrganisation, CostOfGoodsSoldMethod).WithSingularName("CostOfGoodsSoldMethod")  .WithPluralName("CostOfGoodsSoldMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6d10bb5d-babe-40fc-be6b-9adba27bbe71"), new Guid("00321c87-f4d8-4c1c-8028-f5764ade308f"), new Guid("d44f70e9-4558-4836-ac55-abfe17b8eab7")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("NextSubAccountNumber")  .WithPluralName("NextSubAccountNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("76acc9c6-0aa1-4b30-8cca-4629fdd56b91"), new Guid("b9f585f4-a612-451a-ac0e-4a2584982385"), new Guid("e9e6013f-9458-4d27-89a2-bced57a2b15f")).WithObjectTypes(InternalOrganisation, Role).WithSingularName("EmployeeRole")  .WithPluralName("EmployeeRoles")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("77ae5145-791a-4ef0-94cc-6c9683b02f13"), new Guid("0da6cc82-b538-4346-bb48-19b02223a566"), new Guid("bef210d2-7af6-4653-872b-a5eebba2af87")).WithObjectTypes(InternalOrganisation, allorsBoolean).WithSingularName("VatDeactivated")  .WithPluralName("VatsDeactivated")      .Build();
            new RelationTypeBuilder(domain, new Guid("7e210c5e-a68b-4ea0-b019-1dd452d8e407"), new Guid("b9c41192-1666-44c0-9365-b24df29a2cdf"), new Guid("dfe3042c-babb-416b-a414-5dda0a2958c0")).WithObjectTypes(InternalOrganisation, allorsInteger).WithSingularName("FiscalYearStartDay")  .WithPluralName("FiscalYearStartDays")      .Build();
            new RelationTypeBuilder(domain, new Guid("848f3098-ce8b-400c-9775-85c00ac68f28"), new Guid("44649646-ccf8-48b7-9ebf-a09df75d23fc"), new Guid("3d5a74b3-cb62-4803-8115-fde43a648af5")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("89f4907d-4a10-428d-9e6b-ef9fb045c019"), new Guid("a0fd3167-5d4e-400f-b593-1497fce5d024"), new Guid("0ab03957-8140-4e1c-8a13-a9647f6c9e47")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("RetainedEarningsAccount")  .WithPluralName("RetainedEarningsAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9306706d-c35c-4e7d-af31-60ff13c348bd"), new Guid("7299e411-7fd5-4500-8e25-a44f0339806a"), new Guid("1975353f-30b6-4c4a-ab41-94886c8f7f5a")).WithObjectTypes(InternalOrganisation, StringTemplate).WithSingularName("PackagingSlipTemplate")  .WithPluralName("PackagingSlipTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9a2ab89e-c3bc-4b6b-a82d-417dc21c8f9e"), new Guid("fcc6e653-3787-44a0-8a3a-35e80e232a02"), new Guid("31549cf9-6418-4d19-96b0-5813cc964491")).WithObjectTypes(InternalOrganisation, Party).WithSingularName("Customer")  .WithPluralName("Customers")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9d6aaa81-9f97-427e-9f46-1f1e93748248"), new Guid("b251ce10-b4ac-45da-bc57-0a42e75f3660"), new Guid("6ffd36cd-37de-444b-93b6-4128de34254f")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("PurchaseInvoiceNumberPrefix")  .WithPluralName("PurchaseInvoiceNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a8d09b7d-5f55-4adb-897b-73b2c107932f"), new Guid("82e7ff09-f69c-4f22-97ad-80cae5730f19"), new Guid("9309b4cb-0552-4f9a-bf50-77bb686d8b8e")).WithObjectTypes(InternalOrganisation, StringTemplate).WithSingularName("PickListTemplate")  .WithPluralName("PickListTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ab2004c1-fd91-4298-87cd-532a6fe5efb0"), new Guid("b53b80a4-92f5-4e08-a9fb-86bf2bd9572e"), new Guid("7fe218f4-975d-40f4-82b6-b43cb308aff4")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("SalesPaymentDiscountDifferencesAccount")  .WithPluralName("SalesPaymentDiscountDifferencesAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("afbaffe6-b03c-463e-b074-08b32641b482"), new Guid("4a02dff3-4bd3-4453-b2fa-e6e79f1b18b0"), new Guid("334cb83a-410e-4abb-8b51-9dd19e2fc21b")).WithObjectTypes(InternalOrganisation, AccountingTransactionNumber).WithSingularName("AccountingTransactionNumber")  .WithPluralName("AccountingTransactionNumbers")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b79d1af6-f14a-4466-aba2-893bf554dbc1"), new Guid("e24f08c4-a880-42f7-907d-d844d48fa152"), new Guid("78650a41-e592-4889-8c62-2f16a2e7fa01")).WithObjectTypes(InternalOrganisation, StringTemplate).WithSingularName("QuoteTemplate")  .WithPluralName("QuoteTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b8af8dce-d0e8-4e16-8d72-e56b920a04b4"), new Guid("57168fae-0b31-4370-afff-ab5a02c9a8ee"), new Guid("d4c44bcf-c38b-46b9-86f6-462a48714389")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("TransactionReferenceNumberPrefix")  .WithPluralName("TransactionReferenceNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("bce0d7d9-cfc9-4092-99a1-93ff5c0b94dd"), new Guid("5c4cfec8-5bc0-48d2-ac92-132c0538e614"), new Guid("bece103b-dde6-4319-938b-e08d23d9f99e")).WithObjectTypes(InternalOrganisation, Currency).WithSingularName("PreviousCurrency")  .WithPluralName("PreviousCurrencies")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d0ebaa65-260a-4511-a137-89f25016f12c"), new Guid("70e0b1a9-4c82-4c47-a6b8-df7a62423a08"), new Guid("1413c3f9-a226-4356-8bcc-6f8ae1963a6e")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("PurchasePaymentDifferencesAccount")  .WithPluralName("PurchasePaymentDifferencesAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d2ad57d5-de30-4bc0-90a7-9aea7a9da8c7"), new Guid("dbd28e59-d5d6-4d95-aef1-6881e0fe2d48"), new Guid("2e28606f-5708-4c0f-bdfa-04019a0e97d9")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("SuspenceAccount")  .WithPluralName("SuspenceAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d48ef8bb-064b-4360-8162-a138fb601761"), new Guid("91252cf4-eca3-4b9d-b06d-5df795c3709c"), new Guid("3ef1f904-5b02-4f00-8486-2e452668be67")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("NetIncomeAccount")  .WithPluralName("NetIncomeAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d5645df8-2b10-435d-8e47-57b5d268541a"), new Guid("4d145acb-007a-46b9-98a8-a86888221e28"), new Guid("c2fe67e4-a100-4d96-b4ff-df1ec73db5fe")).WithObjectTypes(InternalOrganisation, allorsBoolean).WithSingularName("DoAccounting")  .WithPluralName("DosAccounting")      .Build();
            new RelationTypeBuilder(domain, new Guid("dcf24d2f-7bf2-43fd-82b4-bd30fd545022"), new Guid("b4b8b2e6-141c-416f-89a8-746c72c26e5c"), new Guid("dc7127c4-c4ca-49a3-95ae-970de554d4f3")).WithObjectTypes(InternalOrganisation, Facility).WithSingularName("DefaultFacility")  .WithPluralName("DefaultFacilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dd008dfe-a219-42ab-bc08-d091da3f8ea4"), new Guid("77ce8418-a00b-46d0-ab7b-4a782b7387da"), new Guid("a5ac9bc1-5323-41ec-a791-d4ecc7d0eee8")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("PurchasePaymentDiscountDifferencesAccount")  .WithPluralName("PurchasePaymentDiscountDifferencesAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e09976e8-dc99-4539-9b0b-0bbe98cc5404"), new Guid("0d828c12-82bd-4b37-96c8-68997a7c2f48"), new Guid("3d362cd1-d49c-422f-9722-7276a6ee07c4")).WithObjectTypes(InternalOrganisation, Party).WithSingularName("Supplier")  .WithPluralName("Suppliers")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e617fcf1-63fb-4333-aa79-7a8ac4d267e4"), new Guid("34c6c91f-3bb2-4821-a9cf-49a7d8959e9b"), new Guid("2f6236fc-4f59-4694-acd1-4ae339898bc3")).WithObjectTypes(InternalOrganisation, allorsLong).WithSingularName("NextAccountingTransactionNumber")  .WithPluralName("NextAccountingTransactionNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("e9af1ca5-d24f-4af2-8687-833744941b24"), new Guid("ca041d3d-7adf-43da-ac42-c749633bd9b8"), new Guid("4b50a77e-4343-415b-ad45-6cd074b681b5")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("QuoteNumberPrefix")  .WithPluralName("QuoteNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ec8e7400-0088-4237-af32-a687e1c45d77"), new Guid("3ee2e0b2-6835-490b-937a-a853c85dd3e4"), new Guid("c70fd771-87a6-4e97-98ea-34c2c0265450")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("PurchaseTransactionReferenceNumberPrefix")  .WithPluralName("PurchaseTransactionReferenceNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f14f1865-7820-4ed5-8ca9-dffcbeb6b1ec"), new Guid("4b8a7531-3996-4af2-a921-3b5653dc46ba"), new Guid("0b574be0-2369-41df-9f64-71235c0b9e9a")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("TaxNumber")  .WithPluralName("TaxNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f353e7ef-d24d-4a27-8ec9-e930ef936240"), new Guid("cf556dfd-4b43-48e4-8243-df2650d8ce97"), new Guid("6b7bda76-5fe7-4526-8c79-b42324fd4090")).WithObjectTypes(InternalOrganisation, GeneralLedgerAccount).WithSingularName("CalculationDifferencesAccount")  .WithPluralName("CalculationDifferencesAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f7ad4cfe-fc31-412c-8df7-2a514783e2ed"), new Guid("4145ddcd-726a-405f-b4cb-2e85b0bd60a2"), new Guid("7940e9e6-e6f6-4eac-bfa4-e14f63315276")).WithObjectTypes(InternalOrganisation, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fe96e14b-9dbd-4497-935f-f605abd2ada7"), new Guid("68f9e5fd-7398-456b-bcb8-27b23dbce3f1"), new Guid("3fc3517d-0d36-4401-a56f-ac3a83f1f892")).WithObjectTypes(InternalOrganisation, allorsString).WithSingularName("IncomingShipmentNumberPrefix")  .WithPluralName("IncomingShipmentNumberPrefixes")      .WithSize(256).Build();
			
            // EstimatedProductCost
            new RelationTypeBuilder(domain, new Guid("2a8f919f-19f0-4b33-b8b8-26937d49d298"), new Guid("6d46215f-6af1-49b9-bc27-41de412a5b43"), new Guid("0d0adab4-db9a-492b-8aaf-e40b864705aa")).WithObjectTypes(EstimatedProductCost, allorsDecimal).WithSingularName("Cost")  .WithPluralName("Costs")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("78a7ee9c-4aeb-471d-ae17-5878737f1f67"), new Guid("d4e26be2-9adc-4ded-b373-e88c7ecd7e29"), new Guid("51bb9283-5e98-4a69-ae20-85460ee532d7")).WithObjectTypes(EstimatedProductCost, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ce0f4392-cf76-49ba-a6bd-47b4e125ec61"), new Guid("acc9ae9a-8cb4-46cc-a507-db82759435d8"), new Guid("5ebf8530-9a22-43d0-a1db-d976dfcbeaea")).WithObjectTypes(EstimatedProductCost, Organisation).WithSingularName("Organisation")  .WithPluralName("Organisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d5e63839-7009-4582-8d9a-ac9591aa10c9"), new Guid("bfc2363f-b9ef-43ba-b5de-83104b9492ba"), new Guid("31982d33-6240-4718-b9db-6762adb85670")).WithObjectTypes(EstimatedProductCost, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e7942246-0343-437e-9b92-fc2d5e6438fd"), new Guid("434c6b12-146d-4f53-b1a3-5b75afaf57f2"), new Guid("c763f72b-aa80-4caa-91b0-eddb949d3d34")).WithObjectTypes(EstimatedProductCost, GeographicBoundary).WithSingularName("GeographicBoundary")  .WithPluralName("GeographicBoundaries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // OwnBankAccount
            new RelationTypeBuilder(domain, new Guid("d83ca1e3-4137-4e92-a61d-0b8a1b8f7085"), new Guid("8a492054-a6be-4824-a0d2-daeed69c091b"), new Guid("c90ac5e5-2368-45d1-bc4a-0621c30f20e5")).WithObjectTypes(OwnBankAccount, BankAccount).WithSingularName("BankAccount")  .WithPluralName("BankAccounts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // DeploymentUsage
            new RelationTypeBuilder(domain, new Guid("50c6bc05-83ff-4d40-b476-51418355eb0c"), new Guid("e8aa74ab-d70a-43f4-9cac-de0160e3f257"), new Guid("cc27af60-5ddd-4cce-bcc1-d68b3d5c6ab4")).WithObjectTypes(DeploymentUsage, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartyContactMechanism
            new RelationTypeBuilder(domain, new Guid("2ca2f403-67f8-49e6-9a62-4547d2cc83a1"), new Guid("b4dea5e8-2fa0-49a4-aed3-6bc32aade7e6"), new Guid("6d98949f-823f-4a66-92c2-8182156efef9")).WithObjectTypes(PartyContactMechanism, ContactMechanismPurpose).WithSingularName("ContactPurpose")  .WithPluralName("ContactPurposes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("afd94e13-db8e-45cd-8d6c-d9085054d71f"), new Guid("55fa72b2-2d47-442b-90a8-03537771df30"), new Guid("d435dd59-d047-4952-bd96-f644d226e975")).WithObjectTypes(PartyContactMechanism, ContactMechanism).WithSingularName("ContactMechanism")  .WithPluralName("ContactMechanisms")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("eb412c34-7127-4b37-8831-5280b9ed1885"), new Guid("d24adc95-f792-4caa-b6f4-de6a0caa8114"), new Guid("a9168214-b208-4a21-905c-da53f9a4619d")).WithObjectTypes(PartyContactMechanism, allorsBoolean).WithSingularName("UseAsDefault")  .WithPluralName("UseAsDefaults")      .Build();
            new RelationTypeBuilder(domain, new Guid("f859fd15-4359-4de1-9927-75b6e443ffab"), new Guid("0935e3ed-7141-47b2-b4cc-72274b9e7680"), new Guid("1c57da10-ffcb-4b97-a930-ae10c2059b98")).WithObjectTypes(PartyContactMechanism, allorsBoolean).WithSingularName("NonSolicitationIndicator")  .WithPluralName("NonSolicitationIndicators")      .Build();
			
            // QuoteTerm
            new RelationTypeBuilder(domain, new Guid("ce70acf3-9bc4-4572-9487-ef1ab900b488"), new Guid("df24f334-df05-48b2-95c8-dc69bafbdf06"), new Guid("c64eb6c1-0bf8-4504-8c35-e4753f050911")).WithObjectTypes(QuoteTerm, allorsString).WithSingularName("TermValue")  .WithPluralName("TermValues")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e53203f0-1d8f-45ea-bcc2-627c9440e66f"), new Guid("8319e551-dc5c-461e-bbf2-6c37b50becce"), new Guid("88fc03e5-6ab5-4b95-9027-282a595ca3f7")).WithObjectTypes(QuoteTerm, TermType).WithSingularName("TermType")  .WithPluralName("TermTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Transfer
            new RelationTypeBuilder(domain, new Guid("002399be-0fef-46ca-bb53-018287430a08"), new Guid("2ee2901c-eee5-48ad-a213-518d464d8ffd"), new Guid("e77f67f8-6be9-4425-a8ca-630dccb5ecc1")).WithObjectTypes(Transfer, TransferObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("01757aca-7f45-4061-8721-1fa3d8cca852"), new Guid("b0b86e04-cd64-4a19-94dd-86ba558b478b"), new Guid("d775ad19-df10-4941-b384-d0de7c3ed943")).WithObjectTypes(Transfer, TransferObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2fc36280-2378-4c2d-aab1-b2f038a5cfa5"), new Guid("731be3ab-46e5-4ff9-acc7-c7d106f32896"), new Guid("ea288e25-6d3c-4138-86fc-4e0fb86a088e")).WithObjectTypes(Transfer, TransferStatus).WithSingularName("CurrentShipmentStatus")  .WithPluralName("CurrentShipmentStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e415cf27-7ae7-48a7-a889-ad90a7384a68"), new Guid("b205e173-5355-4dcc-a615-521b46e3759a"), new Guid("96976d0f-10b8-4c67-a9a1-9b87b64eb46c")).WithObjectTypes(Transfer, TransferStatus).WithSingularName("ShipmentStatus")  .WithPluralName("ShipmentStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // Facility
            new RelationTypeBuilder(domain, new Guid("1a7f255a-3e94-41df-b71d-10ab36f38ffb"), new Guid("1341fb5d-26b6-4c07-bb31-a444c451c547"), new Guid("cd2ee41e-ffba-4a59-9b9c-0d3eb581420c")).WithObjectTypes(Facility, Facility).WithSingularName("MadeUpOf")  .WithPluralName("MadeUpOfs")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1daad895-cf57-4110-a4e0-117e0212c3e4"), new Guid("304a1b7b-215a-4fad-ab99-d0a974e8b0c0"), new Guid("5ab48116-f33a-484e-9e6e-05a912efc9d5")).WithObjectTypes(Facility, allorsDecimal).WithSingularName("SquareFootage")  .WithPluralName("SquareFootages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2df0999d-97cb-4c76-9f3e-076376e60e38"), new Guid("d29a1df5-e08f-4f7c-876c-a1ab737206a5"), new Guid("5dd1abff-5a4c-4d30-8c69-1bcc83e5460e")).WithObjectTypes(Facility, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("4b55ee38-64e9-4c11-a204-36e2f460c5f8"), new Guid("87db14ec-a82a-4107-bae1-8ea945a68bce"), new Guid("d576e2ee-dcf0-4f06-a496-42bceaf94399")).WithObjectTypes(Facility, ContactMechanism).WithSingularName("FacilityContactMechanism")  .WithPluralName("FacilityContactMechanisms")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b8f50794-848b-42be-9114-5eea579f5f71"), new Guid("7c9d689d-c38d-48b1-b1f0-5b211828ae8a"), new Guid("d9ee92cb-3131-4442-be42-269ae294378d")).WithObjectTypes(Facility, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c73693db-9eae-4d81-a801-2ef4d619544b"), new Guid("e72fd5d0-f80f-4ecc-a6f7-a0f697c91e0b"), new Guid("61dedec6-5aa1-4717-9b7b-34b77a1b31b9")).WithObjectTypes(Facility, InternalOrganisation).WithSingularName("Owner")  .WithPluralName("Owners")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // RevenueQuantityBreak
            new RelationTypeBuilder(domain, new Guid("7bca5fd8-016d-43d8-a38e-a811f3bd77ab"), new Guid("f312afd5-a46c-4fb6-88a4-e945b30098da"), new Guid("087a6ce6-9a53-4ab1-8d9a-b8d00107c533")).WithObjectTypes(RevenueQuantityBreak, allorsDecimal).WithSingularName("Through")  .WithPluralName("Throughs")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("bae5ce9b-1bc0-46e4-89d9-26e8de81f54a"), new Guid("c1b72b95-f55f-4aaa-86a8-aba006489ec5"), new Guid("03cc5c67-caf6-4d1f-8a46-edd1c0f76fa9")).WithObjectTypes(RevenueQuantityBreak, allorsDecimal).WithSingularName("From")  .WithPluralName("Froms")      .WithPrecision(19).WithScale(2).Build();
			
            // GeneralLedgerAccountType
            new RelationTypeBuilder(domain, new Guid("e01a0752-531b-4ee3-a58e-711f377247e1"), new Guid("dcfb5761-0d99-4a8f-afc9-2c0e64cd1c68"), new Guid("7d579eae-a239-4f55-9719-02f39dbc42d8")).WithObjectTypes(GeneralLedgerAccountType, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // FaceToFaceCommunication
            new RelationTypeBuilder(domain, new Guid("2dfb4cd7-ed48-4b79-ba26-b430058356ef"), new Guid("b935b150-4172-46fb-ab1a-b7197ea71f18"), new Guid("e6f4591e-ee9a-4fe8-b908-ee4e18084aa8")).WithObjectTypes(FaceToFaceCommunication, PostalAddress).WithSingularName("Location")  .WithPluralName("Location")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("52b8614b-799e-4aea-a012-ea8dbc23f8dd"), new Guid("ac424847-d426-4614-99a2-37c70841c454"), new Guid("bcf4a8df-8b57-4b3c-a6e5-f9b56c71a13b")).WithObjectTypes(FaceToFaceCommunication, Person).WithSingularName("Participant")  .WithPluralName("Participants")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
			
            // BudgetReview
            new RelationTypeBuilder(domain, new Guid("4396be4d-edb4-405d-a39a-ee6ff5c39ca5"), new Guid("9cbcaf98-22d1-41ed-b7d4-88a32e41de5f"), new Guid("61c422a4-cfb0-4e7a-b8ee-29ecf92589ee")).WithObjectTypes(BudgetReview, allorsDateTime).WithSingularName("ReviewDate")  .WithPluralName("ReviewDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("6d065017-6c6f-413c-bc79-1a6349180c34"), new Guid("b0f12ce4-58e3-4757-996f-3e3aca8aafbb"), new Guid("eff0da0c-1ea3-40d8-8894-141d43f20a5f")).WithObjectTypes(BudgetReview, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // EngineeringChangeStatus
            new RelationTypeBuilder(domain, new Guid("0a6c34f7-b37b-4abc-b12e-05ef14a8d986"), new Guid("66d0b0a8-b39a-4654-9ce6-3b8a8e9bbf4a"), new Guid("765d186a-a5b5-4895-ae27-93bfe6ef98f2")).WithObjectTypes(EngineeringChangeStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("6a7695dc-4343-4645-b4f1-78348d6873c3"), new Guid("7a1f031f-29ca-4b1c-95c0-1bdc35856412"), new Guid("09cf51ab-77a3-4188-97ad-590b6bca6a97")).WithObjectTypes(EngineeringChangeStatus, EngineeringChangeObjectState).WithSingularName("EngineeringChangeObjectState")  .WithPluralName("EngineeringChangeObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // PartBillOfMaterial
            new RelationTypeBuilder(domain, new Guid("06c3a64a-ef2c-44a0-81ee-1335842cf844"), new Guid("738ee8fd-307a-4d12-a0fc-238640386eee"), new Guid("e0145603-3f58-46f5-8348-77ad4d211543")).WithObjectTypes(PartBillOfMaterial, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("24de2b73-c51b-47b5-bd80-2022c0e37841"), new Guid("a6dc16b1-6c02-4060-9f64-982d09ffe5dc"), new Guid("f3a70021-f5af-4493-b71c-74c65649a6c1")).WithObjectTypes(PartBillOfMaterial, allorsString).WithSingularName("Instruction")  .WithPluralName("Instructions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("ac18525c-57ef-4a11-a775-e27c397b334c"), new Guid("2a043325-46cb-4219-a580-e71efe6814b5"), new Guid("f526f282-0f15-4e98-af8f-9e8d658c4d38")).WithObjectTypes(PartBillOfMaterial, allorsInteger).WithSingularName("QuantityUsed")  .WithPluralName("QuantitiesUsed")      .Build();
            new RelationTypeBuilder(domain, new Guid("eb1b2313-df9b-407d-9cf9-617d58c6f4be"), new Guid("9d9c4b58-8144-4d64-92ca-b81abecc5f40"), new Guid("8f188307-b996-41eb-8811-462a3a4d436e")).WithObjectTypes(PartBillOfMaterial, Part).WithSingularName("ComponentPart")  .WithPluralName("ComponentParts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Journal
            new RelationTypeBuilder(domain, new Guid("01abf1e4-c2f8-4d04-8046-f5ac5428ff11"), new Guid("1ee04497-3585-4910-83c3-1bcdbe3c3bd2"), new Guid("82e88c20-35ac-4346-9881-157d305ed33b")).WithObjectTypes(Journal, allorsBoolean).WithSingularName("UseAsDefault")  .WithPluralName("UseAsDefaults")      .Build();
            new RelationTypeBuilder(domain, new Guid("04f786b4-66be-4616-9966-ac026384c0d3"), new Guid("a1ed1007-5f6a-4177-9847-45339c24331a"), new Guid("0a653d75-829b-4612-b260-ec470eea0221")).WithObjectTypes(Journal, OrganisationGlAccount).WithSingularName("GlPaymentInTransit")  .WithPluralName("GlPaymentsInTransit")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1ec79ec4-60a8-4fdc-b11e-8c25697cd457"), new Guid("cfda1134-8fdf-449c-99cb-1e9ad29448fc"), new Guid("511439d3-de78-4b3b-8489-9fd661c41fdc")).WithObjectTypes(Journal, JournalType).WithSingularName("JournalType")  .WithPluralName("JournalTypes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("37493cfc-e817-4b89-b7cb-7d29f69cf41e"), new Guid("3a30aed2-9a0b-4eb2-948e-6ac033d2a5e0"), new Guid("13871d2d-ae7a-48be-99da-79711be267f8")).WithObjectTypes(Journal, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("3a52aa7c-fa01-4845-866c-976e48ea2179"), new Guid("8ab91bc9-99da-4bb4-b249-866a54fb4117"), new Guid("8bf5236d-6b99-4b86-b686-1cbf96bcde03")).WithObjectTypes(Journal, allorsBoolean).WithSingularName("BlockUnpaidTransactions")  .WithPluralName("BlockUnpaidTransactionss")      .Build();
            new RelationTypeBuilder(domain, new Guid("4f1b0471-67f9-4fa1-9b69-b5d9cbeda5e7"), new Guid("b18129bb-5cf8-4408-a4fb-b5782fe67684"), new Guid("bf1656e9-0124-4403-855e-da94157e293d")).WithObjectTypes(Journal, OrganisationGlAccount).WithSingularName("ContraAccount")  .WithPluralName("ContraAccounts")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("76e76063-ddce-4a8a-98fb-884cd6179c45"), new Guid("1395e70f-cd1c-457e-bfd8-c7f31a32c8f4"), new Guid("b6416161-0b77-4a30-b00e-59a0eaaa1e87")).WithObjectTypes(Journal, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("774f30df-26b4-41d5-9ecb-d1fd62244e1f"), new Guid("9a4078bb-350b-4fbb-8fc2-16e86928d32e"), new Guid("dfbac079-a8f6-4576-8d54-39bf76553e0c")).WithObjectTypes(Journal, JournalType).WithSingularName("PreviousJournalType")  .WithPluralName("PreviousJournalTypes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9aa48ebb-0ee0-4662-bfc5-f6b8ccb7a7c3"), new Guid("96d76f8b-5121-401f-bf2b-3f504494f4d7"), new Guid("e4d8e7b3-48be-4a9a-848e-c35dd3889715")).WithObjectTypes(Journal, OrganisationGlAccount).WithSingularName("PreviousContraAccount")  .WithPluralName("PreviousContraAccounts")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9b7c3687-b268-4c2b-8b04-c04a0c55d79f"), new Guid("47be858f-a462-4e23-acee-18aecdebc95e"), new Guid("f1907d6b-5015-4583-8d63-5c74f5954b97")).WithObjectTypes(Journal, JournalEntry).WithSingularName("JournalEntry")  .WithPluralName("JournalEntries")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dbdca15b-5337-44f1-b490-c69cb36df9c3"), new Guid("b4d7972b-4e79-4192-b862-c358ad10b48e"), new Guid("1ba97bd5-9644-47c6-b5e0-52207322cc38")).WithObjectTypes(Journal, allorsBoolean).WithSingularName("CloseWhenInBalance")  .WithPluralName("CloseWhenInBalances")      .Build();
			
            // ShipmentItem
            new RelationTypeBuilder(domain, new Guid("082e7e0d-190c-463f-89c8-af8e2c57c68d"), new Guid("cfbef516-6673-4496-ad91-54e772557ef5"), new Guid("7e235029-4dc3-46d2-878d-58a05e68c4e1")).WithObjectTypes(ShipmentItem, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("158f104b-fa5c-425e-8b55-ee4e866820ec"), new Guid("77f01592-48e6-486f-9217-7c9cfc477267"), new Guid("6dcd6646-e5fa-42c9-a54b-c95380e860a2")).WithObjectTypes(ShipmentItem, Part).WithSingularName("Part")  .WithPluralName("Parts")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("19c691ae-f849-451e-ac7e-ea84f4a9b51a"), new Guid("9a57f102-0b43-4f10-af75-c808c718c8b7"), new Guid("b18cc4e1-0be7-48d7-9e92-efc1e3a3edca")).WithObjectTypes(ShipmentItem, allorsString).WithSingularName("ContentsDescription")  .WithPluralName("ContentsDescriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6b3ab563-a19b-4d92-be3a-ddf3046d5b18"), new Guid("d41aeb48-bd41-40b2-bbc4-f4dd096a6c5f"), new Guid("e9a936df-2165-455e-8f9c-02b3dc5d7ebb")).WithObjectTypes(ShipmentItem, Document).WithSingularName("Document")  .WithPluralName("Documents")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("92cca1c2-4c7e-49a0-ba4b-88693b87c379"), new Guid("d7dca1d7-e678-4b52-9c25-d93f353ca25d"), new Guid("50dd2c62-6d8f-45bf-a77b-c8e1f7933ad3")).WithObjectTypes(ShipmentItem, allorsDecimal).WithSingularName("QuantityShipped")  .WithPluralName("QuantitiesShipped")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b5d35e87-f741-4600-9838-4419b127681d"), new Guid("797743d0-c0e9-4a75-9180-4e05eb55423f"), new Guid("d37d1290-af88-45c1-8e70-2774de0c58c2")).WithObjectTypes(ShipmentItem, ShipmentItem).WithSingularName("InResponseToShipmentItem")  .WithPluralName("InResponseToShipmentItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b8ca6fae-0866-4806-9ffd-64d5d2b978f9"), new Guid("2a9e81f6-6009-4706-a0d0-cd180cb825e6"), new Guid("31227051-0164-40e7-9e37-d1b31719e483")).WithObjectTypes(ShipmentItem, InventoryItem).WithSingularName("InventoryItem")  .WithPluralName("InventoryItems")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b9bfaea8-e5f0-4b0e-955f-df28ed63e8e3"), new Guid("7da8c058-92b7-4fd7-9eaf-7b7fb94f62cf"), new Guid("fb45aece-26e0-42ec-8dac-ddfcf11e61d9")).WithObjectTypes(ShipmentItem, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bdd8041b-b1d1-4902-980a-f085c0af166d"), new Guid("150fd46f-8768-4210-a559-740386e7c03d"), new Guid("2fb644a8-bbd4-419e-8c30-ef10efeb07d7")).WithObjectTypes(ShipmentItem, InvoiceItem).WithSingularName("InvoiceItem")  .WithPluralName("InvoiceItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f5c0279e-5ce4-4f09-bb93-ecaeb4825bcf"), new Guid("59b2bb80-3e60-4958-a3d8-9b5f7242d95c"), new Guid("fbac397f-52f2-4903-95bc-ee3f6ab3ae7b")).WithObjectTypes(ShipmentItem, Good).WithSingularName("Good")  .WithPluralName("Goods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ProductFeature
            new RelationTypeBuilder(domain, new Guid("4a8c511a-8146-4d6d-bc35-d8d6b8f1786d"), new Guid("31ff19c6-9916-4f4c-8d67-30649d3a07ea"), new Guid("8e35afcf-c606-4099-ba83-87c6b6fc37e1")).WithObjectTypes(ProductFeature, EstimatedProductCost).WithSingularName("EstimatedProductCost")  .WithPluralName("EstimatedProductCosts")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("8ac8ab84-f78f-4232-a4f7-390f55019663"), new Guid("6c65b1ad-91e4-4aae-a78e-d6d142bb98e5"), new Guid("3115f401-fc58-48da-a769-afecafbeb729")).WithObjectTypes(ProductFeature, PriceComponent).WithSingularName("BasePrice")  .WithPluralName("BasePrices")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b75855b8-c921-4d60-8ea0-650a0f574f7f"), new Guid("dd0b49c7-56f4-43ac-a470-ddc191d1c279"), new Guid("64bfaf6d-aaac-42ec-ac37-22a1d674611f")).WithObjectTypes(ProductFeature, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("badde93b-4691-435e-9ba3-e52435e9f574"), new Guid("c49ca161-d15b-4ba9-b42f-06144e8ca9f6"), new Guid("a3a58a70-3bab-4c8a-ac47-8beaca3b46d2")).WithObjectTypes(ProductFeature, ProductFeature).WithSingularName("DependentFeature")  .WithPluralName("DependentFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ce228118-f5b2-49bb-b0cd-7e0ca8e10315"), new Guid("d90b3906-a48d-473c-85a7-baae359d58a7"), new Guid("0305f707-683e-4fcd-94a0-7c0b3a2b27e4")).WithObjectTypes(ProductFeature, ProductFeature).WithSingularName("IncompatibleFeature")  .WithPluralName("IncompatibleFeatures")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("efe16e22-edfb-40b1-83c0-110f874c285a"), new Guid("3c78c391-cf55-40ce-9d11-a0600787ed82"), new Guid("6c3e8238-f1dd-4461-a73b-0927cd26db29")).WithObjectTypes(ProductFeature, VatRate).WithSingularName("VatRate")  .WithPluralName("VatRates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Requirement
            new RelationTypeBuilder(domain, new Guid("0f2c9ca2-9f2a-403e-8110-311fc0622326"), new Guid("099c426c-7b3f-4c9a-9059-525851488030"), new Guid("178dfe82-99e2-4026-84f9-223e10e852c7")).WithObjectTypes(Requirement, allorsDateTime).WithSingularName("RequiredByDate")  .WithPluralName("RequiredByDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("289a217e-4705-4bf0-b3bb-e51a056e13ee"), new Guid("320a457b-8a35-487d-9ebb-eaa7f6443ebf"), new Guid("63cbc132-2108-4686-9cc6-811914524bcc")).WithObjectTypes(Requirement, RequirementObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2b828f2b-201d-4ae2-b64c-b2c5be713653"), new Guid("8bd1a8cc-4f4d-41ad-b4fb-d43d4759c0e4"), new Guid("041107e2-0936-48a6-86dd-58ace8cbf7ac")).WithObjectTypes(Requirement, Party).WithSingularName("Authorizer")  .WithPluralName("Authorizers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("36511540-8c83-467c-9ed0-ff5dee38c378"), new Guid("047c0186-3878-4895-9946-4b5a32c5bae1"), new Guid("c80ac083-3b01-432c-81e1-56da054a5023")).WithObjectTypes(Requirement, RequirementStatus).WithSingularName("RequirementStatus")  .WithPluralName("RequirementStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3a6ba1d0-3efb-44f3-b90b-7e504ed11140"), new Guid("5e36946c-46d4-4cd4-9ba7-e1c94746ffe9"), new Guid("93f93798-b587-4f8f-9a82-2e0e9c870a52")).WithObjectTypes(Requirement, allorsString).WithSingularName("Reason")  .WithPluralName("Reasons")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("3ecf2b1e-ac3d-4533-9da1-341111fca04d"), new Guid("ea9f2ab4-6774-44eb-91ce-545f499ae792"), new Guid("483b60d4-f3b7-47da-abb4-c7cefee78e2a")).WithObjectTypes(Requirement, Requirement).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("43e11ee6-dcee-4a2c-80a7-8e04ee36ceb8"), new Guid("d2351d54-e600-400b-a350-9d2f81b5cf3d"), new Guid("0d52a5f8-3852-4483-9f0d-a6877fc3b5a0")).WithObjectTypes(Requirement, Party).WithSingularName("NeededFor")  .WithPluralName("NeededFors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5ed2803c-02d4-4187-8155-bee79e1a0829"), new Guid("e0d08055-60ad-4417-b861-ef3b44f00e79"), new Guid("c4abf003-69be-4e79-8958-701aac912d13")).WithObjectTypes(Requirement, Party).WithSingularName("Originator")  .WithPluralName("Originators")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ad98fd99-98b3-4876-b4af-b0c6aa7f41eb"), new Guid("d7a1af36-aea0-4e99-ab8b-b264c6bad301"), new Guid("5d88d9ac-6895-4a72-811b-2c02c9daed9b")).WithObjectTypes(Requirement, RequirementObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b21d89b3-dfbf-484d-afa8-d6ee43cbef6c"), new Guid("7a949028-b354-4749-b048-ba487958fb01"), new Guid("5a205868-1893-444d-8fa2-5ad1361555b9")).WithObjectTypes(Requirement, RequirementStatus).WithSingularName("CurrentRequirementStatus")  .WithPluralName("CurrentRequirementStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b6b7e1e9-6cce-4ca0-a085-0afd3a58ec50"), new Guid("fc02e70b-da78-4f1e-aac3-8b4ba32cea90"), new Guid("1137e61a-5efc-4c7c-9073-0f02c03b9408")).WithObjectTypes(Requirement, Facility).WithSingularName("Facility")  .WithPluralName("Facilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bfce13c0-b5c2-46f0-b0fd-d0d288f8dc07"), new Guid("7c7ea2fb-451e-4a94-b5fd-cdeab8d97844"), new Guid("f3923b48-a297-43b6-b318-bdafac87c36b")).WithObjectTypes(Requirement, Party).WithSingularName("ServicedBy")  .WithPluralName("ServicedBys")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c34694b4-bd8e-46e9-8bf1-fb1296738ab4"), new Guid("3bd6d711-d49b-4477-9173-e4f8a17f1d8b"), new Guid("6f53fe03-c9a2-43b8-b38e-99597d751a82")).WithObjectTypes(Requirement, allorsDecimal).WithSingularName("EstimatedBudget")  .WithPluralName("EstimatedBudgets")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d902fe48-c91f-43fe-b402-e0d87606124a"), new Guid("dfda3196-d793-4f58-af1e-661d943c8908"), new Guid("943f924a-5e11-4e5e-9f3a-fc3df42acfc7")).WithObjectTypes(Requirement, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f553ad3c-675f-4b97-95c9-42f4d85eb5f9"), new Guid("995dbc52-905b-4572-a41f-8d39584f4132"), new Guid("81fa089d-cc7f-4893-8186-ef6c98780b68")).WithObjectTypes(Requirement, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
			
            // NewsItem
            new RelationTypeBuilder(domain, new Guid("1a86dc14-eadc-4aad-83c2-238e31a20658"), new Guid("f8d3058b-3e81-4da8-a29f-52dd267e1733"), new Guid("ae0eba55-6aaf-4ed4-a784-006f5bf95f49")).WithObjectTypes(NewsItem, allorsBoolean).WithSingularName("IsPublished")  .WithPluralName("ArePublished")      .Build();
            new RelationTypeBuilder(domain, new Guid("2f1736ea-0e74-43a9-b047-cc37bc9618fa"), new Guid("c1202b23-5507-43ea-849d-94cd46392927"), new Guid("0add2328-35a3-4286-bd0e-258a47756ce5")).WithObjectTypes(NewsItem, allorsString).WithSingularName("Title")  .WithPluralName("Titles")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("369f8b36-1bb8-45b6-b02d-6cd7c126cb54"), new Guid("bc2dd1eb-0f39-4717-bd3e-bfcaee6e0a0c"), new Guid("c43bf977-c5de-44f9-99a9-bb8e9dd96122")).WithObjectTypes(NewsItem, allorsInteger).WithSingularName("DisplayOrder")  .WithPluralName("DisplayOrders")      .Build();
            new RelationTypeBuilder(domain, new Guid("372331ef-70a4-4a67-8f85-a0907ace9194"), new Guid("39e20d75-09f5-4692-a16a-86c2b284e0fa"), new Guid("0ad8a7a1-3466-452c-ac7f-5df53627ae5f")).WithObjectTypes(NewsItem, Locale).WithSingularName("Locale")  .WithPluralName("Locales")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4a03f057-339f-4dd4-ac89-3b97d27d2170"), new Guid("ca816c14-6aaf-4ed8-b140-f9c941e4f769"), new Guid("687c0be7-138d-4ea3-a87f-58df8ac7e60d")).WithObjectTypes(NewsItem, allorsString).WithSingularName("LongText")  .WithPluralName("LongTexts")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7aee11d0-f9b4-450d-83b8-357811e99246"), new Guid("2ddcf225-907a-4d99-921c-f61893aa7ac8"), new Guid("a08e2201-208f-4823-a814-a498aa0db9a5")).WithObjectTypes(NewsItem, allorsString).WithSingularName("Text")  .WithPluralName("Texts")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("a184408c-a1b0-47b2-821a-a2ab643b523e"), new Guid("213a7484-741a-4a2b-b765-3bc1b8427a64"), new Guid("55094ebe-bf49-44f4-bb0f-5722eca4ae90")).WithObjectTypes(NewsItem, allorsDateTime).WithSingularName("Date")  .WithPluralName("Date")      .Build();
            new RelationTypeBuilder(domain, new Guid("d29e707f-66dc-4fbf-aba4-63473498dd4b"), new Guid("b977e1eb-5a75-4f36-8381-6b49615e7969"), new Guid("5fe8e6f1-a992-452b-8711-0a9ed007ef2e")).WithObjectTypes(NewsItem, allorsBoolean).WithSingularName("Announcement")  .WithPluralName("Announcements")      .Build();
			
            // PartyBenefit
            new RelationTypeBuilder(domain, new Guid("15638ba3-73c7-4c32-aaa7-a91d4a5e9951"), new Guid("e904820e-49c4-4fa7-9f91-55e9430bcf38"), new Guid("0af63f52-dd36-45b4-9123-4d12a74d502a")).WithObjectTypes(PartyBenefit, TimeFrequency).WithSingularName("TimeFrequency")  .WithPluralName("TimeFrequencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("1c4a69e7-62c7-4e6b-b7a5-69817d1788df"), new Guid("67280aad-73cd-4366-8a4f-2d38257e022e"), new Guid("0d391786-7d0b-488f-95db-f449f85459ec")).WithObjectTypes(PartyBenefit, allorsDecimal).WithSingularName("Cost")  .WithPluralName("Costs")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("320e98c9-adff-41cf-894a-500730cf6c09"), new Guid("b9693920-2e4d-41e2-8925-c6e40b0ed673"), new Guid("2d48ba77-63e8-4397-9004-09329058f01b")).WithObjectTypes(PartyBenefit, allorsDecimal).WithSingularName("ActualEmployerPaidPercentage")  .WithPluralName("ActualEmployerPaidPercentages")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("9a8fcada-bf2c-450d-a941-e0c7ec414cf3"), new Guid("56813128-50b2-4fbf-ad0f-0385930a6805"), new Guid("95e9e94b-0fad-47e0-bfcf-f131e6962694")).WithObjectTypes(PartyBenefit, Benefit).WithSingularName("Benefit")  .WithPluralName("Benefits")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e4bd1497-824b-477a-9842-a87b4193b430"), new Guid("fc6f6c2a-5732-4c3d-8db0-58e3a4f26d6c"), new Guid("53891641-e7a9-46a6-bd2c-56b7f23b0ab5")).WithObjectTypes(PartyBenefit, allorsDecimal).WithSingularName("ActualAvailableTime")  .WithPluralName("ActualAvailableTimes")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("f377fde6-39b4-47e1-a3c0-e574f416f6ad"), new Guid("8a5b1e5f-e945-43ac-a1ae-72c1c0457a73"), new Guid("c4d10070-cbf6-4ebf-a9f1-a53a8d6d41b4")).WithObjectTypes(PartyBenefit, Employment).WithSingularName("Employment")  .WithPluralName("Employments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PostalAddress
            new RelationTypeBuilder(domain, new Guid("24216a78-41d8-4ffc-958a-2411530eeb94"), new Guid("649eb363-210c-4567-be0a-bcd3e666294e"), new Guid("1e9fb472-c39d-4e46-a58c-3cbf2b99c2cd")).WithObjectTypes(PostalAddress, GeographicBoundary).WithSingularName("GeographicBoundary")  .WithPluralName("GeographicBoundaries")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5440794c-8569-46fb-a5cb-42dc523e1264"), new Guid("1f14c608-3744-4697-a226-443196a57e94"), new Guid("db609423-3100-46a1-890d-0dbef16daf3f")).WithObjectTypes(PostalAddress, allorsString).WithSingularName("Address3")  .WithPluralName("Addresses3")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5c513411-ca39-4f39-844d-54cf0468a702"), new Guid("d72a1dc4-a61b-4f91-89ac-29f633b6944b"), new Guid("13e57a25-6265-4a56-a4e7-c914a0c57cb9")).WithObjectTypes(PostalAddress, Country).WithSingularName("Country")  .WithPluralName("Countries")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9475dd68-43f7-4195-bf57-8ce82333980e"), new Guid("c0a0b7b4-5f1a-4b8b-a858-5de3abc5e66f"), new Guid("58ffb62f-f270-4a64-b2a9-26b2cec8eaee")).WithObjectTypes(PostalAddress, allorsString).WithSingularName("Address2")  .WithPluralName("Addresses2")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9801fa63-ac82-4774-bf84-d2752b95b8a3"), new Guid("6eb0ec18-2d30-4529-b741-785aad15842f"), new Guid("a477adeb-04b9-449c-b61c-4a1384fe10aa")).WithObjectTypes(PostalAddress, City).WithSingularName("City")  .WithPluralName("Cities")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c83eb0ff-8503-4f2a-9280-f8e46b382b6a"), new Guid("2976fdd4-19c4-4913-8875-1bf413da02fd"), new Guid("04c32889-628b-4f9f-8504-65a73268055d")).WithObjectTypes(PostalAddress, allorsString).WithSingularName("Address1")  .WithPluralName("Addresses1")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("dfec7833-a7c1-4c27-bbdb-7bc2cc9e8f30"), new Guid("c206f13e-d4eb-4818-81ef-e134251698cd"), new Guid("f6a78ca4-be61-41ff-803b-05934f8691e7")).WithObjectTypes(PostalAddress, PostalBoundary).WithSingularName("PostalBoundary")  .WithPluralName("PostalBoundaries")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e642d557-a842-4357-be4a-ed7da965d592"), new Guid("ad1d5592-71a7-4410-91e3-f00ea1c29ce1"), new Guid("23f50fbf-71df-4532-9b45-17e5ac3ad7f4")).WithObjectTypes(PostalAddress, PostalCode).WithSingularName("PostalCode")  .WithPluralName("PostalCodes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f2ba6a39-2e34-42bc-accb-0d8838311994"), new Guid("6d2b7cbb-1825-40a9-9000-2854a9cd6a26"), new Guid("53349be7-9048-494f-96e0-367468eb9dad")).WithObjectTypes(PostalAddress, allorsString).WithSingularName("Directions")  .WithPluralName("Directions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f97e6fa7-3c30-4ab1-9b9b-d2c9ad257009"), new Guid("d57e1d69-6f84-4601-8ae8-0807d6bf4d5c"), new Guid("26ca4fa5-70e5-4f38-a731-ca3a34f24a91")).WithObjectTypes(PostalAddress, allorsString).WithSingularName("FormattedFullAddress")  .WithPluralName("FormattedFullAddresses")  .WithIsDerived(true)    .WithSize(-1).Build();
			
            // PackageQuantityBreak
            new RelationTypeBuilder(domain, new Guid("0df167e7-e1b7-4c2a-9de4-f06fc359600f"), new Guid("66f59599-97de-44a7-908a-a86a43e332e0"), new Guid("7d181c6a-e465-4e2e-a789-82f2c956b0c2")).WithObjectTypes(PackageQuantityBreak, allorsInteger).WithSingularName("From")  .WithPluralName("Froms")      .Build();
            new RelationTypeBuilder(domain, new Guid("c282c1db-d9a5-40b8-aed1-ddbd060cdbcd"), new Guid("edc54775-b7d9-4d2c-94a3-93e8974f5da8"), new Guid("2c753aa2-9ee7-4b06-9851-ce992a3545e3")).WithObjectTypes(PackageQuantityBreak, allorsInteger).WithSingularName("Through")  .WithPluralName("Throughs")      .Build();
			
            // SubContractorRelationship
            new RelationTypeBuilder(domain, new Guid("567a8c58-2584-4dc7-96c8-13fea5b51cf9"), new Guid("f12711a8-11ce-4f9c-a75b-02594b476a9e"), new Guid("8f21a29f-a0b0-412c-b7dd-e2fcdee561d6")).WithObjectTypes(SubContractorRelationship, Party).WithSingularName("Contractor")  .WithPluralName("Contractors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d95ecb34-dfe4-42df-bc9f-1ad4af72abaa"), new Guid("597810f4-da06-4d63-837e-6cd0419f3d4b"), new Guid("6aca0d56-be58-4876-bfef-918430a119a7")).WithObjectTypes(SubContractorRelationship, Party).WithSingularName("SubContractor")  .WithPluralName("SubContractors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // InvoiceItem
            new RelationTypeBuilder(domain, new Guid("0599c28d-11f3-4ccb-b78c-2d6c8748b952"), new Guid("a8520b8c-37c2-4b4b-a31d-649ba867f9b8"), new Guid("04cdcfac-28ee-4c0d-9e6f-aaa2b5297eea")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalIncVatCustomerCurrency")  .WithPluralName("TotalIncVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("067674d0-6d9b-4a7e-b0c6-62c24f3a4815"), new Guid("72cdddb8-711d-491c-9965-cef190a10913"), new Guid("5f894db7-f9ed-47d0-a438-c2e00446edbf")).WithObjectTypes(InvoiceItem, AgreementTerm).WithSingularName("InvoiceTerm")  .WithPluralName("InvoiceTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("0805a468-9d72-4199-a88e-402b84fbe3e6"), new Guid("3e586376-57fc-45e3-930a-49ac79c66431"), new Guid("6dbb9536-45c2-4ce4-b215-9ffac8f96450")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalVatCustomerCurrency")  .WithPluralName("TotalVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("11bfeaf2-f89e-433c-aef2-7154a5e1fa9a"), new Guid("647c6c5f-c2b8-4e39-b5cb-5860dda100b2"), new Guid("c2804f56-758d-4677-83f7-dac3671fa0b7")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalBasePrice")  .WithPluralName("TotalBasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1b42d64f-db5e-4c28-8234-53458f269c0a"), new Guid("11e16623-b6d5-40cf-9ea0-c4dd6b189105"), new Guid("ff7a63e0-0805-4ed5-a358-8bb87c418829")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalSurcharge")  .WithPluralName("TotalSurcharges")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1f92aed8-8a8f-4eb6-8102-83a6395788d6"), new Guid("b65ecb61-b074-47fc-aac7-74119295c827"), new Guid("e8c62a38-a856-4db6-a971-575d7971689c")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalInvoiceAdjustment")  .WithPluralName("TotalInvoiceAdjustments")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2fe594e6-1dfe-4be5-9842-98e7e669a8c4"), new Guid("d8846b2b-476b-4396-89f3-273e6fb5c01e"), new Guid("ea7e4254-fdbb-4427-805a-0025245051ec")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalExVatCustomerCurrency")  .WithPluralName("TotalExVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("32587de5-c4e7-4048-b21d-d3770bda87b0"), new Guid("6d099688-2cf7-463d-bce6-44f171e6d375"), new Guid("bb0476d5-0953-43dc-a461-f19a1ae4d7c4")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalDiscount")  .WithPluralName("TotalDiscounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("33caab05-ec61-4cf9-b903-b5d5a8d7eef9"), new Guid("77489b35-b46a-4540-8359-005adbd9d1f9"), new Guid("cf9b4f4a-b867-4a47-919e-2cb90be72980")).WithObjectTypes(InvoiceItem, InvoiceVatRateItem).WithSingularName("InvoiceVatRateItem")  .WithPluralName("InvoiceVatRateItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("374fd832-986e-44fb-b010-5db6ecbdc29a"), new Guid("5d08d863-b624-4a39-984d-63e58a3c39e6"), new Guid("aca753e1-37e1-44a3-b709-41171ae38b9a")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalDiscountAsPercentage")  .WithPluralName("TotalDiscountsAsPercentage")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("42c5114f-7963-477d-82cf-09bfa0b194bb"), new Guid("26a8e75e-bc48-45e1-a957-fd41c813bfe3"), new Guid("033c9d78-7940-4485-844c-dedd7e40bd62")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("CalculatedUnitPrice")  .WithPluralName("CalculatedUnitPrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("43eeabd8-99c6-4f35-a804-0723f695db87"), new Guid("ca00444f-ae9e-4688-942c-017f46227615"), new Guid("65ffee00-11cc-44b0-8f0e-91dd4007a865")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("UnitDiscount")  .WithPluralName("UnitDiscounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("448b5a4a-f876-48d9-9bae-c770a908997f"), new Guid("0060c405-1ed9-4f3c-be5c-bd12469ab019"), new Guid("9bd2aa77-b22e-43dc-be67-fddeb648d6c4")).WithObjectTypes(InvoiceItem, VatRegime).WithSingularName("AssignedVatRegime")  .WithPluralName("AssignedVatRegimes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("45289923-9da5-4d4a-b07c-78ee71d30e31"), new Guid("0dc30627-9f98-4f9e-9d2c-4e09e5994c2d"), new Guid("5a7a7b07-882e-4f3f-bde7-f09c719892ec")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalIncVat")  .WithPluralName("TotalIncVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("475d7a79-27a1-4d5a-90c1-3896fa2e892e"), new Guid("ad65733c-6d3d-4e90-97d5-ca91bc4505d9"), new Guid("651b29f8-644d-4588-ac56-0d51f2068ebd")).WithObjectTypes(InvoiceItem, InvoiceItem).WithSingularName("AdjustmentFor")  .WithPluralName("AdjustmentFors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5f355cb5-5156-4f76-97bc-ec153a41e9ef"), new Guid("3233b6d6-5bca-401a-ae03-77706efda65b"), new Guid("15d66771-3550-44e6-919b-2bbfbdf8b66d")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("UnitBasePrice")  .WithPluralName("UnitBasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6718d6f1-e62f-4e1f-8368-813ad6fe4417"), new Guid("83c2ab80-c364-445f-a2dc-33cbc8d88d97"), new Guid("e5b9ea20-d51b-4621-979c-238f43ce2f90")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalSurchargeCustomerCurrency")  .WithPluralName("TotalSurchargesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6df95cf4-115f-4f43-aaea-52313c47d824"), new Guid("93ba1265-4050-41c1-aaf8-d09786889245"), new Guid("0abd9811-a8ac-42bf-9113-4f9760cfe9eb")).WithObjectTypes(InvoiceItem, SerializedInventoryItem).WithSingularName("SerializedInventoryItem")  .WithPluralName("SerializedInventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6dfc7def-6790-4841-90db-c37a431593ec"), new Guid("27f066ba-c894-4ced-9c3f-8af137d0ffb4"), new Guid("88d55469-1e0c-4dc8-83a7-ce0a48d9c9d9")).WithObjectTypes(InvoiceItem, PriceComponent).WithSingularName("CurrentPriceComponent")  .WithPluralName("CurrentPriceComponents")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("741d7629-aa2f-45f9-b66c-4ab8abf07518"), new Guid("c2f98a95-6577-403a-bb7e-5e3f158fe11b"), new Guid("90e76224-e139-436d-a998-0dc24709e52e")).WithObjectTypes(InvoiceItem, DiscountAdjustment).WithSingularName("DiscountAdjustment")  .WithPluralName("DiscountAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7d3a259d-c27f-45d4-96f1-3a43a0e5043f"), new Guid("cafd1700-a59e-4278-90a5-cb387f413b8f"), new Guid("721f302f-94eb-4ccd-b688-d13383d21571")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("ActualUnitPrice")  .WithPluralName("ActualUnitPrices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7eed800d-c2b5-4837-a288-150803578b27"), new Guid("9dbf4d82-0d36-42a0-81a7-49f59e5cd226"), new Guid("f3b11549-8cf9-4ade-8465-111536b00171")).WithObjectTypes(InvoiceItem, allorsString).WithSingularName("Message")  .WithPluralName("Messages")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("8696b970-07e7-4d04-aec5-d42bcd47ce72"), new Guid("ad3bd0d4-12c1-4994-abaa-c47dc0d17a7a"), new Guid("245ad362-41b4-4fa9-91a6-b52329215d13")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalInvoiceAdjustmentCustomerCurrency")  .WithPluralName("TotalInvoiceAdjustmentsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8fd19791-85ed-44c9-8580-a6768578ca3a"), new Guid("72e1379d-a9c3-41d5-8ae4-a9a82c88ad01"), new Guid("1ca3573f-812f-41ca-a5e8-ec13ea6168aa")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("AmountPaid")  .WithPluralName("AmountPaids")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("93e0e781-4755-4cd4-aeff-bff905d6e99b"), new Guid("6f45d5e3-ef25-45ea-9f31-11fc6a480c6c"), new Guid("0f3e4887-e43f-4b51-86ee-c4f474de2d7a")).WithObjectTypes(InvoiceItem, VatRate).WithSingularName("DerivedVatRate")  .WithPluralName("DerivedVatRates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b114b874-9e68-4451-b3f1-c5aa7a139a02"), new Guid("4f22e0be-bdcb-496e-b7e5-77c21d1c90df"), new Guid("5bfd82d4-0d28-4382-8447-a72fec2fa8a2")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalDiscountCustomerCurrency")  .WithPluralName("TotalDiscountsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b20c5190-65f8-4d71-a3f1-30da9b41173a"), new Guid("5fc0c0b2-4fcc-4f94-853e-eef98f404c28"), new Guid("c85bb656-2979-4144-b56a-cb97005e6de9")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("UnitSurcharge")  .WithPluralName("UnitSurcharges")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b3f7be6a-2374-40e3-98e1-095b0117847e"), new Guid("6c6cffda-dc95-437e-90dc-f98ce86e4fdc"), new Guid("c056df57-dd5b-40f2-8032-ef58f4ec3f7d")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalExVat")  .WithPluralName("TotalExVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ba90acfe-0d55-4854-a046-35279f872e0b"), new Guid("d231d38a-2e1e-4e21-8622-d5b30199f857"), new Guid("b525a1c4-5f1f-402f-9f40-105e711bf45d")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("c540c7fe-924d-4616-a49a-515ac65c4cf7"), new Guid("08276c02-e869-40e9-a99d-63448f6c94fb"), new Guid("71bd0f31-0867-4391-a45d-a8684d50d772")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalSurchargeAsPercentage")  .WithPluralName("TotalSurchargesAsPercentage")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("cccc995c-1478-4145-ba90-ace3ae7ba184"), new Guid("d479cadc-2f62-4d56-8e82-96c2b3166b4e"), new Guid("9258d1fc-7788-46f2-baf0-5eff2443fd53")).WithObjectTypes(InvoiceItem, VatRegime).WithSingularName("VatRegime")  .WithPluralName("VatRegimes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d4144a78-f466-44e9-a62f-d84bcdf22b0f"), new Guid("ac68b612-25dc-4ab9-81a2-06c36cc42bed"), new Guid("67073f7a-6888-40e0-adf5-c4938a966a06")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalBasePriceCustomerCurrency")  .WithPluralName("TotalBasePricesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e30596f0-7c79-4d13-b1d6-26e4fd1f55f2"), new Guid("74658b67-6e2d-4c80-b745-35af0f3f8654"), new Guid("00bae3ee-3825-414b-ad25-595b9ed469f9")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("TotalVat")  .WithPluralName("TotalVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e52e9b0a-4772-465a-bab7-c79372d7000a"), new Guid("6e45a92d-491d-491d-9a28-ee1421a87aaa"), new Guid("4b9e3dd2-09a5-47a3-be48-8bdbe5a5ca7f")).WithObjectTypes(InvoiceItem, SurchargeAdjustment).WithSingularName("SurchargeAdjustment")  .WithPluralName("SurchargeAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e7a24fa8-f664-4cf0-a392-8b7827a7f537"), new Guid("ffc4548d-098f-4aa2-8343-309c39583875"), new Guid("8bcba64b-9745-4d98-8f60-f3ce548acd03")).WithObjectTypes(InvoiceItem, allorsDecimal).WithSingularName("UnitVat")  .WithPluralName("UnitVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("fb202916-1a87-439e-b2d8-b3f3ed4f681a"), new Guid("13dda3fd-6011-4876-9860-158d86024dbd"), new Guid("50ab8ac2-daca-4e66-861d-4134fcaa0e98")).WithObjectTypes(InvoiceItem, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
			
            // Store
            new RelationTypeBuilder(domain, new Guid("0a0ad3b1-afa2-4c78-8414-e657fabebb3e"), new Guid("c460c53b-1460-4bc2-8390-98e9c1492b71"), new Guid("06502486-41cb-4840-856e-7d44c0038375")).WithObjectTypes(Store, allorsDecimal).WithSingularName("ShipmentThreshold")  .WithPluralName("ShipmentThreshold")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("190d2363-affa-4a4b-8662-d8b566c506d6"), new Guid("76194162-1ef0-45f0-8804-2183f42e0e17"), new Guid("b4d8f605-9a66-468d-b9a0-f2157e8528b7")).WithObjectTypes(Store, StringTemplate).WithSingularName("SalesInvoiceTemplate")  .WithPluralName("SalesInvoiceTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("3a837bae-993a-4765-8d4f-b690bf65dc79"), new Guid("0304eacc-65bc-475d-9a82-00b0cdb233ad"), new Guid("21c8c056-2997-4f75-82db-597e258dceb6")).WithObjectTypes(Store, allorsString).WithSingularName("OutgoingShipmentNumberPrefix")  .WithPluralName("OutgoingShipmentNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("3e378f04-0d14-4b03-b8e2-b58da3039184"), new Guid("b4f8b63a-d4c6-4a40-a603-84c4225f02ed"), new Guid("3a00ec26-a46e-4262-aed0-56cb25abf2b1")).WithObjectTypes(Store, allorsString).WithSingularName("SalesInvoiceNumberPrefix")  .WithPluralName("SalesInvoiceNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("4927a65d-a9d3-4fad-afce-1ec8679d3a55"), new Guid("e2dc511c-86b0-46fe-b5cf-680dfe012f47"), new Guid("f18df944-920c-474a-ac8e-2e10b460c522")).WithObjectTypes(Store, allorsInteger).WithSingularName("PaymentGracePeriod")  .WithPluralName("PaymentGracePeriods")      .Build();
            new RelationTypeBuilder(domain, new Guid("4a647ddb-9a17-4544-8cae-6204140c413a"), new Guid("d657040d-138b-4f6f-9dc7-547448a1fd11"), new Guid("93cab392-5d64-4270-9e5d-ac3a62dcde4d")).WithObjectTypes(Store, Media).WithSingularName("LogoImage")  .WithPluralName("LogoImages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("555c3b9a-7556-4fdf-a431-6d18a6ae7cbd"), new Guid("b3625e45-4568-4030-85cc-565f77ccc1a1"), new Guid("5bda243d-b0c9-47bd-9d33-9fd3723512b9")).WithObjectTypes(Store, allorsInteger).WithSingularName("PaymentNetDays")  .WithPluralName("PaymentsNetDays")      .Build();
            new RelationTypeBuilder(domain, new Guid("63d433b9-8cb3-428b-b516-be25f1895673"), new Guid("273cfb27-2698-469b-91ab-24901a4df9fd"), new Guid("67f48e4c-6e56-47c8-a87f-47160453ece6")).WithObjectTypes(Store, Facility).WithSingularName("DefaultFacility")  .WithPluralName("DefaultFacilities")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6e4b701a-2540-4cec-8413-50bfb69d3a7c"), new Guid("2a1d8fe1-51af-4747-b6e4-7c2532e5fa8c"), new Guid("2bcd6952-16c1-4153-a23c-6e58fae6a49c")).WithObjectTypes(Store, allorsString).WithSingularName("Name")  .WithPluralName("Name")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("72ea05f1-a631-4dec-a568-1307e380d41f"), new Guid("3799c587-25f3-4d18-a671-2d2301dae0df"), new Guid("c6bb5b3c-c599-427d-9c42-dc1966f11ce5")).WithObjectTypes(Store, StringTemplate).WithSingularName("SalesOrderTemplate")  .WithPluralName("SalesOrderTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("79244ed7-6388-48ca-86db-7b57a64fe680"), new Guid("5d145726-fa9e-46f9-b389-7f8380e0088c"), new Guid("1df01d8b-9d72-495c-baae-0f5ea4c9e76c")).WithObjectTypes(Store, allorsDecimal).WithSingularName("CreditLimit")  .WithPluralName("CreditLimits")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("7c9cda07-5920-4037-b934-5b74355c4b85"), new Guid("0da06b1f-12ce-43d1-9e21-82e506ce7750"), new Guid("f1c26a78-d986-4fcc-ac55-3658783790ef")).WithObjectTypes(Store, ShipmentMethod).WithSingularName("DefaultShipmentMethod")  .WithPluralName("DefaultShipmentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("80670a7a-1be8-4407-917e-fa359e632519"), new Guid("dcd8b2e0-7490-40e4-ae4b-d1b0c0be0527"), new Guid("72993b04-4c10-4467-9d99-064e1b39f9e2")).WithObjectTypes(Store, Carrier).WithSingularName("DefaultCarrier")  .WithPluralName("DefaultCarriers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("954d4e3c-f188-45f4-98b8-ece14ac7dabd"), new Guid("a08d71c8-6aa1-4ae3-bccc-a8078bd51071"), new Guid("92f5fc7a-eabe-4710-b1fe-aa35e7fd1606")).WithObjectTypes(Store, allorsDecimal).WithSingularName("OrderThreshold")  .WithPluralName("OrdersThreshold")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("9a0dfe33-016a-4b41-979c-d17a6f87d2d2"), new Guid("a6741bcc-527d-4cbe-bd1e-fc881fd30951"), new Guid("611edb41-213f-4ade-8ea5-a512b99ee9b6")).WithObjectTypes(Store, PaymentMethod).WithSingularName("DefaultPaymentMethod")  .WithPluralName("DefaultPaymentMethods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a77dc8f3-4d28-43a6-8d2d-b9a0253128b1"), new Guid("19216ddb-edb4-421a-91de-72f937abc8be"), new Guid("ae3269f7-6f0d-43e5-baac-a8c09a6a76ef")).WithObjectTypes(Store, allorsInteger).WithSingularName("NextSalesOrderNumber")  .WithPluralName("NextSalesOrderNumbers")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("b64dd0b0-8f35-4c71-9e7c-f47ee7ea1097"), new Guid("6a3bac6d-f9c6-460e-9c02-29728c567109"), new Guid("aaa0ce97-c540-42d7-aed3-e6b3430b1f23")).WithObjectTypes(Store, InternalOrganisation).WithSingularName("Owner")  .WithPluralName("Owners")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bc11d48f-bcab-4880-afe8-0a52d3c11e44"), new Guid("d44420aa-80fc-4d55-8032-18b1b1c63d69"), new Guid("6a37b722-41b4-411e-bc84-18918990ad14")).WithObjectTypes(Store, FiscalYearInvoiceNumber).WithSingularName("FiscalYearInvoiceNumber")  .WithPluralName("FiscalYearInvoiceNumbers")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ca82d0f8-f886-4936-80f5-a7dbb7c550b5"), new Guid("94284261-b6db-4d74-ae74-955f6481375f"), new Guid("49940864-e0db-4d0b-a607-b1725e6f45c9")).WithObjectTypes(Store, PaymentMethod).WithSingularName("PaymentMethod")  .WithPluralName("PaymentMethods")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e00e948e-6fc3-43fd-a49b-008fc6d6133f"), new Guid("3f5cbcd9-c36b-4792-b1fc-15cf533ba6f3"), new Guid("a7e750a0-2e69-485d-8208-ab04682b6efd")).WithObjectTypes(Store, allorsString).WithSingularName("SalesOrderNumberPrefix")  .WithPluralName("SalesOrderNumberPrefixes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("e38613e1-0aec-4f95-b87e-747ac3e5dc5d"), new Guid("dc95bbe2-b04b-4b88-b74c-1ba6ebba70f4"), new Guid("637b7853-7e00-4a09-b22a-84cf4e116f37")).WithObjectTypes(Store, allorsInteger).WithSingularName("NextOutgoingShipmentNumber")  .WithPluralName("NextOutgoingShipmentNumbers")      .Build();
            new RelationTypeBuilder(domain, new Guid("f024d205-2420-40bb-ab1d-71533fa25557"), new Guid("68800a34-f973-4339-b5d5-7c611b39a2b1"), new Guid("e858cc6c-2a6e-4c04-be7a-e019009bf3f7")).WithObjectTypes(Store, StringTemplate).WithSingularName("CustomerShipmentTemplate")  .WithPluralName("CustomerShipmentTemplates")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ffa4098f-3015-4977-8524-2d14609d77cc"), new Guid("b6643398-f851-4d24-9604-a116793960fa"), new Guid("70b4ef21-d969-4b70-b995-87c81fb61438")).WithObjectTypes(Store, allorsInteger).WithSingularName("NextSalesInvoiceNumber")  .WithPluralName("NextSalesInvoiceNumbers")  .WithIsDerived(true)    .Build();
			
            // Lot
            new RelationTypeBuilder(domain, new Guid("4888a06a-fcf5-42a7-a1c3-721d3abaa755"), new Guid("0f922c04-b617-4b72-8c22-02f43ac2afb9"), new Guid("46b3ec4d-0463-48eb-8764-8dedf8c48b1a")).WithObjectTypes(Lot, allorsDateTime).WithSingularName("ExpirationDate")  .WithPluralName("ExpirationDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("8680f7e2-c5f1-43af-a127-68ac8404fbf4"), new Guid("e350d93d-c5ce-496b-a210-c01b4ff82c60"), new Guid("92953ece-133e-4402-ad5c-5357c34bb99e")).WithObjectTypes(Lot, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("ca7a3e0f-e036-40ed-9346-0d1dae45c560"), new Guid("fdb9e9dc-1395-43ed-8234-187f35b8a7ef"), new Guid("03e6a4fc-2336-4761-807f-20c1b5b80af0")).WithObjectTypes(Lot, allorsString).WithSingularName("LotNumber")  .WithPluralName("LotNumbers")      .WithSize(256).Build();
			
            // WorkEffortSkillStandard
            new RelationTypeBuilder(domain, new Guid("13a68eeb-7ca1-4ecd-a82b-ecbd75da99b6"), new Guid("fe6ffe1f-a4eb-4478-922f-4c786e40709c"), new Guid("e0a9c761-26d1-48cd-bf13-bd6f66d863ba")).WithObjectTypes(WorkEffortSkillStandard, Skill).WithSingularName("Skill")  .WithPluralName("Skills")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("20623472-f4f3-40fc-bd7c-cd3da44fe224"), new Guid("5bc7090b-cf0e-4d08-8c8a-12db14e42ec3"), new Guid("57be9ba9-bb08-4b38-80eb-596f550f7963")).WithObjectTypes(WorkEffortSkillStandard, allorsInteger).WithSingularName("EstimatedNumberOfPeople")  .WithPluralName("EstimatedNumbersOfPeople")      .Build();
            new RelationTypeBuilder(domain, new Guid("e05c673f-6c4b-492d-bf68-b4af15310aea"), new Guid("4cd6b8fb-6713-4ba2-8cf8-7fa80e824a0e"), new Guid("72b3e6fe-1f43-4964-90e0-a718635d985d")).WithObjectTypes(WorkEffortSkillStandard, allorsDecimal).WithSingularName("EstimatedDuration")  .WithPluralName("EstimatedDurations")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ed6a55d4-def6-49e0-8b1a-9ee99d8b3c3d"), new Guid("d5289442-6578-4928-873e-7e64cafadf66"), new Guid("206a7548-e6bc-4886-a53c-c11afcd83ede")).WithObjectTypes(WorkEffortSkillStandard, allorsDecimal).WithSingularName("EstimatedCost")  .WithPluralName("EstimatedCosts")      .WithPrecision(19).WithScale(2).Build();
			
            // RequestItem
            new RelationTypeBuilder(domain, new Guid("542f3de9-e808-443b-b6e6-baf2db1ec2b1"), new Guid("30b2b652-b7a8-42ec-bd10-dd606a1be951"), new Guid("c176a9ff-7656-4c22-bf00-e19cdcd16566")).WithObjectTypes(RequestItem, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("5c0f0069-b7f9-47f1-8346-c30f14afbc0c"), new Guid("0f924664-8b58-45f4-b6f3-d8201610de8f"), new Guid("3560f38b-1945-4eb1-9b9a-c3e84d267647")).WithObjectTypes(RequestItem, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("6544faeb-a4cf-447c-a696-b6561c45086e"), new Guid("3d03cbae-7618-458e-b705-94112c8f66db"), new Guid("0204dc28-cec2-4d6b-b525-c7e4c65f958b")).WithObjectTypes(RequestItem, Requirement).WithSingularName("Requirement")  .WithPluralName("Requirements")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a5d1bef9-3086-4c32-9a6d-ce33c4f09539"), new Guid("1d3eedcb-dc13-46ad-ac43-e6979995e00b"), new Guid("474a6350-abba-4c53-ba26-0320c60aa8a8")).WithObjectTypes(RequestItem, Deliverable).WithSingularName("Deliverable")  .WithPluralName("Deliverables")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("bf40cb6b-e561-4df1-9ac4-e5a72933c7db"), new Guid("2eddcab2-e293-4699-8392-c198018a8ce4"), new Guid("90b8c610-e703-4109-92c7-bad2f5e1501b")).WithObjectTypes(RequestItem, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d02d15ae-2938-4753-95f1-686ea8b02f47"), new Guid("0abe5f12-ae64-4a8e-b5cc-175d7d2ea1d7"), new Guid("91d62ba3-943e-4aa5-b4fc-6a1f62fcd63f")).WithObjectTypes(RequestItem, NeededSkill).WithSingularName("NeededSkill")  .WithPluralName("NeededSkills")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f03c07b5-44f2-4e61-ad23-7c373851dafc"), new Guid("d4cce9f6-ebe0-4b72-86f6-d41c8cdf072e"), new Guid("bd7d900b-d5c6-46dc-8843-e4041429858b")).WithObjectTypes(RequestItem, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fa33c3e6-53c4-428a-bd9c-feba1dd9ed45"), new Guid("1aa99128-9989-4933-8204-9acefc7b040d"), new Guid("99251c00-d729-4363-8ce8-403ace61725e")).WithObjectTypes(RequestItem, allorsDecimal).WithSingularName("MaximumAllowedPrice")  .WithPluralName("MaximumAllowedPrices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ff41a43c-997d-4158-984e-e669eb935148"), new Guid("b46ffa62-adcb-4928-bdb0-79d0eef9e676"), new Guid("7c4353a9-efd5-437e-8789-fae92a0be1ed")).WithObjectTypes(RequestItem, allorsDateTime).WithSingularName("RequiredByDate")  .WithPluralName("RequiredByDates")      .Build();
			
            // DesiredProductFeature
            new RelationTypeBuilder(domain, new Guid("24695d7b-5c61-4b5c-be90-0f18ca46c6a6"), new Guid("c0720b85-3e00-4ad7-8a19-9c6761aa1bba"), new Guid("360db95e-a5ad-4771-ad23-d591be1640d2")).WithObjectTypes(DesiredProductFeature, allorsBoolean).WithSingularName("Required")  .WithPluralName("Requireds")      .Build();
            new RelationTypeBuilder(domain, new Guid("d09dbd42-5c59-4d78-b5d7-4dbee0406ead"), new Guid("4a611e8a-1bd5-42b3-b2ba-14403328696b"), new Guid("93f73ab2-af4a-47d1-822b-df576f5a5e86")).WithObjectTypes(DesiredProductFeature, ProductFeature).WithSingularName("ProductFeature")  .WithPluralName("ProductFeatures")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesOrderItemStatus
            new RelationTypeBuilder(domain, new Guid("90dd5f56-af80-4f78-a0b6-c13f34c87193"), new Guid("05167075-7e33-42ba-a40a-ef2233af019a"), new Guid("3b0a60b9-b8f3-41f0-af4f-75598240bde1")).WithObjectTypes(SalesOrderItemStatus, SalesOrderItemObjectState).WithSingularName("SalesOrderItemObjectState")  .WithPluralName("SalesOrderItemObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f31a9949-c0b3-45c5-854f-29884ce45c9b"), new Guid("73152227-ec98-4b5a-9fad-0e2d38cd7c61"), new Guid("47852a3a-f906-4757-87b9-29c6e8d560f9")).WithObjectTypes(SalesOrderItemStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // ActivityUsage
            new RelationTypeBuilder(domain, new Guid("1c8929c2-090a-41f2-8a22-691a63df4ff7"), new Guid("ab9d6daf-e245-4281-9ff0-fb865c275f79"), new Guid("9acc53b1-4e7a-46c7-a34a-158f5eb05d07")).WithObjectTypes(ActivityUsage, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b7672e5b-5ddc-46ba-82f2-4804f8b43ebf"), new Guid("3c0cd8a9-c033-4ff1-9ff5-60e90cfffdf5"), new Guid("ed7d8046-4596-4055-af88-b2e4c9da6898")).WithObjectTypes(ActivityUsage, UnitOfMeasure).WithSingularName("UnitOfMeasure")  .WithPluralName("UnitsOfMeasure")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // CommunicationEventStatus
            new RelationTypeBuilder(domain, new Guid("414fc983-3086-4362-806a-d77b09f04b24"), new Guid("0da368d4-31c3-45b3-b556-561b080a03a5"), new Guid("d0e76e79-e797-44dd-8256-f88c10b1d440")).WithObjectTypes(CommunicationEventStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("5ad71d39-d9e2-4b08-a6ac-322c18f14be5"), new Guid("51e4f7d6-a511-493e-8207-b60343fccae6"), new Guid("4d04158e-c9a4-490c-9a35-c2205a01938a")).WithObjectTypes(CommunicationEventStatus, CommunicationEventObjectState).WithSingularName("CommunicationEventObjectState")  .WithPluralName("CommunicationEventObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Good
            new RelationTypeBuilder(domain, new Guid("04cd1e20-a031-4a4f-9f40-6debb52b002c"), new Guid("4441b31a-7807-41c6-803b-aeacd18e2867"), new Guid("8dc2ddca-4ae2-48b9-92db-ac68f2f5542e")).WithObjectTypes(Good, allorsDecimal).WithSingularName("AvailableToPromise")  .WithPluralName("AvailablesToPromise")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("1e977b9c-8582-48be-ac1d-20a055598290"), new Guid("be920e49-abff-4ef0-80c2-02df6dfa55e3"), new Guid("67e83a0e-db03-439d-832a-b5685887eeaa")).WithObjectTypes(Good, Media).WithSingularName("Thumbnail")  .WithPluralName("Thumbnails")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2ca90db1-8595-4de0-957e-dc4476be1654"), new Guid("637fa802-fc65-4b5e-aaf5-e49ac5218b9b"), new Guid("64036e01-6767-46d0-aca7-def5876db81f")).WithObjectTypes(Good, InventoryItemKind).WithSingularName("InventoryItemKind")  .WithPluralName("InventoryItemKinds")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4e8eceff-aec2-44f8-9820-4e417ed904c1"), new Guid("30f4ec83-5854-4a53-a594-ba1247d02b2f"), new Guid("80361383-e1fc-4256-9b69-7cd43469d0de")).WithObjectTypes(Good, allorsString).WithSingularName("BarCode")  .WithPluralName("BarCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("82295ab2-8488-4d7e-8703-9f7fbec55925"), new Guid("c1801b8f-013b-42ff-b02a-a6c9b0e361b8"), new Guid("cdc45553-9c60-4c40-8c82-56c488ee6aae")).WithObjectTypes(Good, FinishedGood).WithSingularName("FinishedGood")  .WithPluralName("FinishedGoods")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("859487f7-9759-4c30-8528-8cd5d014b0a2"), new Guid("e293bfae-afd7-4f15-8e01-58c9078364b6"), new Guid("3a499b07-0d4f-4f5a-b679-7d76118f8441")).WithObjectTypes(Good, allorsString).WithSingularName("Sku")  .WithPluralName("Skus")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("989d9c6c-56d6-407a-a890-3769cb7a675e"), new Guid("4da4bb2d-f830-4827-bdaf-1c584cdeb437"), new Guid("c31005b1-787d-4a0f-b281-f74551df7be7")).WithObjectTypes(Good, allorsString).WithSingularName("ArticleNumber")  .WithPluralName("ArticleNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("98d99ee6-6dc1-4ef5-ad5c-e24bcd1dfa27"), new Guid("60d2c039-b034-4e7f-a677-d65a302d9f5f"), new Guid("eeba67a7-b5c4-4783-b391-b9dd35093efb")).WithObjectTypes(Good, allorsString).WithSingularName("ManufacturerId")  .WithPluralName("ManufacturerIds")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("acbe2dc6-63ad-4910-9752-4cab83e24afb"), new Guid("70d193cf-8985-4c25-84a5-31f4e2fd2a34"), new Guid("73361510-c5a2-4c4f-afe5-94d2b9eaeea3")).WithObjectTypes(Good, Product).WithSingularName("ProductSubstitution")  .WithPluralName("ProductSubstitutions")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e1ee15a9-f173-4d81-a11d-82abff076fb4"), new Guid("20928aed-02cc-4ea1-9640-cd31cb54ba13"), new Guid("e1c65763-9c2d-4111-bca1-638a69490e99")).WithObjectTypes(Good, Product).WithSingularName("ProductIncompatibility")  .WithPluralName("ProductIncompatibilities")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f52c0b7e-dbc4-4082-a2b9-9b1a05ce7179"), new Guid("50478ca9-3eb4-487b-8c8a-6ff48d9155b5"), new Guid("802b6cdb-873a-4455-9fa7-7f2267407f0f")).WithObjectTypes(Good, Media).WithSingularName("Photo")  .WithPluralName("Photos")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // AccountingTransactionDetail
            new RelationTypeBuilder(domain, new Guid("63447735-fdfc-4f32-ab89-d848903d71eb"), new Guid("8207e741-2250-4b1a-a6aa-86ca531c9d7c"), new Guid("6bba133d-e323-4acd-a26e-2cb4ee6d8821")).WithObjectTypes(AccountingTransactionDetail, AccountingTransactionDetail).WithSingularName("AssociatedWith")  .WithPluralName("AssociatedWiths")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("644b956b-58e3-465b-b431-5637d9a209e5"), new Guid("1d6d156a-ac84-4453-9efc-40840539d48b"), new Guid("8cdd505a-f5f9-4e85-a49d-d88673f08004")).WithObjectTypes(AccountingTransactionDetail, OrganisationGlAccountBalance).WithSingularName("OrganisationGlAccountBalance")  .WithPluralName("OrganisationGlAccountBalances")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9b5a3978-9859-432a-939b-73838c2bb3b2"), new Guid("bc276111-7fc2-4ae0-a3d8-ac9af05229b2"), new Guid("f2f407bb-7350-4d02-95ea-e35e680a352d")).WithObjectTypes(AccountingTransactionDetail, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d04a0632-e5ec-4a06-bc57-413cf58d2086"), new Guid("e874eb66-e9d3-4bb5-bac0-b322d3db4fd5"), new Guid("c457a4ce-368f-4956-8749-4a66d703c59b")).WithObjectTypes(AccountingTransactionDetail, allorsBoolean).WithSingularName("Debit")  .WithPluralName("Debits")      .Build();
			
            // County
            new RelationTypeBuilder(domain, new Guid("89a67d5c-8f78-41aa-9152-91f8496535bc"), new Guid("93664b6a-08d3-48b7-aada-214db9d19cb8"), new Guid("20477c4e-3c7f-4239-a5ae-313465682966")).WithObjectTypes(County, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("926ce4e6-cc76-4005-964f-f4d5af5fe944"), new Guid("71bf2977-eb86-4c5d-84f3-7ee97412e460"), new Guid("66743b3b-180e-4a8d-baec-b728fd4ed29c")).WithObjectTypes(County, State).WithSingularName("State")  .WithPluralName("States")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PostalBoundary
            new RelationTypeBuilder(domain, new Guid("2edd7f54-5596-46c1-9f8a-813c947d95fb"), new Guid("61f54d7a-9ad7-447e-ae79-227833f2473c"), new Guid("68f52b6f-6feb-4e22-ae1a-8ef8334c578f")).WithObjectTypes(PostalBoundary, allorsString).WithSingularName("PostalCode")  .WithPluralName("PostalCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7166cc1b-1f00-4cef-9875-8092cd4a76a0"), new Guid("cb2ca991-e054-44af-b6d1-d860072a0859"), new Guid("dea67366-e6ec-4f64-b450-68c6bae4fec7")).WithObjectTypes(PostalBoundary, allorsString).WithSingularName("Locality")  .WithPluralName("Localities")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("c0e1c31b-5506-48c0-b46f-239f89eca08f"), new Guid("09a54b9f-1461-4956-ba7a-fc6f086abf77"), new Guid("226183cc-ae5d-4292-982b-aba15304ab70")).WithObjectTypes(PostalBoundary, Country).WithSingularName("Country")  .WithPluralName("Countries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d92c5fd4-68e9-402b-b540-86053df1b70d"), new Guid("ce1593e7-a08d-43f3-a6af-ea5800ff9d3b"), new Guid("f35bdd80-6821-4d72-8cd7-a8f4d0542fc4")).WithObjectTypes(PostalBoundary, allorsString).WithSingularName("Region")  .WithPluralName("Regions")      .WithSize(256).Build();
			
            // ProductCategory
            new RelationTypeBuilder(domain, new Guid("049849d5-514b-418d-8397-29db6671b4fa"), new Guid("51631226-3c9e-46a5-9748-b9ab44e36173"), new Guid("11d9ba5c-1012-4442-893b-223d21ba7df7")).WithObjectTypes(ProductCategory, Package).WithSingularName("Package")  .WithPluralName("Packages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("22b7b6ef-7adf-424d-a675-d5338478ed44"), new Guid("b80ca91e-846f-4af6-a3a7-b361ef7b6058"), new Guid("55f938db-31e5-468c-90ad-1f7db319afce")).WithObjectTypes(ProductCategory, allorsString).WithSingularName("Code")  .WithPluralName("Codes")    .WithIsIndexed(true)  .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("25367f24-0f84-44f2-adce-ea3c082b6449"), new Guid("729444e1-4cdb-4090-a83b-bfff6d72ac95"), new Guid("956368ba-ca75-4e93-be53-6c0cfc41d704")).WithObjectTypes(ProductCategory, Media).WithSingularName("NoImageAvailableImage")  .WithPluralName("NoImageAvailableImages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2dcea42e-2c3d-483c-b514-b7bd418318ab"), new Guid("98564463-d7a9-4605-997c-2ceacb5c3302"), new Guid("f8ad2d5e-eab0-4c5b-8cb7-35b3439e62e6")).WithObjectTypes(ProductCategory, ProductCategory).WithSingularName("Parent")  .WithPluralName("Parents")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6ad49c7d-8c4e-455b-8073-a5ef72e92725"), new Guid("a1d92298-5c2e-42eb-bf1b-1e15a07f1eac"), new Guid("6b6cf3e5-c1ca-4502-ad27-85c33db1f183")).WithObjectTypes(ProductCategory, ProductCategory).WithSingularName("Child")  .WithPluralName("Children")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("743985f3-cfee-45b5-b971-30adf46b5297"), new Guid("9bc06415-c87c-44ab-8644-6a3d53595bd1"), new Guid("22e25946-7262-4fc3-a6ee-d9a25494298a")).WithObjectTypes(ProductCategory, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("8af8b1b1-a711-4e98-a6a0-2948f2d1f315"), new Guid("042e65b2-6df9-4e76-91bd-7766e935cbfe"), new Guid("991971a4-4ced-4cad-a7a5-48cde31f5e95")).WithObjectTypes(ProductCategory, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("9f50cbbc-d0af-46e6-8e04-2bfb0bf1facf"), new Guid("4fe64d4c-747c-4e8f-a657-8174eb8e0b73"), new Guid("bdd11ee4-ade5-46f3-a2b1-2fbb0261ae14")).WithObjectTypes(ProductCategory, Media).WithSingularName("CategoryImage")  .WithPluralName("CategoryImages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b02c92d3-8b3a-4ce0-a49d-5c608a25b7d4"), new Guid("b01ed533-259c-429c-8827-c61222896b8f"), new Guid("7efeb782-6278-4482-8cbb-b46d2a146e96")).WithObjectTypes(ProductCategory, ProductCategory).WithSingularName("Ancestor")  .WithPluralName("Ancestors")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
			
            // CountryBound
            new RelationTypeBuilder(domain, new Guid("095460a7-fffa-4c94-8b51-a4fd9fb80a2e"), new Guid("f5aa22da-64f3-447a-864c-4db5b77d221b"), new Guid("799ab886-ce30-4270-8293-6c302d17e3e3")).WithObjectTypes(CountryBound, Country).WithSingularName("Country")  .WithPluralName("Countries")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // VatForm
            new RelationTypeBuilder(domain, new Guid("180f9887-5973-4c4a-9277-a383e4f66bc6"), new Guid("db1bf9e9-dc26-40e1-aa5d-c863955e2947"), new Guid("5a3a106c-8a5e-4a4b-b86e-311853aa4353")).WithObjectTypes(VatForm, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("f3683ece-e247-490f-be4f-4fe12e5cfd06"), new Guid("e8a9518b-d33b-4db5-ac01-6283028a7e1f"), new Guid("657b667e-cd15-4671-bc18-9f49c8aa04e6")).WithObjectTypes(VatForm, VatReturnBox).WithSingularName("VatReturnBox")  .WithPluralName("VatReturnBoxes")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // BudgetRevisionImpact
            new RelationTypeBuilder(domain, new Guid("16b0c91f-5746-4ebe-a071-7c42887cccb1"), new Guid("d8f69482-e661-4447-b055-7f3806cace95"), new Guid("ca98b59f-ac83-457f-a5fd-24b35347ea14")).WithObjectTypes(BudgetRevisionImpact, BudgetItem).WithSingularName("BudgetItem")  .WithPluralName("BudgetItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("55e9b1e3-0545-471e-97b0-07d8968629c2"), new Guid("87269928-a93d-43b3-82d5-4d26d771b113"), new Guid("972f8cb7-72bf-42bd-bf7f-405cbe9f8497")).WithObjectTypes(BudgetRevisionImpact, allorsString).WithSingularName("Reason")  .WithPluralName("Reasons")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("6b3a80c1-eff1-478c-a54e-4912bc4a1242"), new Guid("c8f87804-9940-491d-a6aa-3b4dd888a016"), new Guid("d187ff95-ee86-4d13-90b2-64adebc19be7")).WithObjectTypes(BudgetRevisionImpact, allorsBoolean).WithSingularName("Deleted")  .WithPluralName("DeletedPlural")      .Build();
            new RelationTypeBuilder(domain, new Guid("7d0ad499-1e3d-41cd-bc6c-79aac1a7fa57"), new Guid("d409452f-bd4f-4c71-b71b-8512068d3ce8"), new Guid("ba16d574-8bea-45b8-9da0-f9f14f21ca5f")).WithObjectTypes(BudgetRevisionImpact, allorsBoolean).WithSingularName("Added")  .WithPluralName("AddedPlural")      .Build();
            new RelationTypeBuilder(domain, new Guid("80106b6d-8e1d-4db1-a4eb-71a56e9a4c94"), new Guid("81e2607d-d1fc-475d-8a19-b60c34fae7f9"), new Guid("80d4b2a2-f67e-4e76-b527-5b2d067a0499")).WithObjectTypes(BudgetRevisionImpact, allorsDecimal).WithSingularName("RevisedAmount")  .WithPluralName("RevisedAmounts")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b93df76d-439a-45cf-885d-4887afe5fd6f"), new Guid("ed0a9f21-20e3-4f26-a020-5b0afc8ec335"), new Guid("dd1b9041-cff6-4d03-9215-d963a1c2a992")).WithObjectTypes(BudgetRevisionImpact, BudgetRevision).WithSingularName("BudgetRevision")  .WithPluralName("BudgetRevisions")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Budget
            new RelationTypeBuilder(domain, new Guid("1848add9-ab90-4191-b7f1-eb392be3ec4e"), new Guid("8232c215-e592-4ec7-8c44-391c917b7e89"), new Guid("5e27d83d-a601-4101-b4dd-7eef98de82e8")).WithObjectTypes(Budget, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("1c3dd3b4-b514-4a42-965f-d3200325d78c"), new Guid("dccc1ed1-0cac-4e25-a7ee-5848af5b390e"), new Guid("684c491e-c764-4d83-a11f-d3cf80d671ad")).WithObjectTypes(Budget, BudgetRevision).WithSingularName("BudgetRevision")  .WithPluralName("BudgetRevisions")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2163a044-c967-4137-b1d0-dfd3fac80869"), new Guid("3ec284eb-944c-4ff0-8e24-9be0ceeda22a"), new Guid("f02d72a7-2547-44dc-bb8b-42e58afe186d")).WithObjectTypes(Budget, BudgetStatus).WithSingularName("BudgetStatus")  .WithPluralName("BudgetStatuses")  .WithCardinality(Cardinalities.OneToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("494d04ef-aafc-4482-a5c2-4ec9fa93d158"), new Guid("eda25f81-bba9-4e23-9074-4e22338ace23"), new Guid("d2a2990a-2966-4302-8c18-0884915f9d33")).WithObjectTypes(Budget, allorsString).WithSingularName("BudgetNumber")  .WithPluralName("BudgetNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("59cbc253-e17d-4405-bea8-09ad420bf8bc"), new Guid("6f6d9d35-daf5-4a79-85ce-d662cd7ec2d4"), new Guid("a6ec675f-c28a-470e-9923-e623e0ca9c58")).WithObjectTypes(Budget, BudgetObjectState).WithSingularName("CurrentObjectState")  .WithPluralName("CurrentObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5bb46810-419d-4b1e-be3a-a360e7e35ffd"), new Guid("c7e6b216-0292-4079-a0dc-5f87e86d4f95"), new Guid("d8fabb81-7e41-4efa-b008-2ace24694d36")).WithObjectTypes(Budget, BudgetObjectState).WithSingularName("PreviousObjectState")  .WithPluralName("PreviousObjectStates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("834432b1-65b2-4499-a83d-71f0db6e177b"), new Guid("b7f09631-6b4c-417d-ba12-115d07d9d6f5"), new Guid("b9ba1402-ce06-4bdd-9290-165ff8e555d2")).WithObjectTypes(Budget, BudgetReview).WithSingularName("BudgetReview")  .WithPluralName("BudgetReviews")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d4d205b5-6f23-41f5-93fc-08d4d9ad0727"), new Guid("181fd812-5a57-44d5-92c7-70755df1c9e3"), new Guid("516885f8-6aee-4e63-bd38-3134ed753e28")).WithObjectTypes(Budget, BudgetStatus).WithSingularName("CurrentBudgetStatus")  .WithPluralName("CurrentBudgetStatuses")  .WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f6078f5b-036f-45de-ab4f-fb26b6939d11"), new Guid("ba8edec9-a429-482d-bfbd-4f7fd419eaf7"), new Guid("9b9e4779-bb7d-4edb-b432-eab76472135a")).WithObjectTypes(Budget, BudgetItem).WithSingularName("BudgetItem")  .WithPluralName("BudgetItems")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
			
            // WebSiteCommunication
            new RelationTypeBuilder(domain, new Guid("18faf993-316a-4990-8ffd-8bda40f61164"), new Guid("b6c8df26-71f6-49a8-86d0-f38b9717fdc4"), new Guid("96f92902-be8e-41f8-893a-afe4e93ef6d5")).WithObjectTypes(WebSiteCommunication, Party).WithSingularName("Originator")  .WithPluralName("Originators")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("39077571-13b2-4cc4-be85-517dbc11703e"), new Guid("be25f23d-6c17-4940-abe6-b6936244bcea"), new Guid("f956749a-b0b3-45a4-a4b8-b0bf913d24c2")).WithObjectTypes(WebSiteCommunication, Party).WithSingularName("Receiver")  .WithPluralName("Receivers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Withdrawal
            new RelationTypeBuilder(domain, new Guid("b97344ac-a848-4ee0-bdb5-a9d79bb785fc"), new Guid("265511f9-0f02-47c8-b7c4-392f09a69fa2"), new Guid("c7dcd911-b352-4f0f-98fd-ea1c3d8d77d6")).WithObjectTypes(Withdrawal, Disbursement).WithSingularName("Disbursement")  .WithPluralName("Disbursements")    .WithIsIndexed(true)  .Build();
			
            // Deployment
            new RelationTypeBuilder(domain, new Guid("212653db-1677-47bd-944c-b5468673ec63"), new Guid("7543cf10-97dd-4823-b386-f06379e398b2"), new Guid("685a54f0-4e66-4ce3-93a2-f6f45dcf8c8b")).WithObjectTypes(Deployment, Good).WithSingularName("ProductOffering")  .WithPluralName("ProductOfferings")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c322fbbd-3406-4e73-83ed-033282ab0cfb"), new Guid("d265b170-3854-4276-9a20-325984097991"), new Guid("501b64c8-4181-45ca-a4f3-075232c8b270")).WithObjectTypes(Deployment, DeploymentUsage).WithSingularName("DeploymentUsage")  .WithPluralName("DeploymentUsages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d588ba7f-7b67-43fd-bb67-b9ff82fcffaf"), new Guid("bbee5696-6e53-4ea3-8f57-4e018e6bc61d"), new Guid("33c8e0e5-be98-44bb-a9eb-cfbabd8451b2")).WithObjectTypes(Deployment, SerializedInventoryItem).WithSingularName("SerializedInventoryItem")  .WithPluralName("SerializedInventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PayCheck
            new RelationTypeBuilder(domain, new Guid("59ddff84-5e67-4210-b721-955e08f8453e"), new Guid("5d445586-f239-4e3b-a3cb-368d46df306f"), new Guid("9a3b62ee-6197-4670-ad8b-c01201ea2235")).WithObjectTypes(PayCheck, Deduction).WithSingularName("Deduction")  .WithPluralName("Deductions")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5db6f5b5-e24e-44fd-bc41-4e0466e97906"), new Guid("53d7d8c9-7028-4ec8-82af-6373e21e3532"), new Guid("c2e4cf65-7a57-4dcd-ab49-c6cbc6b9d9fb")).WithObjectTypes(PayCheck, Employment).WithSingularName("Employment")  .WithPluralName("Employments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // InventoryItemConfiguration
            new RelationTypeBuilder(domain, new Guid("92a85a6b-4f65-4ba4-bd5e-bf44d5a9ca56"), new Guid("e7e7fef5-a973-42b7-8c96-5ede712a353c"), new Guid("6beb2ac6-0319-4524-80a2-54393ba77e69")).WithObjectTypes(InventoryItemConfiguration, InventoryItem).WithSingularName("InventoryItem")  .WithPluralName("InventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f041b297-e2bb-4ada-ab89-08ec9bcd6513"), new Guid("6ec9252a-817c-4e39-a2f7-809c86888b9c"), new Guid("0454d8e0-ab31-4907-85b1-41103091a08f")).WithObjectTypes(InventoryItemConfiguration, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
            new RelationTypeBuilder(domain, new Guid("f1d4ceeb-f859-4996-babc-dc55837489e0"), new Guid("a0cb4a4e-322e-4f8c-b7b8-b171b1b0aaa5"), new Guid("df3d337b-4998-4604-961c-3c074f91cd1b")).WithObjectTypes(InventoryItemConfiguration, InventoryItem).WithSingularName("ComponentInventoryItem")  .WithPluralName("ComponentInventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // CustomerShipmentStatus
            new RelationTypeBuilder(domain, new Guid("591d3237-220b-4765-8001-4bc18ecd2d8c"), new Guid("2a2704d6-44f6-4e86-a8c9-407842b7eb83"), new Guid("fd56e773-b27b-4336-b1be-e262d1d26b41")).WithObjectTypes(CustomerShipmentStatus, CustomerShipmentObjectState).WithSingularName("CustomerShipmentObjectState")  .WithPluralName("CustomerShipmentObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("74e826e5-75d0-4e7d-b2fe-73a7c58e30ef"), new Guid("261ca695-c146-493d-b059-3836913268c4"), new Guid("eb029966-6353-401b-b24b-190460f0c035")).WithObjectTypes(CustomerShipmentStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
			
            // ExpenseEntry
            new RelationTypeBuilder(domain, new Guid("0bb04781-d5b4-455c-8880-b5bfbc9d69f8"), new Guid("cc956cd1-4910-4977-afc5-e76f8bb2dc16"), new Guid("821a410a-afa4-4e6e-b505-3732a864554a")).WithObjectTypes(ExpenseEntry, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // PartSpecificationStatus
            new RelationTypeBuilder(domain, new Guid("14fdbce7-3494-48fb-a885-3b688b0c4e15"), new Guid("9b060799-5fca-4e1f-96c0-953444f4b6ac"), new Guid("53c22224-4741-4b2a-ac1f-2174c1bda312")).WithObjectTypes(PartSpecificationStatus, allorsDateTime).WithSingularName("StartDateTime")  .WithPluralName("StartDateTimes")      .Build();
            new RelationTypeBuilder(domain, new Guid("3b3db2a8-bd50-422b-8605-01142cac2654"), new Guid("f3ae080e-2c22-46e2-9a78-2178f32eab55"), new Guid("a62f66c9-5ed6-4059-8c2e-3a01e268f4eb")).WithObjectTypes(PartSpecificationStatus, PartSpecificationObjectState).WithSingularName("PartSpecificationObjectState")  .WithPluralName("PartSpecificationObjectStates")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // DistributionChannelRelationship
            new RelationTypeBuilder(domain, new Guid("86a07419-5dfd-4618-a472-168ba5fdf3ff"), new Guid("2800f775-ce61-4684-b6a3-5ce28dcf140b"), new Guid("b61fdf73-2420-498c-af0b-49ecdeec359a")).WithObjectTypes(DistributionChannelRelationship, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("e7c812db-f6c8-431b-9f4d-5317a0d8673c"), new Guid("21844f4b-372c-45de-acfa-02c428afdbd8"), new Guid("00b349c4-e7f6-4d8f-b4d3-0922a3465a91")).WithObjectTypes(DistributionChannelRelationship, Organisation).WithSingularName("Distributor")  .WithPluralName("Distributors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PaymentMethod
            new RelationTypeBuilder(domain, new Guid("0b16fdbc-c535-45a5-8be9-7b1d2c12337a"), new Guid("0d9ba18d-46fa-4a98-aa6a-37261f2f11a8"), new Guid("7af97652-a1cf-49f8-a33c-33c45dcadd4e")).WithObjectTypes(PaymentMethod, allorsDecimal).WithSingularName("BalanceLimit")  .WithPluralName("BalanceLimits")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("2e5e9d24-4697-4811-8636-1ebf9d86b9c2"), new Guid("d479d315-4478-4b97-98c4-bfe964ca9921"), new Guid("bdbca6c9-6a5e-4700-a987-cd62db5b831a")).WithObjectTypes(PaymentMethod, allorsDecimal).WithSingularName("CurrentBalance")  .WithPluralName("CurrentBalances")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("36559f29-1182-42d1-831d-587103456ce6"), new Guid("ce33ccae-be2c-4081-abd5-be803bdbc1a4"), new Guid("0c76bfba-2ef1-46fb-bb1f-b49b57f792c0")).WithObjectTypes(PaymentMethod, Journal).WithSingularName("Journal")  .WithPluralName("Journals")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("386c301e-8f0f-48fc-8bec-10ac0df6be9d"), new Guid("168be3f3-97ef-490d-ab65-29c7928310cc"), new Guid("555b2755-24a1-4238-b390-f77e0fd205ac")).WithObjectTypes(PaymentMethod, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("59da5fc4-e861-4c7d-aa96-c15cebbb63f2"), new Guid("7e050127-bbea-490a-ac78-354c37daa799"), new Guid("e48560a6-b94d-459b-98d1-4bf429816798")).WithObjectTypes(PaymentMethod, OrganisationGlAccount).WithSingularName("GlPaymentInTransit")  .WithPluralName("GlsPaymentInTransit")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6e61f71f-77a1-4795-b876-ba5d74ebdc3e"), new Guid("ce1e09f8-1260-462d-be94-726a8716f6d8"), new Guid("108077d8-ca1c-48c1-8154-f52f7807eb5b")).WithObjectTypes(PaymentMethod, allorsString).WithSingularName("Remarks")  .WithPluralName("RemarksPlural")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("8b11feda-09c8-4f8d-a21d-dddd87531d5b"), new Guid("d7361d9b-b76c-4a22-a385-487219d861d5"), new Guid("2c58e2a1-c7bb-481e-a828-c5bfa0eaec49")).WithObjectTypes(PaymentMethod, OrganisationGlAccount).WithSingularName("GeneralLedgerAccount")  .WithPluralName("GeneralLedgerAccounts")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a937fc55-d737-444b-93b0-525994e09f6a"), new Guid("c33d3bc7-13ff-4d83-be7b-e9fbd7c21d55"), new Guid("2b00353d-bc87-4aa1-b260-3650f93320ff")).WithObjectTypes(PaymentMethod, SupplierRelationship).WithSingularName("Creditor")  .WithPluralName("Creditors")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c32243ac-8810-478b-b0f4-11a1fe4773bd"), new Guid("433b6034-88a1-4355-81cd-dbd92ef6f7da"), new Guid("238a0b8f-882a-47ea-96cc-ff19126974c1")).WithObjectTypes(PaymentMethod, allorsBoolean).WithSingularName("IsActive")  .WithPluralName("IsActives")      .Build();
			
            // OrderItem
            new RelationTypeBuilder(domain, new Guid("05254848-d99a-430e-80cd-e042ded3de71"), new Guid("b10824dd-70de-4fe1-bdc6-d970ebe33e4a"), new Guid("c3de2ade-8b8b-4423-b40b-fa665a2d6215")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalDiscountAsPercentage")  .WithPluralName("TotalDiscountsAsPercentage")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("0dc8733d-816a-4231-8a56-24363923080f"), new Guid("f41fe55a-b9f4-4a81-a7c6-cffb5e3e8cc1"), new Guid("8aa28a5f-d801-4751-b37a-435b461b1b54")).WithObjectTypes(OrderItem, DiscountAdjustment).WithSingularName("DiscountAdjustment")  .WithPluralName("DiscountAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("131359fb-29f2-4ebb-adc2-1e53a99a4e6b"), new Guid("e687dc65-d903-47c4-8e39-ad43f8d5633e"), new Guid("a1031b6a-897b-43e4-99c9-0308acbe708b")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("UnitVat")  .WithPluralName("UnitsVat")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("27534e6f-55d3-45e3-82ba-1580af4647d6"), new Guid("cda2b9ab-8d74-471c-95ea-38fb0c4a7589"), new Guid("32f1c0f7-59d6-4c2c-a39e-607d359b6f53")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalVatCustomerCurrency")  .WithPluralName("TotalVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("27f86828-7b4e-4d80-9c3c-095813240c1a"), new Guid("628cb976-30ef-42fd-be72-282b0f291bb2"), new Guid("b78ae277-ca2b-43ff-a730-257281533822")).WithObjectTypes(OrderItem, VatRegime).WithSingularName("VatRegime")  .WithPluralName("VatRegimes")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("30493d04-3298-4888-8ee4-b8995d9cd5a1"), new Guid("0ab1707d-be04-49c2-a6b1-b6a17eb0a195"), new Guid("95bd36e9-a956-46d2-b2b5-7d7d0f73c411")).WithObjectTypes(OrderItem, BudgetItem).WithSingularName("BudgetItem")  .WithPluralName("BudgetItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("32792771-06c8-4805-abc4-2e2f9c37c6f3"), new Guid("8f165d5a-ad87-431f-bea6-b8531f78d731"), new Guid("dda45a1d-2377-40ad-88e7-0a8961c1f1e1")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalVat")  .WithPluralName("TotalVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("3722807a-0634-4df2-8964-4778b4edc314"), new Guid("091b8400-d566-472d-a804-a55bfd99ff92"), new Guid("7dcca5fa-73b5-4751-9d16-b05e6e5ef5b7")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("UnitSurcharge")  .WithPluralName("UnitSurcharges")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("37623a64-9f6c-4f35-8e72-c4332037db4d"), new Guid("c99eecb1-6b8a-4f44-999d-35b32ea93605"), new Guid("cc34ad5e-43bd-4e4a-bd9a-afdb64a409ae")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("UnitDiscount")  .WithPluralName("UnitDiscounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("38cd5e9e-7305-4c56-bff7-13918bd9f059"), new Guid("d21a1eff-5920-4dbb-9fcb-8f99ea1187f9"), new Guid("a6e6c1d9-0009-4d5a-bebd-6e62c1d71a5a")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("PreviousQuantity")  .WithPluralName("PreviousQuantities")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("454f28cf-bf52-4465-83e4-e871ec36c491"), new Guid("5abe5891-40b6-4b87-a587-e6a2c7658c64"), new Guid("c6d6c5dd-9239-45ae-970c-3716443bed29")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("QuantityOrdered")  .WithPluralName("QuantitiesOrdered")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("47839962-efc3-4def-be94-4a5831c3a629"), new Guid("061745f4-94f0-4370-ad60-d08e46d6d474"), new Guid("eef09133-a624-455e-81f4-7c84ea41c931")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalExVatCustomerCurrency")  .WithPluralName("TotalExVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("5367e41e-b1c3-4311-87b4-6ba2732de1e6"), new Guid("1f602ef8-dfa3-45f6-8577-e6256206bf94"), new Guid("bf829774-be83-4c46-9174-dfeee0eb1fd7")).WithObjectTypes(OrderItem, VatRate).WithSingularName("DerivedVatRate")  .WithPluralName("DerivedVatRates")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5ffe1fc4-704e-4a3f-a27f-d4a47c99c37b"), new Guid("7996c255-663f-462e-bb23-ae61a55a3a48"), new Guid("827f06dd-fc4a-4323-9120-49f9a1ae9abf")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("ActualUnitPrice")  .WithPluralName("ActualUnitPrices")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6d1a448e-112a-4513-87b8-fd4e5bd03dac"), new Guid("0fe2ff69-d63c-4361-acb3-4fd10ddf30bc"), new Guid("f38daa9a-0303-4dba-9c92-8483f8a134c4")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalIncVatCustomerCurrency")  .WithPluralName("TotalIncVatsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6da42dec-ba03-4615-badb-9113a82ff2f7"), new Guid("f8b1946c-f4d3-4c9b-89c3-371b8ce1e329"), new Guid("29fa13ea-307f-49ed-86ad-ff8321911013")).WithObjectTypes(OrderItem, allorsString).WithSingularName("Description")  .WithPluralName("Descriptions")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6dc95126-3287-46e0-9c21-4d6561262a2e"), new Guid("f041eec1-749c-4b73-a01a-c3d692a9d9db"), new Guid("cf6e5ebb-4f3b-42e7-b6a3-486de6ca6d53")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("UnitBasePrice")  .WithPluralName("UnitBasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("70f92965-d99a-4a6a-bc27-029eec7b5c2d"), new Guid("93ffaaa6-7401-4b7f-a297-081a98bee032"), new Guid("233fd990-758e-4f1d-87bd-ae3de8d9486b")).WithObjectTypes(OrderItem, PurchaseOrder).WithSingularName("CorrespondingPurchaseOrder")  .WithPluralName("CorrespondingPurchaseOrders")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7595b52c-012b-42db-9cf2-46939b39f57f"), new Guid("93e899b5-b472-4aea-9f7c-d0863883abb1"), new Guid("c5f0b047-8a9b-4743-bac6-0d358b54a794")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("CalculatedUnitPrice")  .WithPluralName("CalculatedUnitPrice")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("84faada9-1bdc-4c08-8892-760eb0cee2ba"), new Guid("3166f432-b675-474a-9a1f-7e558cc1dc58"), new Guid("a2a2b2e8-0477-4b79-8602-7ca37fb17372")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalOrderAdjustmentCustomerCurrency")  .WithPluralName("TotalOrderAdjustmentsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8f06f480-ff7e-4e34-bb7e-6f1271dcc551"), new Guid("dcdcdd88-63b5-4680-80a7-e915abe1cc98"), new Guid("8b0d5be1-a4ed-49de-8913-bdafc5da57ae")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalOrderAdjustment")  .WithPluralName("TotalOrderAdjustments")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8f3d28ac-7693-4943-9398-a30f3f957283"), new Guid("035e178a-7de0-45c8-a4c1-269eee4c3f0c"), new Guid("3ed702c2-bf3c-472b-8a00-829c0853b7f7")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalSurchargeCustomerCurrency")  .WithPluralName("TotalSurchargesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("9674f349-3fcc-495c-b7eb-27b5b580597c"), new Guid("d7c3f753-9db0-4bb5-9b5d-4adbc695e320"), new Guid("34e4011b-c124-4d53-ab30-26734e8ba04c")).WithObjectTypes(OrderItem, QuoteItem).WithSingularName("QuoteItem")  .WithPluralName("QuoteItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9dc87cdb-a35f-4a48-9f99-bf0fe07cad5c"), new Guid("b6f17e6b-f61a-4155-8e4c-79ebec1a01d4"), new Guid("9ec4f475-ecb7-4d57-a642-043b0a703094")).WithObjectTypes(OrderItem, allorsDateTime).WithSingularName("AssignedDeliveryDate")  .WithPluralName("AssignedDeliveryDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("a1769a74-d832-4ade-be59-a98b17033ca1"), new Guid("72f9c5a1-a66a-4181-b683-c0546f7cb95d"), new Guid("279735e0-974a-46b3-b460-2bd528895f5a")).WithObjectTypes(OrderItem, allorsDateTime).WithSingularName("DeliveryDate")  .WithPluralName("DeliveryDates")  .WithIsDerived(true)    .Build();
            new RelationTypeBuilder(domain, new Guid("a271f7d4-cda1-4ae9-94e4-dda482bd8cd5"), new Guid("68f757fc-3cf5-4dae-b6ba-e2cb08033381"), new Guid("b856b6e9-93e8-47ac-8070-3bb8c0ff29d7")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalIncVat")  .WithPluralName("TotaIIncVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a573b8bf-42a6-4389-9f46-1def243220bf"), new Guid("d70fe012-fbfb-486c-8ac7-ac3ae9ea380f"), new Guid("6bff9fb4-7b17-4c5a-a7cb-fa8bd1bf9f5c")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalSurchargeAsPercentage")  .WithPluralName("TotalSurchargesAsPercentage")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("a819e4fe-f829-4e1c-9e93-46d9c4b31bd4"), new Guid("1f3b767e-58ee-483d-aa18-ee5c7421a244"), new Guid("b0b1feaa-279f-488a-9062-e0b1ff2dcd7c")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalDiscountCustomerCurrency")  .WithPluralName("TotalDiscountsCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b4398edb-2a36-459d-95a1-5d209462ae02"), new Guid("82b15a97-315c-4671-a420-f1b4f50f7ce6"), new Guid("b561ab6f-8843-4409-ac10-accb4b6d123e")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalDiscount")  .WithPluralName("TotalDiscounts")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b81633d1-5b22-42b9-a484-d401d06022fb"), new Guid("17f4d6e4-fe43-46d4-b28d-651e6e766713"), new Guid("e897b861-0e8a-4b57-ae52-b875d2f67c39")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalSurcharge")  .WithPluralName("TotalSurcharges")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("b82c7b21-5ade-40b6-ba5d-62b6384eaaec"), new Guid("0f950d26-6a3f-4140-9273-7fe886f06582"), new Guid("4aefa88c-6ce9-441e-a8a7-e65b2271c3b9")).WithObjectTypes(OrderItem, OrderTerm).WithSingularName("OrderTerm")  .WithPluralName("OrderTerms")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c897fe12-da96-47e6-b00e-920cb9e1c790"), new Guid("40f8d741-df32-487e-8ca5-2764dcaa2200"), new Guid("081c9f92-53c0-448a-bc8c-b19335f43da4")).WithObjectTypes(OrderItem, VatRegime).WithSingularName("AssignedVatRegime")  .WithPluralName("AssignedVatRegimes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ce398ebb-3b1e-476e-afd5-d32518542b70"), new Guid("d7f29cb6-bae1-41ce-bc67-c37c38f0ba73"), new Guid("49a61617-586a-4b7b-bcc0-e2cf1f4cdee4")).WithObjectTypes(OrderItem, allorsString).WithSingularName("ShippingInstruction")  .WithPluralName("ShippingInstructions")      .WithSize(-1).Build();
            new RelationTypeBuilder(domain, new Guid("d0b1e607-07dc-43e2-a003-89559c87a441"), new Guid("610d7c57-41eb-436f-b3ac-652798619441"), new Guid("f17ef23b-b1c8-43e0-8adb-e605f7fef7ba")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalBasePrice")  .WithPluralName("TotalBasePrices")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("dadeac55-1586-47ce-9983-2113179e275d"), new Guid("f6bdee3b-d274-4bd6-841e-7dc3d373083f"), new Guid("6a038221-f3ec-4fd0-a235-7f6205404113")).WithObjectTypes(OrderItem, OrderItem).WithSingularName("Association")  .WithPluralName("Associations")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("dc71aecf-1735-4858-b74f-65e805565eed"), new Guid("e16d7c3d-628a-438f-b141-102d3d508380"), new Guid("746ae197-8f1c-4631-8dcc-a7c9328f41e8")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalExVat")  .WithPluralName("TotalExVats")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("dc7d46b9-7c46-42bf-b8ac-20065a958c51"), new Guid("d935d887-08c0-499b-be79-37973dac97e5"), new Guid("edfe7ff0-3a6d-40b4-ab75-86d4b231de95")).WithObjectTypes(OrderItem, allorsDecimal).WithSingularName("TotalBasePriceCustomerCurrency")  .WithPluralName("TotalBasePricesCustomerCurrency")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("dcc8e49e-5770-4686-8f3c-ecedf5bbbfed"), new Guid("b855278d-96ab-486d-b12b-71e2ffe8353d"), new Guid("1bdfc536-bdcc-41dc-b7d3-357c4bcc24cf")).WithObjectTypes(OrderItem, PriceComponent).WithSingularName("CurrentPriceComponent")  .WithPluralName("CurrentPriceComponents")  .WithCardinality(Cardinalities.ManyToMany).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fa02ba8e-24a6-45ca-acfc-9ef69301efa2"), new Guid("fc87d284-a120-43fa-86eb-f4aea034cbf4"), new Guid("97d6f184-64c7-4ec7-953e-7ff587cd29af")).WithObjectTypes(OrderItem, SurchargeAdjustment).WithSingularName("SurchargeAdjustment")  .WithPluralName("SurchargeAdjustments")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("feeed27a-c421-476c-b233-02d2fb9db76d"), new Guid("d1458a15-e035-4b07-a6b8-5a9af704a4ac"), new Guid("34e046c2-881c-43e7-8c67-c14c595ac074")).WithObjectTypes(OrderItem, allorsString).WithSingularName("Message")  .WithPluralName("Messages")      .WithSize(-1).Build();
			
            // WorkEffortInventoryAssignment
            new RelationTypeBuilder(domain, new Guid("0bf425d4-7468-4e28-8fda-0b04278cb2cd"), new Guid("2c6841c6-c161-48e0-a257-d932d99ae7b4"), new Guid("1afed0f6-15fa-4fd2-91f5-648773933e3b")).WithObjectTypes(WorkEffortInventoryAssignment, WorkEffort).WithSingularName("Assignment")  .WithPluralName("Assignments")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5fcdb553-4b8f-419b-9f12-b9cefa68d39f"), new Guid("dba27480-4d2f-4e69-af01-4e9afba2cc98"), new Guid("3f7a72a4-2727-4dd6-a602-60ef9b6896af")).WithObjectTypes(WorkEffortInventoryAssignment, InventoryItem).WithSingularName("InventoryItem")  .WithPluralName("InventoryItems")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("70121570-c02d-4977-80e4-23e14cbc3fc9"), new Guid("b4224775-005c-4078-a5b6-2b8a60bc143a"), new Guid("c82f1c25-9c42-4d38-8fae-f8790e2333ef")).WithObjectTypes(WorkEffortInventoryAssignment, allorsInteger).WithSingularName("Quantity")  .WithPluralName("Quantities")      .Build();
			
            // City
            new RelationTypeBuilder(domain, new Guid("05ea705c-9800-4442-a684-b8b4251b51ed"), new Guid("a584625d-889d-4943-a130-fab2697def9f"), new Guid("889ccbe9-96a3-4d8e-9b8c-a1877ab89255")).WithObjectTypes(City, allorsString).WithSingularName("Name")  .WithPluralName("Names")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("559dd596-e784-4067-a993-b651ac17329d"), new Guid("06cc0af4-6bb9-4a86-a3e9-496f36002c92"), new Guid("89811da3-093a-42fe-8142-60692f1c3f05")).WithObjectTypes(City, State).WithSingularName("State")  .WithPluralName("States")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // MaterialsUsage
            new RelationTypeBuilder(domain, new Guid("a244ab38-6469-4aa4-ae7e-c245f17f2368"), new Guid("719acc0e-aaa9-465a-a08a-a283635cf48c"), new Guid("441feb11-9913-4c2d-a27f-01f0c4ed27ae")).WithObjectTypes(MaterialsUsage, allorsDecimal).WithSingularName("Amount")  .WithPluralName("Amounts")      .WithPrecision(19).WithScale(2).Build();
			
            // WorkRequirement
            new RelationTypeBuilder(domain, new Guid("b2d15c8b-a739-4c9d-bc16-eff5e6ca112e"), new Guid("94c8458e-e890-46b0-bdd4-dbfcb9877ded"), new Guid("22899775-0083-4171-801f-9396c9ba16a3")).WithObjectTypes(WorkRequirement, FixedAsset).WithSingularName("FixedAsset")  .WithPluralName("FixedAssets")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c9b7298e-1a19-4805-94d6-a6a33acccce0"), new Guid("664c20b0-6cba-43f8-a52a-2655501b9348"), new Guid("9ae7027e-6541-41fd-bae0-6e61c424c864")).WithObjectTypes(WorkRequirement, Deliverable).WithSingularName("Deliverable")  .WithPluralName("Deliverables")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ef364ba6-62ed-40db-a580-cf7f6f473e27"), new Guid("beb281bd-199b-4416-bb38-7d21ec376398"), new Guid("8f541554-8bfa-404a-85e9-453f2809d4a4")).WithObjectTypes(WorkRequirement, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PhoneCommunication
            new RelationTypeBuilder(domain, new Guid("5e3c675b-b329-47a4-9d53-b0e95837a23b"), new Guid("16fa813c-15d6-4bfb-a7b3-c295efe47a1c"), new Guid("f9320b55-230d-4f10-9a1b-6960137326b7")).WithObjectTypes(PhoneCommunication, Person).WithSingularName("Receiver")  .WithPluralName("Receivers")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7a37ab85-222a-4d13-b832-b222faefcf39"), new Guid("79c04646-6f62-4867-9f89-f2ce1876e981"), new Guid("507e6ff3-3baa-4c77-b41b-4d1893443dc2")).WithObjectTypes(PhoneCommunication, Person).WithSingularName("Caller")  .WithPluralName("Callers")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // ProductDeliverySkillRequirement
            new RelationTypeBuilder(domain, new Guid("12c6abaf-a080-45f3-820d-b462978d2539"), new Guid("4a6bd8f2-ea2a-4e07-a018-4b4b37b45a96"), new Guid("3e12bb69-b0bb-40ba-a987-89f5cc40c436")).WithObjectTypes(ProductDeliverySkillRequirement, allorsDateTime).WithSingularName("StartedUsingDate")  .WithPluralName("StartedUsingDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("5a52b67e-23e4-45ac-a1d4-cb083bf897cc"), new Guid("7de9d895-a524-4fc2-a5f4-7b9e78921d6c"), new Guid("b44bf4b9-3aa8-4abb-b145-11d888bf55c5")).WithObjectTypes(ProductDeliverySkillRequirement, Service).WithSingularName("Service")  .WithPluralName("Services")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6d4ec793-41a7-4044-9744-42d1bd44bbd4"), new Guid("fe73df0c-f46c-42e6-8274-a5de09de72d5"), new Guid("2c2f1476-48a5-45f4-86df-03a86a965af4")).WithObjectTypes(ProductDeliverySkillRequirement, Skill).WithSingularName("Skill")  .WithPluralName("Skills")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // SalesRepProductCategoryRevenue
            new RelationTypeBuilder(domain, new Guid("30a0a0de-0c74-494c-bf21-ebf13238dd61"), new Guid("e4e3c7ad-970d-4439-ba86-81be13a05dd8"), new Guid("ab9e97c7-3c5c-4fdb-a36e-637875cb714d")).WithObjectTypes(SalesRepProductCategoryRevenue, allorsInteger).WithSingularName("Month")  .WithPluralName("Months")      .Build();
            new RelationTypeBuilder(domain, new Guid("59d7cb27-e752-405b-9515-7db04aa37da7"), new Guid("348f4a59-ef85-47b1-af43-05ed9982c594"), new Guid("61a193c4-66ae-48ff-b810-437b9812e77f")).WithObjectTypes(SalesRepProductCategoryRevenue, allorsString).WithSingularName("SalesRepName")  .WithPluralName("SalesRepNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("6ee43a8c-8f42-491d-a0ab-3ea5d4352dc8"), new Guid("9a6c58ab-5119-47f6-974d-fa01fbb3d320"), new Guid("4d85870c-75ec-4153-894a-30a6cb253060")).WithObjectTypes(SalesRepProductCategoryRevenue, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("7f39eefb-9210-4796-9b91-dc4d5e0b4ea1"), new Guid("f35dfa6d-eca3-49b9-8b83-cd152b9be673"), new Guid("db1bd6a7-a47c-45a3-8e6d-767a4a8e06a5")).WithObjectTypes(SalesRepProductCategoryRevenue, ProductCategory).WithSingularName("ProductCategory")  .WithPluralName("ProductCategories")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("92e8bbd9-fe5a-4053-aef3-593dbb13eac0"), new Guid("6e9761f4-fc3d-485a-9b45-32f60f993f3b"), new Guid("b57c833b-6083-4fc5-a6d5-1350caab9a22")).WithObjectTypes(SalesRepProductCategoryRevenue, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a875a617-4d86-4d53-bcb5-bc4b40963cb4"), new Guid("32f41de8-6ea6-4a34-a36c-bfc0b3e5ca77"), new Guid("4fc24ed9-01b7-4475-9309-fe3702031b63")).WithObjectTypes(SalesRepProductCategoryRevenue, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("e291d604-4afc-4d73-b28e-04a723a4747b"), new Guid("9a6dcfe5-c728-4c33-a859-d0f5538790c3"), new Guid("be3e8061-a5b1-482f-9d0b-5a1b4774cc80")).WithObjectTypes(SalesRepProductCategoryRevenue, allorsInteger).WithSingularName("Year")  .WithPluralName("Years")      .Build();
            new RelationTypeBuilder(domain, new Guid("f978075e-05b8-4204-aa2f-97ab416fd3e8"), new Guid("0695bc15-2d76-4464-b61f-7627cf885ad3"), new Guid("52cfc399-8014-4b2e-94f0-bb67fec26e65")).WithObjectTypes(SalesRepProductCategoryRevenue, Person).WithSingularName("SalesRep")  .WithPluralName("SalesReps")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // PartyProductRevenueHistory
            new RelationTypeBuilder(domain, new Guid("317be52d-211d-4c11-8027-3cdffa9995e7"), new Guid("1359a604-a2ba-4291-84a9-82fe2f4d4108"), new Guid("4ba5744c-d4ed-4218-ac61-681681fbe80e")).WithObjectTypes(PartyProductRevenueHistory, allorsDecimal).WithSingularName("Revenue")  .WithPluralName("Revenues")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("8a0f314e-522a-43d2-a1a5-5c24ac611f68"), new Guid("4a0d36f0-57b7-4080-a03e-88bcfed45550"), new Guid("6e05bb8d-c23e-4e63-8dd0-25807023e7e9")).WithObjectTypes(PartyProductRevenueHistory, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("9cc5bc41-3dff-4628-aaa5-d6acaf245b5c"), new Guid("f9a1dcd6-b3f0-47d9-a17d-26c619c584b6"), new Guid("bd2426e5-d1ae-4e0e-bd87-a1721cd6bd2b")).WithObjectTypes(PartyProductRevenueHistory, Product).WithSingularName("Product")  .WithPluralName("Products")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a717227b-927e-4fde-aece-2c70773e64c7"), new Guid("a34ff46d-a9cf-4584-858e-141a872bf1c2"), new Guid("73be43b2-e675-4120-9a85-d5fc15cbe0d3")).WithObjectTypes(PartyProductRevenueHistory, allorsDecimal).WithSingularName("Quantity")  .WithPluralName("Quantities")      .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("d1703751-31aa-461c-af8a-7f52ac8bc96f"), new Guid("f1db8806-c9ee-4b0d-bb11-da333ecd6acf"), new Guid("640ce1ea-0fea-475f-bb07-a45c34350314")).WithObjectTypes(PartyProductRevenueHistory, InternalOrganisation).WithSingularName("InternalOrganisation")  .WithPluralName("InternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ffc08a0f-5abc-4f3c-9e8d-7ebb4b43a656"), new Guid("6bbf007d-4212-4437-b06e-daeb76566c23"), new Guid("7b3cc4f1-6b9d-438b-b82a-9701ecfbd716")).WithObjectTypes(PartyProductRevenueHistory, Currency).WithSingularName("Currency")  .WithPluralName("Currencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // StringTemplate
            new RelationTypeBuilder(domain, new Guid("a09af300-8983-4211-af75-3e04efa8ec36"), new Guid("c8d26b39-96a6-4be4-9675-5a6fdb54bae5"), new Guid("e40af32d-abdd-4d7b-9b3d-92abf7fb129a")).WithObjectTypes(StringTemplate, TemplatePurpose).WithSingularName("TemplatePurpose")  .WithPluralName("TemplatePurposes")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // Country
            new RelationTypeBuilder(domain, new Guid("13010743-231f-43a8-9539-b95b83ab15da"), new Guid("de4d0d90-e41b-4b7c-bcdc-23269020ab4e"), new Guid("5e2328a6-5413-401f-8106-7b8b29907b06")).WithObjectTypes(Country, VatRate).WithSingularName("VatRate")  .WithPluralName("VatRates")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("2ecb8cfb-011d-4c31-a9cd-ed5a13ae23a4"), new Guid("ebdfd8e3-9d24-4721-b72b-5a5e4327d62b"), new Guid("45aa4f50-a23b-4ce6-872f-d72b648e4e90")).WithObjectTypes(Country, allorsInteger).WithSingularName("IbanLength")  .WithPluralName("IbanLengths")      .Build();
            new RelationTypeBuilder(domain, new Guid("6553ee71-66dd-45f2-9de9-5656b011d2fc"), new Guid("0a5662c3-1f60-41d5-a703-638480cb3c15"), new Guid("a14f5154-bcf2-44f4-a49e-3c17aca71247")).WithObjectTypes(Country, allorsBoolean).WithSingularName("EuMemberState")  .WithPluralName("EuMemberStates")      .Build();
            new RelationTypeBuilder(domain, new Guid("7f0adb03-db73-44f2-a4a2-ece00f4908a2"), new Guid("081e6909-c744-4795-b587-82bbf938b5fe"), new Guid("38546e92-a238-4d72-a731-a3f91dbcc61f")).WithObjectTypes(Country, allorsString).WithSingularName("TelephoneCode")  .WithPluralName("TelephoneCodes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("a2aa65d7-e0ef-4f6f-a194-9aeb49a1d898"), new Guid("86d7d9a6-77fd-491b-b563-86b8d0c76ee4"), new Guid("4f6f041b-a1ea-47bc-92e4-650bddaa46ed")).WithObjectTypes(Country, allorsString).WithSingularName("IbanRegex")  .WithPluralName("IbanRegexes")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("b829da1c-2eb7-495b-a4a9-98e335cd87f9"), new Guid("a0377434-67ae-4ab4-90b3-99fb6bc2bf90"), new Guid("8a306049-a4b9-4489-a2b8-d627fa6444c3")).WithObjectTypes(Country, VatForm).WithSingularName("VatForm")  .WithPluralName("VatForms")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("c231ce68-bf03-4122-8699-c3c6473ab90a"), new Guid("153203db-be9a-4722-aab3-7163de779a2a"), new Guid("e72228ee-ae28-406c-b7ee-a9be1a4d3286")).WithObjectTypes(Country, allorsString).WithSingularName("UriExtension")  .WithPluralName("UriExtension")      .WithSize(256).Build();
			
            // Person
            new RelationTypeBuilder(domain, new Guid("348dd7c2-c534-422c-90aa-d48b1e504df9"), new Guid("7516fdc6-10c1-4f61-8a9f-f1d84b1a9899"), new Guid("f0306c09-0b6f-4e73-b789-47c3b3c2b0d6")).WithObjectTypes(Person, Salutation).WithSingularName("Salutation")  .WithPluralName("Salutations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("4a01889c-ed4f-41f5-8a25-f0e3bbeb095b"), new Guid("1282318d-0ac0-406b-868c-36176b4b0610"), new Guid("b62f0e23-6928-40b5-abc0-feac01a40e98")).WithObjectTypes(Person, allorsDecimal).WithSingularName("YTDCommission")  .WithPluralName("YTDCommissions")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("539b51e6-dd15-481c-86d3-ceb84588c078"), new Guid("280bf735-be99-4c2e-b867-efbf187d8a67"), new Guid("766470ee-34f8-4a49-8622-28e5f79bea72")).WithObjectTypes(Person, Citizenship).WithSingularName("Citizenship")  .WithPluralName("Citizenships")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5df11d30-c6e7-4778-890c-c24b162bd20a"), new Guid("da7d2f05-d84e-48c2-b2a8-b33c43f1345c"), new Guid("8db737e3-c93e-42b0-b5ac-0a7b64309b51")).WithObjectTypes(Person, Employment).WithSingularName("CurrentEmployment")  .WithPluralName("CurrentEmployment")  .WithCardinality(Cardinalities.ManyToOne).WithIsDerived(true)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("5f5d8dd2-33e6-4924-bae7-b6710a789ac9"), new Guid("007ba2c5-9fdd-425e-8842-27554cdbaf27"), new Guid("99b8085b-0ccf-44a3-a4d4-e1d091af8969")).WithObjectTypes(Person, allorsDecimal).WithSingularName("LastYearsCommission")  .WithPluralName("LastYearsCommissions")  .WithIsDerived(true)    .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("6d425613-b821-46f2-896a-a04dc4b377a3"), new Guid("18b225ef-df54-4fd1-9423-36d334d1d876"), new Guid("128f6659-8313-4c53-8ff5-eb1fcffd1b36")).WithObjectTypes(Person, PersonalTitle).WithSingularName("Title")  .WithPluralName("Titles")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("6f7b0a7f-0b8e-4fbe-b248-b7b90fb18613"), new Guid("33d02f85-4e00-40ef-821d-19278666b178"), new Guid("1e4214e8-f228-4f6e-8885-29a59fcd19f3")).WithObjectTypes(Person, allorsString).WithSingularName("MothersMaidenName")  .WithPluralName("MothersMaidenNames")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("7bcba7fd-6419-4324-8a11-c56bd46581a1"), new Guid("78ccda0f-4b17-40f1-93ad-b86e1181cb80"), new Guid("1babd38a-8a52-4a92-bb99-7a289d41bb1e")).WithObjectTypes(Person, allorsDateTime).WithSingularName("BirthDate")  .WithPluralName("BirthDates")      .Build();
            new RelationTypeBuilder(domain, new Guid("a2ace3b0-e38e-49c8-8c4b-0e97672830c4"), new Guid("e5e9e017-d642-4c03-97c0-f106aff2eff5"), new Guid("55ed88ed-5634-4267-961f-c75469302637")).WithObjectTypes(Person, allorsDecimal).WithSingularName("Height")  .WithPluralName("Heights")    .WithIsIndexed(true)  .WithPrecision(19).WithScale(2).Build();
            new RelationTypeBuilder(domain, new Guid("ab9b5c70-3d58-4e2b-a140-f8f1a904da51"), new Guid("45889e13-eba5-4648-8f89-ee161e9335c9"), new Guid("f634cc39-2f16-4dea-958d-bcc56fbe61aa")).WithObjectTypes(Person, PersonTraining).WithSingularName("PersonTraining")  .WithPluralName("PersonTrainings")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("b6f28dbd-f20f-44ed-a2e7-476f1a8a5518"), new Guid("3ddb90b4-84df-4214-818d-7fa05a464815"), new Guid("a609ed29-bde6-4b43-bfe7-4abda8630b90")).WithObjectTypes(Person, GenderType).WithSingularName("Gender")  .WithPluralName("Genders")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("d48c94ea-5106-44a2-8eda-959e03480960"), new Guid("32e77969-92bc-4387-9f93-350eaba42fea"), new Guid("b7789142-9c4a-452c-927b-7de2c7e09e83")).WithObjectTypes(Person, allorsInteger).WithSingularName("Weight")  .WithPluralName("Weights")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("ee6e4476-b1fa-431f-add3-30afe199cdd1"), new Guid("ffecd512-f3cd-44d1-868e-824fd81e6431"), new Guid("8a302cbf-f784-42d9-a127-55b256895959")).WithObjectTypes(Person, Hobby).WithSingularName("Hobby")  .WithPluralName("Hobbies")  .WithCardinality(Cardinalities.ManyToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("eeb16852-431b-4b84-983d-559e64af6dfb"), new Guid("85960e64-77a1-4744-9be5-c1704030247c"), new Guid("6fe6349d-b63f-4387-9a3f-bf83576e0d97")).WithObjectTypes(Person, allorsInteger).WithSingularName("TotalYearsWorkExperience")  .WithPluralName("TotalYearsWorkExperiences")      .Build();
            new RelationTypeBuilder(domain, new Guid("f0708d80-a9cf-47be-9bed-76201fe9f17d"), new Guid("bfce261c-2f85-4be2-97ee-de15d3158b1d"), new Guid("e55b5f5e-931f-40a5-b092-45cdc57fd0ec")).WithObjectTypes(Person, Passport).WithSingularName("Passport")  .WithPluralName("Passports")  .WithCardinality(Cardinalities.OneToMany)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f15d6344-e4f4-4b79-a1af-c6a7417af844"), new Guid("13d296f7-0118-48dc-9f60-cbbdee324ad7"), new Guid("284e3a85-eba6-47f4-b97e-848bf2a163e5")).WithObjectTypes(Person, MaritalStatus).WithSingularName("MaritalStatus")  .WithPluralName("MaritalStatusses")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f92c5c86-c32a-41e0-99ff-2d94a8d6ccfa"), new Guid("0ff499d5-300f-483c-b722-757787c1f4b3"), new Guid("2162765c-5fd8-42aa-85f7-20f0effbc308")).WithObjectTypes(Person, Media).WithSingularName("Picture")  .WithPluralName("Pictures")    .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("fefb8dc2-cfe5-4078-b3a9-8c4622047c34"), new Guid("7ecef213-f2db-4f79-8bf3-fc0979f81420"), new Guid("e19baff7-c31c-4462-8a48-ac30e862b4ea")).WithObjectTypes(Person, allorsString).WithSingularName("SocialSecurityNumber")  .WithPluralName("SocialSecurityNumbers")      .WithSize(256).Build();
            new RelationTypeBuilder(domain, new Guid("ffda06c0-7dff-42fa-abd5-1ed6fa8c43da"), new Guid("8dabd93a-badc-40f3-96af-f97c1b61d262"), new Guid("92041fa4-b675-4fd4-b6c4-d9143393878e")).WithObjectTypes(Person, allorsDateTime).WithSingularName("DeceasedDate")  .WithPluralName("DeceasedDates")      .Build();
			
            // Singleton
            new RelationTypeBuilder(domain, new Guid("9dee4a94-26d5-410f-a3e3-3fcde21c5c89"), new Guid("0322b71b-0389-4393-8b1f-1b3fb12bb7b1"), new Guid("68f80e6a-7ff4-4f07-b2c5-728459c376ae")).WithObjectTypes(Singleton, Currency).WithSingularName("DefaultCurrency")  .WithPluralName("DefaultCurrencies")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("a0fdc553-8081-43fa-ae1a-b9f7767d2d3e"), new Guid("c36bd0ce-d912-4935-b2e2-5aecc822a524"), new Guid("65e3b040-4191-4f26-a51b-6c2a17ec35c7")).WithObjectTypes(Singleton, Media).WithSingularName("NoImageAvailableImage")  .WithPluralName("NoImageAvailableImages")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
            new RelationTypeBuilder(domain, new Guid("f154f01e-e8bb-49c0-be80-ef6c6c195ff3"), new Guid("2c42c9e4-72e3-4673-8653-aaf586ebb06a"), new Guid("979d1e59-7a9f-462a-9927-efb8ad2cada5")).WithObjectTypes(Singleton, InternalOrganisation).WithSingularName("DefaultInternalOrganisation")  .WithPluralName("DefaultInternalOrganisations")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();
			
            // UserGroup
            new RelationTypeBuilder(domain, new Guid("6b3e1fa8-5718-4a60-91c6-c6bb42be26fd"), new Guid("35c67ad6-c5ff-4e53-82a7-c457323b02b3"), new Guid("5c5e8dd8-4277-4be0-a59b-cc92dc8dde97")).WithObjectTypes(UserGroup, Party).WithSingularName("Party")  .WithPluralName("Parties")  .WithCardinality(Cardinalities.ManyToOne)  .WithIsIndexed(true)  .Build();


            // MehodTypes
            // AccountingTransactionDetail
            new MethodTypeBuilder(domain, new Guid("FDC41297-5A41-4bbb-994C-2CF48DF0BEF9")).WithObjectType(AccountingTransactionDetail).WithName("DebitCreditString").Build();
            
            // Budget
            new MethodTypeBuilder(domain, new Guid("2A5694F0-694B-4a66-8E97-50B1C4C597E0")).WithObjectType(Budget).WithName("Close").Build();
            new MethodTypeBuilder(domain, new Guid("5F721966-8DC9-46f7-9891-18CA7251C9A9")).WithObjectType(Budget).WithName("Reopen").Build();

            // PurchaseInvoice
            new MethodTypeBuilder(domain, new Guid("A6915CF2-BEE7-4cf7-8DF2-9C4C27B0DC93")).WithObjectType(PurchaseInvoice).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("2DE63AC3-68FA-4a40-B258-0CC81DD7720D")).WithObjectType(PurchaseInvoice).WithName("Approve").Build();
            new MethodTypeBuilder(domain, new Guid("F11B3549-6159-4dcf-A391-878B4EBF75B9")).WithObjectType(PurchaseInvoice).WithName("Ready").Build();
            
            // SalesInvoice
            new MethodTypeBuilder(domain, new Guid("EA954A7D-3988-4F6F-BDB0-F32CE4214025")).WithObjectType(SalesInvoice).WithName("SalesRepNames").Build();
            new MethodTypeBuilder(domain, new Guid("AA63BFF7-1120-43a7-B869-31DB78235445")).WithObjectType(SalesInvoice).WithName("CancelInvoice").Build();
            new MethodTypeBuilder(domain, new Guid("F43B75AA-589D-49cf-825D-050C4C6DE37D")).WithObjectType(SalesInvoice).WithName("Send").Build();
            new MethodTypeBuilder(domain, new Guid("CC1DA3B5-9CC4-4dc7-91E7-8D2E894397CC")).WithObjectType(SalesInvoice).WithName("WriteOff").Build();

            // PurchaseOrder
            new MethodTypeBuilder(domain, new Guid("C36C6737-8994-4fef-A6CE-B15276105725")).WithObjectType(PurchaseOrder).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("BD27F062-5647-4039-8D36-74617DFA57D6")).WithObjectType(PurchaseOrder).WithName("Confirm").Build();
            new MethodTypeBuilder(domain, new Guid("4FD081E8-79AB-4e7e-B9C5-5AF368E218F7")).WithObjectType(PurchaseOrder).WithName("Reject").Build();
            new MethodTypeBuilder(domain, new Guid("61F517D0-E1D1-4653-9CBF-FE0F0233D73A")).WithObjectType(PurchaseOrder).WithName("Hold").Build();
            new MethodTypeBuilder(domain, new Guid("4DC43C8A-231B-496b-9C6D-36CCC1DF09C5")).WithObjectType(PurchaseOrder).WithName("Approve").Build();
            new MethodTypeBuilder(domain, new Guid("071A8586-FF11-4b21-BAD1-C6BC3C1B5581")).WithObjectType(PurchaseOrder).WithName("Continue").Build();
    
            // PurchaseOrderItem
            new MethodTypeBuilder(domain, new Guid("C5865BD8-5361-461b-B981-D97E2EAD7A4D")).WithObjectType(PurchaseOrderItem).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("40E87252-DF49-4330-8BC5-2EB187FD1CDC")).WithObjectType(PurchaseOrderItem).WithName("Reject").Build();
            new MethodTypeBuilder(domain, new Guid("49C3FFBA-5A01-4CE6-A479-54409FD410E2")).WithObjectType(PurchaseOrderItem).WithName("Confirm").Build();
            new MethodTypeBuilder(domain, new Guid("D4A89211-D773-4B09-B86B-94A8E2A040E1")).WithObjectType(PurchaseOrderItem).WithName("Approve").Build();
            new MethodTypeBuilder(domain, new Guid("68C7B445-8260-4004-BB94-F07EFDCDA5EE")).WithObjectType(PurchaseOrderItem).WithName("Complete").Build();
            new MethodTypeBuilder(domain, new Guid("17958A95-5F55-4637-B544-53A6330CCA72")).WithObjectType(PurchaseOrderItem).WithName("Finish").Build();
            new MethodTypeBuilder(domain, new Guid("A2311C03-C375-4b9b-B3F8-05DB02CD955F")).WithObjectType(PurchaseOrderItem).WithName("Delete").Build();

            // Requirement
            new MethodTypeBuilder(domain, new Guid("7F643BF6-6500-4ab5-A647-50E4E1D6B3BC")).WithObjectType(Requirement).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("0C9740A3-F0FA-4965-9AA8-B6AC88142BAC")).WithObjectType(Requirement).WithName("Close").Build();
            new MethodTypeBuilder(domain, new Guid("7017F137-7964-4782-951D-0B5DA0186D60")).WithObjectType(Requirement).WithName("Ready").Build();

            // SalesOrder
            new MethodTypeBuilder(domain, new Guid("599BB88F-C056-4505-A1DD-319599A9E1C6")).WithObjectType(SalesOrder).WithName("SalesRepNames").Build();
            new MethodTypeBuilder(domain, new Guid("B114C3A1-E86F-4c3f-933A-626E1772EAFF")).WithObjectType(SalesOrder).WithName("CancelOrder").Build();
            new MethodTypeBuilder(domain, new Guid("800D5136-D3AB-4d4e-9855-76158CFB0725")).WithObjectType(SalesOrder).WithName("Confirm").Build();
            new MethodTypeBuilder(domain, new Guid("93E31115-ADB1-4ced-928D-FEBFDFF64678")).WithObjectType(SalesOrder).WithName("Reject").Build();
            new MethodTypeBuilder(domain, new Guid("9CF5BE09-FE52-44d7-AEDD-CC5D464FAC4D")).WithObjectType(SalesOrder).WithName("Hold").Build();
            new MethodTypeBuilder(domain, new Guid("8D04D69F-4517-4bc5-B8F2-8A8F31C3C342")).WithObjectType(SalesOrder).WithName("Approve").Build();
            new MethodTypeBuilder(domain, new Guid("81B794DC-FE47-4fc5-AB16-9C39082E5271")).WithObjectType(SalesOrder).WithName("Continue").Build();
            new MethodTypeBuilder(domain, new Guid("4F108BA5-002E-4D98-96C1-60FEBA23EC39")).WithObjectType(SalesOrder).WithName("Complete").Build();
            new MethodTypeBuilder(domain, new Guid("904C6666-B2D3-44D2-8D90-5E1E6B73CF88")).WithObjectType(SalesOrder).WithName("Finish").Build();
            new MethodTypeBuilder(domain, new Guid("4117EFBF-6B7F-4107-A835-1368DB5C9E3C")).WithObjectType(SalesOrder).WithName("Ship").Build();
            new MethodTypeBuilder(domain, new Guid("7E6089B9-689F-4ABF-939D-25D7E5CFE0CF")).WithObjectType(SalesOrder).WithName("TryShip").Build();

            // SalesOrderItem
            new MethodTypeBuilder(domain, new Guid("F96B6DAF-D344-4916-97BA-F1A4DCDE46A4")).WithObjectType(SalesOrderItem).WithName("Delete").Build();
            new MethodTypeBuilder(domain, new Guid("394072DA-1AB2-4031-A72A-AC816599D6E4")).WithObjectType(SalesOrderItem).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("3A934C3E-4291-472A-ADF3-7BB5A86DABAD")).WithObjectType(SalesOrderItem).WithName("Confirm").Build();
            new MethodTypeBuilder(domain, new Guid("8A18AB2C-762C-4f68-B526-29C2B24CF708")).WithObjectType(SalesOrderItem).WithName("Reject").Build();
            new MethodTypeBuilder(domain, new Guid("D6653FE0-3C09-48C6-B604-6F075D0BE3A0")).WithObjectType(SalesOrderItem).WithName("Approve").Build();
            new MethodTypeBuilder(domain, new Guid("B1FE36CF-270B-4A5C-825A-CDD4082BE704")).WithObjectType(SalesOrderItem).WithName("Continue").Build();
            new MethodTypeBuilder(domain, new Guid("D43E3EFE-C8E5-4D3C-9F52-792DB7C8C09C")).WithObjectType(SalesOrderItem).WithName("Finish").Build();

            // PartSpecification
            new MethodTypeBuilder(domain, new Guid("7E08EA40-5A3B-4a18-A675-97212BA896DE")).WithObjectType(PartSpecification).WithName("Approve").Build();

            // Case
            new MethodTypeBuilder(domain, new Guid("2424081C-B0EE-44ce-8408-20E9C2506F7E")).WithObjectType(Case).WithName("Close").Build();
            new MethodTypeBuilder(domain, new Guid("4F37D92B-2B2D-4ebc-9C18-6B35B9D74CEA")).WithObjectType(Case).WithName("Complete").Build();
            new MethodTypeBuilder(domain, new Guid("23811AB1-EB92-473c-9E37-D8C709D15CD9")).WithObjectType(Case).WithName("Reopen").Build();

            // CommunicationEvent
            new MethodTypeBuilder(domain, new Guid("0DEFCE41-4F4A-496c-AF9D-23DA6F3EDCD4")).WithObjectType(CommunicationEvent).WithName("Close").Build();
            new MethodTypeBuilder(domain, new Guid("C59CA8F0-E2FC-4b57-981C-A69480989B64")).WithObjectType(CommunicationEvent).WithName("Reopen").Build();
            new MethodTypeBuilder(domain, new Guid("10F20740-EB05-4674-970F-BC41F2354205")).WithObjectType(CommunicationEvent).WithName("Cancel").Build();

            // CustomerShipment
            new MethodTypeBuilder(domain, new Guid("AC91935E-8A7B-4342-8F77-02134F56CAC9")).WithObjectType(CustomerShipment).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("E4A1DB81-6EBE-4EDF-A80F-421C66871010")).WithObjectType(CustomerShipment).WithName("Ship").Build();
            new MethodTypeBuilder(domain, new Guid("4955B760-0972-495B-A0CA-E927794074E6")).WithObjectType(CustomerShipment).WithName("Hold").Build();
            new MethodTypeBuilder(domain, new Guid("2A529B67-9454-48D4-AB1F-36DA3A54A97C")).WithObjectType(CustomerShipment).WithName("Continue").Build();

            // PickList
            new MethodTypeBuilder(domain, new Guid("C525B8AC-7BB3-41fc-9C99-794AA345A153")).WithObjectType(PickList).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("44C9F165-2394-41a9-895C-EE415D392602")).WithObjectType(PickList).WithName("SetPicked").Build();
            new MethodTypeBuilder(domain, new Guid("41FA216F-1A0E-47DA-9965-32F2FEB46F1A")).WithObjectType(PickList).WithName("Hold").Build();
            new MethodTypeBuilder(domain, new Guid("56F4109E-893C-4E7E-BA3F-93AF67830815")).WithObjectType(PickList).WithName("Continue").Build();

            // PurchaseShipment
            new MethodTypeBuilder(domain, new Guid("0D49EB0C-C0D9-4BAE-B94D-636F04DF853A")).WithObjectType(PurchaseShipment).WithName("Cancel").Build();
            
            // WorkEffort
            new MethodTypeBuilder(domain, new Guid("DAF547D9-AD4B-47AD-A9AA-C2A54A76A2C3")).WithObjectType(WorkEffort).WithName("Confirm").Build();
            new MethodTypeBuilder(domain, new Guid("5359113C-6642-43F8-8134-398C36DD2E45")).WithObjectType(WorkEffort).WithName("WorkDone").Build();
            new MethodTypeBuilder(domain, new Guid("499AD64C-1A7C-4962-A56F-94B605F23DEC")).WithObjectType(WorkEffort).WithName("Finish").Build();
            new MethodTypeBuilder(domain, new Guid("79681B12-542B-442d-ABA9-6C64E8591EB1")).WithObjectType(WorkEffort).WithName("Cancel").Build();
            new MethodTypeBuilder(domain, new Guid("874A7A4E-09D2-44d2-84E5-4748DBCCD2D2")).WithObjectType(WorkEffort).WithName("Reopen").Build();
            
            return domain;
        }
    }
}