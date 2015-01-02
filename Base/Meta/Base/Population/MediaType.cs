namespace Allors.Meta
{
	public partial class MediaTypeClass
	{
        internal override void BaseExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
    }
}