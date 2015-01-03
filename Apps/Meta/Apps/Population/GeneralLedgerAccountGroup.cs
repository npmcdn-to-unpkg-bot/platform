namespace Allors.Meta
{
	using System;

	public partial class GeneralLedgerAccountGroupClass
	{
	    internal override void AppsExtend()
        {
            this.ConcreteRoles.Description.IsRequiredOverride = true;
        }
	}
}