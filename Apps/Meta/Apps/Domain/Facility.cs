namespace Allors.Meta
{
    public partial class FacilityInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}