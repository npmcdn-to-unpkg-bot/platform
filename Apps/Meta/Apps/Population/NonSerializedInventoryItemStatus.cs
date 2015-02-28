namespace Allors.Meta
{
    public partial class NonSerializedInventoryItemStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.NonSerializedInventoryItemObjectState.IsRequired = true;
        }
	}
}