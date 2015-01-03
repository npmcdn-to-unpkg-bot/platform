namespace Allors.Meta
{
	using System;

	public partial class MarketingPackageClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}