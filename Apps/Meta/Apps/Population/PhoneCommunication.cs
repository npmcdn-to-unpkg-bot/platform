namespace Allors.Meta
{
	using System;

	public partial class PhoneCommunicationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Caller.IsRequired = true;
            this.Roles.Receiver.IsRequired = true;
		}
	}
}