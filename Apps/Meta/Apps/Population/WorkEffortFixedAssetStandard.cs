namespace Allors.Meta
{
	using System;

	public partial class WorkEffortFixedAssetStandardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.FixedAsset.IsRequired = true;
        }
	}
}