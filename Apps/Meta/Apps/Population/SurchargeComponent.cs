namespace Allors.Meta
{
	using System;

	public partial class SurchargeComponentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Percentage.IsRequired = true;
		}
	}
}