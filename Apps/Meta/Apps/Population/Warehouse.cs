namespace Allors.Meta
{
	using System;

	public partial class WarehouseClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Owner.IsRequiredOverride = true;
        }
	}
}