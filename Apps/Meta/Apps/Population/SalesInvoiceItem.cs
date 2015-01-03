namespace Allors.Meta
{
	public partial class SalesInvoiceItemClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.CurrentObjectState.IsRequired = true;

			this.Roles.InitialMarkupPercentage.IsRequired = true;
			this.Roles.MaintainedMarkupPercentage.IsRequired = true;
			this.Roles.UnitPurchasePrice.IsRequired = true;
			this.Roles.InitialProfitMargin.IsRequired = true;
			this.Roles.MaintainedProfitMargin.IsRequired = true;

            this.Roles.SalesInvoiceItemType.IsRequired = true;

            this.ConcreteRoles.Quantity.IsRequiredOverride = true;
            this.ConcreteRoles.AmountPaid.IsRequiredOverride = true;
		}
	}
}