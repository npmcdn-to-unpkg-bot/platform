namespace Allors.Meta
{
    public partial class BudgetRevisionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.RevisionDate.IsRequired = true;
        }
	}
}