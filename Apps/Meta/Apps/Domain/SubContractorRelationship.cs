namespace Allors.Meta
{
    public partial class SubContractorRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Contractor.IsRequired = true;
            this.Roles.SubContractor.IsRequired = true;
		}
	}
}