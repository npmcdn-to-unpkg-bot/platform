namespace Allors.Meta
{
	using System;

	public partial class InvoiceTermClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.TermType.IsRequiredOverride = true;
        }
	}
}