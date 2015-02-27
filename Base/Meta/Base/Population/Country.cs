namespace Allors.Meta
{
    public partial class CountryClass
    {
        internal override void BaseExtend()
        {
            this.Roles.IsoCode.IsRequired = true;
            this.Roles.IsoCode.IsUnique = true;
            this.Roles.Name.IsRequired = true;
        }
    }
}