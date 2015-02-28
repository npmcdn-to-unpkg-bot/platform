namespace Allors.Meta
{
    public partial class PartyBenefitClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employment.IsRequired = true;
            this.Roles.Benefit.IsRequired = true;
        }
	}
}