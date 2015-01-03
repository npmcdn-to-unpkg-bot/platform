namespace Allors.Meta
{
	using System;

	public partial class QuoteInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}