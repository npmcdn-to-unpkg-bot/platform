namespace Allors.Meta
{
	using System;

	public partial class InventoryItemConfigurationInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.ComponentInventoryItem.IsRequired = true;
            this.Roles.InventoryItem.IsRequired = true;
            this.Roles.Quantity.IsRequired = true;
        }
	}
}