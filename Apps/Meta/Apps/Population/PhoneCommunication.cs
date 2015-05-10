namespace Allors.Meta
{
    public partial class PhoneCommunicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Caller.IsRequired = true;
            this.Roles.Receiver.IsRequired = true;
            this.Roles.IncomingCall.IsRequired = true;
        }
	}
}