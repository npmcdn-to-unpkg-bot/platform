namespace Allors.Meta
{
    public partial class MetaUserGroup
    {
        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;
            this.Name.RoleType.IsUnique = true;
        }
    }
}