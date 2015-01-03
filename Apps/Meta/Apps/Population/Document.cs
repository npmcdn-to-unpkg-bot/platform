namespace Allors.Meta
{
	using System;

	public partial class DocumentInterface
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}