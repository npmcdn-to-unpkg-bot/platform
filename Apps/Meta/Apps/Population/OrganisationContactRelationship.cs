namespace Allors.Meta
{
    public partial class OrganisationContactRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Contact.IsRequired = true;
            this.Roles.Organisation.IsRequired = true;
        }
	}
}