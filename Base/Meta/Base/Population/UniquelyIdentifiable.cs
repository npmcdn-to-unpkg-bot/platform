namespace Allors.Meta
{
    public partial class UniquelyIdentifiableInterface
    {
        internal override void BaseExtend()
        {
            this.Roles.UniqueId.IsRequired = true;
        }
    }
}