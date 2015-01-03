namespace Allors.Meta
{
	using System;

	public partial class UnitOfMeasureConversionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.ToUnitOfMeasure.IsRequired = true;
            this.Roles.ConversionFactor.IsRequired = true;
        }
	}
}