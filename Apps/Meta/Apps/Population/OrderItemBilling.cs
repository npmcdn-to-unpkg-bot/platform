namespace Allors.Meta
{
	using System;

	public partial class OrderItemBillingClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.OrderItem.IsRequired = true;
            this.Roles.Amount.IsRequired = true;
        }
	}
}