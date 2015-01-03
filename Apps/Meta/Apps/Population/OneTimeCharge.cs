namespace Allors.Meta
{
	using System;

	public partial class OneTimeChargeClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
        }
	}
}