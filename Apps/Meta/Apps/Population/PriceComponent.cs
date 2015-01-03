namespace Allors.Meta
{
	using System;

	public partial class PriceComponentInterface
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Price.IsRequired = true;
            this.Roles.Description.IsRequired = true;
            this.Roles.SpecifiedFor.IsRequired = true;
        }
	}
}