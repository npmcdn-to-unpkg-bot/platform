namespace Allors.Meta
{
    public partial class InvoiceTermClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.TermType.IsRequiredOverride = true;
        }
	}
}