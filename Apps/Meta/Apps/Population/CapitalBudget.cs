namespace Allors.Meta
{
    public partial class CapitalBudgetClass
	{
	    internal override void AppsExtend()
	    {
	        this.ConcreteRoles.CurrentObjectState.IsRequiredOverride = true;
	    }
	}
}