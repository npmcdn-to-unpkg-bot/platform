namespace Allors.Meta
{
    public partial class AccountingTransactionDetailClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.Debit.IsRequired = true;
            this.Roles.OrganisationGlAccountBalance.IsRequired = true;
        }
	}
}