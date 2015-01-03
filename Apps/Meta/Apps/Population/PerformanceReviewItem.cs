namespace Allors.Meta
{
	using System;

	public partial class PerformanceReviewItemClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.PerformanceReviewItemType.IsRequired = true;
            this.Roles.RatingType.IsRequired = true;
        }
	}
}