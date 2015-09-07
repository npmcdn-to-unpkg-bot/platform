namespace Allors.Meta
{
    public partial class BillingAccountClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Description.IsRequired = true;
	    }
	}
}