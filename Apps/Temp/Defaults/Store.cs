namespace Allors.Meta
{
	using System;

	public partial class StoreClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.NextSalesOrderNumber.IsRequired = true;
			this.Roles.NextSalesInvoiceNumber.IsRequired = true;

		}
	}
}