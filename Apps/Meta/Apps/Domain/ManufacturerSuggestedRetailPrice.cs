namespace Allors.Meta
{
    public partial class ManufacturerSuggestedRetailPriceClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
        }
	}
}