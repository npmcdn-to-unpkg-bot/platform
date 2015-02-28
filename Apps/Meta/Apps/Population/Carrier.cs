namespace Allors.Meta
{
    public partial class CarrierClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}