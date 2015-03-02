namespace Allors.Meta
{
    public partial class InternalOrganisationClass
    {
        internal override void AppsExtend()
        {
            this.Roles.PurchaseInvoiceCounter.IsRequired = true;
            this.Roles.QuoteCounter.IsRequired = true;
            this.Roles.PurchaseOrderCounter.IsRequired = true;
            this.Roles.IncomingShipmentCounter.IsRequired = true;

            this.Roles.Name.IsRequired = true;
            this.Roles.FiscalYearStartMonth.IsRequired = true;
            this.Roles.FiscalYearStartDay.IsRequired = true;
            this.Roles.DoAccounting.IsRequired = true;
            this.Roles.InvoiceSequence.IsRequired = true;

            this.ConcreteRoles.DefaultPaymentMethod.IsRequiredOverride = true;
            this.ConcreteRoles.Locale.IsRequiredOverride = true;
            this.ConcreteRoles.PreferredCurrency.IsRequiredOverride = true;
        }
    }
}