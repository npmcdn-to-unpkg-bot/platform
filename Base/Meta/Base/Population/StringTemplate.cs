namespace Allors.Meta
{
    public partial class StringTemplateClass
    {
        internal override void BaseExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.ConcreteRoles.Locale.IsRequiredOverride = true;
        }
    }
}