namespace Allors.Meta
{
	using System;

	public partial class RequirementBudgetAllocationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.BudgetItem.IsRequired = true;
            this.Roles.Requirement.IsRequired = true;
        }
	}
}