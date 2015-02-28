namespace Allors.Meta
{
    public partial class PartyRevenueHistoryClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Revenue.IsRequired = true;
	    }
	}
}