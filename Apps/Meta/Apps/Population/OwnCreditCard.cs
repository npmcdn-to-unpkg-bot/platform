namespace Allors.Meta
{
    public partial class OwnCreditCardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CreditCard.IsRequired = true;
        }
	}
}