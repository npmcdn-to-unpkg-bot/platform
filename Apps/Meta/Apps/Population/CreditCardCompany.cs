namespace Allors.Meta
{
	using System;

	public partial class CreditCardCompanyClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}