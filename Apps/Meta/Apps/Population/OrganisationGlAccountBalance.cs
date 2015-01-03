namespace Allors.Meta
{
	using System;

	public partial class OrganisationGlAccountBalanceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.OrganisationGlAccount.IsRequired = true;
        }
	}
}