namespace Allors.Meta
{
    public partial class FixedAssetInterface
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Name.IsRequired = true;
	    }
	}
}