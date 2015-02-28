namespace Allors.Meta
{
    public partial class VatRateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Rate.IsRequired = true;
        }
	}
}