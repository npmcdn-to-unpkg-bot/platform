namespace Allors.Meta
{
	using System;

	public partial class WorkEffortGoodStandardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Good.IsRequired = true;
        }
	}
}