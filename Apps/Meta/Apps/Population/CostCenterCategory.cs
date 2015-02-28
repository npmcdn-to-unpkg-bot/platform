namespace Allors.Meta
{
    public partial class CostCenterCategoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}