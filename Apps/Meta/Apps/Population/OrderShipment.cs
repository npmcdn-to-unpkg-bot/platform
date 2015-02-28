namespace Allors.Meta
{
    public partial class OrderShipmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Quantity.IsRequired = true;
            this.Roles.Picked.IsRequired = true;
            this.Roles.ShipmentItem.IsRequired = true;
        }
	}
}