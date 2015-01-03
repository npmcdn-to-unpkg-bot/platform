namespace Allors.Meta
{
	using System;

	public partial class AutomatedAgentClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}