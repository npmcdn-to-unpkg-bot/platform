namespace Allors.Meta
{
	using System;

	public partial class SalesRepRelationshipClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.LastYearsCommission.IsRequired = true;
			this.Roles.YTDCommission.IsRequired = true;

		}
	}
}