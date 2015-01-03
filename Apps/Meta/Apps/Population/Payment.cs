namespace Allors.Meta
{
	using System;

	public partial class PaymentInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.EffectiveDate.IsRequired = true;
            this.Roles.Amount.IsRequired = true;
        }
	}
}