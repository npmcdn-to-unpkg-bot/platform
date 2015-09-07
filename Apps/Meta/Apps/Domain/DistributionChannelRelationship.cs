namespace Allors.Meta
{
    public partial class DistributionChannelRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Distributor.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
        }
	}
}