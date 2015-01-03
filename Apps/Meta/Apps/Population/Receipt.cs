namespace Allors.Meta
{
	using System;

	public partial class ReceiptClass
	{
	    internal override void AppsExtend()
	    {
	        this.ConcreteRoles.EffectiveDate.IsRequiredOverride = true;
	    }
	}
}