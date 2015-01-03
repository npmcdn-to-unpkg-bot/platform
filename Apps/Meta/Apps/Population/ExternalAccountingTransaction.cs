namespace Allors.Meta
{
	using System;

	public partial class ExternalAccountingTransactionInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.FromParty.IsRequired = true;
            this.Roles.ToParty.IsRequired = true;
        }
	}
}