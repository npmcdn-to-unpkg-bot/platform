namespace Allors.Meta
{
    public partial class ContainerInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.ContainerDescription.IsRequired = true;
        }
	}
}