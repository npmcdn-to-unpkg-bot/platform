namespace Allors.Meta
{
	using System;

	public partial class TelecommunicationsNumberClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ContactNumber.IsRequired = true;
        }
	}
}