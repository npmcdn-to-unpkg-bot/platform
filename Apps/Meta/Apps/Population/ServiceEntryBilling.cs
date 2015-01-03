namespace Allors.Meta
{
	using System;

	public partial class ServiceEntryBillingClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.InvoiceItem.IsRequired = true;
            this.Roles.ServiceEntry.IsRequired = true;
        }
	}
}