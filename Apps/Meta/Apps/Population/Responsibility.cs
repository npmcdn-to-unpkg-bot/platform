namespace Allors.Meta
{
    public partial class ResponsibilityClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}