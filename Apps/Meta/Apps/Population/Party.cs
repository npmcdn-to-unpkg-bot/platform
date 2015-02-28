namespace Allors.Meta
{
    public partial class PartyInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.YTDRevenue.IsRequired = true;
			this.Roles.LastYearsRevenue.IsRequired = true;
			this.Roles.PartyName.IsRequired = true;
			this.Roles.OpenOrderAmount.IsRequired = true;

		}
	}
}