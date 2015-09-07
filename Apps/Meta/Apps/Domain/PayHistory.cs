namespace Allors.Meta
{
    public partial class PayHistoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Employment.IsRequired = true;
            this.Roles.TimeFrequency.IsRequired = true;
        }
	}
}