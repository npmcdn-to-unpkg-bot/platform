namespace Allors.Meta
{
	using System;

	public partial class PackageClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}