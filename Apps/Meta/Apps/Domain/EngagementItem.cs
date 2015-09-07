namespace Allors.Meta
{
    public partial class EngagementItemInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}