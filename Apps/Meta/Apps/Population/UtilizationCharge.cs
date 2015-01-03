namespace Allors.Meta
{
	using System;

	public partial class UtilizationChargeClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}