namespace Allors.Meta
{
    public partial class MetaStringTemplate
    {
        internal override void BaseExtend()
        {
            this.Name.IsRequired = true;

            this.Locale.IsRequiredOverride = true;
        }
    }
}