namespace Allors.Meta
{
	using System;

	public partial class AccountingTransactionInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.DerivedTotalAmount.IsRequired = true;

		}
	}
}