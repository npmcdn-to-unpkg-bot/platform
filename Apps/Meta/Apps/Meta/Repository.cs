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
            // BasePrice
            BasePricePrice.AssignedIsRequired = true;

            // Budget
            new MethodTypeBuilder(Apps, new Guid("3E913270-98BC-4A29-8C54-AD94B78D62A3")).WithObjectType(Budget).WithName("Close").Build();
            new MethodTypeBuilder(Apps, new Guid("4D8FD306-049E-4909-AFA8-91A615B76314")).WithObjectType(Budget).WithName("Reopen").Build();

            // CommunicationEvent
            new MethodTypeBuilder(Apps, new Guid("433211EF-4376-451E-863F-376F5EC66758")).WithObjectType(CommunicationEvent).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("53138963-6B25-4A90-BFE3-89B77AF73329")).WithObjectType(CommunicationEvent).WithName("Close").Build();
            new MethodTypeBuilder(Apps, new Guid("0E18F37B-39AA-452A-8085-6BD8AA686D33")).WithObjectType(CommunicationEvent).WithName("Reopen").Build();

            // CustomerShipment
            new MethodTypeBuilder(Apps, new Guid("9E89A8AD-2EFE-4A21-815B-9598D7D7C1F7")).WithObjectType(CustomerShipment).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("1A64504B-0115-4D4D-BBE0-35792A8BCA1A")).WithObjectType(CustomerShipment).WithName("PutOnHold").Build();
            new MethodTypeBuilder(Apps, new Guid("9DD73148-A1C0-4631-91AF-E13116FC0102")).WithObjectType(CustomerShipment).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("6E09CAC6-327F-49DD-B4AB-07D075C7579E")).WithObjectType(CustomerShipment).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("1B56BF7E-08BE-49B1-92A1-4CE89B329D77")).WithObjectType(CustomerShipment).WithName("Ship").Build();
            new MethodTypeBuilder(Apps, new Guid("9AFF4390-9B51-4C33-A0CF-125FED33E34F")).WithObjectType(CustomerShipment).WithName("ProcessOnContinue").Build();
            new MethodTypeBuilder(Apps, new Guid("BD7F0406-29E2-4A10-AE55-C2849D257B01")).WithObjectType(CustomerShipment).WithName("SetPicked").Build();
            new MethodTypeBuilder(Apps, new Guid("F484244D-BB1D-4158-9A4D-40267D4B7D5B")).WithObjectType(CustomerShipment).WithName("SetPacked").Build();

            // Order
            new MethodTypeBuilder(Apps, new Guid("73F0DD8B-8290-48CC-8AAF-D5B1B578A841")).WithObjectType(Order).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("DB067D32-3007-4D11-93FF-D25FE8378B9B")).WithObjectType(Order).WithName("Reject").Build();
            new MethodTypeBuilder(Apps, new Guid("716909AB-F88C-4BD4-B238-87D117CE1515")).WithObjectType(Order).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("0D0F41BB-11C8-44A0-8B6D-1F7657BB85A8")).WithObjectType(Order).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("2142CD4A-C861-4E7A-986B-CDBFC1AD0E53")).WithObjectType(Order).WithName("Confirm").Build();
            new MethodTypeBuilder(Apps, new Guid("CC489BED-55FA-449D-BC22-C9E0954DA8E3")).WithObjectType(Order).WithName("CancelOrder").Build();
            new MethodTypeBuilder(Apps, new Guid("7154A033-6A07-49FE-B928-9EDD843FC56C")).WithObjectType(Order).WithName("Complete").Build();
            new MethodTypeBuilder(Apps, new Guid("E3441FE1-E403-4709-AF7F-84238D0E69F0")).WithObjectType(Order).WithName("Finish").Build();

            // OrderItem
            new MethodTypeBuilder(Apps, new Guid("AC6B2E9E-DC3B-4FA5-80B2-EA13C0461F5F")).WithObjectType(OrderItem).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("A1E84095-C5A3-4E4E-B449-FC400A3E0D06")).WithObjectType(OrderItem).WithName("Reject").Build();
            new MethodTypeBuilder(Apps, new Guid("D0FDE3AB-EEC4-46C6-A545-30C4EB57B9D9")).WithObjectType(OrderItem).WithName("Confirm").Build();
            new MethodTypeBuilder(Apps, new Guid("D3953352-DB9E-4A59-8504-A0C400DC515E")).WithObjectType(OrderItem).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("C1517567-1708-47E6-8298-9D9B157E45FF")).WithObjectType(OrderItem).WithName("Finish").Build();
            new MethodTypeBuilder(Apps, new Guid("3962ED58-44BD-4A79-8F0C-6A98ED88BD44")).WithObjectType(OrderItem).WithName("Delete").Build();

            // PayHistory
            PayHistoryEmployment.IsRequired = true;
            PayHistoryTimeFrequency.IsRequired = true;

            // PerformanceReview
            PerformanceReviewEmployee.IsRequired = true;

            // PersonTraining
            PersonTrainingTraining.IsRequired = true;
            PersonTrainingThroughDate.AssignedIsRequired = true;

            // PickList
            new MethodTypeBuilder(Apps, new Guid("CCBD7DB6-EC0F-4D70-9833-BC2A9E3A9292")).WithObjectType(PickList).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("B88AF2FA-0940-4C3B-90E7-9937DF6C05AC")).WithObjectType(PickList).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("41E4C5C4-2CFE-4B7F-80FD-E4C0263FDF62")).WithObjectType(PickList).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("CAC524A5-47A9-4FFD-ABC2-D5D3C0ABBFDD")).WithObjectType(PickList).WithName("SetPicked").Build();

            // PurchaseInvoice
            new MethodTypeBuilder(Apps, new Guid("ECD12D89-5B32-416C-8478-06FF904C6A61")).WithObjectType(PurchaseInvoice).WithName("Ready").Build();
            new MethodTypeBuilder(Apps, new Guid("16C0CC36-B908-4912-B420-2FD3E31BB966")).WithObjectType(PurchaseInvoice).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("46BB5168-5250-4B5A-9DF0-045AFB589AAD")).WithObjectType(PurchaseInvoice).WithName("Cancel").Build();

            // PurchaseOrderItem
            new MethodTypeBuilder(Apps, new Guid("3F65C670-B891-4979-B664-D47D45833AF5")).WithObjectType(PurchaseOrderItem).WithName("Complete").Build();

            // PriceComponent
            PriceComponentDescription.IsRequired = true;
            PriceComponentSpecifiedFor.IsRequired = true;

            // SalesOrderItem
            new MethodTypeBuilder(Apps, new Guid("F04381CD-3B28-4DD5-BBE8-873C5A56AEE2")).WithObjectType(SalesOrderItem).WithName("Continue").Build();

            // Requirement
            new MethodTypeBuilder(Apps, new Guid("B96906C0-83CB-48D5-A67C-8E3E05073B14")).WithObjectType(Requirement).WithName("Reopen").Build();

            // SalesInvoice
            new MethodTypeBuilder(Apps, new Guid("1E1B769E-6E07-4F75-8620-E6308558329B")).WithObjectType(SalesInvoice).WithName("Send").Build();
            new MethodTypeBuilder(Apps, new Guid("9B314F84-7D49-45F7-9F7C-D419DCE445EE")).WithObjectType(SalesInvoice).WithName("CancelInvoice").Build();
            new MethodTypeBuilder(Apps, new Guid("7E5BD6D4-A4D7-4648-90E6-3398CE6FF3FE")).WithObjectType(SalesInvoice).WithName("WriteOff").Build();

            // WorkEffort
            new MethodTypeBuilder(Apps, new Guid("A8D6C356-6AB3-47EA-A0F7-25FBFB711A81")).WithObjectType(WorkEffort).WithName("Confirm").Build();
            new MethodTypeBuilder(Apps, new Guid("69BC6603-F9FA-4B4A-B4C5-2FAA62BD0BB4")).WithObjectType(WorkEffort).WithName("WorkDone").Build();
            new MethodTypeBuilder(Apps, new Guid("860F33C9-7CD9-427D-9FFD-93B1274C9EB2")).WithObjectType(WorkEffort).WithName("Finish").Build();
            new MethodTypeBuilder(Apps, new Guid("0A66E9CA-89A8-4D5A-B63F-E061CDBC0A2E")).WithObjectType(WorkEffort).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("A1189C0F-8E2E-41B7-B61E-36525B3895B5")).WithObjectType(WorkEffort).WithName("Reopen").Build();

            // AccountingPeriod
            AccountingPeriodActive.IsRequired = true;
            AccountingPeriodPeriodNumber.IsRequired = true;
            AccountingPeriodTimeFrequency.IsRequired = true;

            // AccountingTransaction
            AccountingTransactionEntryDate.IsRequired = true;
            AccountingTransactionTransactionDate.IsRequired = true;
            AccountingTransactionDescription.IsRequired = true;

            // AccountingTransactionDetail
            AccountingTransactionDetailAmount.IsRequired = true;
            AccountingTransactionDetailDebit.IsRequired = true;
            AccountingTransactionDetailOrganisationGlAccountBalance.IsRequired = true;

            // Bank
            BankName.IsRequired = true;
            BankCountry.IsRequired = true;
            BankBic.IsRequired = true;

            // BankAccount
            BankAccountIban.IsRequired = true;

            // Budget
            BudgetDescription.IsRequired = true;

            // BudgetItem
            BudgetItemAmount.IsRequired = true;
            BudgetItemPurpose.IsRequired = true;

            // BudgetReview
            BudgetReviewReviewDate.IsRequired = true;
            BudgetReviewDescription.IsRequired = true;

            // BudgetRevision
            BudgetRevisionRevisionDate.IsRequired = true;

            // BudgetRevisionImpact
            BudgetRevisionImpactReason.IsRequired = true;

            // BudgetStatus
            BudgetStatusStartDateTime.IsRequired = true;
            BudgetStatusBudgetObjectState.IsRequired = true;

            // ChartOfAccounts
            ChartOfAccountsName.IsRequired = true;
            
            // CostCenter
            CostCenterName.IsRequired = true;

            // CostCenterCategory
            CostCenterCategoryDescription.IsRequired = true;

            // CreditCard
            CreditCardCardNumber.IsRequired = true;
            CreditCardCreditCardCompany.IsRequired = true;
            CreditCardExpirationMonth.IsRequired = true;
            CreditCardExpirationYear.IsRequired = true;
            CreditCardNameOnCard.IsRequired = true;

            // CreditCardCompany
            CreditCardCompanyName.IsRequired = true;

            // Depreciation
            DepreciationFixedAsset.IsRequired = true;

            // DepreciationMethod
            DepreciationMethodFormula.IsRequired = true;

            // DisbursementAccountingTransaction
            DisbursementAccountingTransactionDisbursement.IsRequired = true;

            // DiscountComponent
            DiscountComponentPercentage.IsRequired = true;

            // ExternalAccountingTransaction
            ExternalAccountingTransactionFromParty.IsRequired = true;
            ExternalAccountingTransactionToParty.IsRequired = true;

            // FixedAsset
            FixedAssetName.IsRequired = true;

            // GeneralLedgerAccount
            GeneralLedgerAccountCashAccount.IsRequired = true;
            GeneralLedgerAccountCostCenterAccount.IsRequired = true;
            GeneralLedgerAccountCostCenterRequired.IsRequired = true;
            GeneralLedgerAccountCostUnitAccount.IsRequired = true;
            GeneralLedgerAccountCostUnitRequired.IsRequired = true;
            GeneralLedgerAccountReconciliationAccount.IsRequired = true;
            GeneralLedgerAccountProtected.IsRequired = true;
            GeneralLedgerAccountAccountNumber.IsRequired = true;
            GeneralLedgerAccountName.IsRequired = true;
            GeneralLedgerAccountBalanceSheetAccount.IsRequired = true;
            GeneralLedgerAccountSide.IsRequired = true;
            GeneralLedgerAccountGeneralLedgerAccountType.IsRequired = true;
            GeneralLedgerAccountGeneralLedgerAccountGroup.IsRequired = true;

            // GeneralLedgerAccountGroup
            GeneralLedgerAccountGroupDescription.IsRequired = true;

            // GeneralLedgerAccountType
            GeneralLedgerAccountTypeDescription.IsRequired = true;

            // GlBudgetAllocation
            GlBudgetAllocationAllocationPercentage.IsRequired = true;
            GlBudgetAllocationBudgetItem.IsRequired = true;
            GlBudgetAllocationGeneralLedgerAccount.IsRequired = true;

            // InternalAccountingTransaction
            InternalAccountingTransactionInternalOrganisation.IsRequired = true;
            
            // InternalOrganisation
            InternalOrganisationNextSubAccountNumber.IsRequired = true;
            InternalOrganisationFiscalYearStartMonth.IsRequired = true;
            InternalOrganisationFiscalYearStartDay.IsRequired = true;

            // InternalOrganisationRevenue
            InternalOrganisationRevenueYear.IsRequired = true;
            InternalOrganisationRevenueMonth.IsRequired = true;

            // Invoice
            InvoiceInvoiceDate.IsRequired = true;

            // InvoiceItem
            InvoiceItemQuantity.IsRequired = true;

            // Journal
            JournalDescription.IsRequired = true;
            JournalInternalOrganisation.IsRequired = true;
            JournalJournalType.IsRequired = true;
            JournalContraAccount.IsRequired = true;

            // NonSerializedInventoryItem
            NonSerializedInventoryItemAvailableToPromise.IsRequired = true;

            // Order
            OrderOrderDate.IsRequired = true;

            // OrderAdjustment
            OrderAdjustmentPercentage.IsRequired = true;
            OrderAdjustmentAmount.IsRequired = true;

            // OrderItem
            OrderItemPreviousQuantity.IsRequired = true;
            OrderItemQuantityOrdered.IsRequired = true;
            OrderItemDerivedVatRate.IsRequired = true;
            OrderItemDeliveryDate.IsRequired = true;

            // OrderShipment
            OrderShipmentQuantity.IsRequired = true;
            OrderShipmentPicked.IsRequired = true;

            // OrganisationGlAccount
            OrganisationGlAccountGeneralLedgerAccount.IsRequired = true;
            OrganisationGlAccountInternalOrganisation.IsRequired = true;

            // OrganisationGlAccountBalance
            OrganisationGlAccountBalanceAmount.IsRequired = true;
            OrganisationGlAccountBalanceOrganisationGlAccount.IsRequired = true;

            // OwnBankAccount
            OwnBankAccountBankAccount.IsRequired = true;

            // OwnCreditCard
            OwnBankAccountBankAccount.IsRequired = true;

            // PackagingContent
            PackagingContentQuantity.IsRequired = true;

            // PackageRevenue
            PackageRevenueYear.IsRequired = true;
            PackageRevenueMonth.IsRequired = true;

            // PartyContactMechanism
            PartyContactMechanismUseAsDefault.IsRequired = true;

            // PartyRevenue
            PartyRevenueYear.IsRequired = true;
            PartyRevenueMonth.IsRequired = true;

            // PartyProductCategoryRevenue
            PartyProductCategoryRevenueYear.IsRequired = true;
            PartyProductCategoryRevenueMonth.IsRequired = true;

            // PartyProductRevenue
            PartyProductRevenueYear.IsRequired = true;
            PartyProductRevenueMonth.IsRequired = true;

            // PartyPackageRevenue
            PartyPackageRevenueYear.IsRequired = true;
            PartyPackageRevenueMonth.IsRequired = true;

            // PaymentBudgetAllocation
            PaymentBudgetAllocationAmount.IsRequired = true;
            PaymentBudgetAllocationBudgetItem.IsRequired = true;
            PaymentBudgetAllocationPayment.IsRequired = true;

            // PaymentMethod
            PaymentMethodDescription.IsRequired = true;

            // PriceComponent
            PriceComponentPrice.IsRequired = true;

            // ProductPurchasePrice
            ProductPurchasePricePrice.IsRequired = true;

            // PaymentApplication
            PaymentApplicationAmountApplied.IsRequired = true;

            // Period
            PeriodFromDate.IsRequired = true;
            PeriodThroughDate.IsRequired = true;

            // ProductCategoryRevenue
            ProductCategoryRevenueYear.IsRequired = true;
            ProductCategoryRevenueMonth.IsRequired = true;

            // ProductRevenue
            ProductRevenueYear.IsRequired = true;
            ProductRevenueMonth.IsRequired = true;

            // ReceiptAccountingTransaction
            ReceiptAccountingTransactionReceipt.IsRequired = true;

            // RequirementBudgetAllocation
            RequirementBudgetAllocationAmount.IsRequired = true;
            RequirementBudgetAllocationBudgetItem.IsRequired = true;
            RequirementBudgetAllocationRequirement.IsRequired = true;

            // SalesAccountingTransaction
            SalesAccountingTransactionInvoice.IsRequired = true;

            // SalesChannelRevenue
            SalesChannelRevenueYear.IsRequired = true;
            SalesChannelRevenueMonth.IsRequired = true;

            // SalesRepPartyProductCategoryRevenue
            SalesRepPartyProductCategoryRevenueYear.IsRequired = true;
            SalesRepPartyProductCategoryRevenueMonth.IsRequired = true;

            // SalesRepPartyRevenue
            SalesRepPartyRevenueYear.IsRequired = true;
            SalesRepPartyRevenueMonth.IsRequired = true;

            // SalesRepRevenue
            SalesRepRevenueYear.IsRequired = true;
            SalesRepRevenueMonth.IsRequired = true;

            // SalesRepProductCategoryRevenue
            SalesRepProductCategoryRevenueYear.IsRequired = true;
            SalesRepProductCategoryRevenueMonth.IsRequired = true;

            // ShipmentReceipt
            ShipmentReceiptReceivedDateTime.IsRequired = true;

            // SurchargeComponent
            SurchargeComponentPercentage.IsRequired = true;

            // SalesOrderItem
            SalesOrderItemQuantityReserved.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.IsRequired = true;
            SalesOrderItemQuantityShortFalled.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.IsRequired = true;
            SalesOrderItemQuantityReserved.IsRequired = true;

            // Store
            StorePaymentNetDays.IsRequired = true;
            StorePaymentGracePeriod.IsRequired = true;

            // StoreRevenue
            StoreRevenueYear.IsRequired = true;
            StoreRevenueMonth.IsRequired = true;

            // UnitOfMeasureConversion
            UnitOfMeasureConversionConversionFactor.IsRequired = true;

            // VatRate
            VatRateRate.IsRequired = true;

            // HACK: 
            foreach (var roleType in meta.RoleTypes)
            {
                if (roleType.RelationType.IsDerived)
                {
                    roleType.IsRequired = true;
                }
            }
        }
    }
}