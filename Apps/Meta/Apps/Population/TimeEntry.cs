namespace Allors.Meta
{
    public partial class TimeEntryClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.GrossMargin.IsRequired = true;

            this.Roles.UnitOfMeasure.IsRequired = true;
            this.Roles.Cost.IsRequired = true;
		}
	}
}