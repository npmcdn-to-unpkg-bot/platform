namespace Allors.Meta
{
	using System;

	public partial class PositionTypeRateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Rate.IsRequired = true;
            this.Roles.RateType.IsRequired = true;
            this.Roles.TimeFrequency.IsRequired = true;
        }
	}
}