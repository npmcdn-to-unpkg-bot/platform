namespace Allors.Meta
{
	using System;

	public partial class PaymentMethodInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.CurrentBalance.IsRequired = true;

		}
	}
}