namespace Allors.Meta
{
	using System;

	public partial class PositionResponsibilityClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Position.IsRequired = true;
            this.Roles.Responsibility.IsRequired = true;
        }
	}
}