namespace Allors.Meta
{
    public partial class InvestmentAccountClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Name.IsRequired = true;
	    }
	}
}