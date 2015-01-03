namespace Allors.Meta
{
	using System;

	public partial class UserInterfaceableInterface
	{
	    internal override void AppsExtend()
        {
			this.Roles.DisplayName.IsRequired = true;

		}
	}
}