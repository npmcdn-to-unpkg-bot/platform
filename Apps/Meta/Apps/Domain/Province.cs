namespace Allors.Meta
{
    public partial class ProvinceClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}