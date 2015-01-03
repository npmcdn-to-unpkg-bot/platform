namespace Allors.Meta
{
	using System;

	public partial class TimeEntryClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.GrossMargin.IsRequired = true;

		}
	}
}