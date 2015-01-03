namespace Allors.Meta
{
	using System;

	public partial class GlBudgetAllocationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.AllocationPercentage.IsRequired = true;
            this.Roles.BudgetItem.IsRequired = true;
            this.Roles.GeneralLedgerAccount.IsRequired = true;
        }
	}
}