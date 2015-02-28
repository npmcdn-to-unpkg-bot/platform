namespace Allors.Meta
{
    public partial class ShipmentItemClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.QuantityShipped.IsRequired = true;
		}
	}
}