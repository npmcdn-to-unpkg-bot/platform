namespace Allors.Meta
{
    public partial class StringTemplateClass
	{
	    internal override void AppsExtend()
	    {
	        this.Roles.TemplatePurpose.IsRequired = true;
	    }
	}
}