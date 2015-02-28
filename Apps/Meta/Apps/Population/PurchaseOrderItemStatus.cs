namespace Allors.Meta
{
    public partial class PurchaseOrderItemStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PurchaseOrderItemObjectState.IsRequired = true;
        }
	}
}