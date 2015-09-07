namespace Allors.Meta
{
    public partial class SupplierOfferingClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Supplier.IsRequired = true;
            this.Roles.ProductPurchasePrice.IsRequired = true;
        }
	}
}