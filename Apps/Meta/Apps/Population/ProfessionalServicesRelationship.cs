namespace Allors.Meta
{
    public partial class ProfessionalServicesRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Professional.IsRequired = true;
            this.Roles.ProfessionalServicesProvider.IsRequired = true;
        }
	}
}