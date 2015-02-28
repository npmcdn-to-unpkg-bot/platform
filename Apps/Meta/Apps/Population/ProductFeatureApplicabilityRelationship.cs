namespace Allors.Meta
{
    public partial class ProductFeatureApplicabilityRelationshipClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.AvailableFor.IsRequired = true;
            this.Roles.UsedToDefine.IsRequired = true;
		}
	}
}