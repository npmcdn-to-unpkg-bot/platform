namespace Allors.Meta
{
	public partial class LanguageClass
	{
        internal override void BaseExtend()
        {
            this.Roles.IsoCode.IsRequired = true;
            this.Roles.Name.IsRequired = true;
        }
    }
}