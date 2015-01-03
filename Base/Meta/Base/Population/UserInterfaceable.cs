namespace Allors.Meta
{
	using System;

	public partial class UserInterfaceableInterface
	{
	    internal override void BaseExtend()
	    {
	        this.Roles.DisplayName.IsRequired = true;
	    }
	}
}