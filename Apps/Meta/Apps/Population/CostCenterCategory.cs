namespace Allors.Meta
{
	using System;

	public partial class CostCenterCategoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}