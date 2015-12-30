namespace Allors.Meta
{
    public partial class MetaLanguage
    {
        internal override void BaseExtend()
        {
            this.IsoCode.RoleType.IsRequired = true;
            this.Name.RoleType.IsRequired = true;
        }
    }
}