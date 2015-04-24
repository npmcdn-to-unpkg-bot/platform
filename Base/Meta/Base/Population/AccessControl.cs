namespace Allors.Meta
{
    public partial class AccessControlClass
    {
        internal override void BaseExtend()
        {
            this.Roles.CacheId.IsRequired = true;
            this.Roles.Role.IsRequired = true;
            this.Roles.Object.IsRequired = true;
        }
    }
}