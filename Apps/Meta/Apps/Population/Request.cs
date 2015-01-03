namespace Allors.Meta
{
	using System;

	public partial class RequestInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Description.IsRequired = true;
        }
	}
}