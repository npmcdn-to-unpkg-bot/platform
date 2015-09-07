namespace Allors.Meta
{
    public partial class WorkEffortInventoryAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Assignment.IsRequired = true;
            this.Roles.InventoryItem.IsRequired = true;
        }
	}
}