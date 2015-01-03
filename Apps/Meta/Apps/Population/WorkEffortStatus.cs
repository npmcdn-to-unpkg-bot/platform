namespace Allors.Meta
{
	using System;

	public partial class WorkEffortStatusClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.StartDateTime.IsRequired = true;
            this.Roles.WorkEffortObjectState.IsRequired = true;
        }
	}
}