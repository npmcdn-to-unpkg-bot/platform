namespace Allors.Meta
{
    public partial class UniquelyIdentifiableInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.UniqueId.IsRequired = true;

		}
	}
}