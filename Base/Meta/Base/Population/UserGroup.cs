namespace Allors.Meta
{
    public partial class UserGroupClass
    {
        internal override void BaseExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.Roles.Name.IsUnique = true;
        }
    }
}