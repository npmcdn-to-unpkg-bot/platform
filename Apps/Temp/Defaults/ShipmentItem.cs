namespace Allors.Meta
{
	using System;

	public partial class ShipmentItemClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.QuantityShipped.IsRequired = true;

		}
	}
}