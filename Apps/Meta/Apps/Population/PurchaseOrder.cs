namespace Allors.Meta
{
	using System;

	public partial class PurchaseOrderClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.ShipToBuyer.IsRequired = true;
            this.Roles.BillToPurchaser.IsRequired = true;
            this.Roles.TakenViaSupplier.IsRequired = true;
            this.Roles.BillToContactMechanism.IsRequired = true;
        }
	}
}