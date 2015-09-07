namespace Allors.Meta
{
    public partial class WorkEffortStatusClass
	{
	    internal override void AppsExtend()
	    {
	        this.StartDateTime.RoleType.IsRequired = true;
            this.WorkEffortObjectState.RoleType.IsRequired = true;
        }
	}
}