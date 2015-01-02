namespace Allors.Meta
{
	public partial class MediaContentClass
	{
        internal override void BaseExtend()
        {
            this.Roles.Value.IsRequired = true;
        }
	}
}