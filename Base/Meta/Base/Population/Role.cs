namespace Allors.Meta
{
	public partial class RoleClass : Class
	{
        internal override void BaseExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
    }
}