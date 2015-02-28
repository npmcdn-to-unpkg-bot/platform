namespace Allors.Meta
{
    public partial class PartnershipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Partner.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
		}
	}
}