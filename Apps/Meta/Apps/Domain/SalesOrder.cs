namespace Allors.Meta
{
    public partial class SalesOrderClass
	{
	    internal override void AppsExtend()
        {
			this.TotalPurchasePrice.RoleType.IsRequired = true;
			this.TotalListPriceCustomerCurrency.RoleType.IsRequired = true;
			this.MaintainedProfitMargin.RoleType.IsRequired = true;
			this.InitialProfitMargin.RoleType.IsRequired = true;
			this.TotalListPrice.RoleType.IsRequired = true;
			this.MaintainedMarkupPercentage.RoleType.IsRequired = true;
			this.InitialMarkupPercentage.RoleType.IsRequired = true;

            this.CurrentObjectState.RoleType.IsRequired = true;
            this.TakenByInternalOrganisation.RoleType.IsRequired = true;
            this.Store.RoleType.IsRequired = true;
            this.PartiallyShip.RoleType.IsRequired = true;

            this.ConcreteRoles.CustomerCurrency.IsRequiredOverride = true;
            this.ConcreteRoles.DeliveryDate.IsRequiredOverride = true;
        }
	}
}