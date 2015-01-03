namespace Allors.Meta
{
	using System;

	public partial class SupplierRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Supplier.IsRequired = true;
            this.Roles.InternalOrganisation.IsRequired = true;
            this.Roles.SubAccountNumber.IsRequired = true;
        }
	}
}