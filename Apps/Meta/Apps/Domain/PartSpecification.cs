namespace Allors.Meta
{
    public partial class PartSpecificationInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.CurrentObjectState.IsRequired = true;
        }
	}
}