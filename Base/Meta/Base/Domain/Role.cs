namespace Allors.Meta
{
    public partial class MetaRole
    {
        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;
            this.Name.RoleType.IsUnique = true;
        }
    }
}