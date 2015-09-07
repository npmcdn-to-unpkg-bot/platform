namespace Allors.Meta
{
    public partial class PrintableInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.PrintContent.IsRequired = true;

		}
	}
}