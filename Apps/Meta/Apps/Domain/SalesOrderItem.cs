namespace Allors.Meta
{
	using System;

	public partial class SalesOrderItemClass
	{
        #region Allors
        [Id("F04381CD-3B28-4DD5-BBE8-873C5A56AEE2")]
        #endregion
        public MethodType Continue;

	    internal override void AppsExtend()
	    {
            this.UnitPurchasePrice.RoleType.IsRequired = true;
            this.InitialMarkupPercentage.RoleType.IsRequired = true;
            this.InitialProfitMargin.RoleType.IsRequired = true;
            this.MaintainedProfitMargin.RoleType.IsRequired = true;
            this.MaintainedMarkupPercentage.RoleType.IsRequired = true;
            
			this.QuantityShortFalled.RoleType.IsRequired = true;
			this.QuantityShipped.RoleType.IsRequired = true;
			this.QuantityPicked.RoleType.IsRequired = true;
			this.QuantityReserved.RoleType.IsRequired = true;
			this.QuantityPendingShipment.RoleType.IsRequired = true;
			this.QuantityRequestsShipping.RoleType.IsRequired = true;
            this.QuantityReturned.RoleType.IsRequired = true;
            this.QuantityShipped.RoleType.IsRequired = true;
            
            this.CurrentObjectState.RoleType.IsRequired = true;

	        this.ConcreteRoles.QuantityOrdered.IsRequiredOverride = true;
	    }
	}
}