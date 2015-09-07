namespace Allors.Meta
{
    public partial class AccountingPeriodClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Active.IsRequired = true;
            this.Roles.PeriodNumber.IsRequired = true;
            this.Roles.TimeFrequency.IsRequired = true;
        }
	}
}