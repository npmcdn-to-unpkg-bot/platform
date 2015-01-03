namespace Allors.Meta
{
	using System;

	public partial class PeriodInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.FromDate.IsRequired = true;
        }
	}
}