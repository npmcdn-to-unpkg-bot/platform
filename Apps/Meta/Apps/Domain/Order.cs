namespace Allors.Meta
{
	using System;

	public partial class OrderInterface
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("73F0DD8B-8290-48CC-8AAF-D5B1B578A841")){ObjectType=this, Name="Approve"};
            new MethodType(AppsDomain.Instance, new Guid("DB067D32-3007-4D11-93FF-D25FE8378B9B")){ObjectType=this, Name="Reject"};
            new MethodType(AppsDomain.Instance, new Guid("716909AB-F88C-4BD4-B238-87D117CE1515")){ObjectType=this, Name="Hold"};
            new MethodType(AppsDomain.Instance, new Guid("0D0F41BB-11C8-44A0-8B6D-1F7657BB85A8")){ObjectType=this, Name="Continue"};
            new MethodType(AppsDomain.Instance, new Guid("2142CD4A-C861-4E7A-986B-CDBFC1AD0E53")){ObjectType=this, Name="Confirm"};
            new MethodType(AppsDomain.Instance, new Guid("CC489BED-55FA-449D-BC22-C9E0954DA8E3")){ObjectType=this, Name="Cancel"};
            new MethodType(AppsDomain.Instance, new Guid("7154A033-6A07-49FE-B928-9EDD843FC56C")){ObjectType=this, Name="Complete"};
            new MethodType(AppsDomain.Instance, new Guid("E3441FE1-E403-4709-AF7F-84238D0E69F0")){ObjectType=this, Name="Finish"};

			this.TotalBasePriceCustomerCurrency.RoleType.IsRequired = true;
			this.TotalIncVatCustomerCurrency.RoleType.IsRequired = true;
			this.TotalDiscountCustomerCurrency.RoleType.IsRequired = true;
			this.TotalExVat.RoleType.IsRequired = true;
			this.TotalVat.RoleType.IsRequired = true;
			this.TotalSurcharge.RoleType.IsRequired = true;
			this.OrderNumber.RoleType.IsRequired = true;
			this.TotalVatCustomerCurrency.RoleType.IsRequired = true;
			this.TotalDiscount.RoleType.IsRequired = true;
			this.TotalShippingAndHandlingCustomerCurrency.RoleType.IsRequired = true;
			this.EntryDate.RoleType.IsRequired = true;
			this.TotalIncVat.RoleType.IsRequired = true;
			this.TotalSurchargeCustomerCurrency.RoleType.IsRequired = true;
			this.TotalFeeCustomerCurrency.RoleType.IsRequired = true;
			this.TotalShippingAndHandling.RoleType.IsRequired = true;
			this.TotalExVatCustomerCurrency.RoleType.IsRequired = true;
			this.TotalBasePrice.RoleType.IsRequired = true;
			this.TotalFee.RoleType.IsRequired = true;

            this.OrderDate.RoleType.IsRequired = true;

            this.OrderDate.RoleType.IsRequired = true;
            this.OrderDate.RoleType.IsRequired = true;
            this.OrderDate.RoleType.IsRequired = true;
            this.OrderDate.RoleType.IsRequired = true;
            this.OrderDate.RoleType.IsRequired = true;
            this.OrderDate.RoleType.IsRequired = true;
        }
	}
}