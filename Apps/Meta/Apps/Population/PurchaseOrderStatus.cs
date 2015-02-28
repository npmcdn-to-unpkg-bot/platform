namespace Allors.Meta
{
    public partial class PurchaseOrderStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PurchaseOrderObjectState.IsRequired = true;
        }
	}
}