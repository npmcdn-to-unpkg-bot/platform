namespace Allors.Meta
{
    public partial class WorkEffortTypeClass
	{
	    internal override void AppsExtend()
	    {
	        this.Description.RoleType.IsRequired = true;
	        this.WorkEffortTypeKind.RoleType.IsRequired = true;
	    }
	}
}