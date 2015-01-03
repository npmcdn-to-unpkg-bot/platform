namespace Allors.Meta
{
	using System;

	public partial class PositionTypeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}