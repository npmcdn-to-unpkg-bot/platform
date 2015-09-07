namespace Allors.Meta
{
    public partial class GeneralLedgerAccountTypeClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}