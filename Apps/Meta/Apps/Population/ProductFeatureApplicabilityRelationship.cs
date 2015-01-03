namespace Allors.Meta
{
	using System;

	public partial class ProductFeatureApplicabilityRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.AvailableFor.IsRequired = true;
            this.Roles.UsedToDefine.IsRequired = true;
		}
	}
}