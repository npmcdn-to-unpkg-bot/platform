namespace Allors.Meta
{
    public partial class FiscalYearInvoiceNumberClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.NextSalesInvoiceNumber.IsRequired = true;

            this.Roles.FiscalYear.IsRequired = true;
		}
	}
}