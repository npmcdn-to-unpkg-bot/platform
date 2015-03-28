namespace Allors.Meta
{
	using System;

	public partial class OrderItemInterface
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("AC6B2E9E-DC3B-4FA5-80B2-EA13C0461F5F")){ObjectType=this, Name="Cancel"};
            new MethodType(AppsDomain.Instance, new Guid("A1E84095-C5A3-4E4E-B449-FC400A3E0D06")){ObjectType=this, Name="Reject"};
            new MethodType(AppsDomain.Instance, new Guid("D0FDE3AB-EEC4-46C6-A545-30C4EB57B9D9")){ObjectType=this, Name="Confirm"};
            new MethodType(AppsDomain.Instance, new Guid("D3953352-DB9E-4A59-8504-A0C400DC515E")){ObjectType=this, Name="Approve"};
            new MethodType(AppsDomain.Instance, new Guid("C1517567-1708-47E6-8298-9D9B157E45FF")){ObjectType=this, Name="Finish"};
            new MethodType(AppsDomain.Instance, new Guid("3962ED58-44BD-4A79-8F0C-6A98ED88BD44")){ObjectType=this, Name="Delete"};

			this.Roles.TotalDiscountAsPercentage.IsRequired = true;
			this.Roles.UnitVat.IsRequired = true;
			this.Roles.TotalVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalVat.IsRequired = true;
			this.Roles.UnitSurcharge.IsRequired = true;
			this.Roles.UnitDiscount.IsRequired = true;
			this.Roles.PreviousQuantity.IsRequired = true;
			this.Roles.TotalExVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalIncVatCustomerCurrency.IsRequired = true;
			this.Roles.UnitBasePrice.IsRequired = true;
			this.Roles.CalculatedUnitPrice.IsRequired = true;
			this.Roles.TotalOrderAdjustmentCustomerCurrency.IsRequired = true;
			this.Roles.TotalOrderAdjustment.IsRequired = true;
			this.Roles.TotalSurchargeCustomerCurrency.IsRequired = true;
			this.Roles.TotalIncVat.IsRequired = true;
			this.Roles.TotalSurchargeAsPercentage.IsRequired = true;
			this.Roles.TotalDiscountCustomerCurrency.IsRequired = true;
			this.Roles.TotalDiscount.IsRequired = true;
			this.Roles.TotalSurcharge.IsRequired = true;
			this.Roles.TotalBasePrice.IsRequired = true;
			this.Roles.TotalExVat.IsRequired = true;
			this.Roles.TotalBasePriceCustomerCurrency.IsRequired = true;

            this.Roles.QuantityOrdered.IsRequired = true;
            this.Roles.DerivedVatRate.IsRequired = true;
		}
	}
}