namespace Allors.Meta
{
	using System;

	public partial class FacilityInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}