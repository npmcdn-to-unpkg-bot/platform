namespace Allors.Meta
{
	using System;

	public partial class CitizenshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Country.IsRequired = true;
        }
	}
}