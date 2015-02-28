namespace Allors.Meta
{
    public partial class ShipmentInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.ShipmentNumber.IsRequired = true;
            this.Roles.ShipToParty.IsRequired = true;
            this.Roles.ShipFromParty.IsRequired = true;
        }
	}
}