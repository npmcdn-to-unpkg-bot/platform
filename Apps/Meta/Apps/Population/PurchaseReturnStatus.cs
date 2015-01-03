namespace Allors.Meta
{
	using System;

	public partial class PurchaseReturnStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PurchaseReturnObjectState.IsRequired = true;
        }
	}
}