namespace Allors.Meta
{
	using System;

	public partial class DeliverableClass
	{
	    internal override void AppsExtend()
        {
            this.Roles.Name.IsRequired = true;
        }
	}
}