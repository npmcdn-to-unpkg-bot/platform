namespace Allors.Meta
{
	using System;

	public partial class PerformanceReviewClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employee.IsRequired = true;
        }
	}
}