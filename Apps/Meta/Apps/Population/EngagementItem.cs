namespace Allors.Meta
{
	using System;

	public partial class EngagementItemInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}