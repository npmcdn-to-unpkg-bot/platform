namespace Allors.Meta
{
    public partial class ReceiptClass
	{
	    internal override void AppsExtend()
	    {
	        this.ConcreteRoles.EffectiveDate.IsRequiredOverride = true;
	    }
	}
}