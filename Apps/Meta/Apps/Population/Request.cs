namespace Allors.Meta
{
    public partial class RequestInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}