namespace Allors.Meta
{
	using System;

	public partial class CostCenterClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}