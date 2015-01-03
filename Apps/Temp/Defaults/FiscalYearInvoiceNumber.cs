namespace Allors.Meta
{
	using System;

	public partial class FiscalYearInvoiceNumberClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.NextSalesInvoiceNumber.IsRequired = true;

		}
	}
}