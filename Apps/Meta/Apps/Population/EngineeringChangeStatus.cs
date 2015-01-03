namespace Allors.Meta
{
	using System;

	public partial class EngineeringChangeStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.EngineeringChangeObjectState.IsRequired = true;
        }
	}
}