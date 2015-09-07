namespace Allors.Meta
{
    public partial class WorkEffortPartyAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Assignment.RoleType.IsRequired = true;
            this.Party.RoleType.IsRequired = true;
        }
	}
}