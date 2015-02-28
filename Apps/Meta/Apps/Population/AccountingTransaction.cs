namespace Allors.Meta
{
    public partial class AccountingTransactionInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.DerivedTotalAmount.IsRequired = true;

            this.Roles.EntryDate.IsRequired = true;
            this.Roles.TransactionDate.IsRequired = true;
            this.Roles.Description.IsRequired = true;
		}
	}
}