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

		}
	}
}