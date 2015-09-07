namespace Allors.Meta
{
    public partial class PurchaseInvoiceItemClass
	{
	    internal override void AppsExtend()
        {
            this.Part.RoleType.IsRequired = true;
            this.PurchaseInvoiceItemType.RoleType.IsRequired = true;
            
            this.ConcreteRoles.ActualUnitPrice.IsRequiredOverride = true;
        }
	}
}