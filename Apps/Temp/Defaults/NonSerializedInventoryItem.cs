namespace Allors.Meta
{
	using System;

	public partial class NonSerializedInventoryItemClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.QuantityCommittedOut.IsRequired = true;
			this.Roles.QuantityOnHand.IsRequired = true;
			this.Roles.PreviousQuantityOnHand.IsRequired = true;
			this.Roles.QuantityExpectedIn.IsRequired = true;

		}
	}
}