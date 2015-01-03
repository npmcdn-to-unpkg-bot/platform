namespace Allors.Meta
{
	using System;

	public partial class OrderItemInterface
	{
	    internal override void AppsExtend()
        {
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
			this.Roles.DeliveryDate.IsRequired = true;
			this.Roles.TotalIncVat.IsRequired = true;
			this.Roles.TotalSurchargeAsPercentage.IsRequired = true;
			this.Roles.TotalDiscountCustomerCurrency.IsRequired = true;
			this.Roles.TotalDiscount.IsRequired = true;
			this.Roles.TotalSurcharge.IsRequired = true;
			this.Roles.TotalBasePrice.IsRequired = true;
			this.Roles.TotalExVat.IsRequired = true;
			this.Roles.TotalBasePriceCustomerCurrency.IsRequired = true;

		}
	}
}