namespace Allors.Meta
{
    public partial class MetaMedia
    {
        internal override void BaseExtend()
        {
            this.MediaType.RoleType.IsRequired = true;
            this.MediaContent.RoleType.IsRequired = true;
        }
    }
}