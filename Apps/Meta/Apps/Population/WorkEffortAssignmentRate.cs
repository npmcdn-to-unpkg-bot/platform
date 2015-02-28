namespace Allors.Meta
{
    public partial class WorkEffortAssignmentRateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.RateType.IsRequired = true;
            this.Roles.WorkEffortPartyAssignment.IsRequired = true;
        }
	}
}