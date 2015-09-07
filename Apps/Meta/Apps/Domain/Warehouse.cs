namespace Allors.Meta
{
    public partial class WarehouseClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Owner.IsRequiredOverride = true;
        }
	}
}