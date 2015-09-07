namespace Allors.Meta
{
    public partial class RespondingPartyClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Party.IsRequired = true;
        }
	}
}