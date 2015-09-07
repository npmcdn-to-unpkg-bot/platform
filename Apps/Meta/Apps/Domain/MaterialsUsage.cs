namespace Allors.Meta
{
    public partial class MaterialsUsageClass
	{
	    internal override void AppsExtend()
        {
            this.Amount.RoleType.IsRequired = true;

            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}