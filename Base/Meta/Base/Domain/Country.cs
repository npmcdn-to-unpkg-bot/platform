namespace Allors.Meta
{
    public partial class MetaCountry
    {
        internal override void BaseExtend()
        {
            this.IsoCode.RoleType.IsRequired = true;
            this.IsoCode.RoleType.IsUnique = true;
            this.Name.RoleType.IsRequired = true;
        }
    }
}