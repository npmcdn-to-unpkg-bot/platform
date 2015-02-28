namespace Allors.Meta
{
    public partial class BrandClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}