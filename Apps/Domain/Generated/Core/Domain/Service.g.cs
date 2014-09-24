// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Service :  Product, Allors.IObjectBase
	{


		global::Allors.Extent<ProductDeliverySkillRequirement> ProductDeliverySkillRequirementsWhereService
		{ 
			get;
		}

		bool ExistProductDeliverySkillRequirementsWhereService
		{
			get;
		}

	}

	public class ServiceMeta
	{
		public static readonly ServiceMeta Instance = new ServiceMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Service;

		public global::Allors.Meta.RoleType PrimaryProductCategory 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductPrimaryProductCategory;
			}
		} 
		public global::Allors.Meta.RoleType SupportDiscontinuationDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductSupportDiscontinuationDate;
			}
		} 
		public global::Allors.Meta.RoleType SalesDiscontinuationDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductSalesDiscontinuationDate;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductDescription;
			}
		} 
		public global::Allors.Meta.RoleType VirtualProductPriceComponent 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductVirtualProductPriceComponent;
			}
		} 
		public global::Allors.Meta.RoleType IntrastatCode 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductIntrastatCode;
			}
		} 
		public global::Allors.Meta.RoleType ProductCategoryExpanded 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductProductCategoryExpanded;
			}
		} 
		public global::Allors.Meta.RoleType ProductComplement 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductProductComplement;
			}
		} 
		public global::Allors.Meta.RoleType OptionalFeature 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductOptionalFeature;
			}
		} 
		public global::Allors.Meta.RoleType ManufacturedBy 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductManufacturedBy;
			}
		} 
		public global::Allors.Meta.RoleType Variant 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductVariant;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductName;
			}
		} 
		public global::Allors.Meta.RoleType IntroductionDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductIntroductionDate;
			}
		} 
		public global::Allors.Meta.RoleType Document 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductDocument;
			}
		} 
		public global::Allors.Meta.RoleType StandardFeature 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductStandardFeature;
			}
		} 
		public global::Allors.Meta.RoleType UnitOfMeasure 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductUnitOfMeasure;
			}
		} 
		public global::Allors.Meta.RoleType EstimatedProductCost 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductEstimatedProductCost;
			}
		} 
		public global::Allors.Meta.RoleType ProductObsolescence 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductProductObsolescence;
			}
		} 
		public global::Allors.Meta.RoleType SelectableFeature 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductSelectableFeature;
			}
		} 
		public global::Allors.Meta.RoleType VatRate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductVatRate;
			}
		} 
		public global::Allors.Meta.RoleType BasePrice 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductBasePrice;
			}
		} 
		public global::Allors.Meta.RoleType ProductCategory 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductProductCategory;
			}
		} 
		public global::Allors.Meta.RoleType SoldBy 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.ProductSoldBy;
			}
		} 
		public global::Allors.Meta.RoleType DisplayName 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UserInterfaceableDisplayName;
			}
		} 
		public global::Allors.Meta.RoleType DeniedPermission 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectDeniedPermission;
			}
		} 
		public global::Allors.Meta.RoleType SecurityToken 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.AccessControlledObjectSecurityToken;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 

		public global::Allors.Meta.AssociationType ProductDeliverySkillRequirementsWhereService 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductDeliverySkillRequirementService;
			}
		} 
		public global::Allors.Meta.AssociationType ProductFeatureApplicabilityRelationshipsWhereAvailableFor 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductFeatureApplicabilityRelationshipAvailableFor;
			}
		} 
		public global::Allors.Meta.AssociationType ProductRequirementsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductRequirementProduct;
			}
		} 
		public global::Allors.Meta.AssociationType QuoteItemsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.QuoteItemProduct;
			}
		} 
		public global::Allors.Meta.AssociationType SupplierOfferingsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SupplierOfferingProduct;
			}
		} 
		public global::Allors.Meta.AssociationType GeneralLedgerAccountsWhereDefaultCostUnit 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.GeneralLedgerAccountDefaultCostUnit;
			}
		} 
		public global::Allors.Meta.AssociationType GeneralLedgerAccountsWhereCostUnitAllowed 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.GeneralLedgerAccountCostUnitAllowed;
			}
		} 
		public global::Allors.Meta.AssociationType ProductConfigurationsWhereProductUsedIn 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductConfigurationProductUsedIn;
			}
		} 
		public global::Allors.Meta.AssociationType ProductConfigurationsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductConfigurationProduct;
			}
		} 
		public global::Allors.Meta.AssociationType ProductRevenueHistoriesWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductRevenueHistoryProduct;
			}
		} 
		public global::Allors.Meta.AssociationType PriceComponentsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PriceComponentProduct;
			}
		} 
		public global::Allors.Meta.AssociationType PartyProductRevenuesWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PartyProductRevenueProduct;
			}
		} 
		public global::Allors.Meta.AssociationType MarketingPackageWhereProductUsedIn 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.MarketingPackageProductUsedIn;
			}
		} 
		public global::Allors.Meta.AssociationType MarketingPackagesWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.MarketingPackageProduct;
			}
		} 
		public global::Allors.Meta.AssociationType ProductsWhereProductComplement 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductProductComplement;
			}
		} 
		public global::Allors.Meta.AssociationType ProductWhereVariant 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductVariant;
			}
		} 
		public global::Allors.Meta.AssociationType ProductsWhereProductObsolescence 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductProductObsolescence;
			}
		} 
		public global::Allors.Meta.AssociationType OrganisationGlAccountsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.OrganisationGlAccountProduct;
			}
		} 
		public global::Allors.Meta.AssociationType WorkEffortTypesWhereProductToProduce 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkEffortTypeProductToProduce;
			}
		} 
		public global::Allors.Meta.AssociationType SalesOrderItemsWherePreviousProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesOrderItemPreviousProduct;
			}
		} 
		public global::Allors.Meta.AssociationType SalesOrderItemsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesOrderItemProduct;
			}
		} 
		public global::Allors.Meta.AssociationType ProductRevenuesWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ProductRevenueProduct;
			}
		} 
		public global::Allors.Meta.AssociationType SalesInvoiceItemsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesInvoiceItemProduct;
			}
		} 
		public global::Allors.Meta.AssociationType EngagementItemsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.EngagementItemProduct;
			}
		} 
		public global::Allors.Meta.AssociationType PurchaseOrderItemsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PurchaseOrderItemProduct;
			}
		} 
		public global::Allors.Meta.AssociationType RequestItemsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.RequestItemProduct;
			}
		} 
		public global::Allors.Meta.AssociationType GoodsWhereProductSubstitution 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.GoodProductSubstitution;
			}
		} 
		public global::Allors.Meta.AssociationType GoodsWhereProductIncompatibility 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.GoodProductIncompatibility;
			}
		} 
		public global::Allors.Meta.AssociationType WorkRequirementsWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.WorkRequirementProduct;
			}
		} 
		public global::Allors.Meta.AssociationType PartyProductRevenueHistoriesWhereProduct 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PartyProductRevenueHistoryProduct;
			}
		} 

	}
}