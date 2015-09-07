namespace Allors.Meta
{
    public partial class PositionClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.PositionType.IsRequired = true;
            this.Roles.Organisation.IsRequired = true;
            this.Roles.ActualFromDate.IsRequired = true;
        }
	}
}