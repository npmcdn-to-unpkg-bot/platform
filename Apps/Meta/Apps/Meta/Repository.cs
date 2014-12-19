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
        public static void AppsPostInit(MetaPopulation meta)
        {
            // Budget
            new MethodTypeBuilder(Apps, new Guid("3E913270-98BC-4A29-8C54-AD94B78D62A3")).WithObjectType(Budget).WithName("Close").Build();
            new MethodTypeBuilder(Apps, new Guid("4D8FD306-049E-4909-AFA8-91A615B76314")).WithObjectType(Budget).WithName("Reopen").Build();

            // CommunicationEvent
            new MethodTypeBuilder(Apps, new Guid("433211EF-4376-451E-863F-376F5EC66758")).WithObjectType(CommunicationEvent).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("53138963-6B25-4A90-BFE3-89B77AF73329")).WithObjectType(CommunicationEvent).WithName("Close").Build();
            new MethodTypeBuilder(Apps, new Guid("0E18F37B-39AA-452A-8085-6BD8AA686D33")).WithObjectType(CommunicationEvent).WithName("Reopen").Build();

            // CustomerShipment
            new MethodTypeBuilder(Apps, new Guid("9E89A8AD-2EFE-4A21-815B-9598D7D7C1F7")).WithObjectType(CustomerShipment).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("6E09CAC6-327F-49DD-B4AB-07D075C7579E")).WithObjectType(CustomerShipment).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("1B56BF7E-08BE-49B1-92A1-4CE89B329D77")).WithObjectType(CustomerShipment).WithName("Ship").Build();

            // PickList
            new MethodTypeBuilder(Apps, new Guid("CCBD7DB6-EC0F-4D70-9833-BC2A9E3A9292")).WithObjectType(PickList).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("B88AF2FA-0940-4C3B-90E7-9937DF6C05AC")).WithObjectType(PickList).WithName("Continue").Build();

            // SalesInvoice
            new MethodTypeBuilder(Apps, new Guid("1E1B769E-6E07-4F75-8620-E6308558329B")).WithObjectType(SalesInvoice).WithName("Send").Build();
            new MethodTypeBuilder(Apps, new Guid("9B314F84-7D49-45F7-9F7C-D419DCE445EE")).WithObjectType(SalesInvoice).WithName("CancelInvoice").Build();

            // PurchaseInvoice
            new MethodTypeBuilder(Apps, new Guid("ECD12D89-5B32-416C-8478-06FF904C6A61")).WithObjectType(PurchaseInvoice).WithName("Ready").Build();
            new MethodTypeBuilder(Apps, new Guid("16C0CC36-B908-4912-B420-2FD3E31BB966")).WithObjectType(PurchaseInvoice).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("46BB5168-5250-4B5A-9DF0-045AFB589AAD")).WithObjectType(PurchaseInvoice).WithName("Cancel").Build();

            // Order
            new MethodTypeBuilder(Apps, new Guid("73F0DD8B-8290-48CC-8AAF-D5B1B578A841")).WithObjectType(Order).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("DB067D32-3007-4D11-93FF-D25FE8378B9B")).WithObjectType(Order).WithName("Reject").Build();
            new MethodTypeBuilder(Apps, new Guid("716909AB-F88C-4BD4-B238-87D117CE1515")).WithObjectType(Order).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("0D0F41BB-11C8-44A0-8B6D-1F7657BB85A8")).WithObjectType(Order).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("2142CD4A-C861-4E7A-986B-CDBFC1AD0E53")).WithObjectType(Order).WithName("Confirm").Build();

            // OrderItem
            new MethodTypeBuilder(Apps, new Guid("AC6B2E9E-DC3B-4FA5-80B2-EA13C0461F5F")).WithObjectType(OrderItem).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("A1E84095-C5A3-4E4E-B449-FC400A3E0D06")).WithObjectType(OrderItem).WithName("Reject").Build();
            new MethodTypeBuilder(Apps, new Guid("D0FDE3AB-EEC4-46C6-A545-30C4EB57B9D9")).WithObjectType(OrderItem).WithName("Delete").Build();

            // Requirement
            new MethodTypeBuilder(Apps, new Guid("B96906C0-83CB-48D5-A67C-8E3E05073B14")).WithObjectType(Requirement).WithName("Reopen").Build();

            // WorkEffort
            new MethodTypeBuilder(Apps, new Guid("A8D6C356-6AB3-47EA-A0F7-25FBFB711A81")).WithObjectType(WorkEffort).WithName("Reopen").Build();

            foreach (var roleType in meta.RoleTypes)
            {
                if (roleType.RelationType.IsDerived)
                {
                    roleType.IsRequired = true;
                }
            }

            // AccountingPeriod
            AccountingPeriodActive.RoleType.IsRequired = true;

            // AccountingTransactionDetail
            AccountingTransactionDetailDebit.RoleType.IsRequired = true;

            // DiscountComponent
            DiscountComponentPercentage.RoleType.IsRequired = true;

            // InternalOrganisation
            InternalOrganisationNextSubAccountNumber.RoleType.IsRequired = true;
            InternalOrganisationFiscalYearStartMonth.RoleType.IsRequired = true;
            InternalOrganisationFiscalYearStartDay.RoleType.IsRequired = true;

            // InternalOrganisationRevenue
            InternalOrganisationRevenueYear.RoleType.IsRequired = true;
            InternalOrganisationRevenueMonth.RoleType.IsRequired = true;

            // Invoice
            InvoiceInvoiceDate.RoleType.IsRequired = true;

            // InvoiceItem
            InvoiceItemQuantity.RoleType.IsRequired = true;

            // NonSerializedInventoryItem
            NonSerializedInventoryItemAvailableToPromise.RoleType.IsRequired = true;

            // Order
            OrderOrderDate.RoleType.IsRequired = true;

            // OrderAdjustment
            OrderAdjustmentPercentage.RoleType.IsRequired = true;
            OrderAdjustmentAmount.RoleType.IsRequired = true;

            // OrderItem
            OrderItemPreviousQuantity.RoleType.IsRequired = true;
            OrderItemQuantityOrdered.RoleType.IsRequired = true;
            OrderItemDerivedVatRate.RoleType.IsRequired = true;
            OrderItemDeliveryDate.RoleType.IsRequired = true;

            // OrderShipment
            OrderShipmentQuantity.RoleType.IsRequired = true;
            OrderShipmentPicked.RoleType.IsRequired = true;

            // PackagingContent
            PackagingContentQuantity.RoleType.IsRequired = true;

            // PackageRevenue
            PackageRevenueYear.RoleType.IsRequired = true;
            PackageRevenueMonth.RoleType.IsRequired = true;

            // PartyContactMechanism
            PartyContactMechanismUseAsDefault.RoleType.IsRequired = true;

            // PartyRevenue
            PartyRevenueYear.RoleType.IsRequired = true;
            PartyRevenueMonth.RoleType.IsRequired = true;

            // PartyProductCategoryRevenue
            PartyProductCategoryRevenueYear.RoleType.IsRequired = true;
            PartyProductCategoryRevenueMonth.RoleType.IsRequired = true;

            // PartyProductRevenue
            PartyProductRevenueYear.RoleType.IsRequired = true;
            PartyProductRevenueMonth.RoleType.IsRequired = true;

            // PartyPackageRevenue
            PartyPackageRevenueYear.RoleType.IsRequired = true;
            PartyPackageRevenueMonth.RoleType.IsRequired = true;

            // PriceComponent
            PriceComponentPrice.RoleType.IsRequired = true;

            // ProductPurchasePrice
            ProductPurchasePricePrice.RoleType.IsRequired = true;

            // PaymentApplication
            PaymentApplicationAmountApplied.RoleType.IsRequired = true;

            // Period
            PeriodFromDate.RoleType.IsRequired = true;
            PeriodThroughDate.RoleType.IsRequired = true;

            // ProductCategoryRevenue
            ProductCategoryRevenueYear.RoleType.IsRequired = true;
            ProductCategoryRevenueMonth.RoleType.IsRequired = true;

            // ProductRevenue
            ProductRevenueYear.RoleType.IsRequired = true;
            ProductRevenueMonth.RoleType.IsRequired = true;

            // SalesChannelRevenue
            SalesChannelRevenueYear.RoleType.IsRequired = true;
            SalesChannelRevenueMonth.RoleType.IsRequired = true;

            // SalesRepPartyProductCategoryRevenue
            SalesRepPartyProductCategoryRevenueYear.RoleType.IsRequired = true;
            SalesRepPartyProductCategoryRevenueMonth.RoleType.IsRequired = true;

            // SalesRepPartyRevenue
            SalesRepPartyRevenueYear.RoleType.IsRequired = true;
            SalesRepPartyRevenueMonth.RoleType.IsRequired = true;

            // SalesRepRevenue
            SalesRepRevenueYear.RoleType.IsRequired = true;
            SalesRepRevenueMonth.RoleType.IsRequired = true;

            // SalesRepProductCategoryRevenue
            SalesRepProductCategoryRevenueYear.RoleType.IsRequired = true;
            SalesRepProductCategoryRevenueMonth.RoleType.IsRequired = true;

            // ShipmentReceipt
            ShipmentReceiptReceivedDateTime.RoleType.IsRequired = true;

            // SurchargeComponent
            SurchargeComponentPercentage.RoleType.IsRequired = true;

            // SalesOrderItem
            SalesOrderItemQuantityReserved.RoleType.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.RoleType.IsRequired = true;
            SalesOrderItemQuantityShortFalled.RoleType.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.RoleType.IsRequired = true;
            SalesOrderItemQuantityReserved.RoleType.IsRequired = true;

            // Store
            StorePaymentNetDays.RoleType.IsRequired = true;
            StorePaymentGracePeriod.RoleType.IsRequired = true;

            // StoreRevenue
            StoreRevenueYear.RoleType.IsRequired = true;
            StoreRevenueMonth.RoleType.IsRequired = true;

            // UnitOfMeasureConversion
            UnitOfMeasureConversionConversionFactor.RoleType.IsRequired = true;

            // VatRate
            VatRateRate.RoleType.IsRequired = true;
        }
    }
}