namespace Allors.Meta
{
	using System;

	public partial class PurchaseOrderItemClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.QuantityReceived.IsRequired = true;

		}
	}
}