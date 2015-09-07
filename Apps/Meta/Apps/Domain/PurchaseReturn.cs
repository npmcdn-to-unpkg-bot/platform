namespace Allors.Meta
{
    public partial class PurchaseReturnClass
	{
	    internal override void AppsExtend()
        {
            this.CurrentObjectState.RoleType.IsRequired = true;

            this.ConcreteRoles.ShipToAddress.IsRequiredOverride = true;
        }
	}
}