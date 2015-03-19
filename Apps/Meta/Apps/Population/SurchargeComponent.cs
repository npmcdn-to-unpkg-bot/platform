namespace Allors.Meta
{
    public partial class SurchargeComponentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Percentage.IsRequired = true;
		}
	}
}