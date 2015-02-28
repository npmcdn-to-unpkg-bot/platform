namespace Allors.Meta
{
    public partial class ElectronicAddressInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.ElectronicAddressString.IsRequired = true;
        }
	}
}