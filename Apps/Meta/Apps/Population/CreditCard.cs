namespace Allors.Meta
{
	using System;

	public partial class CreditCardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CardNumber.IsRequired = true;
            this.Roles.CreditCardCompany.IsRequired = true;
            this.Roles.ExpirationMonth.IsRequired = true;
            this.Roles.ExpirationYear.IsRequired = true;
            this.Roles.NameOnCard.IsRequired = true;
        }
	}
}