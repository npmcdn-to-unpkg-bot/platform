// Allors generated file. 
// Do not edit this file, changes will be overwritten.
namespace Allors.Domain
{

	public partial class PurchaseOrder
	{		
		internal override void OnBuild(global::Allors.IObjectBuilder objectBuilder)
		{
			var builder = (PurchaseOrderBuilder)objectBuilder;
			

			if(builder.TotalBasePriceCustomerCurrency.HasValue)
			{
				this.TotalBasePriceCustomerCurrency = builder.TotalBasePriceCustomerCurrency.Value;
			}			
					

			if(builder.TotalIncVatCustomerCurrency.HasValue)
			{
				this.TotalIncVatCustomerCurrency = builder.TotalIncVatCustomerCurrency.Value;
			}			
					

			if(builder.TotalDiscountCustomerCurrency.HasValue)
			{
				this.TotalDiscountCustomerCurrency = builder.TotalDiscountCustomerCurrency.Value;
			}			
		

			this.CustomerReference = builder.CustomerReference;
					

			if(builder.TotalExVat.HasValue)
			{
				this.TotalExVat = builder.TotalExVat.Value;
			}			
					

			if(builder.TotalVat.HasValue)
			{
				this.TotalVat = builder.TotalVat.Value;
			}			
					

			if(builder.TotalSurcharge.HasValue)
			{
				this.TotalSurcharge = builder.TotalSurcharge.Value;
			}			
		

			this.OrderNumber = builder.OrderNumber;
					

			if(builder.TotalVatCustomerCurrency.HasValue)
			{
				this.TotalVatCustomerCurrency = builder.TotalVatCustomerCurrency.Value;
			}			
					

			if(builder.TotalDiscount.HasValue)
			{
				this.TotalDiscount = builder.TotalDiscount.Value;
			}			
		

			this.Message = builder.Message;
					

			if(builder.TotalShippingAndHandlingCustomerCurrency.HasValue)
			{
				this.TotalShippingAndHandlingCustomerCurrency = builder.TotalShippingAndHandlingCustomerCurrency.Value;
			}			
					

			if(builder.EntryDate.HasValue)
			{
				this.EntryDate = builder.EntryDate.Value;
			}			
					

			if(builder.TotalIncVat.HasValue)
			{
				this.TotalIncVat = builder.TotalIncVat.Value;
			}			
					

			if(builder.TotalSurchargeCustomerCurrency.HasValue)
			{
				this.TotalSurchargeCustomerCurrency = builder.TotalSurchargeCustomerCurrency.Value;
			}			
					

			if(builder.TotalFeeCustomerCurrency.HasValue)
			{
				this.TotalFeeCustomerCurrency = builder.TotalFeeCustomerCurrency.Value;
			}			
					

			if(builder.TotalShippingAndHandling.HasValue)
			{
				this.TotalShippingAndHandling = builder.TotalShippingAndHandling.Value;
			}			
					

			if(builder.OrderDate.HasValue)
			{
				this.OrderDate = builder.OrderDate.Value;
			}			
					

			if(builder.TotalExVatCustomerCurrency.HasValue)
			{
				this.TotalExVatCustomerCurrency = builder.TotalExVatCustomerCurrency.Value;
			}			
					

			if(builder.DeliveryDate.HasValue)
			{
				this.DeliveryDate = builder.DeliveryDate.Value;
			}			
					

			if(builder.TotalBasePrice.HasValue)
			{
				this.TotalBasePrice = builder.TotalBasePrice.Value;
			}			
					

			if(builder.TotalFee.HasValue)
			{
				this.TotalFee = builder.TotalFee.Value;
			}			
		

			this.DisplayName = builder.DisplayName;
		

			this.PrintContent = builder.PrintContent;
					

			if(builder.UniqueId.HasValue)
			{
				this.UniqueId = builder.UniqueId.Value;
			}			
		

			this.Comment = builder.Comment;
		
			if(builder.PurchaseOrderItems!=null)
			{
				this.PurchaseOrderItems = builder.PurchaseOrderItems.ToArray();
			}


			this.PreviousTakenViaSupplier = builder.PreviousTakenViaSupplier;


			if(builder.PaymentStatuses!=null)
			{
				this.PaymentStatuses = builder.PaymentStatuses.ToArray();
			}


			this.CurrentPaymentStatus = builder.CurrentPaymentStatus;



			this.TakenViaSupplier = builder.TakenViaSupplier;



			this.CurrentObjectState = builder.CurrentObjectState;



			this.CurrentShipmentStatus = builder.CurrentShipmentStatus;



			this.TakenViaContactMechanism = builder.TakenViaContactMechanism;


			if(builder.OrderStatuses!=null)
			{
				this.OrderStatuses = builder.OrderStatuses.ToArray();
			}


			this.BillToContactMechanism = builder.BillToContactMechanism;


			if(builder.ShipmentStatuses!=null)
			{
				this.ShipmentStatuses = builder.ShipmentStatuses.ToArray();
			}


			this.ShipToBuyer = builder.ShipToBuyer;



			this.CurrentOrderStatus = builder.CurrentOrderStatus;



			this.Facility = builder.Facility;



			this.ShipToAddress = builder.ShipToAddress;



			this.PreviousObjectState = builder.PreviousObjectState;



			this.BillToPurchaser = builder.BillToPurchaser;



			this.CustomerCurrency = builder.CustomerCurrency;



			this.Fee = builder.Fee;


			if(builder.OrderTerms!=null)
			{
				this.OrderTerms = builder.OrderTerms.ToArray();
			}

			if(builder.ValidOrderItems!=null)
			{
				this.ValidOrderItems = builder.ValidOrderItems.ToArray();
			}


			this.DiscountAdjustment = builder.DiscountAdjustment;



			this.OrderKind = builder.OrderKind;



			this.VatRegime = builder.VatRegime;



			this.ShippingAndHandlingCharge = builder.ShippingAndHandlingCharge;



			this.SurchargeAdjustment = builder.SurchargeAdjustment;


			if(builder.DeniedPermissions!=null)
			{
				this.DeniedPermissions = builder.DeniedPermissions.ToArray();
			}

			if(builder.SecurityTokens!=null)
			{
				this.SecurityTokens = builder.SecurityTokens.ToArray();
			}


			this.SearchData = builder.SearchData;



			this.Locale = builder.Locale;


		}
	}

