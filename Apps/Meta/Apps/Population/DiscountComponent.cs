namespace Allors.Meta
{
    public partial class DiscountComponentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Percentage.IsRequired = true;
        }
	}
}