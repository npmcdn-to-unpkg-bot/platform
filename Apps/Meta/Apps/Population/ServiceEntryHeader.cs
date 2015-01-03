namespace Allors.Meta
{
	using System;

	public partial class ServiceEntryHeaderClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.SubmittedDate.IsRequired = true;
            this.Roles.SubmittedBy.IsRequired = true;
        }
	}
}