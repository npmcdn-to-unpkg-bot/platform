namespace Allors.Meta
{
    public partial class SalesRepCommissionClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Year.IsRequired = true;
	    }
	}
}