namespace Allors.Meta
{
    public partial class PurchaseShipmentStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PurchaseShipmentObjectState.IsRequired = true;
        }
	}
}