namespace Allors.Meta
{
    public partial class MediaContentClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.Value.IsRequired = true;
			this.Roles.Hash.IsRequired = true;

		}
	}
}