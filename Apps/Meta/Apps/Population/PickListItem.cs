namespace Allors.Meta
{
	using System;

	public partial class PickListItemClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.InventoryItem.IsRequired = true;
            this.Roles.RequestedQuantity.IsRequired = true;
        }
	}
}