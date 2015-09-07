namespace Allors.Meta
{
    public partial class ProductPurchasePriceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Price.IsRequired = true;
            this.Roles.Currency.IsRequired = true;
            this.Roles.UnitOfMeasure.IsRequired = true;
        }
	}
}