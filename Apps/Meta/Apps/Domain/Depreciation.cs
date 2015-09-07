namespace Allors.Meta
{
    public partial class DepreciationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.FixedAsset.IsRequired = true;
        }
	}
}