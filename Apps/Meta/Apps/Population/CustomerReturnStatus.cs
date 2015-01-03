namespace Allors.Meta
{
	using System;

	public partial class CustomerReturnStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.CustomerReturnObjectState.IsRequired = true;
		}
	}
}