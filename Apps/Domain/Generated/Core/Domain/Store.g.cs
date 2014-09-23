// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	[System.Diagnostics.DebuggerNonUserCode]
	public partial class Store : Allors.ObjectBase , UniquelyIdentifiable, UserInterfaceable
	{
		public static readonly StoreMeta Meta = StoreMeta.Instance;

		public Store(Allors.IStrategy allors) : base(allors) {}

		public static Store Instantiate (Allors.ISession allorsSession, string allorsObjectId)
		{
			return (Store) allorsSession.Instantiate(allorsObjectId);		
		}




		virtual public global::System.Decimal? ShipmentThreshold 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.ShipmentThreshold);
			}
			set
			{
				Strategy.SetUnitRole(Meta.ShipmentThreshold, value);
			}
		}

		virtual public bool ExistShipmentThreshold{
			get
			{
				return Strategy.ExistUnitRole(Meta.ShipmentThreshold);
			}
		}

		virtual public void RemoveShipmentThreshold()
		{
			Strategy.RemoveUnitRole(Meta.ShipmentThreshold);
		}


		virtual public global::Allors.Extent<StringTemplate> SalesInvoiceTemplates
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.SalesInvoiceTemplate);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.SalesInvoiceTemplate, value);
			}
		}

		virtual public void AddSalesInvoiceTemplate (StringTemplate value)
		{
			Strategy.AddCompositeRole(Meta.SalesInvoiceTemplate, value);
		}

		virtual public void RemoveSalesInvoiceTemplate (StringTemplate value)
		{
			Strategy.RemoveCompositeRole(Meta.SalesInvoiceTemplate,value);
		}

		virtual public bool ExistSalesInvoiceTemplates
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.SalesInvoiceTemplate);
			}
		}

		virtual public void RemoveSalesInvoiceTemplates()
		{
			Strategy.RemoveCompositeRoles(Meta.SalesInvoiceTemplate);
		}



		virtual public global::System.String OutgoingShipmentNumberPrefix 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.OutgoingShipmentNumberPrefix);
			}
			set
			{
				Strategy.SetUnitRole(Meta.OutgoingShipmentNumberPrefix, value);
			}
		}

		virtual public bool ExistOutgoingShipmentNumberPrefix{
			get
			{
				return Strategy.ExistUnitRole(Meta.OutgoingShipmentNumberPrefix);
			}
		}

		virtual public void RemoveOutgoingShipmentNumberPrefix()
		{
			Strategy.RemoveUnitRole(Meta.OutgoingShipmentNumberPrefix);
		}



		virtual public global::System.String SalesInvoiceNumberPrefix 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.SalesInvoiceNumberPrefix);
			}
			set
			{
				Strategy.SetUnitRole(Meta.SalesInvoiceNumberPrefix, value);
			}
		}

		virtual public bool ExistSalesInvoiceNumberPrefix{
			get
			{
				return Strategy.ExistUnitRole(Meta.SalesInvoiceNumberPrefix);
			}
		}

		virtual public void RemoveSalesInvoiceNumberPrefix()
		{
			Strategy.RemoveUnitRole(Meta.SalesInvoiceNumberPrefix);
		}



		virtual public global::System.Int32? PaymentGracePeriod 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.PaymentGracePeriod);
			}
			set
			{
				Strategy.SetUnitRole(Meta.PaymentGracePeriod, value);
			}
		}

		virtual public bool ExistPaymentGracePeriod{
			get
			{
				return Strategy.ExistUnitRole(Meta.PaymentGracePeriod);
			}
		}

		virtual public void RemovePaymentGracePeriod()
		{
			Strategy.RemoveUnitRole(Meta.PaymentGracePeriod);
		}


		virtual public Media LogoImage
		{ 
			get
			{
				return (Media) Strategy.GetCompositeRole(Meta.LogoImage);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.LogoImage ,value);
			}
		}

		virtual public bool ExistLogoImage
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.LogoImage);
			}
		}

		virtual public void RemoveLogoImage()
		{
			Strategy.RemoveCompositeRole(Meta.LogoImage);
		}



		virtual public global::System.Int32? PaymentNetDays 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.PaymentNetDays);
			}
			set
			{
				Strategy.SetUnitRole(Meta.PaymentNetDays, value);
			}
		}

		virtual public bool ExistPaymentNetDays{
			get
			{
				return Strategy.ExistUnitRole(Meta.PaymentNetDays);
			}
		}

		virtual public void RemovePaymentNetDays()
		{
			Strategy.RemoveUnitRole(Meta.PaymentNetDays);
		}


		virtual public Facility DefaultFacility
		{ 
			get
			{
				return (Facility) Strategy.GetCompositeRole(Meta.DefaultFacility);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.DefaultFacility ,value);
			}
		}

		virtual public bool ExistDefaultFacility
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.DefaultFacility);
			}
		}

		virtual public void RemoveDefaultFacility()
		{
			Strategy.RemoveCompositeRole(Meta.DefaultFacility);
		}



		virtual public global::System.String Name 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.Name);
			}
			set
			{
				Strategy.SetUnitRole(Meta.Name, value);
			}
		}

		virtual public bool ExistName{
			get
			{
				return Strategy.ExistUnitRole(Meta.Name);
			}
		}

		virtual public void RemoveName()
		{
			Strategy.RemoveUnitRole(Meta.Name);
		}


		virtual public global::Allors.Extent<StringTemplate> SalesOrderTemplates
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.SalesOrderTemplate);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.SalesOrderTemplate, value);
			}
		}

		virtual public void AddSalesOrderTemplate (StringTemplate value)
		{
			Strategy.AddCompositeRole(Meta.SalesOrderTemplate, value);
		}

		virtual public void RemoveSalesOrderTemplate (StringTemplate value)
		{
			Strategy.RemoveCompositeRole(Meta.SalesOrderTemplate,value);
		}

		virtual public bool ExistSalesOrderTemplates
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.SalesOrderTemplate);
			}
		}

		virtual public void RemoveSalesOrderTemplates()
		{
			Strategy.RemoveCompositeRoles(Meta.SalesOrderTemplate);
		}



		virtual public global::System.Decimal? CreditLimit 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.CreditLimit);
			}
			set
			{
				Strategy.SetUnitRole(Meta.CreditLimit, value);
			}
		}

		virtual public bool ExistCreditLimit{
			get
			{
				return Strategy.ExistUnitRole(Meta.CreditLimit);
			}
		}

		virtual public void RemoveCreditLimit()
		{
			Strategy.RemoveUnitRole(Meta.CreditLimit);
		}


		virtual public ShipmentMethod DefaultShipmentMethod
		{ 
			get
			{
				return (ShipmentMethod) Strategy.GetCompositeRole(Meta.DefaultShipmentMethod);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.DefaultShipmentMethod ,value);
			}
		}

		virtual public bool ExistDefaultShipmentMethod
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.DefaultShipmentMethod);
			}
		}

		virtual public void RemoveDefaultShipmentMethod()
		{
			Strategy.RemoveCompositeRole(Meta.DefaultShipmentMethod);
		}


		virtual public Carrier DefaultCarrier
		{ 
			get
			{
				return (Carrier) Strategy.GetCompositeRole(Meta.DefaultCarrier);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.DefaultCarrier ,value);
			}
		}

		virtual public bool ExistDefaultCarrier
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.DefaultCarrier);
			}
		}

		virtual public void RemoveDefaultCarrier()
		{
			Strategy.RemoveCompositeRole(Meta.DefaultCarrier);
		}



		virtual public global::System.Decimal? OrderThreshold 
		{
			get
			{
				return (global::System.Decimal?) Strategy.GetUnitRole(Meta.OrderThreshold);
			}
			set
			{
				Strategy.SetUnitRole(Meta.OrderThreshold, value);
			}
		}

		virtual public bool ExistOrderThreshold{
			get
			{
				return Strategy.ExistUnitRole(Meta.OrderThreshold);
			}
		}

		virtual public void RemoveOrderThreshold()
		{
			Strategy.RemoveUnitRole(Meta.OrderThreshold);
		}


		virtual public PaymentMethod DefaultPaymentMethod
		{ 
			get
			{
				return (PaymentMethod) Strategy.GetCompositeRole(Meta.DefaultPaymentMethod);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.DefaultPaymentMethod ,value);
			}
		}

		virtual public bool ExistDefaultPaymentMethod
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.DefaultPaymentMethod);
			}
		}

		virtual public void RemoveDefaultPaymentMethod()
		{
			Strategy.RemoveCompositeRole(Meta.DefaultPaymentMethod);
		}



		virtual public global::System.Int32? NextSalesOrderNumber 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.NextSalesOrderNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.NextSalesOrderNumber, value);
			}
		}

		virtual public bool ExistNextSalesOrderNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.NextSalesOrderNumber);
			}
		}

		virtual public void RemoveNextSalesOrderNumber()
		{
			Strategy.RemoveUnitRole(Meta.NextSalesOrderNumber);
		}


		virtual public InternalOrganisation Owner
		{ 
			get
			{
				return (InternalOrganisation) Strategy.GetCompositeRole(Meta.Owner);
			}
			set
			{
				Strategy.SetCompositeRole(Meta.Owner ,value);
			}
		}

		virtual public bool ExistOwner
		{
			get
			{
				return Strategy.ExistCompositeRole(Meta.Owner);
			}
		}

		virtual public void RemoveOwner()
		{
			Strategy.RemoveCompositeRole(Meta.Owner);
		}


		virtual public global::Allors.Extent<FiscalYearInvoiceNumber> FiscalYearInvoiceNumbers
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.FiscalYearInvoiceNumber);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.FiscalYearInvoiceNumber, value);
			}
		}

		virtual public void AddFiscalYearInvoiceNumber (FiscalYearInvoiceNumber value)
		{
			Strategy.AddCompositeRole(Meta.FiscalYearInvoiceNumber, value);
		}

		virtual public void RemoveFiscalYearInvoiceNumber (FiscalYearInvoiceNumber value)
		{
			Strategy.RemoveCompositeRole(Meta.FiscalYearInvoiceNumber,value);
		}

		virtual public bool ExistFiscalYearInvoiceNumbers
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.FiscalYearInvoiceNumber);
			}
		}

		virtual public void RemoveFiscalYearInvoiceNumbers()
		{
			Strategy.RemoveCompositeRoles(Meta.FiscalYearInvoiceNumber);
		}


		virtual public global::Allors.Extent<PaymentMethod> PaymentMethods
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.PaymentMethod);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.PaymentMethod, value);
			}
		}

		virtual public void AddPaymentMethod (PaymentMethod value)
		{
			Strategy.AddCompositeRole(Meta.PaymentMethod, value);
		}

		virtual public void RemovePaymentMethod (PaymentMethod value)
		{
			Strategy.RemoveCompositeRole(Meta.PaymentMethod,value);
		}

		virtual public bool ExistPaymentMethods
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.PaymentMethod);
			}
		}

		virtual public void RemovePaymentMethods()
		{
			Strategy.RemoveCompositeRoles(Meta.PaymentMethod);
		}



		virtual public global::System.String SalesOrderNumberPrefix 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.SalesOrderNumberPrefix);
			}
			set
			{
				Strategy.SetUnitRole(Meta.SalesOrderNumberPrefix, value);
			}
		}

		virtual public bool ExistSalesOrderNumberPrefix{
			get
			{
				return Strategy.ExistUnitRole(Meta.SalesOrderNumberPrefix);
			}
		}

		virtual public void RemoveSalesOrderNumberPrefix()
		{
			Strategy.RemoveUnitRole(Meta.SalesOrderNumberPrefix);
		}



		virtual public global::System.Int32? NextOutgoingShipmentNumber 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.NextOutgoingShipmentNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.NextOutgoingShipmentNumber, value);
			}
		}

		virtual public bool ExistNextOutgoingShipmentNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.NextOutgoingShipmentNumber);
			}
		}

		virtual public void RemoveNextOutgoingShipmentNumber()
		{
			Strategy.RemoveUnitRole(Meta.NextOutgoingShipmentNumber);
		}


		virtual public global::Allors.Extent<StringTemplate> CustomerShipmentTemplates
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.CustomerShipmentTemplate);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.CustomerShipmentTemplate, value);
			}
		}

		virtual public void AddCustomerShipmentTemplate (StringTemplate value)
		{
			Strategy.AddCompositeRole(Meta.CustomerShipmentTemplate, value);
		}

		virtual public void RemoveCustomerShipmentTemplate (StringTemplate value)
		{
			Strategy.RemoveCompositeRole(Meta.CustomerShipmentTemplate,value);
		}

		virtual public bool ExistCustomerShipmentTemplates
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.CustomerShipmentTemplate);
			}
		}

		virtual public void RemoveCustomerShipmentTemplates()
		{
			Strategy.RemoveCompositeRoles(Meta.CustomerShipmentTemplate);
		}



		virtual public global::System.Int32? NextSalesInvoiceNumber 
		{
			get
			{
				return (global::System.Int32?) Strategy.GetUnitRole(Meta.NextSalesInvoiceNumber);
			}
			set
			{
				Strategy.SetUnitRole(Meta.NextSalesInvoiceNumber, value);
			}
		}

		virtual public bool ExistNextSalesInvoiceNumber{
			get
			{
				return Strategy.ExistUnitRole(Meta.NextSalesInvoiceNumber);
			}
		}

		virtual public void RemoveNextSalesInvoiceNumber()
		{
			Strategy.RemoveUnitRole(Meta.NextSalesInvoiceNumber);
		}



		virtual public global::System.Guid? UniqueId 
		{
			get
			{
				return (global::System.Guid?) Strategy.GetUnitRole(Meta.UniqueId);
			}
			set
			{
				Strategy.SetUnitRole(Meta.UniqueId, value);
			}
		}

		virtual public bool ExistUniqueId{
			get
			{
				return Strategy.ExistUnitRole(Meta.UniqueId);
			}
		}

		virtual public void RemoveUniqueId()
		{
			Strategy.RemoveUnitRole(Meta.UniqueId);
		}



		virtual public global::System.String DisplayName 
		{
			get
			{
				return (global::System.String) Strategy.GetUnitRole(Meta.DisplayName);
			}
			set
			{
				Strategy.SetUnitRole(Meta.DisplayName, value);
			}
		}

		virtual public bool ExistDisplayName{
			get
			{
				return Strategy.ExistUnitRole(Meta.DisplayName);
			}
		}

		virtual public void RemoveDisplayName()
		{
			Strategy.RemoveUnitRole(Meta.DisplayName);
		}


		virtual public global::Allors.Extent<Permission> DeniedPermissions
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.DeniedPermission);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.DeniedPermission, value);
			}
		}

		virtual public void AddDeniedPermission (Permission value)
		{
			Strategy.AddCompositeRole(Meta.DeniedPermission, value);
		}

		virtual public void RemoveDeniedPermission (Permission value)
		{
			Strategy.RemoveCompositeRole(Meta.DeniedPermission,value);
		}

		virtual public bool ExistDeniedPermissions
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.DeniedPermission);
			}
		}

		virtual public void RemoveDeniedPermissions()
		{
			Strategy.RemoveCompositeRoles(Meta.DeniedPermission);
		}


		virtual public global::Allors.Extent<SecurityToken> SecurityTokens
		{ 
			get
			{
				return Strategy.GetCompositeRoles(Meta.SecurityToken);
			}
			set
			{
				Strategy.SetCompositeRoles(Meta.SecurityToken, value);
			}
		}

		virtual public void AddSecurityToken (SecurityToken value)
		{
			Strategy.AddCompositeRole(Meta.SecurityToken, value);
		}

		virtual public void RemoveSecurityToken (SecurityToken value)
		{
			Strategy.RemoveCompositeRole(Meta.SecurityToken,value);
		}

		virtual public bool ExistSecurityTokens
		{
			get
			{
				return Strategy.ExistCompositeRoles(Meta.SecurityToken);
			}
		}

		virtual public void RemoveSecurityTokens()
		{
			Strategy.RemoveCompositeRoles(Meta.SecurityToken);
		}



		virtual public global::Allors.Extent<PickList> PickListsWhereStore
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.PickListsWhereStore);
			}
		}

		virtual public bool ExistPickListsWhereStore
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.PickListsWhereStore);
			}
		}


		virtual public global::Allors.Extent<StoreRevenue> StoreRevenuesWhereStore
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.StoreRevenuesWhereStore);
			}
		}

		virtual public bool ExistStoreRevenuesWhereStore
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.StoreRevenuesWhereStore);
			}
		}


		virtual public global::Allors.Extent<SalesInvoice> SalesInvoicesWhereStore
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SalesInvoicesWhereStore);
			}
		}

		virtual public bool ExistSalesInvoicesWhereStore
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SalesInvoicesWhereStore);
			}
		}


		virtual public global::Allors.Extent<StoreRevenueHistory> StoreRevenueHistoriesWhereStore
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.StoreRevenueHistoriesWhereStore);
			}
		}

		virtual public bool ExistStoreRevenueHistoriesWhereStore
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.StoreRevenueHistoriesWhereStore);
			}
		}


		virtual public global::Allors.Extent<SalesOrder> SalesOrdersWhereStore
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.SalesOrdersWhereStore);
			}
		}

		virtual public bool ExistSalesOrdersWhereStore
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.SalesOrdersWhereStore);
			}
		}


		virtual public global::Allors.Extent<Shipment> ShipmentsWhereStore
		{ 
			get
			{
				return Strategy.GetCompositeAssociations(Meta.ShipmentsWhereStore);
			}
		}

		virtual public bool ExistShipmentsWhereStore
		{
			get
			{
				return Strategy.ExistCompositeAssociations(Meta.ShipmentsWhereStore);
			}
		}

	}

	public class StoreMeta
	{
		public static readonly StoreMeta Instance = new StoreMeta();

		public global::Allors.Meta.Class ObjectType = global::Allors.Meta.Classes.Store;

		public global::Allors.Meta.RoleType ShipmentThreshold 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreShipmentThreshold;
			}
		} 
		public global::Allors.Meta.RoleType SalesInvoiceTemplate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreSalesInvoiceTemplate;
			}
		} 
		public global::Allors.Meta.RoleType OutgoingShipmentNumberPrefix 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreOutgoingShipmentNumberPrefix;
			}
		} 
		public global::Allors.Meta.RoleType SalesInvoiceNumberPrefix 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreSalesInvoiceNumberPrefix;
			}
		} 
		public global::Allors.Meta.RoleType PaymentGracePeriod 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StorePaymentGracePeriod;
			}
		} 
		public global::Allors.Meta.RoleType LogoImage 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreLogoImage;
			}
		} 
		public global::Allors.Meta.RoleType PaymentNetDays 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StorePaymentNetDays;
			}
		} 
		public global::Allors.Meta.RoleType DefaultFacility 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreDefaultFacility;
			}
		} 
		public global::Allors.Meta.RoleType Name 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreName;
			}
		} 
		public global::Allors.Meta.RoleType SalesOrderTemplate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreSalesOrderTemplate;
			}
		} 
		public global::Allors.Meta.RoleType CreditLimit 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreCreditLimit;
			}
		} 
		public global::Allors.Meta.RoleType DefaultShipmentMethod 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreDefaultShipmentMethod;
			}
		} 
		public global::Allors.Meta.RoleType DefaultCarrier 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreDefaultCarrier;
			}
		} 
		public global::Allors.Meta.RoleType OrderThreshold 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreOrderThreshold;
			}
		} 
		public global::Allors.Meta.RoleType DefaultPaymentMethod 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreDefaultPaymentMethod;
			}
		} 
		public global::Allors.Meta.RoleType NextSalesOrderNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreNextSalesOrderNumber;
			}
		} 
		public global::Allors.Meta.RoleType Owner 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreOwner;
			}
		} 
		public global::Allors.Meta.RoleType FiscalYearInvoiceNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreFiscalYearInvoiceNumber;
			}
		} 
		public global::Allors.Meta.RoleType PaymentMethod 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StorePaymentMethod;
			}
		} 
		public global::Allors.Meta.RoleType SalesOrderNumberPrefix 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreSalesOrderNumberPrefix;
			}
		} 
		public global::Allors.Meta.RoleType NextOutgoingShipmentNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreNextOutgoingShipmentNumber;
			}
		} 
		public global::Allors.Meta.RoleType CustomerShipmentTemplate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreCustomerShipmentTemplate;
			}
		} 
		public global::Allors.Meta.RoleType NextSalesInvoiceNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.StoreNextSalesInvoiceNumber;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
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

		public global::Allors.Meta.AssociationType PickListsWhereStore 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PickListStore;
			}
		} 
		public global::Allors.Meta.AssociationType StoreRevenuesWhereStore 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.StoreRevenueStore;
			}
		} 
		public global::Allors.Meta.AssociationType SalesInvoicesWhereStore 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesInvoiceStore;
			}
		} 
		public global::Allors.Meta.AssociationType StoreRevenueHistoriesWhereStore 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.StoreRevenueHistoryStore;
			}
		} 
		public global::Allors.Meta.AssociationType SalesOrdersWhereStore 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesOrderStore;
			}
		} 
		public global::Allors.Meta.AssociationType ShipmentsWhereStore 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.ShipmentStore;
			}
		} 

	}
}