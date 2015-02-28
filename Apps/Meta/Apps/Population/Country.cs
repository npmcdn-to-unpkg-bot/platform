namespace Allors.Meta
{
    public partial class CountryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}