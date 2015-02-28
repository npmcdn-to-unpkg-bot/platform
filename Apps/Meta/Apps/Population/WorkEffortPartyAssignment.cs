namespace Allors.Meta
{
    public partial class WorkEffortPartyAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Assignment.IsRequired = true;
            this.Roles.Party.IsRequired = true;
        }
	}
}