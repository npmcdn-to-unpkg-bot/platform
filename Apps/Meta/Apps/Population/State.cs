namespace Allors.Meta
{
	using System;

	public partial class StateClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}