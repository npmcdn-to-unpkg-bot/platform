namespace Allors.Meta
{
    public partial class MetaLocale
    {
        internal override void BaseExtend()
        {
            this.Language.RoleType.IsRequired = true;
            this.Country.RoleType.IsRequired = true;
        }
    }
}