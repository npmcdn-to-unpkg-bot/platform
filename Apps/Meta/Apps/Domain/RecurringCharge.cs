namespace Allors.Meta
{
    public partial class RecurringChargeClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Price.IsRequiredOverride = true;
        }
	}
}