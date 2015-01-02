namespace Allors.Meta
{
	public partial class LocaleClass
	{
        internal override void BaseExtend()
        {
            this.Roles.Language.IsRequired = true;
            this.Roles.Country.IsRequired = true;
        }
    }
}