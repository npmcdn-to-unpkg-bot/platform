namespace Allors.Meta
{
    public partial class StringTemplateClass
	{
	    internal override void AppsExtend()
	    {
	        this.TemplatePurpose.RoleType.IsRequired = true;
	    }
	}
}