namespace Allors.Meta
{
    public partial class ActivityUsageClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.UnitOfMeasure.IsRequired = true;
        }
	}
}