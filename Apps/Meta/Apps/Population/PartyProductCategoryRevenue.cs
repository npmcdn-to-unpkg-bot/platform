namespace Allors.Meta
{
    public partial class PartyProductCategoryRevenueClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Year.IsRequired = true;
            this.Roles.Month.IsRequired = true;
            this.Roles.Revenue.IsRequired = true;
            this.Roles.Quantity.IsRequired = true;
        }
	}
}