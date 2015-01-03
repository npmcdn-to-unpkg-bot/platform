namespace Allors.Meta
{
	using System;

	public partial class OrganisationGlAccountClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.HasBankStatementTransactions.IsRequired = true;

		}
	}
}