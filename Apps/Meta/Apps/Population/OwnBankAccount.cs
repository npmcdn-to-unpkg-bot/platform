namespace Allors.Meta
{
	using System;

	public partial class OwnBankAccountClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.BankAccount.IsRequired = true;
        }
	}
}