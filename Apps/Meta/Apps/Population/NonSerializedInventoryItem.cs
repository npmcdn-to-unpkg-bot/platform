namespace Allors.Meta
{
    public partial class NonSerializedInventoryItemClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.QuantityCommittedOut.IsRequired = true;
			this.Roles.QuantityOnHand.IsRequired = true;
			this.Roles.PreviousQuantityOnHand.IsRequired = true;
			this.Roles.QuantityExpectedIn.IsRequired = true;

            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.AvailableToPromise.IsRequired = true;
        }
	}
}