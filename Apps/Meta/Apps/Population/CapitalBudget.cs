namespace Allors.Meta
{
	using System;

	public partial class CapitalBudgetClass
	{
	    internal override void AppsExtend()
	    {
	        this.ConcreteRoles.CurrentObjectState.IsRequiredOverride = true;
	    }
	}
}