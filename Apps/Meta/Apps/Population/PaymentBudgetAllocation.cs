namespace Allors.Meta
{
	using System;

	public partial class PaymentBudgetAllocationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.BudgetItem.IsRequired = true;
            this.Roles.Payment.IsRequired = true;
        }
	}
}