namespace Allors.Meta
{
    public partial class MetaPrintable
    {
        internal override void BaseExtend()
        {
            this.PrintContent.RoleType.IsRequired = true;
        }
    }
}