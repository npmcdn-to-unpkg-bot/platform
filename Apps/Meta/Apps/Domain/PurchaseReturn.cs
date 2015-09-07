namespace Allors.Meta
{
    public partial class PurchaseReturnClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CurrentObjectState.IsRequired = true;

            this.ConcreteRoles.ShipToAddress.IsRequiredOverride = true;
        }
	}
}