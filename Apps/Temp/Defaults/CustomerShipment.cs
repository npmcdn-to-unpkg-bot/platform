namespace Allors.Meta
{
	using System;

	public partial class CustomerShipmentClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.ShipmentValue.IsRequired = true;

		}
	}
}