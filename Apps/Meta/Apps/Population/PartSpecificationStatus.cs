namespace Allors.Meta
{
    public partial class PartSpecificationStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.PartSpecificationObjectState.IsRequired = true;
        }
	}
}