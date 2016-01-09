namespace Allors.Meta
{
    public partial class MetaMediaType
    {
        internal override void BaseExtend()
        {
            this.Name.IsRequired = true;
        }
    }
}