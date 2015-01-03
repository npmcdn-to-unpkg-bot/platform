namespace Allors.Meta
{
	using System;

	public partial class SalesChannelRevenueClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Year.IsRequired = true;
            this.Roles.Month.IsRequired = true;
            this.Roles.Revenue.IsRequired = true;
        }
	}
}