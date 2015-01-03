namespace Allors.Meta
{
	using System;

	public partial class DimensionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.UnitOfMeasure.IsRequired = true;
        }
	}
}