namespace Allors.Meta
{
	using System;

	public partial class PackagingContentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.ShipmentItem.IsRequired = true;
        }
	}
}