namespace Allors.Meta
{
    public partial class OrganisationGlAccountClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.HasBankStatementTransactions.IsRequired = true;

            this.Roles.GeneralLedgerAccount.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
		}
	}
}