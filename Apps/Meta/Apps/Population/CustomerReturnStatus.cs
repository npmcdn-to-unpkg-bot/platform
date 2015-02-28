namespace Allors.Meta
{
    public partial class CustomerReturnStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.CustomerReturnObjectState.IsRequired = true;
		}
	}
}