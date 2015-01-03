namespace Allors.Meta
{
	using System;

	public partial class PurchaseShipmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.Facility.IsRequired = true;

            this.ConcreteRoles.ShipToParty.IsRequiredOverride = true;
            this.ConcreteRoles.EstimatedArrivalDate.IsRequiredOverride = true;
        }
	}
}