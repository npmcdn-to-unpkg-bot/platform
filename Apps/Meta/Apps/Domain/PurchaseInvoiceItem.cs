namespace Allors.Meta
{
    public partial class PurchaseInvoiceItemClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Part.IsRequired = true;
            this.Roles.PurchaseInvoiceItemType.IsRequired = true;
            
            this.ConcreteRoles.ActualUnitPrice.IsRequiredOverride = true;
        }
	}
}