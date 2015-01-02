namespace Allors.Meta
{
	public partial class PeriodInterface
	{
        internal override void BaseExtend()
        {
            this.Roles.FromDate.IsRequired = true;
        }
    }
}