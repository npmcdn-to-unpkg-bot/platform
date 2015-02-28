namespace Allors.Meta
{
    public partial class ChartOfAccountsClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}