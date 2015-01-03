namespace Allors.Meta
{
	using System;

	public partial class CustomerRelationshipClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.AmountDue.IsRequired = true;
			this.Roles.YTDRevenue.IsRequired = true;
			this.Roles.LastYearsRevenue.IsRequired = true;

		}
	}
}