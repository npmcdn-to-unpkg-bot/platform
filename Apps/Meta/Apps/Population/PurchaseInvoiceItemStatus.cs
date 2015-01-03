namespace Allors.Meta
{
	using System;

	public partial class PurchaseInvoiceItemStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PurchaseInvoiceItemObjectState.IsRequired = true;
        }
	}
}