namespace Allors.Meta
{
    public partial class WorkEffortFixedAssetStandardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.FixedAsset.IsRequired = true;
        }
	}
}