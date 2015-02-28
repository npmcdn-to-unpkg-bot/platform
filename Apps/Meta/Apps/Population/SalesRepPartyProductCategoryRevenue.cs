namespace Allors.Meta
{
    public partial class SalesRepPartyProductCategoryRevenueClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Year.IsRequired = true;
            this.Roles.Month.IsRequired = true;
            this.Roles.Revenue.IsRequired = true;
        }
	}
}