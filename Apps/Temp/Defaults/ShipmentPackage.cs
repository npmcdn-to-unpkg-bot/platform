namespace Allors.Meta
{
	using System;

	public partial class ShipmentPackageClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.SequenceNumber.IsRequired = true;

		}
	}
}