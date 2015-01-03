namespace Allors.Meta
{
	using System;

	public partial class VatRateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Rate.IsRequired = true;
        }
	}
}