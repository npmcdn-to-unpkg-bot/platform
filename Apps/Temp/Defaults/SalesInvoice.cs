namespace Allors.Meta
{
	using System;

	public partial class SalesInvoiceClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.InitialProfitMargin.IsRequired = true;
			this.Roles.InitialMarkupPercentage.IsRequired = true;
			this.Roles.MaintainedMarkupPercentage.IsRequired = true;
			this.Roles.MaintainedProfitMargin.IsRequired = true;
			this.Roles.TotalListPriceCustomerCurrency.IsRequired = true;

		}
	}
}