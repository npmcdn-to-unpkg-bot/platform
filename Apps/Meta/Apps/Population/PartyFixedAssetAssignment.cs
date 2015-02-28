namespace Allors.Meta
{
    public partial class PartyFixedAssetAssignmentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.FixedAsset.IsRequired = true;
            this.Roles.Party.IsRequired = true;
        }
	}
}