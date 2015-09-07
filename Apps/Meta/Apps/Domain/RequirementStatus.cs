namespace Allors.Meta
{
    public partial class RequirementStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.RequirementObjectState.IsRequired = true;
        }
	}
}