namespace Allors.Meta
{
    public partial class PriceComponentInterface
	{
	    internal override void AppsExtend()
	    {
            this.Roles.Description.IsRequired = true;
            this.Roles.SpecifiedFor.IsRequired = true;
        }
	}
}