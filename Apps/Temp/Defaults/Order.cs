namespace Allors.Meta
{
	using System;

	public partial class OrderInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.TotalBasePriceCustomerCurrency.IsRequired = true;
			this.Roles.TotalIncVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalDiscountCustomerCurrency.IsRequired = true;
			this.Roles.TotalExVat.IsRequired = true;
			this.Roles.TotalVat.IsRequired = true;
			this.Roles.TotalSurcharge.IsRequired = true;
			this.Roles.OrderNumber.IsRequired = true;
			this.Roles.TotalVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalDiscount.IsRequired = true;
			this.Roles.TotalShippingAndHandlingCustomerCurrency.IsRequired = true;
			this.Roles.EntryDate.IsRequired = true;
			this.Roles.TotalIncVat.IsRequired = true;
			this.Roles.TotalSurchargeCustomerCurrency.IsRequired = true;
			this.Roles.TotalFeeCustomerCurrency.IsRequired = true;
			this.Roles.TotalShippingAndHandling.IsRequired = true;
			this.Roles.TotalExVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalBasePrice.IsRequired = true;
			this.Roles.TotalFee.IsRequired = true;

		}
	}
}