namespace Allors.Meta
{
	using System;

	public partial class SerializedInventoryItemStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.SerializedInventoryItemObjectState.IsRequired = true;
        }
	}
}