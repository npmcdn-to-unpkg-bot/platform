namespace Allors.Meta
{
	using System;

	public partial class CityClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}