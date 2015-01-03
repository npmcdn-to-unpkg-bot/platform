namespace Allors.Meta
{
	using System;

	public partial class InvestmentAccountClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Name.IsRequired = true;
	    }
	}
}