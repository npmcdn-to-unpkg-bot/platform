namespace Allors.Meta
{
	using System;

	public partial class FinancialAccountTransactionInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.TransactionDate.IsRequired = true;
        }
	}
}