namespace Allors.Meta
{
	using System;

	public partial class SalesInvoiceItemStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.SalesInvoiceItemObjectState.IsRequired = true;
        }
	}
}