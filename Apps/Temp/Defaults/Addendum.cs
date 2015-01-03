namespace Allors.Meta
{
	using System;

	public partial class AddendumClass
	{
	    internal override void AppsExtend()
        {
			this.Roles.CreationDate.IsRequired = true;

		}
	}
}