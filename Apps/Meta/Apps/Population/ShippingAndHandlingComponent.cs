namespace Allors.Meta
{
    public partial class ShippingAndHandlingComponentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.SpecifiedFor.IsRequired = true;
        }
	}
}