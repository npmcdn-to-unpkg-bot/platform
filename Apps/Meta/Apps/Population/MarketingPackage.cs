namespace Allors.Meta
{
    public partial class MarketingPackageClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}