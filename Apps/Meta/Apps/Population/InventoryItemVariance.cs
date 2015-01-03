namespace Allors.Meta
{
	using System;

	public partial class InventoryItemVarianceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Reason.IsRequired = true;
            this.Roles.Quantity.IsRequired = true;
        }
	}
}