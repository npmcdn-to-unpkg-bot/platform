namespace Allors.Meta
{
    public partial class ServiceTerritoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}