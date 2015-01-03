namespace Allors.Meta
{
	using System;

	public partial class EngagementRateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.RatingType.IsRequired = true;
            this.Roles.BillingRate.IsRequired = true;
        }
	}
}