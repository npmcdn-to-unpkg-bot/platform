namespace Allors.Meta
{
	using System;

	public partial class CustomerReturnClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CurrentObjectState.IsRequired = true;

            this.ConcreteRoles.ShipToAddress.IsRequiredOverride = true;
        }
	}
}