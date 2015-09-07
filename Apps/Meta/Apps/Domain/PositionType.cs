namespace Allors.Meta
{
    public partial class PositionTypeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}