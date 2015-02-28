namespace Allors.Meta
{
    public partial class ExternalAccountingTransactionInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.FromParty.IsRequired = true;
            this.Roles.ToParty.IsRequired = true;
        }
	}
}