namespace Allors.Meta
{
	using System;

	public partial class InvoiceItemInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.TotalIncVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalBasePrice.IsRequired = true;
			this.Roles.TotalSurcharge.IsRequired = true;
			this.Roles.TotalInvoiceAdjustment.IsRequired = true;
			this.Roles.TotalExVatCustomerCurrency.IsRequired = true;
			this.Roles.TotalDiscount.IsRequired = true;
			this.Roles.CalculatedUnitPrice.IsRequired = true;
			this.Roles.UnitDiscount.IsRequired = true;
			this.Roles.TotalIncVat.IsRequired = true;
			this.Roles.UnitBasePrice.IsRequired = true;
			this.Roles.TotalSurchargeCustomerCurrency.IsRequired = true;
			this.Roles.TotalInvoiceAdjustmentCustomerCurrency.IsRequired = true;
			this.Roles.AmountPaid.IsRequired = true;
			this.Roles.TotalDiscountCustomerCurrency.IsRequired = true;
			this.Roles.UnitSurcharge.IsRequired = true;
			this.Roles.TotalExVat.IsRequired = true;
			this.Roles.TotalBasePriceCustomerCurrency.IsRequired = true;
			this.Roles.TotalVat.IsRequired = true;
			this.Roles.UnitVat.IsRequired = true;

		}
	}
}