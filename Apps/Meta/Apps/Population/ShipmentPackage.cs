namespace Allors.Meta
{
    public partial class ShipmentPackageClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.SequenceNumber.IsRequired = true;

            this.Roles.CreationDate.IsRequired = true;
		}
	}
}