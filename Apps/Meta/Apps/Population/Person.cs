namespace Allors.Meta
{
    public partial class PersonClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.YTDCommission.IsRequired = true;
			this.Roles.LastYearsCommission.IsRequired = true;

		}
	}
}