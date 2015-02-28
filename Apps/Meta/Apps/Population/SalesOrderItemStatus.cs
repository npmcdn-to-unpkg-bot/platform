namespace Allors.Meta
{
    public partial class SalesOrderItemStatusClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.StartDateTime.IsRequired = true;
            this.Roles.SalesOrderItemObjectState.IsRequired = true;
        }
	}
}