namespace Allors.Meta
{
	using System;

	public partial class OrderKindClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.ScheduleManually.IsRequired = true;
        }
	}
}