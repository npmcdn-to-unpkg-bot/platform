namespace Allors.Meta
{
    public partial class EnumerationInterface
	{
        internal override void BaseExtend()
        {
            this.Roles.Name.IsRequired = true;
            this.Roles.IsActive.IsRequired = true;
        }
    }
}