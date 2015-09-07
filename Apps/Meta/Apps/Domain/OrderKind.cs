namespace Allors.Meta
{
    public partial class OrderKindClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.ScheduleManually.IsRequired = true;
        }
	}
}