namespace Allors.Meta
{
	using System;

	public partial class GeoLocatableInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.Latitude.IsRequired = true;
			this.Roles.Longitude.IsRequired = true;

		}
	}
}