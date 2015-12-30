namespace Allors.Meta
{
    public partial class MetaLocalisedText
    {
        internal override void BaseExtend()
        {
            this.Text.RoleType.IsRequired = true;
        }
    }
}