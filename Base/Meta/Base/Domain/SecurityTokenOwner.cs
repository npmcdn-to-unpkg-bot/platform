namespace Allors.Meta
{
    public partial class MetaSecurityTokenOwner
    {
        internal override void BaseExtend()
        {
            this.OwnerSecurityToken.RoleType.IsRequired = true;
            //this.OwnerAccessControl.RoleType.IsRequired = true;
        }
    }
}