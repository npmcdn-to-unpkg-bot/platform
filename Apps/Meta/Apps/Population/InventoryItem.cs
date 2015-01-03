namespace Allors.Meta
{
	using System;

	public partial class InventoryItemInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.Name.IsRequired = true;
			this.Roles.Sku.IsRequired = true;
            this.Roles.Facility.IsRequired = true;
		}
	}
}