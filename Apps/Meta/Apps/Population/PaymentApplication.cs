namespace Allors.Meta
{
    public partial class PaymentApplicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.AmountApplied.IsRequired = true;
        }
	}
}