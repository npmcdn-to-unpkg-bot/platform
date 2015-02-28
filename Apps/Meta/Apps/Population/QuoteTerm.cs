namespace Allors.Meta
{
    public partial class QuoteTermClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.TermType.IsRequired = true;
        }
	}
}