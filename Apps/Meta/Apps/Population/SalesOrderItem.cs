namespace Allors.Meta
{
	using System;

	public partial class SalesOrderItemClass
	{
	    internal override void AppsExtend()
	    {
	        new MethodType(AppsDomain.Instance, new Guid("F04381CD-3B28-4DD5-BBE8-873C5A56AEE2")) { ObjectType = this, Name = "Continue" };

            this.Roles.UnitPurchasePrice.IsRequired = true;
            this.Roles.InitialMarkupPercentage.IsRequired = true;
            this.Roles.InitialProfitMargin.IsRequired = true;
            this.Roles.MaintainedProfitMargin.IsRequired = true;
            this.Roles.MaintainedMarkupPercentage.IsRequired = true;
            
			this.Roles.QuantityShortFalled.IsRequired = true;
			this.Roles.QuantityShipped.IsRequired = true;
			this.Roles.QuantityPicked.IsRequired = true;
			this.Roles.QuantityReserved.IsRequired = true;
			this.Roles.QuantityPendingShipment.IsRequired = true;
			this.Roles.QuantityRequestsShipping.IsRequired = true;
            this.Roles.QuantityReturned.IsRequired = true;
            this.Roles.QuantityShipped.IsRequired = true;
            
            this.Roles.CurrentObjectState.IsRequired = true;

	        this.ConcreteRoles.QuantityOrdered.IsRequiredOverride = true;
	    }
	}
}