	public partial class PurchaseOrderBuilder : Allors.ObjectBuilder<PurchaseOrder> , OrderBuilder, global::System.IDisposable
	{		
		public PurchaseOrderBuilder(Allors.ISession session) : base(session)
	    {
	    }

				public global::System.Collections.Generic.List<PurchaseOrderItem> PurchaseOrderItems {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithPurchaseOrderItem(PurchaseOrderItem value)
		        {
					if(this.PurchaseOrderItems == null)
					{
						this.PurchaseOrderItems = new global::System.Collections.Generic.List<PurchaseOrderItem>(); 
					}
		            this.PurchaseOrderItems.Add(value);
		            return this;
		        }		

				
				public Party PreviousTakenViaSupplier {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithPreviousTakenViaSupplier(Party value)
		        {
		            if(this.PreviousTakenViaSupplier!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.PreviousTakenViaSupplier = value;
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<PurchaseOrderStatus> PaymentStatuses {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithPaymentStatus(PurchaseOrderStatus value)
		        {
					if(this.PaymentStatuses == null)
					{
						this.PaymentStatuses = new global::System.Collections.Generic.List<PurchaseOrderStatus>(); 
					}
		            this.PaymentStatuses.Add(value);
		            return this;
		        }		

				
				public PurchaseOrderStatus CurrentPaymentStatus {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithCurrentPaymentStatus(PurchaseOrderStatus value)
		        {
		            if(this.CurrentPaymentStatus!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CurrentPaymentStatus = value;
		            return this;
		        }		

				
				public Party TakenViaSupplier {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTakenViaSupplier(Party value)
		        {
		            if(this.TakenViaSupplier!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.TakenViaSupplier = value;
		            return this;
		        }		

				
				public PurchaseOrderObjectState CurrentObjectState {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithCurrentObjectState(PurchaseOrderObjectState value)
		        {
		            if(this.CurrentObjectState!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CurrentObjectState = value;
		            return this;
		        }		

				
				public PurchaseOrderStatus CurrentShipmentStatus {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithCurrentShipmentStatus(PurchaseOrderStatus value)
		        {
		            if(this.CurrentShipmentStatus!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CurrentShipmentStatus = value;
		            return this;
		        }		

				
				public ContactMechanism TakenViaContactMechanism {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTakenViaContactMechanism(ContactMechanism value)
		        {
		            if(this.TakenViaContactMechanism!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.TakenViaContactMechanism = value;
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<PurchaseOrderStatus> OrderStatuses {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithOrderStatus(PurchaseOrderStatus value)
		        {
					if(this.OrderStatuses == null)
					{
						this.OrderStatuses = new global::System.Collections.Generic.List<PurchaseOrderStatus>(); 
					}
		            this.OrderStatuses.Add(value);
		            return this;
		        }		

				
				public ContactMechanism BillToContactMechanism {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithBillToContactMechanism(ContactMechanism value)
		        {
		            if(this.BillToContactMechanism!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.BillToContactMechanism = value;
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<PurchaseOrderStatus> ShipmentStatuses {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithShipmentStatus(PurchaseOrderStatus value)
		        {
					if(this.ShipmentStatuses == null)
					{
						this.ShipmentStatuses = new global::System.Collections.Generic.List<PurchaseOrderStatus>(); 
					}
		            this.ShipmentStatuses.Add(value);
		            return this;
		        }		

				
				public InternalOrganisation ShipToBuyer {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithShipToBuyer(InternalOrganisation value)
		        {
		            if(this.ShipToBuyer!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.ShipToBuyer = value;
		            return this;
		        }		

				
				public PurchaseOrderStatus CurrentOrderStatus {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithCurrentOrderStatus(PurchaseOrderStatus value)
		        {
		            if(this.CurrentOrderStatus!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CurrentOrderStatus = value;
		            return this;
		        }		

				
				public Facility Facility {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithFacility(Facility value)
		        {
		            if(this.Facility!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Facility = value;
		            return this;
		        }		

				
				public PostalAddress ShipToAddress {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithShipToAddress(PostalAddress value)
		        {
		            if(this.ShipToAddress!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.ShipToAddress = value;
		            return this;
		        }		

				
				public PurchaseOrderObjectState PreviousObjectState {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithPreviousObjectState(PurchaseOrderObjectState value)
		        {
		            if(this.PreviousObjectState!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.PreviousObjectState = value;
		            return this;
		        }		

				
				public InternalOrganisation BillToPurchaser {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithBillToPurchaser(InternalOrganisation value)
		        {
		            if(this.BillToPurchaser!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.BillToPurchaser = value;
		            return this;
		        }		

				
				public Currency CustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithCustomerCurrency(Currency value)
		        {
		            if(this.CustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.CustomerCurrency = value;
		            return this;
		        }		

				
				public global::System.Decimal? TotalBasePriceCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalBasePriceCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalBasePriceCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalBasePriceCustomerCurrency = value;
		            return this;
		        }	

				public global::System.Decimal? TotalIncVatCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalIncVatCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalIncVatCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalIncVatCustomerCurrency = value;
		            return this;
		        }	

				public global::System.Decimal? TotalDiscountCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalDiscountCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalDiscountCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalDiscountCustomerCurrency = value;
		            return this;
		        }	

				public global::System.String CustomerReference {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithCustomerReference(global::System.String value)
		        {
				    if(this.CustomerReference!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.CustomerReference = value;
		            return this;
		        }	

				public Fee Fee {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithFee(Fee value)
		        {
		            if(this.Fee!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Fee = value;
		            return this;
		        }		

				
				public global::System.Decimal? TotalExVat {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalExVat(global::System.Decimal? value)
		        {
				    if(this.TotalExVat!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalExVat = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<OrderTerm> OrderTerms {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithOrderTerm(OrderTerm value)
		        {
					if(this.OrderTerms == null)
					{
						this.OrderTerms = new global::System.Collections.Generic.List<OrderTerm>(); 
					}
		            this.OrderTerms.Add(value);
		            return this;
		        }		

				
				public global::System.Decimal? TotalVat {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalVat(global::System.Decimal? value)
		        {
				    if(this.TotalVat!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalVat = value;
		            return this;
		        }	

				public global::System.Decimal? TotalSurcharge {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalSurcharge(global::System.Decimal? value)
		        {
				    if(this.TotalSurcharge!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalSurcharge = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<OrderItem> ValidOrderItems {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithValidOrderItem(OrderItem value)
		        {
					if(this.ValidOrderItems == null)
					{
						this.ValidOrderItems = new global::System.Collections.Generic.List<OrderItem>(); 
					}
		            this.ValidOrderItems.Add(value);
		            return this;
		        }		

				
				public global::System.String OrderNumber {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithOrderNumber(global::System.String value)
		        {
				    if(this.OrderNumber!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.OrderNumber = value;
		            return this;
		        }	

				public global::System.Decimal? TotalVatCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalVatCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalVatCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalVatCustomerCurrency = value;
		            return this;
		        }	

				public global::System.Decimal? TotalDiscount {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalDiscount(global::System.Decimal? value)
		        {
				    if(this.TotalDiscount!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalDiscount = value;
		            return this;
		        }	

				public global::System.String Message {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithMessage(global::System.String value)
		        {
				    if(this.Message!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Message = value;
		            return this;
		        }	

				public global::System.Decimal? TotalShippingAndHandlingCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalShippingAndHandlingCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalShippingAndHandlingCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalShippingAndHandlingCustomerCurrency = value;
		            return this;
		        }	

				public global::System.DateTime? EntryDate {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithEntryDate(global::System.DateTime? value)
		        {
				    if(this.EntryDate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.EntryDate = value;
		            return this;
		        }	

				public DiscountAdjustment DiscountAdjustment {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithDiscountAdjustment(DiscountAdjustment value)
		        {
		            if(this.DiscountAdjustment!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.DiscountAdjustment = value;
		            return this;
		        }		

				
				public OrderKind OrderKind {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithOrderKind(OrderKind value)
		        {
		            if(this.OrderKind!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.OrderKind = value;
		            return this;
		        }		

				
				public global::System.Decimal? TotalIncVat {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalIncVat(global::System.Decimal? value)
		        {
				    if(this.TotalIncVat!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalIncVat = value;
		            return this;
		        }	

				public global::System.Decimal? TotalSurchargeCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalSurchargeCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalSurchargeCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalSurchargeCustomerCurrency = value;
		            return this;
		        }	

				public VatRegime VatRegime {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithVatRegime(VatRegime value)
		        {
		            if(this.VatRegime!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.VatRegime = value;
		            return this;
		        }		

				
				public global::System.Decimal? TotalFeeCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalFeeCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalFeeCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalFeeCustomerCurrency = value;
		            return this;
		        }	

				public global::System.Decimal? TotalShippingAndHandling {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalShippingAndHandling(global::System.Decimal? value)
		        {
				    if(this.TotalShippingAndHandling!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalShippingAndHandling = value;
		            return this;
		        }	

				public ShippingAndHandlingCharge ShippingAndHandlingCharge {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithShippingAndHandlingCharge(ShippingAndHandlingCharge value)
		        {
		            if(this.ShippingAndHandlingCharge!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.ShippingAndHandlingCharge = value;
		            return this;
		        }		

				
				public global::System.DateTime? OrderDate {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithOrderDate(global::System.DateTime? value)
		        {
				    if(this.OrderDate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.OrderDate = value;
		            return this;
		        }	

				public global::System.Decimal? TotalExVatCustomerCurrency {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalExVatCustomerCurrency(global::System.Decimal? value)
		        {
				    if(this.TotalExVatCustomerCurrency!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalExVatCustomerCurrency = value;
		            return this;
		        }	

				public global::System.DateTime? DeliveryDate {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithDeliveryDate(global::System.DateTime? value)
		        {
				    if(this.DeliveryDate!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DeliveryDate = value;
		            return this;
		        }	

				public global::System.Decimal? TotalBasePrice {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalBasePrice(global::System.Decimal? value)
		        {
				    if(this.TotalBasePrice!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalBasePrice = value;
		            return this;
		        }	

				public global::System.Decimal? TotalFee {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithTotalFee(global::System.Decimal? value)
		        {
				    if(this.TotalFee!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.TotalFee = value;
		            return this;
		        }	

				public SurchargeAdjustment SurchargeAdjustment {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithSurchargeAdjustment(SurchargeAdjustment value)
		        {
		            if(this.SurchargeAdjustment!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SurchargeAdjustment = value;
		            return this;
		        }		

				
				public global::System.String DisplayName {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithDisplayName(global::System.String value)
		        {
				    if(this.DisplayName!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.DisplayName = value;
		            return this;
		        }	

				public global::System.Collections.Generic.List<Permission> DeniedPermissions {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithDeniedPermission(Permission value)
		        {
					if(this.DeniedPermissions == null)
					{
						this.DeniedPermissions = new global::System.Collections.Generic.List<Permission>(); 
					}
		            this.DeniedPermissions.Add(value);
		            return this;
		        }		

				
				public global::System.Collections.Generic.List<SecurityToken> SecurityTokens {get; set;}	

				/// <exclude/>
				public PurchaseOrderBuilder WithSecurityToken(SecurityToken value)
		        {
					if(this.SecurityTokens == null)
					{
						this.SecurityTokens = new global::System.Collections.Generic.List<SecurityToken>(); 
					}
		            this.SecurityTokens.Add(value);
		            return this;
		        }		

				
				public global::System.String PrintContent {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithPrintContent(global::System.String value)
		        {
				    if(this.PrintContent!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.PrintContent = value;
		            return this;
		        }	

				public global::System.Guid? UniqueId {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithUniqueId(global::System.Guid? value)
		        {
				    if(this.UniqueId!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.UniqueId = value;
		            return this;
		        }	

				public SearchData SearchData {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithSearchData(SearchData value)
		        {
		            if(this.SearchData!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.SearchData = value;
		            return this;
		        }		

				
				public global::System.String Comment {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithComment(global::System.String value)
		        {
				    if(this.Comment!=null){throw new global::System.ArgumentException("One multicplicity");}
		            this.Comment = value;
		            return this;
		        }	

				public Locale Locale {get; set;}

				/// <exclude/>
				public PurchaseOrderBuilder WithLocale(Locale value)
		        {
		            if(this.Locale!=null){throw new global::System.ArgumentException("One multicplicity");}
					this.Locale = value;
		            return this;
		        }		

				

	}

	public partial class PurchaseOrders : global::Allors.ObjectsBase<PurchaseOrder>
	{
		public static readonly PurchaseOrderMeta Meta = PurchaseOrderMeta.Instance;

		public PurchaseOrders(Allors.ISession session) : base(session)
		{
		}

		public override Allors.Meta.Composite ObjectType
		{
			get
			{
				return Meta.ObjectType;
			}
		}
	}

}