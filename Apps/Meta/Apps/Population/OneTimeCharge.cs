namespace Allors.Meta
{
    public partial class OneTimeChargeClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
        }
	}
}