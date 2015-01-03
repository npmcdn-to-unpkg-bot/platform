namespace Allors.Meta
{
	using System;

	public partial class ExpenseEntryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;

            this.ConcreteRoles.Description.IsRequiredOverride = true;
		}
	}
}