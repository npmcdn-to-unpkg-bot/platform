// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Product :  SearchResult,UniquelyIdentifiable,UserInterfaceable,Searchable, Allors.IObjectBase
	{


		ProductCategory PrimaryProductCategory
		{ 
			get;
			set;
		}

		bool ExistPrimaryProductCategory
		{
			get;
		}

		void RemovePrimaryProductCategory();


		global::System.DateTime SupportDiscontinuationDate 
		{
			get;
			set;
		}

		bool ExistSupportDiscontinuationDate{get;}

		void RemoveSupportDiscontinuationDate();


		global::System.DateTime SalesDiscontinuationDate 
		{
			get;
			set;
		}

		bool ExistSalesDiscontinuationDate{get;}

		void RemoveSalesDiscontinuationDate();


		global::System.String Description 
		{
			get;
			set;
		}

		bool ExistDescription{get;}

		void RemoveDescription();


		global::Allors.Extent<PriceComponent> VirtualProductPriceComponents
		{ 
			get;
			set;
		}

		void AddVirtualProductPriceComponent (PriceComponent value);

		void RemoveVirtualProductPriceComponent (PriceComponent value);

		bool ExistVirtualProductPriceComponents
		{
			get;
		}

		void RemoveVirtualProductPriceComponents();


		global::System.String IntrastatCode 
		{
			get;
			set;
		}

		bool ExistIntrastatCode{get;}

		void RemoveIntrastatCode();


		global::Allors.Extent<ProductCategory> ProductCategoriesExpanded
		{ 
			get;
			set;
		}

		void AddProductCategoryExpanded (ProductCategory value);

		void RemoveProductCategoryExpanded (ProductCategory value);

		bool ExistProductCategoriesExpanded
		{
			get;
		}

		void RemoveProductCategoriesExpanded();


		Product ProductComplement
		{ 
			get;
			set;
		}

		bool ExistProductComplement
		{
			get;
		}

		void RemoveProductComplement();


		global::Allors.Extent<ProductFeature> OptionalFeatures
		{ 
			get;
			set;
		}

		void AddOptionalFeature (ProductFeature value);

		void RemoveOptionalFeature (ProductFeature value);

		bool ExistOptionalFeatures
		{
			get;
		}

		void RemoveOptionalFeatures();


		Party ManufacturedBy
		{ 
			get;
			set;
		}

		bool ExistManufacturedBy
		{
			get;
		}

		void RemoveManufacturedBy();


		global::Allors.Extent<Product> Variants
		{ 
			get;
			set;
		}

		void AddVariant (Product value);

		void RemoveVariant (Product value);

		bool ExistVariants
		{
			get;
		}

		void RemoveVariants();


		global::System.String Name 
		{
			get;
			set;
		}

		bool ExistName{get;}

		void RemoveName();


		global::System.DateTime IntroductionDate 
		{
			get;
			set;
		}

		bool ExistIntroductionDate{get;}

		void RemoveIntroductionDate();


		global::Allors.Extent<Document> Documents
		{ 
			get;
			set;
		}

		void AddDocument (Document value);

		void RemoveDocument (Document value);

		bool ExistDocuments
		{
			get;
		}

		void RemoveDocuments();


		global::Allors.Extent<ProductFeature> StandardFeatures
		{ 
			get;
			set;
		}

		void AddStandardFeature (ProductFeature value);

		void RemoveStandardFeature (ProductFeature value);

		bool ExistStandardFeatures
		{
			get;
		}

		void RemoveStandardFeatures();


		UnitOfMeasure UnitOfMeasure
		{ 
			get;
			set;
		}

		bool ExistUnitOfMeasure
		{
			get;
		}

		void RemoveUnitOfMeasure();


		global::Allors.Extent<EstimatedProductCost> EstimatedProductCosts
		{ 
			get;
			set;
		}

		void AddEstimatedProductCost (EstimatedProductCost value);

		void RemoveEstimatedProductCost (EstimatedProductCost value);

		bool ExistEstimatedProductCosts
		{
			get;
		}

		void RemoveEstimatedProductCosts();


		global::Allors.Extent<Product> ProductObsolescences
		{ 
			get;
			set;
		}

		void AddProductObsolescence (Product value);

		void RemoveProductObsolescence (Product value);

		bool ExistProductObsolescences
		{
			get;
		}

		void RemoveProductObsolescences();


		global::Allors.Extent<ProductFeature> SelectableFeatures
		{ 
			get;
			set;
		}

		void AddSelectableFeature (ProductFeature value);

		void RemoveSelectableFeature (ProductFeature value);

		bool ExistSelectableFeatures
		{
			get;
		}

		void RemoveSelectableFeatures();


		VatRate VatRate
		{ 
			get;
			set;
		}

		bool ExistVatRate
		{
			get;
		}

		void RemoveVatRate();


		global::Allors.Extent<PriceComponent> BasePrices
		{ 
			get;
			set;
		}

		void AddBasePrice (PriceComponent value);

		void RemoveBasePrice (PriceComponent value);

		bool ExistBasePrices
		{
			get;
		}

		void RemoveBasePrices();


		global::Allors.Extent<ProductCategory> ProductCategories
		{ 
			get;
			set;
		}

		void AddProductCategory (ProductCategory value);

		void RemoveProductCategory (ProductCategory value);

		bool ExistProductCategories
		{
			get;
		}

		void RemoveProductCategories();


		InternalOrganisation SoldBy
		{ 
			get;
			set;
		}

		bool ExistSoldBy
		{
			get;
		}

		void RemoveSoldBy();



		global::Allors.Extent<ProductFeatureApplicabilityRelationship> ProductFeatureApplicabilityRelationshipsWhereAvailableFor
		{ 
			get;
		}

		bool ExistProductFeatureApplicabilityRelationshipsWhereAvailableFor
		{
			get;
		}


		global::Allors.Extent<ProductRequirement> ProductRequirementsWhereProduct
		{ 
			get;
		}

		bool ExistProductRequirementsWhereProduct
		{
			get;
		}


		global::Allors.Extent<QuoteItem> QuoteItemsWhereProduct
		{ 
			get;
		}

		bool ExistQuoteItemsWhereProduct
		{
			get;
		}


		global::Allors.Extent<SupplierOffering> SupplierOfferingsWhereProduct
		{ 
			get;
		}

		bool ExistSupplierOfferingsWhereProduct
		{
			get;
		}


		global::Allors.Extent<GeneralLedgerAccount> GeneralLedgerAccountsWhereDefaultCostUnit
		{ 
			get;
		}

		bool ExistGeneralLedgerAccountsWhereDefaultCostUnit
		{
			get;
		}


		global::Allors.Extent<GeneralLedgerAccount> GeneralLedgerAccountsWhereCostUnitAllowed
		{ 
			get;
		}

		bool ExistGeneralLedgerAccountsWhereCostUnitAllowed
		{
			get;
		}


		global::Allors.Extent<ProductConfiguration> ProductConfigurationsWhereProductUsedIn
		{ 
			get;
		}

		bool ExistProductConfigurationsWhereProductUsedIn
		{
			get;
		}


		global::Allors.Extent<ProductConfiguration> ProductConfigurationsWhereProduct
		{ 
			get;
		}

		bool ExistProductConfigurationsWhereProduct
		{
			get;
		}


		global::Allors.Extent<ProductRevenueHistory> ProductRevenueHistoriesWhereProduct
		{ 
			get;
		}

		bool ExistProductRevenueHistoriesWhereProduct
		{
			get;
		}


		global::Allors.Extent<PriceComponent> PriceComponentsWhereProduct
		{ 
			get;
		}

		bool ExistPriceComponentsWhereProduct
		{
			get;
		}


		global::Allors.Extent<PartyProductRevenue> PartyProductRevenuesWhereProduct
		{ 
			get;
		}

		bool ExistPartyProductRevenuesWhereProduct
		{
			get;
		}


		MarketingPackage MarketingPackageWhereProductUsedIn
		{
			get;
		}

		bool ExistMarketingPackageWhereProductUsedIn
		{
			get;
		}


		global::Allors.Extent<MarketingPackage> MarketingPackagesWhereProduct
		{ 
			get;
		}

		bool ExistMarketingPackagesWhereProduct
		{
			get;
		}


		global::Allors.Extent<Product> ProductsWhereProductComplement
		{ 
			get;
		}

		bool ExistProductsWhereProductComplement
		{
			get;
		}


		Product ProductWhereVariant
		{
			get;
		}

		bool ExistProductWhereVariant
		{
			get;
		}


		global::Allors.Extent<Product> ProductsWhereProductObsolescence
		{ 
			get;
		}

		bool ExistProductsWhereProductObsolescence
		{
			get;
		}


		global::Allors.Extent<OrganisationGlAccount> OrganisationGlAccountsWhereProduct
		{ 
			get;
		}

		bool ExistOrganisationGlAccountsWhereProduct
		{
			get;
		}


		global::Allors.Extent<WorkEffortType> WorkEffortTypesWhereProductToProduce
		{ 
			get;
		}

		bool ExistWorkEffortTypesWhereProductToProduce
		{
			get;
		}


		global::Allors.Extent<SalesOrderItem> SalesOrderItemsWherePreviousProduct
		{ 
			get;
		}

		bool ExistSalesOrderItemsWherePreviousProduct
		{
			get;
		}


		global::Allors.Extent<SalesOrderItem> SalesOrderItemsWhereProduct
		{ 
			get;
		}

		bool ExistSalesOrderItemsWhereProduct
		{
			get;
		}


		global::Allors.Extent<ProductRevenue> ProductRevenuesWhereProduct
		{ 
			get;
		}

		bool ExistProductRevenuesWhereProduct
		{
			get;
		}


		global::Allors.Extent<SalesInvoiceItem> SalesInvoiceItemsWhereProduct
		{ 
			get;
		}

		bool ExistSalesInvoiceItemsWhereProduct
		{
			get;
		}


		global::Allors.Extent<EngagementItem> EngagementItemsWhereProduct
		{ 
			get;
		}

		bool ExistEngagementItemsWhereProduct
		{
			get;
		}


		global::Allors.Extent<PurchaseOrderItem> PurchaseOrderItemsWhereProduct
		{ 
			get;
		}

		bool ExistPurchaseOrderItemsWhereProduct
		{
			get;
		}


		global::Allors.Extent<RequestItem> RequestItemsWhereProduct
		{ 
			get;
		}

		bool ExistRequestItemsWhereProduct
		{
			get;
		}


		global::Allors.Extent<Good> GoodsWhereProductSubstitution
		{ 
			get;
		}

		bool ExistGoodsWhereProductSubstitution
		{
			get;
		}


		global::Allors.Extent<Good> GoodsWhereProductIncompatibility
		{ 
			get;
		}

		bool ExistGoodsWhereProductIncompatibility
		{
			get;
		}


		global::Allors.Extent<WorkRequirement> WorkRequirementsWhereProduct
		{ 
			get;
		}

		bool ExistWorkRequirementsWhereProduct
		{
			get;
		}


		global::Allors.Extent<PartyProductRevenueHistory> PartyProductRevenueHistoriesWhereProduct
		{ 
			get;
		}

		bool ExistPartyProductRevenueHistoriesWhereProduct
		{
			get;
		}

	}

	public class ProductMeta
	{
		public static readonly ProductMeta Instance = new ProductMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Product;

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