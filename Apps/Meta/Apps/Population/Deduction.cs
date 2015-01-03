namespace Allors.Meta
{
	using System;

	public partial class DeductionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Amount.IsRequired = true;
            this.Roles.DeductionType.IsRequired = true;
        }
	}
}