namespace Allors.Meta
{
	using System;

	public partial class BudgetRevisionImpactClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Reason.IsRequired = true;
        }
	}
}