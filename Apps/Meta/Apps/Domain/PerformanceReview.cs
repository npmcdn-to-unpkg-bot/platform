namespace Allors.Meta
{
    public partial class PerformanceReviewClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employee.IsRequired = true;
        }
	}
}