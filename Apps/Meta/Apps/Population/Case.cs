namespace Allors.Meta
{
    public partial class CaseClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
            this.Roles.CurrentObjectState.IsRequired = true;
        }
	}
}