namespace Allors.Meta
{
    public partial class PostalBoundaryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Locality.IsRequired = true;
            this.Roles.Country.IsRequired = true;
        }
	}
}