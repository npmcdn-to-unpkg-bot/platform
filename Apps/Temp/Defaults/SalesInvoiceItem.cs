namespace Allors.Meta
{
	using System;

	public partial class SalesInvoiceItemClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.InitialMarkupPercentage.IsRequired = true;
			this.Roles.MaintainedMarkupPercentage.IsRequired = true;
			this.Roles.UnitPurchasePrice.IsRequired = true;
			this.Roles.InitialProfitMargin.IsRequired = true;
			this.Roles.MaintainedProfitMargin.IsRequired = true;

		}
	}
}