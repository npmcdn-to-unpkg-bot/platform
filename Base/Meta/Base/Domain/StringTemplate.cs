namespace Allors.Meta
{
    public partial class MetaStringTemplate
    {
        internal override void BaseExtend()
        {
            this.Name.RoleType.IsRequired = true;

            var locale = this.ConcreteRoleTypeByRoleType[LocalisedInterface.Instance.Locale.RoleType];
            locale.IsRequiredOverride = true;
        }
    }
}