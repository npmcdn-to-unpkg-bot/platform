namespace Allors.Meta
{
    public partial class LegalFormClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}