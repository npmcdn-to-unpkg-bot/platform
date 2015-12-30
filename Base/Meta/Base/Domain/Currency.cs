namespace Allors.Meta
{
    public partial class MetaCurrency
    {
        internal override void BaseExtend()
        {
            this.IsoCode.RoleType.IsRequired = true;
            this.Name.RoleType.IsRequired = true;
            this.Symbol.RoleType.IsRequired = true;
        }
    }
}