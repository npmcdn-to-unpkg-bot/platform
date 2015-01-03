namespace Allors.Meta
{
	using System;

	public partial class ProductConfigurationClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}