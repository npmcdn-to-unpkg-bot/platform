namespace Allors.Meta
{
    public partial class OrganisationClass
    {
        internal override void AppsExtend()
        {
            this.Name.RoleType.IsRequired = true;
            
            this.ConcreteRoles.Locale.IsRequiredOverride = true;
        }
    }
}