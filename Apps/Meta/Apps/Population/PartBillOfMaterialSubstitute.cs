namespace Allors.Meta
{
	using System;

	public partial class PartBillOfMaterialSubstituteClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.PartBillOfMaterial.IsRequired = true;
            this.Roles.SubstitutionPartBillOfMaterial.IsRequired = true;
        }
	}
}