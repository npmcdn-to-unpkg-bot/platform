namespace Allors.Meta
{
	using System;

	public partial class PersonClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.YTDCommission.IsRequired = true;
			this.Roles.LastYearsCommission.IsRequired = true;

		}
	}
}