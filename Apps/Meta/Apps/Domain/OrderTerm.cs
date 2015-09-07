namespace Allors.Meta
{
    public partial class OrderTermClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.TermType.IsRequired = true;
        }
	}
}