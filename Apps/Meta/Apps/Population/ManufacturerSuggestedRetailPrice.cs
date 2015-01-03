namespace Allors.Meta
{
	using System;

	public partial class ManufacturerSuggestedRetailPriceClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
        }
	}
}