namespace Allors.Meta
{
	using System;

	public partial class PurchaseInvoiceClass
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("ECD12D89-5B32-416C-8478-06FF904C6A61")){ObjectType=this, Name="Ready"};
            new MethodType(AppsDomain.Instance, new Guid("16C0CC36-B908-4912-B420-2FD3E31BB966")){ObjectType=this, Name="Approve"};
            new MethodType(AppsDomain.Instance, new Guid("46BB5168-5250-4B5A-9DF0-045AFB589AAD")){ObjectType=this, Name="Cancel"};

	        this.Roles.PurchaseInvoiceType.IsRequired = true;
            this.Roles.BilledToInternalOrganisation.IsRequired = true;
            this.Roles.BilledFromParty.IsRequired = true;

            this.Roles.CurrentObjectState.IsRequired = true;
        }
	}
}