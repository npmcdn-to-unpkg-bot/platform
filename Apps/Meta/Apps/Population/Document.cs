namespace Allors.Meta
{
    public partial class DocumentInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}