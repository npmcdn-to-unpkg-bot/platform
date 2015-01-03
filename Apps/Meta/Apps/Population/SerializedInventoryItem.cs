namespace Allors.Meta
{
	using System;

	public partial class SerializedInventoryItemClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.SerialNumber.IsRequired = true;
        }
	}
}