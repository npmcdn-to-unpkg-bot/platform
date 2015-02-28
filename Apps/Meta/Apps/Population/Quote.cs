namespace Allors.Meta
{
    public partial class QuoteInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}