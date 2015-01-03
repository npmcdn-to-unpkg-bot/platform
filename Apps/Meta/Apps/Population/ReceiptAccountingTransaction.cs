namespace Allors.Meta
{
	using System;

	public partial class ReceiptAccountingTransactionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Receipt.IsRequired = true;
        }
	}
}