namespace Allors.Meta
{
    public partial class MetaMediaContent
    {
        internal override void BaseExtend()
        {
            this.Value.RoleType.IsRequired = true;
        }
    }
}