namespace Allors.Meta
{
    public partial class SalesAccountingTransactionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Invoice.IsRequired = true;
        }
	}
}