namespace Allors.Meta
{
    public partial class ProductConfigurationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}