namespace Allors.Meta
{
	using System;

	public partial class BankAccountClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Iban.IsRequired = true;
        }
	}
}