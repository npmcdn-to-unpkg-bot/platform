namespace Allors.Meta
{
    public partial class PartyProductCategoryRevenueHistoryClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Quantity.IsRequired = true;
            this.Roles.Revenue.IsRequired = true;
        }
	}
}