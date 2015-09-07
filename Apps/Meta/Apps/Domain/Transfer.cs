namespace Allors.Meta
{
    public partial class TransferClass
	{
	    internal override void AppsExtend()
        {
            this.CurrentObjectState.RoleType.IsRequired = true;

            this.ConcreteRoles.ShipToAddress.IsRequiredOverride = true;
        }
	}
}