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

            this.Roles.Customer.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
            this.Roles.SubAccountNumber.IsRequired = true;
            this.Roles.AmountOverDue.IsRequired = true;
		}
	}
}