namespace Allors.Meta
{
    public partial class BudgetStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.BudgetObjectState.IsRequired = true;
        }
	}
}