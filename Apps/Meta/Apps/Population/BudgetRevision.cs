namespace Allors.Meta
{
	using System;

	public partial class BudgetRevisionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.RevisionDate.IsRequired = true;
        }
	}
}