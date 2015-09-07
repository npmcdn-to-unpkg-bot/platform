namespace Allors.Meta
{
    public partial class AddendumClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.CreationDate.IsRequired = true;
            this.Roles.Description.IsRequired = true;
		}
	}
}