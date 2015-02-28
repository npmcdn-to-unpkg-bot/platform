namespace Allors.Meta
{
    public partial class GeneralLedgerAccountGroupClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}