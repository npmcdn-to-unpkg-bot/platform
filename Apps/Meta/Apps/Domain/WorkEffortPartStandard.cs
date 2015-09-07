namespace Allors.Meta
{
    public partial class WorkEffortPartStandardClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Part.IsRequired = true;
        }
	}
}