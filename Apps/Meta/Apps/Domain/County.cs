namespace Allors.Meta
{
    public partial class CountyClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}