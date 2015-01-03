namespace Allors.Meta
{
	using System;

	public partial class PartBillOfMaterialInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.QuantityUsed.IsRequired = true;
            this.Roles.ComponentPart.IsRequired = true;
            this.Roles.Part.IsRequired = true;
        }
	}
}