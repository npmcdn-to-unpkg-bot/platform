namespace Allors.Meta
{
	using System;

	public partial class PartyProductRevenueClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Year.IsRequired = true;
            this.Roles.Month.IsRequired = true;
            this.Roles.Revenue.IsRequired = true;
            this.Roles.Quantity.IsRequired = true;
        }
	}
}