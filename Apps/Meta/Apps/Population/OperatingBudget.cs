namespace Allors.Meta
{
	using System;

	public partial class OperatingBudgetClass
	{
	    internal override void AppsExtend()
	    {
	        this.ConcreteRoles.CurrentObjectState.IsRequiredOverride = true;
	    }
	}
}