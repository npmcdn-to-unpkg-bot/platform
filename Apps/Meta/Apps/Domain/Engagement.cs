namespace Allors.Meta
{
    public partial class EngagementClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.BillToParty.IsRequired = true;
            this.Roles.TakenViaInternalOrganisation.IsRequired = true;
            this.Roles.BillToContactMechanism.IsRequired = true;
        }
	}
}