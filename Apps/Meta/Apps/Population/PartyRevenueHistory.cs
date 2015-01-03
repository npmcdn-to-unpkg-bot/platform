namespace Allors.Meta
{
	using System;

	public partial class PartyRevenueHistoryClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Revenue.IsRequired = true;
	    }
	}
}