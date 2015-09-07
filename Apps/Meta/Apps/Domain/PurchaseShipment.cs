namespace Allors.Meta
{
    public partial class PurchaseShipmentClass
	{
	    internal override void AppsExtend()
        {
            this.CurrentObjectState.RoleType.IsRequired = true;
            this.Facility.RoleType.IsRequired = true;

            this.ConcreteRoles.ShipToParty.IsRequiredOverride = true;
            this.ConcreteRoles.EstimatedArrivalDate.IsRequiredOverride = true;
        }
	}
}