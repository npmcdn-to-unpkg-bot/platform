namespace Allors.Meta
{
    public partial class CreditCardClass
    {
        internal override void AppsExtend()
        {
            this.Roles.CardNumber.IsRequired = true;
            this.Roles.CardNumber.IsUnique = true;
            this.Roles.CreditCardCompany.IsRequired = true;
            this.Roles.ExpirationMonth.IsRequired = true;
            this.Roles.ExpirationYear.IsRequired = true;
            this.Roles.NameOnCard.IsRequired = true;
        }
    }
}