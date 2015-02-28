namespace Allors.Meta
{
    public partial class CommunicationEventStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.CommunicationEventObjectState.IsRequired = true;
        }
	}
}