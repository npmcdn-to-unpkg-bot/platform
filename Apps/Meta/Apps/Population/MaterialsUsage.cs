namespace Allors.Meta
{
	using System;

	public partial class MaterialsUsageClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;

            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}