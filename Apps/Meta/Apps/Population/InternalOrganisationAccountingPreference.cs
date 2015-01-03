namespace Allors.Meta
{
	using System;

	public partial class InternalOrganisationAccountingPreferenceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.InternalOrganisation.IsRequired = true;
        }
	}
}