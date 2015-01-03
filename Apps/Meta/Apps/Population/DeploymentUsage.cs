namespace Allors.Meta
{
	using System;

	public partial class DeploymentUsageInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.TimeFrequency.IsRequired = true;
        }
	}
}