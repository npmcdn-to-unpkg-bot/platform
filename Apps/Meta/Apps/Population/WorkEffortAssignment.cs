namespace Allors.Meta
{
	using System;

	public partial class WorkEffortAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Assignment.IsRequired = true;
            this.Roles.Professional.IsRequired = true;
        }
	}
}