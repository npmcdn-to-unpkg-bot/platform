namespace Allors.Meta
{
	using System;

	public partial class EngineeringChangeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}