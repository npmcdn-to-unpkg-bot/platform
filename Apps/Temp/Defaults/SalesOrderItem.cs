namespace Allors.Meta
{
	using System;

	public partial class SalesOrderItemClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.InitialProfitMargin.IsRequired = true;
			this.Roles.QuantityShortFalled.IsRequired = true;
			this.Roles.MaintainedProfitMargin.IsRequired = true;
			this.Roles.QuantityShipped.IsRequired = true;
			this.Roles.QuantityPicked.IsRequired = true;
			this.Roles.UnitPurchasePrice.IsRequired = true;
			this.Roles.QuantityReserved.IsRequired = true;
			this.Roles.QuantityPendingShipment.IsRequired = true;
			this.Roles.MaintainedMarkupPercentage.IsRequired = true;
			this.Roles.InitialMarkupPercentage.IsRequired = true;
			this.Roles.QuantityRequestsShipping.IsRequired = true;

		}
	}
}