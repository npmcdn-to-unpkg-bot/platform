namespace Allors.Meta
{
    public partial class SalesRepRelationshipClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.LastYearsCommission.IsRequired = true;
			this.Roles.YTDCommission.IsRequired = true;

            this.Roles.Customer.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
            this.Roles.SalesRepresentative.IsRequired = true;
		}
	}
}