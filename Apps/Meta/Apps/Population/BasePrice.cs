namespace Allors.Meta
{
	using System;

	public partial class BasePriceClass
	{
	    internal override void AppsExtend()
	    {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
            this.ConcreteRoles.Currency.IsRequiredOverride = true;
	    }
	}
}