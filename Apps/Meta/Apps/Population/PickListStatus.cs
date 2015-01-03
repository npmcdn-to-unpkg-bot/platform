namespace Allors.Meta
{
	using System;

	public partial class PickListStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PickListObjectState.IsRequired = true;
        }
	}
}