namespace Allors.Meta
{
	using System;

	public partial class WorkEffortPartyAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Assignment.IsRequired = true;
            this.Roles.Party.IsRequired = true;
        }
	}
}