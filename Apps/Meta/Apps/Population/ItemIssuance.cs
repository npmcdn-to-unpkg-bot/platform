namespace Allors.Meta
{
	using System;

	public partial class ItemIssuanceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.InventoryItem.IsRequired = true;
            this.Roles.ShipmentItem.IsRequired = true;
        }
	}
}