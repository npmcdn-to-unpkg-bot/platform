namespace Allors.Meta
{
    public partial class PeriodInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.FromDate.IsRequired = true;
        }
	}
}