namespace Allors.Meta
{
	using System;

	public partial class OrderShipmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.Picked.IsRequired = true;
            this.Roles.ShipmentItem.IsRequired = true;
        }
	}
}