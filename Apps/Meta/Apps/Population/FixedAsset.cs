namespace Allors.Meta
{
	using System;

	public partial class FixedAssetInterface
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Name.IsRequired = true;
	    }
	}
}