namespace Allors.Meta
{
	using System;

	public partial class SalesInvoiceStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.SalesInvoiceObjectState.IsRequired = true;
        }
	}
}