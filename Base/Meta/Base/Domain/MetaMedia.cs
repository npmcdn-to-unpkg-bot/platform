namespace Allors.Meta
{
    public partial class MetaMedia
    {
        internal override void BaseExtend()
        {
            this.MediaType.IsRequired = true;
            this.MediaContent.IsRequired = true;
        }
    }
}