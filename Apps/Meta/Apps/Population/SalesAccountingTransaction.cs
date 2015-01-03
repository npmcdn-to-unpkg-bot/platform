namespace Allors.Meta
{
	using System;

	public partial class SalesAccountingTransactionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Invoice.IsRequired = true;
        }
	}
}