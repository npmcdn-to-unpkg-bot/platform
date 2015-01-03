namespace Allors.Meta
{
	using System;

	public partial class PaymentApplicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.AmountApplied.IsRequired = true;
        }
	}
}