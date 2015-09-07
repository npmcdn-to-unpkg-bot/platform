namespace Allors.Meta
{
	using System;

	public partial class SalesInvoiceClass
	{
	    internal override void AppsExtend()
        {
            new MethodType(AppsDomain.Instance, new Guid("1E1B769E-6E07-4F75-8620-E6308558329B")){ObjectType=this, Name="Send"};
            new MethodType(AppsDomain.Instance, new Guid("9B314F84-7D49-45F7-9F7C-D419DCE445EE")){ObjectType=this, Name="CancelInvoice"};
            new MethodType(AppsDomain.Instance, new Guid("7E5BD6D4-A4D7-4648-90E6-3398CE6FF3FE")){ObjectType=this, Name="WriteOff"};

			this.InitialProfitMargin.RoleType.IsRequired = true;
			this.InitialMarkupPercentage.RoleType.IsRequired = true;
			this.MaintainedMarkupPercentage.RoleType.IsRequired = true;
			this.MaintainedProfitMargin.RoleType.IsRequired = true;
			this.TotalListPriceCustomerCurrency.RoleType.IsRequired = true;

            this.SalesInvoiceType.RoleType.IsRequired = true;
            this.BilledFromInternalOrganisation.RoleType.IsRequired = true;
            this.Store.RoleType.IsRequired = true;
            this.BillToCustomer.RoleType.IsRequired = true;
            this.BillToContactMechanism.RoleType.IsRequired = true;

            this.CurrentObjectState.RoleType.IsRequired = true;
            
            this.ConcreteRoles.AmountPaid.IsRequiredOverride = true;
        }
	}
}