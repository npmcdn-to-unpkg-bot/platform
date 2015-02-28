namespace Allors.Meta
{
    public partial class PurchaseReturnStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PurchaseReturnObjectState.IsRequired = true;
        }
	}
}