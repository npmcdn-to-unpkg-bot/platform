namespace Allors.Meta
{
	using System;

	public partial class SalesOrderStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.SalesOrderObjectState.IsRequired = true;
        }
	}
}