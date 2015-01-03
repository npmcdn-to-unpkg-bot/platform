namespace Allors.Meta
{
	using System;

	public partial class RegionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}