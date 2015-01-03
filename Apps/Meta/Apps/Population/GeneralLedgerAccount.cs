namespace Allors.Meta
{
	using System;

	public partial class GeneralLedgerAccountClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CashAccount.IsRequired = true;
            this.Roles.CostCenterAccount.IsRequired = true;
            this.Roles.CostCenterRequired.IsRequired = true;
            this.Roles.CostUnitAccount.IsRequired = true;
            this.Roles.CostUnitRequired.IsRequired = true;
            this.Roles.ReconciliationAccount.IsRequired = true;
            this.Roles.Protected.IsRequired = true;
            this.Roles.AccountNumber.IsRequired = true;
            this.Roles.Name.IsRequired = true;
            this.Roles.BalanceSheetAccount.IsRequired = true;
            this.Roles.Side.IsRequired = true;
            this.Roles.GeneralLedgerAccountType.IsRequired = true;
            this.Roles.GeneralLedgerAccountGroup.IsRequired = true;
        }
	}
}