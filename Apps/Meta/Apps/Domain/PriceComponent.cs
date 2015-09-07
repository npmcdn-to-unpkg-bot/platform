namespace Allors.Meta
{
    public partial class PriceComponentInterface
	{
	    internal override void AppsExtend()
	    {
            this.Description.RoleType.IsRequired = true;
            this.SpecifiedFor.RoleType.IsRequired = true;
        }
	}
}