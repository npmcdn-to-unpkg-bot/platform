namespace Allors.Meta
{
    public partial class OrganisationRollUpClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Child.IsRequired = true;
            this.Roles.Parent.IsRequired = true;
            this.Roles.RollupKind.IsRequired = true;
        }
	}
}