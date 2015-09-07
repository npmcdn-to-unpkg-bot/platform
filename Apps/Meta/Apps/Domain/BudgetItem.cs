namespace Allors.Meta
{
    public partial class BudgetItemClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.Purpose.IsRequired = true;
        }
	}
}