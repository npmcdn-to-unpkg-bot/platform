namespace Allors.Meta
{
    public partial class TransferClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CurrentObjectState.IsRequired = true;

            this.ConcreteRoles.ShipToAddress.IsRequiredOverride = true;
        }
	}
}