namespace Allors.Meta
{
    public partial class StateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}