namespace Allors.Meta
{
	using System;

	public partial class RequirementStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.RequirementObjectState.IsRequired = true;
        }
	}
}