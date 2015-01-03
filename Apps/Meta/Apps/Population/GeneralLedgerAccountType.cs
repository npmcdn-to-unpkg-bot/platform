namespace Allors.Meta
{
	using System;

	public partial class GeneralLedgerAccountTypeClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}