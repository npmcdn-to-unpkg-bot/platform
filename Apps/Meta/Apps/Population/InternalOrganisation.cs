namespace Allors.Meta
{
	using System;

	public partial class InternalOrganisationClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.NextPurchaseInvoiceNumber.IsRequired = true;
			this.Roles.NextQuoteNumber.IsRequired = true;
			this.Roles.NextPurchaseOrderNumber.IsRequired = true;
			this.Roles.NextIncomingShipmentNumber.IsRequired = true;

            this.Roles.Name.IsRequired = true;
            this.Roles.NextSubAccountNumber.IsRequired = true;
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