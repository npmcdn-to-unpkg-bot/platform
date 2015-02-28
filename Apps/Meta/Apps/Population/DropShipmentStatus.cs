namespace Allors.Meta
{
    public partial class DropShipmentStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.DropShipmentObjectState.IsRequired = true;
        }
	}
}