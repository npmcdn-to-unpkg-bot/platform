namespace Allors.Meta
{
	using System;

	public partial class OwnCreditCardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.CreditCard.IsRequired = true;
        }
	}
}