namespace Allors.Meta
{
	using System;

	public partial class DepreciationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.FixedAsset.IsRequired = true;
        }
	}
}