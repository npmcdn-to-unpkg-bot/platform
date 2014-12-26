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
            AppsAccountingPeriod();
            AppsAccountingTransaction();
            AppsAccountingTransactionDetail();
            AppsBank();
            AppsBankAccount();
            AppsBasePrice();
            AppsBenefit();
            AppsBillingAccount();
            AppsBudget();
            AppsBudgetItem();
            AppsBudgetReview();
            AppsBudgetRevision();
            AppBudgetRevisionImpact();
            AppsBudgetStatus();
            AppsChartOfAccounts();
            AppsCommunicationEvent();
            AppsCostCenter();
            AppsCostCenterCategory();
            AppsCreditCard();
            AppsCreditCardCompany();
            AppsCustomerShipment();
            AppsDeduction();
            AppsDepreciation();
            AppsDepreciationMethod();
            AppsDesiredProductFeature();
            AppsDisbursementAccountingTransaction();
            AppsDiscountComponent();
            AppsEmploymentApplication();
            AppsEngagement();
            AppsEngagementItem();
            AppsEngagementRate();
            AppsExternalAccountingTransaction();
            AppsFinancialAccountTransaction();
            AppsFiscalYearInvoiceNumber();
            AppsFixedAsset();
            AppsGeneralLedgerAccount();
            AppsGeneralLedgerAccountGroup();
            AppsGeneralLedgerAccountType();
            AppsGlBudgetAllocation();
            AppsInternalAccountingTransaction();
            AppsInternalOrganisation();
            AppsInternalOrganisationRevenue();
            AppsInvestmentAccount();
            AppsInvoice();
            AppsInvoiceItem();
            AppsInvoiceTerm();
            AppsJournal();
            AppsNeededSkill();
            AppsNonSerializedInventoryItem();
            AppsOrder();
            AppsOrderAdjustment();
            AppsOrderItem();
            AppsOrderItemBilling();
            AppsOrderKind();
            AppsOrderRequirementCommitment();
            AppsOrderShipment();
            AppsOrganisationGlAccount();
            AppsOrganisationGlAccountBalance();
            AppsOwnBankAccount();
            AppsOwnCreditCard();
            AppsPackagingContent();
            AppsPackageRevenue();
            AppsPartyBenefit();
            AppsPartyContactMechanism();
            AppsPartyRevenue();
            AppsPartyProductCategoryRevenue();
            AppsPartyProductRevenue();
            AppsPartyPackageRevenue();
            AppsPartySkill();
            AppsPayment();
            AppsPayCheck();
            AppsPayGrade();
            AppsPayHistory();
            AppsPaymentApplication();
            AppsPaymentBudgetAllocation();
            AppsPaymentMethod();
            AppsPayrollPreference();
            AppsPerformanceNote();
            AppsPerformanceReview();
            AppsPerformanceReviewItem();
            AppsPersonTraining();
            AppsPickList();
            AppsPosition();
            AppsPositionFulfillment();
            AppsPositionReportingStructure();
            AppsPositionResponsibility();
            AppsPositionType();
            AppsPositionTypeRate();
            AppsPriceComponent();
            AppsProductPurchasePrice();
            AppsPeriod();
            AppsProductCategoryRevenue();
            AppsProductRevenue();
            AppsPurchaseOrderItem();
            AppsPurchaseInvoice();
            AppsPurchaseInvoiceItem();
            AppsPurchaseInvoiceItemStatus();
            AppsPurchaseInvoiceStatus();
            AppsReceiptAccountingTransaction();
            AppsRequirement();
            AppsRequirementBudgetAllocation();
            AppsResponsibility();
            AppsResume();
            AppsSalaryStep();
            AppsSalesAccountingTransaction();
            AppsSalesChannelRevenue();
            AppsSalesInvoice();
            AppsSalesInvoiceItem();
            AppsSalesInvoiceItemStatus();
            AppsSalesInvoiceStatus();
            AppsSalesRepPartyProductCategoryRevenue();
            AppsSalesRepPartyRevenue();
            AppsSalesRepRevenue();
            AppsSalesRepProductCategoryRevenue();
            AppsServiceEntryBilling();
            AppsShipmentReceipt();
            AppsSurchargeComponent();
            AppsSalesOrderItem();
            AppsStorePayment();
            AppsStoreRevenue();
            AppsTraining();
            AppsUnitOfMeasureConversion();
            AppsVatRate();
            AppsWorkEffort();
            AppsWorkEffortBilling();

            AppsOrderTerm();
            AppsProfessionalAssignment();
            AppsPurchaseOrder();
            AppsSalesOrder();
            AppsPurchaseOrderItemStatus();
            AppsPurchaseOrderStatus();
            AppsQuoteTerm();
            AppsRequest();
            AppRequirementCommunication();
            AppsRequirementStatus();
            AppsRespondingParty();
            AppsSalesOrderItemStatus();
            AppsSalesOrderStatus();
            AppsQuote();
            AppsFacility();
            AppsWarehouse();
            AppsCity();
            AppsCountry();
            AppsCounty();
            AppsPostalAddress();
            AppsPostalBoundary();
            AppsPostalCode();
            AppsProvince();
            AppsRegion();
            AppsSalesTerritory();
            AppsServiceTerritory();
            AppsState();
            AppsStore();
            AppsTerritory();
            AppContainer();
            AppsActivityUsage();
            AppsVolumeUsage();
            AppsDeploymentUsage();
            AppsManufacturerSuggestedRetailPrice();
            AppsOneTimeCharge();
            AppsUtilizationCharge();
            AppsRecurringCharge();
            AppsBrand();
            AppsPartSpecification();
            AppsProduct();
            AppsGood();
            AppsPartBillOfMaterial();
            AppsEngineeringChange();
            AppsEngineeringChangeStatus();
            AppsDocument();
            AppsDimension();
            AppsEstimatedProductCost();
            AppsInventoryItemVariance();
            AppsLot();
            AppsInventoryItemConfiguration();
            AppsMarketingPackage();
            AppsInventoryItem();
            AppsSerializedInventoryItem();
            AppsNonSerializedInventoryItemStatus();
            AppsPackage();
            AppsPartBillOfMaterialSubstitute();
            AppsPartRevision();
            AppsPartSpecificationStatus();
            AppsPartSubstitute();
            AppsProductCategory();
            AppsProductConfiguration();
            AppsProductDeliverySkillRequirement();
            AppsPart();
            AppsSerializedInventoryItemStatus();
            AppsSupplierOffering();
            AppsAddendum();
            AppsAgreementItem();
            AppsAutomatedAgent();
            AppsCarrier();
            AppsCase();
            AppsCaseStatus();
            AppsCitizenship();
            AppsAgreement();
            AppsClientRelationship();
            AppsCommunicationEventStatus();
            AppsCustomerRelationship();
            AppsDistributionChannelRelationship();
            AppsElectronicAddress();
            AppsEmailCommunication();
            AppsEmailTemplate();
            AppsEmployment();
            AppsOrganisation();
            AppsLegalForm();
            AppsOrganisationContactKind();
            AppsProfessionalServicesRelationship();
            AppsSupplierRelationship();
            AppsSubContractorRelationship();
            AppsOrganisationContactRelationship();
            AppsOrganisationRollUp();
            AppsPartnership();
            AppsSalesRepRelationship();
            AppsPartyClassification();
            AppPassport();
            AppsProductFeatureApplicabilityRelationship();
            AppsTelecommunicationsNumber();
            AppsShipment();
            AppsTransfer();
            AppsPurchaseShipment();
            AppsPurchaseReturn();
            AppsDropShipment();
            AppsCustomerReturn();
            AppsCustomerReturnStatus();
            AppsCustomerShipmentStatus();
            AppsDropShipmentStatus();
            AppsItemIssuance();
            AppsPickListItem();
            AppsPickListStatus();
            AppsPurchaseReturnStatus();
            AppsPurchaseShipmentStatus();
            AppsShipmentPackage();
            AppsTransferStatus();
            AppsDeliverable();
            AppsTimeEntry();
            AppsDeliverableTurnover();
            AppsExpenseEntry();
            AppsMaterialsUsage();
            AppPartyFixedAssetAssignment();
            AppsServiceEntryHeader();
            AppsWorkEffortAssignment();
            AppsWorkEffortAssignmentRate();
            AppsWorkEffortFixedAssetAssignment();
            AppsWorkEffortFixedAssetStandard();
            AppsWorkEffortGoodStandard();
            AppsWorkEffortInventoryAssignment();
            AppsWorkEffortPartStandard();
            AppsWorkEffortPartyAssignment();
            AppsWorkEffortStatus();
            AppsWorkEffortType();
            AppsOperatingBudget();
            AppsCapitalBudget();
            AppsReceipt();
            AppSalesInvoiceItem();
            AppsShipmentItem();
            AppsShippingAndHandlingComponent();
            AppsPartyProductCategoryRevenueHistory();
            AppsPartyRevenueHistory();
            AppsSalesRepCommission();
            AppsStringTemplate();


            // HACK: 
            foreach (var roleType in meta.RoleTypes)
            {
                if (roleType.RelationType.IsDerived && roleType.ObjectType.IsUnit)
                {
                    var unit = roleType.ObjectType as Unit;

                    switch (unit.UnitTag)
                    {
                        case UnitTags.AllorsBoolean:
                        case UnitTags.AllorsDateTime:
                        case UnitTags.AllorsDecimal:
                        case UnitTags.AllorsFloat:
                        case UnitTags.AllorsInteger:
                        case UnitTags.AllorsUnique:
                            roleType.IsRequired = true;
                            break;
                    }
                }
            }
        }

        private static void AppsStringTemplate()
        {
            StringTemplateTemplatePurpose.IsRequired = true;
        }

        private static void AppsSalesRepCommission()
        {
            SalesRepCommissionYear.IsRequired = true;
        }

        private static void AppsPartyRevenueHistory()
        {
            PartyRevenueHistoryRevenue.IsRequired = true;
        }

        private static void AppsPartyProductCategoryRevenueHistory()
        {
            PartyProductCategoryRevenueHistoryQuantity.IsRequired = true;
            PartyProductCategoryRevenueHistoryRevenue.IsRequired = true;
        }

        private static void AppsShippingAndHandlingComponent()
        {
            ShippingAndHandlingComponentSpecifiedFor.IsRequired = true;
        }

        private static void AppsShipmentItem()
        {
            ShipmentItemQuantity.IsRequired = true;
        }

        private static void AppSalesInvoiceItem()
        {
            SalesInvoiceItemCurrentObjectState.IsRequired = true;

            SalesInvoiceItemQuantity.IsRequiredOverride = true;
            SalesInvoiceItemAmountPaid.IsRequiredOverride = true;
        }

        private static void AppsReceipt()
        {
            ReceiptEffectiveDate.IsRequiredOverride = true;
        }

        private static void AppsCapitalBudget()
        {
            CapitalBudgetCurrentObjectState.IsRequiredOverride = true;
        }

        private static void AppsOperatingBudget()
        {
            OperatingBudgetCurrentObjectState.IsRequiredOverride = true;
        }

        private static void AppsWorkEffortType()
        {
            WorkEffortTypeDescription.IsRequired = true;
            WorkEffortTypeWorkEffortTypeKind.IsRequired = true;
        }

        private static void AppsWorkEffortStatus()
        {
            WorkEffortStatusStartDateTime.IsRequired = true;
            WorkEffortStatusWorkEffortObjectState.IsRequired = true;
        }

        private static void AppsWorkEffortPartyAssignment()
        {
            WorkEffortPartyAssignmentAssignment.IsRequired = true;
            WorkEffortPartyAssignmentParty.IsRequired = true;
        }

        private static void AppsWorkEffortPartStandard()
        {
            WorkEffortPartStandardPart.IsRequired = true;
        }

        private static void AppsWorkEffortInventoryAssignment()
        {
            WorkEffortInventoryAssignmentAssignment.IsRequired = true;
            WorkEffortInventoryAssignmentInventoryItem.IsRequired = true;
        }

        private static void AppsWorkEffortGoodStandard()
        {
            WorkEffortGoodStandardGood.IsRequired = true;
        }

        private static void AppsWorkEffortFixedAssetStandard()
        {
            WorkEffortFixedAssetStandardFixedAsset.IsRequired = true;
        }

        private static void AppsWorkEffortFixedAssetAssignment()
        {
            WorkEffortFixedAssetAssignmentAssignment.IsRequired = true;
            WorkEffortFixedAssetAssignmentFixedAsset.IsRequired = true;
        }

        private static void AppsWorkEffortAssignmentRate()
        {
            WorkEffortAssignmentRateRateType.IsRequired = true;
            WorkEffortAssignmentRateWorkEffortPartyAssignment.IsRequired = true;
        }

        private static void AppsWorkEffortAssignment()
        {
            WorkEffortAssignmentAssignment.IsRequired = true;
            WorkEffortAssignmentProfessional.IsRequired = true;
        }

        private static void AppsServiceEntryHeader()
        {
            ServiceEntryHeaderSubmittedBy.IsRequired = true;
            ServiceEntryHeaderSubmittedDate.IsRequired = true;
        }

        private static void AppPartyFixedAssetAssignment()
        {
            PartyFixedAssetAssignmentFixedAsset.IsRequired = true;
            PartyFixedAssetAssignmentParty.IsRequired = true;
        }

        private static void AppsMaterialsUsage()
        {
            MaterialsUsageAmount.IsRequired = true;
            MaterialsUsageDescription.IsRequiredOverride = true;
        }

        private static void AppsExpenseEntry()
        {
            ExpenseEntryAmount.IsRequired = true;
            ExpenseEntryDescription.IsRequiredOverride = true;
        }

        private static void AppsDeliverableTurnover()
        {
            DeliverableTurnoverAmount.IsRequired = true;
            DeliverableTurnoverDescription.IsRequiredOverride = true;
        }

        private static void AppsTimeEntry()
        {
            TimeEntryUnitOfMeasure.IsRequired = true;
            TimeEntryCost.IsRequired = true;
        }

        private static void AppsDeliverable()
        {
            DeliverableName.IsRequired = true;
        }

        private static void AppsTransferStatus()
        {
            TransferStatusStartDateTime.IsRequired = true;
            TransferStatusTransferObjectState.IsRequired = true;
        }

        private static void AppsShipmentPackage()
        {
            ShipmentPackageCreationDate.IsRequired = true;
        }

        private static void AppsPurchaseShipmentStatus()
        {
            PurchaseShipmentStatusStartDateTime.IsRequired = true;
            PurchaseShipmentStatusPurchaseShipmentObjectState.IsRequired = true;
        }

        private static void AppsPurchaseReturnStatus()
        {
            PurchaseReturnStatusStartDateTime.IsRequired = true;
            PurchaseReturnStatusPurchaseReturnObjectState.IsRequired = true;
        }

        private static void AppsPickListStatus()
        {
            PickListStatusStartDateTime.IsRequired = true;
            PickListStatusPickListObjectState.IsRequired = true;
        }

        private static void AppsPickListItem()
        {
            PickListItemInventoryItem.IsRequired = true;
            PickListItemRequestedQuantity.IsRequired = true;
        }

        private static void AppsItemIssuance()
        {
            ItemIssuanceQuantity.IsRequired = true;
            ItemIssuanceInventoryItem.IsRequired = true;
            ItemIssuanceShipmentItem.IsRequired = true;
        }

        private static void AppsDropShipmentStatus()
        {
            DropShipmentStatusStartDateTime.IsRequired = true;
            DropShipmentStatusDropShipmentObjectState.IsRequired = true;
        }

        private static void AppsCustomerShipmentStatus()
        {
            CustomerShipmentStatusStartDateTime.IsRequired = true;
            CustomerShipmentStatusCustomerShipmentObjectState.IsRequired = true;
        }

        private static void AppsCustomerReturnStatus()
        {
            CustomerReturnStatusStartDateTime.IsRequired = true;
            CustomerReturnStatusCustomerReturnObjectState.IsRequired = true;
        }

        private static void AppsCustomerReturn()
        {
            CustomerReturnShipToAddress.IsRequiredOverride = true;
            CustomerReturnCurrentObjectState.IsRequired = true;
        }

        private static void AppsDropShipment()
        {
            DropShipmentShipToAddress.IsRequiredOverride = true;
            DropShipmentCurrentObjectState.IsRequired = true;
        }

        private static void AppsPurchaseReturn()
        {
            PurchaseReturnShipToAddress.IsRequiredOverride = true;
            PurchaseReturnCurrentObjectState.IsRequired = true;
        }

        private static void AppsPurchaseShipment()
        {
            PurchaseShipmentFacility.IsRequired = true;
            PurchaseShipmentCurrentObjectState.IsRequired = true;

            PurchaseShipmentEstimatedArrivalDate.IsRequiredOverride = true;
            PurchaseShipmentShipToParty.IsRequiredOverride = true;
        }

        private static void AppsTransfer()
        {
            TransferShipToAddress.IsRequiredOverride = true;
            TransferCurrentObjectState.IsRequired = true;
        }

        private static void AppsShipment()
        {
            ShipmentShipmentNumber.IsRequired = true;
            ShipmentShipToParty.IsRequired = true;
            ShipmentShipFromParty.IsRequired = true;
        }

        private static void AppsTelecommunicationsNumber()
        {
            TelecommunicationsNumberContactNumber.IsRequired = true;
        }

        private static void AppsProductFeatureApplicabilityRelationship()
        {
            ProductFeatureApplicabilityRelationshipAvailableFor.IsRequired = true;
            ProductFeatureApplicabilityRelationshipUsedToDefine.IsRequired = true;
        }

        private static void AppPassport()
        {
            PassportNumber.IsRequired = true;
        }

        private static void AppsPartyClassification()
        {
            PartyClassificationDescription.IsRequired = true;
        }

        private static void AppsSalesRepRelationship()
        {
            SalesRepRelationshipCustomer.IsRequired = true;
            SalesRepRelationshipInternalOrganisation.IsRequired = true;
            SalesRepRelationshipSalesRepresentative.IsRequired = true;
        }

        private static void AppsPartnership()
        {
            PartnershipPartner.IsRequired = true;
            PartnershipInternalOrganisation.IsRequired = true;
        }

        private static void AppsOrganisationRollUp()
        {
            OrganisationRollUpChild.IsRequired = true;
            OrganisationRollUpParent.IsRequired = true;
            OrganisationRollUpRollupKind.IsRequired = true;
        }

        private static void AppsOrganisationContactRelationship()
        {
            OrganisationContactRelationshipContact.IsRequired = true;
            OrganisationContactRelationshipOrganisation.IsRequired = true;
        }

        private static void AppsSubContractorRelationship()
        {
            SubContractorRelationshipContractor.IsRequired = true;
            SubContractorRelationshipSubContractor.IsRequired = true;
        }

        private static void AppsSupplierRelationship()
        {
            SupplierRelationshipSupplier.IsRequired = true;
            SupplierRelationshipInternalOrganisation.IsRequired = true;
            SupplierRelationshipSubAccountNumber.IsRequired = true;
        }

        private static void AppsProfessionalServicesRelationship()
        {
            ProfessionalServicesRelationshipProfessional.IsRequired = true;
            ProfessionalServicesRelationshipProfessionalServicesProvider.IsRequired = true;
        }

        private static void AppsOrganisationContactKind()
        {
            OrganisationContactKindDescription.IsRequired = true;
        }

        private static void AppsLegalForm()
        {
            LegalFormDescription.IsRequired = true;
        }

        private static void AppsOrganisation()
        {
            OrganisationName.IsRequired = true;
            OrganisationLocale.IsRequiredOverride = true;
        }

        private static void AppsEmployment()
        {
            EmploymentEmployer.IsRequired = true;
            EmploymentEmployee.IsRequired = true;
        }

        private static void AppsEmailTemplate()
        {
            EmailTemplateDescription.IsRequired = true;
        }

        private static void AppsEmailCommunication()
        {
            EmailCommunicationSubject.IsRequiredOverride = true;
        }

        private static void AppsElectronicAddress()
        {
            ElectronicAddressElectronicAddressString.IsRequired = true;
        }

        private static void AppsDistributionChannelRelationship()
        {
            DistributionChannelRelationshipDistributor.IsRequired = true;
            DistributionChannelRelationshipInternalOrganisation.IsRequired = true;
        }

        private static void AppsCustomerRelationship()
        {
            CustomerRelationshipCustomer.IsRequired = true;
            CustomerRelationshipInternalOrganisation.IsRequired = true;
            CustomerRelationshipSubAccountNumber.IsRequired = true;
            CustomerRelationshipAmountDue.IsRequired = true;
            CustomerRelationshipAmountOverDue.IsRequired = true;
        }

        private static void AppsCommunicationEventStatus()
        {
            CommunicationEventStatusStartDateTime.IsRequired = true;
            CommunicationEventStatusCommunicationEventObjectState.IsRequired = true;
        }

        private static void AppsClientRelationship()
        {
            ClientRelationshipClient.IsRequired = true;
            ClientRelationshipInternalOrganisation.IsRequired = true;
        }

        private static void AppsAgreement()
        {
            AgreementDescription.IsRequired = true;
        }

        private static void AppsCitizenship()
        {
            CitizenshipCountry.IsRequired = true;
        }

        private static void AppsCaseStatus()
        {
            CaseStatusStartDateTime.IsRequired = true;
            CaseStatusCaseObjectState.IsRequired = true;
        }

        private static void AppsCase()
        {
            CaseDescription.IsRequired = true;
            CaseCurrentObjectState.IsRequired = true;
        }

        private static void AppsCarrier()
        {
            CarrierName.IsRequired = true;
        }

        private static void AppsAutomatedAgent()
        {
            AutomatedAgentDescription.IsRequired = true;
        }

        private static void AppsAgreementItem()
        {
            AgreementItemDescription.IsRequired = true;
        }

        private static void AppsAddendum()
        {
            AddendumDescription.IsRequired = true;
            AddendumCreationDate.IsRequired = true;
        }

        private static void AppsSupplierOffering()
        {
            SupplierOfferingSupplier.IsRequired = true;
            SupplierOfferingProductPurchasePrice.IsRequired = true;
        }

        private static void AppsSerializedInventoryItemStatus()
        {
            SerializedInventoryItemStatusStartDateTime.IsRequired = true;
            SerializedInventoryItemStatusSerializedInventoryItemObjectState.IsRequired = true;
        }

        private static void AppsPart()
        {
            PartName.IsRequired = true;
            PartInventoryItemKind.IsRequired = true;
            PartOwnedByParty.IsRequired = true;
        }

        private static void AppsProductDeliverySkillRequirement()
        {
            ProductDeliverySkillRequirementService.IsRequired = true;
            ProductDeliverySkillRequirementSkill.IsRequired = true;
        }

        private static void AppsProductConfiguration()
        {
            ProductConfigurationDescription.IsRequired = true;
        }

        private static void AppsProductCategory()
        {
            ProductCategoryDescription.IsRequired = true;
        }

        private static void AppsPartSubstitute()
        {
            PartSubstitutePart.IsRequired = true;
            PartSubstituteSubstitutionPart.IsRequired = true;
            PartSubstituteQuantity.IsRequired = true;
        }

        private static void AppsPartSpecificationStatus()
        {
            PartSpecificationStatusStartDateTime.IsRequired = true;
            PartSpecificationStatusPartSpecificationObjectState.IsRequired = true;
        }

        private static void AppsPartRevision()
        {
            PartRevisionPart.IsRequired = true;
            PartRevisionSupersededByPart.IsRequired = true;
        }

        private static void AppsPartBillOfMaterialSubstitute()
        {
            PartBillOfMaterialSubstitutePartBillOfMaterial.IsRequired = true;
            PartBillOfMaterialSubstituteSubstitutionPartBillOfMaterial.IsRequired = true;
        }

        private static void AppsPackage()
        {
            PackageName.IsRequired = true;
        }

        private static void AppsNonSerializedInventoryItemStatus()
        {
            NonSerializedInventoryItemStatusStartDateTime.IsRequired = true;
            NonSerializedInventoryItemStatusNonSerializedInventoryItemObjectState.IsRequired = true;
        }

        private static void AppsSerializedInventoryItem()
        {
            SerializedInventoryItemCurrentObjectState.IsRequired = true;
            SerializedInventoryItemSerialNumber.IsRequired = true;
        }

        private static void AppsInventoryItem()
        {
            InventoryItemFacility.IsRequired = true;
        }

        private static void AppsMarketingPackage()
        {
            MarketingPackageDescription.IsRequired = true;
        }

        private static void AppsInventoryItemConfiguration()
        {
            InventoryItemConfigurationComponentInventoryItem.IsRequired = true;
            InventoryItemConfigurationInventoryItem.IsRequired = true;
            InventoryItemConfigurationQuantity.IsRequired = true;
        }

        private static void AppsLot()
        {
            LotLotNumber.IsRequired = true;
        }

        private static void AppsInventoryItemVariance()
        {
            InventoryItemVarianceReason.IsRequired = true;
            InventoryItemVarianceQuantity.IsRequired = true;
        }

        private static void AppsEstimatedProductCost()
        {
            EstimatedProductCostCost.IsRequired = true;
            EstimatedProductCostCurrency.IsRequired = true;
        }

        private static void AppsDimension()
        {
            DimensionUnitOfMeasure.IsRequired = true;
        }

        private static void AppsDocument()
        {
            DocumentName.IsRequired = true;
        }

        private static void AppsEngineeringChangeStatus()
        {
            EngineeringChangeStatusStartDateTime.IsRequired = true;
            EngineeringChangeStatusEngineeringChangeObjectState.IsRequired = true;
        }

        private static void AppsEngineeringChange()
        {
            EngineeringChangeDescription.IsRequired = true;
        }

        private static void AppsPartBillOfMaterial()
        {
            PartBillOfMaterialQuantityUsed.IsRequired = true;
            PartBillOfMaterialComponentPart.IsRequired = true;
            PartBillOfMaterialPart.IsRequired = true;
        }

        private static void AppsGood()
        {
            GoodUnitOfMeasure.IsRequiredOverride = true;
        }

        private static void AppsProduct()
        {
            ProductName.IsRequired = true;
            ProductVatRate.IsRequired = true;
            ProductSoldBy.IsRequired = true;
        }

        private static void AppsPartSpecification()
        {
            PartSpecificationDescription.IsRequired = true;
            PartSpecificationCurrentObjectState.IsRequired = true;
        }

        private static void AppsBrand()
        {
            BrandName.IsRequired = true;
        }

        private static void AppsRecurringCharge()
        {
            RecurringChargePrice.IsRequiredOverride = true;
        }

        private static void AppsUtilizationCharge()
        {
            UtilizationChargePrice.IsRequiredOverride = true;
            UtilizationChargeDescription.IsRequiredOverride = true;
        }

        private static void AppsOneTimeCharge()
        {
            OneTimeChargePrice.IsRequiredOverride = true;
        }

        private static void AppsManufacturerSuggestedRetailPrice()
        {
            ManufacturerSuggestedRetailPricePrice.IsRequiredOverride = true;
        }

        private static void AppsDeploymentUsage()
        {
            DeploymentUsageTimeFrequency.IsRequired = true;
        }

        private static void AppsVolumeUsage()
        {
            VolumeUsageQuantity.IsRequired = true;
            VolumeUsageUnitOfMeasure.IsRequired = true;
        }

        private static void AppsActivityUsage()
        {
            ActivityUsageQuantity.IsRequired = true;
            ActivityUsageUnitOfMeasure.IsRequired = true;
        }

        private static void AppContainer()
        {
            ContainerContainerDescription.IsRequired = true;
        }

        private static void AppsTerritory()
        {
            TerritoryName.IsRequired = true;
        }

        private static void AppsStore()
        {
            StorePaymentGracePeriod.IsRequired = true;
            StoreDefaultPaymentMethod.IsRequired = true;
            StoreName.IsRequired = true;
            StoreCreditLimit.IsRequired = true;
            StoreDefaultShipmentMethod.IsRequired = true;
            StoreDefaultCarrier.IsRequired = true;
            StoreCreditLimit.IsRequired = true;
            StoreShipmentThreshold.IsRequired = true;
            StoreOrderThreshold.IsRequired = true;
            StoreOrderThreshold.IsRequired = true;
            StorePaymentGracePeriod.IsRequired = true;
            StoreSalesInvoiceNumberPrefix.IsRequired = true;

        }

        private static void AppsState()
        {
            StateName.IsRequired = true;
        }

        private static void AppsServiceTerritory()
        {
            ServiceTerritoryName.IsRequired = true;
        }

        private static void AppsSalesTerritory()
        {
            SalesTerritoryName.IsRequired = true;
        }

        private static void AppsRegion()
        {
            RegionName.IsRequired = true;
        }

        private static void AppsProvince()
        {
            ProvinceName.IsRequired = true;
        }

        private static void AppsPostalCode()
        {
            PostalCodeCode.IsRequired = true;
        }

        private static void AppsPostalBoundary()
        {
            PostalBoundaryLocality.IsRequired = true;
            PostalBoundaryCountry.IsRequired = true;
        }

        private static void AppsPostalAddress()
        {
            PostalAddressAddress1.IsRequired = true;
        }

        private static void AppsCounty()
        {
            CountyName.IsRequired = true;
        }

        private static void AppsCountry()
        {
            CountryCurrency.IsRequired = true;
        }

        private static void AppsCity()
        {
            CityName.IsRequired = true;
        }

        private static void AppsWarehouse()
        {
            WarehouseOwner.IsRequiredOverride = true;
        }

        private static void AppsFacility()
        {
            FacilityName.IsRequired = true;
        }

        private static void AppsQuote()
        {
            QuoteDescription.IsRequired = true;
        }

        private static void AppsSalesOrderStatus()
        {
            SalesOrderStatusStartDateTime.IsRequired = true;
            SalesOrderStatusSalesOrderObjectState.IsRequired = true;
        }

        private static void AppsSalesOrderItemStatus()
        {
            SalesOrderItemStatusStartDateTime.IsRequired = true;
            SalesOrderItemStatusSalesOrderItemObjectState.IsRequired = true;
        }

        private static void AppsRespondingParty()
        {
            RespondingPartyParty.IsRequired = true;
        }

        private static void AppsRequirementStatus()
        {
            RequirementStatusStartDateTime.IsRequired = true;
            RequirementStatusRequirementObjectState.IsRequired = true;
        }

        private static void AppRequirementCommunication()
        {
            RequirementCommunicationAssociatedProfessional.IsRequired = true;
            RequirementCommunicationCommunicationEvent.IsRequired = true;
            RequirementCommunicationRequirement.IsRequired = true;
        }

        private static void AppsRequest()
        {
            RequestDescription.IsRequired = true;
        }

        private static void AppsQuoteTerm()
        {
            QuoteTermTermType.IsRequired = true;
        }

        private static void AppsPurchaseOrderStatus()
        {
            PurchaseOrderStatusStartDateTime.IsRequired = true;
            PurchaseOrderStatusPurchaseOrderObjectState.IsRequired = true;
        }

        private static void AppsPurchaseOrderItemStatus()
        {
            PurchaseOrderItemStatusStartDateTime.IsRequired = true;
            PurchaseOrderItemStatusPurchaseOrderItemObjectState.IsRequired = true;
        }

        private static void AppsSalesOrder()
        {
            SalesOrderCurrentObjectState.IsRequired = true;
            SalesOrderTakenByInternalOrganisation.IsRequired = true;
            SalesOrderStore.IsRequired = true;
            SalesOrderCustomerCurrency.IsRequiredOverride = true;
            SalesOrderDeliveryDate.IsRequiredOverride = true;
            SalesOrderPartiallyShip.IsRequired = true;

            SalesOrderDeliveryDate.IsRequiredOverride = true;
        }

        private static void AppsPurchaseOrder()
        {
            PurchaseOrderCurrentObjectState.IsRequired = true;
            PurchaseOrderShipToBuyer.IsRequired = true;
            PurchaseOrderBillToPurchaser.IsRequired = true;
            PurchaseOrderTakenViaSupplier.IsRequired = true;
            PurchaseOrderBillToContactMechanism.IsRequired = true;
        }

        private static void AppsProfessionalAssignment()
        {
            ProfessionalAssignmentProfessional.IsRequired = true;
            ProfessionalAssignmentEngagementItem.IsRequired = true;
        }

        private static void AppsOrderTerm()
        {
            OrderTermTermType.IsRequired = true;
        }

        private static void AppsOrderRequirementCommitment()
        {
            OrderRequirementCommitmentQuantity.IsRequired = true;
            OrderRequirementCommitmentOrderItem.IsRequired = true;
            OrderRequirementCommitmentRequirement.IsRequired = true;
        }

        private static void AppsOrderKind()
        {
            OrderKindDescription.IsRequired = true;
            OrderKindScheduleManually.IsRequired = true;
        }

        private static void AppsNeededSkill()
        {
            NeededSkillSkill.IsRequired = true;
            NeededSkillSkillLevel.IsRequired = true;
        }

        private static void AppsEngagementRate()
        {
            EngagementRateRatingType.IsRequired = true;
            EngagementRateBillingRate.IsRequired = true;
        }

        private static void AppsEngagement()
        {
            EngagementDescription.IsRequired = true;
            EngagementBillToParty.IsRequired = true;
            EngagementTakenViaInternalOrganisation.IsRequired = true;
            EngagementBillToContactMechanism.IsRequired = true;
        }

        private static void AppsDesiredProductFeature()
        {
            DesiredProductFeatureRequired.IsRequired = true;
            DesiredProductFeatureProductFeature.IsRequired = true;
        }

        private static void AppsEngagementItem()
        {
            EngagementItemDescription.IsRequired = true;
        }

        private static void AppsWorkEffort()
        {
            new MethodTypeBuilder(Apps, new Guid("A8D6C356-6AB3-47EA-A0F7-25FBFB711A81")).WithObjectType(WorkEffort).WithName("Confirm").Build();
            new MethodTypeBuilder(Apps, new Guid("69BC6603-F9FA-4B4A-B4C5-2FAA62BD0BB4")).WithObjectType(WorkEffort).WithName("WorkDone").Build();
            new MethodTypeBuilder(Apps, new Guid("860F33C9-7CD9-427D-9FFD-93B1274C9EB2")).WithObjectType(WorkEffort).WithName("Finish").Build();
            new MethodTypeBuilder(Apps, new Guid("0A66E9CA-89A8-4D5A-B63F-E061CDBC0A2E")).WithObjectType(WorkEffort).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("A1189C0F-8E2E-41B7-B61E-36525B3895B5")).WithObjectType(WorkEffort).WithName("Reopen").Build();

            WorkEffortDescription.IsRequired = true;
            WorkEffortCurrentObjectState.IsRequired = true;
        }

        private static void AppsRequirement()
        {
            new MethodTypeBuilder(Apps, new Guid("B96906C0-83CB-48D5-A67C-8E3E05073B14")).WithObjectType(Requirement).WithName("Reopen").Build();

            RequirementDescription.IsRequired = true;
            RequirementCurrentObjectState.IsRequired = true;
        }

        private static void AppsPurchaseOrderItem()
        {
            new MethodTypeBuilder(Apps, new Guid("3F65C670-B891-4979-B664-D47D45833AF5")).WithObjectType(PurchaseOrderItem).WithName("Complete").Build();

            PurchaseOrderItemCurrentObjectState.IsRequired = true;
        }

        private static void AppsPickList()
        {
            new MethodTypeBuilder(Apps, new Guid("CCBD7DB6-EC0F-4D70-9833-BC2A9E3A9292")).WithObjectType(PickList).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("B88AF2FA-0940-4C3B-90E7-9937DF6C05AC")).WithObjectType(PickList).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("41E4C5C4-2CFE-4B7F-80FD-E4C0263FDF62")).WithObjectType(PickList).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("CAC524A5-47A9-4FFD-ABC2-D5D3C0ABBFDD")).WithObjectType(PickList).WithName("SetPicked").Build();

            PickListCreationDate.IsRequired = true;
            PickListCurrentObjectState.IsRequired = true;
        }

        private static void AppsPersonTraining()
        {
            PersonTrainingTraining.IsRequired = true;
            PersonTrainingThroughDate.IsRequiredOverride = true;
        }

        private static void AppsPerformanceReview()
        {
            PerformanceReviewEmployee.IsRequired = true;
        }

        private static void AppsPayHistory()
        {
            PayHistoryEmployment.IsRequired = true;
            PayHistoryTimeFrequency.IsRequired = true;
        }

        private static void AppsCustomerShipment()
        {
            new MethodTypeBuilder(Apps, new Guid("9E89A8AD-2EFE-4A21-815B-9598D7D7C1F7")).WithObjectType(CustomerShipment).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("1A64504B-0115-4D4D-BBE0-35792A8BCA1A")).WithObjectType(CustomerShipment).WithName("PutOnHold").Build();
            new MethodTypeBuilder(Apps, new Guid("9DD73148-A1C0-4631-91AF-E13116FC0102")).WithObjectType(CustomerShipment).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("6E09CAC6-327F-49DD-B4AB-07D075C7579E")).WithObjectType(CustomerShipment).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("1B56BF7E-08BE-49B1-92A1-4CE89B329D77")).WithObjectType(CustomerShipment).WithName("Ship").Build();
            new MethodTypeBuilder(Apps, new Guid("9AFF4390-9B51-4C33-A0CF-125FED33E34F")).WithObjectType(CustomerShipment).WithName("ProcessOnContinue").Build();
            new MethodTypeBuilder(Apps, new Guid("BD7F0406-29E2-4A10-AE55-C2849D257B01")).WithObjectType(CustomerShipment).WithName("SetPicked").Build();
            new MethodTypeBuilder(Apps, new Guid("F484244D-BB1D-4158-9A4D-40267D4B7D5B")).WithObjectType(CustomerShipment).WithName("SetPacked").Build();

            CustomerShipmentShipToAddress.IsRequiredOverride = true;
            CustomerShipmentShipFromAddress.IsRequiredOverride = true;
            CustomerShipmentShipmentMethod.IsRequiredOverride = true;
            CustomerShipmentCarrier.IsRequiredOverride = true;
            CustomerShipmentCurrentObjectState.IsRequired = true;
            CustomerShipmentReleasedManually.IsRequired = true;
            CustomerShipmentHeldManually.IsRequired = true;
            CustomerShipmentWithoutCharges.IsRequired = true;

            CustomerShipmentEstimatedShipDate.IsRequiredOverride = true;
            CustomerShipmentShipFromParty.IsRequiredOverride = true;
        }

        private static void AppsCommunicationEvent()
        {
            new MethodTypeBuilder(Apps, new Guid("433211EF-4376-451E-863F-376F5EC66758")).WithObjectType(CommunicationEvent).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("53138963-6B25-4A90-BFE3-89B77AF73329")).WithObjectType(CommunicationEvent).WithName("Close").Build();
            new MethodTypeBuilder(Apps, new Guid("0E18F37B-39AA-452A-8085-6BD8AA686D33")).WithObjectType(CommunicationEvent).WithName("Reopen").Build();

            CommunicationEventDescription.IsRequired = true;
            CommunicationEventCurrentObjectState.IsRequired = true;
        }

        private static void AppsBasePrice()
        {
            BasePricePrice.IsRequiredOverride = true;
            BasePriceCurrency.IsRequiredOverride = true;
        }

        private static void AppsWorkEffortBilling()
        {
            WorkEffortBillingInvoiceItem.IsRequired = true;
            WorkEffortBillingWorkEffort.IsRequired = true;
        }

        private static void AppsVatRate()
        {
            VatRateRate.IsRequired = true;
        }

        private static void AppsUnitOfMeasureConversion()
        {
            UnitOfMeasureConversionToUnitOfMeasure.IsRequired = true;
            UnitOfMeasureConversionConversionFactor.IsRequired = true;
        }

        private static void AppsTraining()
        {
            TrainingDescription.IsRequired = true;
        }

        private static void AppsStoreRevenue()
        {
            StoreRevenueYear.IsRequired = true;
            StoreRevenueMonth.IsRequired = true;
            StoreRevenueRevenue.IsRequired = true;
        }

        private static void AppsStorePayment()
        {
            StorePaymentNetDays.IsRequired = true;
            StorePaymentGracePeriod.IsRequired = true;
        }

        private static void AppsSalesOrderItem()
        {
            new MethodTypeBuilder(Apps, new Guid("F04381CD-3B28-4DD5-BBE8-873C5A56AEE2")).WithObjectType(SalesOrderItem).WithName("Continue").Build();

            SalesOrderItemQuantityReserved.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.IsRequired = true;
            SalesOrderItemQuantityShortFalled.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.IsRequired = true;
            SalesOrderItemQuantityReserved.IsRequired = true;
            SalesOrderItemCurrentObjectState.IsRequired = true;
            SalesOrderItemQuantityPicked.IsRequired = true;
            SalesOrderItemQuantityRequestsShipping.IsRequired = true;
            SalesOrderItemQuantityReserved.IsRequired = true;
            SalesOrderItemQuantityReturned.IsRequired = true;
            SalesOrderItemQuantityShipped.IsRequired = true;
            SalesOrderItemQuantityPendingShipment.IsRequired = true;
            SalesOrderItemQuantityShortFalled.IsRequired = true;

            SalesOrderItemQuantityOrdered.IsRequiredOverride = true;
        }

        private static void AppsSurchargeComponent()
        {
            SurchargeComponentPercentage.IsRequired = true;
        }

        private static void AppsShipmentReceipt()
        {
            ShipmentReceiptReceivedDateTime.IsRequired = true;
            ShipmentReceiptInventoryItem.IsRequired = true;
            ShipmentReceiptQuantityAccepted.IsRequired = true;
            ShipmentReceiptQuantityRejected.IsRequired = true;
            ShipmentReceiptShipmentItem.IsRequired = true;
        }

        private static void AppsServiceEntryBilling()
        {
            ServiceEntryBillingInvoiceItem.IsRequired = true;
            ServiceEntryBillingServiceEntry.IsRequired = true;
        }

        private static void AppsSalesRepProductCategoryRevenue()
        {
            SalesRepProductCategoryRevenueYear.IsRequired = true;
            SalesRepProductCategoryRevenueMonth.IsRequired = true;
            SalesRepProductCategoryRevenueRevenue.IsRequired = true;
        }

        private static void AppsSalesRepRevenue()
        {
            SalesRepRevenueYear.IsRequired = true;
            SalesRepRevenueMonth.IsRequired = true;
            SalesRepRevenueRevenue.IsRequired = true;
        }

        private static void AppsSalesRepPartyRevenue()
        {
            SalesRepPartyRevenueYear.IsRequired = true;
            SalesRepPartyRevenueMonth.IsRequired = true;
            SalesRepPartyRevenueRevenue.IsRequired = true;
        }

        private static void AppsSalesRepPartyProductCategoryRevenue()
        {
            SalesRepPartyProductCategoryRevenueYear.IsRequired = true;
            SalesRepPartyProductCategoryRevenueMonth.IsRequired = true;
            SalesRepPartyProductCategoryRevenueRevenue.IsRequired = true;
        }

        private static void AppsSalesInvoiceStatus()
        {
            SalesInvoiceStatusStartDateTime.IsRequired = true;
            SalesInvoiceItemStatusSalesInvoiceItemObjectState.IsRequired = true;
        }

        private static void AppsSalesInvoiceItemStatus()
        {
            SalesInvoiceItemStatusStartDateTime.IsRequired = true;
            SalesInvoiceItemStatusSalesInvoiceItemObjectState.IsRequired = true;
        }

        private static void AppsSalesInvoiceItem()
        {
            SalesInvoiceItemSalesInvoiceItemType.IsRequired = true;
        }

        private static void AppsSalesInvoice()
        {
            new MethodTypeBuilder(Apps, new Guid("1E1B769E-6E07-4F75-8620-E6308558329B")).WithObjectType(SalesInvoice).WithName("Send").Build();
            new MethodTypeBuilder(Apps, new Guid("9B314F84-7D49-45F7-9F7C-D419DCE445EE")).WithObjectType(SalesInvoice).WithName("CancelInvoice").Build();
            new MethodTypeBuilder(Apps, new Guid("7E5BD6D4-A4D7-4648-90E6-3398CE6FF3FE")).WithObjectType(SalesInvoice).WithName("WriteOff").Build();

            SalesInvoiceSalesInvoiceType.IsRequired = true;
            SalesInvoiceBilledFromInternalOrganisation.IsRequired = true;
            SalesInvoiceStore.IsRequired = true;
            SalesInvoiceBillToCustomer.IsRequired = true;
            SalesInvoiceBillToContactMechanism.IsRequired = true;
            SalesInvoiceCurrentObjectState.IsRequired = true;

            SalesInvoiceAmountPaid.IsRequiredOverride = true;
        }

        private static void AppsSalesChannelRevenue()
        {
            SalesChannelRevenueYear.IsRequired = true;
            SalesChannelRevenueMonth.IsRequired = true;
            SalesChannelRevenueRevenue.IsRequired = true;
        }

        private static void AppsSalesAccountingTransaction()
        {
            SalesAccountingTransactionInvoice.IsRequired = true;
        }

        private static void AppsSalaryStep()
        {
            SalaryStepModifiedDate.IsRequired = true;
            SalaryStepAmount.IsRequired = true;
        }

        private static void AppsResume()
        {
            ResumeResumeDate.IsRequired = true;
            ResumeResumeText.IsRequired = true;
        }

        private static void AppsResponsibility()
        {
            ResponsibilityDescription.IsRequired = true;
        }

        private static void AppsRequirementBudgetAllocation()
        {
            RequirementBudgetAllocationAmount.IsRequired = true;
            RequirementBudgetAllocationBudgetItem.IsRequired = true;
            RequirementBudgetAllocationRequirement.IsRequired = true;
        }

        private static void AppsReceiptAccountingTransaction()
        {
            ReceiptAccountingTransactionReceipt.IsRequired = true;
        }

        private static void AppsPurchaseInvoiceStatus()
        {
            PurchaseInvoiceStatusStartDateTime.IsRequired = true;
            PurchaseInvoiceStatusPurchaseInvoiceObjectState.IsRequired = true;
        }

        private static void AppsPurchaseInvoiceItemStatus()
        {
            PurchaseInvoiceItemStatusStartDateTime.IsRequired = true;
            PurchaseInvoiceItemStatusPurchaseInvoiceItemObjectState.IsRequired = true;
        }

        private static void AppsPurchaseInvoiceItem()
        {
            PurchaseInvoiceItemPart.IsRequired = true;
            PurchaseInvoiceItemPurchaseInvoiceItemType.IsRequired = true;
            PurchaseInvoiceItemActualUnitPrice.IsRequiredOverride = true;
        }

        private static void AppsPurchaseInvoice()
        {
            new MethodTypeBuilder(Apps, new Guid("ECD12D89-5B32-416C-8478-06FF904C6A61")).WithObjectType(PurchaseInvoice).WithName("Ready").Build();
            new MethodTypeBuilder(Apps, new Guid("16C0CC36-B908-4912-B420-2FD3E31BB966")).WithObjectType(PurchaseInvoice).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("46BB5168-5250-4B5A-9DF0-045AFB589AAD")).WithObjectType(PurchaseInvoice).WithName("Cancel").Build();

            PurchaseInvoicePurchaseInvoiceType.IsRequired = true;
            PurchaseInvoiceBilledToInternalOrganisation.IsRequired = true;
            PurchaseInvoiceBilledFromParty.IsRequired = true;
            PurchaseInvoiceCurrentObjectState.IsRequired = true;
        }

        private static void AppsProductRevenue()
        {
            ProductRevenueYear.IsRequired = true;
            ProductRevenueMonth.IsRequired = true;
            ProductRevenueRevenue.IsRequired = true;
        }

        private static void AppsProductCategoryRevenue()
        {
            ProductCategoryRevenueYear.IsRequired = true;
            ProductCategoryRevenueMonth.IsRequired = true;
            ProductCategoryRevenueRevenue.IsRequired = true;
        }

        private static void AppsPeriod()
        {
            PeriodFromDate.IsRequired = true;
        }

        private static void AppsPaymentApplication()
        {
            PaymentApplicationAmountApplied.IsRequired = true;
        }

        private static void AppsProductPurchasePrice()
        {
            ProductPurchasePricePrice.IsRequired = true;
            ProductPurchasePriceCurrency.IsRequired = true;
            ProductPurchasePriceUnitOfMeasure.IsRequired = true;
        }

        private static void AppsPriceComponent()
        {
            PriceComponentPrice.IsRequired = true;
            PriceComponentDescription.IsRequired = true;
            PriceComponentSpecifiedFor.IsRequired = true;
        }

        private static void AppsPositionTypeRate()
        {
            PositionTypeRateRate.IsRequired = true;
            PositionTypeRateRateType.IsRequired = true;
            PositionTypeRateTimeFrequency.IsRequired = true;
        }

        private static void AppsPositionType()
        {
            PositionTypeDescription.IsRequired = true;
        }

        private static void AppsPositionResponsibility()
        {
            PositionResponsibilityPosition.IsRequired = true;
            PositionResponsibilityResponsibility.IsRequired = true;
        }

        private static void AppsPositionReportingStructure()
        {
            PositionReportingStructurePosition.IsRequired = true;
            PositionReportingStructureManagedByPosition.IsRequired = true;
        }

        private static void AppsPositionFulfillment()
        {
            PositionFulfillmentPerson.IsRequired = true;
            PositionFulfillmentPosition.IsRequired = true;
        }

        private static void AppsPosition()
        {
            PositionPositionType.IsRequired = true;
            PositionOrganisation.IsRequired = true;
            PositionActualFromDate.IsRequired = true;
        }

        private static void AppsPerformanceReviewItem()
        {
            PerformanceReviewItemPerformanceReviewItemType.IsRequired = true;
            PerformanceReviewItemRatingType.IsRequired = true;
        }

        private static void AppsPerformanceNote()
        {
            PerformanceNoteDescription.IsRequired = true;
            PerformanceNoteEmployee.IsRequired = true;
        }

        private static void AppsPayrollPreference()
        {
            PayrollPreferencePaymentMethod.IsRequired = true;
            PayrollPreferenceTimeFrequency.IsRequired = true;
        }

        private static void AppsPaymentMethod()
        {
            PaymentMethodDescription.IsRequired = true;
            PaymentMethodIsActive.IsRequired = true;
        }

        private static void AppsPaymentBudgetAllocation()
        {
            PaymentBudgetAllocationAmount.IsRequired = true;
            PaymentBudgetAllocationBudgetItem.IsRequired = true;
            PaymentBudgetAllocationPayment.IsRequired = true;
        }

        private static void AppsPayGrade()
        {
            PayGradeName.IsRequired = true;
        }

        private static void AppsPayCheck()
        {
            PayCheckEmployment.IsRequired = true;
        }

        private static void AppsPayment()
        {
            PaymentEffectiveDate.IsRequired = true;
            PaymentAmount.IsRequired = true;
        }

        private static void AppsPartySkill()
        {
            PartySkillSkill.IsRequired = true;
        }

        private static void AppsPartyPackageRevenue()
        {
            PartyPackageRevenueYear.IsRequired = true;
            PartyPackageRevenueMonth.IsRequired = true;
            PartyPackageRevenueRevenue.IsRequired = true;
        }

        private static void AppsPartyProductRevenue()
        {
            PartyProductRevenueYear.IsRequired = true;
            PartyProductRevenueMonth.IsRequired = true;
            PartyProductRevenueRevenue.IsRequired = true;
            PartyProductRevenueQuantity.IsRequired = true;
        }

        private static void AppsPartyProductCategoryRevenue()
        {
            PartyProductCategoryRevenueYear.IsRequired = true;
            PartyProductCategoryRevenueMonth.IsRequired = true;
            PartyProductCategoryRevenueRevenue.IsRequired = true;
            PartyProductCategoryRevenueQuantity.IsRequired = true;
        }

        private static void AppsPartyRevenue()
        {
            PartyRevenueYear.IsRequired = true;
            PartyRevenueMonth.IsRequired = true;
            PartyRevenueRevenue.IsRequired = true;
        }

        private static void AppsPartyContactMechanism()
        {
            PartyContactMechanismContactMechanism.IsRequired = true;
            PartyContactMechanismUseAsDefault.IsRequired = true;
        }

        private static void AppsPartyBenefit()
        {
            PartyBenefitEmployment.IsRequired = true;
            PartyBenefitBenefit.IsRequired = true;
        }

        private static void AppsPackageRevenue()
        {
            PackageRevenueYear.IsRequired = true;
            PackageRevenueMonth.IsRequired = true;
            PackageRevenueRevenue.IsRequired = true;
        }

        private static void AppsPackagingContent()
        {
            PackagingContentQuantity.IsRequired = true;
            PackagingContentShipmentItem.IsRequired = true;
        }

        private static void AppsOwnCreditCard()
        {
            OwnBankAccountBankAccount.IsRequired = true;
        }

        private static void AppsOwnBankAccount()
        {
            OwnBankAccountBankAccount.IsRequired = true;
        }

        private static void AppsOrganisationGlAccountBalance()
        {
            OrganisationGlAccountBalanceAmount.IsRequired = true;
            OrganisationGlAccountBalanceOrganisationGlAccount.IsRequired = true;
        }

        private static void AppsOrganisationGlAccount()
        {
            OrganisationGlAccountGeneralLedgerAccount.IsRequired = true;
            OrganisationGlAccountInternalOrganisation.IsRequired = true;
        }

        private static void AppsOrderShipment()
        {
            OrderShipmentQuantity.IsRequired = true;
            OrderShipmentPicked.IsRequired = true;
            OrderShipmentShipmentItem.IsRequired = true;
        }

        private static void AppsOrderItemBilling()
        {
            OrderItemBillingOrderItem.IsRequired = true;
            OrderItemBillingAmount.IsRequired = true;
        }

        private static void AppsOrderItem()
        {
            new MethodTypeBuilder(Apps, new Guid("AC6B2E9E-DC3B-4FA5-80B2-EA13C0461F5F")).WithObjectType(OrderItem).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("A1E84095-C5A3-4E4E-B449-FC400A3E0D06")).WithObjectType(OrderItem).WithName("Reject").Build();
            new MethodTypeBuilder(Apps, new Guid("D0FDE3AB-EEC4-46C6-A545-30C4EB57B9D9")).WithObjectType(OrderItem).WithName("Confirm").Build();
            new MethodTypeBuilder(Apps, new Guid("D3953352-DB9E-4A59-8504-A0C400DC515E")).WithObjectType(OrderItem).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("C1517567-1708-47E6-8298-9D9B157E45FF")).WithObjectType(OrderItem).WithName("Finish").Build();
            new MethodTypeBuilder(Apps, new Guid("3962ED58-44BD-4A79-8F0C-6A98ED88BD44")).WithObjectType(OrderItem).WithName("Delete").Build();

            OrderItemPreviousQuantity.IsRequired = true;
            OrderItemQuantityOrdered.IsRequired = true;
            OrderItemDerivedVatRate.IsRequired = true;
            OrderItemDeliveryDate.IsRequired = true;
        }

        private static void AppsOrderAdjustment()
        {
            OrderAdjustmentPercentage.IsRequired = true;
            OrderAdjustmentAmount.IsRequired = true;
        }

        private static void AppsOrder()
        {
            new MethodTypeBuilder(Apps, new Guid("73F0DD8B-8290-48CC-8AAF-D5B1B578A841")).WithObjectType(Order).WithName("Approve").Build();
            new MethodTypeBuilder(Apps, new Guid("DB067D32-3007-4D11-93FF-D25FE8378B9B")).WithObjectType(Order).WithName("Reject").Build();
            new MethodTypeBuilder(Apps, new Guid("716909AB-F88C-4BD4-B238-87D117CE1515")).WithObjectType(Order).WithName("Hold").Build();
            new MethodTypeBuilder(Apps, new Guid("0D0F41BB-11C8-44A0-8B6D-1F7657BB85A8")).WithObjectType(Order).WithName("Continue").Build();
            new MethodTypeBuilder(Apps, new Guid("2142CD4A-C861-4E7A-986B-CDBFC1AD0E53")).WithObjectType(Order).WithName("Confirm").Build();
            new MethodTypeBuilder(Apps, new Guid("CC489BED-55FA-449D-BC22-C9E0954DA8E3")).WithObjectType(Order).WithName("Cancel").Build();
            new MethodTypeBuilder(Apps, new Guid("7154A033-6A07-49FE-B928-9EDD843FC56C")).WithObjectType(Order).WithName("Complete").Build();
            new MethodTypeBuilder(Apps, new Guid("E3441FE1-E403-4709-AF7F-84238D0E69F0")).WithObjectType(Order).WithName("Finish").Build();

            OrderOrderNumber.IsRequired = true;
            OrderOrderDate.IsRequired = true;
            OrderEntryDate.IsRequired = true;
            OrderTotalBasePrice.IsRequired = true;
            OrderTotalBasePriceCustomerCurrency.IsRequired = true;
            OrderTotalDiscount.IsRequired = true;
            OrderTotalDiscountCustomerCurrency.IsRequired = true;
            OrderTotalVat.IsRequired = true;
            OrderTotalVatCustomerCurrency.IsRequired = true;
            OrderTotalExVat.IsRequired = true;
            OrderTotalExVatCustomerCurrency.IsRequired = true;
            OrderTotalIncVat.IsRequired = true;
            OrderTotalIncVatCustomerCurrency.IsRequired = true;
            OrderTotalFee.IsRequired = true;
            OrderTotalFeeCustomerCurrency.IsRequired = true;
            OrderTotalShippingAndHandling.IsRequired = true;
            OrderTotalShippingAndHandlingCustomerCurrency.IsRequired = true;
            OrderTotalSurcharge.IsRequired = true;
            OrderTotalSurchargeCustomerCurrency.IsRequired = true;
        }

        private static void AppsNonSerializedInventoryItem()
        {
            NonSerializedInventoryItemCurrentObjectState.IsRequired = true;
            NonSerializedInventoryItemAvailableToPromise.IsRequired = true;
            NonSerializedInventoryItemQuantityCommittedOut.IsRequired = true;
            NonSerializedInventoryItemQuantityExpectedIn.IsRequired = true;
            NonSerializedInventoryItemQuantityOnHand.IsRequired = true;
        }

        private static void AppsJournal()
        {
            JournalDescription.IsRequired = true;
            JournalInternalOrganisation.IsRequired = true;
            JournalJournalType.IsRequired = true;
            JournalContraAccount.IsRequired = true;
            JournalBlockUnpaidTransactions.IsRequired = true;
            JournalCloseWhenInBalance.IsRequired = true;
            JournalUseAsDefault.IsRequired = true;
        }

        private static void AppsInvoiceTerm()
        {
            InvoiceTermTermType.IsRequiredOverride = true;
        }

        private static void AppsInvoiceItem()
        {
            InvoiceItemQuantity.IsRequired = true;
        }

        private static void AppsInvoice()
        {
            InvoiceInvoiceDate.IsRequired = true;
            InvoiceInvoiceNumber.IsRequired = true;
            InvoiceEntryDate.IsRequired = true;
            InvoiceTotalBasePrice.IsRequired = true;
            InvoiceTotalBasePriceCustomerCurrency.IsRequired = true;
            InvoiceTotalDiscount.IsRequired = true;
            InvoiceTotalDiscountCustomerCurrency.IsRequired = true;
            InvoiceTotalExVat.IsRequired = true;
            InvoiceTotalExVatCustomerCurrency.IsRequired = true;
            InvoiceTotalFee.IsRequired = true;
            InvoiceTotalFeeCustomerCurrency.IsRequired = true;
            InvoiceTotalIncVat.IsRequired = true;
            InvoiceTotalIncVatCustomerCurrency.IsRequired = true;
            InvoiceTotalShippingAndHandling.IsRequired = true;
            InvoiceTotalShippingAndHandlingCustomerCurrency.IsRequired = true;
            InvoiceTotalSurcharge.IsRequired = true;
            InvoiceTotalSurchargeCustomerCurrency.IsRequired = true;
            InvoiceTotalVat.IsRequired = true;
            InvoiceTotalVatCustomerCurrency.IsRequired = true;
        }

        private static void AppsInvestmentAccount()
        {
            InvestmentAccountName.IsRequired = true;
        }

        private static void AppsInternalOrganisationRevenue()
        {
            InternalOrganisationRevenueYear.IsRequired = true;
            InternalOrganisationRevenueMonth.IsRequired = true;
            InternalOrganisationRevenueRevenue.IsRequired = true;
        }

        private static void AppsInternalOrganisation()
        {
            InternalOrganisationName.IsRequired = true;
            InternalOrganisationDefaultPaymentMethod.IsRequiredOverride = true;
            InternalOrganisationLocale.IsRequiredOverride = true;
            InternalOrganisationPreferredCurrency.IsRequiredOverride = true;
            InternalOrganisationNextSubAccountNumber.IsRequired = true;
            InternalOrganisationFiscalYearStartMonth.IsRequired = true;
            InternalOrganisationFiscalYearStartDay.IsRequired = true;
            InternalOrganisationDoAccounting.IsRequired = true;
            InternalOrganisationInvoiceSequence.IsRequired = true;
        }

        private static void AppsInternalAccountingTransaction()
        {
            InternalAccountingTransactionInternalOrganisation.IsRequired = true;
        }

        private static void AppsGlBudgetAllocation()
        {
            GlBudgetAllocationAllocationPercentage.IsRequired = true;
            GlBudgetAllocationBudgetItem.IsRequired = true;
            GlBudgetAllocationGeneralLedgerAccount.IsRequired = true;
        }

        private static void AppsGeneralLedgerAccountType()
        {
            GeneralLedgerAccountTypeDescription.IsRequired = true;
        }

        private static void AppsGeneralLedgerAccountGroup()
        {
            GeneralLedgerAccountGroupDescription.IsRequired = true;
        }

        private static void AppsGeneralLedgerAccount()
        {
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
        }

        private static void AppsFixedAsset()
        {
            FixedAssetName.IsRequired = true;
        }

        private static void AppsFiscalYearInvoiceNumber()
        {
            FiscalYearInvoiceNumberFiscalYear.IsRequired = true;
            FiscalYearInvoiceNumberNextSalesInvoiceNumber.IsRequired = true;
        }

        private static void AppsFinancialAccountTransaction()
        {
            FinancialAccountTransactionTransactionDate.IsRequired = true;
        }

        private static void AppsExternalAccountingTransaction()
        {
            ExternalAccountingTransactionFromParty.IsRequired = true;
            ExternalAccountingTransactionToParty.IsRequired = true;
        }

        private static void AppsEmploymentApplication()
        {
            EmploymentApplicationPerson.IsRequired = true;
            EmploymentApplicationApplicationDate.IsRequired = true;
            EmploymentApplicationPosition.IsRequired = true;
        }

        private static void AppsDiscountComponent()
        {
            DiscountComponentPercentage.IsRequired = true;
        }

        private static void AppsDisbursementAccountingTransaction()
        {
            DisbursementAccountingTransactionDisbursement.IsRequired = true;
        }

        private static void AppsDepreciationMethod()
        {
            DepreciationMethodFormula.IsRequired = true;
        }

        private static void AppsDepreciation()
        {
            DepreciationFixedAsset.IsRequired = true;
        }

        private static void AppsDeduction()
        {
            DeductionAmount.IsRequired = true;
            DeductionDeductionType.IsRequired = true;
        }

        private static void AppsCreditCardCompany()
        {
            CreditCardCompanyName.IsRequired = true;
        }

        private static void AppsCreditCard()
        {
            CreditCardCardNumber.IsRequired = true;
            CreditCardCreditCardCompany.IsRequired = true;
            CreditCardExpirationMonth.IsRequired = true;
            CreditCardExpirationYear.IsRequired = true;
            CreditCardNameOnCard.IsRequired = true;
        }

        private static void AppsCostCenterCategory()
        {
            CostCenterCategoryDescription.IsRequired = true;
        }

        private static void AppsCostCenter()
        {
            CostCenterName.IsRequired = true;
        }

        private static void AppsChartOfAccounts()
        {
            ChartOfAccountsName.IsRequired = true;
        }

        private static void AppsBudgetStatus()
        {
            BudgetStatusStartDateTime.IsRequired = true;
            BudgetStatusBudgetObjectState.IsRequired = true;
        }

        private static void AppBudgetRevisionImpact()
        {
            BudgetRevisionImpactReason.IsRequired = true;
        }

        private static void AppsBudgetRevision()
        {
            BudgetRevisionRevisionDate.IsRequired = true;
        }

        private static void AppsBudgetReview()
        {
            BudgetReviewReviewDate.IsRequired = true;
            BudgetReviewDescription.IsRequired = true;
        }

        private static void AppsBudgetItem()
        {
            BudgetItemAmount.IsRequired = true;
            BudgetItemPurpose.IsRequired = true;
        }

        private static void AppsBudget()
        {
            new MethodTypeBuilder(Apps, new Guid("3E913270-98BC-4A29-8C54-AD94B78D62A3")).WithObjectType(Budget).WithName("Close").Build();
            new MethodTypeBuilder(Apps, new Guid("4D8FD306-049E-4909-AFA8-91A615B76314")).WithObjectType(Budget).WithName("Reopen").Build();

            BudgetDescription.IsRequired = true;
        }

        private static void AppsBillingAccount()
        {
            BillingAccountDescription.IsRequired = true;
        }

        private static void AppsBenefit()
        {
            BenefitName.IsRequired = true;
        }

        private static void AppsBankAccount()
        {
            BankAccountIban.IsRequired = true;
        }

        private static void AppsAccountingPeriod()
        {
            AccountingPeriodActive.IsRequired = true;
            AccountingPeriodPeriodNumber.IsRequired = true;
            AccountingPeriodTimeFrequency.IsRequired = true;
        }

        private static void AppsAccountingTransaction()
        {
            AccountingTransactionEntryDate.IsRequired = true;
            AccountingTransactionTransactionDate.IsRequired = true;
            AccountingTransactionDescription.IsRequired = true;
        }

        private static void AppsAccountingTransactionDetail()
        {
            AccountingTransactionDetailAmount.IsRequired = true;
            AccountingTransactionDetailDebit.IsRequired = true;
            AccountingTransactionDetailOrganisationGlAccountBalance.IsRequired = true;
        }

        private static void AppsBank()
        {
            BankName.IsRequired = true;
            BankCountry.IsRequired = true;
            BankBic.IsRequired = true;
        }
    }
}