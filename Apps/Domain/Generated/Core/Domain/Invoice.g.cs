// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{
	public partial interface Invoice :  UserInterfaceable,Localised,Transitional,SearchResult,Commentable,Searchable,Printable,UniquelyIdentifiable, Allors.IObject
	{


		global::System.Decimal? TotalShippingAndHandlingCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalShippingAndHandlingCustomerCurrency{get;}

		void RemoveTotalShippingAndHandlingCustomerCurrency();


		Currency CustomerCurrency
		{ 
			get;
			set;
		}

		bool ExistCustomerCurrency
		{
			get;
		}

		void RemoveCustomerCurrency();


		global::System.String Description 
		{
			get;
			set;
		}

		bool ExistDescription{get;}

		void RemoveDescription();


		ShippingAndHandlingCharge ShippingAndHandlingCharge
		{ 
			get;
			set;
		}

		bool ExistShippingAndHandlingCharge
		{
			get;
		}

		void RemoveShippingAndHandlingCharge();


		global::System.Decimal? TotalFeeCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalFeeCustomerCurrency{get;}

		void RemoveTotalFeeCustomerCurrency();


		Fee Fee
		{ 
			get;
			set;
		}

		bool ExistFee
		{
			get;
		}

		void RemoveFee();


		global::System.Decimal? TotalExVatCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalExVatCustomerCurrency{get;}

		void RemoveTotalExVatCustomerCurrency();


		global::System.String CustomerReference 
		{
			get;
			set;
		}

		bool ExistCustomerReference{get;}

		void RemoveCustomerReference();


		DiscountAdjustment DiscountAdjustment
		{ 
			get;
			set;
		}

		bool ExistDiscountAdjustment
		{
			get;
		}

		void RemoveDiscountAdjustment();


		global::System.Decimal? AmountPaid 
		{
			get;
			set;
		}

		bool ExistAmountPaid{get;}

		void RemoveAmountPaid();


		global::System.Decimal? TotalDiscount 
		{
			get;
			set;
		}

		bool ExistTotalDiscount{get;}

		void RemoveTotalDiscount();


		BillingAccount BillingAccount
		{ 
			get;
			set;
		}

		bool ExistBillingAccount
		{
			get;
		}

		void RemoveBillingAccount();


		global::System.Decimal? TotalIncVat 
		{
			get;
			set;
		}

		bool ExistTotalIncVat{get;}

		void RemoveTotalIncVat();


		global::System.Decimal? TotalSurcharge 
		{
			get;
			set;
		}

		bool ExistTotalSurcharge{get;}

		void RemoveTotalSurcharge();


		global::System.Decimal? TotalBasePrice 
		{
			get;
			set;
		}

		bool ExistTotalBasePrice{get;}

		void RemoveTotalBasePrice();


		global::System.Decimal? TotalVatCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalVatCustomerCurrency{get;}

		void RemoveTotalVatCustomerCurrency();


		global::System.DateTime? InvoiceDate 
		{
			get;
			set;
		}

		bool ExistInvoiceDate{get;}

		void RemoveInvoiceDate();


		global::System.DateTime? EntryDate 
		{
			get;
			set;
		}

		bool ExistEntryDate{get;}

		void RemoveEntryDate();


		global::System.Decimal? TotalIncVatCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalIncVatCustomerCurrency{get;}

		void RemoveTotalIncVatCustomerCurrency();


		global::System.Decimal? TotalShippingAndHandling 
		{
			get;
			set;
		}

		bool ExistTotalShippingAndHandling{get;}

		void RemoveTotalShippingAndHandling();


		global::System.Decimal? TotalBasePriceCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalBasePriceCustomerCurrency{get;}

		void RemoveTotalBasePriceCustomerCurrency();


		SurchargeAdjustment SurchargeAdjustment
		{ 
			get;
			set;
		}

		bool ExistSurchargeAdjustment
		{
			get;
		}

		void RemoveSurchargeAdjustment();


		global::System.Decimal? TotalExVat 
		{
			get;
			set;
		}

		bool ExistTotalExVat{get;}

		void RemoveTotalExVat();


		global::Allors.Extent<InvoiceTerm> InvoiceTerms
		{ 
			get;
			set;
		}

		void AddInvoiceTerm (InvoiceTerm value);

		void RemoveInvoiceTerm (InvoiceTerm value);

		bool ExistInvoiceTerms
		{
			get;
		}

		void RemoveInvoiceTerms();


		global::System.Decimal? TotalSurchargeCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalSurchargeCustomerCurrency{get;}

		void RemoveTotalSurchargeCustomerCurrency();


		global::System.String InvoiceNumber 
		{
			get;
			set;
		}

		bool ExistInvoiceNumber{get;}

		void RemoveInvoiceNumber();


		global::System.String Message 
		{
			get;
			set;
		}

		bool ExistMessage{get;}

		void RemoveMessage();


		VatRegime VatRegime
		{ 
			get;
			set;
		}

		bool ExistVatRegime
		{
			get;
		}

		void RemoveVatRegime();


		global::System.Decimal? TotalDiscountCustomerCurrency 
		{
			get;
			set;
		}

		bool ExistTotalDiscountCustomerCurrency{get;}

		void RemoveTotalDiscountCustomerCurrency();


		global::System.Decimal? TotalVat 
		{
			get;
			set;
		}

		bool ExistTotalVat{get;}

		void RemoveTotalVat();


		global::System.Decimal? TotalFee 
		{
			get;
			set;
		}

		bool ExistTotalFee{get;}

		void RemoveTotalFee();



		SalesAccountingTransaction SalesAccountingTransactionWhereInvoice
		{
			get;
		}

		bool ExistSalesAccountingTransactionWhereInvoice
		{
			get;
		}


		global::Allors.Extent<PaymentApplication> PaymentApplicationsWhereInvoice
		{ 
			get;
		}

		bool ExistPaymentApplicationsWhereInvoice
		{
			get;
		}

	}

	public class InvoiceMeta
	{
		public static readonly InvoiceMeta Instance = new InvoiceMeta();

		public global::Allors.Meta.Interface ObjectType = global::Allors.Meta.Interfaces.Invoice;

		public global::Allors.Meta.RoleType TotalShippingAndHandlingCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalShippingAndHandlingCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType CustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType Description 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceDescription;
			}
		} 
		public global::Allors.Meta.RoleType ShippingAndHandlingCharge 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceShippingAndHandlingCharge;
			}
		} 
		public global::Allors.Meta.RoleType TotalFeeCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalFeeCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType Fee 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceFee;
			}
		} 
		public global::Allors.Meta.RoleType TotalExVatCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalExVatCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType CustomerReference 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceCustomerReference;
			}
		} 
		public global::Allors.Meta.RoleType DiscountAdjustment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceDiscountAdjustment;
			}
		} 
		public global::Allors.Meta.RoleType AmountPaid 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceAmountPaid;
			}
		} 
		public global::Allors.Meta.RoleType TotalDiscount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalDiscount;
			}
		} 
		public global::Allors.Meta.RoleType BillingAccount 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceBillingAccount;
			}
		} 
		public global::Allors.Meta.RoleType TotalIncVat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalIncVat;
			}
		} 
		public global::Allors.Meta.RoleType TotalSurcharge 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalSurcharge;
			}
		} 
		public global::Allors.Meta.RoleType TotalBasePrice 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalBasePrice;
			}
		} 
		public global::Allors.Meta.RoleType TotalVatCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalVatCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType InvoiceDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceInvoiceDate;
			}
		} 
		public global::Allors.Meta.RoleType EntryDate 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceEntryDate;
			}
		} 
		public global::Allors.Meta.RoleType TotalIncVatCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalIncVatCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType TotalShippingAndHandling 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalShippingAndHandling;
			}
		} 
		public global::Allors.Meta.RoleType TotalBasePriceCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalBasePriceCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType SurchargeAdjustment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceSurchargeAdjustment;
			}
		} 
		public global::Allors.Meta.RoleType TotalExVat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalExVat;
			}
		} 
		public global::Allors.Meta.RoleType InvoiceTerm 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceInvoiceTerm;
			}
		} 
		public global::Allors.Meta.RoleType TotalSurchargeCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalSurchargeCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType InvoiceNumber 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceInvoiceNumber;
			}
		} 
		public global::Allors.Meta.RoleType Message 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceMessage;
			}
		} 
		public global::Allors.Meta.RoleType VatRegime 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceVatRegime;
			}
		} 
		public global::Allors.Meta.RoleType TotalDiscountCustomerCurrency 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalDiscountCustomerCurrency;
			}
		} 
		public global::Allors.Meta.RoleType TotalVat 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalVat;
			}
		} 
		public global::Allors.Meta.RoleType TotalFee 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.InvoiceTotalFee;
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
		public global::Allors.Meta.RoleType Locale 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.LocalisedLocale;
			}
		} 
		public global::Allors.Meta.RoleType Comment 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.CommentableComment;
			}
		} 
		public global::Allors.Meta.RoleType SearchData 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.SearchableSearchData;
			}
		} 
		public global::Allors.Meta.RoleType PrintContent 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.PrintablePrintContent;
			}
		} 
		public global::Allors.Meta.RoleType UniqueId 
		{
			get
			{
				return global::Allors.Meta.RoleTypes.UniquelyIdentifiableUniqueId;
			}
		} 

		public global::Allors.Meta.AssociationType SalesAccountingTransactionWhereInvoice 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.SalesAccountingTransactionInvoice;
			}
		} 
		public global::Allors.Meta.AssociationType PaymentApplicationsWhereInvoice 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PaymentApplicationInvoice;
			}
		} 
		public global::Allors.Meta.AssociationType PrintQueuesWherePrintable 
		{
			get
			{
				return global::Allors.Meta.AssociationTypes.PrintQueuePrintable;
			}
		} 

	}
}