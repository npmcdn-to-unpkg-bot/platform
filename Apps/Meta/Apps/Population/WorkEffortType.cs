namespace Allors.Meta
{
	using System;

	public partial class WorkEffortTypeClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.Description.IsRequired = true;
	        this.Roles.WorkEffortTypeKind.IsRequired = true;
	    }
	}
}