namespace Allors.Meta
{
    public partial class OrderAdjustmentInterface
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Amount.IsRequired = true;
            this.Roles.Percentage.IsRequired = true;
        }
	}
}