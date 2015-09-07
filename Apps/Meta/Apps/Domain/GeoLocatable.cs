namespace Allors.Meta
{
    public partial class GeoLocatableInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.Latitude.IsRequired = true;
			this.Roles.Longitude.IsRequired = true;

		}
	}
}