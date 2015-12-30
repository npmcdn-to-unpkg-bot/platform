namespace Allors.Meta
{
    public partial class MetaMediaType
    {
        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;
        }
    }
}