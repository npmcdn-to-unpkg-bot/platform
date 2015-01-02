namespace Allors.Meta
{
	public partial class CurrencyClass
	{
        internal override void BaseExtend()
        {
            this.Roles.IsoCode.IsRequired = true;
            this.Roles.Name.IsRequired = true;
            this.Roles.Symbol.IsRequired = true;
        }
    }
}