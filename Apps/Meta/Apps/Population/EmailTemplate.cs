namespace Allors.Meta
{
	using System;

	public partial class EmailTemplateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}