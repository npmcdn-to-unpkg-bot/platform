namespace Allors.Meta
{
    public partial class MetaEnumeration
    {
        internal override void BaseExtend()
        {
            this.Name.IsRequired = true;
            this.IsActive.IsRequired = true;
        }
    }
}