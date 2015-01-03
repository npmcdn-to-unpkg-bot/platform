namespace Allors.Meta
{
	using System;

	public partial class ProductCategoryClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}