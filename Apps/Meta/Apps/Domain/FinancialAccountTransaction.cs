namespace Allors.Meta
{
    public partial class FinancialAccountTransactionInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.TransactionDate.IsRequired = true;
        }
	}
}