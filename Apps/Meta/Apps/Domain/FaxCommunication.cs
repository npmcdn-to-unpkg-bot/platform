namespace Allors.Meta
{
    public partial class FaxCommunicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Originator.IsRequired = true;
            this.Roles.Receiver.IsRequired = true;
		}
	}
}