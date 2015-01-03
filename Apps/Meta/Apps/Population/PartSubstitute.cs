namespace Allors.Meta
{
	using System;

	public partial class PartSubstituteClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Part.IsRequired = true;
            this.Roles.SubstitutionPart.IsRequired = true;
            this.Roles.Quantity.IsRequired = true;
        }
	}
}