namespace Allors.Meta
{
    public partial class CustomerShipmentStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.CustomerShipmentObjectState.IsRequired = true;
        }
	}
}