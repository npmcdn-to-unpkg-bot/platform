namespace Allors.Meta
{
    public partial class CaseStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.CaseObjectState.IsRequired = true;
        }
	}
}