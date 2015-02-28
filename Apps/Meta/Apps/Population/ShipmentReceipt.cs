namespace Allors.Meta
{
    public partial class ShipmentReceiptClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ReceivedDateTime.IsRequired = true;
            this.Roles.InventoryItem.IsRequired = true;
            this.Roles.QuantityAccepted.IsRequired = true;
            this.Roles.QuantityRejected.IsRequired = true;
            this.Roles.ShipmentItem.IsRequired = true;
        }
	}
}