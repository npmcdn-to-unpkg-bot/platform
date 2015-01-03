namespace Allors.Meta
{
	using System;

	public partial class PartyPackageRevenueClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Year.IsRequired = true;
            this.Roles.Month.IsRequired = true;
            this.Roles.Revenue.IsRequired = true;
        }
	}
}