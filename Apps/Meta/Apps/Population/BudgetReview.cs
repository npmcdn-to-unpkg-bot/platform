namespace Allors.Meta
{
    public partial class BudgetReviewClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ReviewDate.IsRequired = true;
            this.Roles.Description.IsRequired = true;
        }
	}
}