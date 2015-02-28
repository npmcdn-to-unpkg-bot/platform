namespace Allors.Meta
{
    public partial class BudgetRevisionImpactClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Reason.IsRequired = true;
        }
	}
}