namespace Allors.Meta
{
    public partial class PositionResponsibilityClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Position.IsRequired = true;
            this.Roles.Responsibility.IsRequired = true;
        }
	}
}