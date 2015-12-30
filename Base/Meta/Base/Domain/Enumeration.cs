namespace Allors.Meta
{
    public partial class MetaEnumeration
    {
        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;
            this.IsActive.RoleType.IsRequired = true;
        }
    }
}