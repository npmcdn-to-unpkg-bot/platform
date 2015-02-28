namespace Allors.Meta
{
    public partial class PartyContactMechanismClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ContactMechanism.IsRequired = true;
            this.Roles.UseAsDefault.IsRequired = true;
        }
	}
}