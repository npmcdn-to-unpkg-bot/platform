namespace Allors.Meta
{
    public partial class EngineeringChangeClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}