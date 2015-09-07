namespace Allors.Meta
{
    public partial class DesiredProductFeatureClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Required.IsRequired = true;
            this.Roles.ProductFeature.IsRequired = true;
        }
	}
}