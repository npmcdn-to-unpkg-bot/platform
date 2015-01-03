namespace Allors.Meta
{
	using System;

	public partial class ChartOfAccountsClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}