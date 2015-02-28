namespace Allors.Meta
{
    public partial class ReceiptAccountingTransactionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Receipt.IsRequired = true;
        }
	}
}