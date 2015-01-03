namespace Allors.Meta
{
	using System;

	public partial class SalesOrderClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.TotalPurchasePrice.IsRequired = true;
			this.Roles.TotalListPriceCustomerCurrency.IsRequired = true;
			this.Roles.MaintainedProfitMargin.IsRequired = true;
			this.Roles.InitialProfitMargin.IsRequired = true;
			this.Roles.TotalListPrice.IsRequired = true;
			this.Roles.MaintainedMarkupPercentage.IsRequired = true;
			this.Roles.InitialMarkupPercentage.IsRequired = true;

            this.Roles.CurrentObjectState.IsRequired = true;
            this.Roles.TakenByInternalOrganisation.IsRequired = true;
            this.Roles.Store.IsRequired = true;
            this.Roles.PartiallyShip.IsRequired = true;

            this.ConcreteRoles.CustomerCurrency.IsRequiredOverride = true;
            this.ConcreteRoles.DeliveryDate.IsRequiredOverride = true;
        }
	}
}