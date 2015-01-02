namespace Allors.Meta
{
	public partial class MediaClass
	{
        internal override void BaseExtend()
        {
            this.Roles.MediaType.IsRequired = true;
            this.Roles.MediaContent.IsRequired = true;
        }
	}
}