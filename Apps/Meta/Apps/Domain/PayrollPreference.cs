namespace Allors.Meta
{
    public partial class PayrollPreferenceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.PaymentMethod.IsRequired = true;
            this.Roles.TimeFrequency.IsRequired = true;
        }
	}
}