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

			this.TotalDiscountAsPercentage.RoleType.IsRequired = true;
			this.UnitVat.RoleType.IsRequired = true;
			this.TotalVatCustomerCurrency.RoleType.IsRequired = true;
			this.TotalVat.RoleType.IsRequired = true;
			this.UnitSurcharge.RoleType.IsRequired = true;
			this.UnitDiscount.RoleType.IsRequired = true;
			this.PreviousQuantity.RoleType.IsRequired = true;
			this.TotalExVatCustomerCurrency.RoleType.IsRequired = true;
			this.TotalIncVatCustomerCurrency.RoleType.IsRequired = true;
			this.UnitBasePrice.RoleType.IsRequired = true;
			this.CalculatedUnitPrice.RoleType.IsRequired = true;
			this.TotalOrderAdjustmentCustomerCurrency.RoleType.IsRequired = true;
			this.TotalOrderAdjustment.RoleType.IsRequired = true;
			this.TotalSurchargeCustomerCurrency.RoleType.IsRequired = true;
			this.TotalIncVat.RoleType.IsRequired = true;
			this.TotalSurchargeAsPercentage.RoleType.IsRequired = true;
			this.TotalDiscountCustomerCurrency.RoleType.IsRequired = true;
			this.TotalDiscount.RoleType.IsRequired = true;
			this.TotalSurcharge.RoleType.IsRequired = true;
			this.TotalBasePrice.RoleType.IsRequired = true;
			this.TotalExVat.RoleType.IsRequired = true;
			this.TotalBasePriceCustomerCurrency.RoleType.IsRequired = true;

            this.QuantityOrdered.RoleType.IsRequired = true;
            this.DerivedVatRate.RoleType.IsRequired = true;
		}
	}
}