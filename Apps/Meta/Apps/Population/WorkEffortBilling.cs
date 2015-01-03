namespace Allors.Meta
{
	using System;

	public partial class WorkEffortBillingClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.InvoiceItem.IsRequired = true;
            this.Roles.WorkEffort.IsRequired = true;
        }
	}
}