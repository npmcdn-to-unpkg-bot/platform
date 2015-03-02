namespace Allors.Meta
{
    public partial class StoreClass
    {
        internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.Roles.PaymentNetDays.IsRequired = true;
            this.Roles.PaymentGracePeriod.IsRequired = true;
            this.Roles.CreditLimit.IsRequired = true;
            this.Roles.ShipmentThreshold.IsRequired = true;
            this.Roles.OrderThreshold.IsRequired = true;
            this.Roles.SalesInvoiceNumberPrefix.IsRequired = true;

            this.Roles.DefaultPaymentMethod.IsRequired = true;
            this.Roles.DefaultShipmentMethod.IsRequired = true;
            this.Roles.DefaultCarrier.IsRequired = true;
        }
    }
}