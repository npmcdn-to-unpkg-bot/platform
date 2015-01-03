namespace Allors.Meta
{
	using System;

	public partial class SalesRepCommissionClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Year.IsRequired = true;
	    }
	}
}