namespace Allors.Meta
{
    public partial class WorkEffortFixedAssetAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.FixedAsset.IsRequired = true;
            this.Roles.Assignment.IsRequired = true;
        }
	}
}