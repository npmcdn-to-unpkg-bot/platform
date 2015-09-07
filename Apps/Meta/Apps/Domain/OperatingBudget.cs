namespace Allors.Meta
{
    public partial class OperatingBudgetClass
	{
	    internal override void AppsExtend()
	    {
	        this.ConcreteRoles.CurrentObjectState.IsRequiredOverride = true;
	    }
	}
}