namespace Allors.Meta
{
    public partial class InternalOrganisationClass
	{
	    internal override void AppsExtend()
        {
			this.PurchaseInvoiceCounter.RoleType.IsRequired = true;
			this.QuoteCounter.RoleType.IsRequired = true;
			this.PurchaseOrderCounter.RoleType.IsRequired = true;
			this.IncomingShipmentCounter.RoleType.IsRequired = true;
            this.SubAccountCounter.RoleType.IsRequired = true;

            this.Name.RoleType.IsRequired = true;
            this.FiscalYearStartMonth.RoleType.IsRequired = true;
            this.FiscalYearStartDay.RoleType.IsRequired = true;
            this.DoAccounting.RoleType.IsRequired = true;
            this.InvoiceSequence.RoleType.IsRequired = true;

	        this.ConcreteRoles.DefaultPaymentMethod.IsRequiredOverride = true;
            this.ConcreteRoles.Locale.IsRequiredOverride = true;
            this.ConcreteRoles.PreferredCurrency.IsRequiredOverride = true;
	        this.ConcreteRoles.BillingAddress.IsRequiredOverride = true;
        }
	}
}