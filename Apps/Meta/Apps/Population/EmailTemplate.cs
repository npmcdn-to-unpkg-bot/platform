namespace Allors.Meta
{
    public partial class EmailTemplateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}