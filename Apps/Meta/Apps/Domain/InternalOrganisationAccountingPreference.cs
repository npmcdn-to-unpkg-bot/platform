namespace Allors.Meta
{
    public partial class InternalOrganisationAccountingPreferenceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.InternalOrganisation.IsRequired = true;
        }
	}
}